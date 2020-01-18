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


        //  UPDATE
        ///<summary>
        ///Splice güncellemesi için kullanılır.
        ///</summary>
        public void updateComponent(string componentID , String name)
        {
            var row = getFromLocalTablesmain("Components").Select("ID_component = '" + componentID + "'")[0];
            row[1] = name;

        }
        ///<summary>
        ///Kapasitör ve direnç güncellemesi için kullanılır.
        ///</summary>
        public void updateComponent(string componentID, String name, int value, int valuemultiplier, int tolerence)
        {
            var row = getFromLocalTablesmain("Components").Select("ID_component = '" + componentID + "'")[0];
            row[1] = name;
            row[3]= value;
            row[4] = valuemultiplier;
            row[5] = tolerence;            
        }
        ///<summary>
        ///Diyot güncellemesi için kullanılır.
        ///</summary>
        public void updateComponent(string componentID,  String name, int forwardvoltage, int tolerence)
        {
            var row = getFromLocalTablesmain("Components").Select("ID_component = '" + componentID + "'")[0];
            row[1] = name;
            row[3] = forwardvoltage;
            row[5] = tolerence;
           
        }
        ///<summary>
        ///Termistör güncellemesi için kullanılır.
        ///</summary>
        public void updateComponent(string componentID,  String name, int Comparasiontolerence, int firsttestpoint, int secondtestpoint, int minResistance, int maxResistence, int minResistincemultiplier, int maxResistenceMultiplier)
        {
            var row = getFromLocalTablesmain("Components").Select("ID_component = '" + componentID + "'")[0];
            row[1] = name;
            row[6] = firsttestpoint.ToString();
            row[7] = secondtestpoint.ToString();
            row[8] = minResistance;
            row[9] = minResistincemultiplier;
            row[10] = maxResistence;
            row[11] = Comparasiontolerence;
            row[12] = maxResistenceMultiplier;
        }
        ///<summary>
        ///Generic Componenet güncellemesi için kullanılır.
        ///</summary>
        public void updateComponent(string componentID,  String name, int testCurrent, int testCurrentMultiplier, int voltageDrop, int tolerence)
        {
            var row = getFromLocalTablesmain("Components").Select("ID_component = '" + componentID + "'")[0];
            row[1] = name;
            row[3] = testCurrent;
            row[4] = testCurrentMultiplier;
            row[5] = tolerence;
            row[8] = voltageDrop;
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
                if(rows[0].RowState != System.Data.DataRowState.Added)
                {
                    rows[0].SetAdded();
                    getFromLocalTablesproject("PComponents").ImportRow(rows[0]);
                    rows[0].AcceptChanges();
                }
                else
                {
                    getFromLocalTablesproject("PComponents").ImportRow(rows[0]);
                }
              
            }         
        }




        private string getComponentInfo(String componenetID)
        {         
            return getFromLocalTablesproject("PComponents").Select("ID_component = '" + componenetID + "'")[0][1].ToString();          
        }



    }

}
