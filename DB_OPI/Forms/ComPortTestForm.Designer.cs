namespace DB_OPI.Forms
{
    partial class ComPortTestForm
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
            this.testGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.testGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // testGrid
            // 
            this.testGrid.AllowUserToAddRows = false;
            this.testGrid.AllowUserToDeleteRows = false;
            this.testGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.testGrid.Location = new System.Drawing.Point(12, 12);
            this.testGrid.Name = "testGrid";
            this.testGrid.ReadOnly = true;
            this.testGrid.RowTemplate.Height = 24;
            this.testGrid.Size = new System.Drawing.Size(681, 304);
            this.testGrid.TabIndex = 0;
            // 
            // ComPortTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 328);
            this.Controls.Add(this.testGrid);
            this.Name = "ComPortTestForm";
            this.Text = "ComPortTestForm";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ComPortTestForm_FormClosing);
            this.Load += new System.EventHandler(this.ComPortTestForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.testGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView testGrid;
    }
}