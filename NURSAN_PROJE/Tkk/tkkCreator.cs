using NURSAN_PROJE.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NURSAN_PROJE.Tkk
{
    class tkkCreator
    {
        private DataManager manager;
        
        public tkkCreator(DataManager manager)
        {
            this.manager = manager;
        }
        
        public void test(int syc)
        {
            for(int i = 1; i <= syc; i++)
            {
                string originID = getOriginIDbyOrder(i.ToString());
                string destinationID = getDestinationIDbyOrder(i.ToString());
                Console.WriteLine(i.ToString().PadLeft(4, '0') +" 01 "+getIO(originID) +" "+ getIO(destinationID) + " 0000 0000 0000 0000 \"METİN\" \"" + getNameWithID(originID)+"-"+getPin(originID)+"\" \"" + getNameWithID(destinationID) + "-" + getPin(destinationID) + "\" \""+ getLed(originID)+"\" \""+ getLed(destinationID)+"\"");
            }          
        }

        public string getOriginIDbyOrder(string order)
        {
          //  MessageBox.Show(order);
           return manager.getFromLocalTablesproject("PConnections").Select("Order = '" + order + "'")[0][2].ToString();
        }
        public string getDestinationIDbyOrder(string order)
        {
            //  MessageBox.Show(order);
            return manager.getFromLocalTablesproject("PConnections").Select("Order = '" + order + "'")[0][4].ToString();
        }
        public string getSocketIDbyIOID(string ID)
        {
            return manager.getFromLocalTablesproject("PIO_connection").Select("ID_IO ='" + ID + "'")[0][1].ToString();
        }
        public string getNameWithID(string ID)
        {
           // MessageBox.Show(ID);
            return manager.getSocketNameInfo(manager.getFromLocalTablesproject("PIO_connection").Select("ID_IO ='" + ID + "'")[0][1].ToString());
        }
        public string getPin(string ID)
        {
            return manager.getFromLocalTablesproject("PIO_connection").Select("ID_IO ='" + ID + "'")[0][2].ToString();
        }
        public string getIO(string ID)
        {            
            return int2hex(Convert.ToInt32(manager.getFromLocalTablesproject("PIO_connection").Select("ID_IO ='" + ID + "'")[0][3]));
        }
        public string getLed(string ID)
        {
           // MessageBox.Show(getSocketIDbyIOID(ID));
            return manager.getFromLocalTablesproject("PSockets").Select("ID_soket ='" + getSocketIDbyIOID(ID) + "'")[0][4].ToString() ;
        }

        public string int2hex(int x)
        {
            return x.ToString("X4");
        }
    }
}
