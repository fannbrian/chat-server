using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;

namespace chat_server
{
    /// <summary>
    /// A user of this application
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/22/18
    /// </summary>
    public class ChatUser : IUser
    {
        public string Name { get; set; }
        public IRoom CurrentRoom { get; set; }
        public IServer CurrentServer { get; private set; }
        public IUserState CurrentState { get; private set; }
        public TcpClient NetworkClient { get; private set; }

        private NetworkStream _stream { get; set; }

        public ChatUser(TcpClient client, IServer server)
        {
            NetworkClient = client;
            CurrentServer = server;
            _stream = client.GetStream();
        }

        public void Run()
        {
            Task.Run(() => SetState(new NamingState(this)));
        }

        public async Task Message(IMessage message)
        {
            var output = Encoding.Default.GetBytes(message.ToString() + "\n");
            await _stream.WriteAsync(output, 0, output.Length);
        }

        public async Task Write(string message)
        {
            var output = Encoding.Default.GetBytes($"{message.ToString()}{AnsiColor.RESET}\n");
            await _stream.WriteAsync(output, 0, output.Length);
        }

        public async Task SetState(IUserState state)
        {
            if (CurrentState != null)
            {
                await CurrentState.OnExit();
            }

            CurrentState = state;

            await state.OnEnter();
            await state.Handle();
        }
    }
}
