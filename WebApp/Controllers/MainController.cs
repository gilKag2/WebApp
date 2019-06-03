﻿
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
        public ActionResult display(string ip, int port)
        {
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
                Location location = c.GetLocation;
                UpdateSessionValues(location.Lon, location.Lat);
            }
            // file case.
            catch
            {
                // case for reading the file here.

            }

            return View();
        }

        [HttpGet]
        public ActionResult displayWithRoute(string ip, int port, int refreshRate)
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
        public ActionResult saveToFile(string ip, int port, int refreshRate, int duration, string fileName)
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
        [HttpGet]
        private void UpdateSessionValues(double lon, double lat, int refresheRate = 0, int duration = 0)
        {
            Session["Lon"] = lon;
            Session["Lat"] = lat;
            Session["RefreshRate"] = refresheRate;
            Session["Duration"] = duration;
        }

    }
}