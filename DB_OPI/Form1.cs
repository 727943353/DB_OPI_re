using DB_OPI.Forms;
using DB_OPI.Proxy;
using DB_OPI.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DB_OPI
{
    public partial class Form1 : Form
    {
        private System.Timers.Timer eqpStateTimer;

        string gComputerName;
        string logPath = @"C:\OPI_PIC\Log\";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            eqpStateTimer = new System.Timers.Timer();
            eqpStateTimer.Interval = 60000;
            eqpStateTimer.Elapsed += new System.Timers.ElapsedEventHandler(_TimersTimer_Elapsed);
            eqpStateTimer.Stop();
        }

        private void _TimersTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            LoadEqpState();//update eqp state
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
            loginForm.Dispose();

            
            try
            {
                string eqpNo = MesWsAutoProxy.LoadOPIEquipmentNo(DnsUtil.GetLocalIp());
                if (string.IsNullOrEmpty(eqpNo))
                {
                    MessageBox.Show("Can't get the [" + DnsUtil.GetLocalIp() + "] EQP NO.");
                    this.Close();
                }
                grbContent.Text = eqpNo;
                gComputerName = eqpNo;

                LoadEqpState();

                LoadLotState();

                LoadMaterialState();

                eqpStateTimer.Start();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                this.Close();
            }
        }

        private void btnMaterialTurnEQP_Click(object sender, EventArgs e)
        {
            MaterialLogonForm matLogonForm = new MaterialLogonForm();
            matLogonForm.userNo = loginUserLab.Text;
            matLogonForm.equipmentNo = gComputerName;
            matLogonForm.isVerifyGlue = glueVerifyChk.Checked;

            matLogonForm.ShowDialog();

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
    }
}
