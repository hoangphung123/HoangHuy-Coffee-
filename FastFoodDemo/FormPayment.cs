using FastFoodDemo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FastFoodDemo.Budgets;
using static FastFoodDemo.MySecondCustmControl;

namespace FastFoodDemo
{
    public partial class FormPayment : Form
    {
        private string chairNumber;

        private Main main;
        public FormPayment(string chairNumber, Main main)
        {
            InitializeComponent();
            this.chairNumber = chairNumber;
            orderItems = new Dictionary<string, OrderItem>();
            this.main = main;
        }

        DB mydb = new DB();

        private Dictionary<string, OrderItem> orderItems;

        

        public class OrderItem
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
            public decimal InitialPrice { get; set; }
            public decimal TotalPrice { get; set; }
            public DateTime CreatedDate { get; set; }
        }

        private void FormPayment_Load(object sender, EventArgs e)
        {
            mydb.openConnection();

            // Truy vấn dữ liệu từ bảng "oder" với điều kiện Char = chairNumber
            SqlCommand oderCommand = new SqlCommand("SELECT NameItem, Char, Quantity, InitialPrice, TotalPrice FROM oder WHERE Char = @chairNumber", mydb.getConnection);
            oderCommand.Parameters.Add("@chairNumber", SqlDbType.VarChar).Value = chairNumber;
            SqlDataReader oderReader = oderCommand.ExecuteReader();

            while (oderReader.Read())
            {
                string nameItem = oderReader["NameItem"].ToString();
                string quantityValue = oderReader["Quantity"].ToString();
                string initialPriceValue = oderReader["InitialPrice"].ToString();
                string totalPriceValue = oderReader["TotalPrice"].ToString();



                // Cập nhật dữ liệu vào orderItems
                OrderItem existingItem;
                if (orderItems.TryGetValue(nameItem, out existingItem))
                {
                    int quantity;
                    decimal initialPrice;
                    decimal totalPrice;

                    if (int.TryParse(quantityValue, out quantity))
                    {
                        existingItem.Quantity = quantity;
                    }

                    if (decimal.TryParse(initialPriceValue, out initialPrice))
                    {
                        existingItem.InitialPrice = initialPrice;
                    }

                    if (decimal.TryParse(totalPriceValue, out totalPrice))
                    {
                        existingItem.TotalPrice = totalPrice;
                    }
                }
                else
                {
                    int quantity;
                    decimal initialPrice;
                    decimal totalPrice;

                    if (int.TryParse(quantityValue, out quantity) && decimal.TryParse(initialPriceValue, out initialPrice) && decimal.TryParse(totalPriceValue, out totalPrice))
                    {
                        OrderItem newItem = new OrderItem
                        {
                            Name = nameItem,
                            Quantity = quantity,
                            InitialPrice = ConvertPriceToDecimal(initialPriceValue),
                            TotalPrice = ConvertPriceToDecimal(totalPriceValue),
                            CreatedDate = DateTime.Now
                        };
                        orderItems.Add(nameItem, newItem);
                    }
                }
            }

            oderReader.Close();

            SqlCommand command = new SqlCommand("SELECT STT, Name, Price, Note, Picture FROM budget WHERE userID = @us", mydb.getConnection);
            command.Parameters.Add("@us", SqlDbType.VarChar).Value = global.userID;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Lấy dữ liệu từ database vào DataTable

            foreach (DataRow row in dataTable.Rows)
            {
                MySecondCustmControl customControl = new MySecondCustmControl();

                customControl.SetData(row); // Thiết lập dữ liệu cho UserControl từ mỗi hàng dữ liệu trong DataTable

                customControl.AddItemClicked += CustomControl_AddItemClicked;
                flowLayoutPanel1.Controls.Add(customControl); // Thêm UserControl vào panel hoặc flowLayoutPanel
            }

            mydb.closeConnection();

            // Hiển thị danh sách các món và số lượng tương ứng trong ListBox
            UpdateListBox();
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
            int maxNameLength = GetMaxNameLength();

            // Tạo một danh sách tạm thời để chứa các mục mới
            List<string> tempList = new List<string>();

            foreach (var item in orderItems.Values)
            {
                string displayName = GetFormattedDisplayName(item.Name, maxNameLength);
                string displayText = $"{displayName}\t:          ({item.Quantity})";
                tempList.Add(displayText);
            }

            // Xóa danh sách hiện tại trong listBox1
            listBox1.Items.Clear();

            // Thêm các mục mới vào listBox1 từ danh sách tạm thời
            listBox1.Items.AddRange(tempList.ToArray());
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

                string selectQuery = "SELECT COUNT(*) FROM [oder] WHERE NameItem = @Name AND [char] = @char";

                using (SqlCommand selectCommand = new SqlCommand(selectQuery, mydb.getConnection))
                {
                    selectCommand.Parameters.AddWithValue("@Name", itemName);
                    selectCommand.Parameters.AddWithValue("@char", chairNumber);

                    int existingCount = (int)selectCommand.ExecuteScalar();

                    if (existingCount > 0)
                    {
                        string updateQuery = "UPDATE [oder] SET Quantity = @Quantity, InitialPrice = @InitialPrice, TotalPrice = @TotalPrice, CreatedDate = @CreatedDate " +
                                             "WHERE NameItem = @Name AND [char] = @char";

                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, mydb.getConnection))
                        {
                            updateCommand.Parameters.AddWithValue("@Quantity", quantity);
                            updateCommand.Parameters.AddWithValue("@InitialPrice", initialPrice);
                            updateCommand.Parameters.AddWithValue("@TotalPrice", totalPrice);
                            updateCommand.Parameters.AddWithValue("@CreatedDate", createdDate);
                            updateCommand.Parameters.AddWithValue("@Name", itemName);
                            updateCommand.Parameters.AddWithValue("@char", chairNumber);

                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string insertQuery = "INSERT INTO [oder] (NameItem, Quantity, InitialPrice, TotalPrice, CreatedDate, [char]) " +
                                             "VALUES (@Name, @Quantity, @InitialPrice, @TotalPrice, @CreatedDate, @char)";

                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, mydb.getConnection))
                        {
                            insertCommand.Parameters.AddWithValue("@Name", itemName);
                            insertCommand.Parameters.AddWithValue("@Quantity", quantity);
                            insertCommand.Parameters.AddWithValue("@InitialPrice", initialPrice);
                            insertCommand.Parameters.AddWithValue("@TotalPrice", totalPrice);
                            insertCommand.Parameters.AddWithValue("@CreatedDate", createdDate);
                            insertCommand.Parameters.AddWithValue("@char", chairNumber);

                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }

            mydb.closeConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("Không được để trống");
            }
            else
            {
                SaveToDatabase();
                MessageBox.Show("Đã lưu thành công vào cơ sở dữ liệu.");
                this.Close();
                main.panel10.BringToFront();
            }
        }
        private string selectedItemName;
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

        private void button1_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        //private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        //{

        //    int billWidth = 80;  // Đơn vị: mm
        //    int billHeight = 150;  // Đơn vị: mm
        //    e.PageSettings.PaperSize = new PaperSize("Custom", billWidth * 10, billHeight * 10); // Kích thước theo đơn vị 1/10 mm


        //    // Lấy thông tin cửa hàng
        //    string tenCuaHang = "LOCO";
        //    string diaChiCuaHang = "Địa chỉ của hàng";
        //    string soDienThoaiCuaHang = "Số điện thoại của hàng";

        //    // Lấy thông tin từ database (Ví dụ)
        //    string nameItem = "Tên sản phẩm";
        //    int quantity = 2;
        //    decimal initialPrice = 10.5m;
        //    decimal totalPrice = quantity * initialPrice;

        //    // Lấy ngày giờ hiện tại
        //    DateTime currentDate = DateTime.Now;

        //    // Tạo đối tượng Font cho các phần tử trong bill
        //    Font fontHeader = new Font("Arial", 32, FontStyle.Bold);
        //    Font fontContent = new Font("Arial", 16, FontStyle.Regular);
        //    Font fontFooter = new Font("Arial", 16, FontStyle.Italic);

        //    // Tạo đối tượng Brush cho việc vẽ các phần tử trong bill
        //    SolidBrush brush = new SolidBrush(Color.Black);

        //    // Vị trí bắt đầu của bill
        //    int startX = 10;
        //    int startY = 10;

        //    // Xác định chiều cao của giấy
        //    int paperHeight = e.PageBounds.Height;

        //    // Tạo đối tượng StringFormat để căn giữa văn bản
        //    StringFormat centerFormat = new StringFormat();
        //    centerFormat.Alignment = StringAlignment.Center;

        //    // Vẽ logo của cửa hàng (tuỳ chọn)
        //    // Image logo = Image.FromFile("logo.png");
        //    // e.Graphics.DrawImage(logo, startX, startY, 100, 100);

        //    // Vẽ tên cửa hàng
        //    e.Graphics.DrawString(tenCuaHang, fontHeader, brush, e.PageBounds.Width / 2, startY, centerFormat);
        //    startY += 60;

        //    // Vẽ địa chỉ cửa hàng
        //    e.Graphics.DrawString(diaChiCuaHang, fontContent, brush, e.PageBounds.Width / 2, startY, centerFormat);
        //    startY += 50;

        //    // Vẽ số điện thoại cửa hàng
        //    e.Graphics.DrawString("SĐT: " + soDienThoaiCuaHang, fontContent, brush, e.PageBounds.Width / 2, startY, centerFormat);
        //    startY += 40;

        //    // Vẽ đường kẻ ngang
        //    e.Graphics.DrawLine(new Pen(Color.Black, 2), startX, startY, e.PageBounds.Width - startX, startY);
        //    startY += 20;

        //    // Vẽ tiêu đề các cột
        //    e.Graphics.DrawString("Tên sản phẩm", fontContent, brush, startX, startY);
        //    e.Graphics.DrawString("Số lượng", fontContent, brush, startX + 200, startY);
        //    e.Graphics.DrawString("Giá gốc", fontContent, brush, startX + 300, startY);
        //    e.Graphics.DrawString("Thành tiền", fontContent, brush, startX + 400, startY);
        //    startY += 20;

        //    // Vẽ đường kẻ ngang
        //    e.Graphics.DrawLine(new Pen(Color.Black, 1), startX, startY, e.PageBounds.Width - startX, startY);
        //    startY += 10;

        //    // Vẽ nội dung bill
        //    e.Graphics.DrawString(nameItem, fontContent, brush, startX, startY);
        //    e.Graphics.DrawString(quantity.ToString(), fontContent, brush, startX + 200, startY);
        //    e.Graphics.DrawString(initialPrice.ToString("C"), fontContent, brush, startX + 300, startY);
        //    e.Graphics.DrawString(totalPrice.ToString("C"), fontContent, brush, startX + 400, startY);
        //    startY += 20;

        //    // Vẽ đường kẻ ngang
        //    e.Graphics.DrawLine(new Pen(Color.Black, 1), startX, startY, e.PageBounds.Width - startX, startY);
        //    startY += 30;

        //    // Vẽ tổng tiền
        //    e.Graphics.DrawString("Tổng tiền:", fontFooter, brush, startX, startY);
        //    e.Graphics.DrawString(totalPrice.ToString("C"), fontFooter, brush, startX + 400, startY);
        //    startY += 20;

        //    // Vẽ ngày giờ bill
        //    e.Graphics.DrawString("Ngày giờ: " + currentDate.ToString(), fontFooter, brush, startX, startY);
        //}

        //private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //    // Thiết lập kích thước giấy của bill tiêu chuẩn
        //    int billWidth = 80;  // Đơn vị: mm
        //    int billHeight = 150;  // Đơn vị: mm
        //    e.PageSettings.PaperSize = new PaperSize("Custom", billWidth * 10, billHeight * 10); // Kích thước theo đơn vị 1/10 mm

        //    // Thực hiện truy vấn dữ liệu từ cơ sở dữ liệu
        //    // Thay bằng giá trị thích hợp của chairNumber
        //    List<string> nameItems = new List<string>();
        //    List<int> quantities = new List<int>();
        //    List<decimal> initialPrices = new List<decimal>();
        //    List<decimal> totalPrices = new List<decimal>();


        //    mydb.openConnection();

        //        string query = "SELECT nameItem, Quantity, InitialPrice, TotalPrice FROM oder WHERE char = @chairNumber";

        //        using (SqlCommand command = new SqlCommand(query, mydb.getConnection))
        //        {
        //            command.Parameters.AddWithValue("@chairNumber", chairNumber);

        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    nameItems.Add(reader.GetString(0));
        //                    quantities.Add(reader.GetInt32(1));
        //                    initialPrices.Add(reader.GetDecimal(2));
        //                    totalPrices.Add(reader.GetDecimal(3));
        //                }
        //            }
        //        }
        //     mydb.closeConnection();


        //    // Các thông số vẽ bill
        //    int startX = 10;
        //    int startY = 10;
        //    int lineHeight = 20;

        //    // Tạo đối tượng Font và Brush

        //    Font fontContent = new Font("Arial", 16, FontStyle.Regular);
        //    SolidBrush brush = new SolidBrush(Color.Black);

        //    // Vẽ thông tin cửa hàng
        //    string storeName = "LOCO";
        //    string storeAddress = "Địa chỉ cửa hàng";
        //    string storePhoneNumber = "Số điện thoại cửa hàng";

        //    e.Graphics.DrawString(storeName, fontContent, brush, startX, startY);
        //    e.Graphics.DrawString(storeAddress, fontContent, brush, startX, startY + lineHeight);
        //    e.Graphics.DrawString(storePhoneNumber, fontContent, brush, startX, startY + lineHeight * 2);

        //    // Vẽ thông tin sản phẩm
        //    e.Graphics.DrawString("Name", fontContent, brush, startX, startY + lineHeight * 3);
        //    e.Graphics.DrawString("Quantity", fontContent, brush, startX + 200, startY + lineHeight * 3);
        //    e.Graphics.DrawString("Initial Price", fontContent, brush, startX + 300, startY + lineHeight * 3);
        //    e.Graphics.DrawString("Total Price", fontContent, brush, startX + 600, startY + lineHeight * 3);

        //    // Vẽ các sản phẩm trong bill
        //    for (int i = 0; i < nameItems.Count; i++)
        //    {
        //        // Vẽ thông tin sản phẩm
        //        e.Graphics.DrawString(nameItems[i], fontContent, brush, startX, startY + lineHeight * (i + 5));
        //        e.Graphics.DrawString(quantities[i].ToString(), fontContent, brush, startX + 200, startY + lineHeight * (i + 5));
        //        e.Graphics.DrawString(initialPrices[i].ToString("C"), fontContent, brush, startX + 300, startY + lineHeight * (i + 5));
        //        e.Graphics.DrawString(totalPrices[i].ToString("C"), fontContent, brush, startX + 600, startY + lineHeight * (i + 5));

        //        startY += 10;

        //        // Tính toán vị trí đường ngang dưới sản phẩm
        //        int horizontalLineY = (int)(startY + lineHeight * (i + 6));

        //        // Vẽ đường ngang
        //        e.Graphics.DrawLine(new Pen(Color.Black, 1), startX, horizontalLineY, e.PageBounds.Width - startX, horizontalLineY);
        //    }

        //    // Vẽ đường ngang cuối cùng của bill
        //    int finalHorizontalLineY = startY + lineHeight * (nameItems.Count + 5);
        //    e.Graphics.DrawLine(new Pen(Color.Black, 1), startX, finalHorizontalLineY, e.PageBounds.Width - startX, finalHorizontalLineY);

        //    // Tính tổng tiền
        //    decimal totalAmount = totalPrices.Sum();

        //    // Vẽ tổng tiền
        //    e.Graphics.DrawString("Total Amount:", fontContent, brush, startX, startY + 80 +  lineHeight * (nameItems.Count + 1));
        //    e.Graphics.DrawString(totalAmount.ToString("C"), fontContent, brush, startX + 600, startY + 80  + lineHeight * (nameItems.Count + 1));
        //}

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Thiết lập kích thước giấy của bill tiêu chuẩn
            int billWidth = 80;  // Đơn vị: mm
            int billHeight = 150;  // Đơn vị: mm
            e.PageSettings.PaperSize = new PaperSize("Custom", billWidth * 10, billHeight * 10); // Kích thước theo đơn vị 1/10 mm

            // Thực hiện truy vấn dữ liệu từ cơ sở dữ liệu
            // Thay bằng giá trị thích hợp của chairNumber
            List<string> nameItems = new List<string>();
            List<int> quantities = new List<int>();
            List<decimal> initialPrices = new List<decimal>();
            List<decimal> totalPrices = new List<decimal>();

            mydb.openConnection();

            string query = "SELECT nameItem, Quantity, InitialPrice, TotalPrice FROM oder WHERE char = @chairNumber";

            using (SqlCommand command = new SqlCommand(query, mydb.getConnection))
            {
                command.Parameters.AddWithValue("@chairNumber", chairNumber);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nameItems.Add(reader.GetString(0));
                        quantities.Add(reader.GetInt32(1));
                        initialPrices.Add(reader.GetDecimal(2));
                        totalPrices.Add(reader.GetDecimal(3));
                    }
                }
            }
            mydb.closeConnection();

            // Các thông số vẽ bill
            int startX = 10;
            int startY = 10;
            int lineHeight = 30;

            // Tạo đối tượng Font và Brush
            Font fontContent = new Font("Arial", 16, FontStyle.Regular);
            SolidBrush brush = new SolidBrush(Color.Black);

            // Tải hình ảnh logo từ tệp
            Image logoImage = Image.FromFile("C:\\Users\\hoanghuy\\Desktop\\Pictureghe.png");

            // Vẽ hình ảnh logo
            int logoWidth = 100;  // Độ rộng logo (đơn vị: pixel)
            int logoHeight = 100;  // Chiều cao logo (đơn vị: pixel)
            int logoX = startX + 350;  // Vị trí X để vẽ logo (tương tự startX)
            int logoY = startY;  // Vị trí Y để vẽ logo

            // Vẽ logo trên tên cửa hàng
            e.Graphics.DrawImage(logoImage, logoX, logoY, logoWidth, logoHeight);

            // Vẽ thông tin cửa hàng
            string storeName = "LOCO";
            string storeAddress = "Địa chỉ cửa hàng";
            string storePhoneNumber = "Số điện thoại cửa hàng";

            int storeNameX = startX + (billWidth * 10 - (int)e.Graphics.MeasureString(storeName, fontContent).Width) / 2;
            int storeAddressX = startX + (billWidth * 10 - (int)e.Graphics.MeasureString(storeAddress, fontContent).Width) / 2;
            int storePhoneNumberX = startX + (billWidth * 10 - (int)e.Graphics.MeasureString(storePhoneNumber, fontContent).Width) / 2;


            // Vị trí Y của thông tin cửa hàng
            int storeInfoY = startY + logoHeight + 10;  // 10 là khoảng cách giữa logo và thông tin cửa hàng

            e.Graphics.DrawString(storeName, fontContent, brush, storeNameX, storeInfoY);
            e.Graphics.DrawString(storeAddress, fontContent, brush, storeAddressX, storeInfoY + lineHeight);
            e.Graphics.DrawString(storePhoneNumber, fontContent, brush, storePhoneNumberX, storeInfoY + lineHeight * 2);
            

            // Vẽ thông tin sản phẩm
            int productInfoY = storeInfoY + lineHeight * 4;

            e.Graphics.DrawString("Tên món", fontContent, brush, startX, productInfoY);
            e.Graphics.DrawString("số lượng", fontContent, brush, startX + 200, productInfoY);
            e.Graphics.DrawString("Đơn giá", fontContent, brush, startX + 350, productInfoY);
            e.Graphics.DrawString("Thành tiền", fontContent, brush, startX + 600, productInfoY);

            // Vẽ các sản phẩm trong bill
            for (int i = 0; i < nameItems.Count; i++)
            {
                int productY = productInfoY + lineHeight * (i + 1);

                // Vẽ thông tin sản phẩm
                e.Graphics.DrawString(nameItems[i], fontContent, brush, startX, productY);
                e.Graphics.DrawString(quantities[i].ToString(), fontContent, brush, startX + 200, productY);
                e.Graphics.DrawString(initialPrices[i].ToString("#,0.##") + " ₫", fontContent, brush, startX + 350, productY);
                e.Graphics.DrawString(totalPrices[i].ToString("#,0.##") + " ₫", fontContent, brush, startX + 600, productY);

                // Tính toán vị trí đường ngang dưới sản phẩm
                int horizontalLineY = productY + lineHeight;

                // Vẽ đường ngang
                e.Graphics.DrawLine(new Pen(Color.Black, 1), startX, horizontalLineY, e.PageBounds.Width - startX, horizontalLineY);
            }

            // Vẽ đường ngang cuối cùng của bill
            int finalHorizontalLineY = productInfoY + lineHeight * (nameItems.Count + 1);
            e.Graphics.DrawLine(new Pen(Color.Black, 1), startX, finalHorizontalLineY, e.PageBounds.Width - startX, finalHorizontalLineY);

            // Tính tổng tiền
            decimal totalAmount = totalPrices.Sum();

            // Vẽ tổng tiền
            int totalAmountY = finalHorizontalLineY + lineHeight;
            e.Graphics.DrawString("Total Amount:", fontContent, brush, startX, totalAmountY);
            e.Graphics.DrawString(totalAmount.ToString("#,0.##") + " ₫", fontContent, brush, startX + 600, totalAmountY);

            int totalAmountWordsY = totalAmountY + lineHeight;

            // Chuyển đổi tổng số tiền thành chữ số tiếng Việt
            string totalAmountInWords = NumberToWords(totalAmount);

            // Tính toán vị trí x của chuỗi totalAmountInWords
            int totalAmountWordsX = startX + (billWidth * 10 - (int)e.Graphics.MeasureString(totalAmountInWords + " đồng", fontContent).Width) / 2;

            // Vẽ chuỗi totalAmountInWords
            e.Graphics.DrawString(totalAmountInWords + " đồng", fontContent, brush, totalAmountWordsX, totalAmountWordsY + 50);
        }

        // Hàm chuyển đổi số sang chữ
        // Hàm chuyển đổi số sang chữ số tiếng Việt
        private string NumberToWords(decimal number)
        {
            string[] ones = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] multiplesOfTen = { "", "mười", "hai mươi", "ba mươi", "bốn mươi", "năm mươi", "sáu mươi", "bảy mươi", "tám mươi", "chín mươi" };
            string[] powersOfTen = { "", "nghìn", "triệu", "tỷ" };

            if (number == 0)
                return "không đồng";

            if (number < 0)
                return "Âm " + NumberToWords(Math.Abs(number));

            string words = "";

            int powerOfTenIndex = 0;
            while (number > 0)
            {
                int currentGroup = (int)(number % 1000);
                if (currentGroup > 0)
                {
                    string groupWords = "";

                    int hundreds = currentGroup / 100;
                    int tensAndOnes = currentGroup % 100;

                    if (hundreds > 0)
                    {
                        groupWords += ones[hundreds] + " trăm ";
                    }

                    if (tensAndOnes >= 10 && tensAndOnes <= 19)
                    {
                        groupWords += ones[tensAndOnes] + " ";
                    }
                    else
                    {
                        int tens = tensAndOnes / 10;
                        int onesDigit = tensAndOnes % 10;

                        if (tens > 0)
                        {
                            groupWords += multiplesOfTen[tens] + " ";
                        }

                        if (onesDigit > 0)
                        {
                            if (tens == 0)
                            {
                                groupWords += "lẻ ";
                            }
                            groupWords += ones[onesDigit] + " ";
                        }
                    }

                    groupWords += powersOfTen[powerOfTenIndex] + " ";
                    words = groupWords + words;
                }

                powerOfTenIndex++;
                number = Math.Floor(number / 1000);
            }

            return words.Trim();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            main.panel10.BringToFront();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedItemName = listBox1.SelectedItem.ToString();
        }
    }
}
