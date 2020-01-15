using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NURSAN_PROJE.SQL
{
    public partial class DataManager
    {
        private void addIOforSocket(object[] soc_parameters, string[,] tp_parameters)
        {
            for (int i = 0; i < tp_parameters.GetLength(1); i++)
            {
                if (tp_parameters[0, i] == null)
                {
                    break;
                }
                LocalTables.localtables.maintables.Tables["IO_connections"].Rows.Add(Guid.NewGuid().ToString(), soc_parameters[0].ToString(), tp_parameters[1, i], tp_parameters[2, i]);
            }
        }
    }
}
