﻿using System;
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
            if (registeredsocketgridview.GetRowCellValue(registeredsocketgridview.GetSelectedRows()[0], "ID_soket").ToString().Length > 0)
            {
                Task.Factory.StartNew(() => db.register_using_socket(registeredsocketgridview.GetRowCellValue(registeredsocketgridview.GetSelectedRows()[0], "ID_soket").ToString())).ContinueWith(delegate { refresh_socket_grids(); });
            }

        }

        private void newsocketpinc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int pinvalue = 0;
                int switchvalue = 0;
                if (newsocketpinc.Text != "")
                {
                    pinvalue = Convert.ToInt32(newsocketpinc.Text);
                }
                if (newsocketpinc.Text != "")
                {
                    switchvalue = Convert.ToInt32(newsocketswc.Text);
                }
                table1 = new DataTable("pins");
                table1.Columns.Add("Soket");
                table1.Columns.Add("Pin");
                table1.Columns.Add("Test noktası");
                if(new_socket_auto_assign_pin.Checked == true)
                {
                    for (int i = 1; i <= pinvalue; i++)
                    {
                        table1.Rows.Add(newsocketname.Text, i,i);
                    }
                    for (int i = 1; i <= switchvalue; i++)
                    {
                        table1.Rows.Add(newsocketname.Text+"-SW(+)", i, i);
                        table1.Rows.Add(newsocketname.Text+"-SW(-)", i, i);
                    }
                    gridControl4.DataSource = table1;
                    gridControl4.RefreshDataSource();

                }
                else
                {
                    for (int i = 1; i <= pinvalue; i++)
                    {
                        table1.Rows.Add(newsocketname.Text, i);
                    }
                    for (int i = 1; i <= switchvalue; i++)
                    {
                        table1.Rows.Add(newsocketname.Text + "-SW(+)", i);
                        table1.Rows.Add(newsocketname.Text + "-SW(-)", i);
                    }
                    gridControl4.DataSource = table1;
                    gridControl4.RefreshDataSource();
                }

                Console.WriteLine("test");
            }
            catch (FormatException err)
            {
                Console.WriteLine(newsocketpinc.Text);
                Console.WriteLine(newsocketswc.Text);
            }
        }

        private void selected_sockets_delete_button_Click(object sender, EventArgs e)
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
                if (registeredsocketgridview.GetRowCellValue(registeredsocketgridview.GetSelectedRows()[0], "ID_soket").ToString().Length > 0)
                {
                    Task.Factory.StartNew(() => db.unregister_socket(registeredsocketgridview.GetRowCellValue(registeredsocketgridview.GetSelectedRows()[0], "ID_soket").ToString())).ContinueWith(delegate { refresh_socket_grids(); });
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
            if(registeredsocketimg.Image != null)
            {
                new determine_pin_locations_window(registeredsocketgridview.GetRowCellValue(registeredsocketgridview.GetSelectedRows()[0], "ID_soket").ToString(), (Bitmap)registeredsocketimg.Image).ShowDialog();
            }
            else
            {
                new determine_pin_locations_window(registeredsocketgridview.GetRowCellValue(registeredsocketgridview.GetSelectedRows()[0], "ID_soket").ToString()).ShowDialog();
            }
            GC.Collect();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*mainsource.tbl_Socket.Addtbl_SocketRow("allah", "aaa", 54, 4, 2);
              this.tbl_SocketTableAdapter.Update(this.mainsource.tbl_Socket);*/
            
        }

        private void gridView5_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                registeredsocketimg.Image = db.get_socket_image(registeredsocketgridview.GetRowCellValue(registeredsocketgridview.GetSelectedRows()[0], "ID_soket").ToString());
            }catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {
         
        }

        private void new_socket_auto_assign_pin_CheckStateChanged(object sender, EventArgs e)
        {
            newsocketpinc_TextChanged(this, e);
        }

        private void newsocketswc_EditValueChanged(object sender, EventArgs e)
        {
            newsocketpinc_TextChanged(this, e);
        }

        private void add_splice_button_Click(object sender, EventArgs e)
        {
            db.addComponent("SPLICE", add_splice_name.Text);
        }

        private void add_capacitor_button_Click(object sender, EventArgs e)
        {
            db.addComponent("CAPACITOR", add_capacitor_name.Text,Convert.ToInt32(add_capacitor_value_number.Text),multipliertonumber(add_capacitor_value_multipler.Text),Convert.ToInt32(add_capacitor_tolerance.Text));

        }

        private void add_diode_button_Click(object sender, EventArgs e)
        {
            db.addComponent("DIODE", add_diode_name.Text, Convert.ToInt32(add_diode_value_number .Text), Convert.ToInt32(add_diode_tolerance.Text));
        }

        private void add_resistor_button_Click(object sender, EventArgs e)
        {
            db.addComponent("RESISTOR", add_resistor_name.Text, Convert.ToInt32(add_resistor_value.Text), multipliertonumber(add_resistor_multiplier.Text), Convert.ToInt32(add_resistor_tolerance.Text));
        }

        private void add_thermistor_button_Click(object sender, EventArgs e)
        {
            db.addComponent("THERMISTOR",
                            add_thermistor_name.Text,
                            Convert.ToInt32(add_thermistor_tolerance.Text),
                            Convert.ToInt32(add_thermistor_firsttestpoint.Text),
                            Convert.ToInt32(add_thermistor_secondtestpoint.Text),
                            Convert.ToInt32(add_thermistor_minresistance.Text),
                            Convert.ToInt32(add_thermistor_maxresistence.Text),
                            Convert.ToInt32(add_thermistor_minresistancemultiplier.Text),
                            Convert.ToInt32(add_thermistor_maxresistancemultiplier.Text));

        }

        private void simpleButton14_Click(object sender, EventArgs e)
        {
            db.addComponent("GENERIC", add_generic_name.Text, Convert.ToInt32(add_generic_current.Text), 5, Convert.ToInt32(add_generic_voltagedrop.Text), Convert.ToInt32(add_resistor_tolerance.Text));

        }
        public int multipliertonumber(string multiplier)
        {
            int multipliednumber = 0;
            switch (multiplier)
            {
                case "mΩ":
                    multipliednumber = -3;
                    break;
                case "Ω":
                    multipliednumber = 1;
                    break;
                case "KΩ":
                    multipliednumber = 3;
                    break;
                case "MΩ":
                    multipliednumber = 6;
                    break;
                case "uf":
                    multipliednumber = -6;
                    break;
                case "pf":
                    multipliednumber = -12;
                    break;
                case "uA":
                    multipliednumber = -6;
                    break;
                case "mA":
                    multipliednumber = -3;
                    break;



            }
            return multipliednumber;
        }



    }
}


