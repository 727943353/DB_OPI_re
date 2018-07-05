namespace DB_OPI.Forms
{
    partial class CstLogonForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLoadingCassette = new System.Windows.Forms.TextBox();
            this.txtUnloadingCassette = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.closeBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.userNoTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pwdTxt = new System.Windows.Forms.MaskedTextBox();
            this.glueCtrlStateLab = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loading Cassette :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Unloading Cassette :";
            // 
            // txtLoadingCassette
            // 
            this.txtLoadingCassette.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLoadingCassette.Location = new System.Drawing.Point(108, 44);
            this.txtLoadingCassette.Name = "txtLoadingCassette";
            this.txtLoadingCassette.Size = new System.Drawing.Size(140, 22);
            this.txtLoadingCassette.TabIndex = 1;
            this.txtLoadingCassette.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLoadingCassette_KeyPress);
            // 
            // txtUnloadingCassette
            // 
            this.txtUnloadingCassette.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUnloadingCassette.Location = new System.Drawing.Point(108, 90);
            this.txtUnloadingCassette.Name = "txtUnloadingCassette";
            this.txtUnloadingCassette.Size = new System.Drawing.Size(140, 22);
            this.txtUnloadingCassette.TabIndex = 2;
            this.txtUnloadingCassette.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUnloadingCassette_KeyPress);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(174, 225);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(115, 40);
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.Text = "Comfrim";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMessage);
            this.groupBox1.Location = new System.Drawing.Point(328, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Message";
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(6, 18);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(187, 79);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "label3";
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(328, 225);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(115, 40);
            this.closeBtn.TabIndex = 2;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "User No :";
            // 
            // userNoTxt
            // 
            this.userNoTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.userNoTxt.Location = new System.Drawing.Point(108, 131);
            this.userNoTxt.Name = "userNoTxt";
            this.userNoTxt.Size = new System.Drawing.Size(140, 22);
            this.userNoTxt.TabIndex = 3;
            this.userNoTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.userNoTxt_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "Password :";
            // 
            // pwdTxt
            // 
            this.pwdTxt.Location = new System.Drawing.Point(108, 167);
            this.pwdTxt.Name = "pwdTxt";
            this.pwdTxt.PasswordChar = '*';
            this.pwdTxt.Size = new System.Drawing.Size(140, 22);
            this.pwdTxt.TabIndex = 4;
            this.pwdTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pwdTxt_KeyPress);
            // 
            // glueCtrlStateLab
            // 
            this.glueCtrlStateLab.AutoSize = true;
            this.glueCtrlStateLab.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.glueCtrlStateLab.Location = new System.Drawing.Point(12, 9);
            this.glueCtrlStateLab.Name = "glueCtrlStateLab";
            this.glueCtrlStateLab.Size = new System.Drawing.Size(137, 16);
            this.glueCtrlStateLab.TabIndex = 6;
            this.glueCtrlStateLab.Text = "Glue Ctrl Enabled";
            // 
            // CstLogonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 286);
            this.Controls.Add(this.glueCtrlStateLab);
            this.Controls.Add(this.pwdTxt);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtUnloadingCassette);
            this.Controls.Add(this.userNoTxt);
            this.Controls.Add(this.txtLoadingCassette);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CstLogonForm";
            this.Text = "CstLogonForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.CstLogonForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLoadingCassette;
        private System.Windows.Forms.TextBox txtUnloadingCassette;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox userNoTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox pwdTxt;
        private System.Windows.Forms.Label glueCtrlStateLab;
    }
}