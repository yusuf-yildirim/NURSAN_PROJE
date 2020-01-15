using System;
using System.Collections.Generic;
using System.Drawing;
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


    }
}
