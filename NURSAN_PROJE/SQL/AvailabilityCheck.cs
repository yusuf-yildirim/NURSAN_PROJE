using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NURSAN_PROJE.SQL
{
    public partial class DataManager
    {


        ///<summary>
        ///Soket ismi uygunluğunu kontrol eder bool döndürür.
        ///</summary>
        public bool checknameAvailability(String name, Databases database)
        {
            if (database == Databases.Main)
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
        public bool checklednumberAvailability(String lednumber, Databases database)
        {
            if (database == Databases.Main)
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

        public bool checksplicenameAvailability(string splicename)
        {
            var rows = getFromLocalTablesmain("Components").Select("Component_name ='" + splicename + "'");
            if (rows.Length > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        ///<summary>
        ///Test noktasının müsaitliği kontrol eder bool olarak döndürür.
        ///</summary>
        public bool checkIOpoint(string point)
        {
            var rows = getFromLocalTablesproject("PIO_connection").Select("IO_PIN =" + Convert.ToInt32(point) + "", "IO_PIN ASC");
            if (rows.Length > 0)
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
        public string checkIOpointWhoUse(string point, string name)
        {
            var rows = getFromLocalTablesproject("PIO_connection").Select("IO_PIN =" + Convert.ToInt32(point) + "", "IO_PIN ASC");

            if (rows.Length > 0)
            {
                if (rows[0][1].ToString() == "TEMP")
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

    }
}
