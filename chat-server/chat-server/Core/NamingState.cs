using System;
using System.Threading.Tasks;

namespace chat_server
{
    public class NamingState : BaseUserState
    {
        public NamingState(ChatUser context) : base(context) { }

        public override async Task OnEnter()
        {
            await User.Write("Hello! Welcome to my chat server. Please enter a name to begin!\n");
        }

        public override async Task OnExit()
        {
            await User.Write($"Your name has been set to: {User.Name}\n");
        }

        internal override async Task ProcessInput(string input)
        {
            foreach (var user in User.CurrentServer.Users)
            {
                if (user.Name == input)
                {
                    await User.Write("That name is taken. Try again!\n");
                    return;
                }
            }

            User.Name = input;
            Console.WriteLine(input + " is now in lobby.");
            await NextState(new JoiningState(User));
        }
    }
}
