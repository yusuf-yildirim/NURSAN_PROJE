using DevExpress.Utils.CommonDialogs.Internal;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
<<<<<<< HEAD
            System.Windows.Forms.DialogResult rs = XtraMessageBox.Show("Fazı ve içindeki bağlantıları Kalıcı olarak silmek üzeresiniz EMİN MİSİNİZ ?", "Silme Bilgisi", MessageBoxButtons.YesNo);
            if (rs == System.Windows.Forms.DialogResult.Yes)
            {
                var rows = getFromLocalTablesproject("PConnections").Select("ID_etap_1 = '" + PhaseID + "'");

=======


            System.Windows.Forms.DialogResult rs = XtraMessageBox.Show("Fazı ve içindeki bağlantıları Kalıcı olarak silmek üzeresiniz EMİN MİSİNİZ ?", "Silme Bilgisi", MessageBoxButtons.YesNo);
            if (rs == System.Windows.Forms.DialogResult.Yes)//Yes butonunu tıklar isek
            {
                var rows = getFromLocalTablesproject("PConnections").Select("ID_etap_1 = '" + PhaseID + "'");

>>>>>>> master
                foreach (var row in rows)
                {
                    row.Delete();
                }
                getFromLocalTablesproject("tbl_etap").Select("ID_etap = '" + PhaseID + "'")[0].Delete();

            }
<<<<<<< HEAD
            else
            {
                XtraMessageBox.Show("İşlem iptal edildi!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
=======
            else//No butonuna tıklar isek
            {
                XtraMessageBox.Show("İşlem iptal edildi!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



          

>>>>>>> master
        }


    }
    }
