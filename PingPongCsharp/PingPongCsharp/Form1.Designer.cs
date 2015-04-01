namespace PingPongCsharp
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.serveur = new System.Windows.Forms.Button();
            this.client = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.NamePlayer = new System.Windows.Forms.Label();
            this.stop = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // serveur
            // 
            this.serveur.Location = new System.Drawing.Point(118, 254);
            this.serveur.Name = "serveur";
            this.serveur.Size = new System.Drawing.Size(97, 23);
            this.serveur.TabIndex = 0;
            this.serveur.Text = "Serveur";
            this.serveur.UseVisualStyleBackColor = true;
            this.serveur.Click += new System.EventHandler(this.button2_Click);
            // 
            // client
            // 
            this.client.Location = new System.Drawing.Point(518, 250);
            this.client.Name = "client";
            this.client.Size = new System.Drawing.Size(97, 23);
            this.client.TabIndex = 0;
            this.client.Text = "Client";
            this.client.UseVisualStyleBackColor = true;
            this.client.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(118, 36);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(214, 20);
            this.textBox1.TabIndex = 1;
            // 
            // NamePlayer
            // 
            this.NamePlayer.AutoSize = true;
            this.NamePlayer.Location = new System.Drawing.Point(57, 39);
            this.NamePlayer.Name = "NamePlayer";
            this.NamePlayer.Size = new System.Drawing.Size(44, 13);
            this.NamePlayer.TabIndex = 2;
            this.NamePlayer.Text = "Name : ";
            // 
            // stop
            // 
            this.stop.Location = new System.Drawing.Point(575, 309);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(97, 23);
            this.stop.TabIndex = 3;
            this.stop.Text = "Stop";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(38, 71);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(294, 177);
            this.textBox2.TabIndex = 4;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(475, 71);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(182, 173);
            this.listBox1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(472, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Devices Available :";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 344);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NamePlayer);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.serveur);
            this.Controls.Add(this.client);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Configuration Bluetooth";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button serveur;
        private System.Windows.Forms.Button client;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label NamePlayer;
        private System.Windows.Forms.Button stop;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
    }
}

