using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NURSAN_PROJE
{
    static class tmplog
    {
        public static void start_debug()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt");
            }

            // Create a new file     
            using (FileStream fs = File.Create(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt"))
            {
                // Add some text to file    
                Byte[] title = new UTF8Encoding(true).GetBytes("New Log Created\n");
                fs.Write(title, 0, title.Length);
            }
        }
        public static void WriteDebugLog(String Message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
                sw.Flush();
                sw.Close();
            }
            catch
            {

            }
        }
        public static void WriteDebugLog(String Message, bool a)
        {
            if (a)
            {
                StreamWriter sw = null;
                try
                {
                    sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                    sw.WriteLine("Hata Bilgisi : " + Message);
                    sw.Flush();
                    sw.Close();
                }
                catch
                {

                }
            }
            else
            {
                StreamWriter sw = null;
                try
                {
                    sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                    sw.WriteLine(Message);
                    sw.Flush();
                    sw.Close();
                }
                catch
                {

                }
            }

        }
    }
}
