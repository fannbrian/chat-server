using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;

namespace chat_server
{
    class Program
    {
        static void Main(string[] args)
        {
            Int32 port = 23;
            IPAddress ip = IPAddress.Parse("192.168.1.26");
            var listen = new ListeningSocket(ip, port, new System.Threading.CancellationToken());
            Task.Run(() => listen.Run()).Wait();
        }
    }
}