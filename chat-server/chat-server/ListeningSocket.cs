using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace chat_server
{
    public class ListeningSocket
    {
        TcpListener Listener { get; set; }
        CancellationToken CancelToken { get; set; }

        public ListeningSocket(IPAddress ip, Int32 port, CancellationToken cancellationToken)
        {
            Listener = new TcpListener(ip, port);
            CancelToken = cancellationToken;
        }

        public async Task Run()
        {
            Listener.Start();

            Console.WriteLine("Server has started");

            while(!CancelToken.IsCancellationRequested)
            {
                try
                {
                    var client = await Listener.AcceptTcpClientAsync();
                    var stream = client.GetStream();

                    var msg = System.Text.Encoding.ASCII.GetBytes("HELLO WORLD");
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine("Client Detected");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
