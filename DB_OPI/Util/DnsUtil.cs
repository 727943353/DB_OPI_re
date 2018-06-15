using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DB_OPI.Util
{
    class DnsUtil
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static string localIp;
        public static string GetLocalIp()
        {
            if (!string.IsNullOrEmpty(localIp))
                return localIp;

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIp = ip.ToString();
                    logger.Info("Get ap IP : {0}", localIp);
                    
                    return localIp;
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
