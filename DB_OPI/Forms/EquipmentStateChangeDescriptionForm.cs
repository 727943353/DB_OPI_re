using DB_OPI.Proxy;
using DB_OPI.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DB_OPI.Forms
{
    public partial class EquipmentStateChangeDescriptionForm : Form
    {
        public string userNo;
        public string equipmentNo;
        public string ReasonSubType;
        public int eqpStateNo = 0;

        //output to main form
        public string description;
        public string selReason;
        public bool isPrintLabel;
        public string strLabelFormat;
        public string printer;
        public string reasonName;
        public string strIPQC_Lot;
        public bool isChange;

        private DataTable eqpLotListTb;//已在此機台上機的lot list
        private DataTable printerTb;
        
        public EquipmentStateChangeDescriptionForm()
        {
            InitializeComponent();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EquipmentStateChangeDescriptionForm_Load(object sender, EventArgs e)
        {
            this.Text += " ___ Ver : " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            try
            {
#if DEBUG
                this.TopMost = false;
#endif

                Cursor.Current = Cursors.WaitCursor;

                reasonListGrid.AutoGenerateColumns = false;
                reasonSubTypeGrid.AutoGenerateColumns = false;
                reasonSubTypeGrid.DataSource = MesWsProxy.LoadReasonSubTypeBasis(userNo, equipmentNo, ReasonSubType);

                //query 已在此機台上機的 lot
                eqpLotListTb = MesWsLextarProxy.LoadTemp_EquipmentLot(userNo, equipmentNo);

                DataTable resTb = MesWsProxy.LoadReasonBasis(userNo, equipmentNo, ReasonSubType);
                resTb.Columns.Add("CHECKFLAG", typeof(Boolean));
                
                if (eqpLotListTb.Rows.Count > 0)
                {
                    var selRows = resTb.Select("Reasonname like '%" + eqpLotListTb.Rows[0]["OPNo"].ToString() + "%'");
                    if (selRows.Length > 0)
                    {
                        resTb.DefaultView.RowFilter = "Reasonname like '%" + eqpLotListTb.Rows[0]["OPNo"].ToString() + "%'";
                    }
                }
                
                reasonListGrid.DataSource = resTb;
                
                printerTb = MesWsLextarProxy.LoadFunctionPrinter(userNo, equipmentNo);
                var defPrinterRows = printerTb.Select("AREANO = 'DEFAULT'");
                if (defPrinterRows.Length > 0)
                {
                    printerTxt.Text = Convert.ToString(defPrinterRows[0]["Printer"]);
                    strLabelFormat = Convert.ToString(defPrinterRows[0]["LabelFormat"]);

                }


                Console.WriteLine("");
                //CHECKFLAG
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), "Error!");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

                
        }

        private void descBtn_Click(object sender, EventArgs e)
        {
            DataTable reasonTb = (DataTable)reasonListGrid.DataSource;
            var selReasRows = (from row in reasonTb.AsEnumerable()
                               where row["CHECKFLAG"] != DBNull.Value && row.Field<bool>("CHECKFLAG") == true
                               select row).ToArray();
            if (selReasRows.Length == 0)
            {
                MessageBox.Show("Please select ReasonNo !","Warning");
                return;
            }

            StringBuilder selResSb = new StringBuilder();
            StringBuilder descSb = new StringBuilder();

            int idx = 1;
            this.reasonName = "";
            string reasonNO;
            string reaName;
            foreach (DataRow row in selReasRows)
            {
                reasonNO = row.Field<string>("REASONNO");
                reaName = row.Field<string>("REASONNAME");

                selResSb.Append(XmlGenUtil.CombineXMLValueTag(
                    XmlGenUtil.CombineXMLValue("reasonno", reasonNO) +
                    XmlGenUtil.CombineXMLValue("reasonname", reaName)));

                descSb.AppendLine("(" + idx + ")" + reasonNO + " : " + reaName);

                this.reasonName += reaName + "(" + row.Field<string>("Description") + ");";

                idx++;
            }

            this.selReason = selResSb.ToString();
            txtDescription.Text = descSb.ToString();
            this.reasonName = this.reasonName.TrimEnd(';');

            var selRowsLevel1 = (from row in selReasRows
                             where row.Field<Decimal>("ReasonLevel") == 1
                             select row).ToArray();

            if (selRowsLevel1.Length > 0 && eqpLotListTb.Rows.Count > 0)
            {
                if (eqpStateNo == 30) //30 , state = TEST
                {
                    ipqcLotBtn.Visible = false;
                    ipqcLotTxt.ReadOnly = false;
                }
                else
                {
                    ipqcLotBtn.Visible = true;
                    ipqcLotTxt.ReadOnly = true;
                }

                prtBtn.Visible = true;
                printerTxt.Visible = true;
                printerLab.Visible = true;
                isPrintLabel = true;
                ipqcLotLab.Visible = true;
                ipqcLotTxt.Visible = true;

            }
            else
            {
                prtBtn.Visible = false;
                printerTxt.Visible = false;
                printerLab.Visible = false;
                isPrintLabel = false;
                ipqcLotLab.Visible = false;
                ipqcLotTxt.Visible = false;
                ipqcLotTxt.ReadOnly = true;
                ipqcLotBtn.Visible = false;
            }


            var selRowsLevel4 = (from row in selReasRows
                                 where row.Field<Decimal>("ReasonLevel") == 4
                                 select row).ToArray();

            if (selRowsLevel4.Length > 0)
            {
                ipqcLotBtn.Visible = true;
                ipqcLotLab.Visible = true;
                ipqcLotTxt.Visible = true;
                DataTable eqpStateTb = MesWsProxy.LoadEquipmentState(userNo, equipmentNo);
                if (eqpStateTb.Rows.Count > 0 && Convert.ToString(eqpStateTb.Rows[0]["LotSerial"]) != "NA")
                {
                    ipqcLotTxt.Text = Convert.ToString(eqpStateTb.Rows[0]["LotSerial"]);
                }
            }

        }

        private void ipqcLotBtn_Click(object sender, EventArgs e)
        {
            DataTable tb = new DataTable();
            
            DataColumn col = new DataColumn();
            col.Caption = "載具編號";
            col.ColumnName = "CARRIERNO";
            tb.Columns.Add(col);

            col = new DataColumn();
            col.Caption = "批號";
            col.ColumnName = "LOTNO";
            tb.Columns.Add(col);


            DataRow row;
            foreach (DataRow eqpLotRow in eqpLotListTb.Rows)
            {
                row = tb.NewRow();
                row["LOTNO"] = eqpLotRow["LOTNO"];
                row["CARRIERNO"] = eqpLotRow["CARRIERNO"];
                tb.Rows.Add(row);
            }

            ComponentSelectForm selForm = new ComponentSelectForm();
            selForm.gridData = tb;
            if (selForm.ShowDialog() == DialogResult.OK)
            {
                ipqcLotTxt.Text = Convert.ToString(selForm.SelectedRow["LOTNO"]);
            }


        }

        private void prtBtn_Click(object sender, EventArgs e)
        {
            DataTable tb = new DataTable();

            DataColumn col = new DataColumn();
            col.Caption = "功能編號";
            col.ColumnName = "FUNCTIONNO";
            tb.Columns.Add(col);

            col = new DataColumn();
            col.Caption = "區域";
            col.ColumnName = "AREANO";
            tb.Columns.Add(col);

            col = new DataColumn();
            col.Caption = "印表機名稱";
            col.ColumnName = "PRINTER";
            tb.Columns.Add(col);

            col = new DataColumn();
            col.Caption = "LABELFORMAT";
            col.ColumnName = "LABELFORMAT";
            tb.Columns.Add(col);

            col = new DataColumn();
            col.Caption = "設備編號";
            col.ColumnName = "EQUIPMENTNO";
            tb.Columns.Add(col);
            DataRow newRow = null;
            foreach (DataRow row in printerTb.Rows)
            {
                newRow = tb.NewRow();
                foreach (DataColumn column in tb.Columns)
                {
                    newRow[column.ColumnName] = row[column.ColumnName];
                }
                tb.Rows.Add(newRow);
            }

            ComponentSelectForm selForm = new ComponentSelectForm();
            selForm.gridData = tb;
            if (selForm.ShowDialog() == DialogResult.OK)
            {
                printerTxt.Text = Convert.ToString(selForm.SelectedRow["PRINTER"]);
                strLabelFormat = Convert.ToString(selForm.SelectedRow["LABELFORMAT"]);
            }
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            if (ipqcLotTxt.Visible && String.IsNullOrEmpty(ipqcLotTxt.Text))
            {
                MessageBox.Show("IPQC LotNo Can Not Null!!");
                return;
            }

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                MessageBox.Show("Please select ReasonNo !");
                return;
            }

            if (ipqcLotTxt.ReadOnly == false)
            {
                DataTable lotNoTb = MesWsLextarProxy.LoadLotBasisJoinState(userNo, equipmentNo, ipqcLotTxt.Text);
                if (lotNoTb.Rows.Count == 0)
                {
                    MessageBox.Show("IPQC Lot [" + ipqcLotTxt.Text + "] 並不存在 (IPQC LotNo [" + ipqcLotTxt.Text + "] does't exist)!");
                    return;
                }
            }

            if (ipqcLotTxt.Visible)
            {
                strIPQC_Lot = ipqcLotTxt.Text;
            }

            description = txtDescription.Text;
            printer = printerTxt.Text;
            isChange = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
