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
    public partial class MaterialLogonForm : Form
    {
        public string equipmentNo;
        public string userNo;
        public bool isVerifyGlue = false;
        
        private DataTable msgTb = new DataTable();
        public MaterialLogonForm()
        {
            InitializeComponent();
        }

        private void MaterialLogonForm_Load(object sender, EventArgs e)
        {
            msgTb.Columns.Add("CreateTime");
            msgTb.Columns.Add("Message");
            msgGrid.DataSource = msgTb;

            eqpTxt.Text = equipmentNo;

            matHistGrid.DataSource = MesWsLextarProxy.LoadMaterialRecord(userNoTxt.Text, equipmentNo);
            userNoTxt.Focus();

            
        }

        private void matLotNoTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(13))
                return;

            AddMessageToGrid(matLotNoTxt.Text + " 物料批號即將上機");
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

        private void clsBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comfBtn_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(userNoTxt.Text.Trim()))
            {
                MessageBox.Show("Please Keyin UserNo 請刷輸入工號!!","Warning");
                userNoTxt.Focus();
                return;
            }

            if (string.IsNullOrEmpty(matLotNoTxt.Text.Trim()))
            {
                MessageBox.Show("MaterialLotNo can't be empty!!", "Warning");
                matLotNoTxt.Focus();
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            
            try
            {
                string operName = equipmentNo.Substring(2, 2);
                string matNo = matLotNoTxt.Text.Split('-')[0];
                if (operName == "DB")
                {
                    if (matLotNoTxt.Text.StartsWith("42.") && isVerifyGlue)
                    {
                        //檢查是否有在其它機台上機
                        DateTime endTime = DateTime.Now;
                        DateTime stTime = endTime.AddMonths(-1);
                        DataTable matUsedTb = MesWsLextarProxy.LoadMaterialRecordByMaterialLotNo(userNoTxt.Text, matLotNoTxt.Text, stTime, endTime);
                        var selRows = (from row in matUsedTb.AsEnumerable()
                                       where row["LOGOFF_TIME"] == DBNull.Value
                                       select row).ToArray();

                        if (selRows.Length > 0)
                        {
                            string eqpNo = Convert.ToString(selRows[0]["EQUIPMENTNO"]);
                            MessageBox.Show("上機失敗，" + matLotNoTxt.Text + " 已在 [" + eqpNo + "] 上機。", "Error");
                            return;
                        }

                        if (VerifyGlueLifeTime() == false)
                            return;
                    }
                    
                    MesWsLextarProxy.Add_Material_Record(userNoTxt.Text, equipmentNo, matNo, matLotNoTxt.Text);
                }
                else if (operName == "WB")
                {
                    MesWsLextarProxy.UpdateMaterialRecord(userNoTxt.Text, equipmentNo, "Null");
                    MesWsLextarProxy.Add_Material_Record(userNoTxt.Text, equipmentNo, matNo, matLotNoTxt.Text);
                }
                Cursor.Current = Cursors.Default;
                MessageBox.Show("SuccessFully");
                matHistGrid.DataSource = MesWsLextarProxy.LoadMaterialRecord(userNoTxt.Text, equipmentNo);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), "Error");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

            
        }

        private bool VerifyGlueLifeTime()
        {
            try
            {
                string materLotNo = matLotNoTxt.Text.Trim();
                DataTable tb = MesWsLextarProxy.LoadGlueUsedState(userNoTxt.Text, materLotNo);
                //如果query 不到膠的上機狀態，表示是第一次使用
                if (tb.Rows.Count == 0)
                {
                    MessageBox.Show("找不到膠 [" + materLotNo + "] 回溫記錄，請確認是否有做回溫設定.", "Error");
                    return false;
                }
                string onEqpNo = Convert.ToString(tb.Rows[0]["EQP_NO"]); //已上機的機台ID
                if (string.IsNullOrEmpty(onEqpNo) == false)
                {
                    MessageBox.Show("[" + materLotNo + "] 已在 " + onEqpNo + " 上機。", "Error");
                    return false;
                }

                DateTime reheatEndTime = Convert.ToDateTime(tb.Rows[0]["REHEAT_END_TIME"]);
                if (DateTime.Now < reheatEndTime)
                {
                    MessageBox.Show("[" + materLotNo + "] 未完成回溫，回溫完成時間 [" + reheatEndTime.ToString("yyyy-MM-dd HH:mm:ss") + "]。", "Error");
                    return false;
                }

                DateTime expTime = Convert.ToDateTime(tb.Rows[0]["EXP_TIME"]);
                if (expTime < DateTime.Now)
                {
                    MessageBox.Show("[" + materLotNo + "] 已超過使用期限，使用期限 [" + expTime.ToString("yyyy-MM-dd HH:mm:ss") + "]。", "Error");
                    return false;
                }

                string matNo = materLotNo.Split('-')[0];
                DataTable lifeTimeTb = MesWsLextarProxy.GetMaterialLifeTimeSetting(userNoTxt.Text, equipmentNo, matNo);
                if (lifeTimeTb.Rows.Count == 0)
                {
                    MessageBox.Show("找不到膠 [" + matNo + "] LIFE_TIME 設定，請確認 MES Recipe Parameter 設定.");
                    return false;
                }

                double lifeTime = Convert.ToDouble(lifeTimeTb.Rows[0]["PARAMETERVALUE"]);
                DateTime stTime = DateTime.Now;
                MesWsLextarProxy.UpdateGlueLifeTime(userNoTxt.Text, materLotNo, equipmentNo, stTime, stTime.AddHours(lifeTime));

                //string materLotNo = matLotNoTxt.Text.Trim();
                ////DateTime endTime = DateTime.Now;
                ////DateTime stTime = endTime.AddMonths(-2);

                //DataTable tb = MesWsLextarProxy.LoadMaterialUsedState(userNoTxt.Text, materLotNo, Form1.GLUE_LIFE_TIME_TYPE);


                ////如果query 不到膠的上機狀態，表示是第一次使用
                //if (tb.Rows.Count == 0)
                //{
                //    string matNo = materLotNo.Split('-')[0];
                //    DataTable lifeTimeTb = MesWsLextarProxy.GetMaterialLifeTimeSetting(userNoTxt.Text, equipmentNo, matNo);
                //    if (lifeTimeTb.Rows.Count == 0)
                //    {
                //        MessageBox.Show("找不到膠 [" + matNo + "] LIFE_TIME 設定，請確認 MES Recipe Parameter 設定.");
                //        return false;
                //    }

                //    double lifeTime = Convert.ToDouble(lifeTimeTb.Rows[0]["PARAMETERVALUE"]);
                //    DateTime stTime = DateTime.Now;
                //    MesWsLextarProxy.InsertMaterialUsedState(userNoTxt.Text, equipmentNo, materLotNo, stTime, stTime.AddHours(lifeTime), Form1.GLUE_LIFE_TIME_TYPE);

                //    return true;
                //}

                //DataRow materStateRow = tb.Rows[0];

                //DateTime expTime = Convert.ToDateTime(materStateRow["EXP_TIME"]);
                //if (expTime < DateTime.Now)
                //{
                //    StringBuilder errMsg = new StringBuilder();
                //    errMsg.AppendLine("上機失敗，膠 [" + materLotNo + "] 的使用期限已超過。 " + materLotNo + " Exp Time : " + expTime.ToString("yyyy-MM-dd HH:mm:ss"));
                //    errMsg.AppendLine("Logon fail, Glue [" + materLotNo + "] is over exp time。 " + materLotNo + " Exp Time : " + expTime.ToString("yyyy-MM-dd HH:mm:ss"));
                //    MessageBox.Show(errMsg.ToString());
                //    return false;
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private void matLogoffBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userNoTxt.Text))
            {
                MessageBox.Show(" Please Keyin UserNo 請刷輸入工號!! ");
                userNoTxt.Focus();
                return;
            }

            int selIdx = matHistGrid.SelectedCells[0].RowIndex;
            DataTable matTb = (DataTable)matHistGrid.DataSource;
            string logoffTimeStr = Convert.ToString(matTb.Rows[selIdx]["LOGOFF_TIME"]);
            string selectedMaterialLotNo = Convert.ToString(matTb.Rows[selIdx]["MATERIALLOTNO"]);
            if (string.IsNullOrEmpty(logoffTimeStr) == false)
            {
                MessageBox.Show("物料批號：" + selectedMaterialLotNo + " 非上機狀態，請檢查!! ");
                return;
            }

            MesWsLextarProxy.UpdateMaterialRecord(userNoTxt.Text, equipmentNo, selectedMaterialLotNo);
            matHistGrid.DataSource = MesWsLextarProxy.LoadMaterialRecord(userNoTxt.Text, equipmentNo);

            AddMessageToGrid(selectedMaterialLotNo + " 物料批號下機成功 ");
            userNoTxt.Text = "";
        }
    }
}
