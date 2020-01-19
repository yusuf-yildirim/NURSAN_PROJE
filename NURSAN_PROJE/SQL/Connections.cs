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
        ///WheretoType [SOCKET,COMPONENT]
        ///</summary>
        public void updateConnection(string connectionID,string origin,string originType,string destination,string destinationType,string colorID,string cablename)
        {
            var row = getFromLocalTablesproject("PConnections").Select("ID_connection = '" + connectionID + "'")[0];
            row[2] = origin;
            row[3] = originType;
            row[4] = destination;
            row[5] = destinationType;
            row[6] = colorID;
            row[7] = cablename;
        }
        ///<summary>
        ///İlgili ConnectionID yi siler.
        ///</summary>
        public void removeConnection(string connectionID,string phaseID)
        {
             getFromLocalTablesproject("PConnections").Select("ID_connection = '" + connectionID + "'")[0].Delete();
            remapConnectionOrder(phaseID);
        }

        public void remapConnectionOrder(string phaseID)
        {
            var rows = getFromLocalTablesproject("PConnections").Select("ID_etap_1 = '" + phaseID + "'", "Order ASC");
            int neworder = 1;
            foreach(var row in rows)
            {
                row[1] = neworder;
                neworder++;
            }
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
            connections.Columns.Add("CONNECTONID");
            connections.Columns.Add("NEREDEN");
            connections.Columns.Add("NEREDENID");
            connections.Columns.Add("NEREYE");
            connections.Columns.Add("NEREYEID");
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
                connections.Rows.Add(row[0],origin, row[2], destination,row[4], row[7], color, null);  //TO-DO
            }
            try
            {
                localTables.addLocalTable(connections,Databases.Project);
            }
            catch
            {
                localTables.removeLocalTable("CONNECTIONS",Databases.Project);
                localTables.addLocalTable(connections, Databases.Project);
            }                       
        }
    }

}