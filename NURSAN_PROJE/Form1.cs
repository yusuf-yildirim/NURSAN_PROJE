﻿using System;
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

            selectscreendb.Fill();
            
        }

        public void initializelistbox()
        {
         //   string[] xmlFiles = Directory.GetFiles("C:\\Users\\yyill\\Desktop\\xmlfile", "*.txt").Select(Path.GetFileName).ToArray();
       //     listBoxControl1.DataSource = xmlFiles;
        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (firstlogin==false)
            {
                Data.selectedfile = projectlistbox.SelectedItem.ToString();
                MessageBox.Show(Data.selectedfile);
                this.Hide();
                
            }
            firstlogin = false;

        }

        private void newproject_Click(object sender, EventArgs e)
        {
            new askprojectname().ShowDialog();
            if(SQL.DBeng.created == true)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("İŞLEM KULLANICI TARAFNDAN İPTAL EDİLDİ");
            }
           
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            new newuserform().ShowDialog();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
           // Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         //   toolTip1.SetToolTip(projectlistbox.Controls, selectscreendb.Result[0].ElementAt(0).ElementAt(1).ToString());
          
        }
    }
}

