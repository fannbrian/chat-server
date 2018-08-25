using System.Net.Sockets;

namespace chat_server
{
    public class ChatSocket
    {
        IUser User { get; set; }
        TcpClient TcpClient { get; set; }
        NetworkStream Stream { get; set; }

        public ChatSocket(TcpClient client, IServer server)
        {
            TcpClient = client;
            User = new ChatUser(client, server);
            Stream = client.GetStream();
        }
    }
}
