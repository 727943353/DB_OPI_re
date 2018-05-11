namespace DB_OPI.Components
{
    partial class StateButton
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.stateBtn = new System.Windows.Forms.Button();
            this.colorLab = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // stateBtn
            // 
            this.stateBtn.Location = new System.Drawing.Point(0, 15);
            this.stateBtn.Name = "stateBtn";
            this.stateBtn.Size = new System.Drawing.Size(70, 40);
            this.stateBtn.TabIndex = 0;
            this.stateBtn.Text = "button1";
            this.stateBtn.UseVisualStyleBackColor = true;
            this.stateBtn.Click += new System.EventHandler(this.stateBtn_Click);
            // 
            // colorLab
            // 
            this.colorLab.Location = new System.Drawing.Point(3, 3);
            this.colorLab.Name = "colorLab";
            this.colorLab.Size = new System.Drawing.Size(70, 10);
            this.colorLab.TabIndex = 1;
            this.colorLab.Text = "label1";
            // 
            // StateButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.colorLab);
            this.Controls.Add(this.stateBtn);
            this.Name = "StateButton";
            this.Size = new System.Drawing.Size(71, 56);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button stateBtn;
        private System.Windows.Forms.Label colorLab;
    }
}
