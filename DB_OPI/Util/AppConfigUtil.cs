using DB_OPI.Proxy;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DB_OPI.Util
{
    class AppConfigUtil
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public static string EqpNo
        { get; set;}
        /// <summary>
        /// 回溫模式
        /// </summary>
        public static bool ReheatMode
        { get; set; }

        /// <summary>
        /// 自動過帳模式
        /// </summary>
        public static bool AutoMode
        { get; set; }

        /// <summary>
        /// 膠才卡控模式
        /// </summary>
        public static bool GlueCtrlMode
        { get; set; }

        /// <summary>
        /// 自動過帳 Cassette 上機 barcode 的 com port
        /// </summary>
        public static string LogonPort
        { get; set; }

        /// <summary>
        /// 自動過帳 Cassette 下機 barcode 的 com port
        /// </summary>
        public static string LogoffPort
        { get; set; }



        public static void LoadApConfig()
        {
            logger.Info("wsAuto Get Ap config by IP : {0}", IP);
            //用機台電腦 IP query config
            DataTable tb = MesWsAutoProxy.GetApConfigByIP(IP);
            if (tb.Rows.Count == 0)
            {
                //IP 抓不到 config 在去 Query DEFAULT 的設定
                logger.Info("wsAuto Get Ap config by IP , result no data , then Get GL_OPI default Ap config");
                tb = MesWsAutoProxy.GetApConfigByApID("DEFAULT");
            }
            else
            {
                string value = Convert.ToString(tb.Rows[0]["AP_ID"]);
                if (value != "DEFAULT")
                    EqpNo = value;
            }

            
            foreach (DataRow row in tb.Rows)
            {
                string key = Convert.ToString(row["CONFIG_KEY"]);
                string value = Convert.ToString(row["CONFIG_VALUE"]);
                logger.Debug("Key : {0} , Value : {1}", key, value);

                if (key == "AUTO_MODE")
                {
                    AutoMode = value.ToUpper() == "TRUE";
                }
                else if (key == "GLUE_CTRL_MODE")
                {
                    GlueCtrlMode = value.ToUpper() == "TRUE";
                }
                else if (key == "REHEAT_MODE")
                {
                    ReheatMode = value.ToUpper() == "TRUE";
                }
                else if (key == "LOGOFF_PORT")
                {
                    LogoffPort = value;
                }
                else if (key == "LOGON_PORT")
                {
                    LogonPort = value;
                }
                

            }


        }


        public static string IP = GetLocalIPAddress();

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public static void UpdateApConfig()
        {
            logger.Info("Update Ap Config.");
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("AUTO_MODE", AutoMode.ToString().ToUpper());
            param.Add("GLUE_CTRL_MODE", GlueCtrlMode.ToString().ToUpper());
            param.Add("REHEAT_MODE", ReheatMode.ToString().ToUpper());
            param.Add("LOGOFF_PORT", LogoffPort);
            param.Add("LOGON_PORT", LogonPort);
            //param.Add("EqpID", EqpNo);
            param.Add("IP", IP);

            foreach (var item in param)
            {
                logger.Debug("Setup => Key : {0}, value : {1}", item.Key, item.Value);
            }

            string[] keys = param.Keys.ToArray();

            string[] values = param.Values.ToArray();

            //string ip = GetLocalIPAddress();
            logger.Debug("Insert Setup to DB.");
            MesWsAutoProxy.DeleteApConfigByIP(IP);
            MesWsAutoProxy.InsertApConfig(AppConfigUtil.IP, AppConfigUtil.EqpNo, keys, values);
        }

    }
}
