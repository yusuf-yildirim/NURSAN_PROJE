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
                getFromLocalTablesproject("PSockets").Rows.Add(socketrow.ElementAt(0).ItemArray.ElementAt(0), socketrow.ElementAt(0).ItemArray.ElementAt(1), socketrow.ElementAt(0).ItemArray.ElementAt(2), socketrow.ElementAt(0).ItemArray.ElementAt(3), socketrow.ElementAt(0).ItemArray.ElementAt(4));
                foreach (var row in iorow)
                    getFromLocalTablesproject("PIO_connection").Rows.Add(row[0], row[1], row[2], row[3]);
                
                getFromLocalTablesproject("ImageStore").Rows.Add(imagerow[0].ItemArray.ElementAt(0), imagerow[0].ItemArray.ElementAt(1), imagerow[0].ItemArray.ElementAt(2), imagerow[0].ItemArray.ElementAt(3));

            }


        }
        public void setSocketImage(string SocketID, Image img)
        {
            try
            {
               byte[] data = image2Blob(img);
               LocalTables.localtables.maintables.Tables["ImageStore"].Rows.Add(SocketID, null, data, imageSize(img, data.Length));            
              
            }
            catch
            {
                byte[] data = image2Blob(img);
                LocalTables.localtables.maintables.Tables["ImageStore"].Select("ID_soket ='" + SocketID + "'").SetValue(data, 2);
            }
        }




    }





}
