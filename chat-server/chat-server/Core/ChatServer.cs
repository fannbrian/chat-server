using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace chat_server
{
    /// <summary>
    /// Listens for incoming TCP Connection requests and opens connections for them. Also topmost hierarchy
    /// Server => Room => Users.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/25/18
    /// </summary>
    public class ChatServer : IServer
    {
        public List<IUser> Users { get; private set; }
        public Dictionary<string, IRoom> Rooms { get; private set; }

        TcpListener Listener { get; set; }

        public ChatServer(IPAddress ip, Int32 port)
        {
            Listener = new TcpListener(ip, port);

            Rooms = new Dictionary<string, IRoom>()
            {
                { "General", new ChatRoom("General", true) },
                { "Random", new ChatRoom("Random", true) }
            };
            
            Users = new List<IUser>();
        }
        
        public async Task Run()
        {
            Listener.Start();

            Console.WriteLine("Server started");

            while(true)
            {
                try
                {
                    var client = await Listener.AcceptTcpClientAsync();
                    var stream = client.GetStream();

                    var user = new ChatUser(client, this);
                    Users.Add(user);
                    user.Run();

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
