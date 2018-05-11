namespace DB_OPI.Forms
{
    partial class MaterialLogonForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.eqpTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.userNoTxt = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.matLogoffBtn = new System.Windows.Forms.Button();
            this.msgGrid = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matHistGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matLotNoTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comfBtn = new System.Windows.Forms.Button();
            this.clsBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.msgGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.matHistGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Equipment No :";
            // 
            // eqpTxt
            // 
            this.eqpTxt.Location = new System.Drawing.Point(97, 6);
            this.eqpTxt.Name = "eqpTxt";
            this.eqpTxt.ReadOnly = true;
            this.eqpTxt.Size = new System.Drawing.Size(100, 22);
            this.eqpTxt.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(272, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "User No :";
            // 
            // userNoTxt
            // 
            this.userNoTxt.Location = new System.Drawing.Point(327, 6);
            this.userNoTxt.Name = "userNoTxt";
            this.userNoTxt.Size = new System.Drawing.Size(100, 22);
            this.userNoTxt.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.matLogoffBtn);
            this.groupBox1.Controls.Add(this.msgGrid);
            this.groupBox1.Controls.Add(this.matHistGrid);
            this.groupBox1.Controls.Add(this.matLotNoTxt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(14, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(707, 438);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // matLogoffBtn
            // 
            this.matLogoffBtn.Location = new System.Drawing.Point(548, 15);
            this.matLogoffBtn.Name = "matLogoffBtn";
            this.matLogoffBtn.Size = new System.Drawing.Size(134, 32);
            this.matLogoffBtn.TabIndex = 4;
            this.matLogoffBtn.Text = "手動物料下機";
            this.matLogoffBtn.UseVisualStyleBackColor = true;
            this.matLogoffBtn.Click += new System.EventHandler(this.matLogoffBtn_Click);
            // 
            // msgGrid
            // 
            this.msgGrid.AllowUserToAddRows = false;
            this.msgGrid.AllowUserToDeleteRows = false;
            this.msgGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.msgGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column9});
            this.msgGrid.Location = new System.Drawing.Point(8, 282);
            this.msgGrid.Name = "msgGrid";
            this.msgGrid.ReadOnly = true;
            this.msgGrid.RowTemplate.Height = 24;
            this.msgGrid.Size = new System.Drawing.Size(693, 150);
            this.msgGrid.TabIndex = 3;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "CreateTime";
            dataGridViewCellStyle13.Format = "yyyy-MM-dd HH:mm:ss";
            this.Column8.DefaultCellStyle = dataGridViewCellStyle13;
            this.Column8.HeaderText = "時間";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 150;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "Message";
            this.Column9.HeaderText = "Message";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 400;
            // 
            // matHistGrid
            // 
            this.matHistGrid.AllowUserToAddRows = false;
            this.matHistGrid.AllowUserToDeleteRows = false;
            this.matHistGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.matHistGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.matHistGrid.Location = new System.Drawing.Point(8, 53);
            this.matHistGrid.MultiSelect = false;
            this.matHistGrid.Name = "matHistGrid";
            this.matHistGrid.ReadOnly = true;
            this.matHistGrid.RowTemplate.Height = 24;
            this.matHistGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.matHistGrid.Size = new System.Drawing.Size(693, 218);
            this.matHistGrid.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "MATERIALNO";
            this.Column1.HeaderText = "物料料號";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "MATERIALLOTNO";
            this.Column2.FillWeight = 250F;
            this.Column2.HeaderText = "物料批號";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 200;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "EQUIPMENTNO";
            this.Column3.HeaderText = "設備編號";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "LOGON_USERNO";
            this.Column4.HeaderText = "上機人員";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "LOGON_TIME";
            dataGridViewCellStyle14.Format = "yyyy-MM-dd HH:mm:ss";
            this.Column5.DefaultCellStyle = dataGridViewCellStyle14;
            this.Column5.HeaderText = "上機時間";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 150;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "LOGOFF_TIME";
            dataGridViewCellStyle15.Format = "yyyy-MM-dd HH:mm:ss";
            this.Column6.DefaultCellStyle = dataGridViewCellStyle15;
            this.Column6.HeaderText = "下機時間";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 150;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "LOGOFF_USERNO";
            this.Column7.HeaderText = "下機人員";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // matLotNoTxt
            // 
            this.matLotNoTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.matLotNoTxt.Location = new System.Drawing.Point(97, 15);
            this.matLotNoTxt.Name = "matLotNoTxt";
            this.matLotNoTxt.Size = new System.Drawing.Size(200, 22);
            this.matLotNoTxt.TabIndex = 1;
            this.matLotNoTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.matLotNoTxt_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Material Lot No :";
            // 
            // comfBtn
            // 
            this.comfBtn.Location = new System.Drawing.Point(539, 473);
            this.comfBtn.Name = "comfBtn";
            this.comfBtn.Size = new System.Drawing.Size(79, 30);
            this.comfBtn.TabIndex = 4;
            this.comfBtn.Text = "Comfirm";
            this.comfBtn.UseVisualStyleBackColor = true;
            this.comfBtn.Click += new System.EventHandler(this.comfBtn_Click);
            // 
            // clsBtn
            // 
            this.clsBtn.Location = new System.Drawing.Point(636, 472);
            this.clsBtn.Name = "clsBtn";
            this.clsBtn.Size = new System.Drawing.Size(79, 30);
            this.clsBtn.TabIndex = 4;
            this.clsBtn.Text = "Close";
            this.clsBtn.UseVisualStyleBackColor = true;
            this.clsBtn.Click += new System.EventHandler(this.clsBtn_Click);
            // 
            // MaterialLogonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 512);
            this.Controls.Add(this.clsBtn);
            this.Controls.Add(this.comfBtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.userNoTxt);
            this.Controls.Add(this.eqpTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MaterialLogonForm";
            this.Text = "MaterialLogonForm";
            this.Load += new System.EventHandler(this.MaterialLogonForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.msgGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.matHistGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox eqpTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox userNoTxt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView matHistGrid;
        private System.Windows.Forms.TextBox matLotNoTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button matLogoffBtn;
        private System.Windows.Forms.DataGridView msgGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.Button comfBtn;
        private System.Windows.Forms.Button clsBtn;
    }
}