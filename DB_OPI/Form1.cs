using DB_OPI.Forms;
using DB_OPI.Proxy;
using DB_OPI.Queues;
using DB_OPI.Util;
using MesCommonCode.WebService.Msg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_OPI
{
    public partial class Form1 : Form
    {
        private System.Timers.Timer eqpStateTimer;
        private FixedSizedQueue<string> glReheatDoneQueue = new FixedSizedQueue<string>(1000);
        private FixedSizedQueue<string> glLifeEndQueue = new FixedSizedQueue<string>(1000);
        private FixedSizedQueue<string> glWillLifeEndQueue = new FixedSizedQueue<string>(1000);

        //public static readonly string GLUE_LIFE_TIME_TYPE = "GlueLife";
        //public static readonly string GLUE_REHEAT_TYPE = "GlueReheat";
        private DataTable glueLifeTb = new DataTable();
        string gComputerName;
        string logPath = @"C:\OPI_PIC\Log\";
        bool reheatMode = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            this.Text += " ___ Ver : " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();


        }
        
        private void glueReloadTimerElspsed(object sender, EventArgs e)
        {
            
            LoadAllGlReheatingData();

            if (reheatMode)
                return;

            LoadGlueLifeTimeData();
        }
        
        private void _TimersTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            LoadEqpState();//update eqp state
            //LoadGlueLifeTimeData();
            //Task.Factory.StartNew(LoadGlueLifeTimeData);
            
        }

        private void LoadMaterialState()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string moNo = "";
                if (dvLotNo.DataSource != null)
                {
                    DataTable lotNoTb = (DataTable)dvLotNo.DataSource;
                    if (lotNoTb.Rows.Count > 0)
                        moNo = lotNoTb.Rows[0].Field<string>("MONo");
                }

                DataTable tb = MesWsAutoProxy.LoadEquipmentBlueTape_DBOPI(loginUserLab.Text, gComputerName, moNo);
                dvBlueTape.DataSource = tb;
                txtBlueTapeCount.Text = tb.Rows.Count.ToString();
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


        private void LoadLotState()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                DataTable tb = MesWsAutoProxy.LoadLotState_DBOPI(loginUserLab.Text, gComputerName);

                dvLotNo.DataSource = tb;
                txtLotCount.Text = tb.Rows.Count.ToString();
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

        private void LoadEqpState()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                DataTable tb = MesWsAutoProxy.LoadEquipmentState_DBOPI(loginUserLab.Text, gComputerName);

                lblStatusByEQP.Text = tb.Rows[0].Field<string>("STATENAME");
                lblStatusByEQP.BackColor = Color.FromArgb(Convert.ToInt32(tb.Rows[0]["STATECOLOR"]));
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

        private void btnRefresh1_Click(object sender, EventArgs e)
        {
            LoadLotState();
        }

        private void btnRefresh2_Click(object sender, EventArgs e)
        {
            LoadMaterialState();
            
        }

        private void btnLogon_Click(object sender, EventArgs e)
        {
            CstLogonForm cstLogonForm = new CstLogonForm();
            cstLogonForm.userNo = loginUserLab.Text;
            cstLogonForm.eqpNo = gComputerName;
            cstLogonForm.doGlueVerify = glueVerifyChk.Checked;
            cstLogonForm.ShowDialog(this);
            cstLogonForm.Dispose();

            LoadEqpState();
            LoadLotState();
        }

        private void btnLogOff_Click(object sender, EventArgs e)
        {
            CstLogoffForm cstLogoffForm = new CstLogoffForm();
            cstLogoffForm.userNo = loginUserLab.Text;
            cstLogoffForm.eqpNo = gComputerName;
            DataTable tb = (DataTable)dvLotNo.DataSource;
            if (tb.Rows.Count > 0)
                cstLogoffForm.opNo = tb.Rows[0].Field<string>("OPNo");

            cstLogoffForm.ShowDialog(this);
            cstLogoffForm.Dispose();

            LoadEqpState();
            LoadLotState();
            LoadMaterialState();

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            loginUserLab.Text = loginForm.loginUser;
            if (loginForm.reheatMode)
            {
                SetReHeatMode();
                LoadAllGlReheatingData();
                return;
            }

            loginForm.Dispose();

            
            try
            {
                eqpStateTimer = new System.Timers.Timer();
                eqpStateTimer.Interval = 60000;

                eqpStateTimer.Elapsed += new System.Timers.ElapsedEventHandler(_TimersTimer_Elapsed);
                eqpStateTimer.Stop();

                reheatGrid.AutoGenerateColumns = false;
                glueLifeGrid.AutoGenerateColumns = false;

                //10.234.104.63
                string eqpNo = MesWsAutoProxy.LoadOPIEquipmentNo(DnsUtil.GetLocalIp());
                //string eqpNo = MesWsAutoProxy.LoadOPIEquipmentNo("10.234.104.63");
                if (string.IsNullOrEmpty(eqpNo))
                {
                    MessageBox.Show("無法取得 EQP NO by [" + DnsUtil.GetLocalIp() + "].");
                    this.Close();
                }


                grbContent.Text = eqpNo;
                gComputerName = eqpNo;

                LoadEqpState();

                LoadLotState();

                LoadMaterialState();

                eqpStateTimer.Start();
                //LoadGlueLifeTimeData();
                LoadGlueLifeTimeData();

                LoadAllGlReheatingData();
                eqpStateTimer.Start();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                this.Close();
            }
        }

        private void SetReHeatMode()
        {
            reheatMode = true;
            glLotNoTxt.Enabled = true;
            reheatReloadBtn.Enabled = true;
            celRhBtn.Enabled = true;
            tabControl1.SelectedIndex = 3;

            lblStatusByEQP.Enabled = false;
            glueVerifyChk.Enabled = false;
            btnLogon.Enabled = false;
            btnLogOff.Enabled = false;
            btnTurnEQP.Enabled = false;
            btnMaterialTurnEQP.Enabled = false;
            btnChipScreen.Enabled = false;
            btnLeadFrameScreen.Enabled = false;
            btnChangeState.Enabled = false;


        }

        private void btnMaterialTurnEQP_Click(object sender, EventArgs e)
        {
            MaterialLogonForm matLogonForm = new MaterialLogonForm();
            matLogonForm.userNo = loginUserLab.Text;
            matLogonForm.equipmentNo = gComputerName;
            matLogonForm.isVerifyGlue = glueVerifyChk.Checked;

            matLogonForm.ShowDialog();


            LoadGlueLifeTimeData();

            LoadAllGlReheatingData();

        }

        private void btnTurnEQP_Click(object sender, EventArgs e)
        {
            BlueTapeLogonForm btLogonForm = new BlueTapeLogonForm();
            btLogonForm.userNo = loginUserLab.Text;
            btLogonForm.equipmentNo = gComputerName;

            btLogonForm.ShowDialog();
        }

        private void btnChipScreen_Click(object sender, EventArgs e)
        {
            BlueTapeIDInputForm inputForm = new BlueTapeIDInputForm();
            DialogResult result = inputForm.ShowDialog();
            if (result != DialogResult.OK)
                return;
            Thread.Sleep(1000);
            try
            {
                string btID = inputForm.btTapeID;


                string picName = "";
                string logData = "";
                DataTable tb = null;
                if (dvBlueTape.Rows.Count > 0)
                {
                    tb = (DataTable)dvBlueTape.DataSource;
                    picName = Convert.ToString(tb.Rows[0]["MONO"]) + "_" + btID + "_" + grbContent.Text.Substring(grbContent.Text.Length - 3, 3) + "_" + DateTime.Now.ToString("yyMMddHHmmss") + ".jpg";
                }
                else if (dvLotNo.Rows.Count > 0)
                {
                    tb = (DataTable)dvLotNo.DataSource;
                    string mono = Convert.ToString(tb.Rows[0]["MONO"]);
                    picName = mono + "_" + btID + "_" + grbContent.Text.Substring(grbContent.Text.Length - 3, 3) + "_" + DateTime.Now.ToString("yyMMddHHmmss") + ".jpg";
                    logData = mono + "," + btID;
                }
                else
                {
                    picName = grbContent.Text.Substring(grbContent.Text.Length - 3, 3) + "_" + btID + "_" + DateTime.Now.ToString("yyMMddHHmmss") + ".jpg";

                    logData = "N/A," + btID;
                }
                PrintScreen(Path.Combine("C:/OPI_PIC/", DateTime.Now.ToString("yyyy/MM"), picName));
                
                ExportCsv(Path.Combine(logPath, DateTime.Now.ToString("yyyyMM")) + ".csv", logData);

                MessageBox.Show("Screen monitor successfully!!", "Successfully", MessageBoxButtons.OK);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }


        private void PrintScreen(string saveFilePath)
        {
            string folderName = Path.GetDirectoryName(saveFilePath);
            if (Directory.Exists(folderName) == false)
                Directory.CreateDirectory(folderName);

            if (File.Exists(saveFilePath))
                File.Delete(saveFilePath);

            Bitmap myImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(myImage);
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
            IntPtr dc1 = g.GetHdc();
            g.ReleaseHdc(dc1);
            myImage.Save(saveFilePath);
        }

        private void ExportCsv(string saveFilePath, string content)
        {
            string folderName = Path.GetDirectoryName(saveFilePath);
            if (Directory.Exists(folderName) == false)
                Directory.CreateDirectory(folderName);
            

            using (FileStream fs = new FileStream(saveFilePath, FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(content);
                    sw.Flush();
                }
            }



        }

        private void btnLeadFrameScreen_Click(object sender, EventArgs e)
        {
            try
            {
                string picName;
                DataTable tb = null;
                if (dvBlueTape.Rows.Count > 0)
                {
                    tb = (DataTable)dvBlueTape.DataSource;
                    picName = Convert.ToString(tb.Rows[0]["MONO"]) + "_" + grbContent.Text.Substring(grbContent.Text.Length - 3, 3) + "_" + DateTime.Now.ToString("yyMMddHHmmss") + ".jpg";
                }
                else if (dvLotNo.Rows.Count > 0)
                {
                    tb = (DataTable)dvLotNo.DataSource;
                    picName = Convert.ToString(tb.Rows[0]["MONO"]) + "_" + "_" + grbContent.Text.Substring(grbContent.Text.Length - 3, 3) + "_" + DateTime.Now.ToString("yyMMddHHmmss") + ".jpg";

                }
                else
                {
                    picName = grbContent.Text.Substring(grbContent.Text.Length - 3, 3) + "_" + "_" + DateTime.Now.ToString("yyMMddHHmmss") + ".jpg";
                }
                PrintScreen(Path.Combine("C:/OPI_PIC/", DateTime.Now.ToString("yyyy/MM"), picName));

                MessageBox.Show("Screen monitor successfully!!", "Successfully", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), "Error!");
            }
            
        }

        private void btnChangeState_Click(object sender, EventArgs e)
        {
            EquipmentStateChangeForm stateChgForm = new EquipmentStateChangeForm();
            stateChgForm.eqpState = lblStatusByEQP.Text;
            stateChgForm.equipmentNo = grbContent.Text;
            stateChgForm.userNo = loginUserLab.Text;
            stateChgForm.ShowDialog();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            eqpStateTimer.Stop();
            loginUserLab.Text = "";
            Form1_Shown(sender, e);
        }

        private void glueReheatBtn_Click(object sender, EventArgs e)
        {

        }

        private void glLotNoTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(13))
                return;

            if (string.IsNullOrEmpty(glLotNoTxt.Text.Trim()))
                return;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                WsResponse wsRes = MesWsLextarProxy.AddGlueReheatData(loginUserLab.Text, glLotNoTxt.Text);
                if (wsRes.Result != ResultEnum.Success)
                {
                    MessageBox.Show(wsRes.ReturnMsg);
                    return;
                }


                MessageBox.Show("新增回溫資料完成!");
                glLotNoTxt.Text = "";
                glLotNoTxt.Focus();
                LoadAllGlReheatingData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("新增回溫資料錯誤。" + ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            

        }

        private void LoadAllGlReheatingData()
        {
            if (reheatMode == false && glueVerifyChk.Checked == false)
                return;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                reheatGrid.DataSource = MesWsLextarProxy.LoadAllReheatingData(loginUserLab.Text);
                CheckGlReheatState();
            }
            catch (Exception ex)
            {

                MessageBox.Show("取得回溫資料錯誤。" + ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            
        }

        private void CheckGlReheatState()
        {
            if (reheatMode == false && glueVerifyChk.Checked == false)
                return;

            List<string> reheatDoneList = new List<string>();
            

            foreach (DataGridViewRow row in reheatGrid.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;

                if (Convert.ToDateTime(row.Cells["expTimeCol"].Value) < DateTime.Now)
                {
                    row.DefaultCellStyle.BackColor = ColorPicker.Expired();
                }
                else if (Convert.ToDateTime(row.Cells["reheatEndCol"].Value) > DateTime.Now)
                {
                    row.DefaultCellStyle.BackColor = ColorPicker.ReheatingColor();
                }
                else if (Convert.ToDateTime(row.Cells["reheatEndCol"].Value) <= DateTime.Now && DateTime.Now <= Convert.ToDateTime(row.Cells["expTimeCol"].Value))
                {
                    row.DefaultCellStyle.BackColor = ColorPicker.ReheatDone();
                    //rhMatLotNoCol
                    string matLotNo = Convert.ToString(row.Cells["rhMatLotNoCol"].Value);
                    //防止重複跳出完成回溫的訊息，使用queue 來記錄已有提示過的 Material Lot No
                    if (glReheatDoneQueue.Contains(matLotNo) == false)
                    {
                        glReheatDoneQueue.Enqueue(matLotNo);
                        reheatDoneList.Add(matLotNo);
                    }
                    
                }

            }

            if (reheatDoneList.Count > 0)
            {
                MessageBox.Show("已完成回溫: " + Environment.NewLine + string.Join(Environment.NewLine, reheatDoneList), "完成回溫列表");
            }
        }

        private void celRhBtn_Click(object sender, EventArgs e)
        {
            if (reheatGrid.SelectedRows.Count == 0)
                return;
            DataTable tb = (DataTable)reheatGrid.DataSource;
            string matLotNo = tb.Rows[reheatGrid.SelectedRows[0].Index].Field<string>("MATERIAL_LOT_NO");

            DialogResult dialogResult = MessageBox.Show("是否取消 [" + matLotNo + "] 回溫?", "Question", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    MesWsLextarProxy.DeleteGlueUsedData(loginUserLab.Text, matLotNo);

                    this.Cursor = Cursors.Default;
                    MessageBox.Show("刪除完成。");
                    LoadAllGlReheatingData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Delete Error." + ex.ToString());
                }


                this.Cursor = Cursors.Default;

            }
        }

        private void reheatReloadBtn_Click(object sender, EventArgs e)
        {
            LoadAllGlReheatingData();
        }

        private void reheatGrid_VisibleChanged(object sender, EventArgs e)
        {
            CheckGlReheatState();
        }

        private void LoadGlueLifeTimeData()
        {
            
            if (reheatMode == false && glueVerifyChk.Checked == false)
                return;

            DateTime endTime = DateTime.Now;
            DateTime stTime = endTime.AddMonths(-1);

            glueLifeGrid.DataSource = MesWsLextarProxy.LoadMaterialRecordJoinGlueUsedStateOnEquipment(loginUserLab.Text, gComputerName, stTime, endTime);
            CheckGlLifeTime();
        }

        private void CheckGlLifeTime()
        {
            if (reheatMode == false && glueVerifyChk.Checked == false)
                return;

            List<string> willLifeEndList = new List<string>();
            List<string> lifeEndList = new List<string>();

            foreach (DataGridViewRow row in glueLifeGrid.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
                if (row.Cells["lifeEndCol"].Value == null || row.Cells["lifeEndCol"].Value == DBNull.Value)
                    continue;

                DateTime lifeEndTime = Convert.ToDateTime(row.Cells["lifeEndCol"].Value);
                string matLotNo = Convert.ToString(row.Cells["ltMatLotNoCol"].Value);
                if (lifeEndTime < DateTime.Now)
                {
                    row.DefaultCellStyle.BackColor = ColorPicker.LifeTimeEnd();
                    
                    if (glLifeEndQueue.Contains(matLotNo) == false)
                    {
                        glLifeEndQueue.Enqueue(matLotNo);
                        lifeEndList.Add(matLotNo);
                    }

                }
                else if (lifeEndTime.AddMinutes(-30) < DateTime.Now)
                {
                    //剩30分鐘快到使用期限時，需alarm
                    row.DefaultCellStyle.BackColor = ColorPicker.WillLifeEnd();

                    if (glWillLifeEndQueue.Contains(matLotNo) == false)
                    {
                        glWillLifeEndQueue.Enqueue(matLotNo);
                        willLifeEndList.Add(matLotNo);
                    }
                }
                

            }

            StringBuilder msg = new StringBuilder();
            if (lifeEndList.Count > 0)
            {   
                msg.AppendLine("已過使用期限，請做下機 : ")
                    .AppendLine(string.Join(Environment.NewLine, lifeEndList));
                
                //MessageBox.Show("已到達使用期限: " + Environment.NewLine + string.Join(Environment.NewLine, lifeEndList), "到達使用期限列表");
            }
            msg.AppendLine("----------------------");

            if (willLifeEndList.Count > 0)
            {                
                msg.AppendLine("將要到達使用期限 : ")
                    .AppendLine(string.Join(Environment.NewLine, willLifeEndList));

            }

            if(msg.Length > 0)
                MessageBox.Show(msg.ToString(), "Warning");

        }

        private void glueLifeGrid_VisibleChanged(object sender, EventArgs e)
        {
            CheckGlLifeTime();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reheatMode)
            {
                if (tabControl1.SelectedIndex != 3)
                {
                    MessageBox.Show("目前為回溫模式，不可選擇其它頁面。","Warning");
                    tabControl1.SelectedIndex = 3;
                }
            }
        }

        private void glueVerifyChk_CheckedChanged(object sender, EventArgs e)
        {
            if (reheatMode == false && glueVerifyChk.Checked == false)
                return;
            LoadGlueLifeTimeData();
            LoadAllGlReheatingData();

        }
    }
}
