using DevExpress.UserSkins;
using NURSAN_PROJE.SQL;
using System;
using System.Windows.Forms;

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
          /*  if (args.Length > 0)
            {
                using (DBeng db = new DBeng())
                {

                    db.setProjectPath(args[0]);
                    using (LocalTables locals = new LocalTables(true))
                    {
                        locals.getalltables();
                    }
                }
            }*/
            Application.Run(args.Length == 0 ? new Projectwindow(string.Empty) : new Projectwindow(args[0]));

        }
    }
}
