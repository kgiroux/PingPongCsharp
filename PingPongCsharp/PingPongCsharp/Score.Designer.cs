namespace PingPongCsharp
{
    partial class Score
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
            this.ScoreDataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.ScoreDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ScoreDataGrid
            // 
            this.ScoreDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ScoreDataGrid.Location = new System.Drawing.Point(12, 12);
            this.ScoreDataGrid.Name = "ScoreDataGrid";
            this.ScoreDataGrid.Size = new System.Drawing.Size(598, 295);
            this.ScoreDataGrid.TabIndex = 0;
            // 
            // Score
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 319);
            this.Controls.Add(this.ScoreDataGrid);
            this.Name = "Score";
            this.Text = "Score";
            ((System.ComponentModel.ISupportInitialize)(this.ScoreDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ScoreDataGrid;

    }
}