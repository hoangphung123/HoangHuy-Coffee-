namespace FastFoodDemo
{
    partial class FormCreateBudget
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCreateBudget));
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxNote = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonFamily = new System.Windows.Forms.Button();
            this.buttonHome = new System.Windows.Forms.Button();
            this.buttonInvest = new System.Windows.Forms.Button();
            this.buttonCar = new System.Windows.Forms.Button();
            this.PictureBoxBudget = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxBudget)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.textBoxPrice);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBoxNote);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBoxName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(35, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(595, 171);
            this.panel1.TabIndex = 5;
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPrice.Location = new System.Drawing.Point(158, 120);
            this.textBoxPrice.Multiline = true;
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(418, 33);
            this.textBoxPrice.TabIndex = 25;
            this.textBoxPrice.Leave += new System.EventHandler(this.textBoxPrice_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Elephant", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.SeaGreen;
            this.label3.Location = new System.Drawing.Point(16, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 37);
            this.label3.TabIndex = 24;
            this.label3.Text = "Price";
            // 
            // textBoxNote
            // 
            this.textBoxNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNote.Location = new System.Drawing.Point(158, 67);
            this.textBoxNote.Multiline = true;
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(418, 33);
            this.textBoxNote.TabIndex = 23;
            this.textBoxNote.Leave += new System.EventHandler(this.textBoxNote_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Elephant", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.SeaGreen;
            this.label2.Location = new System.Drawing.Point(16, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 37);
            this.label2.TabIndex = 22;
            this.label2.Text = "Note";
            // 
            // textBoxName
            // 
            this.textBoxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxName.Location = new System.Drawing.Point(155, 13);
            this.textBoxName.Multiline = true;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(421, 33);
            this.textBoxName.TabIndex = 21;
            this.textBoxName.Leave += new System.EventHandler(this.textBoxName_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Elephant", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SeaGreen;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 37);
            this.label1.TabIndex = 20;
            this.label1.Text = "Name";
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(185)))), ((int)(((byte)(100)))));
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button5.Location = new System.Drawing.Point(671, 271);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(246, 36);
            this.button5.TabIndex = 6;
            this.button5.Text = "Oder Image";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(124)))), ((int)(((byte)(33)))));
            this.buttonSave.Font = new System.Drawing.Font(".VnAvant", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonSave.Location = new System.Drawing.Point(671, 324);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(246, 43);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Create";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonFamily
            // 
            this.buttonFamily.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(149)))), ((int)(((byte)(133)))));
            this.buttonFamily.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFamily.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFamily.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(167)))), ((int)(((byte)(1)))));
            this.buttonFamily.Image = ((System.Drawing.Image)(resources.GetObject("buttonFamily.Image")));
            this.buttonFamily.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonFamily.Location = new System.Drawing.Point(338, 207);
            this.buttonFamily.Name = "buttonFamily";
            this.buttonFamily.Size = new System.Drawing.Size(134, 160);
            this.buttonFamily.TabIndex = 4;
            this.buttonFamily.Text = "Family";
            this.buttonFamily.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonFamily.UseVisualStyleBackColor = false;
            this.buttonFamily.Click += new System.EventHandler(this.buttonFamily_Click);
            // 
            // buttonHome
            // 
            this.buttonHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(217)))), ((int)(((byte)(215)))));
            this.buttonHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonHome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(167)))), ((int)(((byte)(1)))));
            this.buttonHome.Image = ((System.Drawing.Image)(resources.GetObject("buttonHome.Image")));
            this.buttonHome.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonHome.Location = new System.Drawing.Point(35, 207);
            this.buttonHome.Name = "buttonHome";
            this.buttonHome.Size = new System.Drawing.Size(134, 160);
            this.buttonHome.TabIndex = 3;
            this.buttonHome.Text = "Home";
            this.buttonHome.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonHome.UseVisualStyleBackColor = false;
            this.buttonHome.Click += new System.EventHandler(this.buttonHome_Click);
            // 
            // buttonInvest
            // 
            this.buttonInvest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(195)))), ((int)(((byte)(185)))));
            this.buttonInvest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonInvest.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInvest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(167)))), ((int)(((byte)(1)))));
            this.buttonInvest.Image = ((System.Drawing.Image)(resources.GetObject("buttonInvest.Image")));
            this.buttonInvest.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonInvest.Location = new System.Drawing.Point(187, 207);
            this.buttonInvest.Name = "buttonInvest";
            this.buttonInvest.Size = new System.Drawing.Size(134, 160);
            this.buttonInvest.TabIndex = 2;
            this.buttonInvest.Text = "Invest";
            this.buttonInvest.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonInvest.UseVisualStyleBackColor = false;
            this.buttonInvest.Click += new System.EventHandler(this.buttonInvest_Click);
            // 
            // buttonCar
            // 
            this.buttonCar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(77)))), ((int)(((byte)(67)))));
            this.buttonCar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(167)))), ((int)(((byte)(1)))));
            this.buttonCar.Image = ((System.Drawing.Image)(resources.GetObject("buttonCar.Image")));
            this.buttonCar.Location = new System.Drawing.Point(496, 207);
            this.buttonCar.Name = "buttonCar";
            this.buttonCar.Size = new System.Drawing.Size(134, 160);
            this.buttonCar.TabIndex = 1;
            this.buttonCar.Text = "Car";
            this.buttonCar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonCar.UseVisualStyleBackColor = false;
            this.buttonCar.Click += new System.EventHandler(this.buttonCar_Click);
            // 
            // PictureBoxBudget
            // 
            this.PictureBoxBudget.Image = ((System.Drawing.Image)(resources.GetObject("PictureBoxBudget.Image")));
            this.PictureBoxBudget.Location = new System.Drawing.Point(671, 12);
            this.PictureBoxBudget.Name = "PictureBoxBudget";
            this.PictureBoxBudget.Size = new System.Drawing.Size(246, 253);
            this.PictureBoxBudget.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxBudget.TabIndex = 0;
            this.PictureBoxBudget.TabStop = false;
            // 
            // FormCreateBudget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 396);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonFamily);
            this.Controls.Add(this.buttonHome);
            this.Controls.Add(this.buttonInvest);
            this.Controls.Add(this.buttonCar);
            this.Controls.Add(this.PictureBoxBudget);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormCreateBudget";
            this.Text = "FormCreateBudget";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxBudget)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBoxBudget;
        private System.Windows.Forms.Button buttonCar;
        private System.Windows.Forms.Button buttonInvest;
        private System.Windows.Forms.Button buttonHome;
        private System.Windows.Forms.Button buttonFamily;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxNote;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSave;
    }
}