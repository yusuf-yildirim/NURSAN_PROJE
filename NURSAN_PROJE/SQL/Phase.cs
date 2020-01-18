using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NURSAN_PROJE.SQL
{
    partial class DataManager
    {
        ///<summary>
        ///Projeye faz ekler
        ///</summary>
        public void addPhase(string phaseName)
        {
            LocalTables.localtables.projecttables.Tables["tbl_etap"].Rows.Add(Guid.NewGuid().ToString(), phaseName);
        }




    }
    }
