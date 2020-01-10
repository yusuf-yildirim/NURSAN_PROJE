using NURSAN_PROJE.SQL;
using System;
using System.Windows.Forms;

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

        private void askprojectname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DBeng db = new DBeng();
                db.create_project(textEdit1.Text);
                this.Close();
            }
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DBeng db = new DBeng();
                db.create_project(textEdit1.Text);
                this.Close();
            }
        }
    }
}