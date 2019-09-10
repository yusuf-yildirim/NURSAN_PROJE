using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NURSAN_PROJE
{
    class Sockets
    {
        private String socketname;
        public Sockets(string newsocketname)
        {
            socketname = newsocketname;

        }
        public string getsocketname()
        {
            return socketname;
        }
    }
}
