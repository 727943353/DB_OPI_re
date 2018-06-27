namespace DB_OPI.Forms
{
    partial class CstLogoffForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUnloadingCassette = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLotNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCurQty = new System.Windows.Forms.TextBox();
            this.txtEquipmentNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnKeyboard = new System.Windows.Forms.Button();
            this.iugError = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errQtyCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLotRecord = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.pwdTxt = new System.Windows.Forms.MaskedTextBox();
            this.userNoTxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.closeBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iugError)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Unloading Cassette :";
            // 
            // txtUnloadingCassette
            // 
            this.txtUnloadingCassette.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUnloadingCassette.Location = new System.Drawing.Point(127, 24);
            this.txtUnloadingCassette.Name = "txtUnloadingCassette";
            this.txtUnloadingCassette.Size = new System.Drawing.Size(120, 22);
            this.txtUnloadingCassette.TabIndex = 1;
            this.txtUnloadingCassette.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUnloadingCassette_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(77, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Lot No :";
            // 
            // txtLotNo
            // 
            this.txtLotNo.Location = new System.Drawing.Point(127, 58);
            this.txtLotNo.Name = "txtLotNo";
            this.txtLotNo.ReadOnly = true;
            this.txtLotNo.Size = new System.Drawing.Size(120, 22);
            this.txtLotNo.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(75, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "CurQty :";
            // 
            // txtCurQty
            // 
            this.txtCurQty.Location = new System.Drawing.Point(127, 90);
            this.txtCurQty.Name = "txtCurQty";
            this.txtCurQty.ReadOnly = true;
            this.txtCurQty.Size = new System.Drawing.Size(80, 22);
            this.txtCurQty.TabIndex = 1;
            // 
            // txtEquipmentNo
            // 
            this.txtEquipmentNo.Location = new System.Drawing.Point(127, 123);
            this.txtEquipmentNo.Name = "txtEquipmentNo";
            this.txtEquipmentNo.ReadOnly = true;
            this.txtEquipmentNo.Size = new System.Drawing.Size(120, 22);
            this.txtEquipmentNo.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "EquipmentNo :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMessage);
            this.groupBox1.Location = new System.Drawing.Point(377, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(194, 121);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Message";
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(6, 18);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(182, 96);
            this.lblMessage.TabIndex = 0;
            // 
            // btnKeyboard
            // 
            this.btnKeyboard.Location = new System.Drawing.Point(276, 121);
            this.btnKeyboard.Name = "btnKeyboard";
            this.btnKeyboard.Size = new System.Drawing.Size(75, 23);
            this.btnKeyboard.TabIndex = 3;
            this.btnKeyboard.Text = "KeyBoard";
            this.btnKeyboard.UseVisualStyleBackColor = true;
            this.btnKeyboard.Click += new System.EventHandler(this.btnKeyboard_Click);
            // 
            // iugError
            // 
            this.iugError.AllowUserToAddRows = false;
            this.iugError.AllowUserToDeleteRows = false;
            this.iugError.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.iugError.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column5,
            this.Column4,
            this.Column3,
            this.errQtyCol});
            this.iugError.Location = new System.Drawing.Point(23, 231);
            this.iugError.MultiSelect = false;
            this.iugError.Name = "iugError";
            this.iugError.RowTemplate.Height = 24;
            this.iugError.Size = new System.Drawing.Size(548, 183);
            this.iugError.TabIndex = 4;
            this.iugError.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.iugError_CellEndEdit);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "CheckFlag";
            this.Column1.FalseValue = "false";
            this.Column1.HeaderText = "CheckFlag";
            this.Column1.IndeterminateValue = "false";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.TrueValue = "true";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "ReasonType";
            this.Column5.HeaderText = "ReasonType";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "ErrorNo";
            this.Column4.HeaderText = "ErrorNo";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "ReasonName";
            this.Column3.HeaderText = "ReasonName";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // errQtyCol
            // 
            this.errQtyCol.DataPropertyName = "ErrorQty";
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = "0";
            this.errQtyCol.DefaultCellStyle = dataGridViewCellStyle1;
            this.errQtyCol.HeaderText = "ErrorQty";
            this.errQtyCol.Name = "errQtyCol";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 434);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "Lot Record :";
            // 
            // txtLotRecord
            // 
            this.txtLotRecord.Location = new System.Drawing.Point(91, 431);
            this.txtLotRecord.Multiline = true;
            this.txtLotRecord.Name = "txtLotRecord";
            this.txtLotRecord.Size = new System.Drawing.Size(300, 55);
            this.txtLotRecord.TabIndex = 5;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(447, 434);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(95, 52);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "Comfirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // pwdTxt
            // 
            this.pwdTxt.Location = new System.Drawing.Point(127, 199);
            this.pwdTxt.Name = "pwdTxt";
            this.pwdTxt.PasswordChar = '*';
            this.pwdTxt.Size = new System.Drawing.Size(140, 22);
            this.pwdTxt.TabIndex = 10;
            this.pwdTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pwdTxt_KeyPress);
            // 
            // userNoTxt
            // 
            this.userNoTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.userNoTxt.Location = new System.Drawing.Point(127, 163);
            this.userNoTxt.Name = "userNoTxt";
            this.userNoTxt.Size = new System.Drawing.Size(140, 22);
            this.userNoTxt.TabIndex = 9;
            this.userNoTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.userNoTxt_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(67, 202);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "Password :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(72, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "User No :";
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(608, 434);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(95, 52);
            this.closeBtn.TabIndex = 6;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // CstLogoffForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 521);
            this.Controls.Add(this.pwdTxt);
            this.Controls.Add(this.userNoTxt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtLotRecord);
            this.Controls.Add(this.iugError);
            this.Controls.Add(this.btnKeyboard);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtEquipmentNo);
            this.Controls.Add(this.txtCurQty);
            this.Controls.Add(this.txtLotNo);
            this.Controls.Add(this.txtUnloadingCassette);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CstLogoffForm";
            this.Text = "CstLogoffForm";
            this.Load += new System.EventHandler(this.CstLogoffForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iugError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUnloadingCassette;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLotNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCurQty;
        private System.Windows.Forms.TextBox txtEquipmentNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnKeyboard;
        private System.Windows.Forms.DataGridView iugError;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLotRecord;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn errQtyCol;
        private System.Windows.Forms.MaskedTextBox pwdTxt;
        private System.Windows.Forms.TextBox userNoTxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button closeBtn;
    }
}