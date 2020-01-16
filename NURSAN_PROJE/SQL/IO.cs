using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NURSAN_PROJE.SQL
{
    public partial class DataManager
    {
    
   
        public void updateIObySocketID(string SocketID, string[,] tp_parameters)
        {
            if(getFromLocalTablesproject("PSockets").Select("ID_soket ='" + SocketID + "'").Length > 0)
            {
                if(getFromLocalTablesproject("PIO_connection").Select("ID_soket ='" + SocketID + "'").Length > 0)
                {
                    Console.WriteLine("Var");
                    var rows = getFromLocalTablesproject("PIO_connection").Select("ID_soket ='" + SocketID + "'");
                    int i = 0;
                    foreach(var row in rows)
                    {
                        try
                        {
                            if (tp_parameters[2, i].Length>0)
                            {
                                row[2] = tp_parameters[1, i];
                                row[3] = tp_parameters[2, i];
                            }
                            else
                            {
                                row[2] = tp_parameters[1, i];
                                row[3] = DBNull.Value;
                                Console.WriteLine("WARNING IO:64");
                            }          
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.StackTrace);
                            Console.WriteLine("WARNING IO:69");
                        }
                        i++;
                    }
                }
                else
                {
                    MessageBox.Show("Eğer kod buraya girdiyse todo ekle!");
                    Console.WriteLine("Yok");
                  
                }
            }
            else
            {
                MessageBox.Show("Soket Bulunmuyor Güncelleme İptal Edildi");
            }  

        }


        private void updateMappedIOTable(string SocketID)
        {
            DataTable iotable = new DataTable("MAPPEDIO");
            int i = 0;
            try
            {
                
                iotable.Columns.Add("PİN ADI");
                iotable.Columns.Add("SOKET PİNİ:)");
                iotable.Columns.Add("IO PİNİ");
                var rows = getFromLocalTablesproject("PIO_connection").Select("ID_soket ='" + SocketID + "'");
                DataTable temp = new DataTable();
                temp.Columns.Add("PİN ADI");
                temp.Columns.Add("SOKET PİNİ:)");
                temp.Columns.Add("IO PİNİ");
                int tempi = 1;
                foreach (var row in rows)
                {
                    string pinname, socketpin, ıopin;
                    socketpin = row[2].ToString();
                    ıopin = row[3].ToString();
                    if (row[4] == DBNull.Value)
                    {
                        pinname = getSocketNameInfo(row[1].ToString());
                        iotable.Rows.Add(pinname, socketpin, ıopin);

                    }
                    else
                    {
                        if (row[4].ToString() == "-")
                        {
                            pinname = getSocketNameInfo(row[1].ToString()) + "-SW-" + tempi+"(-)";
                        }
                        else
                        {
                            pinname = getSocketNameInfo(row[1].ToString()) + "-SW-"+ tempi + "(+)";
                            tempi++;
                        }
                        
                        temp.Rows.Add(pinname, socketpin, ıopin);
                       
                    }


                }
                foreach (DataRow rowtemp in temp.Rows)
                {
                    iotable.ImportRow(rowtemp);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
            
            try
            {
                LocalTables.localtables.projecttables.Tables.Add(iotable);
            }
            catch
            {
                LocalTables.localtables.projecttables.Tables.Remove("MAPPEDIO");
                LocalTables.localtables.projecttables.Tables.Add(iotable);
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
