namespace PingPongCsharp
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.ClientBoxPicture = new System.Windows.Forms.PictureBox();
            this.BackMenu = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClientBoxPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 131);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serveur";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(218, 106);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(300, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ClientBoxPicture);
            this.groupBox3.Location = new System.Drawing.Point(256, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(244, 131);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Client";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pictureBox2);
            this.groupBox4.Location = new System.Drawing.Point(122, 164);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(253, 151);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Commande pour jouer";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(6, 19);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(241, 126);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // ClientBoxPicture
            // 
            this.ClientBoxPicture.Image = ((System.Drawing.Image)(resources.GetObject("ClientBoxPicture.Image")));
            this.ClientBoxPicture.Location = new System.Drawing.Point(6, 19);
            this.ClientBoxPicture.Name = "ClientBoxPicture";
            this.ClientBoxPicture.Size = new System.Drawing.Size(232, 106);
            this.ClientBoxPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ClientBoxPicture.TabIndex = 1;
            this.ClientBoxPicture.TabStop = false;
            // 
            // BackMenu
            // 
            this.BackMenu.Location = new System.Drawing.Point(419, 286);
            this.BackMenu.Name = "BackMenu";
            this.BackMenu.Size = new System.Drawing.Size(75, 23);
            this.BackMenu.TabIndex = 3;
            this.BackMenu.Text = "Menu";
            this.BackMenu.UseVisualStyleBackColor = true;
            this.BackMenu.Click += new System.EventHandler(this.BackMenu_Click);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 346);
            this.Controls.Add(this.BackMenu);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "About";
            this.Text = "About";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClientBoxPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox ClientBoxPicture;
        private System.Windows.Forms.Button BackMenu;
    }
}