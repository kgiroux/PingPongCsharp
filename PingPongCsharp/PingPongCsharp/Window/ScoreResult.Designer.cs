namespace PingPongCsharp
{
    partial class ScoreResult
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
            this.ResultDataGrid = new System.Windows.Forms.DataGridView();
            this.BackMenu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ResultDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ResultDataGrid
            // 
            this.ResultDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultDataGrid.Location = new System.Drawing.Point(12, 12);
            this.ResultDataGrid.Name = "ResultDataGrid";
            this.ResultDataGrid.Size = new System.Drawing.Size(645, 237);
            this.ResultDataGrid.TabIndex = 0;
            // 
            // BackMenu
            // 
            this.BackMenu.Location = new System.Drawing.Point(582, 266);
            this.BackMenu.Name = "BackMenu";
            this.BackMenu.Size = new System.Drawing.Size(75, 23);
            this.BackMenu.TabIndex = 1;
            this.BackMenu.Text = "Menu";
            this.BackMenu.UseVisualStyleBackColor = true;
            this.BackMenu.Click += new System.EventHandler(this.BackMenu_Click);
            // 
            // ScoreResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 301);
            this.Controls.Add(this.BackMenu);
            this.Controls.Add(this.ResultDataGrid);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScoreResult";
            this.Text = "ScoreResult";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScoreResult_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ResultDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ResultDataGrid;
        private System.Windows.Forms.Button BackMenu;

    }
}