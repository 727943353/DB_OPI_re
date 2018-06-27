namespace DB_OPI.Forms
{
    partial class EquipmentStateChangeForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.eqpStateLab = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.eqpNoLab = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.stateBtnPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.closeBtn = new System.Windows.Forms.Button();
            this.pwdTxt = new System.Windows.Forms.MaskedTextBox();
            this.userNoTxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.eqpStateLab);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 350);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // eqpStateLab
            // 
            this.eqpStateLab.AutoSize = true;
            this.eqpStateLab.Location = new System.Drawing.Point(71, 215);
            this.eqpStateLab.Name = "eqpStateLab";
            this.eqpStateLab.Size = new System.Drawing.Size(33, 12);
            this.eqpStateLab.TabIndex = 2;
            this.eqpStateLab.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "設備狀態 :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.eqpNoLab);
            this.panel1.Location = new System.Drawing.Point(5, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(128, 170);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(7, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(112, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // eqpNoLab
            // 
            this.eqpNoLab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.eqpNoLab.Location = new System.Drawing.Point(7, 10);
            this.eqpNoLab.Name = "eqpNoLab";
            this.eqpNoLab.Size = new System.Drawing.Size(112, 24);
            this.eqpNoLab.TabIndex = 0;
            this.eqpNoLab.Text = "label2";
            this.eqpNoLab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.stateBtnPanel);
            this.groupBox2.Location = new System.Drawing.Point(218, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(273, 350);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "變更設備狀態";
            // 
            // stateBtnPanel
            // 
            this.stateBtnPanel.Location = new System.Drawing.Point(6, 21);
            this.stateBtnPanel.Name = "stateBtnPanel";
            this.stateBtnPanel.Size = new System.Drawing.Size(261, 323);
            this.stateBtnPanel.TabIndex = 1;
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(410, 413);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 34);
            this.closeBtn.TabIndex = 1;
            this.closeBtn.Text = "關閉";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // pwdTxt
            // 
            this.pwdTxt.Location = new System.Drawing.Point(322, 12);
            this.pwdTxt.Name = "pwdTxt";
            this.pwdTxt.PasswordChar = '*';
            this.pwdTxt.Size = new System.Drawing.Size(140, 22);
            this.pwdTxt.TabIndex = 14;
            // 
            // userNoTxt
            // 
            this.userNoTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.userNoTxt.Location = new System.Drawing.Point(72, 12);
            this.userNoTxt.Name = "userNoTxt";
            this.userNoTxt.Size = new System.Drawing.Size(140, 22);
            this.userNoTxt.TabIndex = 13;
            this.userNoTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.userNoTxt_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(262, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "Password :";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "User No :";
            // 
            // EquipmentStateChangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 456);
            this.Controls.Add(this.pwdTxt);
            this.Controls.Add(this.userNoTxt);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "EquipmentStateChangeForm";
            this.Text = "EquipmentStateChangeForm";
            this.Load += new System.EventHandler(this.EquipmentStateChangeForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label eqpStateLab;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label eqpNoLab;
        private System.Windows.Forms.FlowLayoutPanel stateBtnPanel;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.MaskedTextBox pwdTxt;
        private System.Windows.Forms.TextBox userNoTxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}