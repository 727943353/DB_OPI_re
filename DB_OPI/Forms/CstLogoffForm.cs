﻿using DB_OPI.Proxy;
using DB_OPI.Vo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DB_OPI.Forms
{
    public partial class CstLogoffForm : Form
    {
        private string userNo;
        public string eqpNo;
        public string opNo;

        public CstLogoffForm()
        {
            InitializeComponent();
        }

        private void btnKeyboard_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("osk.exe");
        }

        private void txtUnloadingCassette_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(13))
                return;

            LoadLotInfo();
            userNoTxt.Focus();
        }

        /// <summary>
        /// Set auto logoff data
        /// </summary>
        /// <param name="unloadCst"></param>
        /// <param name="userNo"></param>
        /// <param name="pwd"></param>
        public void SetAutoLogoffData(string unloadCst, string userNo, string pwd)
        {
            txtUnloadingCassette.Text = unloadCst.ToUpper().Trim();
            userNoTxt.Text = userNo.ToUpper().Trim();
            pwdTxt.Text = pwd.ToUpper().Trim();

        }

        private void LoadOPError()
        {
            if (string.IsNullOrEmpty(opNo))
                return;

            DataTable opErrTb = MesWsProxy.LoadOPErrorJoinBasis(eqpNo, userNo, opNo);
            opErrTb.Columns.Add(new DataColumn("CheckFlag", typeof(bool)));
            opErrTb.Columns.Add(new DataColumn("ErrorQty", typeof(Int32)));

            foreach (DataRow row in opErrTb.Rows)
            {
                row["CheckFlag"] = false;
                row["ErrorQty"] = 0;
            }

            //string[] keepCols = new string[] { "ERRORNO", "REASONNAME", "REASONTYPE" };
            //for (int colIdx = 0; colIdx < opErrTb.Columns.Count; colIdx++)
            //{

            //    if (keepCols.Contains(opErrTb.Columns[colIdx].ColumnName) == false)
            //    {
            //        opErrTb.Columns.RemoveAt(colIdx);
            //        colIdx--;
            //    }
            //}

            iugError.DataSource = opErrTb;
        }

        private void CstLogoffForm_Load(object sender, EventArgs e)
        {
            this.Text += " ___ Ver : " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
#if DEBUG
            this.TopMost = false;
#endif
            iugError.AutoGenerateColumns = false;
            txtEquipmentNo.Text = eqpNo;
            LoadOPError();
        }

        private void iugError_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (iugError.Columns[e.ColumnIndex].Name == "errQtyCol")
            {
                int value;
                if (int.TryParse(iugError.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out value) == false)
                {
                    MessageBox.Show("Error Qty 必需為數字。");
                    //iugError.CurrentCell = iugError.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                    iugError.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;


                }
            }
            
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DoLogoffCst();
        }


        public bool DoLogoffCst()
        {
            if (string.IsNullOrEmpty(txtUnloadingCassette.Text))
            {
                lblMessage.Text = "下機 彈匣不能為空 ( Unloading Cassette can,t be empty) !!";
                lblMessage.BackColor = Color.Red;
                txtUnloadingCassette.Focus();
                return false;
            }

            userNo = userNoTxt.Text.Trim();
            string pwd = pwdTxt.Text.Trim();
            if (string.IsNullOrEmpty(userNo) || string.IsNullOrEmpty(pwd))
            {
                lblMessage.Text = "User No , Password 不能為空 (User No , Password  can,t be empty) !!";
                lblMessage.BackColor = Color.Red;
                userNoTxt.Focus();
                return false;
            }

            if (MesWsAutoProxy.Login(userNo, pwd) == false)
            {

                lblMessage.Text = "帳號或密碼錯誤 !! (UserNo or PassWord is error)";
                lblMessage.BackColor = Color.Red;
                userNoTxt.SelectAll();
                MessageBox.Show("帳號或密碼錯誤 !! (UserNo or PassWord is error)", "Log In failed");
                return false;
            }

            try
            {
                string CST_ID = txtUnloadingCassette.Text.Trim();
                if (CST_ID.StartsWith("L") || CST_ID.StartsWith("R"))
                {
                    if (CST_ID.Length >= 8)
                    {
                        CST_ID = CST_ID.Substring(1, CST_ID.Length - 1);
                    }
                }
                LotInfo lotInfo;
                string msg;
                if (MesWsLextarProxy.GetLotInfo(userNo, eqpNo, CST_ID, out lotInfo, out msg) == false)
                {
                    lblMessage.Text = msg;
                    lblMessage.BackColor = Color.Red;
                    txtUnloadingCassette.Focus();
                    return false;
                }

                if (VerifyOpErrorGridInput() == false)
                    return false;

                if (VerifyMaterialQty(lotInfo.LotNo, lotInfo.CurrQty) == false)
                {
                    return false;
                }

                DataTable errOpTb = (DataTable)iugError.DataSource;

                var drSel = errOpTb.Select("CheckFlag=" + true);

                if (MesWsAutoProxy.CheckOutFunction_DB(eqpNo, userNo, CST_ID, opNo, drSel, txtLotRecord.Text.Trim(), ref msg) == true)
                {
                    txtLotRecord.Text = "";
                    foreach (DataRow row in drSel)
                    {
                        row["CheckFlag"] = false;
                        row["ErrorQty"] = 0;
                    }

                    lblMessage.Text = CST_ID + " LogOff Successfully !!";
                    lblMessage.BackColor = Color.Lime;


                    txtUnloadingCassette.Text = "";
                    txtUnloadingCassette.Focus();


                }
                else
                {
                    lblMessage.Text = msg;
                    lblMessage.BackColor = Color.Red;
                    txtUnloadingCassette.SelectAll();
                    if (msg.IndexOf("Future Wait") > -1)
                    {
                        txtLotRecord.Text = "";
                        foreach (DataRow row in drSel)
                        {
                            row["CheckFlag"] = false;
                            row["ErrorQty"] = 0;
                        }

                        lblMessage.BackColor = Color.Yellow;
                        txtUnloadingCassette.Text = "";
                        txtUnloadingCassette.Focus();
                    }


                }

                userNoTxt.Text = "";
                pwdTxt.Text = "";

                return true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.BackColor = Color.Red;
                txtUnloadingCassette.SelectAll();
                return false;
            }
        }

        private bool VerifyMaterialQty(string lotNo, int lotQty)
        {
            DataTable tempMaterTb = MesWsProxy.LoadTemp_Material(eqpNo, userNo, lotNo);
            if (tempMaterTb.Rows.Count > 0)
            {
                string materialNo = tempMaterTb.Rows[0]["MaterialNo"].ToString();
                string materialLevel = tempMaterTb.Rows[0]["MaterialLevel"].ToString();
                string unitNo = tempMaterTb.Rows[0]["UnitNo"].ToString();

                DataTable maertLotTb = MesWsLextarProxy.LoadMaterialLot_DBC(eqpNo, userNo, lotNo, materialNo, materialLevel, unitNo);
                if (maertLotTb.Rows.Count == 0)
                {
                    MessageBox.Show("無藍膜帳料可扣，請確認藍膜數量或產品BOM設定是否正確 (No blueTape to use on equipment)");
                    return false;
                }

                var materSum = (from row in maertLotTb.AsEnumerable()
                                select Convert.ToInt32(row["Qty"])).Sum();

                if (materSum < (lotQty * Convert.ToInt32(tempMaterTb.Rows[0]["STDQty"])))
                {
                    if (MessageBox.Show(" 藍膜數量不足,是否繼續過帳? (BlueTape Qty not enough, Continue ?)", "Waring", MessageBoxButtons.YesNo) == DialogResult.No)
                        return false;
                }

            }

            return true;
        }
        private bool VerifyOpErrorGridInput()
        {
            
            DataTable tb = (DataTable)iugError.DataSource;
            var selRows = tb.Select("CheckFlag=" + true + " and (ErrorQty <= 0 or ErrorQty is null)");
            if (selRows.Length > 0)
            {
                lblMessage.Text = "ErrorNo : " + selRows[0]["ErrorNo"].ToString() + " ErrorQty <= 0 !!";
                lblMessage.BackColor = Color.Red;
                return false;
            }

            

            return true;
        }

        private void userNoTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(13))
                return;

            pwdTxt.Focus();
        }

        private void pwdTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(13))
                return;

            
        }

        public bool LoadLotInfo()
        {
            

            try
            {
                txtLotNo.Text = "";
                txtCurQty.Text = "";

                string unLoadCst = txtUnloadingCassette.Text.Trim();
                if (unLoadCst.StartsWith("L") || unLoadCst.StartsWith("R"))
                {
                    if (unLoadCst.Length >= 8)
                    {
                        txtUnloadingCassette.Text = unLoadCst.Substring(1, unLoadCst.Length - 1);
                    }
                }

                LotInfo lotInfo;
                string msg;
                if (MesWsLextarProxy.GetLotInfo(userNo, "", unLoadCst, out lotInfo, out msg) == false)
                {
                    lblMessage.Text = msg;
                    lblMessage.BackColor = Color.Red;
                    txtUnloadingCassette.SelectAll();
                    return false;
                }

                opNo = lotInfo.OpNo;

                txtLotNo.Text = lotInfo.LotNo;
                txtCurQty.Text = Convert.ToString(lotInfo.CurrQty);
                DataTable tb = MesWsProxy.LoadTemp_EquipmentLot(eqpNo, userNo, lotInfo.LotNo);
                if (tb.Rows.Count == 0)
                {
                    lblMessage.Text = txtLotNo.Text + " status is not running!!";
                    lblMessage.BackColor = Color.Red;
                    return false;
                }

                LoadOPError();

                lblMessage.Text = "";
                lblMessage.BackColor = Color.Transparent;
                btnConfirm.Focus();
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;

            }

            

        }
        
        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
