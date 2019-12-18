using System;
using System.Linq;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using NURSAN_PROJE.Configurator;
using System.Diagnostics;
using System.Threading;
using System.Drawing;

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
        private void getmaincon()
        {
            con = new SQLiteConnection("Data Source=" + Application.StartupPath + "\\tablo.db;Version=3;");
            con.Open();
        }
        private SQLiteConnection _getmaincon()
        {
            return con = new SQLiteConnection("Data Source=" + Application.StartupPath + "\\tablo.db;Version=3;");
        }
        private void getprojectcon()
        {
            con = new SQLiteConnection(getconstring.get_conn_string("tablo") + ";Version=3;");
            con.Open();
        }

        public void connection_add(string nereden, string nereye, string kablodı, string kablorengi)
        {
            getmaincon();
            cmd = new SQLiteCommand();
            cmd.Connection = con;
            cmd.CommandText = "insert into TEST(NEREDEN,NEREYE,\"KABLO KONTROL\",\"KABLO RENGİ\") values ('" + nereden + "','" + nereye + "','" + kablodı + "','" + kablorengi + "')";
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public DataView get_saved_sockets()
        {
            getmaincon();
            ds = new DataSet();
            da = new SQLiteDataAdapter("select *  from[Sockets][Sockets]", con);
            SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(ds);
            ds.Tables[0].DefaultView.AllowEdit = true;
            return ds.Tables[0].DefaultView;
        }
        public DataView get_project_sockets()
        {
            getprojectcon();
            ds = new DataSet();
            da = new SQLiteDataAdapter(@"select[tbl_Socket_using].[ID_soket], [tbl_Socket_using].[Adı],
       [tbl_Socket_using].[Pin_sayisi],
       [tbl_Socket_using].[Anahtar_sayisi],
       [tbl_Socket_using].[Led_numarasi]
  from[tbl_Socket_using][tbl_Socket_using]
", con);
            SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(ds);
            ds.Tables[0].DefaultView.AllowEdit = true;
            return ds.Tables[0].DefaultView;

        }
     


        public void register_socket(object[] soc_parameters, string[,] tp_parameters)
        {
            try
            {
                x.Sockets.AddSocketsRow(soc_parameters[0].ToString(), soc_parameters[1].ToString(), Convert.ToInt32(soc_parameters[2]), Convert.ToInt32(soc_parameters[3]), Convert.ToInt32(soc_parameters[4]));
                mainsourceTableAdapters.SocketsTableAdapter a = new mainsourceTableAdapters.SocketsTableAdapter();       
                a.Update(x.Sockets);
                a.Dispose();
                for (int i = 0; i < tp_parameters.GetLength(1); i++)
                {
                    if (tp_parameters[0, i] == null)
                    {
                        break;
                    }                                        
                        x.IO_connections.AddIO_connectionsRow(Guid.NewGuid().ToString()  ,soc_parameters[0].ToString(), tp_parameters[1, i], tp_parameters[2, i]);     
                    // cmd.CommandText = "INSERT INTO tbl_IO_connection(ID_soket, Socket_PIN, IO_PIN) values ('" + soc_parameters[0] + "','" + tp_parameters[1, i] + "','" + tp_parameters[2, i] + "');";
                    // cmd.ExecuteNonQuery();
                }
                mainsourceTableAdapters.IO_connectionsTableAdapter ax = new mainsourceTableAdapters.IO_connectionsTableAdapter();
                ax.Update(x.IO_connections);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }           

        }
        public void unregister_socket(string SocketID)
        {
            getprojectcon();
            ds = new DataSet();
            da = new SQLiteDataAdapter($"select *  from tbl_Socket_using WHERE ID_soket = '{SocketID}'", connection: con);
            SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DialogResult cikis = new DialogResult();
                cikis = MessageBox.Show("Kaldırmak istediğiniz soket kullanımda, devam etmek istiyormusunuz ?", "Uyarı", MessageBoxButtons.YesNo);
                if (cikis == DialogResult.Yes)
                {
                    getmaincon();
                    cmd = new SQLiteCommand();
                    cmd.Connection = con;
                    cmd.CommandText = $"delete from Sockets where ID_soket ='{SocketID}'";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = $"delete from IO_connections where ID_soket = '{SocketID}'";
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            else
            {
                getmaincon();
                cmd = new SQLiteCommand();
                cmd.Connection = con;
                cmd.CommandText = $"delete from Sockets where ID_soket = '{SocketID}'";
                cmd.ExecuteNonQuery();
                cmd.CommandText = $"delete from IO_connections where ID_soket = '{SocketID}'";
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void register_using_socket(string SocketID)
        {            
            getmaincon();
            ds = new DataSet();
            DataSet ds2 = new DataSet();
            da = new SQLiteDataAdapter($"select *  from[Sockets][Sockets] WHERE ID_soket = '{SocketID}'", connection: con);
            SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(ds);
            da = new SQLiteDataAdapter($"select *  from IO_connections WHERE ID_soket = '{SocketID}'", connection: con);
            sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(ds2);
            con.Close();
            getprojectcon();
            cmd = new SQLiteCommand();
            cmd.Connection = con;
            cmd.CommandText = $"insert into tbl_Socket_using(ID_soket,Adı,\"Pin_sayisi\",\"Anahtar_sayisi\",\"Led_numarasi\") values ('{ds.Tables[0].Rows[0][0].ToString()}','{ds.Tables[0].Rows[0][1].ToString()}','{ds.Tables[0].Rows[0][2].ToString()}','{ds.Tables[0].Rows[0][3].ToString()}','{ds.Tables[0].Rows[0][4].ToString()}')";
            try
            {
                using (var transaction = con.BeginTransaction())
                {
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    {
                        cmd.CommandText = "INSERT INTO tbl_IO_connection(ID_soket, Socket_PIN, IO_PIN) values ('" + ds.Tables[0].Rows[0][0].ToString() + "','" + ds2.Tables[0].Rows[i][2] + "','" + ds2.Tables[0].Rows[i][3] + "');";
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
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
                    MessageBox.Show(ex.Message + " KULLANILACAK SOKET KAYDEDİLİRKEN İŞLENMEMİŞ HATA");
                }
            }
            con.Close();
        }
        public void unregister_using_socket(string SocketID)
        {
            getprojectcon();
            cmd = new SQLiteCommand();
            cmd.Connection = con;
            cmd.CommandText = $"delete from tbl_Socket_using where ID_soket = '{SocketID}'";
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"delete from tbl_IO_connection where ID_soket = '{SocketID}'";
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

            /*  getprojectcon();
              ds = new DataSet();
              da = new SQLiteDataAdapter("select *  from[tbl_Socket][tbl_Socket]", con);
              SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
              da.Fill(ds);
              ds.Tables[0].DefaultView.AllowEdit = true;
              return ds.Tables[0].DefaultView;*/
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
        public Image get_socket_image(string SocketID)
        {
            getmaincon();
            ds = new DataSet();
            da = new SQLiteDataAdapter($"select *  from ImageStore where ID_soket='{SocketID}'", con);
            SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(ds);

            try
            {

                byte[] data = (byte[])ds.Tables[0].Rows[0][2];
                using (var ms = new MemoryStream(data))
                {
                   return Image.FromStream(ms);
                 
                }
                
            }
            catch(Exception ex)
            {
               /* MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);*/
                Console.WriteLine("RESİM YÜKLENEMEDİ yada YOK");
                return null;
            }
        }
        public void set_socket_image(string SocketID,Image img)
        {
            try
            {
                using (SQLiteConnection con = _getmaincon())
                {

                    con.Open();

                    byte[] data = ImageToByteArray(img);


                    SQLiteCommand cmd = new SQLiteCommand(con);

                    cmd.CommandText = $"INSERT INTO ImageStore(ID_soket,imageBlob) VALUES ('{SocketID}',@img)";
                    cmd.Prepare();

                    cmd.Parameters.Add("@img", DbType.Binary, data.Length);
                    cmd.Parameters["@img"].Value = data;
                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
            catch
            {
                using (SQLiteConnection con = _getmaincon())
                {

                    con.Open();

                    byte[] data = ImageToByteArray(img);


                    SQLiteCommand cmd = new SQLiteCommand(con);

                    cmd.CommandText = $"UPDATE ImageStore SET imageBlob = @img WHERE ID_soket= '{SocketID}'";
                    cmd.Prepare();

                    cmd.Parameters.Add("@img", DbType.Binary, data.Length);
                    cmd.Parameters["@img"].Value = data;
                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
          
        }
        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
    }
}
