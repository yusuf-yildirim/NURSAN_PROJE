using NURSAN_PROJE.Configurator;
using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NURSAN_PROJE.SQL
{
    class DBeng : IDisposable
    {
       // mainsource x = new mainsource();
        static SQLiteConnection con;
        static SQLiteDataAdapter da;
        static SQLiteCommand cmd;
        public static bool created = false;
        Config getconstring = new Config();

        public void attachDatabase()
        {
            //TO-DO         
            //PROJE VERİ TABANINI ANA VERİTABANINA BAĞLAYIP İŞLEMLERİ TRİGGERLAR İLE HIZLANDIRMAK İÇİN KULLANILACAK
            //SOKETLER İÇİN İŞLEMLER DÜZENLENECEK
            //KOMPONENTLER İÇİN İŞLEMLER DÜZENLENECEK
        }





        ///<summary>
        ///Proje dosyasını yaratır ve ilk ayarlamaları db.ini dosyasına göre yapar.
        ///</summary>
        public void create_project(string name)
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                connectionStringsSection.ConnectionStrings["tablo"].ConnectionString = @"XpoProvider=SQLite;Data Source=" + Application.StartupPath + "\\" + name + ".nursan";
                // MessageBox.Show(Application.StartupPath + "\\" + name + ".nursan");
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");
                SQLiteConnection m_dbConnection = new SQLiteConnection(@"XpoProvider=SQLite;Data Source=" + Application.StartupPath + "\\" + name + ".nursan");
                m_dbConnection.Open();
                string sql = File.ReadAllText(@"db.ini");
                SQLiteCommand command = new SQLiteCommand(commandText: sql, connection: m_dbConnection);
                command.ExecuteNonQuery();              
                m_dbConnection.Close();
                created = true;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }


        public void setProjectPath(string path)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings["tablo"].ConnectionString = @"XpoProvider=SQLite;Data Source=" +path;
            config.Save();
        }
        ///<summary>
        ///Çağırıldığı yere istenen tabloyu istenen veritabanından DataTable olarak döndürür.
        ///</summary>
        public DataTable GetDataTable(string tablename, Databases db)
        {           
            if (db == Databases.Main)
            {
                con = new SQLiteConnection("Data Source=" + Application.StartupPath + "\\tablo.db;Version=3;");
               
                DataTable DT = new DataTable();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format("SELECT * FROM {0}", tablename);
                da = new SQLiteDataAdapter(cmd);
                da.AcceptChangesDuringFill = false;
                da.Fill(DT);
                con.Close();
                DT.TableName = tablename;
                foreach (DataRow row in DT.Rows)
                {
                    row.AcceptChanges();
                }
                return DT;
            }
            else if(db == Databases.Project)
            {
                con = new SQLiteConnection(getconstring.get_conn_string("tablo") + ";Version=3;");
                Console.WriteLine(getconstring.get_conn_string("tablo") + ";Version=3;");
                DataTable DT = new DataTable();
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = string.Format("SELECT * FROM {0}", tablename);
                da = new SQLiteDataAdapter(cmd);
                da.AcceptChangesDuringFill = false;
                try
                {
                    da.Fill(DT);
                }
                catch
                {
                    Console.WriteLine("WARNING TABLE NOT FOUND!");
                }
                con.Close();
                DT.TableName = tablename;
                foreach (DataRow row in DT.Rows)
                {
                    row.AcceptChanges();
                }
                return DT;
            }
            else
            {
                MessageBox.Show("UNEXPECTED ERROR");
                return null;
            }

        }

        ///<summary>
        ///İstenen tabloyu istenen veritabanıyla senkronize eder.
        ///</summary>
        public void SaveDataTable(DataTable DT, Databases db)
        {
            if (db == Databases.Main)
            {
                con = new SQLiteConnection("Data Source=" + Application.StartupPath + "\\tablo.db;Version=3;");
                try
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = string.Format("SELECT * FROM {0}", DT.TableName);
                    da = new SQLiteDataAdapter(cmd);
                    SQLiteCommandBuilder builder = new SQLiteCommandBuilder(da);
                    da.Update(DT);
                    con.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }


            }
            else if(db == Databases.Project)
            {
                con = new SQLiteConnection(getconstring.get_conn_string("tablo") + ";Version=3;");
                Console.WriteLine("Proje veritabanı " +DT.TableName.ToString() + " tablosu "+DT.Rows.Count.ToString() + " tane satır ile güncelleniyor!");
                try
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = string.Format("SELECT * FROM {0}", DT.TableName);
                    da = new SQLiteDataAdapter(cmd);
                    SQLiteCommandBuilder builder = new SQLiteCommandBuilder(da);                    
                    da.Update(DT);                    
                    con.Close();                    
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    Console.WriteLine(Ex.Message);
                }
            }
            else
            {
                MessageBox.Show("UNEXPECTED ERROR");
                Console.WriteLine("UNEXPECTED ERROR");
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

        ~DBeng()
        {
            Dispose(false);
        }

    }

}
