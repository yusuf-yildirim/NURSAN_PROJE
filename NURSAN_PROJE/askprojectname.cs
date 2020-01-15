using NURSAN_PROJE.SQL;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NURSAN_PROJE
{
    public partial class askprojectname : DevExpress.XtraEditors.XtraForm
    {
        bool finish = false;
        public askprojectname()
        {
            InitializeComponent();
        }

        private void okaskprojectnamebutton_Click(object sender, EventArgs e)
        {

            backgroundWorker1.RunWorkerAsync();        
            splashScreenManager1.ShowWaitForm();
            while(finish == false)
            {

            }
            splashScreenManager1.CloseWaitForm();
            this.Close();
        }

        private void cancelaskprojectnamebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void createproject()
        {
          
        }
        private void askprojectname_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void askprojectname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                backgroundWorker1.RunWorkerAsync();
                splashScreenManager1.ShowWaitForm();
                while (finish == false)
                {

                }
                splashScreenManager1.CloseWaitForm();
                this.Close();
            }
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                backgroundWorker1.RunWorkerAsync();
                splashScreenManager1.ShowWaitForm();
                while (finish == false)
                {

                }
                splashScreenManager1.CloseWaitForm();
                this.Close();
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            DBeng db = new DBeng();
            db.create_project(textEdit1.Text);
            finish = true;
        }
    }
}