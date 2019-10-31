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
    public partial class askprojectname : DevExpress.XtraEditors.XtraForm
    {
        public askprojectname()
        {
            InitializeComponent();
        }

        private void okaskprojectnamebutton_Click(object sender, EventArgs e)
        {
            DevExpress.DataAccess.Sql.SqlDataSource.DisableCustomQueryValidation = true;
            sqlDataSource1.Queries[0].Parameters[0].Value = textEdit1.Text;
            sqlDataSource1.Fill();
            this.Close();
        }

        private void cancelaskprojectnamebutton_Click(object sender, EventArgs e)
        {

        }
    }
}