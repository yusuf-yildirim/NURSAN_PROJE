﻿using NURSAN_PROJE.Configurator;
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
    class DBeng
    {
        mainsource x = new mainsource();
        static SQLiteConnection con;
        static SQLiteDataAdapter da;
        static SQLiteCommand cmd;
        static DataSet ds;
        public static bool created = false;
        Config getconstring = new Config();


        public DBeng()//TO-DO
        {
           
        }
        public void attachDatabase()
        {
            //TO-DO         
            //PROJE VERİ TABANINI ANA VERİTABANINA BAĞLAYIP İŞLEMLERİ TRİGGERLAR İLE HIZLANDIRMAK İÇİN KULLANILACAK
            //SOKETLER İÇİN İŞLEMLER DÜZENLENECEK
            //KOMPONENTLER İÇİN İŞLEMLER DÜZENLENECEK
        }
        private void getmaincon()
        {
            con = new SQLiteConnection("Data Source=" + Application.StartupPath + "\\tablo.db;Version=3;");
            con.Open();
            tmplog.WriteDebugLog("Ana veritabanı bağlantısı açıldı");
        }
        private SQLiteConnection _getmaincon()
        {
            return con = new SQLiteConnection("Data Source=" + Application.StartupPath + "\\tablo.db;Version=3;");
        }
        private void getprojectcon()
        {
            con = new SQLiteConnection(getconstring.get_conn_string("tablo") + ";Version=3;");
            con.Open();
            tmplog.WriteDebugLog("Proje veritabanı bağlantısı açıldı");
        }

        public void connection_add(string origin, string origintype, string destination, string destinationtype, string colorıd,string phaseid)//güncelleme gerekir
        {
            getprojectcon();
            cmd = new SQLiteCommand();
            cmd.Connection = con;
            cmd.CommandText = "insert into PConnections(ID_connection,\"Order\",Origin,OriginType,Destination,DestinationType,Id_color,ID_etap_1) values ('" + Guid.NewGuid().ToString() + "','" + getlastconnectionorder(phaseid) + "','" + origin + "','" + origintype + "','" + destination + "','" + destinationtype + "','" + colorıd + "','" + phaseid + "')";
            Console.WriteLine(cmd.CommandText);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public string getlastconnectionorder(string phaseid)//güncelleme gerekir
        {
            getprojectcon();
            DataTable Orders = new DataTable();
            da = new SQLiteDataAdapter("SELECT MAX(\"Order\") FROM PConnections Where ID_etap_1= '"+phaseid+"'",con);
            SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(Orders);

            try
            {
                con.Close();
                return (Convert.ToInt32(Orders.Rows[0][0]) + 1).ToString();
            }
            catch
            {
                con.Close();
                return "1";

            }

        }

        public DataTable get_ConnectionTable(string phaseid)
        {
            DataTable connections = new DataTable("CONNECTİONS");
            connections.Columns.Add("NEREDEN");
            connections.Columns.Add("NEREYE");
            connections.Columns.Add("KABLO KONTROL");
            connections.Columns.Add("WireColor");
            connections.Columns.Add("ÖZELLİKLER");
            tmplog.WriteDebugLog("----------BAĞLANTI LİSTELEME BAŞLADI----------", false);
            getprojectcon();
            ds = new DataSet();
            da = new SQLiteDataAdapter("SELECT * FROM PConnections Where ID_etap_1= '" + phaseid + "' ORDER BY \"Order\" ASC", con);
            SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string origin, destination, color;
                if (ds.Tables[0].Rows[i][3].ToString() == "SOCKET")
                {
                    origin = getIOInfo(ds.Tables[0].Rows[i][2].ToString());
                }
                else
                {
                    origin = getComponentInfo(ds.Tables[0].Rows[i][2].ToString());
                }
                if (ds.Tables[0].Rows[i][5].ToString() == "SOCKET")
                {
                    destination = getIOInfo(ds.Tables[0].Rows[i][4].ToString());
                }
                else
                {
                    destination = getComponentInfo(ds.Tables[0].Rows[i][4].ToString());
                }
                color = getColorInfo(ds.Tables[0].Rows[i][6].ToString());
                connections.Rows.Add(origin, destination, color, color, color);  //TO-DO
            }
            return connections;
        }

  
        private string getSocketNameInfo(String SocketID)
        {

           
                getprojectcon();                
                DataTable table = new DataTable();
                da = new SQLiteDataAdapter("select * from PSockets where ID_soket = '" + SocketID + "'", con);
                SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
                da.Fill(table);
                return table.Rows[0][1].ToString();
         

        }
        private string getIOInfo(String ıoID)
        {
            try
            {
                string answ = null;
                getprojectcon();
                DataTable table = new DataTable();
                da = new SQLiteDataAdapter("select * from PIO_connection where ID_IO = '" + ıoID + "'", con);
                SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
                da.Fill(table);
                answ += getSocketNameInfo(table.Rows[0][1].ToString()) + " : " + table.Rows[0][2];
                return answ;
            }
            catch
            {
                return "HATA";
            }

        }
        private string getComponentInfo(String componenetID)
        {
            getprojectcon();
            DataTable table = new DataTable();
            da = new SQLiteDataAdapter("select * from PComponents where ID_component = '" + componenetID + "'", con);
            SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(table);
            return table.Rows[0][1].ToString();
        }
        private string getColorInfo(String ColorID)
        {
            string color;
            con.Close();
            getmaincon();
            DataTable table = new DataTable();
            da = new SQLiteDataAdapter("select * from Colours where ID_color = '" + ColorID + "'", con);
            SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(table);
            Console.WriteLine("BULUNAN RENK : " + table.Rows[0][1].ToString());
            color = table.Rows[0][1].ToString();
            return color;
        }



        public void create_recent(String path, DevExpress.DataAccess.Sql.SqlDataSource datasource)//TO-DO CHANGE
        {
            datasource.Queries[0].Parameters[0].Value = path.Split('\\')[(path.Split('\\')).Count() - 1];
            datasource.Queries[0].Parameters[1].Value = path;
            datasource.Fill();
        }

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

        ///<summary>
        ///Projeye splice eklenirken kullanılmalıdır.Tecnicname tüm aynı komponentler için sabit olmalıdır.
        ///</summary>
        public void addComponent(String technicname, String name)
        {
            x.Components.AddComponentsRow(Guid.NewGuid().ToString(), name, technicname, 0, 0, 0, null, null, 0, 0, 0, 0, 0);
            updateComponents();

        }
        ///<summary>
        ///Projeye kapasitör ve direnç eklenirken kullanılmalıdır.Tecnicname tüm aynı komponentler için sabit olmalıdır.
        ///</summary>
        public void addComponent(String technicname, String name, int value, int valuemultiplier, int tolerence)
        {
            x.Components.AddComponentsRow(Guid.NewGuid().ToString(), name, technicname, value, valuemultiplier, tolerence, null, null, 0, 0, 0, 0, 0);
            updateComponents();

        }
        ///<summary>
        ///Projeye diyot eklenirken kullanılmalıdır.Tecnicname tüm aynı komponentler için sabit olmalıdır
        ///</summary>
        public void addComponent(String technicname, String name, int forwardvoltage, int tolerence)
        {
            x.Components.AddComponentsRow(Guid.NewGuid().ToString(), name, technicname, forwardvoltage, 1, tolerence, null, null, 0, 0, 0, 0, 0);
            updateComponents();

        }
        ///<summary>
        ///Projeye termistör eklenirken kullanılmalıdır.Tecnicname tüm aynı komponentler için sabit olmalıdır
        ///</summary>
        public void addComponent(String technicname, String name, int Comparasiontolerence, int firsttestpoint, int secondtestpoint, int minResistance, int maxResistence, int minResistincemultiplier, int maxResistenceMultiplier)
        {
            x.Components.AddComponentsRow(Guid.NewGuid().ToString(), name, technicname, 0, 0, 0, firsttestpoint.ToString(), secondtestpoint.ToString(), minResistance, minResistincemultiplier, maxResistence, Comparasiontolerence, maxResistenceMultiplier);
            updateComponents();
        }
        ///<summary>
        ///Projeye generic component eklenirken kullanılmalıdır.Tecnicname tüm aynı komponentler için sabit olmalıdır
        ///</summary>
        public void addComponent(String technicname, String name, int testCurrent, int testCurrentMultiplier, int voltageDrop, int tolerence)
        {


            x.Components.AddComponentsRow(Guid.NewGuid().ToString(), name, technicname, testCurrent, testCurrentMultiplier, tolerence, null, null, voltageDrop, 0, 0, 0, 0);
            updateComponents();
        }
        public void updateComponents()
        {
            mainsourceTableAdapters.ComponentsTableAdapter a = new mainsourceTableAdapters.ComponentsTableAdapter();
            a.Update(x.Components);
            a.Dispose();
            
        }






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
            else
            {
                MessageBox.Show("UNEXPECTED ERROR");
                return null;
            }

        }
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
            else
            {
                MessageBox.Show("UNEXPECTED ERROR");
            }
          
        }




    }
 
}
