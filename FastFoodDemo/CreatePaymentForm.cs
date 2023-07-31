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
    public partial class CreatePaymentForm : Form
    {

        DB mydb = new DB();

        private Image selectedImage;

        private Main main;
        public CreatePaymentForm(Main main)
        {
            InitializeComponent();
            this.main = main;

            
        }

        public CreatePaymentForm(Budgets budgets)
        {
        }

        private Button selectedButton;
        private Label selectedLabel;
        

        private bool isButtonSelected = false;

        private void CheckExistingData()
        {
            try
            {
                
                mydb.openConnection();

                    // Truy vấn dữ liệu từ bảng "oder"
                    string query = "SELECT char FROM oder";
                    SqlCommand command = new SqlCommand(query, mydb.getConnection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int chair = Convert.ToInt32(reader["char"]);

                    // Tìm button tương ứng với ghế
                        Button button = GetAllButtons().FirstOrDefault(b => b.Name == "button_" + chair);

                    // ...



                        if (chair == 1)
                        {
                            // Thay đổi màu và trạng thái của button
                            button_1.BackColor = Color.FromArgb(40, 121, 116);
                            button_1.ForeColor = Color.White;
                            button_1.Enabled = false;
                        }
                        else if (chair == 2)
                        {
                            button_2.BackColor = Color.FromArgb(40, 121, 116);
                            button_2.ForeColor = Color.White;
                            button_2.Enabled = false;
                        }
                        else if (chair == 3)
                        {
                            button_3.BackColor = Color.FromArgb(40, 121, 116);
                            button_3.ForeColor = Color.White;
                            button_3.Enabled = false;
                        }
                        else if (chair == 4)
                        {
                            button_4.BackColor = Color.FromArgb(40, 121, 116);
                            button_4.ForeColor = Color.White;
                            button_4.Enabled = false;
                        }
                        else if (chair == 5)
                        {
                            button_5.BackColor = Color.FromArgb(40, 121, 116);
                            button_5.ForeColor = Color.White;
                            button_5.Enabled = false;
                        }
                        else if (chair == 6)
                        {
                            button_6.BackColor = Color.FromArgb(40, 121, 116);
                            button_6.ForeColor = Color.White;
                            button_6.Enabled = false;
                        }
                        else if (chair == 7)
                        {
                            button_7.BackColor = Color.FromArgb(40, 121, 116);
                            button_7.ForeColor = Color.White;
                            button_7.Enabled = false;
                        }
                        else if (chair == 8)
                        {
                            button_8.BackColor = Color.FromArgb(40, 121, 116);
                            button_8.ForeColor = Color.White;
                            button_8.Enabled = false;
                        }
                        else if (chair == 9)
                        {
                            button_9.BackColor = Color.FromArgb(40, 121, 116);
                            button_9.ForeColor = Color.White;
                            button_9.Enabled = false;
                        }
                        else if (chair == 10)
                        {
                            button_10.BackColor = Color.FromArgb(40, 121, 116);
                            button_10.ForeColor = Color.White;
                            button_10.Enabled = false;
                        }
                        else if (chair == 11)
                        {
                            button_11.BackColor = Color.FromArgb(40, 121, 116);
                            button_11.ForeColor = Color.White;
                            button_11.Enabled = false;
                        }
                        else if (chair == 12)
                        {
                            button_12.BackColor = Color.FromArgb(40, 121, 116);
                            button_12.ForeColor = Color.White;
                            button_12.Enabled = false;
                        }
                        else if (chair == 13)
                        {
                            button_13.BackColor = Color.FromArgb(40, 121, 116);
                            button_13.ForeColor = Color.White;
                            button_13.Enabled = false;
                        }
                        else if (chair == 14)
                        {
                            button_14.BackColor = Color.FromArgb(40, 121, 116);
                            button_14.ForeColor = Color.White;
                            button_14.Enabled = false;
                        }
                        else if (chair == 15)
                        {
                            button_15.BackColor = Color.FromArgb(40, 121, 116);
                            button_15.ForeColor = Color.White;
                            button_15.Enabled = false;
                        }
                    }

                    reader.Close();
                mydb.closeConnection();
                
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu cần thiết
                
                NotificationForm notificationForms = new NotificationForm("Error: " + ex.Message, Properties.Resources._7693271);
                notificationForms.ShowDialog();
                return;
            }
        }

        private IEnumerable<Button> GetAllButtons()
        {
            return this.Controls.OfType<Button>();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (!isButtonSelected && selectedButton != null)
            {
                selectedButton.FlatAppearance.BorderColor = Color.FromArgb(255, Color.Green);
               

            }

            if (selectedLabel != null)
            {
                selectedLabel.ForeColor = Color.Blue;
            }

        }

       

        private void CreatePaymentForm_Load(object sender, EventArgs e)
        {
            CheckExistingData();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            DB mydb = new DB();
            // Kiểm tra xem đã chọn một button ảnh và một label hay chưa
            if (selectedButton == null || selectedLabel == null /*|| textBoxName == null || textBoxPrice == null*/)
            {
                // Hiển thị thông báo lỗi nếu chưa chọn đủ thông tin
                NotificationForm notificationForms = new NotificationForm("Dữ liệu đang bị trống. Vui lòng không để trống dữ liệu", Properties.Resources._7693271);
                notificationForms.ShowDialog();
                return;
            }

            // Lấy dữ liệu từ các controls
            //string name = textBoxName.Text;
            //string price = textBoxPrice.Text;
            //string note = textBoxNote.Text;
            string staticText = selectedLabel.Text;
            Image picture = selectedImage;
            DateTime date = DateTime.Now;

            // Tạo kết nối đến cơ sở dữ liệu

            // Mở kết nối
                mydb.openConnection();

                // Tạo câu lệnh INSERT
                string query = "INSERT INTO [dbo].[Item] ([Name], [Price], [Note], [Static], [Picture], [Date], [userID]) VALUES (@Name, @Price, @Note, @Static, @Picture, @Date, @userID)";

                // Tạo đối tượng SqlCommand
                using (SqlCommand command = new SqlCommand(query,mydb.getConnection))
                {
                    // Thêm các tham số vào câu lệnh INSERT
                    //command.Parameters.AddWithValue("@Name", name);
                    //command.Parameters.AddWithValue("@Price", price);
                    //command.Parameters.AddWithValue("@Note", note);
                    command.Parameters.AddWithValue("@Static", staticText);
                    command.Parameters.AddWithValue("@Picture", ConvertImageToByteArray(picture));
                    command.Parameters.AddWithValue("@Date", date);
                    command.Parameters.AddWithValue("@userID", global.userID); // Thay thế "your_user_id" bằng giá trị tương ứng

                    // Thực thi câu lệnh INSERT
                    command.ExecuteNonQuery();
                }

                // Đóng kết nối
                mydb.closeConnection();

                // Hiển thị thông báo thành công
                //MessageBox.Show("Dữ liệu đã được lưu vào cơ sở dữ liệu.");
                NotificationForm notificationFormss = new NotificationForm("Dữ liệu đã được lưu vào cơ sở dữ liệu", Properties.Resources._7693271);
                notificationFormss.ShowDialog();


        }

        private byte[] ConvertImageToByteArray(Image image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, image.RawFormat);
                return memoryStream.ToArray();
            }
        }


        private void OpenOrderForm(int tableNumber)
        {
            //Budgets orderForm = new Budgets(tableNumber, main);

            //orderForm.Show();

            main.sidePanel.Height = main.Button3.Height;
            main.sidePanel.Top = main.Button3.Top;



            // Kiểm tra xem panel đã chứa form nào chưa
            if (main.panel7.Controls.Count > 0)
            {
                // Xóa form hiện tại khỏi panel
                main.panel7.Controls.Clear();
            }

            Budgets stdinfors = new Budgets(tableNumber, main);
            stdinfors.TopLevel = false;
            main.panel7.Controls.Add(stdinfors);
            stdinfors.Show();
            main.panel7.BringToFront();


        }
        private void button_1_Click(object sender, EventArgs e)
        {
            OpenOrderForm(1);
        }

        private void button_2_Click(object sender, EventArgs e)
        {
            OpenOrderForm(2);
        }

        private void button_3_Click(object sender, EventArgs e)
        {
            OpenOrderForm(3);
        }

        private void button_4_Click(object sender, EventArgs e)
        {
            OpenOrderForm(4);
        }

        private void button_5_Click(object sender, EventArgs e)
        {
            OpenOrderForm(5);
        }

        private void button_6_Click(object sender, EventArgs e)
        {
            OpenOrderForm(6);
        }

        private void button_7_Click(object sender, EventArgs e)
        {
            OpenOrderForm(7);
        }

        private void button_8_Click(object sender, EventArgs e)
        {
            OpenOrderForm(8);
        }

        private void button_9_Click(object sender, EventArgs e)
        {
            OpenOrderForm(9);
        }

        private void button_10_Click(object sender, EventArgs e)
        {
            OpenOrderForm(10);
        }

        private void button_11_Click(object sender, EventArgs e)
        {
            OpenOrderForm(11);
        }

        private void button_12_Click(object sender, EventArgs e)
        {
            OpenOrderForm(12);
        }

        private void button_13_Click(object sender, EventArgs e)
        {
            OpenOrderForm(13);
        }

        private void button_14_Click(object sender, EventArgs e)
        {
            OpenOrderForm(14);
        }

        private void button_15_Click(object sender, EventArgs e)
        {
            OpenOrderForm(15);
        }
    }
}
