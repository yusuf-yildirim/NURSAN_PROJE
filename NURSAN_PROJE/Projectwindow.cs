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
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using System.Data.SQLite;

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

        private void GridControl1_Click(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

      

        private void BarButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable table1 = new DataTable("Connections");
          
            table1.Columns.Add("name");
            table1.Columns.Add("id");
            table1.Rows.Add("sam", 1);
            table1.Rows.Add("mark", 2);


            gridControl1.DataSource = table1;
            Console.WriteLine("test");
        }

        private void CheckButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void SimpleButton3_Click(object sender, EventArgs e)
        {

        }

        private void LabelControl6_Click(object sender, EventArgs e)
        {

        }

        private void SidePanel1_Click(object sender, EventArgs e)
        {

        }

        private void GridControl2_Click(object sender, EventArgs e)
        {

        }

        private void LabelControl12_Click(object sender, EventArgs e)
        {

        }

        private void LabelControl13_Click(object sender, EventArgs e)
        {

        }
    }
}