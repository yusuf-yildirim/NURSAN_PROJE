using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NURSAN_PROJE.SQL
{
    public partial class DataManager
    {
        public void addSocket(object[] soc_parameters, string[,] tp_parameters)
        {
            LocalTables.localtables.maintables.Tables["Sockets"].Rows.Add(soc_parameters[0].ToString(), 
                                                                          soc_parameters[1].ToString(), 
                                                                          str2ınt(soc_parameters[2]),
                                                                          str2ınt(soc_parameters[3]),
                                                                          str2ınt(soc_parameters[4]));
            addIOforSocket( soc_parameters, tp_parameters);
        }
  
        public void deleteProjectSocket(string SocketID)
        {
            var rows = LocalTables.localtables.projecttables.Tables["PSockets"].Select("ID_soket = '"+SocketID+"'");
            foreach (var row in rows)
                row.Delete();
            rows = LocalTables.localtables.projecttables.Tables["PIO_connection"].Select("ID_soket = '" + SocketID + "'");
            foreach (var row in rows)
                row.Delete();
            rows = LocalTables.localtables.projecttables.Tables["ImageStore"].Select("ID_soket = '" + SocketID + "'");
            foreach (var row in rows)
                row.Delete();
        }
        public void deleteMainSocket(string SocketID)
        {
            var used = LocalTables.localtables.projecttables.Tables["PSockets"].Select("ID_soket = '" + SocketID + "'");
            if (used.Length > 0)
            {


                DialogResult cikis = new DialogResult();
                cikis = MessageBox.Show("Kaldırmak istediğiniz soket kullanımda, devam etmek istiyormusunuz ?", "Uyarı", MessageBoxButtons.YesNo);
                if (cikis == DialogResult.Yes)
                {
                    var rows = LocalTables.localtables.maintables.Tables["Sockets"].Select("ID_soket = '" + SocketID + "'");
                    foreach (var row in rows)
                        row.Delete();
                    rows = LocalTables.localtables.maintables.Tables["IO_connections"].Select("ID_soket = '" + SocketID + "'");
                    foreach (var row in rows)
                        row.Delete();
                    rows = LocalTables.localtables.maintables.Tables["ImageStore"].Select("ID_soket = '" + SocketID + "'");
                    foreach (var row in rows)
                        row.Delete();
                }
            }
            else
            {
                var rows = LocalTables.localtables.maintables.Tables["Sockets"].Select("ID_soket = '" + SocketID + "'");
                foreach (var row in rows)
                    row.Delete();
                rows = LocalTables.localtables.maintables.Tables["IO_connections"].Select("ID_soket = '" + SocketID + "'");
                foreach (var row in rows)
                    row.Delete();
                rows = LocalTables.localtables.maintables.Tables["ImageStore"].Select("ID_soket = '" + SocketID + "'");
                foreach (var row in rows)
                    row.Delete();
            }
       
        }
        public void socket2Project(string SocketID)
        {
            string searchexp = "ID_soket = '" + SocketID + "'";
            if (getFromLocalTablesproject("PSockets").Select(searchexp).Length > 0)
            {
                MessageBox.Show("Soket zaten kullanımda");
            }else
            {
                LoadOption loadop = new LoadOption();
                var socketrow = LocalTables.localtables.maintables.Tables["Sockets"].Select(searchexp);
                var imagerow = LocalTables.localtables.maintables.Tables["ImageStore"].Select(searchexp);
                var iorow = LocalTables.localtables.maintables.Tables["IO_connections"].Select(searchexp);
                getFromLocalTablesproject("PSockets").ImportRow(socketrow[0]);
                if (iorow.Length > 0)
                {
                    foreach (var row in iorow)
                        getFromLocalTablesproject("PIO_connection").ImportRow(row);
                }
                else
                {
                    for(int i = 0; i< Convert.ToInt32(socketrow[0][2])+ Convert.ToInt32(socketrow[0][3])*2; i++)
                    {
                        getFromLocalTablesproject("PIO_connection").Rows.Add(Guid.NewGuid().ToString(), socketrow[0][0], i, null);
                    }
                   
                }
               
                
                getFromLocalTablesproject("ImageStore").ImportRow(imagerow[0]);

            }


        }
        public void setSocketImage(string SocketID, Image img)
        {
                     
                if (LocalTables.localtables.maintables.Tables["ImageStore"].Select("ID_soket = '" + SocketID + "'").Length > 0)
                {
                    byte[] data = image2Blob(img);
                    LocalTables.localtables.maintables.Tables["ImageStore"].Select("ID_soket ='" + SocketID + "'")[0][2] = data;
                  
                }
                else
                {
                    byte[] data = image2Blob(img);
                    LocalTables.localtables.maintables.Tables["ImageStore"].Rows.Add(SocketID, null, data, imageSize(img, data.Length));
               
                   
                }
        }

        private string getSocketNameInfo(String SocketID)
        {
            DataTable table = new DataTable();
            return getFromLocalTablesproject("PSockets").Select("ID_soket = '" + SocketID + "'")[0].ItemArray.ElementAt(1).ToString();       
        }


    }





}
