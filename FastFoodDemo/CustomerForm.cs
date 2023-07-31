using FastFoodDemo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FastFoodDemo.Models.NEWUSER;

namespace FastFoodDemo
{
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if ((opf.ShowDialog() == DialogResult.OK))
            {
                pbxUpFile.Image = Image.FromFile(opf.FileName);
            }
        }

        private List<string> sensitiveWords = new List<string> { "admin", "root", "dcm", "d cm", "ditme", "dit me", "d c m" };

        

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            DB mydb = new DB();
            // Kiểm tra xem đã có thông tin với userID này trong bảng Customer chưa
            bool customerExists;
            
                mydb.openConnection();
                SqlCommand commands = new SqlCommand("SELECT COUNT(*) FROM Customer WHERE userID = @us", mydb.getConnection);
                commands.Parameters.AddWithValue("@us", global.userID);

                int count = (int)commands.ExecuteScalar();
                customerExists = count > 0;
                mydb.closeConnection();
            

            if (customerExists)
            {
                // Lấy thông tin khách hàng từ bảng Customer
                string fullname = "";
                string phone = "";
                string address = "";
                byte[] pictureBytes = null;

                
                    mydb.openConnection();
                    SqlCommand command = new SqlCommand("SELECT Fullname, Phone, Adress, Picture FROM Customer WHERE userID = @us", mydb.getConnection);
                    command.Parameters.AddWithValue("@us", global.userID);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        fullname = reader["Fullname"].ToString();
                        phone = reader["Phone"].ToString();
                        address = reader["Adress"].ToString();
                        pictureBytes = (byte[])reader["Picture"];
                    }
                    reader.Close();
                    mydb.closeConnection();
                

                // Hiển thị thông tin trong các TextBox và pbxUpFile
                usenameTextBox.Text = fullname;
                textBoxPhone.Text = phone;
                textBoxAddress.Text = address;

                using (MemoryStream ms = new MemoryStream(pictureBytes))
                {
                    pbxUpFile.Image = Image.FromStream(ms);
                }
            }
        }

        private void usenameTextBox_TextChanged(object sender, EventArgs e)
        {
            string input = usenameTextBox.Text.Trim().ToLower();

            foreach (string sensitiveWord in sensitiveWords)
            {
                if (input.Contains(sensitiveWord))
                {
                    MessageBox.Show("The username contains a sensitive word. Please choose a different username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    usenameTextBox.Clear();
                    return;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string fullName = usenameTextBox.Text;
            string Phone = textBoxPhone.Text;
            string Address = textBoxAddress.Text;
            byte[] imageBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                pbxUpFile.Image.Save(ms, pbxUpFile.Image.RawFormat);
                imageBytes = (byte[])new ImageConverter().ConvertTo(pbxUpFile.Image, typeof(byte[]));
            }
            MemoryStream pic = new MemoryStream(imageBytes);

            NEWUSER newuser = new NEWUSER();

            // Kiểm tra xem đã có thông tin với userID này trong bảng Customer chưa
            bool customerExists = newuser.checkCustomerExists(global.userID);

            if (customerExists)
            {
                // Thực hiện câu lệnh UPDATE
                bool updateSuccess = newuser.updateCustomer(fullName, Phone, Address, pic);
                if (updateSuccess)
                {
                    // Thành công
                    MessageBox.Show("Thông tin khách hàng đã được cập nhật.");
                }
                else
                {
                    // Lỗi khi cập nhật
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật thông tin khách hàng.");
                }
            }
            else
            {
                // Thực hiện câu lệnh INSERT
                bool insertSuccess = newuser.insertCustomer(fullName, Phone, Address, pic);
                if (insertSuccess)
                {
                    // Thành công
                    MessageBox.Show("Thông tin khách hàng đã được thêm vào.");
                }
                else
                {
                    // Lỗi khi thêm
                    MessageBox.Show("Có lỗi xảy ra khi thêm thông tin khách hàng.");
                }
            }
        }
    }
}
