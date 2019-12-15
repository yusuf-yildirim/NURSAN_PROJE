﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NURSAN_PROJE.Configurator
{
    class Config
    {


        public void set_connectionstring(string connection, string path)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings[connection].ConnectionString = @"XpoProvider=SQLite;Data Source=" + path;
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");

        }
        public string get_conn_string(string connection)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");

            return connectionStringsSection.ConnectionStrings[connection].ConnectionString.Substring(connectionStringsSection.ConnectionStrings[connection].ConnectionString.IndexOf(";") + 1, (connectionStringsSection.ConnectionStrings[connection].ConnectionString.Length - connectionStringsSection.ConnectionStrings[connection].ConnectionString.IndexOf(";")) - 1);
            //  return connectionStringsSection.ConnectionStrings[connection].ConnectionString;
        }










    }
}
