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

namespace FastFoodDemo
{
    public partial class MySecondCustmControl : UserControl
    {
        public MySecondCustmControl()
        {
            InitializeComponent();
        }

        

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            


            // Khi nút "Remove" được nhấn, gọi sự kiện RemoveButtonClick để thông báo cho Form1
            RemoveButtonClick.Invoke(this, EventArgs.Empty);
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            EditButtonClick.Invoke(this, EventArgs.Empty);
        }

        

        public event EventHandler<CustomEventArgs> AddItemClicked;
        public event EventHandler EditButtonClick;
        public event EventHandler RemoveButtonClick;

        public class CustomEventArgs : EventArgs
        {
            public string ItemName { get; set; }
            public string price { get; set; }
        }

        private string dataId; // Thuộc tính để lưu trữ thông tin duy nhất của control (ví dụ: ID của hàng dữ liệu)

        public string GetDataId()
        {
            return dataId;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        public void SetData(DataRow row)
        {
            // Thiết lập giá trị cho các thành phần trong UserControl dựa trên dữ liệu từ DataRow
            textBoxName.Text = row["Name"].ToString();
            textBoxPrice.Text = row["Price"].ToString();
            
            

            byte[] imageData = (byte[])row["Picture"];
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                pictureBoxItem.Image = Image.FromStream(ms);
            }
            dataId = row["STT"].ToString();
        }

        private void MySecondCustmControl_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            CustomEventArgs args = new CustomEventArgs
            {
                ItemName = textBoxName.Text,
                price = textBoxPrice.Text
            };


            // Kích hoạt sự kiện AddItemClicked và chuyển đối tượng CustomEventArgs
            AddItemClicked.Invoke(this, args);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
           
        }
    }
}
