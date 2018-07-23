using DB_OPI.Util;
using MesCommonCode.WebService.Msg;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace DB_OPI.Proxy
{
    class MesWsAutoProxy
    {
        static wsAuto.wsWPSystemSoapClient wsWPSystem = new wsAuto.wsWPSystemSoapClient();

        public static bool Login(string userNo, string pwd)
        {
            return wsWPSystem.Login(userNo, pwd);
        }

        public static string LoadOPIEquipmentNo(string localIp)
        {
            StringBuilder xmlStr = new StringBuilder();
            xmlStr.AppendLine("<TX><TX_NAME>LoadOPIEquipmentBasis</TX_NAME>")
                .AppendLine("<TX_ID>" + DateTime.Now.ToString("yyyyMMddhhmmss") + "</TX_ID>")
                .AppendLine("<TIMESTAMP>" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "</TIMESTAMP>")
                .AppendLine("<client_ip>" + localIp + "</client_ip>")
                .AppendLine("</TX>");

            string outXml = wsWPSystem.LoadOPIEquipmentBasis(xmlStr.ToString());
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);

            MesWsUtil.CheckTxSuccess(xmlDoc);

            DataTable tb = MesWsUtil.RetriveDataTable(xmlDoc, "loadopiequipmentbasis");
            if (tb.Rows.Count == 0)
                return "";

            return tb.Rows[0].Field<string>("EquipmentNo");

        }


        public static DataTable LoadEquipmentState_DBOPI(string userNo, string eqpNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string param = XmlGenUtil.CombineXMLParameter("equipmentno", "EquipmentNo", "String", eqpNo, "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, param);

            string outXml = wsWPSystem.LoadEquipmentState_DBOPI(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);

            MesWsUtil.CheckTxSuccess(xmlDoc);

            return MesWsUtil.RetriveDataTable(xmlDoc, "loadequipmentstate_dbopi");

        }

        public static DataTable LoadLotState_DBOPI(string userNo, string eqpNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string param = XmlGenUtil.CombineXMLParameter("equipmentno", "EquipmentNo", "String", eqpNo, "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, param);

            string outXml = wsWPSystem.LoadLotState_DBOPI(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);

            MesWsUtil.CheckTxSuccess(xmlDoc);

            return MesWsUtil.RetriveDataTable(xmlDoc, "loadlotstate_dbopi");
        }

        public static DataTable LoadEquipmentBlueTape_DBOPI(string userNo, string eqpNo, string moNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string param = XmlGenUtil.CombineXMLParameter("equipmentno", "EquipmentNo", "String", eqpNo, "");
            if(!string.IsNullOrEmpty(moNo))
                param += XmlGenUtil.CombineXMLParameter("mono", "MONo", "String", moNo, "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, param);
            string outXml = wsWPSystem.LoadEquipmentBlueTape_DBOPI(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);

            MesWsUtil.CheckTxSuccess(xmlDoc);

            return MesWsUtil.RetriveDataTable(xmlDoc, "loadequipmentbluetape_dbopi");


        }


        public static bool CheckInFunction_Cassette(string userNo, string inCassette, string outCassette, string eqpNo, string opNo,ref string msg )
        {

            
            return wsWPSystem.CheckInFunction_Cassette(inCassette, outCassette, eqpNo, opNo, userNo, ref msg) == "Y";
        }


        public static bool CheckOutFunction_DB(string eqpNo, string userNo, string cassetteNo, string opNo, DataRow[] errorRows, string lotRecord, ref string msg)
        {
            //For i = 0 To drSel.Length - 1
            //    strError += CombineXMLValueTag(_
            //               CombineXMLValue("errorno", CInput(drSel(i)("ErrorNo"))) & _
            //               CombineXMLValue("errorqty", drSel(i)("ErrorQty")) & _
            //               CombineXMLValue("errorlevel", drSel(i)("ReasonLevel")))
            //Next
            string errorXml = "";
            foreach (DataRow row in errorRows)
            {
                errorXml += XmlGenUtil.CombineXMLValueTag(XmlGenUtil.CombineXMLValue("errorno", row["ErrorNo"].ToString()) +
                    XmlGenUtil.CombineXMLValue("errorqty", row["ErrorQty"].ToString()) +
                    XmlGenUtil.CombineXMLValue("errorlevel", row["ReasonLevel"].ToString()));

            }

            return wsWPSystem.CheckOutFunction_DB(cassetteNo, eqpNo, opNo, errorXml, lotRecord, userNo, ref msg) == "Y";
        }

        public static void MaterialLogoff(string userNo, string eqpNo, string matLotNo, DateTime logonStTime, DateTime logonEndTime)
        {
            string result = wsWPSystem.MaterialLogoff(userNo, eqpNo, matLotNo, logonStTime.ToString("yyyy-MM-dd HH:mm:ss"), logonEndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            WsResponse wsRes = JsonConvert.DeserializeObject<WsResponse>(result);
            if (wsRes.Result == ResultEnum.Exception)
                throw new Exception(wsRes.Exception.Stack);

        }

        public static DataTable GetApConfigByIP(string ip)
        {
            string result = wsWPSystem.LoadApConfigByKeyValue("DB_OPI", "DB_OPI", "IP", ip);

            WsResponse wsRes = JsonConvert.DeserializeObject<WsResponse>(result);
            if (wsRes.Result == ResultEnum.Exception)
                throw new Exception("LoadApConfigByIP fail." + wsRes.Exception.ToString());

            return wsRes.ReturnTable;
        }

        public static DataTable GetApConfigByApID(string apID)
        {
            string result = wsWPSystem.LoadApConfig("DB_OPI", "DB_OPI", apID);
            WsResponse wsRes = JsonConvert.DeserializeObject<WsResponse>(result);
            if (wsRes.Result == ResultEnum.Exception)
                throw new Exception("LoadApConfig fail." + wsRes.Exception.ToString());

            return wsRes.ReturnTable;
        }

        public static bool DeleteApConfigByIP(string ip)
        {
            string result = wsWPSystem.DeleteApConfigByKeyValue("DB_OPI", "DB_OPI", "IP", ip);
            WsResponse wsRes = JsonConvert.DeserializeObject<WsResponse>(result);
            if (wsRes.Result == ResultEnum.Exception)
                throw new Exception("DeleteApConfigByIP fail." + wsRes.Exception.Stack);

            if (wsRes.Result == ResultEnum.Success)
                return true;

            return false;
        }

        public static bool InsertApConfig(string ip, string apID, string[] keys, string[] values)
        {
            string result = wsWPSystem.InsertApConfig("DB_OPI", "DB_OPI", apID, keys, values);
            WsResponse wsRes = JsonConvert.DeserializeObject<WsResponse>(result);
            if (wsRes.Result == ResultEnum.Exception)
                throw new Exception("InsertApConfig fail." + wsRes.Exception.Stack);

            if (wsRes.Result == ResultEnum.Success)
                return true;

            return false;

        }

        //public static DataTable LoadMaterialRecordJoinMaterialUsedStateJson(string eqpNo, string userNo, string type, DateTime logonStTime, DateTime lognEndTime)
        //{
        //    string result = wsWPSystem.LoadMaterialRecordJoinMaterialUsedStateJson(userNo, eqpNo, type, logonStTime.ToString("yyyy-MM-dd HH:mm:ss"), lognEndTime.ToString("yyyy-MM-dd HH:mm:ss"));
        //    WsResponse wsRes = JsonConvert.DeserializeObject<WsResponse>(result);
        //    if (wsRes.Result == ResultEnum.Exception)
        //    {
        //        throw new Exception("LoadMaterialRecordJoinMaterialUsedStateJson error." + wsRes.Exception.Stack);
        //    }

        //    return wsRes.ReturnTable;
        //}

    }
}
