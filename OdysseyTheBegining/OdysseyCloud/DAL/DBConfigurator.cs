using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace OdysseyCloud.DAL
{
    public class DBConfigurator
    {
        public static string ConnectionString

        {

            get

            {
                ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["OdysseyDBCloud"];
                if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                    throw new Exception("Fatal error: missing connecting string in web.config file");
               var conString = mySetting.ConnectionString;
                return conString;

            }

        }
      
    }
}