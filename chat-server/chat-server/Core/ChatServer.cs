using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace chat_server
{
    public class ChatServer : IServer
    {
        static SemaphoreSlim roomSemaphore = new SemaphoreSlim(1, 1);

        public List<IUser> Users { get; private set; }
        public Dictionary<string, IRoom> Rooms { get; private set; }

        TcpListener Listener { get; set; }
        CancellationToken CancelToken { get; set; }

        public ChatServer(IPAddress ip, Int32 port, CancellationToken cancellationToken)
        {
            Listener = new TcpListener(ip, port);
            CancelToken = cancellationToken;

            Rooms = new Dictionary<string, IRoom>()
            {
                { "General", new ChatRoom("General", true) },
                { "Random", new ChatRoom("Random", true) }
            };
            
            Users = new List<IUser>();
        }

        public async Task AddRoom(IRoom room)
        {
            await roomSemaphore.WaitAsync();
            try
            {
                Rooms.Add(room.Name, room);
            }
            finally
            {
                roomSemaphore.Release();
            }
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

                    var msg = System.Text.Encoding.ASCII.GetBytes("Hello! Welcome to my chat server. Please enter a name to begin!\n");
                    await stream.WriteAsync(msg, 0, msg.Length);
                    
                    // Clear weird data
                    await stream.ReadAsync(new byte[4096], 0, 4096);

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

            Console.WriteLine("Task has ended.");
        }
    }
}
