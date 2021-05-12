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
            //Mysqldb.ConnectionString = Mysqldb.GetConnectionString("192.168.2.184", "ulutek_kiosk", "ulutek", "Ulutek_0");
            Mysqldb.ConnectionString = Mysqldb.GetConnectionString("localhost", "ulutek_kiosk", "schoolserver", "School+admin3");
            Mysqldb.Open();
        }
    }
}