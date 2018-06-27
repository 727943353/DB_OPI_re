using DB_OPI.Proxy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace DB_OPI.Forms
{
    public partial class BlueTapeLogonForm : Form
    {
        public string equipmentNo;
        public string userNo;
        private DataTable msgTb = new DataTable();
        private DataTable blueTapeTb = new DataTable();

        public BlueTapeLogonForm()
        {
            InitializeComponent();
        }

        private void BlueTapeLogonForm_Load(object sender, EventArgs e)
        {
            msgTb.Columns.Add("CreateTime");
            msgTb.Columns.Add("Message");
            msgGrid.DataSource = msgTb;

            eqpTxt.Text = equipmentNo;

            blueTapeTb.Columns.Add("TURN_QTY");
            blueTapeTb.Columns.Add("INVENTORYNO");
            blueTapeTb.Columns.Add("MATERIALNO");
            blueTapeTb.Columns.Add("CHIPMODEL");
            blueTapeTb.Columns.Add("MONO");
            blueTapeTb.Columns.Add("MATERIALLOTNO");
            blueTapeTb.Columns.Add("FRAME_ID");
            blueTapeTb.Columns.Add("UNITNO");
            blueTapeTb.Columns.Add("QTY");
            blueTapeTb.Columns.Add("MATERIALTYPE");

            blueTapeGrid.DataSource = blueTapeTb;
        }

        private void matLotNoTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(13))
                return;
            DataTable tb = MesWsLextarProxy.LoadWIPInventory_RawJoinFrameIDByMatLotNo(userNoTxt.Text, equipmentNo, matLotNoTxt.Text);
            if (tb.Rows.Count == 0)
            {
                

                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                matLotNoTxt.SelectAll();
                MessageBox.Show(matLotNoTxt.Text + " 無此物料,請檢查!!");
                return;
            }

            DataRow newRow = blueTapeTb.NewRow();
            newRow["TURN_QTY"] = tb.Rows[0]["QTY"].ToString().Split('.')[0];
            newRow["INVENTORYNO"] = tb.Rows[0]["INVENTORYNO"];
            newRow["MATERIALNO"] = tb.Rows[0]["MATERIALNO"];
            newRow["CHIPMODEL"] = Convert.ToString(tb.Rows[0]["CHIPMODEL"]).Replace(".","V");
            newRow["MONO"] = tb.Rows[0]["MONO"];
            newRow["MATERIALLOTNO"] = tb.Rows[0]["MATERIALLOTNO"];
            newRow["FRAME_ID"] = tb.Rows[0]["FRAME_ID"];
            newRow["UNITNO"] = tb.Rows[0]["UNITNO"];
            newRow["QTY"] = tb.Rows[0]["QTY"].ToString().Split('.')[0];
            newRow["MATERIALTYPE"] = tb.Rows[0]["MATERIALTYPE"];
            blueTapeTb.Rows.Add(newRow);

            matLotNoTxt.Text = "";
            frameIDTxt.Text = "";
            blueTapeCntTxt.Text = Convert.ToString(blueTapeTb.Rows.Count);
        }

        

        private void AddMessageToGrid(string msg)
        {
            DataRow row = msgTb.NewRow();
            row["CreateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            row["Message"] = msg;
            msgTb.Rows.Add(row);

            while (msgTb.Rows.Count > 30)
            {
                msgTb.Rows.RemoveAt(0);
            }
        }

        private void frameIDTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(13))
                return;
            DataTable tb = MesWsLextarProxy.LoadWIPInventory_RawJoinFrameIDByFrameID(userNoTxt.Text, equipmentNo, frameIDTxt.Text);
            if (tb.Rows.Count == 0)
            {


                SystemSounds.Beep.Play();
                SystemSounds.Beep.Play();
                matLotNoTxt.SelectAll();
                MessageBox.Show(frameIDTxt.Text + " 無此物料,請檢查!!");
                return;
            }

            DataRow newRow = blueTapeTb.NewRow();
            newRow["TURN_QTY"] = tb.Rows[0]["QTY"].ToString().Split('.')[0];
            newRow["INVENTORYNO"] = tb.Rows[0]["INVENTORYNO"];
            newRow["MATERIALNO"] = tb.Rows[0]["MATERIALNO"];
            newRow["CHIPMODEL"] = Convert.ToString(tb.Rows[0]["CHIPMODEL"]).Replace(".", "V");
            newRow["MONO"] = tb.Rows[0]["MONO"];
            newRow["MATERIALLOTNO"] = tb.Rows[0]["MATERIALLOTNO"];
            newRow["FRAME_ID"] = tb.Rows[0]["FRAME_ID"];
            newRow["UNITNO"] = tb.Rows[0]["UNITNO"];
            newRow["QTY"] = tb.Rows[0]["QTY"].ToString().Split('.')[0];
            newRow["MATERIALTYPE"] = tb.Rows[0]["MATERIALTYPE"];
            blueTapeTb.Rows.Add(newRow);

            matLotNoTxt.Text = "";
            frameIDTxt.Text = "";
            blueTapeCntTxt.Text = Convert.ToString(blueTapeTb.Rows.Count);

        }

        private void comfBtn_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (string.IsNullOrEmpty(userNoTxt.Text))
                {
                    MessageBox.Show(" Please Keyin UserNo 請刷輸入工號!! ");
                    userNoTxt.Focus();
                    return;
                }

                if (blueTapeTb.Rows.Count == 0)
                {
                    MessageBox.Show("將要上機的 Blue Tape = 0，請輸入要上機的 Blue Tape。");
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
                if (CheckLogonMoNoMatchEqpMoNo() == false)
                    return;

                MesWsLextarProxy.Exe_WIP_RAW_TurnEQP(userNoTxt.Text, equipmentNo, blueTapeTb);

                MessageBox.Show("SuccessFully");

                this.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            
            
        }

        

        private bool CheckLogonMoNoMatchEqpMoNo()
        {
            string logonMoNo = "";
            DataTable blueTapeListTb = MesWsLextarProxy.funLoadEQPMaterialStateJoinBlueTapeList(userNoTxt.Text, equipmentNo);
            if (blueTapeListTb.Rows.Count > 0)
            {
                blueTapeListTb.DefaultView.Sort = "MONO Desc";
                logonMoNo = Convert.ToString(blueTapeListTb.Rows[0]["MONO"]);
            }
            else
            {
                string matLotNo = Convert.ToString(blueTapeTb.Rows[0]["MATERIALLOTNO"]);
                blueTapeListTb = MesWsLextarProxy.LoadLabel_BlueTape(userNoTxt.Text, equipmentNo, matLotNo);
                if (blueTapeListTb.Rows.Count > 0)
                {
                    blueTapeListTb.DefaultView.Sort = "MONO Desc";
                    logonMoNo = Convert.ToString(blueTapeListTb.Rows[0]["MONO"]);
                }
            }

            string eqpMoNo = ""; //目前在機台上生產的工單
            DataTable eqpMoNoTb = MesWsLextarProxy.LoadTemp_EquipmentJoinLotBasis(userNoTxt.Text, equipmentNo);
            if (eqpMoNoTb.Rows.Count > 0)
                eqpMoNo = Convert.ToString(eqpMoNoTb.Rows[0]["MONO"]);
                        
            
            foreach (DataRow uiSelRow in blueTapeTb.Rows)
            {
                string matLotNo = Convert.ToString( uiSelRow["MATERIALLOTNO"]);
                blueTapeListTb = MesWsLextarProxy.LoadLabel_BlueTape(userNoTxt.Text, equipmentNo, matLotNo);
                if (blueTapeListTb.Rows.Count == 0)
                {
                    MessageBox.Show("物料批號：" + Convert.ToString(uiSelRow["MATERIALLOTNO"]) + " 不屬於任何一張工單, 請檢查!!");
                    return false;
                }

                blueTapeListTb.DefaultView.Sort = "MONO Desc";
                string matLotMoNo = Convert.ToString(blueTapeListTb.Rows[0]["MONO"]); //blue tape 所屬工單
                if (logonMoNo != matLotMoNo)
                {
                    MessageBox.Show("物料批號：" + Convert.ToString(uiSelRow["MATERIALLOTNO"]) + " 不屬於此張工單 " + logonMoNo + " 請檢查!!");
                    return false;
                }

                if (string.IsNullOrEmpty(eqpMoNo) == false && eqpMoNo != matLotMoNo)
                {
                    MessageBox.Show("物料批號：" + Convert.ToString(uiSelRow["MATERIALLOTNO"]) + " 不屬於此張工單 " + eqpMoNo + " 請檢查!!");
                    return false;
                }

            }

            return true;
        }

        private void clsBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
