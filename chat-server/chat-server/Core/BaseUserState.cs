using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System;

namespace chat_server
{
    /// <summary>
    /// Base class for implementing states for a user.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/25/18
    /// </summary>
    public abstract class BaseUserState : IUserState
    {
        public ChatUser User { get; set; }

        public BaseUserState(ChatUser context)
        {
            User = context;
        }

        public async Task Handle()
        {
            var stream = User.NetworkClient.GetStream();
            var buffer = new byte[4096];

            while (User.CurrentState == this)
            {
                if (stream.DataAvailable)
                {
                    var length = await stream.ReadAsync(buffer, 0, buffer.Length);
                    var input = Encoding.ASCII.GetString(buffer, 0, length);
                    input = Regex.Replace(input, @"\t|\n|\r", "");
                    await ProcessInput(input);
                }
            }
        }

        public abstract Task OnEnter();
        public abstract Task OnExit();

        internal async Task NextState(IUserState state)
        {
            await User.SetState(state);
        }

        internal abstract Task ProcessInput(string input);
    }
}
