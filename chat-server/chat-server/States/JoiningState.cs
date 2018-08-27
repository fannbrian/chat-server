using System.Threading.Tasks;

namespace chat_server
{
    /// <summary>
    /// State object for user handling behavior while outside a chatroom.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/25/18
    /// </summary>
    public class JoiningState : BaseUserState
    {
        public JoiningState(ChatUser context) : base(context) { }

        // Array of command types that are valid while in this state.
        private readonly CommandType[] ValidCommands = { CommandType.Help, CommandType.Quit, CommandType.Join, CommandType.CreateRoom, CommandType.SetName, CommandType.Rooms };

        public override async Task OnEnter()
        {
            await User.Write($"Enter a room by typing in /join <chatroom>. E.g. /join General" +
                             $"\nYou can check what rooms are available with /rooms");
        }

        public override async Task OnExit()
        {
            await User.Write($"You have joined {User.CurrentRoom.Name}.");
        }

        internal override async Task ProcessInput(string input)
        {
            // Instantiate and execute the appropriate command based on input.
            var command = CommandFactory.Instance.Instantiate(User, input, ValidCommands);
            await command.Execute(input);
            
            // If user is in a room, transition to next state.
            if (User.CurrentRoom != null)
            {
                await NextState(new ChattingState(User));
            }
        }
    }
}
