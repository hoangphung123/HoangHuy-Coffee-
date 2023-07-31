using FastFoodDemo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastFoodDemo
{
    public partial class Main : Form
    {
        DB mydb = new DB();
        private string Name;

        public Button Button3 { get { return button3; } }

        public Button ButtonCreater { get { return buttonCreater; } }
        public Panel Panel7 { get { return panel7; } }

        public Panel Panel6 { get { return panel6; } }

        public Panel sidePanel { get { return SidePanel; } }

        private Button button1;



        

        private DataTable orderTable;

        public Main(string username)
        {
            InitializeComponent();
            //SidePanel.Height = button1.Height;
            //SidePanel.Top = button1.Top;
            //firstCustomControl1.BringToFront();
            this.Name = username;
            
            panel9.Visible = false;

            // Khởi tạo DataTable
            orderTable = new DataTable();
            orderTable.Columns.Add("Chair", typeof(int));

            // Button
            button1 = new Button();
            button1.Location = new System.Drawing.Point(50, 270);
            button1.Size = new System.Drawing.Size(200, 30);
            button1.Text = "Load Chairs";
            button1.Click += button1_Click;

            // Add controls to form
           
            Controls.Add(button1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //firstCustomControl1.BringToFront();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {


            //flowLayoutPanel1.BringToFront();
            //panel4.BringToFront();
            //panel5.BringToFront();

            SidePanel.Height = buttonCreater.Height;
            SidePanel.Top = buttonCreater.Top;

            CreatePaymentForm stdinfor = new CreatePaymentForm(this);
            stdinfor.TopLevel = false;
            stdinfor.Dock = DockStyle.Fill;
            panel6.Controls.Add(stdinfor);
            stdinfor.Show();
            panel6.BringToFront();

            labelusername2.Text = Name;

            mydb.openConnection();

            // Kiểm tra xem bảng Customer có userID = global.userID hay không
            SqlCommand checkCustomerCommand = new SqlCommand("SELECT Picture FROM Customer WHERE userID = @us", mydb.getConnection);
            checkCustomerCommand.Parameters.Add("@us", SqlDbType.VarChar).Value = global.userID;
            object pictureResult = checkCustomerCommand.ExecuteScalar();

            if (pictureResult != null && pictureResult != DBNull.Value)
            {
                // Có hình ảnh trong bảng Customer, tải lên PictureBox
                byte[] pictureBytes = (byte[])pictureResult;
                using (MemoryStream ms = new MemoryStream(pictureBytes))
                {
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            else
            {
                // Không có hình ảnh trong bảng Customer, không tải lên PictureBox
            }
            mydb.closeConnection();




            //try
            //{
            mydb.openConnection();
            SqlCommand command = new SqlCommand("SELECT STT, Name, Price, Note, Static, Picture FROM Item Where userID = @us", mydb.getConnection);
            command.Parameters.Add("@us", SqlDbType.VarChar).Value = global.userID;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Lấy dữ liệu từ database vào DataTable

            foreach (DataRow row in dataTable.Rows)
            {
                FirstCustomControl customControl = new FirstCustomControl();

                customControl.SetData(row); // Thiết lập dữ liệu cho UserControl từ mỗi hàng dữ liệu trong DataTable
                customControl.RemoveButtonClick += CustomControl_RemoveButtonClick;
                customControl.EditButtonClick += CustomControl_EditButtonClick;
                flowLayoutPanel1.Controls.Add(customControl); // Thêm UserControl vào panel hoặc flowLayoutPanel
            }

            string incomeQuery = "SELECT Price FROM Item WHERE Static = 'income' AND userID = @usincome";
            
            SqlCommand incomeCommand = new SqlCommand(incomeQuery, mydb.getConnection);
            incomeCommand.Parameters.Add("@usincome", SqlDbType.VarChar).Value = global.userID;
            SqlDataReader incomeReader = incomeCommand.ExecuteReader();


            decimal totalIncome = 0;
            while (incomeReader.Read())
            {
                string priceString = incomeReader["Price"].ToString(); // Lấy chuỗi giá trị
                decimal price = Convert.ToDecimal(priceString.Split(' ')[0]); // Tách và chuyển đổi thành số

                totalIncome += price; // Cộng dồn vào tổng
            }

            incomeReader.Close();
            label9.Text = totalIncome.ToString();

            string outcomeQuery = "SELECT Price FROM Item WHERE Static = 'Outcome' AND userID = @usoutcome";
            
            SqlCommand outcomeCommand = new SqlCommand(outcomeQuery, mydb.getConnection);
            outcomeCommand.Parameters.Add("@usoutcome", SqlDbType.VarChar).Value = global.userID;
            SqlDataReader outcomeReader = outcomeCommand.ExecuteReader();

            decimal totalOutcome = 0;
            while (outcomeReader.Read())
            {
                string priceString = outcomeReader["Price"].ToString(); // Lấy chuỗi giá trị
                decimal price = Convert.ToDecimal(priceString.Split(' ')[0]); // Tách và chuyển đổi thành số

                totalOutcome += price; // Cộng dồn vào tổng
            }

            outcomeReader.Close();
            label10.Text = totalOutcome.ToString();

            mydb.closeConnection();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message);
            //}
        }


        private void button13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Đường dẫn tới trang web cụ thể
            string url = "https://www.facebook.com/profile.php?id=100012810664049";

            // Mở trang web trong trình duyệt web mặc định
            System.Diagnostics.Process.Start(url);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button6.Height;
            SidePanel.Top = button6.Top;
            //flowLayoutPanel1.BringToFront();
            //panel4.BringToFront();
            //panel5.BringToFront();
            panelAddItems.BringToFront();

            mydb.openConnection();
            // Kiểm tra xem bảng Customer có userID = global.userID hay không
            SqlCommand checkCustomerCommand = new SqlCommand("SELECT Picture FROM Customer WHERE userID = @us", mydb.getConnection);
            checkCustomerCommand.Parameters.Add("@us", SqlDbType.VarChar).Value = global.userID;
            object pictureResult = checkCustomerCommand.ExecuteScalar();

            if (pictureResult != null && pictureResult != DBNull.Value)
            {
                // Có hình ảnh trong bảng Customer, tải lên PictureBox
                byte[] pictureBytes = (byte[])pictureResult;
                using (MemoryStream ms = new MemoryStream(pictureBytes))
                {
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            else
            {
                // Không có hình ảnh trong bảng Customer, không tải lên PictureBox
            }
            mydb.closeConnection();

            if (panelAddItems.Controls.Count > 0)
            {
                // Xóa form hiện tại khỏi panel
                panelAddItems.Controls.Clear();
            }

            //labelusername2.Text = Name;
            ////try
            ////{
            //mydb.openConnection();
            //SqlCommand command = new SqlCommand("SELECT STT, Name, Price, Note, Static, Picture FROM Item Where userID = @us", mydb.getConnection);
            //command.Parameters.Add("@us", SqlDbType.VarChar).Value = global.userID;
            //SqlDataAdapter adapter = new SqlDataAdapter(command);
            //DataTable dataTable = new DataTable();
            //adapter.Fill(dataTable);

            //// Lấy dữ liệu từ database vào DataTable

            //foreach (DataRow row in dataTable.Rows)
            //{
            //    FirstCustomControl customControl = new FirstCustomControl();

            //    customControl.SetData(row); // Thiết lập dữ liệu cho UserControl từ mỗi hàng dữ liệu trong DataTable
            //    customControl.RemoveButtonClick += CustomControl_RemoveButtonClick;
            //    customControl.EditButtonClick += CustomControl_EditButtonClick;
            //    flowLayoutPanel1.Controls.Add(customControl); // Thêm UserControl vào panel hoặc flowLayoutPanel
            //}

            //string incomeQuery = "SELECT Price FROM Item WHERE Static = 'income' AND userID = @usincome";

            //SqlCommand incomeCommand = new SqlCommand(incomeQuery, mydb.getConnection);
            //incomeCommand.Parameters.Add("@usincome", SqlDbType.VarChar).Value = global.userID;
            //SqlDataReader incomeReader = incomeCommand.ExecuteReader();


            //decimal totalIncome = 0;
            //while (incomeReader.Read())
            //{
            //    string priceString = incomeReader["Price"].ToString(); // Lấy chuỗi giá trị
            //    decimal price = Convert.ToDecimal(priceString.Split(' ')[0]); // Tách và chuyển đổi thành số

            //    totalIncome += price; // Cộng dồn vào tổng
            //}

            //incomeReader.Close();
            //label9.Text = totalIncome.ToString();

            //string outcomeQuery = "SELECT Price FROM Item WHERE Static = 'Outcome' AND userID = @usoutcome";

            //SqlCommand outcomeCommand = new SqlCommand(outcomeQuery, mydb.getConnection);
            //outcomeCommand.Parameters.Add("@usoutcome", SqlDbType.VarChar).Value = global.userID;
            //SqlDataReader outcomeReader = outcomeCommand.ExecuteReader();

            //decimal totalOutcome = 0;
            //while (outcomeReader.Read())
            //{
            //    string priceString = outcomeReader["Price"].ToString(); // Lấy chuỗi giá trị
            //    decimal price = Convert.ToDecimal(priceString.Split(' ')[0]); // Tách và chuyển đổi thành số

            //    totalOutcome += price; // Cộng dồn vào tổng
            //}

            //outcomeReader.Close();
            //label10.Text = totalOutcome.ToString();

            //mydb.closeConnection();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message);
            //}

            FormAddItems stdinfor = new FormAddItems();
            stdinfor.TopLevel = false;
            stdinfor.Dock = DockStyle.Fill;
            panelAddItems.Controls.Add(stdinfor);
            stdinfor.Show();
            panelAddItems.BringToFront();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            FormLogin formRegister = new FormLogin();
            formRegister.Show();
        }

        private void CustomControl_RemoveButtonClick(object sender, EventArgs e)
        {
            FirstCustomControl customControl = (FirstCustomControl)sender;

            // Lấy thông tin duy nhất để xác định dữ liệu cần xóa (ví dụ: ID của hàng dữ liệu)
            string dataId = customControl.GetDataId(); // Phương thức GetDataId() trả về thông tin duy nhất

            // Xóa dữ liệu từ database
            mydb.openConnection();
            SqlCommand deleteCommand = new SqlCommand("DELETE FROM Item WHERE STT = @id", mydb.getConnection);
            deleteCommand.Parameters.AddWithValue("@id", dataId);
            deleteCommand.ExecuteNonQuery();
            mydb.closeConnection();

            // Xóa control khỏi panel
            flowLayoutPanel1.Controls.Remove(customControl);

            // Cập nhật lại tổng thu nhập và tổng chi tiêu
            UpdateTotalValues();
        }

        private void CustomControl_EditButtonClick(object sender, EventArgs e)
        {
            FirstCustomControl customControl = (FirstCustomControl)sender;

            // Lấy thông tin duy nhất để xác định dữ liệu cần cập nhật (ví dụ: ID của hàng dữ liệu)
            string dataId = customControl.GetDataId();

            // Lấy nội dung mới từ các thành phần trong control
            string newName = customControl.textBoxName.Text;
            string newPrice = customControl.textBoxPrice.Text;
            string newNote = customControl.textBoxNote.Text;
            string newStatic = customControl.textBoxStatic.Text;

            // Kiểm tra dữ liệu trống
            if (string.IsNullOrEmpty(newName) || string.IsNullOrEmpty(newPrice) || string.IsNullOrEmpty(newNote) || string.IsNullOrEmpty(newStatic))
            {
                // Hiển thị thông báo lỗi
                NotificationForm notificationForms = new NotificationForm("Dữ liệu đang bị trống. Vui lòng không để trống dữ liệu", Properties.Resources._7693271);
                notificationForms.ShowDialog();
                return; // Dừng sviệc cập nhật dữ liệu
            }
            // Cập nhật dữ liệu trong database
            mydb.openConnection();
            SqlCommand updateCommand = new SqlCommand("UPDATE Item SET Name = @name, Price = @price, Note = @note, Static = @static WHERE STT = @id", mydb.getConnection);
            updateCommand.Parameters.AddWithValue("@name", newName);
            updateCommand.Parameters.AddWithValue("@price", newPrice);
            updateCommand.Parameters.AddWithValue("@note", newNote);
            updateCommand.Parameters.AddWithValue("@static", newStatic);
            updateCommand.Parameters.AddWithValue("@id", dataId);
            updateCommand.ExecuteNonQuery();
            mydb.closeConnection();

            // Cập nhật nội dung mới cho control
            customControl.textBoxName.Text = newName;
            customControl.textBoxPrice.Text = newPrice;
            customControl.textBoxNote.Text = newNote;
            customControl.textBoxStatic.Text = newStatic;

            // Hiển thị thông báo hoặc thực hiện các tác vụ khác sau khi cập nhật thành công
            NotificationForm notificationForm = new NotificationForm("Dữ liệu đã được cập nhật thành công.", Properties.Resources.Picture10);
            notificationForm.ShowDialog();

            // Cập nhật lại tổng thu nhập và tổng chi tiêu
            UpdateTotalValues();
        }

        private void UpdateTotalValues()
        {
            mydb.openConnection();
            // Tính toán lại tổng thu nhập (Income)
            string incomeQuery = "SELECT Price FROM Item WHERE Static = 'income' AND userID = @usincome";

            SqlCommand incomeCommand = new SqlCommand(incomeQuery, mydb.getConnection);
            incomeCommand.Parameters.Add("@usincome", SqlDbType.VarChar).Value = global.userID;
            SqlDataReader incomeReader = incomeCommand.ExecuteReader();

            decimal totalIncome = 0;
            while (incomeReader.Read())
            {
                string priceString = incomeReader["Price"].ToString(); // Lấy chuỗi giá trị
                decimal price = Convert.ToDecimal(priceString.Split(' ')[0]); // Tách và chuyển đổi thành số

                totalIncome += price; // Cộng dồn vào tổng
            }

            incomeReader.Close();
            label9.Text = totalIncome.ToString();

            // Tính toán lại tổng chi tiêu (Outcome)
            string outcomeQuery = "SELECT Price FROM Item WHERE Static = 'Outcome' AND userID = @usoutcome";

            SqlCommand outcomeCommand = new SqlCommand(outcomeQuery, mydb.getConnection);
            outcomeCommand.Parameters.Add("@usoutcome", SqlDbType.VarChar).Value = global.userID;
            SqlDataReader outcomeReader = outcomeCommand.ExecuteReader();

            decimal totalOutcome = 0;
            while (outcomeReader.Read())
            {
                string priceString = outcomeReader["Price"].ToString(); // Lấy chuỗi giá trị
                decimal price = Convert.ToDecimal(priceString.Split(' ')[0]); // Tách và chuyển đổi thành số

                totalOutcome += price; // Cộng dồn vào tổng
            }

            outcomeReader.Close();
            mydb.closeConnection();
            label10.Text = totalOutcome.ToString();
        }

        private void buttonCreater_Click(object sender, EventArgs e)
        {
            SidePanel.Height = buttonCreater.Height;
            SidePanel.Top = buttonCreater.Top;

            mydb.openConnection();
            // Kiểm tra xem bảng Customer có userID = global.userID hay không
            SqlCommand checkCustomerCommand = new SqlCommand("SELECT Picture FROM Customer WHERE userID = @us", mydb.getConnection);
            checkCustomerCommand.Parameters.Add("@us", SqlDbType.VarChar).Value = global.userID;
            object pictureResult = checkCustomerCommand.ExecuteScalar();

            if (pictureResult != null && pictureResult != DBNull.Value)
            {
                // Có hình ảnh trong bảng Customer, tải lên PictureBox
                byte[] pictureBytes = (byte[])pictureResult;
                using (MemoryStream ms = new MemoryStream(pictureBytes))
                {
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            else
            {
                // Không có hình ảnh trong bảng Customer, không tải lên PictureBox
            }
            mydb.closeConnection();
            // Kiểm tra xem panel đã chứa form nào chưa
            if (panel6.Controls.Count > 0)
            {
                // Xóa form hiện tại khỏi panel
                panel6.Controls.Clear();
            }

            CreatePaymentForm stdinfor = new CreatePaymentForm(this);
            stdinfor.TopLevel = false;
            stdinfor.Dock = DockStyle.Fill;
            panel6.Controls.Add(stdinfor);
            stdinfor.Show();
            panel6.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button3.Height;
            SidePanel.Top = button3.Top;


            mydb.openConnection();
            // Kiểm tra xem bảng Customer có userID = global.userID hay không
            SqlCommand checkCustomerCommand = new SqlCommand("SELECT Picture FROM Customer WHERE userID = @us", mydb.getConnection);
            checkCustomerCommand.Parameters.Add("@us", SqlDbType.VarChar).Value = global.userID;
            object pictureResult = checkCustomerCommand.ExecuteScalar();

            if (pictureResult != null && pictureResult != DBNull.Value)
            {
                // Có hình ảnh trong bảng Customer, tải lên PictureBox
                byte[] pictureBytes = (byte[])pictureResult;
                using (MemoryStream ms = new MemoryStream(pictureBytes))
                {
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            else
            {
                // Không có hình ảnh trong bảng Customer, không tải lên PictureBox
            }
            mydb.closeConnection();
            // Kiểm tra xem panel đã chứa form nào chưa
            if (panel7.Controls.Count > 0)
            {
                // Xóa form hiện tại khỏi panel
                panel7.Controls.Clear();
            }

            Budgets stdinfors = new Budgets(global.userID);
            stdinfors.TopLevel = false;
            panel7.Controls.Add(stdinfors);
            stdinfors.Show();
            panel7.BringToFront();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button7.Height;
            SidePanel.Top = button7.Top;

            mydb.openConnection();
            // Kiểm tra xem bảng Customer có userID = global.userID hay không
            SqlCommand checkCustomerCommand = new SqlCommand("SELECT Picture FROM Customer WHERE userID = @us", mydb.getConnection);
            checkCustomerCommand.Parameters.Add("@us", SqlDbType.VarChar).Value = global.userID;
            object pictureResult = checkCustomerCommand.ExecuteScalar();

            if (pictureResult != null && pictureResult != DBNull.Value)
            {
                // Có hình ảnh trong bảng Customer, tải lên PictureBox
                byte[] pictureBytes = (byte[])pictureResult;
                using (MemoryStream ms = new MemoryStream(pictureBytes))
                {
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            else
            {
                // Không có hình ảnh trong bảng Customer, không tải lên PictureBox
            }
            mydb.closeConnection();

            // Kiểm tra xem panel đã chứa form nào chưa
            if (panel8.Controls.Count > 0)
            {
                // Xóa form hiện tại khỏi panel
                panel8.Controls.Clear();
            }

            CustomerForm stdinforss = new CustomerForm();
            stdinforss.TopLevel = false;
            panel8.Controls.Add(stdinforss);
            stdinforss.Show();
            panel8.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button4.Height;
            SidePanel.Top = button4.Top;

            panel10.Controls.Clear(); // Xóa các button hiện tại trong panel

            try
            {
                mydb.openConnection();

                string query = "SELECT Char FROM oder";
                SqlCommand command = new SqlCommand(query, mydb.getConnection);
                SqlDataReader reader = command.ExecuteReader();

                int buttonSpacing = 10;
                Size buttonSize = new Size(106, 100);
                
                int topPosition = 0;
                int leftPosition = 0;

                HashSet<int> uniqueChairs = new HashSet<int>(); // Sử dụng HashSet để lưu trữ các giá trị bàn duy nhất

                while (reader.Read())
                {
                    int chairNumber = (int)reader["Char"];
                    uniqueChairs.Add(chairNumber);
                }

                reader.Close();
                mydb.closeConnection();

                foreach (int chairNumber in uniqueChairs)
                {
                    Button chairButton = new Button();
                    chairButton.Text = "Bàn " + chairNumber;
                    chairButton.Size = buttonSize;
                    chairButton.Location = new Point(leftPosition, topPosition);
                    chairButton.Tag = chairNumber;

                    chairButton.BackColor = Color.FromArgb(38, 180, 147); 
                    chairButton.ForeColor = Color.FromArgb(255, 255, 255); // Màu chữ là trắng (RGB: 255, 255, 255)
                    chairButton.Font = new Font("Century Gothic", 14, FontStyle.Bold);

                    chairButton.Click += ChairButton_Click; // Đính kèm phương thức ChairButton_Click vào sự kiện Click

                    panel10.Controls.Add(chairButton);

                    leftPosition += buttonSize.Width + buttonSpacing;

                    if (leftPosition + buttonSize.Width + buttonSpacing > panel10.Width)
                    {
                        topPosition += buttonSize.Height + buttonSpacing;
                        leftPosition = 0;
                    }
                }
                panel10.BringToFront();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ChairButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string chairNumber = clickedButton.Tag.ToString(); // Lấy giá trị "chair" từ thuộc tính Tag của button

            if (panelPayment.Controls.Count > 0)
            {
                // Xóa form hiện tại khỏi panel
                panelPayment.Controls.Clear();
            }
            // Tạo một instance của form mới và truyền giá trị "chair" qua constructor
            FormPayment newForm = new FormPayment(chairNumber,this);
            newForm.TopLevel = false;
            panelPayment.Controls.Add(newForm);
            newForm.Show();
            panelPayment.BringToFront();
             
        }

        private void button14_MouseEnter(object sender, EventArgs e)
        {
            panel9.BackColor = Color.White;
            labelshow.Text = "App by team 7\nMember:\nPhùng Huy Hoàng\nPhạm Đình Hiếu";
            labelshow.Location = new Point(0, 0);
            panel9.Visible = true;
        }

        private void button14_MouseLeave(object sender, EventArgs e)
        {
            panel9.Visible = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonStatic_Click(object sender, EventArgs e)
        {
            SidePanel.Height = buttonStatic.Height;
            SidePanel.Top = buttonStatic.Top;


            // Kiểm tra xem panel đã chứa form nào chưa
            if (panelStatic.Controls.Count > 0)
            {
                // Xóa form hiện tại khỏi panel
                panelStatic.Controls.Clear();
            }

            FormStatic stdinforss = new FormStatic();
            stdinforss.TopLevel = false;
            panelStatic.Controls.Add(stdinforss);
            stdinforss.Show();
            panelStatic.BringToFront();
        }

        //public bool isclick = true;

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    if (isclick)
        //    {
        //        this.WindowState = FormWindowState.Maximized;
        //        isclick = false;
        //    }
        //    else
        //    {
        //        this.WindowState = FormWindowState.Normal;
        //        isclick = true;
        //    }
        //}
    }
}
