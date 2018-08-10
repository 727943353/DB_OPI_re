using DB_OPI.Proxy;
using DB_OPI.Util;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DB_OPI.Forms
{
    public partial class LoginForm : Form
    {
        private ILogger logger = LogManager.GetCurrentClassLogger();
        public string loginUser;
        public string pwd;
        
        //public bool reheatMode;

        //public bool ReheatMode
        //{
        //    get {
        //        return reheModeChk.Checked;
        //    }

        //}

        //public bool GlueCtrlEnabled
        //{
        //    get {
        //        return glueCtrlChk.Checked;
        //    }
        //}

        public LoginForm()
        {
            
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            if (VerifyInput() == false)
                return;

            this.Cursor = Cursors.WaitCursor;
            loginUser = userTxt.Text.Trim();
            pwd = pwdTxt.Text.Trim();
            if (MesWsAutoProxy.Login(loginUser, pwd))
            {
                AppConfigUtil.EqpNo = this.eqpNoTxt.Text.Trim();
                AppConfigUtil.AutoMode = autoModeChk.Checked;
                AppConfigUtil.ReheatMode = reheModeChk.Checked;
                AppConfigUtil.GlueCtrlMode = glueCtrlChk.Checked;
                AppConfigUtil.LogonPort = logonPortCmb.Text.Trim();
                AppConfigUtil.LogoffPort = logoffPortCmb.Text.Trim();


                AppConfigUtil.UpdateApConfig();
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

        private bool VerifyInput()
        {
            string userNo = userTxt.Text.Trim();
            string pwd = pwdTxt.Text.Trim();
            if (string.IsNullOrEmpty(userNo) || string.IsNullOrEmpty(pwd))
            {
                MessageBox.Show("帳號、密碼不可為空白。");
                return false;
            }

            if (string.IsNullOrEmpty(eqpNoTxt.Text.Trim()))
            {
                MessageBox.Show("Eqp No 不可為空白，請輸入機台編號","Warning");
                return false;
            }

            if (autoModeChk.Checked)
            {
                if (string.IsNullOrEmpty(logonPortCmb.Text))
                {
                    MessageBox.Show("已開啟自動過帳，請選擇上機的 Logon Com Port.","Warning");
                    return false;
                }

                if (string.IsNullOrEmpty(logoffPortCmb.Text))
                {
                    MessageBox.Show("已開啟自動過帳，請選擇下機的 Logoff Com Port.", "Warning");
                    return false;
                }
            }
            return true;
        }

        private void pwdTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(13))
                return;

            loginBtn_Click(sender, e);
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            logger.Info("<<<<< AP Exit >>>>>");
            Environment.Exit(Environment.ExitCode);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
#if DEBUG
            this.TopMost = false;
            
#endif

            logger.Info("<<<<< LoginForm Start >>>>>");
            this.Text += " ___ Ver : " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            logger.Info("Version " + this.Text);

            logger.Info("Load ap config.");
            AppConfigUtil.LoadApConfig();
            glueCtrlChk.Checked = AppConfigUtil.GlueCtrlMode;
            reheModeChk.Checked = AppConfigUtil.ReheatMode;
            autoModeChk.Checked = AppConfigUtil.AutoMode;
            logonPortCmb.Text = AppConfigUtil.LogonPort;
            logoffPortCmb.Text = AppConfigUtil.LogoffPort;
            eqpNoTxt.Text = AppConfigUtil.EqpNo;


            string[] comPosts = SerialPort.GetPortNames();
            logger.Debug("Com port list : {0}", string.Join(", ", comPosts));

            logonPortCmb.Items.AddRange(comPosts);
            logoffPortCmb.Items.AddRange(comPosts);
        }

        private void reheModeChk_CheckedChanged(object sender, EventArgs e)
        {
            //reheatMode = reheModeChk.Checked;

            if (reheModeChk.Checked)
            {
                glueCtrlChk.Enabled = false;
                glueCtrlChk.Checked = false;
                autoModeChk.Enabled = false;
                autoModeChk.Checked = false;
            }
            else
            {
                glueCtrlChk.Enabled = true;
                autoModeChk.Enabled = true;
            }
            

        }

        private void autoModeChk_CheckedChanged(object sender, EventArgs e)
        {
            autoGrp.Enabled = autoModeChk.Checked;
            if (autoModeChk.Checked)
            {
                glueCtrlChk.Checked = false;
                glueCtrlChk.Enabled = false;
                reheModeChk.Checked = false;
                reheModeChk.Enabled = false;
            }
            else
            {
                glueCtrlChk.Enabled = true;
                reheModeChk.Enabled = true;
            }
        }

        private void comTestBtn_Click(object sender, EventArgs e)
        {
            ComPortTestForm testForm = new ComPortTestForm();
            testForm.ShowDialog();
        }
    }
}
