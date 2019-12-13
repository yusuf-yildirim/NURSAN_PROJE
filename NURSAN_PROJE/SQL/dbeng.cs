using System;
using System.Linq;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using NURSAN_PROJE.Configurator;

namespace NURSAN_PROJE.SQL
{
    class DBeng
    {
       static SQLiteConnection con;
       static SQLiteDataAdapter da;
       static SQLiteCommand cmd;
       static DataSet ds;
       public static bool created = false;
       Config getconstring = new Config();
        private void getmaincon()
        {
          
            con = new SQLiteConnection("Data Source="+Application.StartupPath+"\\tablo.db;Version=3;");
            con.Open();

        }
        private void getprojectcon()
        {
            con = new SQLiteConnection(getconstring.get_conn_string("tablo")+";Version=3;");
            con.Open();

        }

        public void connection_add(string nereden,string nereye,string kablodı,string kablorengi)
        {
            getmaincon();
            cmd = new SQLiteCommand();            
            cmd.Connection = con;
            cmd.CommandText = "insert into TEST(NEREDEN,NEREYE,\"KABLO KONTROL\",\"KABLO RENGİ\") values ('"+nereden+ "','" + nereye + "','" + kablodı + "','" + kablorengi + "')";
            cmd.ExecuteNonQuery();
            con.Close();
            
        }
        public DataView get_saved_sockets()
        {
            getmaincon();
            ds = new DataSet();
            da = new SQLiteDataAdapter("select *  from[tbl_Socket][tbl_Socket]", con);
            SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(ds);
            ds.Tables[0].DefaultView.AllowEdit = true;
            return ds.Tables[0].DefaultView;
        }
        public void register_socket(object[] parameters)
        {
            
            getmaincon();
            cmd = new SQLiteCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO tbl_Socket(Adı, Pin_sayisi, Anahtar_sayisi, Led_numarasi) values (?,?,?,?)";
           foreach(object value in parameters)
            {
                cmd.Parameters.AddWithValue("",value);    
            }
            try
            {
                cmd.ExecuteNonQuery();

            }
            catch (SQLiteException ex)
            {

                //  MessageBox.Show(ex.ErrorCode.ToString());
                if (ex.ErrorCode == 19)
                {
                    MessageBox.Show("SOKET ZATEN KULLANIMDA");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        public void unregister_socket(int SocketID)
        {
            getprojectcon();
            ds = new DataSet();
            da = new SQLiteDataAdapter("select *  from tbl_Socket_using WHERE ID_soket = '" + SocketID + "'", con);
            SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(ds);
            con.Close();
            if(ds.Tables[0].Rows.Count > 0)
            {
                DialogResult cikis = new DialogResult();
                cikis = MessageBox.Show("Kaldırmak istediğiniz soket kullanımda devam etmek istiyormusunuz ?", "Uyarı", MessageBoxButtons.YesNo);
                if (cikis == DialogResult.Yes)
                {
                    getmaincon();
                    cmd = new SQLiteCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "delete from tbl_Socket where ID_soket = " + SocketID;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
           
            }
            else
            {
                getmaincon();
                cmd = new SQLiteCommand();
                cmd.Connection = con;
                cmd.CommandText = "delete from tbl_Socket where ID_soket = " + SocketID;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            
        }
        public void register_using_socket(int SocketID)
        {
           
            getmaincon();
            ds = new DataSet();
            da = new SQLiteDataAdapter("select *  from[tbl_Socket][tbl_Socket] WHERE ID_soket = '"+SocketID+"'", con);
            SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(ds);
            con.Close();
            
            getprojectcon();
            cmd = new SQLiteCommand();
            cmd.Connection = con;
            cmd.CommandText = "insert into tbl_Socket_using(ID_soket,Adı,\"Pin_sayisi\",\"Anahtar_sayisi\",\"Led_numarasi\") values ('" + ds.Tables[0].Rows[0][0].ToString() + "','" + ds.Tables[0].Rows[0][1].ToString() + "','" + ds.Tables[0].Rows[0][2].ToString() + "','" + ds.Tables[0].Rows[0][3].ToString() + "','" + ds.Tables[0].Rows[0][4].ToString() + "')";
            try
            {
                cmd.ExecuteNonQuery();

            }
            catch(SQLiteException ex )
            {
                
              //  MessageBox.Show(ex.ErrorCode.ToString());
                if (ex.ErrorCode == 19)
                {
                    MessageBox.Show("SOKET ZATEN KULLANIMDA");
                }
               
            }
            
            con.Close();
        }
        public void unregister_using_socket(int SocketID)
        {
            getprojectcon();
            cmd = new SQLiteCommand();
            cmd.Connection = con;
            cmd.CommandText = "delete from tbl_Socket_using where ID_soket = "+ SocketID;
            cmd.ExecuteNonQuery();
            con.Close();
        }
     
        public void get_connectionselect_tree()
        {
            cmd.CommandText = @"SELECT
             tbl_Socket.Adı,
             tbl_Socket.ID_soket,
             tbl_IO_connection.Socket_PIN
             FROM
             tbl_Socket
             INNER JOIN tbl_IO_connection ON tbl_IO_connection.ID_soket = tbl_Socket.ID_soket
             WHERE
             tbl_Socket.ID_soket = 52";
            /*
             SELECT
             tbl_Socket."Adı",
             tbl_Socket.ID_soket,
             tbl_IO_connection.Socket_PIN
             FROM
             tbl_Socket
             INNER JOIN tbl_IO_connection ON tbl_IO_connection.ID_soket = tbl_Socket.ID_soket
             WHERE
             tbl_Socket.ID_soket = 52
             * */
        }




        public void create_recent(String path,DevExpress.DataAccess.Sql.SqlDataSource datasource)
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
                connectionStringsSection.ConnectionStrings["tablo"].ConnectionString = @"XpoProvider=SQLite;Data Source=" + Application.StartupPath + "\\" +name + ".nursan";
               // MessageBox.Show(Application.StartupPath + "\\" + name + ".nursan");
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");

                SQLiteConnection m_dbConnection = new SQLiteConnection(@"XpoProvider=SQLite;Data Source=" + Application.StartupPath + "\\" + name + ".nursan");
                m_dbConnection.Open();
                string sql = File.ReadAllText(@"db.ini");
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                m_dbConnection.Close();
                created = true;
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
     
        }


    }
}
