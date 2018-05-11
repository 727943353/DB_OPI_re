using DB_OPI.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace DB_OPI.Proxy
{

    class MesWsProxy
    {
        static wsWIP.wsWIPSoapClient wsWip = new wsWIP.wsWIPSoapClient();
        static wsOP.wsOPSoapClient wsOp = new wsOP.wsOPSoapClient();
        static wsEMS.wsEMSSoapClient wsEMS = new DB_OPI.wsEMS.wsEMSSoapClient();
        static wsEQP.wsEQPSoapClient wsEQP = new DB_OPI.wsEQP.wsEQPSoapClient();
        static wsQC.wsQCSoapClient wsQC = new DB_OPI.wsQC.wsQCSoapClient();

        public static DataTable LoadTemp_EquipmentLot(string computerName, string userNo, string lotNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(computerName, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("lotno", "LotNo", "String", lotNo, "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);

            string outXml = wsWip.LoadTemp_EquipmentLot(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);

            CheckTxSuccess(xmlDoc);

            return RetriveDataTable(xmlDoc, "loadtemp_equipmentlot");

        }

        public static DataTable LoadOPErrorJoinBasis(string computerName, string userNo, string opNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(computerName, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("opno", "OPNo", "String", opNo, "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);

            string outXml = wsOp.LoadOPErrorJoinBasis(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);

            CheckTxSuccess(xmlDoc);

            return RetriveDataTable(xmlDoc, "loadoperrorjoinbasis");

        }

        public static DataTable LoadTemp_Material(string computerName, string userNo, string lotNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(computerName, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("lotno", "LotNo", "String", lotNo, "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);
            string outXml = wsWip.LoadTemp_Material(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);

            CheckTxSuccess(xmlDoc);

            return RetriveDataTable(xmlDoc, "loadtemp_material");
        }

        private static DataTable RetriveDataTable(XmlDocument xmlDoc, string tagName)
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

        

        private static void CheckTxSuccess(XmlDocument xmlDoc)
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

        public static DataTable LoadEquipmentStateBySMD(string userNo, string eqpNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("equipmentno", "EquipmentNo", "String", eqpNo, "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);
            string outXml = wsEMS.LoadEquipmentStateBySMD(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);

            CheckTxSuccess(xmlDoc);

            return RetriveDataTable(xmlDoc, "loadequipmentstatebysmd");
        }

        public static DataTable LoadEQPStateBasis(string userNo, string eqpNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, "");
            string outXml = wsEQP.LoadEQPStateBasis(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);

            CheckTxSuccess(xmlDoc);

            return RetriveDataTable(xmlDoc, "loadeqpstatebasis");
        }

        public static DataTable LoadReasonSubTypeBasis(string userNo, string eqpNo, string ReasonSubType)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("reasonsubtype", "ReasonSubType", "String", ReasonSubType, "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);

            string outXml = wsQC.LoadReasonSubTypeBasis(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);

            CheckTxSuccess(xmlDoc);
            return RetriveDataTable(xmlDoc, "loadreasonsubtypebasis");
        }

        public static DataTable LoadReasonBasis(string userNo, string eqpNo, string ReasonSubType)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("issuestate", "IssueState", "Integer", "2", "");
            strParameter += XmlGenUtil.CombineXMLParameter("reasontype", "ReasonType", "Integer", "6", ""); //ReasonType = 6 (Eqp)
            strParameter += XmlGenUtil.CombineXMLParameter("reasonsubtype", "ReasonSubType", "Integer", ReasonSubType, ""); //ReasonType = 6 (Eqp)
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);

            string outXml = wsQC.LoadReasonBasis(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);

            CheckTxSuccess(xmlDoc);
            return RetriveDataTable(xmlDoc, "loadreasonbasis");

        }


        public static DataTable LoadEquipmentState(string userNo, string eqpNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("equipmentno", "EquipmentNo", "String", eqpNo, "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);

            string outXml = wsEMS.LoadEquipmentState(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);

            CheckTxSuccess(xmlDoc);
            return RetriveDataTable(xmlDoc, "loadequipmentstate");
        }

        public static void EditEquipmentState(string userNo, string eqpNo, int chgToStateNo, string desc, string reason)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("equipmentno", "EquipmentNo", "String", eqpNo, "");
            strParameter += XmlGenUtil.CombineXMLParameter("equipmentstate", "EquipmentState", "Integer", chgToStateNo.ToString(), "");
            strParameter += XmlGenUtil.CombineXMLParameter("userno", "UserNo", "String", userNo, "");
            strParameter += XmlGenUtil.CombineXMLParameter("description", "Description", "String", desc, "");
            strParameter += XmlGenUtil.CombineXMLParameterMultiValue("reasons", "Reasons", "String", reason, "");

            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);
            string outXml = wsEMS.EditEquipmentState(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);

            CheckTxSuccess(xmlDoc);
        }

        public static void funChangeStateActive(string userNo, string eqpNo, string lotNo, int stateNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("equipmentno", "EquipmentNo", "String", eqpNo, "");
            strParameter += XmlGenUtil.CombineXMLParameter("lotno", "LotNo", "String", lotNo, "");
            strParameter += XmlGenUtil.CombineXMLParameter("equipmentstate", "EquipmentState", "Integer", stateNo.ToString(), "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);
            string outXml = wsQC.funChangeStateActive(inXml);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);

            CheckTxSuccess(xmlDoc);
        }
    }
}
