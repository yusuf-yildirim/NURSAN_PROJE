﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NURSAN_PROJE.SQL
{

    class LocalTables : IDisposable
    {
       
        static SQLiteConnection con;
        static SQLiteDataAdapter adapter;
        static SQLiteCommand cmd;
        DBeng database = new DBeng();
        public static LocalTablesStruct localtables;
        public LocalTables(bool initialize)
        {
            if (initialize)
            {
                localtables = new LocalTablesStruct();
                localtables.initialize();
                getalltables();
            }
            else
            {
                Console.WriteLine("Daha önceden initialize edildiğinden emin olun!");
            }
           
        }

        public struct LocalTablesStruct//TO-DO
        {
            public DataSet projecttables;
            public DataSet maintables;
            int maxorder;        
           
            public void initialize()
            {
                projecttables = new DataSet();
                maintables = new DataSet();
                maxorder = 0;               
            }
        };

        ///<summary>
        ///Local tableları veritabanından doldurur.
        ///</summary>
        public void getalltables()
        {
            foreach (string tablename in Enum.GetNames(typeof(MainTableName)))
            {
                if(localtables.maintables.Tables.Contains(tablename))
                {                  
                    localtables.maintables.Tables.Remove(tablename);
                    localtables.maintables.Tables.Add(database.GetDataTable(tablename, Databases.Main));
                }
                else
                {
                    localtables.maintables.Tables.Add(database.GetDataTable(tablename, Databases.Main));
                }               
            }
            foreach (string tablename in Enum.GetNames(typeof(ProjectTableName)))//TO-DO
            {
                if (localtables.projecttables.Tables.Contains(tablename))
                {                   
                    localtables.projecttables.Tables.Remove(tablename);
                    localtables.projecttables.Tables.Add(database.GetDataTable(tablename, Databases.Project));
                }
                else
                {
                    localtables.projecttables.Tables.Add(database.GetDataTable(tablename, Databases.Project));
                }                    
            }
        }

        public void updateTable(DataTable table,Databases database)
        {
            this.database.SaveDataTable(table, database);
        }

        ///<summary>
        ///İstenen veritabanıyla ilgili tabloyu döndürür.
        ///</summary>
        public DataTable getSpecifiedTableFromDatabase(string tablename, Databases type)
        {
            return database.GetDataTable(tablename, type);
        }

        public DataTable getLocalTable(string tablename, Databases type)
        {
            if(type == Databases.Main)
            {
               return localtables.maintables.Tables[tablename];
            }
            else if (type == Databases.Project)
            {
                return localtables.projecttables.Tables[tablename];
            }
            else
            {
                return null;              
            }
        }
        public void addLocalTable(DataTable table, Databases type)
        {
            if (type == Databases.Main)
            {
                localtables.maintables.Tables.Add(table);
            }
            else if (type == Databases.Project)
            {
                localtables.projecttables.Tables.Add(table);
            }
            else
            {
                MessageBox.Show("HATA");
            }
          
        }
        public void removeLocalTable(string tablename,Databases type)
        {

            if (type == Databases.Main)
            {
                localtables.maintables.Tables.Remove(tablename);
            }
            else if (type == Databases.Project)
            {
                localtables.projecttables.Tables.Remove(tablename);
            }
            else
            {
                MessageBox.Show("HATA");
            }
        }


        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        ~LocalTables()
        {
            Dispose(false);
        }



    }
}
