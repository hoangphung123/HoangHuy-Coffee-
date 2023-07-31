using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace FastFoodDemo
{
    public partial class FirstCustomControl : UserControl
    {
        public FirstCustomControl()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }



        public event EventHandler EditButtonClick;
        public event EventHandler RemoveButtonClick;

        private string dataId; // Thuộc tính để lưu trữ thông tin duy nhất của control (ví dụ: ID của hàng dữ liệu)

        public string GetDataId()
        {
            return dataId;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Khi nút "Remove" được nhấn, gọi sự kiện RemoveButtonClick để thông báo cho Form1
            RemoveButtonClick.Invoke(this, EventArgs.Empty);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditButtonClick.Invoke(this, EventArgs.Empty);
        }

        private void FirstCustomControl_Load(object sender, EventArgs e)
        {

        }

        public void SetData(DataRow row)
        {
            // Thiết lập giá trị cho các thành phần trong UserControl dựa trên dữ liệu từ DataRow
            textBoxName.Text = row["Name"].ToString();
            textBoxPrice.Text = row["Price"].ToString();
            textBoxNote.Text = row["Note"].ToString();
            textBoxStatic.Text = row["Static"].ToString();

            byte[] imageData = (byte[])row["Picture"];
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                pictureBoxItem.Image = Image.FromStream(ms);
            }
            dataId = row["STT"].ToString();
        }

        private void textBoxNote_TextChanged(object sender, EventArgs e)
        {
            textBoxNote.Enter += (s, ev) =>
            {
                // Đặt màu nền khi TextBox được focus
                textBoxNote.BackColor = Color.LightBlue; // Màu xanh nhạt hoặc màu khác tùy chọn
            };

            textBoxNote.Leave += (s, ev) =>
            {
                // Đặt lại màu nền khi TextBox không còn focus
                textBoxNote.BackColor = SystemColors.Window;
            };
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            textBoxName.Enter += (s, ev) =>
            {
                // Đặt màu nền khi TextBox được focus
                textBoxName.BackColor = Color.LightBlue; // Màu xanh nhạt hoặc màu khác tùy chọn
            };

            textBoxName.Leave += (s, ev) =>
            {
                // Đặt lại màu nền khi TextBox không còn focus
                textBoxName.BackColor = SystemColors.Window;
            };
        }

        private void textBoxPrice_TextChanged(object sender, EventArgs e)
        {
            textBoxPrice.Enter += (s, ev) =>
            {
                // Đặt màu nền khi TextBox được focus
                textBoxPrice.BackColor = Color.LightBlue; // Màu xanh nhạt hoặc màu khác tùy chọn
            };

            textBoxPrice.Leave += (s, ev) =>
            {
                // Đặt lại màu nền khi TextBox không còn focus
                textBoxPrice.BackColor = SystemColors.Window;
            };
        }

        private void textBoxStatic_TextChanged(object sender, EventArgs e)
        {
            textBoxStatic.Enter += (s, ev) =>
            {
                // Đặt màu nền khi TextBox được focus
                textBoxStatic.BackColor = Color.LightBlue; // Màu xanh nhạt hoặc màu khác tùy chọn
            };

            textBoxStatic.Leave += (s, ev) =>
            {
                // Đặt lại màu nền khi TextBox không còn focus
                textBoxStatic.BackColor = SystemColors.Window;
            };
        }
    }
}
