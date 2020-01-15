using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NURSAN_PROJE.SQL
{
    partial class DataManager
    {

        public void addPhase(string phaseName)//güncelleme gerekir
        {
            LocalTables.localtables.projecttables.Tables["tbl_etap"].Rows.Add(Guid.NewGuid().ToString(), phaseName);
        }




    }
    }
