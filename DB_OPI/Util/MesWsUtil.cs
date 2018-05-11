using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace DB_OPI.Util
{
    class MesWsUtil
    {
        public static DataTable RetriveDataTable(XmlDocument xmlDoc, string tagName)
        {
            DataSet ds = new DataSet();
            string xmlSchema = xmlDoc.DocumentElement.GetElementsByTagName(tagName).Item(0).SelectNodes("schema").Item(0).InnerXml;
            if (string.IsNullOrEmpty(xmlSchema))
                return null;

            using (StringReader tmpStringReader = new StringReader(xmlSchema))
            {
                ds.ReadXmlSchema(tmpStringReader);
            }

            string xmlData = xmlDoc.DocumentElement.GetElementsByTagName(tagName).Item(0).SelectNodes("value").Item(0).InnerXml;
            using (StringReader tmpStringReader = new StringReader(xmlData))
            {
                ds.ReadXml(tmpStringReader);
            }

            return ds.Tables[0];
        }

        public static void CheckTxSuccess(XmlDocument xmlDoc)
        {

            if (xmlDoc.DocumentElement["result"].InnerXml != "success")
            {
                StringBuilder errStr = new StringBuilder();
                errStr.AppendLine(xmlDoc.DocumentElement.GetElementsByTagName("code").Item(0).InnerText)
                    .AppendLine(xmlDoc.DocumentElement.GetElementsByTagName("sysmsg").Item(0).InnerText)
                    .AppendLine(xmlDoc.DocumentElement.GetElementsByTagName("mesmsg").Item(0).InnerText);

                throw new Exception(errStr.ToString());

            }

        }

    }
}
