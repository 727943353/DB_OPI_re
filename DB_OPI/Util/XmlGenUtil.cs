using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB_OPI.Util
{
    class XmlGenUtil
    {
        public static string CombineXMLIdentity(string computerName, string userNo)
        {
            StringBuilder xml = new StringBuilder();
            xml.AppendLine("<computername>" + computerName + "</computername>")
                .AppendLine("<curuserno>" + userNo + "</curuserno>")
                .AppendLine("<sendtime>" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "</sendtime>");

            return xml.ToString();
        }

        public static string CombineXMLParameter(string tagName, string name, string type, string value, string desc)
        {
            StringBuilder xml = new StringBuilder();
            xml.AppendLine("<"+ tagName.ToLower() + ">")
                .AppendLine("<name>" + name + "</name>")
                .AppendLine("<type>" + type + "</type>")
                .AppendLine("<value>" + value + "</value>")
                .AppendLine("<desc>" + desc + "</desc>")
                .AppendLine("</" + tagName.ToLower() + ">");

            return xml.ToString();
        }

        public static string CombineXMLRequest(string strIdentity, string param)
        {
            StringBuilder xml = new StringBuilder();
            xml.AppendLine("<request>")
                .AppendLine("<identity>" + strIdentity + "</identity>")
                .AppendLine("<parameter>" + param + "</parameter>")
                .AppendLine("</request>");

            return xml.ToString();
        }

        public static string CombineXMLValueTag(string value)
        {
            return "<value>" + value + "</value>";
        }

        public static string CombineXMLValue(string tagName, string value)
        {
            return "<" + tagName + ">" + value + "</" + tagName + ">";
        }

        public static string CombineXMLParameterMultiValue(string valueName, string name, string type, string value, string desc)
        {
            return "<" + valueName.ToLower() + ">" +
                "<name>" + name + "</name>" +
                "<type>" + type + "</type>" + value +
                "<desc>" + desc + "</desc>" +
                "</" + valueName.ToLower() + ">";
        }
    }
}
