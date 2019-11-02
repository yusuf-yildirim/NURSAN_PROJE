using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace NURSAN_PROJE.SQL
{
    class DBeng
    {
       static SQLiteConnection con;
       static SQLiteDataAdapter da;
       static SQLiteCommand cmd;
       static DataSet ds;
        private void getcon()
        {
            con = new SQLiteConnection("Data Source="+Application.StartupPath+"\\database.db;Version=3;");
            con.Open();

        }

        public void connection_add(string nereden,string nereye,string kablodı,string kablorengi)
        {
            getcon();
            cmd = new SQLiteCommand();
            
            cmd.Connection = con;
            cmd.CommandText = "insert into TEST(NEREDEN,NEREYE,\"KABLO KONTROL\",\"KABLO RENGİ\") values ('"+nereden+ "','" + nereye + "','" + kablodı + "','" + kablorengi + "')";
            cmd.ExecuteNonQuery();
            con.Close();
         
        }
        public void create_project()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                connectionStringsSection.ConnectionStrings["tablo"].ConnectionString = @"XpoProvider=SQLite;Data Source=C:\Users\Burakcan\source\repos\NURSAN_PROJE\NURSAN_PROJE\bin\Debug\yeni.nursan";
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");

                SQLiteConnection m_dbConnection = new SQLiteConnection(@"XpoProvider=SQLite;Data Source=C:\Users\Burakcan\source\repos\NURSAN_PROJE\NURSAN_PROJE\bin\Debug\yeni.nursan");
                m_dbConnection.Open();
                string sql = File.ReadAllText(@"db.ini");
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                m_dbConnection.Close();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
     
        }


    }
}
