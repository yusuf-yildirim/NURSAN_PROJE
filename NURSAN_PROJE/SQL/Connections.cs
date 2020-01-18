using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NURSAN_PROJE.SQL
{
    public partial class DataManager
    {
        ///<summary>
        ///Projeye yeni bağlantı eklemek için kullanılır.
        ///</summary>
        public void addConnection(string origin, string origintype, string destination, string destinationtype, string colorıd, string phaseid,string cablename)//güncelleme gerekir
        {     
            getFromLocalTablesproject("PConnections").Rows.Add(Guid.NewGuid().ToString(), getlastconnectionorder(phaseid), origin, origintype, destination, destinationtype, colorıd, cablename, null, null, phaseid, null, null);
        }


        ///<summary>
        ///Projeden ilgili fazdaki son sıra numrasını çeker.
        ///</summary>
        public string getlastconnectionorder(string phaseid)//güncelleme gerekir
        {
            try
            {
                return (str2ınt(getFromLocalTablesproject("PConnections").Select("ID_etap_1 = '" + phaseid + "'", "Order DESC")[0][1]) + 1).ToString();
            }
            catch
            {
                return "1";
            }
           
           

        }

        ///<summary>
        ///Kullanıcıya gösterilecek bağlantı tablosunu günceller.
        ///</summary>
        private void update_ConnectionTable(string phaseid)
        {
            DataTable connections = new DataTable("CONNECTIONS");
            connections.Columns.Add("NEREDEN");
            connections.Columns.Add("NEREYE");
            connections.Columns.Add("KABLO ADI");
            connections.Columns.Add("WireColor");
            connections.Columns.Add("ÖZELLİKLER");
            var rows = getFromLocalTablesproject("PConnections").Select("ID_etap_1 = '" + phaseid + "'","Order ASC");
            foreach(var row in rows)
            {
                string origin, destination, color;
                if (row[3].ToString() == "SOCKET")
                {
                    origin = getIOInfo(row[2].ToString());
                }
                else
                {
                    origin = getComponentInfo(row[2].ToString());
                }
                if (row[5].ToString() == "SOCKET")
                {
                    destination = getIOInfo(row[4].ToString());
                }
                else
                {
                    destination = getComponentInfo(row[4].ToString());
                }
                color = getColorInfo(row[6].ToString());
                connections.Rows.Add(origin, destination, row[7], color, null);  //TO-DO
            }
            try
            {
                LocalTables.localtables.projecttables.Tables.Add(connections);
            }
            catch
            {
                LocalTables.localtables.projecttables.Tables.Remove("CONNECTIONS");
                LocalTables.localtables.projecttables.Tables.Add(connections);
            }                       
        }
    }

}