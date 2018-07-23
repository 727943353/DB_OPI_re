using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DB_OPI.Forms
{
    public partial class BlueTapeIDInputForm : Form
    {
        public string btTapeID = "";
        public BlueTapeIDInputForm()
        {
            InitializeComponent();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            btTapeID = btIDTxt.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btIDTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13))
                okBtn_Click(sender, e);

        }

        private void BlueTapeIDInputForm_Load(object sender, EventArgs e)
        {
            this.Text += " ___ Ver : " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
#if DEBUG
            this.TopMost = false;
#endif
        }
    }
}
