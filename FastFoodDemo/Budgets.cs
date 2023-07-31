using FastFoodDemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static FastFoodDemo.MySecondCustmControl;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace FastFoodDemo
{
    public partial class Budgets : Form
    {
        DB mydb = new DB();
        
        private string userID;

        private Main mainForm;

        private Dictionary<string, OrderItem> orderItems;

        private int tableNumber;

        public Budgets(int tableNumber, Main mainForm) 
        {
            InitializeComponent();
            this.tableNumber = tableNumber;
            this.mainForm = mainForm;
            orderItems = new Dictionary<string, OrderItem>();
        }



        public class OrderItem
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
            public decimal InitialPrice { get; set; }
            public decimal TotalPrice { get; set; }
            public DateTime CreatedDate { get; set; }
        }

        public Budgets(string userID)
        {
            InitializeComponent();
            mydb = new DB();
         
            this.userID = userID;

            orderItems = new Dictionary<string, OrderItem>();


        }

        //private void LoadData()
        //{
        //    //try
        //    //{
        //        mydb.openConnection();

        //        string query = "SELECT Date, SUM(CASE WHEN Static = 'Outcome' THEN CONVERT(FLOAT, REPLACE(Price, ' vnđ', '')) ELSE -CONVERT(FLOAT, REPLACE(Price, ' vnđ', '')) END) AS diferen " +
        //              "FROM Item " +
        //              "WHERE userID = @id " +
        //              "GROUP BY Date";

        //        SqlCommand command = new SqlCommand(query, mydb.getConnection);
        //        command.Parameters.AddWithValue("@id", userID);
        //        SqlDataReader reader = command.ExecuteReader();

        //        DataTable table = new DataTable();
        //        table.Load(reader);

             
                
        //        mydb.closeConnection();
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    MessageBox.Show("An error occurred: " + ex.Message);
        //    //}
        //}

        private void TestForm2_Load(object sender, EventArgs e)
        {
            //LoadData();

            

            mydb.openConnection();
            SqlCommand command = new SqlCommand("SELECT STT, Name, Price, Note, Picture FROM budget Where userID = @us", mydb.getConnection);
            command.Parameters.Add("@us", SqlDbType.VarChar).Value = global.userID;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Lấy dữ liệu từ database vào DataTable

            foreach (DataRow row in dataTable.Rows)
            {
                MySecondCustmControl customControl = new MySecondCustmControl();

                customControl.SetData(row); // Thiết lập dữ liệu cho UserControl từ mỗi hàng dữ liệu trong DataTable
                customControl.RemoveButtonClick += CustomControl_RemoveButtonClick;
                customControl.EditButtonClick += CustomControl_EditButtonClick;
                customControl.AddItemClicked += CustomControl_AddItemClicked;
                flowLayoutPanel1.Controls.Add(customControl); // Thêm UserControl vào panel hoặc flowLayoutPanel
            }
            mydb.closeConnection();
        }

        private void CustomControl_AddItemClicked(object sender, CustomEventArgs e)
        {
            string itemName = e.ItemName;

            OrderItem existingItem;
            if (orderItems.TryGetValue(itemName, out existingItem))
            {
                existingItem.Quantity++;
                existingItem.TotalPrice = existingItem.InitialPrice * existingItem.Quantity;
            }
            else
            {
                // Lấy thông tin về món từ cơ sở dữ liệu hoặc các nguồn dữ liệu khác
                string initialPriceString = e.price;
                decimal initialPrice = ConvertPriceToDecimal(initialPriceString);

                OrderItem newItem = new OrderItem
                {
                    Name = itemName,
                    Quantity = 1,
                    InitialPrice = initialPrice,
                    TotalPrice = initialPrice,
                    CreatedDate = DateTime.Now
                };
                orderItems.Add(itemName, newItem);
            }

            // Hiển thị danh sách các món và số lượng tương ứng trong ListBox
            



            // Hiển thị danh sách các món và số lượng tương ứng trong ListBox
            UpdateListBox();
        }

        private decimal ConvertPriceToDecimal(string priceString)
        {
            // Xóa các ký tự không phải số từ chuỗi giá (ví dụ: "200 vnđ" => "200")
            string numericString = new string(priceString.Where(char.IsDigit).ToArray());

            // Chuyển đổi chuỗi số thành kiểu decimal
            decimal priceDecimal = decimal.Parse(numericString);

            return priceDecimal;
        }


        private void UpdateListBox()
        {
            listBox1.Items.Clear();

            int maxNameLength = GetMaxNameLength();

            foreach (var item in orderItems.Values)
            {
                string displayName = GetFormattedDisplayName(item.Name, maxNameLength);
                string displayText = $"{displayName}\t:          ({item.Quantity})";
                listBox1.Items.Add(displayText);
            }
        }

        private int GetMaxNameLength()
        {
            int maxLength = 0;

            foreach (var item in orderItems.Values)
            {
                if (item.Name.Length > maxLength)
                {
                    maxLength = item.Name.Length;
                }
            }

            return maxLength;
        }

        private string GetFormattedDisplayName(string name, int maxLength)
        {
            if (name.Length > maxLength)
            {
                name = name.Substring(0, maxLength);
            }

            return name.PadRight(maxLength);
        }


        private void SaveToDatabase()
        {

            mydb.openConnection();

                foreach (var item in orderItems.Values)
                {
                    string itemName = item.Name;
                    int quantity = item.Quantity;
                    decimal initialPrice = item.InitialPrice;
                    decimal totalPrice = item.TotalPrice;
                    DateTime createdDate = item.CreatedDate;

                    string insertQuery = "INSERT INTO [oder] (NameItem, Quantity, InitialPrice, TotalPrice, CreatedDate, char) " +
                                         "VALUES (@Name, @Quantity, @InitialPrice, @TotalPrice, @CreatedDate, @char)";

                    using (SqlCommand command = new SqlCommand(insertQuery, mydb.getConnection))
                    {
                        command.Parameters.AddWithValue("@Name", itemName);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@InitialPrice", initialPrice);
                        command.Parameters.AddWithValue("@TotalPrice", totalPrice);
                        command.Parameters.AddWithValue("@CreatedDate", createdDate);
                        command.Parameters.AddWithValue("@char", tableNumber);

                        command.ExecuteNonQuery();
                    }
                }

            mydb.closeConnection();
            
        }

        private void SaveToDatabases()
        {

            mydb.openConnection();

            foreach (var item in orderItems.Values)
            {
                string itemName = item.Name;
                int quantity = item.Quantity;
                decimal initialPrice = item.InitialPrice;
                decimal totalPrice = item.TotalPrice;
                DateTime createdDate = item.CreatedDate;

                string insertQuery = "INSERT INTO [SaveOder] (NameItem, Quantity, InitialPrice, TotalPrice, CreatedDate, char) " +
                                     "VALUES (@Name, @Quantity, @InitialPrice, @TotalPrice, @CreatedDate, @char)";

                using (SqlCommand command = new SqlCommand(insertQuery, mydb.getConnection))
                {
                    command.Parameters.AddWithValue("@Name", itemName);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@InitialPrice", initialPrice);
                    command.Parameters.AddWithValue("@TotalPrice", totalPrice);
                    command.Parameters.AddWithValue("@CreatedDate", createdDate);
                    command.Parameters.AddWithValue("@char", tableNumber);

                    command.ExecuteNonQuery();
                }
            }

            mydb.closeConnection();

        }





        private void CustomControl_RemoveButtonClick(object sender, EventArgs e)
        {
            MySecondCustmControl customControl = (MySecondCustmControl)sender;

            // Lấy thông tin duy nhất để xác định dữ liệu cần xóa (ví dụ: ID của hàng dữ liệu)
            string dataId = customControl.GetDataId(); // Phương thức GetDataId() trả về thông tin duy nhất

            // Xóa dữ liệu từ database
            mydb.openConnection();
            SqlCommand deleteCommand = new SqlCommand("DELETE FROM budget WHERE STT = @id", mydb.getConnection);
            deleteCommand.Parameters.AddWithValue("@id", dataId);
            deleteCommand.ExecuteNonQuery();
            mydb.closeConnection();

            // Xóa control khỏi panel
            flowLayoutPanel1.Controls.Remove(customControl);

            
        }

        private void CustomControl_EditButtonClick(object sender, EventArgs e)
        {
            MySecondCustmControl customControl = (MySecondCustmControl)sender;

            // Lấy thông tin duy nhất để xác định dữ liệu cần cập nhật (ví dụ: ID của hàng dữ liệu)
            string dataId = customControl.GetDataId();

            // Lấy nội dung mới từ các thành phần trong control
            string newName = customControl.textBoxName.Text;
            string newPrice = customControl.textBoxPrice.Text;
            
            

            // Kiểm tra dữ liệu trống
            if (string.IsNullOrEmpty(newName) || string.IsNullOrEmpty(newPrice) )
            {
                // Hiển thị thông báo lỗi
                NotificationForm notificationForms = new NotificationForm("Dữ liệu đang bị trống. Vui lòng không để trống dữ liệu", Properties.Resources._7693271);
                notificationForms.ShowDialog();
                return; // Dừng sviệc cập nhật dữ liệu
            }
            // Cập nhật dữ liệu trong database
            mydb.openConnection();
            SqlCommand updateCommand = new SqlCommand("UPDATE budget SET Name = @name, Price = @price, Note = @note WHERE STT = @id", mydb.getConnection);
            updateCommand.Parameters.AddWithValue("@name", newName);
            updateCommand.Parameters.AddWithValue("@price", newPrice);
            
            
            updateCommand.Parameters.AddWithValue("@id", dataId);
            updateCommand.ExecuteNonQuery();
            mydb.closeConnection();

            // Cập nhật nội dung mới cho control
            customControl.textBoxName.Text = newName;
            customControl.textBoxPrice.Text = newPrice;
            
          

            // Hiển thị thông báo hoặc thực hiện các tác vụ khác sau khi cập nhật thành công
            NotificationForm notificationForm = new NotificationForm("Dữ liệu đã được cập nhật thành công.", Properties.Resources._7693271);
            notificationForm.ShowDialog();

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                NotificationForm notificationForm = new NotificationForm("Không được để trống dữ liệu", Properties.Resources.Picture10);
                notificationForm.ShowDialog();
            }
            else
            {
                SaveToDatabase();
                SaveToDatabases();
                mainForm.sidePanel.Height = mainForm.ButtonCreater.Height;
                mainForm.sidePanel.Top = mainForm.ButtonCreater.Top;


                // Kiểm tra xem panel đã chứa form nào chưa
                if (mainForm.Panel6.Controls.Count > 0)
                {
                    // Xóa form hiện tại khỏi panel
                    mainForm.Panel6.Controls.Clear();
                }

                CreatePaymentForm stdinfor = new CreatePaymentForm(mainForm);
                stdinfor.TopLevel = false;
                mainForm.Panel6.Controls.Add(stdinfor);
                stdinfor.Show();
                mainForm.Panel6.BringToFront();
                NotificationForm notificationForm = new NotificationForm("Dữ liệu đã được lưu thành công.", Properties.Resources.Picture10);
                notificationForm.ShowDialog();
                this.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.sidePanel.Height = mainForm.ButtonCreater.Height;
            mainForm.sidePanel.Top = mainForm.ButtonCreater.Top;


            // Kiểm tra xem panel đã chứa form nào chưa
            if (mainForm.Panel6.Controls.Count > 0)
            {
                // Xóa form hiện tại khỏi panel
                mainForm.Panel6.Controls.Clear();
            }

            CreatePaymentForm stdinfor = new CreatePaymentForm(mainForm);
            stdinfor.TopLevel = false;
            mainForm.Panel6.Controls.Add(stdinfor);
            stdinfor.Show();
            mainForm.Panel6.BringToFront();
            
            this.Close();

         }

        private string ExtractItemName(string displayText)
        {
            // Assuming the displayText has the format "itemName (quantity)"
            int startIndex = displayText.IndexOf(":");
            int endIndex = displayText.LastIndexOf(")");
            string itemName = displayText.Substring(0, startIndex).Trim();
            return itemName;

            //// Assuming the displayText has the format "itemName (quantity)"
            //int startIndex = displayText.IndexOf("(");
            //if (startIndex != -1)
            //{
            //    string itemName = displayText.Substring(0, startIndex).Trim();
            //    return itemName;
            //}
            //else
            //{
            //    // If the displayText doesn't contain "(",
            //    // assume the whole displayText is the itemName
            //    return displayText.Trim();
            //}
        }

        private string selectedItemName;
        

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                selectedItemName = listBox1.SelectedItem.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (selectedItemName != null)
            {
                //string selectedDisplayText = listBox1.SelectedItem.ToString();
                string selectedName = ExtractItemName(selectedItemName);

                if (orderItems.ContainsKey(selectedName))
                {
                    OrderItem selectedItem = orderItems[selectedName];
                    selectedItem.Quantity--;
                    selectedItem.TotalPrice = selectedItem.InitialPrice * selectedItem.Quantity;

                    if (selectedItem.Quantity == 0)
                    {
                        orderItems.Remove(selectedName);
                    }
                }

                UpdateListBox();
            }
        }
    }
}
