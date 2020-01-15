using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NURSAN_PROJE.SQL
{
    public partial class DataManager
    {
        private void addIOforSocket(object[] soc_parameters, string[,] tp_parameters)
        {
            for (int i = 0; i < tp_parameters.GetLength(1); i++)
            {
                if (tp_parameters[0, i] == null)
                {
                    break;
                }
                LocalTables.localtables.maintables.Tables["IO_connections"].Rows.Add(Guid.NewGuid().ToString(), soc_parameters[0].ToString(), tp_parameters[1, i], tp_parameters[2, i]);
            }
        }
        private void addIOforSocket(string SocketID, string[,] tp_parameters)
        {
            for (int i = 0; i < tp_parameters.GetLength(1); i++)
            {
                if (tp_parameters[0, i] == null)
                {
                    break;
                }
                LocalTables.localtables.projecttables.Tables["PIO_connection"].Rows.Add(Guid.NewGuid().ToString(), SocketID, tp_parameters[1, i], tp_parameters[2, i]);
            }
        }
        public void updateIObySocketID(string SocketID, string[,] tp_parameters)
        {
            if(getFromLocalTablesproject("PSockets").Select("ID_soket ='" + SocketID + "'").Length > 0)
            {
                if(getFromLocalTablesproject("PIO_connection").Select("ID_soket ='" + SocketID + "'").Length > 0)
                {
                    Console.WriteLine("Yok");
                    var rows = getFromLocalTablesproject("PIO_connection").Select("ID_soket ='" + SocketID + "'");
                    int i = 0;
                    foreach(var row in rows)
                    {
                        row[2] = tp_parameters[0, i];
                        row[3] = tp_parameters[1, i];
                        i++;
                    }
                }
                else
                {
                    Console.WriteLine("Var");
                    addIOforSocket(SocketID, tp_parameters);
                }
            }
            else
            {
                MessageBox.Show("Soket Bulunmuyor Güncelleme İptal Edildi");
            }  

        }
        private string getIOInfo(String ıoID)
        {
            try
            {
                string answ = null;             
                var rows = getFromLocalTablesproject("PIO_connection").Select("ID_IO = '" + ıoID + "'");
                answ += getSocketNameInfo(rows[0][1].ToString()) + " : " + rows[0][2].ToString();
                return answ;
            }
            catch
            {
                return "HATA";
            }

        }
    }
}
