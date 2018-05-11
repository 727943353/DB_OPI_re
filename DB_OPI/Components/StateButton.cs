using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DB_OPI.Components
{
    public partial class StateButton : UserControl
    {
        

        public event EventHandler ClickedEvent;
        
        public StateButton(string stateName, Color stateColor)
        {
            InitializeComponent();
            this.colorLab.BackColor = stateColor;
            this.colorLab.Text = "";
            this.stateBtn.Text = stateName;
            
        }

        public int EqpStateNo
        {
            set {
                this.stateBtn.Tag = value;
            }
        }

        private void stateBtn_Click(object sender, EventArgs e)
        {
            if (ClickedEvent != null)
                ClickedEvent(sender, e);

        }
    }
}
