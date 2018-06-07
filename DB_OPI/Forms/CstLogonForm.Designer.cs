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
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loading Cassette :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Unloading Cassette :";
            // 
            // txtLoadingCassette
            // 
            this.txtLoadingCassette.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLoadingCassette.Location = new System.Drawing.Point(149, 46);
            this.txtLoadingCassette.Name = "txtLoadingCassette";
            this.txtLoadingCassette.Size = new System.Drawing.Size(140, 22);
            this.txtLoadingCassette.TabIndex = 1;
            this.txtLoadingCassette.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLoadingCassette_KeyPress);
            // 
            // txtUnloadingCassette
            // 
            this.txtUnloadingCassette.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUnloadingCassette.Location = new System.Drawing.Point(149, 92);
            this.txtUnloadingCassette.Name = "txtUnloadingCassette";
            this.txtUnloadingCassette.Size = new System.Drawing.Size(140, 22);
            this.txtUnloadingCassette.TabIndex = 2;
            this.txtUnloadingCassette.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUnloadingCassette_KeyPress);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(174, 165);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(115, 40);
            this.btnConfirm.TabIndex = 2;
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
            // CstLogonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 244);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtUnloadingCassette);
            this.Controls.Add(this.txtLoadingCassette);
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
    }
}