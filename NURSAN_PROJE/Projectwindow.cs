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
    public partial class Projectwindow : DevExpress.XtraBars.ToolbarForm.ToolbarForm
    {
        public Projectwindow()
        {
            this.Hide();
            InitializeComponent();
            new Form1().ShowDialog();
            this.Show();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}