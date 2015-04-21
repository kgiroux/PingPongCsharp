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
            this.temps = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
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
            // temps
            // 
            this.temps.AutoSize = true;
            this.temps.BackColor = System.Drawing.Color.Transparent;
            this.temps.Font = new System.Drawing.Font("OCR A Extended", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.temps.ForeColor = System.Drawing.Color.White;
            this.temps.Location = new System.Drawing.Point(422, 92);
            this.temps.Name = "temps";
            this.temps.Size = new System.Drawing.Size(182, 83);
            this.temps.TabIndex = 3;
            this.temps.Text = "100";
            this.temps.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // score
            // 
            this.score.AutoSize = true;
            this.score.BackColor = System.Drawing.Color.Transparent;
            this.score.Font = new System.Drawing.Font("OCR A Extended", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.score.Location = new System.Drawing.Point(422, 9);
            this.score.Name = "score";
            this.score.Size = new System.Drawing.Size(182, 83);
            this.score.TabIndex = 4;
            this.score.Text = "0-0";
            // 
            // Partie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1125, 506);
            this.Controls.Add(this.ball);
            this.Controls.Add(this.score);
            this.Controls.Add(this.temps);
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
        private System.Windows.Forms.Label temps;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label score;
    }
}