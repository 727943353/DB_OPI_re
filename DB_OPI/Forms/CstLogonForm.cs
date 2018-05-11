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
        public string userNo;
        public string eqpNo;

        public CstLogonForm()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLoadingCassette.Text) || string.IsNullOrEmpty(txtUnloadingCassette.Text))
            {
                lblMessage.Text = "上下機 彈匣不能為空 (Loading Cassette or Unloading Cassette can,t be empty) !!";
                lblMessage.BackColor = Color.Red;
                txtLoadingCassette.Focus();
                return;
            }

            string loadingCst = txtLoadingCassette.Text.Trim();
            string unloadingCst = txtUnloadingCassette.Text.Trim();

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
                lblMessage.Text = loadingCst + " LogOn Successfully !!";
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
                        
        }

        private void txtLoadingCassette_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(13))
                return;
            lblMessage.Text = "";
            lblMessage.BackColor = Color.Empty;

            txtUnloadingCassette.Focus();
        }
    }
}
