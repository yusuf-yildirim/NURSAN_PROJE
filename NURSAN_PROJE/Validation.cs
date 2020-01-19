using DevExpress.XtraEditors;
using NURSAN_PROJE.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                    maintablegrid.DataSource = manager.getFromLocalTablesmain(maintableslist.SelectedValue.ToString());
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
                    projecttablegrid.DataSource = manager.getFromLocalTablesproject(projecttablelist.SelectedValue.ToString());
                }
            }
        }

        private void maintablesyncbutton_Click(object sender, EventArgs e)
        {
            if (maintableslist.SelectedValue.ToString().Length > 0)
            {
                updater.updateTable(manager.getFromLocalTablesmain(maintableslist.SelectedValue.ToString()), Databases.Main);
            }
        }
        private void projecttablesyncbutton_Click(object sender, EventArgs e)
        {
            if (projecttablelist.SelectedValue.ToString().Length > 0)
            {
                updater.updateTable(manager.getFromLocalTablesproject(projecttablelist.SelectedValue.ToString()), Databases.Project);
            }
        }
        private void initializebutton_Click(object sender, EventArgs e)
        {
            initializedatabasescreen();
        }
        private void checkEdit1_CheckStateChanged(object sender, EventArgs e)
        {
        
        }





        bool addsocketvalidateresult = true;
        
        //TO-DO
        private void newsocketname_Properties_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {          
            if (newsocketname.Text.Length > 0)
            {
                if (manager.checknameAvailability(newsocketname.Text,Databases.Main))
                {
                    e.Cancel = false;
                    addsocketvalidateresult = false;
                   
                }
                else
                {
                    
                    newsocketname.ErrorText = "Bu isim daha önce kullanılmış!";
                    e.Cancel = true;
                    addsocketvalidateresult = true;
                    
                }
            }
            else
            {
                newsocketname.ErrorText = "Bu alan boş bırakılamaz!";
                e.Cancel = true;
                addsocketvalidateresult = true;
                
                
            }
          
        }
        private void newsocketpinc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Convert.ToInt32(newsocketpinc.Text);
                if (Convert.ToInt32(newsocketpinc.Text) <= 0)
                {
                    newsocketpinc.ErrorText = "Lütfen bu alana sıfır dan büyük bir sayı giriniz!";
                    e.Cancel = true;
                    addsocketvalidateresult = true;
                   

                }
                else
                {
                    e.Cancel = false;
                    if (addsocketvalidateresult == false)
                        addsocketvalidateresult = false;

                    
                }
            }
            catch
            {
                newsocketpinc.ErrorText = "Lütfen bu alana bir sayı giriniz!";
                e.Cancel = true;                
                addsocketvalidateresult = true;
                
                
            }
         

        }
        private void newsocketledc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Convert.ToInt32(newsocketledc.Text);
                if (manager.checklednumberAvailability(newsocketledc.Text,Databases.Main))
                {
                    e.Cancel = false;
                    if (addsocketvalidateresult == false)
                        addsocketvalidateresult = false;                 
                    
                }
                else
                {
                    e.Cancel = true;
                    addsocketvalidateresult = true;                    
                    newsocketledc.ErrorText = "Bu led numarası daha önce kullanılmış!";
                    
                }
            }
            catch
            {
                e.Cancel = true;
                addsocketvalidateresult = true;                
                newsocketledc.ErrorText = "Lütfen bu alana bir sayı giriniz";
                
            }
         
        }
        private void newsocketswc_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Convert.ToInt32(newsocketswc.Text);
                if (Convert.ToInt32(newsocketswc.Text) > 0)
                {
                    e.Cancel = false;    
                    if(addsocketvalidateresult == false)
                    addsocketvalidateresult = false;    
                   
                }
                else
                {
                    e.Cancel = true;
                    addsocketvalidateresult = true;
                    newsocketswc.ErrorText = "Lütfen bu alana sıfır dan büyük bir sayı giriniz!";
                }
            }
            catch
            {
                e.Cancel = true;
                addsocketvalidateresult = true;
                newsocketswc.ErrorText = "Lütfen bu alana bir sayı giriniz";
            }
        }
        private bool errorprovider_editsockets_isinputpopulated()
        {
            bool exit_code = true;
            foreach (TextEdit t in updatesoketgroup.Controls)
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
        //-*-*-*-*--*-*--*---*-*!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        private void edit_socket_name_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (edit_socket_name.Text.Length > 0)
            {
                if (manager.checknameAvailability(edit_socket_name.Text,Databases.Project) || edit_socket_name.Text == edit_socket_name.OldEditValue.ToString())
                {
                    e.Cancel = false;
                    if(updatesocketvaliteresult == true)
                    updatesocketvaliteresult = true;
                }
                else
                {
                    edit_socket_name.ErrorText = "Bu isim daha önce kullanılmış!";
                    e.Cancel = true;
                    updatesocketvaliteresult = false;

                }
            }
            else
            {
                edit_socket_name.ErrorText = "Bu alan boş bırakılamaz!";
                e.Cancel = true;
                updatesocketvaliteresult = false;
            }
        }

        private void edit_socket_switchnumber_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Convert.ToInt32(edit_socket_switchnumber.Text);
                if (Convert.ToInt32(edit_socket_switchnumber.Text) > 0)
                {
                    e.Cancel = false;
                    if (updatesocketvaliteresult == true)
                        updatesocketvaliteresult = true;

                }
                else
                {
                    e.Cancel = true;
                    updatesocketvaliteresult = false;
                    edit_socket_switchnumber.ErrorText = "Lütfen bu alana sıfır dan büyük bir sayı giriniz!";
                }
            }
            catch
            {
                e.Cancel = true;
                updatesocketvaliteresult = false;
                edit_socket_switchnumber.ErrorText = "Lütfen bu alana bir sayı giriniz";
            }
        }
        private void edit_socket_lednumber_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
                try
                {
                    Convert.ToInt32(edit_socket_lednumber.Text);               
                    if (manager.checklednumberAvailability(edit_socket_lednumber.Text, Databases.Project) || edit_socket_lednumber.Text == edit_socket_lednumber.OldEditValue.ToString())
                    {
                        e.Cancel = false;
                    if (updatesocketvaliteresult == true)
                        updatesocketvaliteresult = true;

                    }
                    else
                    {
                        e.Cancel = true;
                        updatesocketvaliteresult = false;
                        edit_socket_lednumber.ErrorText = "Bu led numarası daha önce kullanılmış!";

                    }
                }
                catch (Exception ex)
                {
                    e.Cancel = true;
                    updatesocketvaliteresult = false;
                    //MessageBox.Show(ex.Message);
                    edit_socket_lednumber.ErrorText = "Lütfen bu alana bir sayı giriniz";

                }
            }
           

         
        

        private void edit_socket_pinnumber_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Convert.ToInt32(edit_socket_pinnumber.Text);
                if (Convert.ToInt32(edit_socket_pinnumber.Text) <= 0)
                {
                    edit_socket_pinnumber.ErrorText = "Lütfen bu alana sıfır dan büyük bir sayı giriniz!";
                    e.Cancel = true;
                   
                        updatesocketvaliteresult = false;


                }
                else
                {
                    e.Cancel = false;
                    if (updatesocketvaliteresult == true)
                        updatesocketvaliteresult = true;


                }
            }
            catch
            {
                edit_socket_pinnumber.ErrorText = "Lütfen bu alana bir sayı giriniz!";
                e.Cancel = true;
                updatesocketvaliteresult = false;
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


     
    }

}
