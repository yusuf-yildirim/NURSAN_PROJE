using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NURSAN_PROJE.SQL
{
    public partial class DataManager
    {



        private string getColorInfo(String ColorID)
        {
            string color;    
            color = getFromLocalTablesmain("Colours").Select("ID_color = '" + ColorID + "'")[0][1].ToString();           
            Console.WriteLine("BULUNAN RENK : " + color);        
            return color;
        }
    }
}
