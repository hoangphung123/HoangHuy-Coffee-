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
using System.Text.RegularExpressions;

namespace FastFoodDemo
{
    public partial class FormCreateBudget : Form
    {
        private Image selectedImage;
        private Button selectedButton;
        private bool isButtonSelected = false;
        public FormCreateBudget()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if ((opf.ShowDialog() == DialogResult.OK))
            {
                PictureBoxBudget.Image = Image.FromFile(opf.FileName);
            }
           
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            isButtonSelected = true;
            if (selectedButton != null)
            {
                selectedButton.FlatAppearance.BorderColor = SystemColors.Control;
            }

            // Gán button mới cho biến selectedButton và đặt viền cho button mới
            selectedButton = button5;
            buttonHome.FlatAppearance.BorderSize = 1;
            selectedButton.FlatAppearance.BorderColor = Color.Green;

            // Gán selectedImage với ảnh từ button 1
            selectedImage = Properties.Resources.Picturehome;
            PictureBoxBudget.Image = null;
        }

        private void buttonInvest_Click(object sender, EventArgs e)
        {

            isButtonSelected = true;
            if (selectedButton != null)
            {
                selectedButton.FlatAppearance.BorderColor = SystemColors.Control;
            }

            // Gán button mới cho biến selectedButton và đặt viền cho button mới
            selectedButton = button5;
            buttonInvest.FlatAppearance.BorderSize = 1;
            selectedButton.FlatAppearance.BorderColor = Color.Green;

            // Gán selectedImage với ảnh từ button 1
            selectedImage = Properties.Resources.PictureMoney;
            PictureBoxBudget.Image = null;
        }

        private void buttonFamily_Click(object sender, EventArgs e)
        {
            isButtonSelected = true;
            if (selectedButton != null)
            {
                selectedButton.FlatAppearance.BorderColor = SystemColors.Control;
            }

            // Gán button mới cho biến selectedButton và đặt viền cho button mới
            selectedButton = button5;
            buttonFamily.FlatAppearance.BorderSize = 1;
            selectedButton.FlatAppearance.BorderColor = Color.Green;

            // Gán selectedImage với ảnh từ button 1
            selectedImage = Properties.Resources.Family;
            PictureBoxBudget.Image = null;
        }

        private void buttonCar_Click(object sender, EventArgs e)
        {
            isButtonSelected = true;
            if (selectedButton != null)
            {
                selectedButton.FlatAppearance.BorderColor = SystemColors.Control;
            }

            // Gán button mới cho biến selectedButton và đặt viền cho button mới
            selectedButton = button5;
            buttonCar.FlatAppearance.BorderSize = 1;
            selectedButton.FlatAppearance.BorderColor = Color.Green;

            // Gán selectedImage với ảnh từ button 1
            selectedImage = Properties.Resources.Family;
            PictureBoxBudget.Image = null;
        }

        private void textBoxName_Leave(object sender, EventArgs e)
        {
            if (!isButtonSelected && selectedButton != null)
            {
                selectedButton.FlatAppearance.BorderColor = Color.FromArgb(255, Color.Green);
            }
        }

        private void textBoxNote_Leave(object sender, EventArgs e)
        {
            if (!isButtonSelected && selectedButton != null)
            {
                selectedButton.FlatAppearance.BorderColor = Color.FromArgb(255, Color.Green);
            }
        }

        private void textBoxPrice_Leave(object sender, EventArgs e)
        {
            if (!isButtonSelected && selectedButton != null)
            {
                selectedButton.FlatAppearance.BorderColor = Color.FromArgb(255, Color.Green);
            }
        }

        private byte[] ConvertImageToByteArray(Image image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, image.RawFormat);
                return memoryStream.ToArray();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DB mydb = new DB();
            
            byte[] imageBytes = null;
            if (selectedImage == null || PictureBoxBudget.Image != null)
            {
                if (PictureBoxBudget.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        PictureBoxBudget.Image.Save(ms, PictureBoxBudget.Image.RawFormat);
                        imageBytes = ms.ToArray();
                    }
                    MemoryStream pic = new MemoryStream(imageBytes);
                    selectedImage = Image.FromStream(pic);
                }
            }


            // Kiểm tra xem đã chọn một button ảnh và một label hay chưa
            if (selectedImage == null || textBoxName == null || textBoxPrice == null)
            {
                // Hiển thị thông báo lỗi nếu chưa chọn đủ thông tin
                NotificationForm notificationForms = new NotificationForm("Dữ liệu đang bị trống. Vui lòng không để trống dữ liệu", Properties.Resources._7693271);
                notificationForms.ShowDialog();
                return;
            }

            // Lấy dữ liệu từ các controls
            string name = textBoxName.Text;
            string price = textBoxPrice.Text;
            string note = textBoxNote.Text;
            
            
            
            Image picture = selectedImage;



            // Tạo kết nối đến cơ sở dữ liệu

            // Mở kết nối
            mydb.openConnection();

            // Tạo câu lệnh INSERT
            string query = "INSERT INTO [dbo].[budget] ([Name], [Price], [Note], [Picture], [userID]) VALUES (@Name, @Price, @Note, @Picture, @userID)";

            // Tạo đối tượng SqlCommand
            using (SqlCommand command = new SqlCommand(query, mydb.getConnection))
            {
                // Thêm các tham số vào câu lệnh INSERT
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Note", note);
                
                command.Parameters.AddWithValue("@Picture", ConvertImageToByteArray(picture));
                
                command.Parameters.AddWithValue("@userID", global.userID); // Thay thế "your_user_id" bằng giá trị tương ứng

                // Thực thi câu lệnh INSERT
                command.ExecuteNonQuery();
            }

            // Đóng kết nối
            mydb.closeConnection();

            // Hiển thị thông báo thành công
            MessageBox.Show("Dữ liệu đã được lưu vào cơ sở dữ liệu.");
        }
    }
}
