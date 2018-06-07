using DB_OPI.Proxy;
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
    public partial class LoginForm : Form
    {
        public string loginUser;
        public bool reheatMode;

        public LoginForm()
        {
            
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            loginUser = userTxt.Text.Trim();
            if (MesWsAutoProxy.Login(loginUser, pwdTxt.Text.Trim()))
            {
                this.Cursor = Cursors.Default;
                this.Close();
            }
            else
            {
                this.Cursor = Cursors.Default;
                pwdTxt.SelectAll();
                MessageBox.Show("帳號或密碼錯誤 !! (UserNo or PassWord is error)", "Log In failed");
            }
        }

        private void pwdTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(13))
                return;

            loginBtn_Click(sender, e);
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void reheModeChk_CheckedChanged(object sender, EventArgs e)
        {
            reheatMode = reheModeChk.Checked;
        }
    }
}
