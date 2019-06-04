
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{

    public class MainController : Controller
    {
       
        public ActionResult Index()
        {
            ViewBag.BackgroundImage = "/Views/Images/export-map-share.png";
            return View();
        }
        [HttpGet]
        public ActionResult Display(string ip, int port)
        {
            Location location;
            int refreshRate = 0;
            try
            {
                // if the ip adress isnt valid, and excetion will be catched, and go to the mission with the file.
                IPAddress.Parse(ip);
                // ip is valid, connect.
                Connection c = Connection.Instance;
                c.Connect(ip, port);
                if (!c.IsConnected) return View();

                // read the lat and lon values from the simulator.
                 c.ReadData();
                 location = c.GetLocation;
            }
            // file case.
            catch
            {
                FileHandler fh = FileHandler.Instance;
                // the ip in this case is the fileName.
                location = fh.load(ip);
                // the port in this case is the refreshe rate.
                refreshRate = port;
            }
            UpdateSessionValues(location.Lon, location.Lat, refreshRate);
            return View();
        }

        [HttpGet]
        public ActionResult DisplayWithRoute(string ip, int port, int refreshRate)
        {
            Connection c = Connection.Instance;
            c.Connect(ip, port);
            if (!c.IsConnected) return View();

            // read the values from the simulator.
            c.ReadData();
            Location location = c.GetLocation;
            UpdateSessionValues(location.Lon, location.Lat, refreshRate);

            return View();
        }
        [HttpGet]
        public ActionResult SaveToFile(string ip, int port, int refreshRate, int duration, string fileName)
        {
            Connection c = Connection.Instance;
            c.Connect(ip, port);
            if (!c.IsConnected) return View();

            // read the values from the simulator.
            c.ReadData();
            Location location = c.GetLocation;
            UpdateSessionValues(location.Lon, location.Lat, refreshRate, duration);

            FileHandler fh = FileHandler.Instance;


            return View();
        }
        [HttpPost]
        public Location postLocation()
        {
            // idk
        }
        
        public Location

        private void UpdateSessionValues(double lon, double lat, int refresheRate = 0, int duration = 0)
        {
            Session["Lon"] = lon;
            Session["Lat"] = lat;
            Session["RefreshRate"] = refresheRate;
            Session["Duration"] = duration;
        }

    }
}