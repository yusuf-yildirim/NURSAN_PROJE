using System;
using System.Windows.Forms;
using DevExpress.UserSkins;

namespace NURSAN_PROJE
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            BonusSkins.Register();
            //  Application.Run(new Projectwindow());
            tmplog.start_debug();
            Application.Run(args.Length == 0 ? new Projectwindow(string.Empty) : new Projectwindow(args[0]));
          
        }
    }
}
