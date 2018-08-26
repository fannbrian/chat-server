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
            IPAddress ip = IPAddress.Any;
            var listen = new ChatServer(ip, port);
            Task.Run(() => listen.Run()).Wait();
        }
    }
}