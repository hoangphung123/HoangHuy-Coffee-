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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }


        private void FormRegister_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormRegisters formLogin = new FormRegisters();
            

            
            formLogin.Show();
            
        }

        private void buttonSignIn_Click(object sender, EventArgs e)
        {
            DB mydb = new DB();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM user_login WHERE username = @User AND password = @Pass", mydb.getConnection);
                command.CommandTimeout = 120;
                command.Parameters.Add("@User", SqlDbType.VarChar).Value = textBoxUserName.Text;
                command.Parameters.Add("@Pass", SqlDbType.VarChar).Value = textBoxPassWord.Text;
                adapter.SelectCommand = command;
                adapter.Fill(table);
                if (table.Rows.Count > 0)
                {
                    // Lấy giá trị ID từ bảng table
                    string userID = table.Rows[0]["ID"].ToString();
                    
                    // Gán giá trị ID vào biến userID trong class Global
                    global.setUserID(userID);

                    string username = table.Rows[0]["username"].ToString();
                    global.setUserName(username);
                    // Mở ứng dụng nếu đúng username và pass
                    this.Hide();
                    Main form = new Main(username);
                    form.Show();

                }
            }
            catch 
            {
                //MessageBox.Show("Invalid Username or Password", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NotificationForm notificationForms = new NotificationForm("Invalid Username or Password", Properties.Resources._7693271);
                notificationForms.ShowDialog();
            }
            
        }

        private void textBoxPassWord_TextChanged(object sender, EventArgs e)
        {
            textBoxPassWord.PasswordChar = '*';
        }
    }
}
