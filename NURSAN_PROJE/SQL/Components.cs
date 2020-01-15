using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NURSAN_PROJE.SQL
{
    public partial class DataManager
    {

        ///<summary>
        ///Projeye splice eklenirken kullanılmalıdır.Tecnicname tüm aynı komponentler için sabit olmalıdır.
        ///</summary>
        public void addComponent(String technicname, String name)
        {
            getFromLocalTablesmain("Components").Rows.Add(Guid.NewGuid().ToString(), name, technicname, 0, 0, 0, null, null, 0, 0, 0, 0, 0);
          

        }
        ///<summary>
        ///Projeye kapasitör ve direnç eklenirken kullanılmalıdır.Tecnicname tüm aynı komponentler için sabit olmalıdır.
        ///</summary>
        public void addComponent(String technicname, String name, int value, int valuemultiplier, int tolerence)
        {
            getFromLocalTablesmain("Components").Rows.Add(Guid.NewGuid().ToString(), name, technicname, value, valuemultiplier, tolerence, null, null, 0, 0, 0, 0, 0);
          
        }
        ///<summary>
        ///Projeye diyot eklenirken kullanılmalıdır.Tecnicname tüm aynı komponentler için sabit olmalıdır
        ///</summary>
        public void addComponent(String technicname, String name, int forwardvoltage, int tolerence)
        {
            getFromLocalTablesmain("Components").Rows.Add(Guid.NewGuid().ToString(), name, technicname, forwardvoltage, 1, tolerence, null, null, 0, 0, 0, 0, 0);
       
        }
        ///<summary>
        ///Projeye termistör eklenirken kullanılmalıdır.Tecnicname tüm aynı komponentler için sabit olmalıdır
        ///</summary>
        public void addComponent(String technicname, String name, int Comparasiontolerence, int firsttestpoint, int secondtestpoint, int minResistance, int maxResistence, int minResistincemultiplier, int maxResistenceMultiplier)
        {
            getFromLocalTablesmain("Components").Rows.Add(Guid.NewGuid().ToString(), name, technicname, 0, 0, 0, firsttestpoint.ToString(), secondtestpoint.ToString(), minResistance, minResistincemultiplier, maxResistence, Comparasiontolerence, maxResistenceMultiplier);
         
        }
        ///<summary>
        ///Projeye generic component eklenirken kullanılmalıdır.Tecnicname tüm aynı komponentler için sabit olmalıdır
        ///</summary>
        public void addComponent(String technicname, String name, int testCurrent, int testCurrentMultiplier, int voltageDrop, int tolerence)
        {


            getFromLocalTablesmain("Components").Rows.Add(Guid.NewGuid().ToString(), name, technicname, testCurrent, testCurrentMultiplier, tolerence, null, null, voltageDrop, 0, 0, 0, 0);
           
        }
        public void componenet2project(string ComponentID)
        {
            if(getFromLocalTablesproject("PComponents").Select("ID_component='" + ComponentID + "'").Length > 0)
            {
                MessageBox.Show("Komponent Zaten Kullanımda!");
            }
            else
            {
                var rows = getFromLocalTablesmain("Components").Select("ID_component='" + ComponentID + "'");
                getFromLocalTablesproject("PComponents").ImportRow(rows[0]);
            }
        

          
        }




        private string getComponentInfo(String componenetID)
        {         
            return getFromLocalTablesproject("PComponents").Select("ID_component = '" + componenetID + "'")[0][1].ToString();          
        }



    }

}
