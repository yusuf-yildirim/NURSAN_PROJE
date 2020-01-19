using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NURSAN_PROJE
{
    public partial class Projectwindow : DevExpress.XtraBars.ToolbarForm.ToolbarForm
    {

        public void addMainSocket()
        {
            string[,] arr2 = new string[3, 999];
          
                object[] arr = new object[5];

                arr[0] = System.Guid.NewGuid().ToString();
                arr[1] = (newsocketname.Text);
                arr[2] = Convert.ToInt32(newsocketpinc.Text);
                arr[3] = Convert.ToInt32(newsocketswc.Text);
                arr[4] = Convert.ToInt32(newsocketledc.Text);

                /*
                for (int i = 0; i < gridView4.RowCount; i++)
                {
                    for (int z = 0; z < gridView4.Columns.Count; z++)
                    {
                        arr2[z, i] = gridView4.GetRowCellValue(i, gridView4.Columns[z]).ToString();
                    }
                }
                */

                Task.Factory.StartNew(() => manager.addSocket(arr)).ContinueWith(delegate { refresh_socket_grids(); });
                navigationPane1.State = DevExpress.XtraBars.Navigation.NavigationPaneState.Collapsed;

                foreach (TextEdit t in newsocketvargroup.Controls)
                {
                    t.Text = "";
                }
        }

        public void updateProjectSocket()
        {
            string socketid;

            if (gridView6.GetRowCellValue(gridView6.GetSelectedRows()[0], "ID_soket").ToString().Length != 0)
            {
                socketid = gridView6.GetRowCellValue(gridView6.GetSelectedRows()[0], "ID_soket").ToString();               
                manager.updateSocketInfo(socketid, edit_socket_name.Text, edit_socket_pinnumber.Text, edit_socket_switchnumber.Text, edit_socket_lednumber.Text);
                assign_pin_datatable = manager.getIObySocketIDMapped(gridView6.GetRowCellValue(gridView6.GetSelectedRows()[0], "ID_soket").ToString());
                gridControl4.DataSource = assign_pin_datatable;               
                gridControl4.RefreshDataSource();              
            }


        }
        private void gridView2_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string componentID = gridView2.GetRowCellValue(gridView2.GetSelectedRows()[0], "ID_component").ToString();
            if (gridView2.GetRowCellValue(gridView2.GetSelectedRows()[0], "Tur").ToString() == "RESISTOR")
            {
                gridView2.OptionsEditForm.CustomEditFormLayout = new ResistorEdit(componentID);
            }
            else
            {
                gridView2.OptionsEditForm.CustomEditFormLayout = new CapacitorEdit();
            }
        }

    
    }
}
