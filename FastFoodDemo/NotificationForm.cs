using System;
using System.Drawing;
using System.Windows.Forms;

namespace FastFoodDemo
{
    public partial class NotificationForm : Form
    {
        public NotificationForm(string message, Image icon)
        {
            InitializeComponent();
            label1.Text = message;
            iconPictureBox.Image = icon;
        }

       

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
