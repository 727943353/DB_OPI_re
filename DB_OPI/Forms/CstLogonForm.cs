using DB_OPI.Proxy;
using DB_OPI.Vo;
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
        private string userNo;
        public string eqpNo;
        public bool doGlueVerify;

        public CstLogonForm()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.BackColor = Color.White;

            if (string.IsNullOrEmpty(txtLoadingCassette.Text) || string.IsNullOrEmpty(txtUnloadingCassette.Text))
            {
                lblMessage.Text = "上下機 彈匣不能為空 (Loading Cassette or Unloading Cassette can,t be empty) !!";
                lblMessage.BackColor = Color.Red;
                txtLoadingCassette.Focus();
                return;
            }

            userNo = userNoTxt.Text.Trim();
            string pwd = pwdTxt.Text.Trim();
            if (string.IsNullOrEmpty(userNo) || string.IsNullOrEmpty(pwd))
            {
                lblMessage.Text = "User No , Password 不能為空 (User No , Password  can,t be empty) !!";
                lblMessage.BackColor = Color.Red;
                userNoTxt.Focus();
                return;
            }
            
            if (MesWsAutoProxy.Login(userNo, pwd) == false)
            {
                
                lblMessage.Text = "帳號或密碼錯誤 !! (UserNo or PassWord is error)";
                lblMessage.BackColor = Color.Red;
                userNoTxt.SelectAll();
                MessageBox.Show("帳號或密碼錯誤 !! (UserNo or PassWord is error)", "Log In failed");
                return;
            }
            

            string loadingCst = txtLoadingCassette.Text.Trim();
            string unloadingCst = txtUnloadingCassette.Text.Trim();
            if (doGlueVerify)
            {
                if (CheckGlueLifeTime() == false)
                    return;
            }
            LotInfo lotInfo;
            string msg;
            if (MesWsLextarProxy.GetLotInfo(userNo, eqpNo, loadingCst, out lotInfo, out msg) == false)
            {
                lblMessage.Text = msg;
                lblMessage.BackColor = Color.Red;
                txtLoadingCassette.Focus();
                return;
            }

            msg = "";
            if (MesWsAutoProxy.CheckInFunction_Cassette(userNo, loadingCst, unloadingCst, eqpNo, lotInfo.OpNo, ref msg) == true)
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
                lblMessage.BackColor = Color.Red;
            }

            userNoTxt.Text = "";
            pwdTxt.Text = "";
        }


        private bool CheckGlueLifeTime()
        {

            try
            {
                
                DataTable tb = MesWsLextarProxy.LoadMaterialRecordJoinGlueUsedStateOnEquipment(userNo, eqpNo);
                if (tb.Rows.Count == 0)
                {
                    ShowErrorMsg("上機失敗，此機台並未上固晶膠。");
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

            if (doGlueVerify)
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
