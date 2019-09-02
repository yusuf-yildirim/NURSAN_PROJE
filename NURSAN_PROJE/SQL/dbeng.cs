using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;

namespace NURSAN_PROJE.SQL
{
    class dbeng
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



    }
}
