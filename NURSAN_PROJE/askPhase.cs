using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NURSAN_PROJE
{
    public partial class askPhase : Form
    {
        public string phasename
        {
            get { return textEdit1.Text; }
        }
        public askPhase()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
         
                //Do some validation for the text in txtInput to be sure the ip is well-formated.

            if (textEdit1.Text.Length > 0)
            {
            this.DialogResult = DialogResult.OK;
            this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            
        }
    }
 

}
