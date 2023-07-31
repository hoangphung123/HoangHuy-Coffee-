using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastFoodDemo.Models;

namespace FastFoodDemo
{
    public partial class FormRegisters : Form
    {
        public FormRegisters()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private List<string> sensitiveWords = new List<string> { "admin", "root", "dcm", "d cm", "ditme", "dit me", "d c m" };

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

        private bool IsValidEmail(string email)
        {
            // Kiểm tra định dạng email bằng biểu thức chính quy
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            passwordTextBox.PasswordChar = '*';
        }


        private void confirmPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            confirmPasswordTextBox.PasswordChar = '*';

        }


        Random random = new Random();
        int otp;

        private void sendOTPButton_Click(object sender, EventArgs e)
        {
            try
            {
                otp = random.Next(100000, 1000000);

                var fromAddress = new MailAddress("phunghoanghuy8@gmail.com");
                var toAddress = new MailAddress(emailTextBox.ToString());
                const string frompass = "whyxjognduwfapuh";
                const string subject = "OTP code";
                string body = otp.ToString();

                var smtp = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, frompass),
                    Timeout = 200000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                //MessageBox.Show("OTP have send to email");
                NotificationForm notificationForms = new NotificationForm("OTP have send to email", Properties.Resources._7693271);
                notificationForms.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            string ID = "user" + random.Next(100, 1000);
            string username = usenameTextBox.Text;
            string password = passwordTextBox.Text;
            string email = emailTextBox.Text;
            string confirmPassword = confirmPasswordTextBox.Text;

            if (password != confirmPassword)
            {
                //MessageBox.Show("Password and Confirm Password do not match.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
                NotificationForm notificationForms = new NotificationForm("Password and Confirm Password do not match", Properties.Resources._7693271);
                notificationForms.ShowDialog();
            }

            if (username.Length == 0 || password.Length == 0)
            {
                //MessageBox.Show("Please fill in all required fields.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
                NotificationForm notificationForms = new NotificationForm("Please fill in all required fields.", Properties.Resources._7693271);
                notificationForms.ShowDialog();
            }

            NEWUSER newuser = new NEWUSER();

            // Check if username exists
            if (newuser.checkUsernameExists(username))
            {
                //MessageBox.Show("Username already exists.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
                NotificationForm notificationForms = new NotificationForm("Username already exists.", Properties.Resources._7693271);
                notificationForms.ShowDialog();
            }

            if (otpTextBox.Text == otp.ToString())
            {
                //try
                //{
                    if (IsValidEmail(email))
                    {
                        bool success = newuser.insertUser(ID, username, password, email);
                        if (success)
                        {
                            //MessageBox.Show("Registration successful.", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            NotificationForm notificationForms = new NotificationForm("Registration successful.", Properties.Resources._7693271);
                            notificationForms.ShowDialog();


                    }
                        else
                        {
                            //MessageBox.Show("Error occurred while registering.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            NotificationForm notificationForms = new NotificationForm("Error occurred while registering.", Properties.Resources._7693271);
                            notificationForms.ShowDialog();
                         }
                    }
                    else
                    {
                        MessageBox.Show("Email is not formatted correctly.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                //}
                //catch
                //{
                //    MessageBox.Show("Error occurred while registering.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            else
            {
                // OTP is incorrect, show error message
                MessageBox.Show("OTP is incorrect. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin formRegister = new FormLogin();
            formRegister.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            FormLogin formRegister = new FormLogin();
            formRegister.Show();
        }
    }

        
    
}
