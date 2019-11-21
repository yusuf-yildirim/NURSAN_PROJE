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
using NURSAN_PROJE.SQL;

namespace NURSAN_PROJE
{
    public partial class askprojectname : DevExpress.XtraEditors.XtraForm
    {
        public askprojectname()
        {
            InitializeComponent();
        }

        private void okaskprojectnamebutton_Click(object sender, EventArgs e)
        {
            DBeng db = new DBeng();
            db.create_project(textEdit1.Text);
            this.Close();                     
        }

        private void cancelaskprojectnamebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void askprojectname_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }
    }
}