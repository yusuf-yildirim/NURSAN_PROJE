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
            getFromLocalTablesproject("tbl_etap").Rows.Add(Guid.NewGuid().ToString(), phaseName);
        }

        public void deletePhase(string PhaseID)
        {
           var rows =  getFromLocalTablesproject("PConnections").Select("ID_etap_1 = '"+PhaseID+"'");

            foreach(var row in rows)
            {
                row.Delete();
            }
            getFromLocalTablesproject("tbl_etap").Select("ID_etap = '" + PhaseID + "'")[0].Delete();

        }


    }
    }
