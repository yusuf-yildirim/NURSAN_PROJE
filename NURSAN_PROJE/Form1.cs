﻿using NURSAN_PROJE.SQL;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;


namespace NURSAN_PROJE
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        private Boolean firstlogin = true;
        DataManager manager;
        public Form1()
        {
            manager = new DataManager(false);
            InitializeComponent();
            initializelistbox();

        }

        public void initializelistbox()
        {
            //   string[] xmlFiles = Directory.GetFiles("C:\\Users\\yyill\\Desktop\\xmlfile", "*.txt").Select(Path.GetFileName).ToArray(); 
            projectlistbox.DataSource = manager.getDatabaseTable("Recent", Databases.Main);
        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable temp = projectlistbox.DataSource as DataTable;
            string path = temp.Rows[projectlistbox.SelectedIndex][2].ToString();
            if (firstlogin == false)
            {
                if (File.Exists(path) == true)
                {
                    using (DBeng db = new DBeng())
                    {
                        db.setProjectPath(path);
                        using (LocalTables locals = new LocalTables(true))
                        {
                            locals.getalltables();
                        }
                    }


                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Dosya artık mevcut değil" + path);
                }
            }

            firstlogin = false;

        }

        private void newproject_Click(object sender, EventArgs e)
        {
            new askprojectname().ShowDialog();
            if (SQL.DBeng.created == true)
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

