using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApp
{
    
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("display", "display/{ip}/{port}",
             defaults: new { controller = "MainController", action = "display" });

            routes.MapRoute("displayWithRoute", "display/{ip}/{port}/{refreshRate}",
                defaults: new { controller = "MainController", action = "displayWithRoute" });
            routes.MapRoute("saveToFile", "save/{ip}/{port}/{refreshRate}/{duration}{fileName}",
                defaults: new { controller = "MainController", action = "saveToFile" });
               
        }
    }
}
