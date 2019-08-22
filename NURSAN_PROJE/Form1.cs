using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace NURSAN_PROJE
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        private Boolean firstlogin = true;

        public Form1()
        {
            InitializeComponent();
            initializelistbox();
        }

        public void initializelistbox()
        {
            string[] xmlFiles = Directory.GetFiles("C:\\Users\\yyill\\Desktop\\xmlfile", "*.txt").Select(Path.GetFileName).ToArray();
            listBoxControl1.DataSource = xmlFiles;
        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (firstlogin==false)
            {
                Data.selectedfile = listBoxControl1.SelectedItem.ToString();
                MessageBox.Show(Data.selectedfile);
                this.Hide();
                
            }
            firstlogin = false;

        }

        private void newproject_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

