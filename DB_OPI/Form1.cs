using BarCodeReader.BarCodeReaders;
using BarCodeReader.Interfaces;
using DB_OPI.Forms;
using DB_OPI.Proxy;
using DB_OPI.Queues;
using DB_OPI.Util;
using MesCommonCode.WebService.Msg;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_OPI
{
    public partial class Form1 : Form
    {
        private ILogger logger = LogManager.GetCurrentClassLogger();
        private System.Timers.Timer eqpStateTimer;
        private FixedSizedQueue<string> glReheatDoneQueue = new FixedSizedQueue<string>(1000);
        private FixedSizedQueue<string> glLifeEndQueue = new FixedSizedQueue<string>(1000);
        private FixedSizedQueue<string> glWillLifeEndQueue = new FixedSizedQueue<string>(1000);

        //public static readonly string GLUE_LIFE_TIME_TYPE = "GlueLife";
        //public static readonly string GLUE_REHEAT_TYPE = "GlueReheat";
        private string userNo;
        private string pwd;

        private DataTable glueLifeTb = new DataTable();
        string gComputerName;
        string logPath = @"C:\OPI_PIC\Log\";
        //bool reheatMode = false;
        //bool glueCtrlEnabled = false;
        bool isLogonCstReady = false;
        bool isLogoffCstReady = false;
        bool isLotLogOned = false;

        string autoLogonCst = "";
        string autoLogoffCst = "";

        private IBarCodeReader logonBarCodeReader;
        private IBarCodeReader logoffBarCodeReader;

        CstLogonForm cstLogonForm = null;
        CstLogoffForm cstLogoffForm = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
#if DEBUG
            this.TopMost = false;

#endif

            //ap main method : Form1_Shown
            this.Text += " ___ Ver : " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            logger.Info("<<<<< AP Start >>>>>");
            reheatGrid.AutoGenerateColumns = false;


        }

        private void glueReloadTimerElspsed(object sender, EventArgs e)
        {
            
            LoadAllGlReheatingData();

            if (AppConfigUtil.ReheatMode)
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
            if (cstLogonForm != null)
            {
                cstLogonForm.Close();
                cstLogonForm.Dispose();
                cstLogonForm = null;
            }

            cstLogonForm = new CstLogonForm();
            cstLogonForm.Owner = this;
            //cstLogonForm.eqpNo = gComputerName;
            //cstLogonForm.doGlueVerify = AppConfigUtil.GlueCtrlMode;
            cstLogonForm.ShowDialog(this);
            cstLogonForm.Dispose();
            cstLogonForm = null;

            LoadEqpState();
            LoadLotState();
        }

        private void btnLogOff_Click(object sender, EventArgs e)
        {
            if (cstLogoffForm != null)
            {
                cstLogoffForm.Close();
                cstLogoffForm.Dispose();
                cstLogoffForm = null;
            }
            cstLogoffForm = new CstLogoffForm();
            //cstLogoffForm.userNo = loginUserLab.Text;
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
            userNo = loginForm.loginUser;
            pwd = loginForm.pwd;
            if (AppConfigUtil.ReheatMode)
            {
                SetReHeatMode();
                LoadAllGlReheatingData();
                loginForm.Dispose();
                return;
            }

            
            if (AppConfigUtil.GlueCtrlMode)
            {
                glueCtrlStateLab.Text = "膠材卡控 : Enabled";
                glueCtrlStateLab.ForeColor = Color.ForestGreen;
            }
            else
            {
                glueCtrlStateLab.Text = "膠材卡控 : Disabled";
            }
            
            loginForm.Dispose();

            if (AppConfigUtil.AutoMode)
            {
                autoModeLab.Text = "AutoMode : Enabled";
                autoModeLab.ForeColor = Color.Green;
                logonPortLab.Text = "Logon : " + AppConfigUtil.LogonPort;
                logoffPortLab.Text = "Logoff : " + AppConfigUtil.LogoffPort;

                InitLogonBarcode();
                
                InitLogoffBarcode();
                

            }
            



            try
            {
                eqpStateTimer = new System.Timers.Timer();
                eqpStateTimer.Interval = 60000;

                eqpStateTimer.Elapsed += new System.Timers.ElapsedEventHandler(_TimersTimer_Elapsed);
                eqpStateTimer.Stop();

                reheatGrid.AutoGenerateColumns = false;
                glueLifeGrid.AutoGenerateColumns = false;

                //10.234.104.63
                //string eqpNo = MesWsAutoProxy.LoadOPIEquipmentNo(DnsUtil.GetLocalIp());
                
                //string setEqpNo = ConfigurationManager.AppSettings["EqpNo"];

                //if (string.IsNullOrEmpty(setEqpNo) == false)
                //{
                //    eqpNo = setEqpNo;
                //}
                

                //string eqpNo = MesWsAutoProxy.LoadOPIEquipmentNo("10.234.104.63");
                //if (string.IsNullOrEmpty(eqpNo))
                //{
                //    MessageBox.Show("無法取得 EQP NO by [" + DnsUtil.GetLocalIp() + "].");
                //    this.Close();
                //}


                grbContent.Text = AppConfigUtil.EqpNo;
                gComputerName = AppConfigUtil.EqpNo;

                LoadEqpState();

                LoadLotState();

                LoadMaterialState();                

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

        /// <summary>
        /// Initial logon com port bar code reader.
        /// </summary>
        private void InitLogonBarcode()
        {
            logger.Info("===== Init LogOn Barcode start =====");
            logger.Info("LogOn port : " + AppConfigUtil.LogonPort);
            try
            {
                SerialPort objSerialPort1 = new SerialPort(AppConfigUtil.LogonPort, 9600, Parity.None, 8, StopBits.One);
                objSerialPort1.ReceivedBytesThreshold = 1;
                objSerialPort1.Open();
                objSerialPort1.DiscardInBuffer();


                logonBarCodeReader = new FixedBarCode(objSerialPort1);
                logonBarCodeReader.PinStableIntervel = 300;
                logonBarCodeReader.SerialPin = SerialPinChange.CDChanged;
                logonBarCodeReader.PinChangedEvent += new BarCodeReader.Interfaces.PinChangedEventHandler(LogonComportPinChangeEventHandler);
                logonBarCodeReader.DataRecivedEvent += new DataRecivedEventHandler(LogonComportDataRecivedEventHandler);

                logonBarCodeReader.Enable();

                //logonBarCodeReader.SerialPin = SerialPinChange.DsrChanged | SerialPinChange.CtsChanged | SerialPinChange.CDChanged | SerialPinChange.Ring;
                
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Init LogOn Barcode error.");
                throw new Exception("Init LogOn Barcode [" + AppConfigUtil.LogonPort + "] Error", ex);
            }
            
            logger.Info("===== Init LogOn Barcode end =====");
        }

        /// <summary>
        /// listen logon com port ping change event. 
        /// sensor trigger
        /// </summary>
        /// <param name="pinState"></param>
        private void LogonComportPinChangeEventHandler(IBarCodeReader barCodeReader, SerialPinState pinState)
        {
            try
            {
                logger.Info("Auto Mode => ===== LogonComportPinChangeEventHandler start =====");
                logger.Debug("Auto Mode => Logon Comport {0} , ping change , CD (Cassette) : {1}, CTS (LF) : {2}", barCodeReader.TargetPort.PortName, pinState.CD, pinState.CTS);
                
                if (pinState.CD)
                {
                    
                    logger.Info("Auto Mode => Logon Com port : {0} Open Reader", barCodeReader.TargetPort.PortName);
                    barCodeReader.OpenReader();
                }
                else
                {
                    isLogonCstReady = false;
                    isLotLogOned = false;
                    logonPortLab.ForeColor = Color.Black;
                    autoLogonCst = "";
                    logonPortLab.Text = "Logon : " + barCodeReader.TargetPort.PortName;
                }
                logger.Info("Auto Mode => ===== LogonComportPinChangeEventHandler end =====");
            }
            catch (Exception ex)
            {
                logger.Error(ex, barCodeReader.TargetPort.PortName + " : LogonComportPinChangeEventHandler error");
                MessageBox.Show("Com port [" + barCodeReader.TargetPort.PortName + "] Pin Change event error . " + ex.ToString());
            }
            
        }

        /// <summary>
        /// logon com port reader to read bar code of cassette
        /// </summary>
        /// <param name="data"></param>
        private void LogonComportDataRecivedEventHandler(IBarCodeReader barCodeReader, string data)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    //在擁有控制項基礎視窗控制代碼的執行緒上執行委派。
                    this.Invoke(new DataRecivedEventHandler(LogonComportDataRecivedEventHandler), new object[] { barCodeReader, data });
                }
                else //已經在UI執行緒
                {
                    logger.Info("Auto Mode => ===== LogonComportDataRecivedEventHandler start =====");
                    logger.Debug("Auto Mode => Logon Comport {0} , Recived data : [{1}]", barCodeReader.TargetPort.PortName, data);
                    autoLogonCst = data.Trim();
                    if (string.IsNullOrEmpty(autoLogonCst))
                    {
                        isLogonCstReady = false;
                        string err = "Auto Mode => Logon Comport " + barCodeReader.TargetPort.PortName + " 無法讀取 Cassette NO，請手動上機。";
                        logger.Warn(err);
                        MessageBox.Show(err, "Warning");

                        return;
                    }

                    isLogonCstReady = true;
                    logger.Debug("Auto Mode => Logon CST ready : {0}, Logoff CST ready : {1}", isLogonCstReady, isLogoffCstReady);
                    logonPortLab.ForeColor = Color.Green;
                    logonPortLab.Text = "Logon : " + autoLogonCst;
                    if (isLogonCstReady && isLogoffCstReady)
                    {
                        if (isLotLogOned)
                            return;
                        isLotLogOned = true;
                        DoAutoLogonCst();
                        
                    }

                    logger.Info("Auto Mode => ===== LogonComportDataRecivedEventHandler end =====");
                }
            }
            catch (Exception ex)
            {

                logger.Error(ex, barCodeReader.TargetPort.PortName + " : LogonComportDataRecivedEventHandler error");
                MessageBox.Show("Com port [" + barCodeReader.TargetPort.PortName + "] reader data error . " + ex.ToString());
            }
            

            
        }

        private void InitLogoffBarcode()
        {

            logger.Info("===== Init LogOff Barcode start =====");
            logger.Info("LogOff port : " + AppConfigUtil.LogoffPort);
            try
            {
                SerialPort objSerialPort1 = new SerialPort(AppConfigUtil.LogoffPort, 9600, Parity.None, 8, StopBits.One);
                objSerialPort1.ReceivedBytesThreshold = 1;
                objSerialPort1.Open();
                objSerialPort1.DiscardInBuffer();


                logoffBarCodeReader = new FixedBarCode(objSerialPort1);
                logoffBarCodeReader.PinStableIntervel = 300;
                logoffBarCodeReader.SerialPin = SerialPinChange.CDChanged;
                logoffBarCodeReader.PinChangedEvent += new BarCodeReader.Interfaces.PinChangedEventHandler(LogoffComportPinChangeEventHandler);
                logoffBarCodeReader.DataRecivedEvent += new DataRecivedEventHandler(LogoffComportDataRecivedEventHandler);

                logoffBarCodeReader.Enable();

                //logoffBarCodeReader.SerialPin = SerialPinChange.DsrChanged | SerialPinChange.CtsChanged | SerialPinChange.CDChanged | SerialPinChange.Ring;
                
            }
            catch (Exception ex)
            {
                string errMsg = "Init LogOff Barcode [" + AppConfigUtil.LogoffPort + "] error";
                logger.Error(ex, errMsg);
                throw new Exception(errMsg, ex);
            }
            

            logger.Info("===== Init LogOff Barcode end =====");
        }

        /// <summary>
        /// listen logoff com port ping change event. 
        /// sensor trigger
        /// </summary>
        /// <param name="pinState"></param>
        private void LogoffComportPinChangeEventHandler(IBarCodeReader barCodeReader, SerialPinState pinState)
        {
            logoffPortLab.ForeColor = Color.Black;

            if (this.InvokeRequired)
            {
                
                //在擁有控制項基礎視窗控制代碼的執行緒上執行委派。
                this.Invoke(new BarCodeReader.Interfaces.PinChangedEventHandler(LogoffComportPinChangeEventHandler), new object[] { barCodeReader, pinState });
            }
            else //已經在UI執行緒
            {
                logger.Debug("Auto Mode => ===== LogoffComportPinChangeEventHandler start =====");
                logger.Debug("Auto Mode => Logoff Comport {0} , ping change , CD (Cassette) : {1}, CTS (LF) : {2}", barCodeReader.TargetPort.PortName, pinState.CD, pinState.CTS);
                if (pinState.CD)
                {
                    //logoffCstReady = pinState.CD;
                    logger.Info("Auto Mode => Logoff Com port : {0} Open Reader", barCodeReader.TargetPort.PortName);
                    logoffBarCodeReader.OpenReader();
                }
                else
                {
                    isLotLogOned = false;
                    if (isLogoffCstReady)
                    {
                        isLogoffCstReady = false;
                        if (string.IsNullOrEmpty(autoLogoffCst))
                        {
                            string err = "Auto Mode => Logoff Comport " + barCodeReader.TargetPort.PortName + " 無法讀取 Cassette NO，請手動下機。";
                            logger.Warn(err);
                            MessageBox.Show(err, "Warning");
                            return;
                        }

                        //log off cassette
                        if (cstLogonForm != null)
                        {
                            cstLogonForm.Close();
                            cstLogonForm.Dispose();
                            cstLogonForm = null;
                        }

                        if (cstLogoffForm != null)
                        {
                            cstLogoffForm.Close();
                            cstLogoffForm.Dispose();
                            cstLogoffForm = null;
                        }

                        cstLogoffForm = new CstLogoffForm();
                        cstLogoffForm.SetAutoLogoffData(autoLogoffCst, userNo, pwd);
                        autoLogoffCst = "";
                        cstLogoffForm.Show();
                        logger.Info("Auto Mode => Open CstLogoffForm");
                        if (cstLogoffForm.LoadLotInfo() == true)
                        {
                            logger.Info("Auto Mode => CstLogoffForm.DoLogoffCst");
                            cstLogoffForm.DoLogoffCst();
                        }


                    }//if (logoffCstReady)

                    
                    logoffPortLab.Text = "Logoff : " + barCodeReader.TargetPort.PortName;
                }//if (pinState.CD)
                logger.Debug("Auto Mode => ===== LogoffComportPinChangeEventHandler end =====");

            }//if (this.InvokeRequired)


        }

        /// <summary>
        /// logoff com port reader to read bar code of cassette
        /// </summary>
        /// <param name="data"></param>
        private void LogoffComportDataRecivedEventHandler(IBarCodeReader barCodeReader, string data)
        {
            if (this.InvokeRequired)
            {

                //在擁有控制項基礎視窗控制代碼的執行緒上執行委派。
                this.Invoke(new DataRecivedEventHandler(LogoffComportDataRecivedEventHandler), new object[] { barCodeReader, data });
            }
            else //已經在UI執行緒
            {
                //logger.Debug("Auto Mode => ===== LogoffComportDataRecivedEventHandler start =====");

                //logger.Info("Auto Mode => Logoff Com port {0} , Recived data : [{1}]", barCodeReader.TargetPort.PortName, data);
                //autoLogoffCst = data.Trim();
                //logoffPortLab.ForeColor = Color.Green;
                //logoffPortLab.Text = "Logoff : " + autoLogoffCst;

                //logger.Debug("Auto Mode => ===== LogoffComportDataRecivedEventHandler end =====");

                logger.Debug("Auto Mode => ===== LogoffComportDataRecivedEventHandler start =====");

                logger.Info("Auto Mode => Logoff Com port {0} , Recived data : [{1}]", barCodeReader.TargetPort.PortName, data);
                autoLogoffCst = data.Trim();
                if (string.IsNullOrEmpty(autoLogoffCst))
                {
                    isLogoffCstReady = false;
                    string err = "Auto Mode => LogOff Comport " + barCodeReader.TargetPort.PortName + " 無法讀取 Cassette No，請手動上機。";
                    logger.Warn(err);
                    MessageBox.Show(err, "Warning");

                    return;
                }

                isLogoffCstReady = true;
                logoffPortLab.ForeColor = Color.Green;
                logoffPortLab.Text = "Logoff : " + autoLogoffCst;
                if (isLogonCstReady && isLogoffCstReady)
                {

                    if (isLotLogOned)
                        return;
                    isLotLogOned = true;
                    DoAutoLogonCst();
                    
                }

                logger.Debug("Auto Mode => ===== LogoffComportDataRecivedEventHandler end =====");

            }
            
        }


        private void DoAutoLogonCst()
        {
            logger.Debug("Auto Mode => ===== Auto Logon CST Start =====");
            try
            {
                if (cstLogonForm != null)
                {
                    cstLogonForm.Close();
                    cstLogonForm.Dispose();
                    cstLogonForm = null;
                }

                if (cstLogoffForm != null)
                {
                    cstLogoffForm.Close();
                    cstLogoffForm.Dispose();
                    cstLogoffForm = null;
                }

                logger.Info("Auto Mode => Open CstLogonForm");
                cstLogonForm = new CstLogonForm();
                cstLogonForm.Show();

                cstLogonForm.SetAutoLogonData(autoLogonCst, autoLogoffCst, userNo, pwd);

                logger.Info("Auto Mode => CstLogonForm do logon cst");
                cstLogonForm.btnConfirm_Click(null, null);
            }
            catch (Exception ex)
            {

                logger.Error(ex, "Auto Logon CST error.");
                MessageBox.Show("Auto Logon CST error. " + ex.ToString());
            }
            

            logger.Debug("Auto Mode => ===== Auto Logon CST End =====");
        }

        private void SetReHeatMode()
        {
            
            glLotNoTxt.Enabled = true;
            reheatReloadBtn.Enabled = true;
            celRhBtn.Enabled = true;
            tabControl1.SelectedIndex = 3;

            lblStatusByEQP.Enabled = false;
            
            btnLogon.Enabled = false;
            btnLogOff.Enabled = false;
            btnTurnEQP.Enabled = false;
            btnMaterialTurnEQP.Enabled = false;
            btnChipScreen.Enabled = false;
            btnLeadFrameScreen.Enabled = false;
            btnChangeState.Enabled = false;

            glueCtrlStateLab.Text = "回溫模式";
        }

        private void btnMaterialTurnEQP_Click(object sender, EventArgs e)
        {
            MaterialLogonForm matLogonForm = new MaterialLogonForm();
            matLogonForm.userNo = loginUserLab.Text;
            matLogonForm.equipmentNo = gComputerName;
            matLogonForm.isVerifyGlue = AppConfigUtil.GlueCtrlMode;

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
            //stateChgForm.userNo = loginUserLab.Text;
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

            string glLotNo = glLotNoTxt.Text.Trim();
            if (string.IsNullOrEmpty(glLotNo))
                return;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                WsResponse wsRes = MesWsLextarProxy.AddGlueReheatData(loginUserLab.Text, glLotNo);
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
            
            if (AppConfigUtil.ReheatMode == false && AppConfigUtil.GlueCtrlMode == false)
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
            if (AppConfigUtil.ReheatMode == false && AppConfigUtil.GlueCtrlMode == false)
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
            
            if (AppConfigUtil.ReheatMode == false && AppConfigUtil.GlueCtrlMode == false)
                return;
                        
            glueLifeGrid.DataSource = MesWsLextarProxy.LoadMaterialRecordJoinGlueUsedStateOnEquipment(loginUserLab.Text, gComputerName);
            CheckGlLifeTime();
        }

        private void CheckGlLifeTime()
        {
            if (AppConfigUtil.ReheatMode == false && AppConfigUtil.GlueCtrlMode == false)
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
            

            if (willLifeEndList.Count > 0)
            {
                msg.AppendLine("----------------------");
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
            if (AppConfigUtil.ReheatMode)
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
            if (AppConfigUtil.ReheatMode == false && AppConfigUtil.GlueCtrlMode == false)
                return;
            LoadGlueLifeTimeData();
            LoadAllGlReheatingData();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            logger.Info("===== Form1_FormClosing start =====");
            if (logonBarCodeReader != null)
            {
                try
                {
                    logonBarCodeReader.TargetPort.Close();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "logonBarCodeReader comport close error.");
                    
                }
            }

            if (logoffBarCodeReader != null)
            {
                try
                {
                    logoffBarCodeReader.TargetPort.Close();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "logoffBarCodeReader comport close error.");

                }
            }

            logger.Info("===== Form1_FormClosing end =====");
        }
    }
}
