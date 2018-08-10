using BarCodeReader.BarCodeReaders;
using BarCodeReader.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DB_OPI.Forms
{
    public partial class ComPortTestForm : Form
    {
        private ILogger logger = LogManager.GetCurrentClassLogger();

        DataTable tb = new DataTable();

        Dictionary<string, IBarCodeReader> barcodeMap = new Dictionary<string, IBarCodeReader>();
        public ComPortTestForm()
        {
            InitializeComponent();
        }

        private void ComPortTestForm_Load(object sender, EventArgs e)
        {
#if DEBUG
            this.TopMost = false;

#endif
            string[] comPorts = SerialPort.GetPortNames();
            InitGrid(comPorts);
            testGrid.DataSource = tb;

            InitBarCodeReader(comPorts);

        }

        private void InitBarCodeReader(string[] comPorts)
        {
            IBarCodeReader logonBarCodeReader;
            SerialPort objSerialPort1;
            foreach (string comPort in comPorts)
            {
                try
                {
                    objSerialPort1 = new SerialPort(comPort, 9600, Parity.None, 8, StopBits.One);
                    if (objSerialPort1.IsOpen)
                        continue;

                    objSerialPort1.ReceivedBytesThreshold = 1;

                    objSerialPort1.Open();
                    objSerialPort1.DiscardInBuffer();


                    logonBarCodeReader = new FixedBarCode(objSerialPort1);
                    logonBarCodeReader.PinStableIntervel = 300;
                    logonBarCodeReader.SerialPin = SerialPinChange.DsrChanged | SerialPinChange.CtsChanged | SerialPinChange.CDChanged | SerialPinChange.Ring;
                    logonBarCodeReader.PinChangedEvent += new BarCodeReader.Interfaces.PinChangedEventHandler(LogonComportPinChangeEventHandler);
                    logonBarCodeReader.DataRecivedEvent += new DataRecivedEventHandler(LogonComportDataRecivedEventHandler);
                    logonBarCodeReader.Enable();
                    

                    barcodeMap.Add(comPort, logonBarCodeReader);
                }
                catch (Exception)
                {
                    
                }
                
            }
        }

        /// <summary>
        /// listen logon com port ping change event. 
        /// sensor trigger
        /// </summary>
        /// <param name="pinState"></param>
        private void LogonComportPinChangeEventHandler(IBarCodeReader logonBarCodeReader, SerialPinState pinState)
        {
            logger.Info("Test Mode => ===== LogonComportPinChangeEventHandler start =====");
            logger.Debug("Test Mode => Logon Comport {0} , ping change , CD (Cassette) : {1}, CTS (LF) : {2}", logonBarCodeReader.TargetPort.PortName, pinState.CD, pinState.CTS);
            lock (tb)
            {
                var selRow = (from row in tb.AsEnumerable()
                              where row.Field<string>("Port No") == logonBarCodeReader.TargetPort.PortName
                              select row).Single();

                selRow["DRS"] = pinState.DSR ? "ON" : "OFF";
                selRow["CTS"] = pinState.CTS ? "ON" : "OFF";
                selRow["CD"] = pinState.CD ? "ON" : "OFF";
            }
            

            logonBarCodeReader.OpenReader();
            logger.Info("Test Mode => ===== LogonComportPinChangeEventHandler end =====");
        }

        /// <summary>
        /// logon com port reader to read bar code of cassette
        /// </summary>
        /// <param name="data"></param>
        private void LogonComportDataRecivedEventHandler(IBarCodeReader logonBarCodeReader, string data)
        {
            logger.Info("Test Mode => ===== LogonComportDataRecivedEventHandler start =====");
            logger.Debug("Test Mode => Logon Comport {0} , Recived data : [{1}]", logonBarCodeReader.TargetPort.PortName, data);
            lock (tb)
            {
                foreach (DataRow row in tb.Rows)
                {
                    if (row.Field<string>("Port No") == logonBarCodeReader.TargetPort.PortName)
                    {
                        row["BarCode Reader"] = data;
                    }
                    else
                    {
                        row["BarCode Reader"] = "";
                    }
                }
                
            }
            logger.Info("Test Mode => ===== LogonComportDataRecivedEventHandler end =====");
            
        }

        private void InitGrid(string[] comPorts)
        {
            tb.Columns.Add("Port No");
            tb.Columns.Add("BarCode Reader");
            tb.Columns.Add("DRS");
            tb.Columns.Add("CTS");
            tb.Columns.Add("CD");

            
            DataRow newRow;
            foreach (string comPort in comPorts)
            {
                newRow = tb.NewRow();
                newRow["Port No"] = comPort;
                newRow["DRS"] = "OFF";
                newRow["CTS"] = "OFF";
                newRow["CD"] = "OFF";

                tb.Rows.Add(newRow);
            }
        }

        private void ComPortTestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (IBarCodeReader logonBarCodeReader in barcodeMap.Values)
            {
                try
                {
                    logonBarCodeReader.TargetPort.Close();
                }
                catch (Exception)
                {   
                }
                
            }
        }
    }
}
