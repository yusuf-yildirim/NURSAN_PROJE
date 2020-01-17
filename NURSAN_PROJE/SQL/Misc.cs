﻿using System;
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
                MessageBox.Show("BU METİN SAYIYA DÖNÜŞTÜRÜLEMEZ = "+source.ToString());
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
                jpegByteSize = ConvertBytesToMegabytes(ms.Length);    
                return jpegByteSize; 
            }
        }
        static string ConvertBytesToMegabytes(long bytes)
        {
            return ((bytes / 1024f) / 1024f).ToString();
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
        bool pointingstarted = false;
        public string getIOPointNumber()
        {
            var rows = getFromLocalTablesproject("PIO_connection").Select("","IO_PIN ASC");           
            int syc = 1;
            int freepoint = 0;           
            foreach(var row in rows)
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
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
             
              
            }
            //  MessageBox.Show();
            getFromLocalTablesproject("PIO_connection").Rows.Add("TEMP", "TEMP", "TEMP", syc, "TEMP");
            return syc.ToString();

        }
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
        private DataTable getFromLocalTablesmain(string tablename)          
        {
            return LocalTables.localtables.maintables.Tables[tablename];
        }
        public DataTable getFromLocalTablesproject(string tablename)
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
