using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return LocalTables.localtables.maintables.Tables["Sockets"];
        }
        public DataTable getProjectSockets()
        {
            return LocalTables.localtables.projecttables.Tables["PSockets"];
        }
        public Image getSocketImage(String SocketID)
        {
           return blob2Image((byte[])LocalTables.localtables.maintables.Tables["ImageStore"].Select("ID_soket ='" + SocketID + "'").ElementAt(0).ItemArray.ElementAt(2));
        }
        public void getPhases()
        {

        }
        public void getComponents()
        {

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
