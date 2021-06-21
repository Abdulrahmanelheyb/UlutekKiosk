using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UlutekKioskModels;

namespace Kiosk_Managment
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //Mysqldb.ConnectionString = Mysqldb.GetConnectionString(Properties.Settings.Default.IP, "ulutek_kiosk", "ulutek", "Ulutek_0");
            //Mysqldb.ConnectionString = Mysqldb.GetConnectionString("localhost", "ulutek_kiosk", "dev", "developer@E#");
            //Mysqldb.Open();
        }
    }
}
