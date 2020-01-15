using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NURSAN_PROJE.SQL
{
    public partial class DataManager
    {






        private string getComponentInfo(String componenetID)
        {         
            return getFromLocalTablesproject("PComponents").Select("ID_component = '" + componenetID + "'")[0][1].ToString();          
        }



    }

}
