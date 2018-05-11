namespace DB_OPI.Forms
{
    partial class EquipmentStateChangeDescriptionForm
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
            this.reasonSubTypeGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reasonListGrid = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.descBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.ipqcLotLab = new System.Windows.Forms.Label();
            this.printerLab = new System.Windows.Forms.Label();
            this.ipqcLotTxt = new System.Windows.Forms.TextBox();
            this.printerTxt = new System.Windows.Forms.TextBox();
            this.ipqcLotBtn = new System.Windows.Forms.Button();
            this.prtBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.reasonSubTypeGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reasonListGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // reasonSubTypeGrid
            // 
            this.reasonSubTypeGrid.AllowUserToAddRows = false;
            this.reasonSubTypeGrid.AllowUserToDeleteRows = false;
            this.reasonSubTypeGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.reasonSubTypeGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.reasonSubTypeGrid.Location = new System.Drawing.Point(12, 12);
            this.reasonSubTypeGrid.Name = "reasonSubTypeGrid";
            this.reasonSubTypeGrid.ReadOnly = true;
            this.reasonSubTypeGrid.RowTemplate.Height = 24;
            this.reasonSubTypeGrid.Size = new System.Drawing.Size(300, 150);
            this.reasonSubTypeGrid.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "REASONSUBTYPE";
            this.Column1.HeaderText = "原因子類別";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "REASONSUBTYPENAME";
            this.Column2.HeaderText = "原因子類別名稱";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 150;
            // 
            // reasonListGrid
            // 
            this.reasonListGrid.AllowUserToAddRows = false;
            this.reasonListGrid.AllowUserToDeleteRows = false;
            this.reasonListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.reasonListGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.reasonListGrid.Location = new System.Drawing.Point(318, 12);
            this.reasonListGrid.Name = "reasonListGrid";
            this.reasonListGrid.RowTemplate.Height = 24;
            this.reasonListGrid.Size = new System.Drawing.Size(623, 226);
            this.reasonListGrid.TabIndex = 1;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "CHECKFLAG";
            this.Column3.FalseValue = "false";
            this.Column3.HeaderText = "選取";
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column3.TrueValue = "true";
            this.Column3.Width = 70;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "REASONNO";
            this.Column4.HeaderText = "原因編號";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "REASONNAME";
            this.Column5.HeaderText = "原因名稱";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "REASONLEVEL";
            this.Column6.HeaderText = "原因等級";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "DESCRIPTION";
            this.Column7.HeaderText = "說明";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 300;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(12, 255);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(613, 80);
            this.txtDescription.TabIndex = 2;
            // 
            // descBtn
            // 
            this.descBtn.Location = new System.Drawing.Point(631, 271);
            this.descBtn.Name = "descBtn";
            this.descBtn.Size = new System.Drawing.Size(80, 30);
            this.descBtn.TabIndex = 3;
            this.descBtn.Text = "取得描述";
            this.descBtn.UseVisualStyleBackColor = true;
            this.descBtn.Click += new System.EventHandler(this.descBtn_Click);
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(631, 346);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(80, 30);
            this.okBtn.TabIndex = 3;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(729, 346);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(80, 30);
            this.closeBtn.TabIndex = 3;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // ipqcLotLab
            // 
            this.ipqcLotLab.AutoSize = true;
            this.ipqcLotLab.Location = new System.Drawing.Point(12, 188);
            this.ipqcLotLab.Name = "ipqcLotLab";
            this.ipqcLotLab.Size = new System.Drawing.Size(73, 12);
            this.ipqcLotLab.TabIndex = 4;
            this.ipqcLotLab.Text = "IPQC Lot No :";
            this.ipqcLotLab.Visible = false;
            // 
            // printerLab
            // 
            this.printerLab.AutoSize = true;
            this.printerLab.Location = new System.Drawing.Point(43, 221);
            this.printerLab.Name = "printerLab";
            this.printerLab.Size = new System.Drawing.Size(42, 12);
            this.printerLab.TabIndex = 4;
            this.printerLab.Text = "Printer :";
            this.printerLab.Visible = false;
            // 
            // ipqcLotTxt
            // 
            this.ipqcLotTxt.Location = new System.Drawing.Point(91, 185);
            this.ipqcLotTxt.Name = "ipqcLotTxt";
            this.ipqcLotTxt.ReadOnly = true;
            this.ipqcLotTxt.Size = new System.Drawing.Size(150, 22);
            this.ipqcLotTxt.TabIndex = 5;
            this.ipqcLotTxt.Visible = false;
            // 
            // printerTxt
            // 
            this.printerTxt.Location = new System.Drawing.Point(91, 218);
            this.printerTxt.Name = "printerTxt";
            this.printerTxt.ReadOnly = true;
            this.printerTxt.Size = new System.Drawing.Size(150, 22);
            this.printerTxt.TabIndex = 5;
            this.printerTxt.Visible = false;
            // 
            // ipqcLotBtn
            // 
            this.ipqcLotBtn.Location = new System.Drawing.Point(247, 185);
            this.ipqcLotBtn.Name = "ipqcLotBtn";
            this.ipqcLotBtn.Size = new System.Drawing.Size(31, 23);
            this.ipqcLotBtn.TabIndex = 6;
            this.ipqcLotBtn.Text = "...";
            this.ipqcLotBtn.UseVisualStyleBackColor = true;
            this.ipqcLotBtn.Visible = false;
            this.ipqcLotBtn.Click += new System.EventHandler(this.ipqcLotBtn_Click);
            // 
            // prtBtn
            // 
            this.prtBtn.Location = new System.Drawing.Point(247, 218);
            this.prtBtn.Name = "prtBtn";
            this.prtBtn.Size = new System.Drawing.Size(31, 23);
            this.prtBtn.TabIndex = 6;
            this.prtBtn.Text = "...";
            this.prtBtn.UseVisualStyleBackColor = true;
            this.prtBtn.Visible = false;
            this.prtBtn.Click += new System.EventHandler(this.prtBtn_Click);
            // 
            // EquipmentStateChangeDescriptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 386);
            this.Controls.Add(this.prtBtn);
            this.Controls.Add(this.ipqcLotBtn);
            this.Controls.Add(this.printerTxt);
            this.Controls.Add(this.ipqcLotTxt);
            this.Controls.Add(this.printerLab);
            this.Controls.Add(this.ipqcLotLab);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.descBtn);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.reasonListGrid);
            this.Controls.Add(this.reasonSubTypeGrid);
            this.Name = "EquipmentStateChangeDescriptionForm";
            this.Text = "EquipmentStateChangeDescriptionForm";
            this.Load += new System.EventHandler(this.EquipmentStateChangeDescriptionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.reasonSubTypeGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reasonListGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView reasonSubTypeGrid;
        private System.Windows.Forms.DataGridView reasonListGrid;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button descBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Label ipqcLotLab;
        private System.Windows.Forms.Label printerLab;
        private System.Windows.Forms.TextBox ipqcLotTxt;
        private System.Windows.Forms.TextBox printerTxt;
        private System.Windows.Forms.Button ipqcLotBtn;
        private System.Windows.Forms.Button prtBtn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    }
}