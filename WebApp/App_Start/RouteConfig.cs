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

            routes.MapRoute("Display", "display/{ip}/{port}",
             defaults: new { controller = "MainController", action = "Display" });

            routes.MapRoute("DisplayWithRoute", "display/{ip}/{port}/{refreshRate}",
                defaults: new { controller = "MainController", action = "DisplayWithRoute" });
            routes.MapRoute("SaveToFile", "save/{ip}/{port}/{refreshRate}/{duration}/{fileName}",
                defaults: new { controller = "MainController", action = "SaveToFile" });
               
        }
    }
}
