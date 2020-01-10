﻿using DevExpress.DataAccess.Sql;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList.Nodes;
using NURSAN_PROJE.Configurator;
using NURSAN_PROJE.SQL;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NURSAN_PROJE
{
    public partial class Projectwindow : DevExpress.XtraBars.ToolbarForm.ToolbarForm
    {


        DataTable table1;
        readonly DBeng db;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Projectwindow_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'mainsource.Components' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.componentsTableAdapter.Fill(this.mainsource.Components);
            // TODO: Bu kod satırı 'mainsource.Sockets' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.socketsTableAdapter.Fill(this.mainsource.Sockets);
            Task.Factory.StartNew(() => refresh_socket_grids());
        }

        private void SimpleButton2_Click(object sender, EventArgs e)
        {
            string origin = null, origintype = null, destination = null, destinationtype = null, color = null;
            if (treeListLookUpEdit1TreeList.FocusedNode["IOID"].ToString().Length > 0)
            {
                origin = treeListLookUpEdit1TreeList.FocusedNode["IOID"].ToString();
                origintype = "SOCKET";
            }
            else if (treeListLookUpEdit1TreeList.FocusedNode["ComponentID"].ToString().Length > 0)
            {
                origin = treeListLookUpEdit1TreeList.FocusedNode["ComponentID"].ToString();
                origintype = "COMPONENT";
            }
            if (treeList1.FocusedNode["IOID"].ToString().Length > 0)
            {
                destination = treeList1.FocusedNode["IOID"].ToString();
                destinationtype = "SOCKET";
            }
            else if (treeList1.FocusedNode["ComponentID"].ToString().Length > 0)
            {
                destination = treeList1.FocusedNode["ComponentID"].ToString();
                destinationtype = "COMPONENT";
            }


            color = gridLookUpEdit1View.GetRowCellValue(gridLookUpEdit1View.GetSelectedRows()[0], "ID_color").ToString();
            db.connection_add(origin, origintype, destination, destinationtype, color);
            gridControl1.DataSource = db.get_ConnectionTable();

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
                if (string.IsNullOrEmpty(t.Text))
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
                if (!string.IsNullOrEmpty(newsocketpinc.Text))
                {
                    pinvalue = Convert.ToInt32(newsocketpinc.Text);
                }

                if (!string.IsNullOrEmpty(newsocketpinc.Text))
                {
                    switchvalue = Convert.ToInt32(newsocketswc.Text);
                }
                table1 = new DataTable("pins");
                table1.Columns.Add("Soket");
                table1.Columns.Add("Pin");
                table1.Columns.Add("Test noktası");
                if (new_socket_auto_assign_pin.Checked == true)
                {
                    for (int i = 1; i <= pinvalue + (switchvalue * 2); i++)
                    {

                        if (i > pinvalue)
                        {
                            table1.Rows.Add(newsocketname.Text + "-SW(+)", i, i);
                            table1.Rows.Add(newsocketname.Text + "-SW(-)", i + 1, i + 1);
                            i++;
                        }
                        else
                        {
                            table1.Rows.Add(newsocketname.Text, i, i);
                        }
                    }

                    gridControl4.DataSource = table1;
                    gridControl4.RefreshDataSource();

                }
                else
                {
                    for (int i = 1; i <= pinvalue + (switchvalue * 2); i++)
                    {

                        if (i > pinvalue)
                        {
                            table1.Rows.Add(newsocketname.Text + "-SW(+)", i);
                            table1.Rows.Add(newsocketname.Text + "-SW(-)", i + 1);
                            i++;
                        }
                        else
                        {
                            table1.Rows.Add(newsocketname.Text, i);
                        }
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
                Console.WriteLine(err.Message);
            }
        }

        private void selected_sockets_delete_button_Click(object sender, EventArgs e)
        {
            //popupControlContainer1.ShowPopup(Cursor.Position);
            db.GuidCheck(newsocketname.Text);
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
            this.componentsTableAdapter.Fill(this.mainsource.Components);
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
            if (registeredsocketimg.Image != null)
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
            }
            catch (Exception err)
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
            refresh_socket_grids();
        }

        private void add_capacitor_button_Click(object sender, EventArgs e)
        {
            db.addComponent("CAPACITOR", add_capacitor_name.Text, Convert.ToInt32(add_capacitor_value_number.Text), multipliertonumber(add_capacitor_value_multipler.Text), Convert.ToInt32(add_capacitor_tolerance.Text));
            refresh_socket_grids();
        }

        private void add_diode_button_Click(object sender, EventArgs e)
        {
            db.addComponent("DIODE", add_diode_name.Text, Convert.ToInt32(add_diode_value_number.Text), Convert.ToInt32(add_diode_tolerance.Text));
            refresh_socket_grids();
        }

        private void add_resistor_button_Click(object sender, EventArgs e)
        {
            db.addComponent("RESISTOR", add_resistor_name.Text, Convert.ToInt32(add_resistor_value.Text), multipliertonumber(add_resistor_multiplier.Text), Convert.ToInt32(add_resistor_tolerance.Text));
            refresh_socket_grids();
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
            refresh_socket_grids();
        }

        private void simpleButton14_Click(object sender, EventArgs e)
        {
            db.addComponent("GENERIC", add_generic_name.Text, Convert.ToInt32(add_generic_current.Text), 5, Convert.ToInt32(add_generic_voltagedrop.Text), Convert.ToInt32(add_resistor_tolerance.Text));
            refresh_socket_grids();
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

        private void simpleButton13_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {





        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(treeListLookUpEdit1TreeList.FocusedNode["IOID"].ToString());  //TO-DO BAĞLANTILARI BELİRLE

        }

        private void treeListLookUpEdit1_Popup(object sender, EventArgs e)
        {

        }

        private void Where_to_treelist_lookup_Popup(object sender, EventArgs e)
        {

            treeList1.ClearNodes();
            try
            {
                DataTable socketlist = db.get_project_sockets().DataViewManager.DataSet.Tables[0];
                Where_to_treelist_lookup.Properties.DisplayMember = "Soket Adı";
                TreeListNode SocketsNode = null;
                for (int i = 0; i < socketlist.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        SocketsNode = treeList1.AppendNode(null, null);
                        SocketsNode.SetValue("Soket Adı", "SOKETLER");
                    }
                    TreeListNode node1 = treeList1.AppendNode(null, SocketsNode);

                    node1.SetValue("Soket Adı", socketlist.Rows[i][1]);
                    node1.SetValue("SoketID", socketlist.Rows[i][0]);

                    DataTable socketiolist = db.determineio(socketlist.Rows[i][0].ToString()).Tables[0];

                    for (int y = 0; y < socketiolist.Rows.Count; y++)
                    {
                        try
                        {
                            TreeListNode childnode1 = treeList1.AppendNode(null, node1);
                            childnode1.SetValue("IOID", socketiolist.Rows[y][0]);

                            childnode1.SetValue("Soket Adı", socketlist.Rows[i][1] + " : " + socketiolist.Rows[y][2]);
                            if ((int)socketiolist.Rows[y][3] != 0)
                            {
                                childnode1.SetValue("SoketIO", "Cihaz Pin : " + socketiolist.Rows[y][3]);
                            }
                        }
                        catch
                        {

                        }
                    }
                }
                TreeListNode ComponentsNode = null;
                TreeListNode ResistorNode = null;
                TreeListNode CapacitorNode = null;
                TreeListNode SpliceNode = null;
                TreeListNode DiodeNode = null;
                TreeListNode GenericComponentNode = null;
                DataTable componentlist = db.get_project_components();
                for (int i = 0; i < componentlist.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        ComponentsNode = treeList1.AppendNode(null, null);
                        ResistorNode = treeList1.AppendNode(null, ComponentsNode);
                        CapacitorNode = treeList1.AppendNode(null, ComponentsNode);
                        SpliceNode = treeList1.AppendNode(null, ComponentsNode);
                        DiodeNode = treeList1.AppendNode(null, ComponentsNode);
                        GenericComponentNode = treeList1.AppendNode(null, ComponentsNode);

                        ComponentsNode.SetValue("Soket Adı", "Komponentler");
                        ResistorNode.SetValue("Soket Adı", "Dirençler");
                        CapacitorNode.SetValue("Soket Adı", "Kapasitörler");
                        SpliceNode.SetValue("Soket Adı", "Düğümler");
                        DiodeNode.SetValue("Soket Adı", "Diyotlar");
                        GenericComponentNode.SetValue("Soket Adı", "Genel Bileşenler");
                    }
                    switch (componentlist.Rows[i][2].ToString())
                    {
                        case "RESISTOR":
                            TreeListNode childnode1 = treeList1.AppendNode(null, ResistorNode);
                            childnode1.SetValue("Soket Adı", componentlist.Rows[i][1]);
                            TreeListNode childnode2 = treeList1.AppendNode(null, childnode1);
                            childnode1.SetValue("ComponentID", componentlist.Rows[i][0] + "!!");
                            break;
                        case "CAPACITOR":
                            childnode1 = treeList1.AppendNode(null, CapacitorNode);
                            childnode1.SetValue("Soket Adı", componentlist.Rows[i][1]);
                            childnode2 = treeList1.AppendNode(null, childnode1);
                            childnode1.SetValue("ComponentID", componentlist.Rows[i][0] + "!!");
                            break;
                        case "SPLICE":
                            childnode1 = treeList1.AppendNode(null, SpliceNode);
                            childnode1.SetValue("Soket Adı", componentlist.Rows[i][1]);
                            childnode2 = treeList1.AppendNode(null, childnode1);
                            childnode1.SetValue("ComponentID", componentlist.Rows[i][0] + "!!");
                            break;
                        case "DIODE":
                            childnode1 = treeList1.AppendNode(null, DiodeNode);
                            childnode1.SetValue("Soket Adı", componentlist.Rows[i][1]);
                            childnode2 = treeList1.AppendNode(null, childnode1);
                            childnode1.SetValue("ComponentID", componentlist.Rows[i][0] + "!!");
                            break;
                        case "GENERIC":
                            childnode1 = treeList1.AppendNode(null, GenericComponentNode);
                            childnode1.SetValue("Soket Adı", componentlist.Rows[i][1]);
                            childnode2 = treeList1.AppendNode(null, childnode1);
                            childnode1.SetValue("ComponentID", componentlist.Rows[i][0] + "!!");
                            break;
                    }
                }
                Console.WriteLine("ok");
                treeList1.Refresh();
                //treeListLookUpEdit1TreeList.RefreshDataSource();
                treeList1.RefreshEditor(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("fail");
                Console.WriteLine(ex.Message);

            }
        }

        private void tabPane1_SelectedPageIndexChanged(object sender, EventArgs e)
        {
            if (tabPane1.SelectedPageIndex == 1)
            {
                treeListLookUpEdit1_Popup(sender, EventArgs.Empty);
            }
        }

        private void Colors_lookup_BeforePopup(object sender, EventArgs e)
        {
            Colors.DataSource = db.get_Colors();
            Console.WriteLine("Renkler listelendi");
            gridLookUpEdit1View.RowCellStyle += (senders, es) =>
            {
                GridView view = sender as GridView;
                if (es.Column.FieldName == "HexCode")
                {
                    System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml(es.CellValue.ToString());
                    es.Appearance.BackColor = col;


                }
            };

        }

        private void treeListLookUpEdit1_BeforePopup(object sender, EventArgs e)
        {
            treeListLookUpEdit1TreeList.ClearNodes();
            try
            {
                DataTable socketlist = db.get_project_sockets().DataViewManager.DataSet.Tables[0];
                treeListLookUpEdit1.Properties.DisplayMember = "Soket Adı";
                TreeListNode SocketsNode = null;
                for (int i = 0; i < socketlist.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        SocketsNode = treeListLookUpEdit1TreeList.AppendNode(null, null);
                        SocketsNode.SetValue("Soket Adı", "SOKETLER");
                    }
                    TreeListNode node1 = treeListLookUpEdit1TreeList.AppendNode(null, SocketsNode);

                    node1.SetValue("Soket Adı", socketlist.Rows[i][1]);
                    node1.SetValue("SoketID", socketlist.Rows[i][0]);

                    DataTable socketiolist = db.determineio(socketlist.Rows[i][0].ToString()).Tables[0];

                    for (int y = 0; y < socketiolist.Rows.Count; y++)
                    {
                        try
                        {
                            TreeListNode childnode1 = treeListLookUpEdit1TreeList.AppendNode(null, node1);
                            childnode1.SetValue("IOID", socketiolist.Rows[y][0]);

                            childnode1.SetValue("Soket Adı", socketlist.Rows[i][1] + " : " + socketiolist.Rows[y][2]);
                            if (Convert.ToInt32(socketiolist.Rows[y][3]) != 0)
                            {
                                childnode1.SetValue("SoketIO", "Cihaz Pin : " + socketiolist.Rows[y][3]);
                            }
                        }
                        catch
                        {

                        }
                    }
                }
                TreeListNode ComponentsNode = null;
                TreeListNode ResistorNode = null;
                TreeListNode CapacitorNode = null;
                TreeListNode SpliceNode = null;
                TreeListNode DiodeNode = null;
                TreeListNode GenericComponentNode = null;
                DataTable componentlist = db.get_project_components();
                for (int i = 0; i < componentlist.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        ComponentsNode = treeListLookUpEdit1TreeList.AppendNode(null, null);
                        ResistorNode = treeListLookUpEdit1TreeList.AppendNode(null, ComponentsNode);
                        CapacitorNode = treeListLookUpEdit1TreeList.AppendNode(null, ComponentsNode);
                        SpliceNode = treeListLookUpEdit1TreeList.AppendNode(null, ComponentsNode);
                        DiodeNode = treeListLookUpEdit1TreeList.AppendNode(null, ComponentsNode);
                        GenericComponentNode = treeListLookUpEdit1TreeList.AppendNode(null, ComponentsNode);

                        ComponentsNode.SetValue("Soket Adı", "Komponentler");
                        ResistorNode.SetValue("Soket Adı", "Dirençler");
                        CapacitorNode.SetValue("Soket Adı", "Kapasitörler");
                        SpliceNode.SetValue("Soket Adı", "Düğümler");
                        DiodeNode.SetValue("Soket Adı", "Diyotlar");
                        GenericComponentNode.SetValue("Soket Adı", "Genel Bileşenler");
                    }
                    switch (componentlist.Rows[i][2].ToString())
                    {
                        case "RESISTOR":
                            TreeListNode childnode1 = treeListLookUpEdit1TreeList.AppendNode(null, ResistorNode);
                            childnode1.SetValue("Soket Adı", componentlist.Rows[i][1]);
                            TreeListNode childnode2 = treeListLookUpEdit1TreeList.AppendNode(null, childnode1);
                            childnode1.SetValue("ComponentID", componentlist.Rows[i][0] + "!!");
                            break;
                        case "CAPACITOR":
                            childnode1 = treeListLookUpEdit1TreeList.AppendNode(null, CapacitorNode);
                            childnode1.SetValue("Soket Adı", componentlist.Rows[i][1]);
                            childnode2 = treeListLookUpEdit1TreeList.AppendNode(null, childnode1);
                            childnode1.SetValue("ComponentID", componentlist.Rows[i][0] + "!!");
                            break;
                        case "SPLICE":
                            childnode1 = treeListLookUpEdit1TreeList.AppendNode(null, SpliceNode);
                            childnode1.SetValue("Soket Adı", componentlist.Rows[i][1]);
                            childnode2 = treeListLookUpEdit1TreeList.AppendNode(null, childnode1);
                            childnode1.SetValue("ComponentID", componentlist.Rows[i][0] + "!!");
                            break;
                        case "DIODE":
                            childnode1 = treeListLookUpEdit1TreeList.AppendNode(null, DiodeNode);
                            childnode1.SetValue("Soket Adı", componentlist.Rows[i][1]);
                            childnode2 = treeListLookUpEdit1TreeList.AppendNode(null, childnode1);
                            childnode1.SetValue("ComponentID", componentlist.Rows[i][0] + "!!");
                            break;
                        case "GENERIC":
                            childnode1 = treeListLookUpEdit1TreeList.AppendNode(null, GenericComponentNode);
                            childnode1.SetValue("Soket Adı", componentlist.Rows[i][1]);
                            childnode2 = treeListLookUpEdit1TreeList.AppendNode(null, childnode1);
                            childnode1.SetValue("ComponentID", componentlist.Rows[i][0] + "!!");
                            break;
                    }
                }
                Console.WriteLine("ok");
                treeListLookUpEdit1TreeList.Refresh();
                //treeListLookUpEdit1TreeList.RefreshDataSource();
                treeListLookUpEdit1TreeList.RefreshEditor(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("fail");
                Console.WriteLine(ex.Message);

            }
        }
    }
}


