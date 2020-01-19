using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NURSAN_PROJE.SQL
{
    public partial class DataManager
    {
        ///<summary>
        ///Gönderilen veriyi int e dönüştürmeye çalışır. Başaramazsa uyarı verir ve sıfır döndürür.
        ///</summary>
        public int str2ınt(object source)
        {
            try
            {
                return Convert.ToInt32(source);
            }
            catch
            {
                MessageBox.Show("BU METİN SAYIYA DÖNÜŞTÜRÜLEMEZ = "+source.ToString());
                return 0;
            }
        }
        ///<summary>
        ///Gönderilen byte[] türünde fotoğraf bilgisini Image türünü çevirip döndürür.
        ///</summary>
        private Image blob2Image(byte[] imagedata)
        {
            try
            {

                byte[] data = imagedata;
                using (var ms = new MemoryStream(data))
                {
                    Image deneme = Image.FromStream(ms);
                    Image workaround = (Image)deneme.Clone();
                    for (int i = 0; i < workaround.PropertyIdList.Length; i++)
                    {
                      //  Console.WriteLine(workaround.PropertyIdList[i] + " -------------------");

                    }
                    return workaround;                

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
                // Console.WriteLine("RESİM YÜKLENEMEDİ");
                return null;
            }
        }
        ///<summary>
        ///Gönderilen Image türünde fotoğraf bilgisini byte[] türünü çevirip döndürür.
        ///</summary>
        private byte[] image2Blob(System.Drawing.Image imageIn)
        {

            try
            {
                using (var ms = new MemoryStream())
                {

                    imageIn.Save(ms, imageIn.RawFormat);
                    byte[] bytesText = ms.ToArray();
                    return bytesText;
                }
            }
            catch
            {

                imageIn.Save("temp.jpg", ImageFormat.Jpeg);
                byte[] data = null;

                try
                {
                    data = File.ReadAllBytes("temp.jpg");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                return data;
            }
        }
        ///<summary>
        ///Gönderilen Image türünün boyutunu string formatında döndürür.
        ///</summary>
        private string imageSize(Image image,int bytelength)
        {
            string jpegByteSize;
            using (var ms = new MemoryStream(bytelength)) // estimatedLength can be original fileLength
            {
                image.Save(ms, ImageFormat.Jpeg); // save image to stream in Jpeg format    
                jpegByteSize = ConvertBytesToMegabytes(ms.Length);    
                return jpegByteSize; 
            }
        }
        ///<summary>
        ///Gönderilen long türünde byte bilgisini megabyte a çevirip string olarak döndürür.
        ///</summary>
        static string ConvertBytesToMegabytes(long bytes)
        {
            return ((bytes / 1024f) / 1024f).ToString();
        }
        ///<summary>
        ///Gönderilen string türünde guid verisinin doğrulamasını yapıp Boolean olarak cevap döndürür.
        ///</summary>
        public bool GuidCheck(string guid)
        {
            try
            {
                Guid.Parse(guid);
                return true;
            }
            catch
            {
                MessageBox.Show("UUID HATASI : " + guid + " ID GEÇERLİ DEĞİL");
                return false;               
            }
        }


        bool pointingstarted = false;

        ///<summary>
        ///Müsait ilk test noktasını string olarak döndürür döndürdüğü test noktasını geçici olarak ("TEMP","TEMP","döndürülennokta","TEMP") ilgili tabloya kaydeder.
        ///</summary>
        public string getIOPointNumber()
        {
            if(pointingstarted = true)
            {
                var rows = getFromLocalTablesproject("PIO_connection").Select("", "IO_PIN ASC");
                int syc = 1;
                int freepoint = 0;
                foreach (var row in rows)
                {
                    try
                    {
                        if (row[3] != DBNull.Value)
                        {
                            if (Convert.ToInt32(row[3]) == syc)
                            {
                                syc++;
                                continue;
                            }
                            else
                            {
                                freepoint = syc + 1;
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("WARNING! " + row[0] + " is not assigned test point!");

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                }
                //  MessageBox.Show();
                getFromLocalTablesproject("PIO_connection").Rows.Add("TEMP", "TEMP", "TEMP", syc, "TEMP");
                return syc.ToString();
            }
            else
            {
                return "HATA lütfen pointingstarted true olarak ayarlayın";
            }
         

        }

        ///<summary>
        ///Test noktasının müsaitliği kontrol eder bool olarak döndürür.
        ///</summary>
        public bool checkIOpoint(string point)
        {
            var rows = getFromLocalTablesproject("PIO_connection").Select("IO_PIN ="+Convert.ToInt32(point)+"", "IO_PIN ASC");
            if(rows.Length > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        ///<summary>
        ///Test noktasının hangi soket tarafından kullanıldığını kontrol eder. String olarak döndürür.
        ///</summary>
        public string checkIOpointWhoUse(string point,string name)
        {
            var rows = getFromLocalTablesproject("PIO_connection").Select("IO_PIN =" + Convert.ToInt32(point) + "", "IO_PIN ASC");
           
            if (rows.Length > 0)
            {
                if(rows[0][1].ToString() == "TEMP")
                {
                    return name;
                }
                else
                {
                    var rows2 = getFromLocalTablesproject("PSockets").Select("ID_soket ='" + rows[0][1] + "'");
                    return rows2[0][1].ToString();
                }             
            }
            else
            {
                return "NULL";
            }
        }

        ///<summary>
        ///Otomatik test pini atama işlemini başlatır/bitirir.
        ///</summary>
        public void setstartpointing(bool state)
        {
            if(pointingstarted == true && state == true)
            {
                pointingstarted = false;
                var rows = getFromLocalTablesproject("PIO_connection").Select("ID_IO = 'TEMP'");

                foreach (var row in rows)
                {
                    row.Delete();
                }
            }
            pointingstarted = state;
            if(state == false)
            {
                var rows = getFromLocalTablesproject("PIO_connection").Select("ID_IO = 'TEMP'");

                foreach(var row in rows)
                {
                    row.Delete();
                }
            }
        }
        ///<summary>
        ///Manuel test pini atama işlemini başlatır/bitirir.
        ///</summary>
        public void manualpointing(bool state,int ıo)
        {
            pointingstarted = state;
            if (state == false)
            {
                var rows = getFromLocalTablesproject("PIO_connection").Select("ID_IO = 'TEMP'");

                foreach (var row in rows)
                {
                    row.Delete();
                }
            }
            else
            {
                getFromLocalTablesproject("PIO_connection").Rows.Add("TEMP", "TEMP", "TEMP", ıo, "TEMP");
            }
        }
        ///<summary>
        ///Local anaveritabanı tablolarından istenen tabloyu döndürür.
        ///</summary>
        public DataTable getFromLocalTablesmain(string tablename)          
        {
            return localTables.getLocalTable(tablename,Databases.Main);
        }
        ///<summary>
        ///Local proje veritabanı tablolarından istenen tabloyu döndürür.
        ///</summary>
        public DataTable getFromLocalTablesproject(string tablename)
        {
            return localTables.getLocalTable(tablename, Databases.Project);
        }
        ///<summary>
        ///Soket ismi uygunluğunu kontrol eder bool döndürür.
        ///</summary>
        public bool checknameAvailability(String name,Databases database)
        {
            if(database == Databases.Main)
            {
                var rows = getFromLocalTablesmain("Sockets").Select("Adı ='" + name + "'");
                if (rows.Length > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                var rows = getFromLocalTablesproject("PSockets").Select("Adı ='" + name + "'");
                if (rows.Length > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
          
        }
        ///<summary>
        ///Led numarası uygunluğunu kontrol eder bool döndürür.
        ///</summary>
        public bool checklednumberAvailability(String lednumber,Databases database)
        {
            if(database == Databases.Main)
            {
                var rows = getFromLocalTablesmain("Sockets").Select("Led_numarasi =" + lednumber + "");
                if (rows.Length > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                var rows = getFromLocalTablesproject("PSockets").Select("Led_numarasi =" + lednumber + "");
                if (rows.Length > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
           
        }

    }
}
