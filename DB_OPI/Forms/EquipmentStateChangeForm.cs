using DB_OPI.Components;
using DB_OPI.Proxy;
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
    public partial class EquipmentStateChangeForm : Form
    {
        public string equipmentNo;
        
        public string eqpState;
        private string userNo;
        private DataTable stateBasisTb = null;
        public EquipmentStateChangeForm()
        {
            InitializeComponent();
        }

        private void EquipmentStateChangeForm_Load(object sender, EventArgs e)
        {
            this.Text += " ___ Ver : " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            eqpNoLab.Text = equipmentNo;
            eqpStateLab.Text = eqpState;

            //DataTable eqpStateTb = MesWsProxy.LoadEquipmentStateBySMD(userNo, equipmentNo);

            stateBasisTb = MesWsProxy.LoadEQPStateBasis(userNo, equipmentNo);
            var colorNum = (from row in stateBasisTb.AsEnumerable()
                           where row.Field<string>("STATENAME") == eqpState
                           select Convert.ToInt32(row["STATECOLOR"])).SingleOrDefault();

            eqpNoLab.BackColor = System.Drawing.Color.FromArgb(colorNum);
            
            CreateStateButton();
        }

        private void CreateStateButton()
        {
            StateButton stateBtn = null;
            Color color = Color.White;
            var sortedRows = stateBasisTb.AsEnumerable().OrderBy(r => r.Field<decimal>("EquipmentState")).ToArray();
            foreach (DataRow row in sortedRows)
            {
                color = System.Drawing.Color.FromArgb(Convert.ToInt32(row["STATECOLOR"]));
                stateBtn = new StateButton(row.Field<string>("STATENAME"), color);
                stateBtn.EqpStateNo = Convert.ToInt32(row["EquipmentState"]);
                stateBtn.ClickedEvent += new EventHandler(StateBtnClicked_Handler);
                stateBtnPanel.Controls.Add(stateBtn);

            }
        }

        private void StateBtnClicked_Handler(object sender, EventArgs e)
        {
            userNo = userNoTxt.Text.Trim();
            string pwd = pwdTxt.Text.Trim();
            if (string.IsNullOrEmpty(userNo) || string.IsNullOrEmpty(pwd))
            {   
                MessageBox.Show("User No , Password 不能為空 (User No , Password  can,t be empty) !!","Warning");
                userNoTxt.Focus();
                return;
            }

            if (MesWsAutoProxy.Login(userNo, pwd) == false)
            {
                
                userNoTxt.SelectAll();
                MessageBox.Show("帳號或密碼錯誤 !! (UserNo or PassWord is error)", "LogIn failed");
                return;
            }


            EquipmentStateChangeDescriptionForm chgDescForm = new EquipmentStateChangeDescriptionForm();
            chgDescForm.equipmentNo = equipmentNo;
            chgDescForm.userNo = userNo;
            chgDescForm.ReasonSubType = ((Button)sender).Text;

            //will change to number of state
            int chgStateNo = (int)((Button)sender).Tag;
            chgDescForm.eqpStateNo = chgStateNo;
            if (chgDescForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string desc = chgDescForm.description;
                string reason = chgDescForm.selReason;
                bool blnPrintLabel = chgDescForm.isPrintLabel;
                string strLabelFormat = chgDescForm.strLabelFormat;
                string strPrinter = chgDescForm.printer;
                string strReasonName = chgDescForm.reasonName;
                string strIPQC_Lot = chgDescForm.strIPQC_Lot;

                MesWsProxy.EditEquipmentState(userNo, equipmentNo, chgStateNo, desc, reason);

                MesWsProxy.funChangeStateActive(userNo, equipmentNo, strIPQC_Lot, chgStateNo);

                var stateRow = (from row in stateBasisTb.AsEnumerable()
                                where row.Field<decimal>("EquipmentState") == chgStateNo
                                select row).SingleOrDefault();

                eqpState = Convert.ToString(stateRow["STATENAME"]);
                eqpStateLab.Text = eqpState;

                eqpNoLab.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(stateRow["STATECOLOR"]));

                if (blnPrintLabel)
                {
                    string result = MesWsLextarProxy.funChangeStatePrintLabel(userNo, equipmentNo, strPrinter, strLabelFormat, strReasonName, strIPQC_Lot);
                    if (result == "NoLotOnEQP")
                    {
                        MessageBox.Show("無批號在機台上,派工單標籤列印失敗 \r\n ( No LotNo On Equipment ,work sheet print label fail )!!");
                    }
                    else
                    {
                        MessageBox.Show("列印派工單標籤成功 \r\n ( Print work sheet Label Successed )!!");
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Change Eqp status error." + ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void userNoTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(13))
                return;

            pwdTxt.Focus();
        }
    }
}
