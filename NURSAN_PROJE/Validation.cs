using NURSAN_PROJE.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NURSAN_PROJE
{
    public partial class Projectwindow : DevExpress.XtraBars.ToolbarForm.ToolbarForm
    {
        // FOR MANUAL VALIDITION START
        private void gridView4_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "IO PİNİ")
            {
                edit_socket_save_button.Enabled = true;
                validate = true;

            }
        }
        // FOR MANUAL VALIDITION START
        private void gridView4_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //MessageBox.Show("Değişiyor");
            if (e.Column.FieldName == "IO PİNİ")
            {
                validate = true;
                edit_socket_save_button.Enabled = false;
            }
        }

       
        bool validate = false;
        private void gridView4_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (validate == true)
            {
                for (int i = 0; i < gridView4.RowCount; i++)
                {
                    if (gridView4.GetRowCellValue(gridView4.GetRowHandle(i), "IO PİNİ").ToString().Length > 0)
                    {
                        manager.manualpointing(true, Convert.ToInt32(gridView4.GetRowCellValue(gridView4.GetRowHandle(i), "IO PİNİ").ToString()));
                    }
                }
                if (e.Value.ToString().Length > 0)
                {
                    if (!manager.checkIOpoint(e.Value.ToString()))
                    {
                        // XtraMessageBox.Show("Yazılan IO zaten kullanımda" + gridView4.GetRowCellValue(gridView6.GetSelectedRows()[0], "IO PİNİ").ToString(), "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Valid = false;
                        e.ErrorText = "Test noktası daha önce " + manager.checkIOpointWhoUse(e.Value.ToString(), edit_socket_name.Text) + " isimli soket tarafından kullanılmış!";
                    }
                    else
                    {
                        e.Valid = true;
                    }
                }
                else
                {
                    e.Valid = true;
                }
            }
            manager.manualpointing(false, 0);//IMPORTANT!
            gridView4.CloseEditor();
            gridView4.UpdateCurrentRow();
        }


        //TO-DO
        private void newsocketname_Properties_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
           // e.Cancel = true;
        }

     


        public void initializedatabasescreen()
        {          
                maintableslist.Items.AddRange(Enum.GetNames(typeof(MainTableName)));
                projecttablelist.Items.AddRange(Enum.GetNames(typeof(ProjectTableName)));     
        }
        private void loadmaintablebutton_Click(object sender, EventArgs e)
        {
            if(maintableslist.SelectedValue.ToString().Length > 0)
            {
                if (checkEdit1.Checked == true)
                {
                    maintablegrid.DataSource = null;
                    maintablegridview.Columns.Clear();
                    maintablegrid.DataSource = manager.getDatabaseTable(maintableslist.SelectedValue.ToString(), Databases.Main);
                }
                else
                {
                    maintablegrid.DataSource = null;
                    maintablegridview.Columns.Clear();
                    maintablegrid.DataSource = LocalTables.localtables.maintables.Tables[maintableslist.SelectedValue.ToString()];
                }               
            }           
        }
        private void loadprojecttablebutton_Click(object sender, EventArgs e)
        {
            if (projecttablelist.SelectedValue.ToString().Length > 0)
            {
                if (checkEdit2.Checked == true)
                {
                    projecttablegrid.DataSource = null;
                    projecttablegridview.Columns.Clear();
                    projecttablegrid.DataSource = manager.getDatabaseTable(projecttablelist.SelectedValue.ToString(), Databases.Project);
                }
                else
                {
                    projecttablegrid.DataSource = null;
                    projecttablegridview.Columns.Clear();
                    projecttablegrid.DataSource = LocalTables.localtables.projecttables.Tables[projecttablelist.SelectedValue.ToString()];
                }
            }
        }

        private void maintablesyncbutton_Click(object sender, EventArgs e)
        {
            if (maintableslist.SelectedValue.ToString().Length > 0)
            {
                updater.updateTable(LocalTables.localtables.maintables.Tables[maintableslist.SelectedValue.ToString()], Databases.Main);
            }
        }
        private void projecttablesyncbutton_Click(object sender, EventArgs e)
        {
            if (projecttablelist.SelectedValue.ToString().Length > 0)
            {
                updater.updateTable(LocalTables.localtables.projecttables.Tables[projecttablelist.SelectedValue.ToString()], Databases.Project);
            }
        }
        private void initializebutton_Click(object sender, EventArgs e)
        {
            initializedatabasescreen();
        }
        private void checkEdit1_CheckStateChanged(object sender, EventArgs e)
        {
        
        }
    }

}
