using System;
using System.Collections.Generic;
using System.Data;
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
      
        public DataTable getMainSocket()
        {
            return LocalTables.localtables.maintables.Tables["Sockets"];
        }
        public DataTable getProjectSocket()
        {
            return LocalTables.localtables.projecttables.Tables["PSockets"];
        }
        public void getSocketImage()
        {

        }
        public void getPhase()
        {

        }
        public void getComponent()
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
