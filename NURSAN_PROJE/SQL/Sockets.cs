using DevExpress.XtraEditors;
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

        ///<summary>
        ///Ana veritabanına soket ekler. 5 elemanlı bir object dizisi gerekir.Elemanların sıralaması tabloya uygun olmalıdır.
        ///</summary>
        public void addSocket(object[] soc_parameters)
        {
            LocalTables.localtables.maintables.Tables["Sockets"].Rows.Add(soc_parameters[0].ToString(), 
                                                                          soc_parameters[1].ToString(), 
                                                                          str2ınt(soc_parameters[2]),
                                                                          str2ınt(soc_parameters[3]),
                                                                          str2ınt(soc_parameters[4]));
          
        }
        ///<summary>
        ///Proje soketini(resimler ve ıo bağlantıları dahil olarak) kaldırır. 
        ///</summary>
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
        ///<summary>
        ///Ana veritabanı soketini(resimler ve ıo bağlantıları dahil olarak) kaldırır. Eğer kullanılıyorsa sorar.
        ///</summary>
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

        ///<summary>
        ///Ana veritabanı soketini(resimler ve ıo bağlantıları dahil olarak) proje veritabanına aktarır. Daha önce aktarılmış soketi aktarmaz. Resim yoksa uyarır
        ///</summary>
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
                if (imagerow.Length > 0)
                {
                    if (imagerow[0].RowState != DataRowState.Added)
                    {
                        imagerow[0].SetAdded();
                        getFromLocalTablesproject("ImageStore").ImportRow(imagerow[0]);
                        imagerow[0].AcceptChanges();
                    }
                    else
                    {
                        getFromLocalTablesproject("ImageStore").ImportRow(imagerow[0]);

                    }

                }
                else
                {
                    XtraMessageBox.Show("Soket resmi belirlenmemiş!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if(socketrow[0].RowState  != DataRowState.Added)
                {
                    socketrow[0].SetAdded();
                    getFromLocalTablesproject("PSockets").ImportRow(socketrow[0]);
                    socketrow[0].AcceptChanges();
                }
                else
                {
                    getFromLocalTablesproject("PSockets").ImportRow(socketrow[0]);
                }         
               

                int swcc, pinc;
                swcc = Convert.ToInt32(socketrow[0][3]);
                pinc = Convert.ToInt32(socketrow[0][2]);
                int order = 0;
                for (int i = 0; i < pinc; i++)
                {
                    getFromLocalTablesproject("PIO_connection").Rows.Add(Guid.NewGuid().ToString(), socketrow[0][0], i + 1, null,null);
                    order = i + 1;
                }
                for (int i = 0; i < swcc*2; i++)
                {
                    if(i%2 == 0)
                    {
                        getFromLocalTablesproject("PIO_connection").Rows.Add(Guid.NewGuid().ToString(), socketrow[0][0], i + 1 + order, null, "-");
                    }
                    else
                    {
                        getFromLocalTablesproject("PIO_connection").Rows.Add(Guid.NewGuid().ToString(), socketrow[0][0], i + 1 + order, null, "+");

                    }

                } 
            }
        }
        ///<summary>
        ///Proje soketinin bilgilerini günceller.
        ///</summary>
        public void updateSocketInfo(string SocketID,string socketname,string pinc,string swcc,string ledn)//TO-DO
        {
            var socketrows = getFromLocalTablesproject("PSockets").Select("ID_soket ='" + SocketID + "'");
            socketrows[0][1] = socketname;
            if(str2ınt(ledn) >= 0 && str2ınt(ledn) <= 1024)
            {
                socketrows[0][4] = ledn;
            }
            else
            {
                XtraMessageBox.Show("Led numarası 0 ile 1024 arasında olmalıdır!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
            if (socketrows[0][2].ToString() != pinc || socketrows[0][3].ToString() != swcc)
            {
                DialogResult result = XtraMessageBox.Show("Soket pin ve switch sayısını değiştirmek pin atamalarının silinmesine neden olur.\n Devam Etmek istiyormusunuz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    var iorows = getFromLocalTablesproject("PIO_connection").Select("ID_soket ='" + SocketID + "'");
                    socketrows[0][2] = pinc;
                    socketrows[0][3] = swcc;
                    foreach (var row in iorows)
                    {
                        row.Delete();
                    }
                    int pinnumber = 0;
                    for (int i = 0; i < str2ınt(pinc); i++)
                    {
                        getFromLocalTablesproject("PIO_connection").Rows.Add(Guid.NewGuid().ToString(), SocketID, i + 1, null, null);
                        pinnumber = i + 1;
                    }
                    for (int i = 0; i < str2ınt(swcc) * 2; i++)
                    {
                        if (i % 2 == 0)
                        {
                            getFromLocalTablesproject("PIO_connection").Rows.Add(Guid.NewGuid().ToString(), SocketID, i + 1 + pinnumber, null, "-");
                        }
                        else
                        {
                            getFromLocalTablesproject("PIO_connection").Rows.Add(Guid.NewGuid().ToString(), SocketID, i + 1 + pinnumber, null, "+");

                        }


                    }
                }
                else
                {
                    Console.WriteLine("SOCKET UPDATE ABORTED!");
                }

            }
        }

        ///<summary>
        ///İlgili sokete ait resmi gönderiler Image'a göre ayarlar.
        ///</summary>
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
        ///<summary>
        ///SoketID sine göre soketin ismini döndürür.
        ///</summary>
        private string getSocketNameInfo(String SocketID)
        {
            DataTable table = new DataTable();
            return getFromLocalTablesproject("PSockets").Select("ID_soket = '" + SocketID + "'")[0].ItemArray.ElementAt(1).ToString();       
        }


    }





}
