namespace PingPongCsharp
{
    partial class Partie
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Partie));
            this.raquette = new System.Windows.Forms.PictureBox();
            this.ball = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.raquette)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ball)).BeginInit();
            this.SuspendLayout();
            // 
            // raquette
            // 
            this.raquette.AccessibleName = "Raquette";
            this.raquette.BackColor = System.Drawing.SystemColors.InfoText;
            this.raquette.Image = ((System.Drawing.Image)(resources.GetObject("raquette.Image")));
            this.raquette.Location = new System.Drawing.Point(34, 143);
            this.raquette.Margin = new System.Windows.Forms.Padding(2);
            this.raquette.Name = "raquette";
            this.raquette.Size = new System.Drawing.Size(16, 127);
            this.raquette.TabIndex = 0;
            this.raquette.TabStop = false;
            // 
            // ball
            // 
            this.ball.AccessibleName = "Ball";
            this.ball.BackColor = System.Drawing.Color.Transparent;
            this.ball.Image = ((System.Drawing.Image)(resources.GetObject("ball.Image")));
            this.ball.Location = new System.Drawing.Point(404, 198);
            this.ball.Margin = new System.Windows.Forms.Padding(2);
            this.ball.Name = "ball";
            this.ball.Size = new System.Drawing.Size(18, 20);
            this.ball.TabIndex = 2;
            this.ball.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Partie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(844, 411);
            this.Controls.Add(this.ball);
            this.Controls.Add(this.raquette);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Partie";
            this.Text = "Partie";
            ((System.ComponentModel.ISupportInitialize)(this.raquette)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ball)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox raquette;
        private System.Windows.Forms.PictureBox ball;
        private System.Windows.Forms.Timer timer1;
    }
}