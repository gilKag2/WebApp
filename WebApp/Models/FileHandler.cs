using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}