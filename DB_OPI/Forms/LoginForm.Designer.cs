namespace DB_OPI.Forms
{
    partial class LoginForm
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
            this.userTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pwdTxt = new System.Windows.Forms.MaskedTextBox();
            this.loginBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.reheModeChk = new System.Windows.Forms.CheckBox();
            this.glueCtrlChk = new System.Windows.Forms.CheckBox();
            this.autoModeChk = new System.Windows.Forms.CheckBox();
            this.autoGrp = new System.Windows.Forms.GroupBox();
            this.logoffPortCmb = new System.Windows.Forms.ComboBox();
            this.logonPortCmb = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.eqpNoTxt = new System.Windows.Forms.TextBox();
            this.comTestBtn = new System.Windows.Forms.Button();
            this.autoGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(18, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "User No :";
            // 
            // userTxt
            // 
            this.userTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.userTxt.Location = new System.Drawing.Point(92, 20);
            this.userTxt.Name = "userTxt";
            this.userTxt.Size = new System.Drawing.Size(125, 22);
            this.userTxt.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Password :";
            // 
            // pwdTxt
            // 
            this.pwdTxt.Location = new System.Drawing.Point(92, 57);
            this.pwdTxt.Name = "pwdTxt";
            this.pwdTxt.PasswordChar = '*';
            this.pwdTxt.Size = new System.Drawing.Size(125, 22);
            this.pwdTxt.TabIndex = 2;
            this.pwdTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pwdTxt_KeyPress);
            // 
            // loginBtn
            // 
            this.loginBtn.Location = new System.Drawing.Point(45, 417);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(75, 23);
            this.loginBtn.TabIndex = 3;
            this.loginBtn.Text = "Login";
            this.loginBtn.UseVisualStyleBackColor = true;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(155, 417);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(75, 23);
            this.exitBtn.TabIndex = 3;
            this.exitBtn.Text = "Exit";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // reheModeChk
            // 
            this.reheModeChk.AutoSize = true;
            this.reheModeChk.Location = new System.Drawing.Point(127, 158);
            this.reheModeChk.Name = "reheModeChk";
            this.reheModeChk.Size = new System.Drawing.Size(75, 16);
            this.reheModeChk.TabIndex = 4;
            this.reheModeChk.Text = " 回溫模式";
            this.reheModeChk.UseVisualStyleBackColor = true;
            this.reheModeChk.CheckedChanged += new System.EventHandler(this.reheModeChk_CheckedChanged);
            // 
            // glueCtrlChk
            // 
            this.glueCtrlChk.AutoSize = true;
            this.glueCtrlChk.Location = new System.Drawing.Point(21, 158);
            this.glueCtrlChk.Name = "glueCtrlChk";
            this.glueCtrlChk.Size = new System.Drawing.Size(72, 16);
            this.glueCtrlChk.TabIndex = 5;
            this.glueCtrlChk.Text = "膠材卡控";
            this.glueCtrlChk.UseVisualStyleBackColor = true;
            // 
            // autoModeChk
            // 
            this.autoModeChk.AutoSize = true;
            this.autoModeChk.Location = new System.Drawing.Point(21, 199);
            this.autoModeChk.Name = "autoModeChk";
            this.autoModeChk.Size = new System.Drawing.Size(72, 16);
            this.autoModeChk.TabIndex = 6;
            this.autoModeChk.Text = "自動過帳";
            this.autoModeChk.UseVisualStyleBackColor = true;
            this.autoModeChk.CheckedChanged += new System.EventHandler(this.autoModeChk_CheckedChanged);
            // 
            // autoGrp
            // 
            this.autoGrp.Controls.Add(this.comTestBtn);
            this.autoGrp.Controls.Add(this.logoffPortCmb);
            this.autoGrp.Controls.Add(this.logonPortCmb);
            this.autoGrp.Controls.Add(this.label4);
            this.autoGrp.Controls.Add(this.label3);
            this.autoGrp.Enabled = false;
            this.autoGrp.Location = new System.Drawing.Point(21, 229);
            this.autoGrp.Name = "autoGrp";
            this.autoGrp.Size = new System.Drawing.Size(256, 168);
            this.autoGrp.TabIndex = 7;
            this.autoGrp.TabStop = false;
            this.autoGrp.Text = "自動過帳 com port 設定";
            // 
            // logoffPortCmb
            // 
            this.logoffPortCmb.FormattingEnabled = true;
            this.logoffPortCmb.Location = new System.Drawing.Point(101, 84);
            this.logoffPortCmb.Name = "logoffPortCmb";
            this.logoffPortCmb.Size = new System.Drawing.Size(90, 20);
            this.logoffPortCmb.TabIndex = 1;
            // 
            // logonPortCmb
            // 
            this.logonPortCmb.FormattingEnabled = true;
            this.logonPortCmb.Location = new System.Drawing.Point(101, 31);
            this.logonPortCmb.Name = "logonPortCmb";
            this.logonPortCmb.Size = new System.Drawing.Size(90, 20);
            this.logonPortCmb.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "Logoff Com Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Logon Com Port:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(22, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Eqp No :";
            // 
            // eqpNoTxt
            // 
            this.eqpNoTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.eqpNoTxt.Location = new System.Drawing.Point(92, 117);
            this.eqpNoTxt.Name = "eqpNoTxt";
            this.eqpNoTxt.Size = new System.Drawing.Size(125, 22);
            this.eqpNoTxt.TabIndex = 8;
            // 
            // comTestBtn
            // 
            this.comTestBtn.Location = new System.Drawing.Point(10, 129);
            this.comTestBtn.Name = "comTestBtn";
            this.comTestBtn.Size = new System.Drawing.Size(89, 23);
            this.comTestBtn.TabIndex = 2;
            this.comTestBtn.Text = "ComPort Test";
            this.comTestBtn.UseVisualStyleBackColor = true;
            this.comTestBtn.Click += new System.EventHandler(this.comTestBtn_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 453);
            this.ControlBox = false;
            this.Controls.Add(this.eqpNoTxt);
            this.Controls.Add(this.autoGrp);
            this.Controls.Add(this.autoModeChk);
            this.Controls.Add(this.glueCtrlChk);
            this.Controls.Add(this.reheModeChk);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.loginBtn);
            this.Controls.Add(this.pwdTxt);
            this.Controls.Add(this.userTxt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DB OPI LoginForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.autoGrp.ResumeLayout(false);
            this.autoGrp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox userTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox pwdTxt;
        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.CheckBox reheModeChk;
        private System.Windows.Forms.CheckBox glueCtrlChk;
        private System.Windows.Forms.CheckBox autoModeChk;
        private System.Windows.Forms.GroupBox autoGrp;
        private System.Windows.Forms.ComboBox logoffPortCmb;
        private System.Windows.Forms.ComboBox logonPortCmb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox eqpNoTxt;
        private System.Windows.Forms.Button comTestBtn;
    }
}