using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace NURSAN_PROJE
{
    public partial class newuserform : DevExpress.XtraEditors.XtraForm
    {
        public newuserform()
        {
            InitializeComponent();
        }

        private void newuserokbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}