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
            this.score = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.raquette)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ball)).BeginInit();
            this.SuspendLayout();
            // 
            // raquette
            // 
            this.raquette.AccessibleName = "Raquette";
            this.raquette.BackColor = System.Drawing.SystemColors.InfoText;
            this.raquette.Image = ((System.Drawing.Image)(resources.GetObject("raquette.Image")));
            this.raquette.Location = new System.Drawing.Point(45, 176);
            this.raquette.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.raquette.Name = "raquette";
            this.raquette.Size = new System.Drawing.Size(21, 156);
            this.raquette.TabIndex = 0;
            this.raquette.TabStop = false;
            // 
            // ball
            // 
            this.ball.AccessibleName = "Ball";
            this.ball.BackColor = System.Drawing.Color.Transparent;
            this.ball.Image = ((System.Drawing.Image)(resources.GetObject("ball.Image")));
            this.ball.Location = new System.Drawing.Point(539, 244);
            this.ball.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ball.Name = "ball";
            this.ball.Size = new System.Drawing.Size(24, 25);
            this.ball.TabIndex = 2;
            this.ball.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // score
            // 
            this.score.AutoSize = true;
            this.score.BackColor = System.Drawing.Color.Transparent;
            this.score.Font = new System.Drawing.Font("OCR A Extended", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.score.ForeColor = System.Drawing.Color.White;
            this.score.Location = new System.Drawing.Point(435, 9);
            this.score.Name = "score";
            this.score.Size = new System.Drawing.Size(271, 123);
            this.score.TabIndex = 3;
            this.score.Text = "100";
            this.score.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Partie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1125, 506);
            this.Controls.Add(this.score);
            this.Controls.Add(this.ball);
            this.Controls.Add(this.raquette);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Partie";
            this.Text = "Partie";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Partie_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.raquette)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ball)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox raquette;
        private System.Windows.Forms.PictureBox ball;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label score;
    }
}