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
using static System.Windows.Forms.AxHost;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FastFoodDemo
{
    public partial class FormAddItems : Form
    {
        public FormAddItems()
        {
            InitializeComponent();
        }

        NEWUSER user = new NEWUSER();
        DB mydb = new DB();

        public void fillGrid(SqlCommand command)
        {
            dataGridView1.ReadOnly = true;

            DataGridViewImageColumn piccol = new DataGridViewImageColumn();

            dataGridView1.RowTemplate.Height = 30;

            dataGridView1.DataSource = user.getItems(command);

            piccol = (DataGridViewImageColumn)dataGridView1.Columns[2];

            piccol.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView1.AllowUserToAddRows = false;
        }





        private void FormAddItems_Load(object sender, EventArgs e)
        {
            LoadDataGridViewData();
        }


        private void LoadDataGridViewData()
        {
            SqlCommand command = new SqlCommand("SELECT Name AS [Tên Món], Price AS [Giá Món], Picture AS [Hình Ảnh] FROM budget");
            fillGrid(command);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "SELECT Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(opf.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = " ";
            textBox2.Text = " ";
            pictureBox1.Image = null;
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            byte[] pic;

            pic = (byte[])dataGridView1.CurrentRow.Cells[2].Value;

            MemoryStream picture = new MemoryStream(pic); pictureBox1.Image = Image.FromStream(picture);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            if (user.DeleteItems(name))
            {
                MessageBox.Show("Delete Items", "Delete Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // clear fields after delete
                textBox1.Text = "";
                textBox2.Text = "";
                pictureBox1.Image = null;

                
                LoadDataGridViewData();
                
                
            }
            else
            {
                MessageBox.Show("Name not exit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        bool verif()
        {
            if ((textBox1.Text.Trim() == "")
                || (textBox2.Text.Trim() == "")
                || (pictureBox1.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string price = textBox2.Text + " vnd";
            MemoryStream pic = new MemoryStream();

            if (verif())
            {
                pictureBox1.Image.Save(pic, pictureBox1.Image.RawFormat);
                //try 
                {
                    if (user.insertItems(name, price, pic))
                    {
                        MessageBox.Show("New Items Add", "Add Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        LoadDataGridViewData();
                    }
                    else
                    {
                        MessageBox.Show("Error", "Add Items", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                //catch { MessageBox.Show( "id already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }



            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string price = textBox2.Text + " vnd";
            MemoryStream pic = new MemoryStream();

            if (verif())
            {
                pictureBox1.Image.Save(pic, pictureBox1.Image.RawFormat);
                //try 
                {
                    if (user.updateItems(name, price, pic))
                    {
                        MessageBox.Show("Items Update", "Update Items", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadDataGridViewData();
                    }
                    else
                    {
                        MessageBox.Show("Error", "Update Items", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                //catch { MessageBox.Show( "id already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }



            }
        }
    }
}