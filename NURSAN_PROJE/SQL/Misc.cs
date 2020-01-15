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
        public int str2ınt(object source)
        {
            try
            {
                return Convert.ToInt32(source);
            }
            catch
            {
                MessageBox.Show("BU METİN SAYIYA DÖNÜŞTÜRÜLEMEZ");
                return 0;
            }
        }
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
                        Console.WriteLine(workaround.PropertyIdList[i] + " -------------------");

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

        private string imageSize(Image image,int bytelength)
        {
            string jpegByteSize;
            using (var ms = new MemoryStream(bytelength)) // estimatedLength can be original fileLength
            {
                image.Save(ms, ImageFormat.Jpeg); // save image to stream in Jpeg format    
                jpegByteSize = ms.Length.ToString();
                return jpegByteSize; 
            }
        }
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
        private DataTable getFromLocalTablesmain(string tablename)          
        {
            return LocalTables.localtables.maintables.Tables[tablename];
        }
        private DataTable getFromLocalTablesproject(string tablename)
        {
            return LocalTables.localtables.projecttables.Tables[tablename];
        }
       /*public static DataTable Delete(this DataTable table, string filter)
        {
            table.Select(filter).Delete();
            return table;
        }
        public static void Delete(this IEnumerable<DataRow> rows)
        {
            foreach (var row in rows)
                row.Delete();
        }
        */
    }
}
