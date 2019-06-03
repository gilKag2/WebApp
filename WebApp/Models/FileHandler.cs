using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebApp.Models
{
    public sealed class FileHandler
    {
        private static FileHandler _instance = null;
       
        private static readonly object padLock = new object();
        public static FileHandler Instance
        {
            get
            {
                lock (padLock)
                {
                    if (_instance == null)
                        _instance = new FileHandler();
                    return _instance;
                }
            }
        }

        public class Data
        {
            public Data() { }
            public Data(Location location, double throttle, double rudder)
            {
                Location = location;
                Throttle = throttle;
                Rudder = rudder;
            }
            private Location location;
            public Location Location
            {
                get { return location; }
                set { location = value; }
            }
            private double _throttle;
            public double Throttle
            {
                get{ return _throttle;}
                set { _throttle = value; }
            }
            private double _rudder;
            public double Rudder
            {
                get { return _rudder; }
                set { _rudder = value; }
            }
        }

        // load
        


        // save
        public void Save(double lon, double lat, double rudder, double throttle, string fileName)
        {
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory;
            filePath += "/" + fileName;          //// check this!!!!!!!!!!!!1
            // data object to pass to the xml writer.
            Data data = new Data(new Location(lat, lon), throttle, rudder);
            WriteToXml<Data>(filePath, data);
        }


        public static void WriteToXml<T>(string filePath, T objectToWrite) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(filePath, false);
                serializer.Serialize(writer, objectToWrite);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        public T ReadToXml<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(filePath);
                return (T)serializer.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}