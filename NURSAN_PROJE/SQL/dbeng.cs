using System;
using System.Linq;
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
       public static bool created = false;
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
