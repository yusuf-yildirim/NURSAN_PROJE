using System;
using System.Data;
using System.Drawing;
using System.Linq;
using DevExpress.XtraEditors;
using System.IO;
using System.Data.SQLite;
using NURSAN_PROJE.SQL;
using DevExpress.DataAccess.Sql;
using NURSAN_PROJE.Configurator;
using System.Windows.Forms;
using System.Windows;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel;

namespace NURSAN_PROJE
{
    public partial class Projectwindow : DevExpress.XtraBars.ToolbarForm.ToolbarForm
    {

     
        DataTable table1;
        DBeng db;
        public Projectwindow(string path)
        {
           
            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            db = new DBeng();
            Config conf = new Config();
            this.Hide();
            InitializeComponent();
            SqlDataSource.DisableCustomQueryValidation = true;
            try
            {
                if (path.Length > 0)
                {                   
                    conf.set_connectionstring("tablo", path);
                    db.create_recent(path, maindatasource);
                }
                else
                {                    
                    new Form1().ShowDialog();
                    this.Show();
                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //*--*-*-*-*-*-*-* FOTOĞRAFI VERİ TABANINDAN ALMA

           /* try
            {
                byte[] data = (byte[])projectdatasource.Result["imageStore"].ElementAt(0).ElementAt(2);
                using (var ms = new MemoryStream(data))
                {
                    pictureEdit1.Image = Image.FromStream(ms);
                }
            }
            catch
            {
                Console.WriteLine("RESİM YÜKLENEMEDİ");
            }*/
        }
        /*
                public void test()
                {

                    string cs = @"XpoProvider=SQLite;Data Source=C:\Users\Burakcan\source\repos\NURSAN_PROJE\NURSAN_PROJE\bin\Debug\PROJE DOSYASI ÖRNEĞİ2.nursan";

                using (SQLiteConnection con = new SQLiteConnection(cs))
                {

                    con.Open();

                    byte[] data = null;

                    try
                    {
                        data = File.ReadAllBytes(@"D:\test.jpg");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }

                    SQLiteCommand cmd = new SQLiteCommand(con);

                    cmd.CommandText = "INSERT INTO ImageStore(imageBlob) VALUES (@img)";
                    cmd.Prepare();

                    cmd.Parameters.Add("@img", DbType.Binary, data.Length);
                    cmd.Parameters["@img"].Value = data;
                    cmd.ExecuteNonQuery();

                    con.Close();
                }
                }
                */

        private void Projectwindow_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'mainsource.Sockets' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.socketsTableAdapter.Fill(this.mainsource.Sockets);
            // TODO: Bu kod satırı 'mainsource.tbl_Socket' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.

            Task.Factory.StartNew(() => refresh_socket_grids());
        }

        private void SimpleButton2_Click(object sender, EventArgs e)
        {
            DBeng test = new DBeng();
            test.connection_add(textEdit5.Text, textEdit6.Text, textEdit7.Text, textEdit8.Text);
            projectdatasource.Fill();
            gridControl1.RefreshDataSource();
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            string[,] arr2 = new string[3, 999];
            if (errorprovider_checktext_null())
            {
                object[] arr = new object[5];

                arr[0] = System.Guid.NewGuid().ToString();
                arr[1] = (newsocketname.Text);
                arr[2] = Convert.ToInt32(newsocketpinc.Text);
                arr[3] = Convert.ToInt32(newsocketswc.Text);
                arr[4] = Convert.ToInt32(newsocketledc.Text);
                for (int i = 0; i < gridView4.RowCount; i++)
                {
                    for (int z = 0; z < gridView4.Columns.Count; z++)
                    {
                        arr2[z, i] = gridView4.GetRowCellValue(i, gridView4.Columns[z]).ToString();
                    }
                }            
               
                Task.Factory.StartNew(() => db.register_socket(arr, arr2)).ContinueWith(delegate { refresh_socket_grids(); });
                navigationPane1.State = DevExpress.XtraBars.Navigation.NavigationPaneState.Collapsed;

                foreach (TextEdit t in newsocketvargroup.Controls)
                {
                    t.Text = "";
                }
            }
            else
            {
                Console.WriteLine("hata yok");
            }


        }

        //BOŞ GEÇİLMEMESİ GEREKEN PARAMETRELERİN KONTROLÜ! YENİ SOKET
        private bool errorprovider_checktext_null()
        {
            bool exit_code = true;
            foreach (TextEdit t in newsocketvargroup.Controls)
            {
                if (t.Text == "")
                {
                    errorprovider.SetError(t, "Bu alan boş geçilemez", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical);
                    exit_code = false;
                }
                else
                {
                    errorprovider.SetError(t, "", DevExpress.XtraEditors.DXErrorProvider.ErrorType.None);
                }

            }
            return exit_code;
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (gridView5.GetRowCellValue(gridView5.GetSelectedRows()[0], "ID_soket").ToString().Length > 0)
            {
                Task.Factory.StartNew(() => db.register_using_socket(gridView5.GetRowCellValue(gridView5.GetSelectedRows()[0], "ID_soket").ToString())).ContinueWith(delegate { refresh_socket_grids(); });
            }

        }

        private void newsocketpinc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int pinvalue = 0;
                if (newsocketpinc.Text != "")
                {
                    pinvalue = Convert.ToInt32(newsocketpinc.Text);
                }
                table1 = new DataTable("pins");
                table1.Columns.Add("Soket");
                table1.Columns.Add("Pin");
                table1.Columns.Add("Test noktası");
                for (int i = 1; i <= pinvalue; i++)
                {
                    table1.Rows.Add(newsocketname.Text, i);
                }
                gridControl4.DataSource = table1;
                gridControl4.RefreshDataSource();
                Console.WriteLine("test");
            }
            catch (FormatException err)
            {
                Console.WriteLine(newsocketpinc.Text);
            }
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView6.GetRowCellValue(gridView6.GetSelectedRows()[0], "ID_soket").ToString().Length != 0)
                {
                    Task.Factory.StartNew(() => db.unregister_using_socket(gridView6.GetRowCellValue(gridView6.GetSelectedRows()[0], "ID_soket").ToString())).ContinueWith(delegate { refresh_socket_grids(); });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void refresh_socket_grids()
        {
            realTimeSource2.DataSource = db.get_project_sockets();
           // realTimeSource1.DataSource = db.get_saved_sockets();
            this.socketsTableAdapter.Fill(this.mainsource.Sockets);
        }

        private void unregistermainsocket_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView5.GetRowCellValue(gridView5.GetSelectedRows()[0], "ID_soket").ToString().Length > 0)
                {
                    Task.Factory.StartNew(() => db.unregister_socket(gridView5.GetRowCellValue(gridView5.GetSelectedRows()[0], "ID_soket").ToString())).ContinueWith(delegate { refresh_socket_grids(); });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void pictureEdit1_Click(object sender, EventArgs e)
        {
            
           new determine_pin_locations_window().ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*mainsource.tbl_Socket.Addtbl_SocketRow("allah", "aaa", 54, 4, 2);
              this.tbl_SocketTableAdapter.Update(this.mainsource.tbl_Socket);*/
            
           
         
        }
    }
}


