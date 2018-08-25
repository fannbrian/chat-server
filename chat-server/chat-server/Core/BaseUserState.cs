using System.Collections.Generic;
using System.Reflection;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace chat_server
{
    public abstract class BaseUserState : IUserState
    {
        public ChatUser User { get; set; }

        public BaseUserState(ChatUser context)
        {
            User = context;
        }

        public async Task Handle(ChatUser user)
        {
            Console.WriteLine("HANDLING NEW STATE");
            var stream = user.NetworkClient.GetStream();
            var buffer = new byte[4096];
            await stream.FlushAsync();
            //await stream.ReadAsync(buffer, 0, buffer.Length);

            while (User.CurrentState == this)
            {
                if (stream.DataAvailable)
                {
                    var length = await stream.ReadAsync(buffer, 0, buffer.Length);
                    var input = Encoding.ASCII.GetString(buffer, 0, length);
                    input = Regex.Replace(input, @"\t|\n|\r", "");
                    Console.WriteLine(input);
                    await ProcessInput(input);
                }
            }
        }

        public abstract Task OnEnter();
        public abstract Task OnExit();

        internal async Task NextState(IUserState state)
        {
            await OnExit();
            Console.WriteLine("EXITED OLD STATE");
            User.SetState(state);
            Console.WriteLine("SET NEW STATE");
            await state.OnEnter();
            Console.WriteLine("ENTERED NEW STATE");
            await state.Handle(User);
        }

        internal abstract Task ProcessInput(string input);
    }
}
