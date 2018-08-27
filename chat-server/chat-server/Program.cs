using System;
using System.Net;
using System.Threading.Tasks;

namespace chat_server
{
    class Program
    {
        static void Main(string[] args)
        {
            // TcpPort
            Int32 port = 9399;
            IPAddress ip = IPAddress.Any;
            var listen = new ChatServer(ip, port);
            Task.Run(() => listen.Run()).Wait();
        }
    }
}