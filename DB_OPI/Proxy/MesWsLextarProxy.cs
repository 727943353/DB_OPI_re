using DB_OPI.Util;
using DB_OPI.Vo;
using MesCommonCode.WebService.Msg;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;

namespace DB_OPI.Proxy
{
    class MesWsLextarProxy
    {
        static string TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";
        static wsWIP_LHTC.wsWIPSoapClient wsWipLHTC = new wsWIP_LHTC.wsWIPSoapClient();
        static wsCUS_LHTC.wsCUSSoapClient wsCusLHTC = new wsCUS_LHTC.wsCUSSoapClient();
        static wsPRD_LHTC.wsPRDSoapClient wsPrdLHTC = new wsPRD_LHTC.wsPRDSoapClient();
        static wsSYS_LHTC.wsSYSSoapClient wsSysLHTC = new wsSYS_LHTC.wsSYSSoapClient();
        static wsEMS_LHTC.wsEMSSoapClient wsEmsLHTC = new wsEMS_LHTC.wsEMSSoapClient();
        public static bool GetLotInfo(string userNo, string eqpNo, string cassetteNo, out LotInfo lotInfo, out string msg)
        {
            StringBuilder inXml = new StringBuilder();
            inXml.AppendLine("<?xml version='1.0' encoding='utf-8' standalone='yes'?>")
                .AppendLine("<TX><TX_NAME>GetLotInfo</TX_NAME>")
                .AppendLine("<TX_ID>" + DateTime.Now.ToString("yyyyMMddhhmmss") + "</TX_ID>")
                .AppendLine("<TIMESTAMP>" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "</TIMESTAMP>")
                .AppendLine("<USER_NO>" + userNo + "</USER_NO>")
                .AppendLine("<EQP_ID>" + eqpNo + "</EQP_ID>")
                .AppendLine("<LOT_NO></LOT_NO>")
                .AppendLine("<CST_ID>" + cassetteNo + "</CST_ID>")
                .AppendLine("<OPNO></OPNO>")
                .AppendLine("</TX>");

            string outXml = (string)wsWipLHTC.GetLotInfo(inXml.ToString());
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);

            msg = "OK";
            lotInfo = new LotInfo();
            if (xmlDoc.DocumentElement.GetElementsByTagName("MSG").Item(0).InnerText == "OK")
            {
                lotInfo.OpNo = xmlDoc.DocumentElement.GetElementsByTagName("OPNO").Item(0).InnerText;
                lotInfo.LotNo = xmlDoc.DocumentElement.GetElementsByTagName("LOT_NO").Item(0).InnerText;
                lotInfo.CurrQty = Convert.ToInt32(xmlDoc.DocumentElement.GetElementsByTagName("QTY").Item(0).InnerText);
                
                return true;
            }
            else
            {
                msg = "GetLotInfo : " + xmlDoc.DocumentElement.GetElementsByTagName("MSG").Item(0).InnerText;
                return false;
            }





        }

        public static DataTable LoadMaterialLot_DBC(string eqpNo, string userNo, string lotNo,string materialNo, string materialLevel, string unitNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("lotno", "LotNo", "String", lotNo, "");
            strParameter += XmlGenUtil.CombineXMLParameter("materialno", "MaterialNo", "String", materialNo, "");
            strParameter += XmlGenUtil.CombineXMLParameter("materiallevel", "MaterialLevel", "Integer", materialLevel, "");
            strParameter += XmlGenUtil.CombineXMLParameter("unitno", "UnitNo", "String", unitNo, "");

            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);
            string outXml = wsWipLHTC.LoadMaterialLot_DBC(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);

            MesWsUtil.CheckTxSuccess(xmlDoc);

            return MesWsUtil.RetriveDataTable(xmlDoc, "loadmateriallot");
        }


        public static DataTable LoadMaterialRecord(string userNo, string eqpNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("equipmentno", "EquipmentNo", "String", eqpNo, "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);
            string outXml = wsWipLHTC.LoadMaterialRecord(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);
            MesWsUtil.CheckTxSuccess(xmlDoc);


            return MesWsUtil.RetriveDataTable(xmlDoc, "loadmaterialrecord");
        }

        public static DataTable LoadMaterialRecordOnEqp(string userNo, string eqpNo, DateTime logonStTime, DateTime logonEndTime)
        {
            string result = wsWipLHTC.LoadMaterialRecordOnEquipment(userNo, eqpNo, logonStTime.ToString(TIME_FORMAT), logonEndTime.ToString(TIME_FORMAT));
            WsResponse wsRes = JsonConvert.DeserializeObject<WsResponse>(result);
            if (wsRes.Result == ResultEnum.Exception)
                throw new Exception(wsRes.Exception.Stack);
            return wsRes.ReturnTable;
        }

        //wsCusLHTC
        //public static DataTable LoadMaterialUsedState(string userNo, string matLotNo, string type)
        //{

        //    string result = wsCusLHTC.LoadMaterialUsedState(userNo, matLotNo, type);
        //    WsResponse wsRes = JsonConvert.DeserializeObject<WsResponse>(result);
        //    if (wsRes.Result == ResultEnum.Exception)
        //    {
        //        throw new Exception("LoadMaterialUsedState error." + wsRes.Exception.Stack);
        //    }

        //    return wsRes.ReturnTable;

        //}

        //public static DataTable LoadMaterialUsedStateByEqpNo(string userNo, string eqpNo, string type)
        //{
        //    string result = wsCusLHTC.LoadMaterialUsedStateByEqpNo(userNo, eqpNo, type);
        //    WsResponse wsRes = JsonConvert.DeserializeObject<WsResponse>(result);
        //    if (wsRes.Result == ResultEnum.Exception)
        //    {
        //        throw new Exception("LoadMaterialUsedStateByEqpNo error." + wsRes.Exception.Stack);
        //    }

        //    return wsRes.ReturnTable;
        //}

        //public static DataTable LoadGlueUsedState(string userNo, string matLotNo)
        //{
        //    string result = wsCusLHTC.LoadGlueUsedState(userNo, matLotNo);
        //    WsResponse wsRes = JsonConvert.DeserializeObject<WsResponse>(result);
        //    if (wsRes.Result == ResultEnum.Exception)
        //        throw new Exception("LoadMaterialUsedStateByEqpNo error." + wsRes.Exception.Stack);

        //    return wsRes.ReturnTable;
        //}

        public static void UpdateGlueLifeTime(string userNo, string matLotNo, string eqpNo, DateTime lifeStTime, DateTime lifeEndTime)
        {
            string result = wsCusLHTC.UpdateGlueLifeTimeData(userNo, matLotNo, eqpNo, lifeStTime.ToString(TIME_FORMAT), lifeEndTime.ToString(TIME_FORMAT));
            WsResponse wsRes = JsonConvert.DeserializeObject<WsResponse>(result);
            if (wsRes.Result == ResultEnum.Exception)
                throw new Exception(wsRes.Exception.Stack);

        }
        public static DataTable LoadGlueUsedState(string userNo, string matLotNo)
        {
            DateTime endTime = DateTime.Now;
            DateTime stTime = endTime.AddMonths(-1);
            string result = wsCusLHTC.LoadGlueUsedState(userNo, matLotNo, stTime.ToString(TIME_FORMAT), endTime.ToString(TIME_FORMAT));
            WsResponse wsRes = JsonConvert.DeserializeObject<WsResponse>(result);
            if (wsRes.Result == ResultEnum.Exception)
                throw new Exception(wsRes.Exception.Stack);

            return wsRes.ReturnTable;
        }

        
        public static WsResponse AddGlueReheatData(string userNo, string matLotNo)
        {
            string result = wsCusLHTC.AddGlueReheatData(userNo, matLotNo);
            WsResponse wsRes = JsonConvert.DeserializeObject<WsResponse>(result);
            if (wsRes.Result == ResultEnum.Exception)
                throw new Exception(wsRes.Exception.Stack);

            return wsRes;
        }

        public static void DeleteGlueUsedData(string userNo, string matLotNo)
        {
            string result = wsCusLHTC.DeleteByMaterialLotNo(userNo, matLotNo);
            WsResponse wsRes = JsonConvert.DeserializeObject<WsResponse>(result);
            if (wsRes.Result == ResultEnum.Exception)
                throw new Exception(wsRes.Exception.Stack);
        }

        public static DataTable LoadAllReheatingData(string userNo)
        {
            string result = wsCusLHTC.LoadAllGlueReheatingData(userNo);
            WsResponse wsRes = JsonConvert.DeserializeObject<WsResponse>(result);
            if (wsRes.Result == ResultEnum.Exception)
                throw new Exception("LoadAllReheatingData error." + wsRes.Exception.Stack);

            return wsRes.ReturnTable;
        }
        public static DataTable GetMaterialLifeTimeSetting(string userNo, string eqpNo, string matNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("productno", "ProductNo", "String", matNo, "");
            strParameter += XmlGenUtil.CombineXMLParameter("parametername", "ParameterName", "String", "LIFE_TIME", "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);
            string outXml = wsPrdLHTC.LoadRecipeParamemter(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);
            MesWsUtil.CheckTxSuccess(xmlDoc);

            return MesWsUtil.RetriveDataTable(xmlDoc, "loadrecipeparamemter_wp");
        }

        public static DataTable LoadMaterialRecordJoinGlueUsedStateOnEquipment(string userNo, string eqpNo, DateTime logonStTime, DateTime logonEndTime)
        {
            string result = wsWipLHTC.LoadMaterialRecordJoinGlueUsedStateOnEquipment(userNo, eqpNo, logonStTime.ToString(TIME_FORMAT), logonEndTime.ToString(TIME_FORMAT));
            WsResponse wsRes = JsonConvert.DeserializeObject<WsResponse>(result);
            if (wsRes.Result == ResultEnum.Exception)
                throw new Exception(wsRes.Exception.Stack);
            return wsRes.ReturnTable;
        }

        //public static void InsertMaterialUsedState(string userNo, string eqpNo, string matLotNo, DateTime stTime, DateTime expTime, string type)
        //{
        //    string result = wsCusLHTC.InsertMaterialUsedState(userNo, matLotNo, eqpNo, stTime.ToString("yyyy-MM-dd HH:mm:ss"), expTime.ToString("yyyy-MM-dd HH:mm:ss"), type);
        //    WsResponse wsRes = JsonConvert.DeserializeObject<WsResponse>(result);
        //    if (wsRes.Result == ResultEnum.Exception)
        //    {
        //        throw new Exception("LoadMaterialUsedState error." + wsRes.Exception.Stack);
        //    }
        //}

        public static void Add_Material_Record(string userNo, string eqpNo, string matNo, string matLotNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("materialno", "MaterialNo", "String", matNo, "");
            strParameter += XmlGenUtil.CombineXMLParameter("materiallotno", "MaterialLotNo", "String", matLotNo, "");
            strParameter += XmlGenUtil.CombineXMLParameter("equipmentno", "EquipmentNo", "String", eqpNo, "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);
            string outXml = wsWipLHTC.Add_Material_Record(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);
            MesWsUtil.CheckTxSuccess(xmlDoc);


        }

        public static void UpdateMaterialRecord(string userNo, string eqpNo, string matLotNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("materiallotno", "MaterialLotNo", "String", matLotNo, "");
            strParameter += XmlGenUtil.CombineXMLParameter("equipmentno", "EquipmentNo", "String", eqpNo, "");
            strParameter += XmlGenUtil.CombineXMLParameter("userno", "UserNo", "String", userNo, "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);
            string outXml = wsWipLHTC.UpdateMaterialRecord(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);
            MesWsUtil.CheckTxSuccess(xmlDoc);
        }


        public static DataTable LoadWIPInventory_RawJoinFrameIDByMatLotNo(string userNo, string eqpNo, string matLotNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("materiallotno", "MaterialLotNo", "String", matLotNo, "");
            strParameter += XmlGenUtil.CombineXMLParameter("blnbluetape", "blnBlueTape", "Boolean", "True", "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);
            string outXml = wsWipLHTC.LoadWIPInventory_RawJoinFrameID(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);
            MesWsUtil.CheckTxSuccess(xmlDoc);

            return MesWsUtil.RetriveDataTable(xmlDoc, "LoadWIPInventory_RawJoinFrameID");

        }


        public static DataTable LoadWIPInventory_RawJoinFrameIDByFrameID(string userNo, string eqpNo, string frameID)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("frame_id", "Frame_ID", "String", frameID, "");
            strParameter += XmlGenUtil.CombineXMLParameter("blnbluetape", "blnBlueTape", "Boolean", "True", "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);
            string outXml = wsWipLHTC.LoadWIPInventory_RawJoinFrameID(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);
            MesWsUtil.CheckTxSuccess(xmlDoc);

            return MesWsUtil.RetriveDataTable(xmlDoc, "LoadWIPInventory_RawJoinFrameID");

        }

        public static DataTable funLoadEQPMaterialStateJoinBlueTapeList(string userNo, string eqpNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("equipmentno", "EquipmentNo", "String", eqpNo, "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);
            string outXml = wsWipLHTC.LoadEQPMaterialStateJoinBlueTapeList(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);
            MesWsUtil.CheckTxSuccess(xmlDoc);


            return MesWsUtil.RetriveDataTable(xmlDoc, "loadeqpmaterialstatejoinbluetapelist");
        }

        public static DataTable LoadLabel_BlueTape(string userNo, string eqpNo, string blueTapeID)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("bluetapeid", "BlueTapeID", "String", blueTapeID, "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);
            string outXml = wsWipLHTC.LoadLabel_BlueTape(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);
            MesWsUtil.CheckTxSuccess(xmlDoc);


            return MesWsUtil.RetriveDataTable(xmlDoc, "loadlabel_bluetape");

        }


        public static DataTable LoadTemp_EquipmentJoinLotBasis(string userNo, string eqpNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("equipmentno", "EquipmentNo", "String", eqpNo, "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);
            string outXml = wsWipLHTC.LoadEQPMaterialStateJoinBlueTapeList(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);
            MesWsUtil.CheckTxSuccess(xmlDoc);


            return MesWsUtil.RetriveDataTable(xmlDoc, "loadeqpmaterialstatejoinbluetapelist");

        }

        public static void Exe_WIP_RAW_TurnEQP(string userNo, string eqpNo, DataTable logonBlueTapeTb)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("equipmentno", "EquipmentNo", "String", eqpNo, "");
            strParameter += XmlGenUtil.CombineXMLParameter("blnbluetape", "blnBlueTape", "Boolean", "True", "");

            string xml = "";
            foreach (DataRow btRow in logonBlueTapeTb.Rows)
            {
                xml += XmlGenUtil.CombineXMLValueTag(XmlGenUtil.CombineXMLValue("materialno", Convert.ToString(btRow["MATERIALNO"])) +
                    XmlGenUtil.CombineXMLValue("materiallotno", Convert.ToString(btRow["MATERIALLOTNO"])) +
                    XmlGenUtil.CombineXMLValue("unitno", Convert.ToString(btRow["UnitNo"])) +
                    XmlGenUtil.CombineXMLValue("qty", Convert.ToString(btRow["TURN_QTY"])) +
                    XmlGenUtil.CombineXMLValue("materialtype", "OPI") +
                    XmlGenUtil.CombineXMLValue("frominventoryno", Convert.ToString(btRow["InventoryNo"])));

            }

            strParameter += XmlGenUtil.CombineXMLParameterMultiValue("material", "Material", "String", xml, "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);
            string outXml = wsWipLHTC.Exe_WIP_RAW_TurnEQP(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);
            MesWsUtil.CheckTxSuccess(xmlDoc);

        }


        public static DataTable LoadTemp_EquipmentLot(string userNo, string eqpNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("equipmentno", "EquipmentNo", "String", eqpNo, "");

            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);
            string outXml = wsWipLHTC.LoadTemp_EquipmentLot(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);
            MesWsUtil.CheckTxSuccess(xmlDoc);
            return MesWsUtil.RetriveDataTable(xmlDoc, "loadtemp_equipmentlot");

        }

        public static DataTable LoadFunctionPrinter(string userNo, string eqpNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("functionno", "FunctionNo", "String", "EQP_WORK_SHEET", "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);
            string outXml = wsSysLHTC.LoadFunctionPrinter(inXml);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);
            MesWsUtil.CheckTxSuccess(xmlDoc);
            return MesWsUtil.RetriveDataTable(xmlDoc, "loadfunctionprinter");

        }

        public static DataTable LoadLotBasisJoinState(string userNo, string eqpNo, string lotNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("lotno", "LotNo", "String", lotNo, "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);
            string outXml = wsWipLHTC.LoadLotBasisJoinState(inXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);
            MesWsUtil.CheckTxSuccess(xmlDoc);
            return MesWsUtil.RetriveDataTable(xmlDoc, "loadlotbasisjoinstate");
        }

        public static string funChangeStatePrintLabel(string userNo, string eqpNo, string printer, string labelFormat, string reasonName, string ipqcLotNo)
        {
            string strIdentity = XmlGenUtil.CombineXMLIdentity(eqpNo, userNo);
            string strParameter = XmlGenUtil.CombineXMLParameter("equipmentno", "EquipmentNo", "String", eqpNo, "");
            strParameter += XmlGenUtil.CombineXMLParameter("printer", "Printer", "String", printer, "");
            strParameter += XmlGenUtil.CombineXMLParameter("labelformat", "LabelFormat", "String", labelFormat, "");
            strParameter += XmlGenUtil.CombineXMLParameter("reasonname", "ReasonName", "String", reasonName, "");
            strParameter += XmlGenUtil.CombineXMLParameter("lotno", "LotNo", "String", ipqcLotNo, "");
            string inXml = XmlGenUtil.CombineXMLRequest(strIdentity, strParameter);
            string outXml = wsEmsLHTC.funChangeStatePrintLabel(inXml);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(outXml);
            MesWsUtil.CheckTxSuccess(xmlDoc);
            
            return xmlDoc.DocumentElement["returnvalue"].InnerText;

        }
    }
}
