using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NURSAN_PROJE.SQL
{
    public partial class DataManager
    {
        public void addSocket(object[] soc_parameters, string[,] tp_parameters)
        {
            LocalTables.localtables.maintables.Tables["Sockets"].Rows.Add(soc_parameters[0].ToString(), 
                                                                          soc_parameters[1].ToString(), 
                                                                          str2ınt(soc_parameters[2]),
                                                                          str2ınt(soc_parameters[3]),
                                                                          str2ınt(soc_parameters[4]));
            addIOforSocket( soc_parameters, tp_parameters);
        }
  
        public void deleteSocket()
        {
            
        }
        public void socket2Project()
        {

        }
      




    }





}
