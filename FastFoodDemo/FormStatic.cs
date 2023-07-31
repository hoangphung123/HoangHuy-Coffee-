using FastFoodDemo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastFoodDemo
{
    public partial class FormStatic : Form
    {
        public FormStatic()
        {
            InitializeComponent();
        }

        DB mydb = new DB();
        NEWUSER user = new NEWUSER();
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            mydb.openConnection();
            DateTime selectedDateTime = dateTimePicker1.Value; // Lấy ngày và giờ được chọn từ DateTimePicker

            // Truy vấn cơ sở dữ liệu để lấy dữ liệu dựa trên CreatedDate
            string query = "SELECT * FROM saveOder WHERE CAST(CreatedDate AS DATE) = CAST(@selectedDateTime AS DATE)";
            SqlCommand command = new SqlCommand(query, mydb.getConnection);
            command.Parameters.AddWithValue("@selectedDateTime", selectedDateTime);

            mydb.openConnection(); // Mở kết nối đến cơ sở dữ liệu

            // Thực hiện truy vấn và lấy dữ liệu vào DataTable
            DataTable table = user.getItems(command);

            mydb.closeConnection(); // Đóng kết nối đến cơ sở dữ liệu

            // Hiển thị dữ liệu trên DataGridView
            dataGridView1.DataSource = table;

            // Tính tổng cột TotalPrice
            double totalPrice = 0;
            foreach (DataRow row in table.Rows)
            {
                totalPrice += Convert.ToDouble(row["TotalPrice"]);
            }

            // Hiển thị tổng cột TotalPrice trên TextBox1
            textBox1.Text = totalPrice.ToString();
            mydb.closeConnection();
        }
    }
}
