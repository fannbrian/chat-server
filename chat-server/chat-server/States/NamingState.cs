using System.Threading.Tasks;

namespace chat_server
{
    /// <summary>
    /// State object for initially naming a user.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/22/18
    /// </summary>
    public class NamingState : BaseUserState
    {
        public NamingState(ChatUser context) : base(context) { }

        public override async Task OnEnter()
        {
            await User.Write($"{AnsiColor.BOLD}Welcome to my chat server. Please enter a name to begin!");
        }

        public override async Task OnExit()
        {
            await User.Write($"Your name has been set to: {AnsiColor.BLUE}{User.Name}");
        }

        internal override async Task ProcessInput(string input)
        {
            // Remove spaces from name.
            input = input.Replace(" ", "");

            // Error and return if name exists.
            foreach (var user in User.CurrentServer.Users)
            {
                if (user.Name == input)
                {
                    await User.Write($"{AnsiColor.RED}That name is taken. Try again!");
                    return;
                }
            }
            
            // Set name and transition to next state.
            User.Name = input;
            await NextState(new JoiningState(User));
        }
    }
}
