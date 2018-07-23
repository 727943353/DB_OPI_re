using DB_OPI.Proxy;
using DB_OPI.Util;
using DB_OPI.Vo;
using NLog;
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
    public partial class CstLogonForm : Form
    {
        private ILogger logger = LogManager.GetCurrentClassLogger();
        public string userNo;
        public string pwd;
        public string loadingCst;
        public string unloadingCst;
        //public string eqpNo;
        //public bool doGlueVerify;

        public CstLogonForm()
        {
            InitializeComponent();
        }

        public void SetAutoLogonData(string loadCst, string unloadCst, string userNo, string pwd)
        {
            txtLoadingCassette.Text = loadCst.ToUpper().Trim();
            txtUnloadingCassette.Text = unloadCst.ToUpper().Trim();
            userNoTxt.Text = userNo.ToUpper().Trim();
            pwdTxt.Text = pwd;

            logger.Info("Auto Mode => Set CstLogonForm Data. LoadingCst: {0}, UnloadingCst: {1}, UserNo: {2}", txtLoadingCassette.Text, txtUnloadingCassette.Text, userNoTxt.Text);
        }


        

        public void btnConfirm_Click(object sender, EventArgs e)
        {
            logger.Info("===== Confirm Click start =====");
            lblMessage.Text = "";
            lblMessage.BackColor = Color.White;

            if (string.IsNullOrEmpty(txtLoadingCassette.Text) || string.IsNullOrEmpty(txtUnloadingCassette.Text))
            {
                lblMessage.Text = "上下機 彈匣不能為空 (Loading Cassette or Unloading Cassette can,t be empty) !!";
                logger.Warn(lblMessage.Text);

                lblMessage.BackColor = Color.Red;
                txtLoadingCassette.Focus();

                logger.Info("===== Confirm Click end =====");
                return;
            }

            userNo = userNoTxt.Text.Trim();
            string pwd = pwdTxt.Text.Trim();
            if (string.IsNullOrEmpty(userNo) || string.IsNullOrEmpty(pwd))
            {
                lblMessage.Text = "User No , Password 不能為空 (User No , Password  can,t be empty) !!";
                logger.Warn(lblMessage.Text);
                lblMessage.BackColor = Color.Red;
                userNoTxt.Focus();
                logger.Info("===== Confirm Click end =====");
                return;
            }
            
            if (MesWsAutoProxy.Login(userNo, pwd) == false)
            {
                
                lblMessage.Text = "帳號或密碼錯誤 !! (UserNo or PassWord is error)";
                logger.Warn(lblMessage.Text);
                lblMessage.BackColor = Color.Red;
                userNoTxt.SelectAll();
                //MessageBox.Show("帳號或密碼錯誤 !! (UserNo or PassWord is error)", "Log In failed");
                logger.Info("===== Confirm Click end =====");
                return;
            }
            

            string loadingCst = txtLoadingCassette.Text.Trim();
            string unloadingCst = txtUnloadingCassette.Text.Trim();
            logger.Info("Glue Control Mode : {0}", AppConfigUtil.GlueCtrlMode);
            if (AppConfigUtil.GlueCtrlMode)
            {
                if (CheckGlueLifeTime() == false)
                {
                    logger.Info("===== Confirm Click end =====");
                    return;
                }
                
            }
            LotInfo lotInfo;
            string msg;
            if (MesWsLextarProxy.GetLotInfo(userNo, AppConfigUtil.EqpNo, loadingCst, out lotInfo, out msg) == false)
            {
                lblMessage.Text = msg;
                logger.Warn(lblMessage.Text);

                lblMessage.BackColor = Color.Red;
                txtLoadingCassette.Focus();
                logger.Info("===== Confirm Click end =====");
                return;
            }

            msg = "";
            if (MesWsAutoProxy.CheckInFunction_Cassette(userNo, loadingCst, unloadingCst, AppConfigUtil.EqpNo, lotInfo.OpNo, ref msg) == true)
            {
                lblMessage.Text = loadingCst + " Logon Successfully !!";
                lblMessage.BackColor = Color.Lime;

                txtLoadingCassette.Text = "";
                txtUnloadingCassette.Text = "";
                txtLoadingCassette.Focus();
            }
            else
            {
                lblMessage.Text = msg;
                logger.Warn(lblMessage.Text);
                lblMessage.BackColor = Color.Red;
            }

            userNoTxt.Text = "";
            pwdTxt.Text = "";
            logger.Info("===== Confirm Click end =====");
        }


        private bool CheckGlueLifeTime()
        {

            try
            {
                
                DataTable tb = MesWsLextarProxy.LoadMaterialRecordJoinGlueUsedStateOnEquipment(userNo, AppConfigUtil.EqpNo);
                if (tb.Rows.Count == 0)
                {
                    ShowErrorMsg("CST 上機失敗，此 " + AppConfigUtil.EqpNo + " 機台並未上固晶膠。");

                    
                    return false;
                }
                

                var lifeEndRows = (from row in tb.AsEnumerable()
                                   where Convert.ToDateTime(row["LIFE_END_TIME"]) < DateTime.Now
                               select row.Field<string>("MATERIALLOTNO")).ToArray();

                if (lifeEndRows.Length > 0)
                {
                    MessageBox.Show("上機失敗，" + string.Join(", ", lifeEndRows) + " 已過使用期限，請做 Material 下機。");
                    //lblMessage.Text = "上機失敗，" + string.Join(", ", lifeEndRows) + " 已過使用期限，請做 Material 下機。";
                    //lblMessage.BackColor = Color.Red;
                    ShowErrorMsg("上機失敗，" + string.Join(", ", lifeEndRows) + " 已過使用期限，請做 Material 下機。");
                    return false;
                }
                

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Check Glue Life Time Error");
                MessageBox.Show("Check Glue Life Time Error." + ex.ToString());
                
                return false;
            }
            return true;
        }
        private void txtLoadingCassette_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(13))
                return;
            lblMessage.Text = "";
            lblMessage.BackColor = Color.Empty;

            txtUnloadingCassette.Focus();
        }

        private void CstLogonForm_Load(object sender, EventArgs e)
        {
            this.Text += " ___ Ver : " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            #if DEBUG
            this.TopMost = false;
            #endif

            userNoTxt.Text = userNo;
            pwdTxt.Text = pwd;
            txtLoadingCassette.Text = loadingCst;
            txtUnloadingCassette.Text = unloadingCst;

            if (AppConfigUtil.GlueCtrlMode)
            {
                glueCtrlStateLab.Text = "膠材卡控 : Enabled";
                glueCtrlStateLab.ForeColor = Color.ForestGreen;
            }
            else
            {
                glueCtrlStateLab.Text = "膠材卡控 : Disabled";
            }
        }

        private void txtUnloadingCassette_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(13))
                return;

            userNoTxt.Focus();
            //btnConfirm_Click(sender, e);
        }

        private void ShowErrorMsg(string msg)
        {
            logger.Warn(msg);

            lblMessage.Text = msg;
            lblMessage.BackColor = Color.Red;
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pwdTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(13))
                return;

            btnConfirm_Click(sender, e);
        }

        private void userNoTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(13))
                return;

            pwdTxt.Focus();
        }
    }
}
