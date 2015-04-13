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
            this.raquette = new System.Windows.Forms.PictureBox();
            this.raquette2 = new System.Windows.Forms.PictureBox();
            this.ball = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.raquette)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.raquette2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ball)).BeginInit();
            this.SuspendLayout();
            // 
            // raquette
            // 
            this.raquette.AccessibleName = "Raquette";
            this.raquette.BackColor = System.Drawing.SystemColors.InfoText;
            this.raquette.Location = new System.Drawing.Point(46, 176);
            this.raquette.Name = "raquette";
            this.raquette.Size = new System.Drawing.Size(22, 156);
            this.raquette.TabIndex = 0;
            this.raquette.TabStop = false;
            // 
            // raquette2
            // 
            this.raquette2.AccessibleName = "Raquette";
            this.raquette2.BackColor = System.Drawing.SystemColors.InfoText;
            this.raquette2.Location = new System.Drawing.Point(1018, 176);
            this.raquette2.Name = "raquette2";
            this.raquette2.Size = new System.Drawing.Size(22, 156);
            this.raquette2.TabIndex = 1;
            this.raquette2.TabStop = false;
            // 
            // ball
            // 
            this.ball.AccessibleName = "Ball";
            this.ball.BackColor = System.Drawing.SystemColors.InfoText;
            this.ball.Location = new System.Drawing.Point(539, 244);
            this.ball.Name = "ball";
            this.ball.Size = new System.Drawing.Size(24, 24);
            this.ball.TabIndex = 2;
            this.ball.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Partie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 506);
            this.Controls.Add(this.ball);
            this.Controls.Add(this.raquette2);
            this.Controls.Add(this.raquette);
            this.Name = "Partie";
            this.Text = "Partie";
            this.Load += new System.EventHandler(this.Partie_Load);
            ((System.ComponentModel.ISupportInitialize)(this.raquette)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.raquette2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ball)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox raquette;
        private System.Windows.Forms.PictureBox raquette2;
        private System.Windows.Forms.PictureBox ball;
        private System.Windows.Forms.Timer timer1;
    }
}