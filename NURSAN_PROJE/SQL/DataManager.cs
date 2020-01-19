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
        private static LocalTables localTables;
        private const bool initialize = true;
        private static bool initialized = false;
        public DataManager()
        {
            if(initialized == false)
            {
                localTables = new LocalTables(initialize);
                initialized = true;                
            }
            else
            {
               // localTables = localTables;
            }                  
        }
        public DataManager(bool temp)
        {
            localTables = new LocalTables(false);
        }
        ///<summary>
        ///Kullanıcıya gösterilecek main veritabanı soketlerini DataTable olarak döndürür.
        ///</summary>
        public DataTable getMainSockets()
        {            
            return getFromLocalTablesmain("Sockets");
        }
        ///<summary>
        ///Kullanıcıya gösterilecek proje veritabanı soketlerini DataTable olarak döndürür.
        ///</summary>
        public DataTable getProjectSockets()
        {
            return getFromLocalTablesproject("PSockets");
        }
        ///<summary>
        ///İlgili SocketID ye ait resmi Image olarak döndürür.
        ///</summary>
        public Image getSocketImage(String SocketID)
        {
           return blob2Image((byte[])getFromLocalTablesmain("ImageStore").Select("ID_soket ='" + SocketID + "'").ElementAt(0).ItemArray.ElementAt(2));
        }
        ///<summary>
        ///Kullanıcıya gösterilecek faz tablosunu DataTable olarak döndürür.
        ///</summary>
        public DataTable getPhases()
        {
            return getFromLocalTablesproject("tbl_etap");
        }
        ///<summary>
        ///Kullanıcıya gösterilecek komponent tablosunu DataTable olarak döndürür.
        ///</summary>
        public DataTable getMainComponents()
        {
            return getFromLocalTablesmain("Components");
        }
        ///<summary>
        ///Kullanıcıya gösterilecek proje komponentleri tablosunu DataTable olarak döndürür.
        ///</summary>
        public DataTable getProjectComponents()
        {
            return getFromLocalTablesproject("PComponents");
        }
        ///<summary>
        ///Kullanıcıya gösterilecek renk tablosunu DataTable olarak döndürür.
        ///</summary>
        public DataTable getColors()
        {
            return getFromLocalTablesmain("Colours");           
        }        
        ///<summary>
        ///Kullanıcıya gösterilecek IO soketleri düzenlenmiş DataTable olarak döndürür.
        ///</summary>
        public DataTable getIObySocketIDMapped(string SocketID)
        {
            updateMappedIOTable(SocketID);
            return getFromLocalTablesproject("MAPPEDIO");      
        }
        ///<summary>
        ///Kullanıcıya gösterilecek IO soketleri ham ve DataTable olarak döndürür.
        ///</summary>
        public DataTable getIObySocketID(string SocketID)
        {
            return getFromLocalTablesproject("PIO_connection").Select("ID_soket ='" + SocketID + "'").CopyToDataTable();
        }
        ///<summary>
        ///Kullanıcıya gösterilecek bağlantıları ilgili faza göre  DataTable olarak döndürür.
        ///</summary>
        public DataTable getConnectionbyPhase(string phaseid)
        {
            update_ConnectionTable(phaseid);
            return getFromLocalTablesproject("CONNECTIONS");
        }
        ///<summary>
        ///İstenen tabloyu istenen veri tabanından DataTable olarak döndürür.
        ///</summary>
        public DataTable getDatabaseTable(string tablename, Databases type)
        {
            return localTables.getSpecifiedTableFromDatabase(tablename, type);
        }

        public DataTable getRecent()
        {
            return getFromLocalTablesmain("Recent");
        }
        ///<summary>
        ///Son açılan dosya kaydını gerçekleştirir.
        ///</summary>
        public void createRecent(String path)//TO-DO CHANGE
        {            
            string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            if(getFromLocalTablesmain("Recent").Select("PATH = '"+path+"'").Length > 0)
            {
                getFromLocalTablesmain("Recent").Select("PATH = '" + path + "'")[0][3] = date;
                localTables.updateTable(getFromLocalTablesmain("Recent"), Databases.Main);
            }
            else
            {
                getFromLocalTablesmain("Recent").Rows.Add(Guid.NewGuid().ToString(), path.Split('\\')[(path.Split('\\')).Count() - 1], path, date);
                localTables.updateTable(getFromLocalTablesmain("Recent"), Databases.Main);
            }
            
        }
    
        public class TableUpdater
        {
         
            ///<summary>
            ///İstenen tabloyu istenen veri tabanında senkronize eder.
            ///</summary>
            public void updateTable(DataTable table,Databases db)
            {
                DataManager.localTables.updateTable(table, db);
            }

        }
    }
}
