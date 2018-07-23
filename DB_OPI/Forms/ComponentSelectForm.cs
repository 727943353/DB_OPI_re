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
    public partial class ComponentSelectForm : Form
    {
        public DataTable gridData;
        public DataRow SelectedRow;
        public ComponentSelectForm()
        {
            InitializeComponent();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ComponentSelectForm_Load(object sender, EventArgs e)
        {
            this.Text += " ___ Ver : " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
#if DEBUG
            this.TopMost = false;
#endif
            gridView.DataSource = gridData;

            foreach (DataGridViewColumn col in gridView.Columns)
            {
                if(string.IsNullOrEmpty(gridData.Columns[col.HeaderText].Caption) == false)
                    col.HeaderText = gridData.Columns[col.HeaderText].Caption;
            }

        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            SelectedRow = gridData.Rows[gridView.SelectedRows[0].Index];
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
