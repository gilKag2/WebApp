using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Models
{
    //singleton.
    public sealed class Connection
    {
        private double _lon;
        public double Lon
        {
            get {return _lon; }
            set {_lon = value;}
        }
        private double _lat;
        public double Lat
        {
            get { return _lat; }
            set { _lat = value; }
        }

        volatile private TcpClient _client;
        private bool _isConnected;
        IPEndPoint ep;
        private static Connection _instance = null;
        private static readonly object padLock = new object();
        public static Connection Instance
        {
            get
            {
                lock (padLock)
                {
                    if (_instance == null)
                        _instance = new Connection();
                    return _instance;
                }
            }
        }
        public Connection()
        {
            _isConnected = false;
            
        }

        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
        }
        public void Connect(string ip, int port)
        {
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
            _client = new TcpClient();
            Thread thread = new Thread(() =>
            {
                // try to connect until sucess.
                while (!_client.Connected)
                {
                    try
                    {
                        _client.Connect(ep);
                    }
                    catch (SocketException) { }
                }
            });
            thread.Start();
            
            _isConnected = true;
        }

        public void ReadData()
        {
            using (NetworkStream stream = _client.GetStream())
            {
                try
                {
                    byte[] data = new Byte[_client.ReceiveBufferSize];
                    Int32 bytesReaden = stream.Read(data, 0, data.Length);
                    string[] parsedData = Encoding.ASCII.GetString(data, 0, bytesReaden).Split(',');

                    Lon = Convert.ToDouble(parsedData[0]);
                    Lat = Convert.ToDouble(parsedData[1]);
                }
                catch (SocketException) { };
            }
        }

        public void CloseConnection()
        {
            if (_client != null && IsConnected)
            {

                _client.Close();
                _isConnected = false;

            }
        }
    }
}
