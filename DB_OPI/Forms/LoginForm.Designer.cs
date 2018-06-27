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
            this.loginBtn.Location = new System.Drawing.Point(32, 163);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(75, 23);
            this.loginBtn.TabIndex = 3;
            this.loginBtn.Text = "Login";
            this.loginBtn.UseVisualStyleBackColor = true;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(142, 163);
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
            this.reheModeChk.Location = new System.Drawing.Point(92, 128);
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
            this.glueCtrlChk.Location = new System.Drawing.Point(92, 94);
            this.glueCtrlChk.Name = "glueCtrlChk";
            this.glueCtrlChk.Size = new System.Drawing.Size(72, 16);
            this.glueCtrlChk.TabIndex = 5;
            this.glueCtrlChk.Text = "膠材卡控";
            this.glueCtrlChk.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 211);
            this.ControlBox = false;
            this.Controls.Add(this.glueCtrlChk);
            this.Controls.Add(this.reheModeChk);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.loginBtn);
            this.Controls.Add(this.pwdTxt);
            this.Controls.Add(this.userTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DB OPI LoginForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.LoginForm_Load);
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
    }
}