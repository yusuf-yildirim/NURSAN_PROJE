using DevExpress.DataAccess.Sql;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList.Nodes;
using NURSAN_PROJE.Configurator;
using NURSAN_PROJE.SQL;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NURSAN_PROJE
{
    public partial class Projectwindow : DevExpress.XtraBars.ToolbarForm.ToolbarForm
    {


        DataTable table1;
        DataManager manager;
        DataManager.TableUpdater updater;
        readonly DBeng db;
        public Projectwindow(string path)
        {


            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
            //db = new DBeng();            
            Config conf = new Config();
            this.Hide();
            InitializeComponent();
            SqlDataSource.DisableCustomQueryValidation = true;
            try
            {
                if (path.Length > 0)
                {
                    manager = new DataManager();
                    updater = new DataManager.TableUpdater();
                    conf.set_connectionstring("tablo", path);
                    manager.createRecent(path);
                }
                else
                {
                    manager = new DataManager(false);
                    updater = new DataManager.TableUpdater();
                    new Form1().ShowDialog();
                    this.Show();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }
          
            Task.Factory.StartNew(() => refresh_socket_grids());
        }

        private void Projectwindow_Load(object sender, EventArgs e)
        {
            
            
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
            manager.addConnection(origin, origintype, destination, destinationtype, color, gridView7.GetRowCellValue(gridView7.GetSelectedRows()[0], "ID_etap").ToString(),textEdit9.Text) ;
            gridControl1.DataSource = manager.getConnectionbyPhase(gridView7.GetRowCellValue(gridView7.GetSelectedRows()[0], "ID_etap").ToString());

        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {

            foreach(TextEdit control in newsocketvargroup.Controls)
            {
                control.IsModified = true;
            }
            //TO-DO
            addsocketvalidateresult = true;
            newsocketname.DoValidate();
            newsocketpinc.DoValidate();
            newsocketledc.DoValidate();
            newsocketswc.DoValidate();
            if(addsocketvalidateresult == false)
            {
                addMainSocket();
            }

        }

   
        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (registeredsocketgridview.GetRowCellValue(registeredsocketgridview.GetSelectedRows()[0], "ID_soket").ToString().Length > 0)
            {
                manager.socket2Project(registeredsocketgridview.GetRowCellValue(registeredsocketgridview.GetSelectedRows()[0], "ID_soket").ToString());
                refresh_socket_grids();
               // Task.Factory.StartNew(() => manager.socket2Project(registeredsocketgridview.GetRowCellValue(registeredsocketgridview.GetSelectedRows()[0], "ID_soket").ToString())).ContinueWith(delegate { refresh_socket_grids(); });
            }

        }

        private void newsocketpinc_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void selected_sockets_delete_button_Click(object sender, EventArgs e)
        {
            //popupControlContainer1.ShowPopup(Cursor.Position);           
            try
            {
                if (gridView6.GetRowCellValue(gridView6.GetSelectedRows()[0], "ID_soket").ToString().Length != 0)
                {
                    Task.Factory.StartNew(() => manager.deleteProjectSocket(gridView6.GetRowCellValue(gridView6.GetSelectedRows()[0], "ID_soket").ToString())).ContinueWith(delegate { refresh_socket_grids(); });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void updateTable()
        {
            
        }
        public void refresh_socket_grids()
        {
            projectsocketsource.DataSource = manager.getProjectSockets();
            mainsocketsource.DataSource = manager.getMainSockets();
            
          
        }

        private void unregistermainsocket_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (registeredsocketgridview.GetRowCellValue(registeredsocketgridview.GetSelectedRows()[0], "ID_soket").ToString().Length > 0)
                {
                    Task.Factory.StartNew(() => manager.deleteMainSocket(registeredsocketgridview.GetRowCellValue(registeredsocketgridview.GetSelectedRows()[0], "ID_soket").ToString())).ContinueWith(delegate { refresh_socket_grids(); });
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
            Task.Factory.StartNew(() => splashScreenManager1.ShowWaitForm());
            if (registeredsocketimg.Image != null)
            {
                new determine_pin_locations_window(registeredsocketgridview.GetRowCellValue(registeredsocketgridview.GetSelectedRows()[0], "ID_soket").ToString(), (Bitmap)registeredsocketimg.Image).ShowDialog();
                splashScreenManager1.CloseWaitForm();
            }
            else
            {
                new determine_pin_locations_window(registeredsocketgridview.GetRowCellValue(registeredsocketgridview.GetSelectedRows()[0], "ID_soket").ToString()).ShowDialog();
                splashScreenManager1.CloseWaitForm();

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
                registeredsocketimg.Image = manager.getSocketImage(registeredsocketgridview.GetRowCellValue(registeredsocketgridview.GetSelectedRows()[0], "ID_soket").ToString());
                gridControl4.Visible = false;
                new_socket_auto_assign_pin.Visible = false;
                Console.WriteLine(registeredsocketgridview.GetRowCellValue(e.FocusedRowHandle, registeredsocketgridview.Columns[0]).ToString());
                edit_socket_name.Text = registeredsocketgridview.GetRowCellValue(e.FocusedRowHandle, registeredsocketgridview.Columns[0]).ToString();
                edit_socket_lednumber.Text = registeredsocketgridview.GetRowCellValue(e.FocusedRowHandle, registeredsocketgridview.Columns[3]).ToString();
                edit_socket_pinnumber.Text = registeredsocketgridview.GetRowCellValue(e.FocusedRowHandle, registeredsocketgridview.Columns[1]).ToString();
                edit_socket_switchnumber.Text = registeredsocketgridview.GetRowCellValue(e.FocusedRowHandle, registeredsocketgridview.Columns[2]).ToString();
            }
            catch (Exception err)
            {
                registeredsocketimg.Image = null;
                Console.WriteLine("252 - RESİM YOK");
            }
        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void new_socket_auto_assign_pin_CheckStateChanged(object sender, EventArgs e)
        {
            edit_socket_pinnumber_EditValueChanged(this, e);
        }

        private void newsocketswc_EditValueChanged(object sender, EventArgs e)
        {
            newsocketpinc_TextChanged(this, e);
        }

        private void add_splice_button_Click(object sender, EventArgs e)
        {
            manager.addComponent("SPLICE", add_splice_name.Text);
            refresh_socket_grids();
        }

        private void add_capacitor_button_Click(object sender, EventArgs e)
        {
            manager.addComponent("CAPACITOR", add_capacitor_name.Text, Convert.ToInt32(add_capacitor_value_number.Text), multipliertonumber(add_capacitor_value_multipler.Text), Convert.ToInt32(add_capacitor_tolerance.Text));
            refresh_socket_grids();
        }

        private void add_diode_button_Click(object sender, EventArgs e)
        {
            manager.addComponent("DIODE", add_diode_name.Text, Convert.ToInt32(add_diode_value_number.Text), Convert.ToInt32(add_diode_tolerance.Text));
            refresh_socket_grids();
        }

        private void add_resistor_button_Click(object sender, EventArgs e)
        {
            manager.addComponent("RESISTOR", add_resistor_name.Text, Convert.ToInt32(add_resistor_value.Text), multipliertonumber(add_resistor_multiplier.Text), Convert.ToInt32(add_resistor_tolerance.Text));
            refresh_socket_grids();
        }

        private void add_thermistor_button_Click(object sender, EventArgs e)
        {
            manager.addComponent("THERMISTOR",
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
            try
            {
                string[] generic_current = add_generic_current.Text.Split(' ');
                manager.addComponent("GENERIC", add_generic_name.Text, Convert.ToInt32(generic_current[0]), multipliertonumber(generic_current[1]), Convert.ToInt32(add_generic_voltagedrop.Text), Convert.ToInt32(add_generic_tolerance.Text));
                refresh_socket_grids();
            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message + err.StackTrace);
                MessageBox.Show("Lütfen değerleri doğru giriniz.");
            }
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
            DataTable temp = manager.getMainComponents();
            manager.componenet2project(temp.Rows[listBoxControl1.SelectedIndex][0].ToString());
            projectcomponentsource.DataSource = manager.getProjectComponents();
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
            treeList1.ForceInitialize();
            try
            {
                DataTable socketlist = manager.getProjectSockets();
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

                    DataTable socketiolist = manager.getIObySocketID(socketlist.Rows[i][0].ToString());

                    for (int y = 0; y < socketiolist.Rows.Count; y++)
                    {

                        try
                        {
                            TreeListNode childnode1 = treeList1.AppendNode(null, node1);
                            childnode1.SetValue("IOID", socketiolist.Rows[y][0]);

                            childnode1.SetValue("Soket Adı", socketlist.Rows[i][1] + " : " + socketiolist.Rows[y][2]);

                            if (Convert.ToInt32(socketiolist.Rows[y][3]) != 0)
                            {
                                childnode1.SetValue("SoketIO", "Cihaz Pin : " + socketiolist.Rows[y][3]);

                            }

                        }
                        catch (Exception err)
                        {
                            Console.WriteLine("where to try catchi = " + err.StackTrace);

                        }
                    }
                }
                TreeListNode ComponentsNode = null;
                TreeListNode ResistorNode = null;
                TreeListNode CapacitorNode = null;
                TreeListNode SpliceNode = null;
                TreeListNode DiodeNode = null;
                TreeListNode GenericComponentNode = null;
                DataTable componentlist = manager.getMainComponents();//TO-DO!!!

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
                for (int i = 0; i < componentlist.Rows.Count; i++)
                {
                    TreeListNode childnode1;
                    TreeListNode childnode2;
                    switch (componentlist.Rows[i][2].ToString())
                    {
                        case "RESISTOR":

                            childnode1 = treeList1.AppendNode(null, ResistorNode);
                            childnode1.SetValue("Soket Adı", componentlist.Rows[i][1]);
                            childnode2 = treeList1.AppendNode(null, childnode1);
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
                Console.WriteLine(ex.StackTrace);

            }
        }

        private void tabPane1_SelectedPageIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("seçilen tab indexi = " + tabPane1.SelectedPageIndex);
            if (tabPane1.SelectedPageIndex == 1)
            {
                Where_to_treelist_lookup_Popup(sender, EventArgs.Empty);
                
                treeListLookUpEdit1_BeforePopup(sender, e);
            }
        }

        private void Colors_lookup_BeforePopup(object sender, EventArgs e)
        {
            Colors.DataSource = manager.getColors();
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
            treeListLookUpEdit1TreeList.ForceInitialize();

            try
            {
                DataTable socketlist = manager.getProjectSockets();
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

                    DataTable socketiolist = manager.getIObySocketID(socketlist.Rows[i][0].ToString());

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
                DataTable componentlist = manager.getMainComponents();//TO-DO!!!!
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
                for (int i = 0; i < componentlist.Rows.Count; i++)
                {
                    TreeListNode childnode1;
                    TreeListNode childnode2;
                    switch (componentlist.Rows[i][2].ToString())
                    {
                        case "RESISTOR":
                            childnode1 = treeListLookUpEdit1TreeList.AppendNode(null, ResistorNode);
                            childnode1.SetValue("Soket Adı", componentlist.Rows[i][1]);
                            childnode2 = treeListLookUpEdit1TreeList.AppendNode(null, childnode1);
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
                Console.WriteLine(ex.StackTrace);

            }
        }

        private void panel2_Enter(object sender, EventArgs e)
        {
           
        }

        private void tabPane1_TabIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tabPane1_TabStopChanged(object sender, EventArgs e)
        {
           
         
        }

        private void tabPane1_SelectedPageChanging(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangingEventArgs e)
        {
            gridControl7.DataSource = manager.getPhases();
            gridView7.FocusedRowHandle =0;


        }

        private void gridView7_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {          
            gridControl1.DataSource = manager.getConnectionbyPhase(gridView7.GetRowCellValue(gridView7.GetSelectedRows()[0], "ID_etap").ToString());
            
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {           
            using (askPhase form = new askPhase())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    //Create a property in SetIPAddressForm to return the input of user.                
                    manager.addPhase(form.phasename);
                    gridControl7.DataSource = manager.getPhases();
                    gridView7.FocusedRowHandle = 0;
                }
                else
                {
                    MessageBox.Show("İŞLEM İPTAL EDİLDİ");
                }
            }
        }

    

        private void edit_socket_pinnumber_EditValueChanged(object sender, EventArgs e)
        {

            if(new_socket_auto_assign_pin.Checked == true)
            {
                manager.setstartpointing(true);
                for (int i = 0; i < gridView4.RowCount; i++)
                {
                    if (gridView4.GetRowCellValue(gridView4.GetRowHandle(i), "IO PİNİ").ToString().Length > 0)
                    {
                        manager.manualpointing(true, Convert.ToInt32(gridView4.GetRowCellValue(gridView4.GetRowHandle(i), "IO PİNİ").ToString()));
                    }
                }
                foreach (DataRow row in assign_pin_datatable.Rows)
                {                   
                    //MessageBox.Show(manager.getIOPointNumber());
                    if (row[2].ToString().Length < 1)
                    {
                        row[2] = Convert.ToInt32(manager.getIOPointNumber());
                    }
                }
                manager.setstartpointing(false);
            }

            new_socket_auto_assign_pin.Checked = false;

        }

        private void edit_socket_switchnumber_EditValueChanged(object sender, EventArgs e)
        {
            edit_socket_pinnumber_EditValueChanged(this, e);
        }
        DataTable assign_pin_datatable;
        private void gridView6_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridControl4.Visible = true;
            new_socket_auto_assign_pin.Visible = true;
            try
            {
                Console.WriteLine(gridView6.GetRowCellValue(e.FocusedRowHandle, gridView6.Columns[0]).ToString());
                edit_socket_name.Text = gridView6.GetRowCellValue(e.FocusedRowHandle, gridView6.Columns[0]).ToString();
                edit_socket_lednumber.Text = gridView6.GetRowCellValue(e.FocusedRowHandle, gridView6.Columns[3]).ToString();
                edit_socket_pinnumber.Text = gridView6.GetRowCellValue(e.FocusedRowHandle, gridView6.Columns[1]).ToString();
                edit_socket_switchnumber.Text = gridView6.GetRowCellValue(e.FocusedRowHandle, gridView6.Columns[2]).ToString();
                Console.WriteLine("made it here");
                
                try
                {

                    if (gridView6.GetRowCellValue(gridView6.GetSelectedRows()[0], "ID_soket").ToString().Length != 0)
                    {
                        Console.WriteLine("up in the deneme");
                        Console.WriteLine(gridView6.GetRowCellValue(gridView6.GetSelectedRows()[0], "ID_soket").ToString());
                        assign_pin_datatable = manager.getIObySocketIDMapped(gridView6.GetRowCellValue(gridView6.GetSelectedRows()[0], "ID_soket").ToString());
                        Console.WriteLine(assign_pin_datatable.Rows.Count);

                            gridControl4.DataSource = assign_pin_datatable;
                            gridControl4.RefreshDataSource();
                    }
                }
                catch
                {
                    Console.WriteLine(" gridview catch");
                    edit_socket_pinnumber_EditValueChanged(this, e);
                }
            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message + " BURADA");
            }


        }

        private void edit_socket_save_button_Click(object sender, EventArgs e)
        {
            if(gridControl4.Visible == true && new_socket_auto_assign_pin.Visible == true)
            {
                string[,] arr2 = new string[3, 999];
                if (errorprovider_editsockets_isinputpopulated())
                {
                    object[] arr = new object[5];
                    if (gridView6.GetRowCellValue(gridView6.GetSelectedRows()[0], "ID_soket").ToString().Length != 0)
                    {
                        arr[0] = gridView6.GetRowCellValue(gridView6.GetSelectedRows()[0], "ID_soket").ToString();
                    }
                    arr[1] = (edit_socket_name.Text);
                    arr[2] = Convert.ToInt32(edit_socket_pinnumber.Text);
                    arr[3] = Convert.ToInt32(edit_socket_switchnumber.Text);
                    arr[4] = Convert.ToInt32(edit_socket_lednumber.Text);
                    for (int i = 0; i < gridView4.RowCount; i++)
                    {
                        for (int z = 0; z < gridView4.Columns.Count; z++)
                        {
                            arr2[z, i] = gridView4.GetRowCellValue(i, gridView4.Columns[z]).ToString();                          
                        }
                    }

                    Task.Factory.StartNew(() => manager.updateIObySocketID(arr[0].ToString(), arr2)).ContinueWith(delegate { refresh_socket_grids(); });//TODO-----YENİ KODA GEÇİRDİM AMA BURAYA GELMİYOR YUSUF BURAYA BAK
                   // navigationPane1.State = DevExpress.XtraBars.Navigation.NavigationPaneState.Collapsed;

                }
                else
                {
                    Console.WriteLine("hata yok--");
                }

            }
            else
            {
              
                string[,] arr2 = new string[3, 999];
                if (errorprovider_editsockets_isinputpopulated())
                {
                    object[] arr = new object[5];
                    if (registeredsocketgridview.GetRowCellValue(registeredsocketgridview.GetSelectedRows()[0], "ID_soket").ToString().Length != 0)
                    {
                        arr[0] = registeredsocketgridview.GetRowCellValue(registeredsocketgridview.GetSelectedRows()[0], "ID_soket").ToString();
                    }
                    arr[1] = (edit_socket_name.Text);
                    arr[2] = Convert.ToInt32(edit_socket_pinnumber.Text);
                    arr[3] = Convert.ToInt32(edit_socket_switchnumber.Text);
                    arr[4] = Convert.ToInt32(edit_socket_lednumber.Text);
                    /* for (int i = 0; i < gridView4.RowCount; i++)
                     {
                         for (int z = 0; z < gridView4.Columns.Count; z++)
                         {
                             arr2[z, i] = gridView4.GetRowCellValue(i, gridView4.Columns[z]).ToString();
                         }
                     }
                     */
                    Console.WriteLine("ÇAĞIRILDI - 861");
                    Task.Factory.StartNew(() => manager.updateIObySocketID(arr[0].ToString(), arr2)).ContinueWith(delegate { refresh_socket_grids(); });//TODO-----------------------------------
                   // navigationPane1.State = DevExpress.XtraBars.Navigation.NavigationPaneState.Collapsed;

                }
                else
                {
                    Console.WriteLine("hata yok");
                }
            }
        }

        private void registeredsocketgridview_Click(object sender, EventArgs e)
        {
            //gridView5_FocusedRowChanged(this, null);
        }

        private void gridView6_Click(object sender, EventArgs e)
        {
            //gridView6_FocusedRowChanged(this, null);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            maincomponentsource.DataSource = manager.getMainComponents();
            Console.WriteLine(manager.getMainComponents().Rows.Count);
            //DİĞER HEPSİ GİBİ YAPILMASINA RAĞMEN BU DATASOURCE VERİLERİ GÖSTERMİYOR !??!?
        }


        private void save_socket_data_button_Click(object sender, EventArgs e)
        {
            string socketid;
           
            if (gridView6.GetRowCellValue(gridView6.GetSelectedRows()[0], "ID_soket").ToString().Length != 0)
            {
                Console.WriteLine("2");

                socketid = gridView6.GetRowCellValue(gridView6.GetSelectedRows()[0], "ID_soket").ToString();
                Console.WriteLine("3");

                manager.updateSocketInfo(socketid, edit_socket_name.Text, edit_socket_pinnumber.Text, edit_socket_switchnumber.Text, edit_socket_lednumber.Text);
                Console.WriteLine("4");

                assign_pin_datatable = manager.getIObySocketIDMapped(gridView6.GetRowCellValue(gridView6.GetSelectedRows()[0], "ID_soket").ToString());
                Console.WriteLine("5");

                gridControl4.DataSource = assign_pin_datatable;
                Console.WriteLine("6");

                gridControl4.RefreshDataSource();
                Console.WriteLine("7");

            }

        }

        private void new_socket_auto_assign_pin_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void gridView4_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
        }

        private void gridView4_ShownEditor(object sender, EventArgs e)
        {
           
                
        }

        private void gridView4_HiddenEditor(object sender, EventArgs e)
        {
        }

  

        private void add_capacitor_value_multipler_Click(object sender, EventArgs e)
        {
            
        }

 

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataManager.TableUpdater updater = new DataManager.TableUpdater();

            foreach(string tablename in Enum.GetNames(typeof(MainTableName)))
            {
                updater.updateTable(manager.getFromLocalTablesmain(tablename), Databases.Main);

            }
            foreach (string tablename in Enum.GetNames(typeof(ProjectTableName)))
            {
                updater.updateTable(manager.getFromLocalTablesproject(tablename), Databases.Project);
            }
        }

 

        private void simpleButton12_Click(object sender, EventArgs e)
        {

           
        }

        private void edit_socket_lednumber_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
         
        }

        private void edit_socket_pinnumber_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Colors_lookup_EditValueChanged(object sender, EventArgs e)
        {
            //Colors_lookup.valu ID_color Color Name Hex Code
        }
        DataTable change_rowindex_phase_datatable;
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            change_rowindex_phase_datatable = manager.getPhases();
            DataRow row = change_rowindex_phase_datatable.Rows[gridView7.FocusedRowHandle];
            int new_focus_phases = MoveRow(row, "Up");
            gridControl7.DataSource = change_rowindex_phase_datatable;
            gridView7.FocusedRowHandle = new_focus_phases;

        }
        public int MoveRow(DataRow row, string direction)
        {
            DataRow oldRow = row;
            DataRow newRow = change_rowindex_phase_datatable.NewRow();

            newRow.ItemArray = oldRow.ItemArray;

            int oldRowIndex = change_rowindex_phase_datatable.Rows.IndexOf(row);

            if (direction =="Down")
            {
                int newRowIndex = oldRowIndex + 1;

                if (oldRowIndex < (change_rowindex_phase_datatable.Rows.Count))
                {
                    change_rowindex_phase_datatable.Rows.Remove(oldRow);
                    change_rowindex_phase_datatable.Rows.InsertAt(newRow, newRowIndex);
                    return change_rowindex_phase_datatable.Rows.IndexOf(newRow);
                }
            }

            if (direction == "Up")
            {
                int newRowIndex = oldRowIndex - 1;

                if (oldRowIndex > 0)
                {
                    change_rowindex_phase_datatable.Rows.Remove(oldRow);
                    change_rowindex_phase_datatable.Rows.InsertAt(newRow, newRowIndex);
                    return change_rowindex_phase_datatable.Rows.IndexOf(newRow);
                }
            }

            return 0;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

            change_rowindex_phase_datatable = manager.getPhases();
            DataRow row = change_rowindex_phase_datatable.Rows[gridView7.FocusedRowHandle];
            int new_focus_phases = MoveRow(row, "Down");
            gridControl7.DataSource = change_rowindex_phase_datatable;
            gridView7.FocusedRowHandle = new_focus_phases;
        }

        private void export_connections_button_Click(object sender, EventArgs e)
        {
            string filepath;
            if (xtraFolderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                filepath = xtraFolderBrowserDialog1.SelectedPath + "\\" + gridView7.GetFocusedRowCellValue(gridColumn2) + ".xlsx";
                var filetoexportedstream = System.IO.File.Create(filepath);
                filetoexportedstream.Close();
                gridControl1.ExportToXlsx(filepath);
                //exportphase(filepath);

            }
        }
        public void exportphase(string fullPath)
        {
            for (int numTries = 0; numTries < 10; numTries++)
            {
                try
                {
                    gridControl1.ExportToXlsx(fullPath);
                    break;
                }
                catch (IOException)
                {
                    Thread.Sleep(50);
                }
            }

        }
    }
}


