namespace DB_OPI.Forms
{
    partial class BlueTapeLogonForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.userNoTxt = new System.Windows.Forms.TextBox();
            this.eqpTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.blueTapeCntTxt = new System.Windows.Forms.TextBox();
            this.msgGrid = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blueTapeGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.frameIDTxt = new System.Windows.Forms.TextBox();
            this.matLotNoTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.clsBtn = new System.Windows.Forms.Button();
            this.comfBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.msgGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueTapeGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // userNoTxt
            // 
            this.userNoTxt.Location = new System.Drawing.Point(327, 6);
            this.userNoTxt.Name = "userNoTxt";
            this.userNoTxt.Size = new System.Drawing.Size(100, 22);
            this.userNoTxt.TabIndex = 6;
            // 
            // eqpTxt
            // 
            this.eqpTxt.Location = new System.Drawing.Point(97, 6);
            this.eqpTxt.Name = "eqpTxt";
            this.eqpTxt.ReadOnly = true;
            this.eqpTxt.Size = new System.Drawing.Size(100, 22);
            this.eqpTxt.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(272, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "User No :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Equipment No :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.blueTapeCntTxt);
            this.groupBox1.Controls.Add(this.msgGrid);
            this.groupBox1.Controls.Add(this.blueTapeGrid);
            this.groupBox1.Controls.Add(this.frameIDTxt);
            this.groupBox1.Controls.Add(this.matLotNoTxt);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(14, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(707, 438);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // blueTapeCntTxt
            // 
            this.blueTapeCntTxt.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.blueTapeCntTxt.Location = new System.Drawing.Point(8, 45);
            this.blueTapeCntTxt.Name = "blueTapeCntTxt";
            this.blueTapeCntTxt.ReadOnly = true;
            this.blueTapeCntTxt.Size = new System.Drawing.Size(69, 27);
            this.blueTapeCntTxt.TabIndex = 5;
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
            dataGridViewCellStyle1.Format = "yyyy-MM-dd HH:mm:ss";
            this.Column8.DefaultCellStyle = dataGridViewCellStyle1;
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
            // blueTapeGrid
            // 
            this.blueTapeGrid.AllowUserToAddRows = false;
            this.blueTapeGrid.AllowUserToDeleteRows = false;
            this.blueTapeGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.blueTapeGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column10,
            this.Column11});
            this.blueTapeGrid.Location = new System.Drawing.Point(8, 73);
            this.blueTapeGrid.MultiSelect = false;
            this.blueTapeGrid.Name = "blueTapeGrid";
            this.blueTapeGrid.RowTemplate.Height = 24;
            this.blueTapeGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.blueTapeGrid.Size = new System.Drawing.Size(693, 198);
            this.blueTapeGrid.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "TURN_QTY";
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = "0";
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "轉移數量";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "INVENTORYNO";
            this.Column2.HeaderText = "庫房編號";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "MATERIALNO";
            this.Column3.HeaderText = "物料料號";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 150;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "MONO";
            this.Column4.HeaderText = "工單編號";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 200;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "MATERIALLOTNO";
            this.Column5.HeaderText = "物料批號";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 150;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "FRAME_ID";
            this.Column6.HeaderText = "FRAME_ID";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 150;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "UNITNO";
            this.Column7.HeaderText = "單位編號";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "QTY";
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = "0";
            this.Column10.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column10.HeaderText = "數量";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "MATERIALTYPE";
            this.Column11.HeaderText = "物料類別";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // frameIDTxt
            // 
            this.frameIDTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.frameIDTxt.Location = new System.Drawing.Point(372, 15);
            this.frameIDTxt.Name = "frameIDTxt";
            this.frameIDTxt.Size = new System.Drawing.Size(150, 22);
            this.frameIDTxt.TabIndex = 1;
            this.frameIDTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frameIDTxt_KeyPress);
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(311, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "Frame ID :";
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
            // clsBtn
            // 
            this.clsBtn.Location = new System.Drawing.Point(636, 481);
            this.clsBtn.Name = "clsBtn";
            this.clsBtn.Size = new System.Drawing.Size(79, 30);
            this.clsBtn.TabIndex = 8;
            this.clsBtn.Text = "Close";
            this.clsBtn.UseVisualStyleBackColor = true;
            this.clsBtn.Click += new System.EventHandler(this.clsBtn_Click);
            // 
            // comfBtn
            // 
            this.comfBtn.Location = new System.Drawing.Point(539, 482);
            this.comfBtn.Name = "comfBtn";
            this.comfBtn.Size = new System.Drawing.Size(79, 30);
            this.comfBtn.TabIndex = 9;
            this.comfBtn.Text = "Comfirm";
            this.comfBtn.UseVisualStyleBackColor = true;
            this.comfBtn.Click += new System.EventHandler(this.comfBtn_Click);
            // 
            // BlueTapeLogonForm
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
            this.Name = "BlueTapeLogonForm";
            this.Text = "BlueTapeLogonForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.BlueTapeLogonForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.msgGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blueTapeGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox userNoTxt;
        private System.Windows.Forms.TextBox eqpTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView msgGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridView blueTapeGrid;
        private System.Windows.Forms.TextBox matLotNoTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox frameIDTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox blueTapeCntTxt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.Button clsBtn;
        private System.Windows.Forms.Button comfBtn;
    }
}