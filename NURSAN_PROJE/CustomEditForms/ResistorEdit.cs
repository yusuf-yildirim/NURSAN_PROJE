using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NURSAN_PROJE.SQL;

namespace NURSAN_PROJE
{
    public partial class ResistorEdit : DevExpress.XtraEditors.XtraUserControl
    {

        private DataManager manager;
        public ResistorEdit(string componentID)
        {
            InitializeComponent();
            manager = new DataManager(true);           
            var rows = manager.getFromLocalTablesproject("PComponents").Select("ID_component = '"+componentID+"'")[0];  
            updateresistorname.EditValue = rows[1].ToString();
            updateresistorvalue.EditValue = rows[3].ToString();
            updateresistortolerence.EditValue = rows[5].ToString();
        }
    }
}
