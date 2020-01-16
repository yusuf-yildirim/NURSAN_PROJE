﻿using System;
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
        private static bool initialize = true;
        public DataManager()
        {
            localTables = new LocalTables(initialize);
            initialize = false;
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
        public DataTable getProjectComponents()
        {
            return getFromLocalTablesproject("PComponents");
        }
        public DataTable getColors()
        {
            return getFromLocalTablesmain("Colours");
        }
        public DataTable getIObySocketIDMapped(string SocketID)
        {
            updateMappedIOTable(SocketID);
            return getFromLocalTablesproject("MAPPEDIO");      
        }
        public DataTable getIObySocketID(string SocketID)
        {
            return getFromLocalTablesproject("PIO_connection").Select("ID_soket ='" + SocketID + "'").CopyToDataTable();
        }
        public DataTable getConnectionbyPhase(string phaseid)
        {
            update_ConnectionTable(phaseid);
            return getFromLocalTablesproject("CONNECTIONS");
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
