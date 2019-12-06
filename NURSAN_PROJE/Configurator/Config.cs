using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NURSAN_PROJE.Configurator
{
    class Config
    {


        public void set_connectionstring(string connection,string path)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings[connection].ConnectionString = @"XpoProvider=SQLite;Data Source=" + path;
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");

        }











    }
}
