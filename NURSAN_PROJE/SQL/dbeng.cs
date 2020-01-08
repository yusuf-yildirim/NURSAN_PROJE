﻿using System;
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
using System.Drawing.Imaging;

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

        public void connection_add(string nereden, string nereye, string kablodı, string kablorengi)
        {
            getmaincon();
            cmd = new SQLiteCommand();
            cmd.Connection = con;
            cmd.CommandText = "insert into TEST(NEREDEN,NEREYE,\"KABLO KONTROL\",\"KABLO RENGİ\") values ('" + nereden + "','" + nereye + "','" + kablodı + "','" + kablorengi + "')";
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void get_ConnectionTable()
        {
            DataTable connections = new DataTable("CONNECTİONS");
            connections.Columns.Add("NEREDEN");
            connections.Columns.Add("NEREYE");
            connections.Columns.Add("KABLO KONTROL");
            connections.Columns.Add("KABLO RENGİ");
            connections.Columns.Add("ÖZELLİKLER");
            //TO-DO
            tmplog.WriteDebugLog("----------BAĞLANTI LİSTELEME BAŞLADI----------", false);
            getprojectcon();
            ds = new DataSet();
            da = new SQLiteDataAdapter("select * from PConnections", con);
            SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(ds);
            for(int i = 0;i < ds.Tables[0].Rows.Count; i++)
            {
                connections.Rows.Add("","","","","");  //TO-DO
            }
        }


        private void getSocketInfo(String SocketID)
        {
            //TO-DO
        }
        private void getIOInfo(String ıoID)
        {
            //TO-DO
        }
        private void getComponentInfo(String componenetID)
        {
            //TO-DO
        }


        public DataView get_saved_sockets()
        {
            tmplog.WriteDebugLog("----------Ana Veritabanı Soketleri Listelemesi Başladı----------", false);
            getmaincon();
            ds = new DataSet();
            da = new SQLiteDataAdapter("select *  from[Sockets][Sockets]", con);
            SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(ds);
            ds.Tables[0].DefaultView.AllowEdit = true;
            con.Close();
            tmplog.WriteDebugLog("Ana veritabanı bağlantısı kapatıldı");           
            tmplog.WriteDebugLog("----------Ana Veritabanı Soketleri Listelemesi Tamamlandı----------", false);
            return ds.Tables[0].DefaultView;

        }
        public DataView get_project_sockets()
        {
            tmplog.WriteDebugLog("----------Proje Soketleri Listelemesi Başladı----------", false);
            getprojectcon();
            
            ds = new DataSet();
            da = new SQLiteDataAdapter(@"select[PSockets].[ID_soket], [PSockets].[Adı],
       [PSockets].[Pin_sayisi],
       [PSockets].[Anahtar_sayisi],
       [PSockets].[Led_numarasi]
  from[PSockets][PSockets]
", con);
            SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(ds);
            tmplog.WriteDebugLog("Veriler başarıyla çekildi");
            ds.Tables[0].DefaultView.AllowEdit = true;
            con.Close();
            tmplog.WriteDebugLog("Proje veritabanı bağlantısı kapatıldı");
            tmplog.WriteDebugLog("----------Proje Soketleri Listelemesi Tamamlandı----------", false);
            return ds.Tables[0].DefaultView;

        }

        public DataTable test()
        {


            tmplog.WriteDebugLog("----------Proje Soketleri Listelemesi Başladı----------", false);
            getprojectcon();

            ds = new DataSet();
            da = new SQLiteDataAdapter(@"SELECT PSockets.Adı,PIO_connection.Socket_PIN,PIO_connection.IO_PIN,PSockets.ID_soket,PIO_connection.ID_IO FROM PSockets INNER JOIN PIO_connection ON PIO_connection.ID_soket = PSockets.ID_soket", con);
            SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(ds);
           
            tmplog.WriteDebugLog("Proje veritabanı bağlantısı kapatıldı");
            tmplog.WriteDebugLog("----------Proje Soketleri Listelemesi Tamamlandı----------", false);
          
            return ds.Tables[0];

        }
      public DataTable test2()
        {
            getmaincon();
            ds = new DataSet();
            da = new SQLiteDataAdapter(@"SELECT Components.ID_component,Components.Component_Name,Components.Tur FROM Components", con);
            SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(ds);
            tmplog.WriteDebugLog("Veriler başarıyla çekildi");
            ds.Tables[0].DefaultView.AllowEdit = true;
            con.Close();
            return ds.Tables[0];
        }


        public void register_socket(object[] soc_parameters, string[,] tp_parameters)
        {
            tmplog.WriteDebugLog("----------Yeni Soket Ekleme başladı----------", false);
            try
            {
                x.Sockets.AddSocketsRow(soc_parameters[0].ToString(), soc_parameters[1].ToString(), Convert.ToInt32(soc_parameters[2]), Convert.ToInt32(soc_parameters[3]), Convert.ToInt32(soc_parameters[4]));
                mainsourceTableAdapters.SocketsTableAdapter a = new mainsourceTableAdapters.SocketsTableAdapter();
                tmplog.WriteDebugLog("Ana veritabanı tabloları na "+ soc_parameters[0].ToString()+" id numaralı soket eklendi");
                a.Update(x.Sockets);
                a.Dispose();
                tmplog.WriteDebugLog("Veritabanı ile tablo eşitlendi");
                for (int i = 0; i < tp_parameters.GetLength(1); i++)
                {
                    if (tp_parameters[0, i] == null)
                    {
                        break;
                    }                                        
                        x.IO_connections.AddIO_connectionsRow(Guid.NewGuid().ToString()  ,soc_parameters[0].ToString(), tp_parameters[1, i], tp_parameters[2, i]);     
                    // cmd.CommandText = "INSERT INTO PIO_connection(ID_soket, Socket_PIN, IO_PIN) values ('" + soc_parameters[0] + "','" + tp_parameters[1, i] + "','" + tp_parameters[2, i] + "');";
                    // cmd.ExecuteNonQuery();
                }
                mainsourceTableAdapters.IO_connectionsTableAdapter ax = new mainsourceTableAdapters.IO_connectionsTableAdapter();
                ax.Update(x.IO_connections);
                tmplog.WriteDebugLog("Bağlantı tablosu güncellendi ve veritabanı ile eşitlendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                tmplog.WriteDebugLog("Bir hata oluştu:");
                tmplog.WriteDebugLog(ex.StackTrace, true);
            }
            tmplog.WriteDebugLog("----------Yeni Soket Ekleme tamamlandı----------", false);
        }
        public void unregister_socket(string SocketID)
        {
            tmplog.WriteDebugLog("----------Soket Silme başladı----------", false);
            getprojectcon();           
            ds = new DataSet();
            da = new SQLiteDataAdapter($"select *  from PSockets WHERE ID_soket = '{SocketID}'", connection: con);
            tmplog.WriteDebugLog(SocketID + " id ye sahip soket, veritabanında arandı");
            SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(ds);
            con.Close();
            tmplog.WriteDebugLog("Proje veritabanı bağlantısı kapatıldı");
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
                    cmd.CommandText = $"delete from ImageStore where ID_soket = '{SocketID}'";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    tmplog.WriteDebugLog("Ana veritabanı bağlantısı kapatıldı");
                    tmplog.WriteDebugLog(SocketID + " id ye sahip soket ve bağlantıları veritabanından silindi.");
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
                cmd.CommandText = $"delete from ImageStore where ID_soket = '{SocketID}'";
                cmd.ExecuteNonQuery();
                con.Close();
                tmplog.WriteDebugLog("Ana veritabanı bağlantısı kapatıldı");
                tmplog.WriteDebugLog(SocketID + " id ye sahip soket ve bağlantıları veritabanından silindi.");
            }
         
            tmplog.WriteDebugLog("----------Soket Silme tamamlandı----------", false);
        }
        
        public void register_using_socket(string SocketID)
        {
            tmplog.WriteDebugLog("----------Projeye Soket Kaydı başladı----------",false);

            getmaincon();          
            ds = new DataSet();
            DataSet ds2 = new DataSet();
            DataSet ds3 = new DataSet();
            da = new SQLiteDataAdapter($"select *  from[Sockets][Sockets] WHERE ID_soket = '{SocketID}'", connection: con);
            tmplog.WriteDebugLog(SocketID+ " id ye sahip soket veritabanında arandı");
            SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(ds);
            da = new SQLiteDataAdapter($"select *  from IO_connections WHERE ID_soket = '{SocketID}'", connection: con);
            tmplog.WriteDebugLog(SocketID + " id ye sahip soket bağlantıları bağlantı veritabanında arandı");
            sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(ds2);
            da = new SQLiteDataAdapter($"select *  from ImageStore WHERE ID_soket = '{SocketID}'", connection: con);
            tmplog.WriteDebugLog(SocketID + " id ye sahip soket resimleri veritabanında arandı");
            sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(ds3);
            con.Close();
            tmplog.WriteDebugLog("Ana veritabanı bağlantısı kapatıldı");
            getprojectcon();        
            cmd = new SQLiteCommand();
            cmd.Connection = con;
            cmd.CommandText = $"insert into PSockets(ID_soket,Adı,\"Pin_sayisi\",\"Anahtar_sayisi\",\"Led_numarasi\") values ('{ds.Tables[0].Rows[0][0].ToString()}','{ds.Tables[0].Rows[0][1].ToString()}','{ds.Tables[0].Rows[0][2].ToString()}','{ds.Tables[0].Rows[0][3].ToString()}','{ds.Tables[0].Rows[0][4].ToString()}');";
            if(ds3.Tables[0].Rows.Count > 0)
            {
                cmd.Parameters.Add("@img", DbType.Binary, ((byte[])(ds3.Tables[0].Rows[0][2])).Length);
                cmd.CommandText += "INSERT INTO ImageStore(ID_soket, imageBlob) values ('" + ds.Tables[0].Rows[0][0].ToString() + "',@img);";
                cmd.Parameters["@img"].Value = (byte[])ds3.Tables[0].Rows[0][2];
            }
          
            try
            {
                using (var transaction = con.BeginTransaction())
                {
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    {
                        cmd.CommandText = "INSERT INTO PIO_connection(ID_IO,ID_soket, Socket_PIN, IO_PIN) values ('" + ds2.Tables[0].Rows[i][0].ToString() + "','" + ds.Tables[0].Rows[0][0].ToString() + "','" + ds2.Tables[0].Rows[i][2] + "','" + ds2.Tables[0].Rows[i][3] + "');";
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    tmplog.WriteDebugLog("Bağlantılar ,soket bilgileri ve resinleri proje veri tabanına aktarıldı!");
                }
            }
            catch (SQLiteException ex)
            {
                //  MessageBox.Show(ex.ErrorCode.ToString());
                if (ex.ErrorCode == 19)
                {
                    MessageBox.Show("SOKET ZATEN KULLANIMDA");
                    MessageBox.Show(ex.StackTrace);
                    MessageBox.Show(ex.Message);
                    tmplog.WriteDebugLog("Soket aktarılamadı! SOKET ZATEN KULLANIMDA", true);
                    tmplog.WriteDebugLog(ex.StackTrace, false);
                }
                else
                {
                    MessageBox.Show(ex.Message + " KULLANILACAK SOKET KAYDEDİLİRKEN İŞLENMEMİŞ HATA");
                    tmplog.WriteDebugLog("Soket aktarılamadı! "+ ex.Message, true);
                    tmplog.WriteDebugLog(ex.StackTrace, false);
                }
            }
            con.Close();
            tmplog.WriteDebugLog("Proje veritabanı bağlantısı kapatıldı!");
            tmplog.WriteDebugLog("----------Soket Kaydı tamamlandı----------", false);
        }
        public void unregister_using_socket(string SocketID)
        {
            tmplog.WriteDebugLog("----------Kullanılan Soket Kaydı Silme başladı----------", false);
            getprojectcon();          
            cmd = new SQLiteCommand();
            cmd.Connection = con;
            cmd.CommandText = $"delete from PSockets where ID_soket = '{SocketID}'";
            tmplog.WriteDebugLog("Socket silindi");
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"delete from PIO_connection where ID_soket = '{SocketID}'";
            tmplog.WriteDebugLog("Soket bağlantıları silindi");
            cmd.ExecuteNonQuery();
            cmd.CommandText = $"delete from ImageStore where ID_soket = '{SocketID}'";
            tmplog.WriteDebugLog("Soket resimleri silindi");
            cmd.ExecuteNonQuery();
            con.Close();
            tmplog.WriteDebugLog("Proje veritabanı kapatıldı!");
            tmplog.WriteDebugLog("----------Kullanılan Soket Kaydı Silme tamamlandı----------", false);
        }
        ///<summary>
        ///Gönderilen Soket ID si için IO bağlantıları arar ve dataset olarak döndürür.
        ///</summary>
        public DataSet determineio(String SocketID)
        {
            ds = new DataSet();
            getprojectcon();           
            da = new SQLiteDataAdapter($"SELECT * FROM PIO_connection WHERE ID_soket = '{SocketID}'", connection: con);           
            SQLiteCommandBuilder sql_command_builder = new SQLiteCommandBuilder(da);
            da.Fill(ds);            
            return ds;           
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
            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {

                    byte[] data = (byte[])ds.Tables[0].Rows[0][2];
                    using (var ms = new MemoryStream(data))
                    {
                        Image deneme = Image.FromStream(ms);
                        Image workaround = (Image)deneme.Clone();
                        for (int i = 0; i < workaround.PropertyIdList.Length; i++)
                        {
                            Console.WriteLine(workaround.PropertyIdList[i] + " -------------------");

                        }
                        return workaround;

                    }

                }
                catch (Exception ex)
                {
                     MessageBox.Show(ex.Message);
                     MessageBox.Show(ex.StackTrace);
                   // Console.WriteLine("RESİM YÜKLENEMEDİ");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("RESİM YÜKLENEMEDİ");
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
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                {
                    Console.WriteLine(encoders[j].MimeType );
                    return encoders[j];
                }
            }
            return null;
        }
       
        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {

            try
            {
                using (var ms = new MemoryStream())
                {

                    imageIn.Save(ms, imageIn.RawFormat);
                    byte[] bytesText = ms.ToArray();
                    return bytesText;
                }
            }
            catch
            {
                /*Bitmap myBitmap;
                ImageCodecInfo myImageCodecInfo;
                Encoder myEncoder;
                EncoderParameter myEncoderParameter;
                EncoderParameters myEncoderParameters;

                // Create a Bitmap object based on a BMP file.
                myBitmap = new Bitmap(imageIn);
                myImageCodecInfo = GetEncoderInfo2("image/jpeg");

                myEncoder = Encoder.Quality;
                myEncoder = Encoder.

                myEncoderParameters = new EncoderParameters(1);
                myEncoderParameter = new EncoderParameter(myEncoder, 80L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                myBitmap.Save("temp.jpg", myImageCodecInfo, myEncoderParameters);*/
                imageIn.Save("temp.jpg", ImageFormat.Jpeg);
                byte[] data = null;

                try
                {
                    data = File.ReadAllBytes("temp.jpg");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                return data;
            }
           
        }
         ImageCodecInfo GetEncoderInfo2(string mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        ///<summary>
        ///Projeye splice eklenirken kullanılmalıdır.Tecnicname tüm aynı komponentler için sabit olmalıdır.
        ///</summary>
        public void addComponent(String technicname, String name)
        {
            x.Components.AddComponentsRow(Guid.NewGuid().ToString(), name, technicname,0,0,0,null,null,0,0,0,0,0);
            updateComponents();

        }
        ///<summary>
        ///Projeye kapasitör ve direnç eklenirken kullanılmalıdır.Tecnicname tüm aynı komponentler için sabit olmalıdır.
        ///</summary>
        public void addComponent(String technicname, String name,int value,int valuemultiplier,int tolerence)
        {
            x.Components.AddComponentsRow(Guid.NewGuid().ToString(), name, technicname, value, valuemultiplier, tolerence, null, null, 0, 0, 0,0,0);
            updateComponents();

        }
        ///<summary>
        ///Projeye diyot eklenirken kullanılmalıdır.Tecnicname tüm aynı komponentler için sabit olmalıdır
        ///</summary>
        public void addComponent(String technicname, String name, int forwardvoltage, int tolerence)
        {
            x.Components.AddComponentsRow(Guid.NewGuid().ToString(), name, technicname, forwardvoltage, 1, tolerence, null, null, 0, 0, 0,0,0);
            updateComponents();

        }
        ///<summary>
        ///Projeye termistör eklenirken kullanılmalıdır.Tecnicname tüm aynı komponentler için sabit olmalıdır
        ///</summary>
        public void addComponent(String technicname ,String name, int Comparasiontolerence,int firsttestpoint, int secondtestpoint,int minResistance,int maxResistence,int minResistincemultiplier,int maxResistenceMultiplier)
        {
            x.Components.AddComponentsRow(Guid.NewGuid().ToString(),name,technicname,0,0,0,firsttestpoint.ToString(),secondtestpoint.ToString(),minResistance,minResistincemultiplier,maxResistence,Comparasiontolerence,maxResistenceMultiplier);
            updateComponents();            
        }
        ///<summary>
        ///Projeye generic component eklenirken kullanılmalıdır.Tecnicname tüm aynı komponentler için sabit olmalıdır
        ///</summary>
        public void addComponent(String technicname, String name, int testCurrent,int testCurrentMultiplier, int voltageDrop, int tolerence)
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

    }
}
