using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NURSAN_PROJE.SQL
{
    partial class DataManager
    {
        LocalTables localTables;
        public DataManager()
        {
            localTables = new LocalTables(true);            
        }
      
        public DataTable getMainSockets()
        {            
            return getFromLocalTablesmain("Sockets");
        }
        public DataTable getProjectSockets()
        {
            return getFromLocalTablesproject("PSockets");

        }
        public Image getSocketImage(String SocketID)
        {
           return blob2Image((byte[])getFromLocalTablesmain("ImageStore").Select("ID_soket ='" + SocketID + "'").ElementAt(0).ItemArray.ElementAt(2));
        }
        public DataTable getPhases()
        {
            return getFromLocalTablesproject("tbl_etap");
        }        
        public DataTable getMainComponents()
        {
            return getFromLocalTablesmain("Components");
        }
        public DataTable getColors()
        {
            return getFromLocalTablesmain("Colours");
        }
        public DataTable getIObySocketID(string SocketID)
        {
            MessageBox.Show(SocketID);
            return getFromLocalTablesproject("PIO_connection").Select("ID_soket ='" + SocketID + "'").CopyToDataTable();      
        }



      



        public class TableUpdater
        {
            public DataManager parent;

            public TableUpdater(DataManager parent)
            {
                this.parent = parent;
            }

            public void updateTable(DataTable table,Databases db)
            {
                parent.localTables.updateTable(table, db);
            }
        }
    }
}
