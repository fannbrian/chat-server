using System.Threading.Tasks;
using System;

namespace chat_server
{
    /// <summary>
    /// State object for user handling behavior while inside a chatroom.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/25/18
    /// </summary>
    public class ChattingState : BaseUserState
    {
        public ChattingState(ChatUser context) : base(context) { }

        // Array of command types that are valid while in this state.
        private readonly CommandType[] ValidCommands = { CommandType.Help, CommandType.Quit, CommandType.Leave, CommandType.Chat, CommandType.Whisper, CommandType.SetName, CommandType.Users };

        public override async Task OnEnter()
        {
            await User.CurrentRoom.Write($"{User.Name} has joined {User.CurrentRoom.Name}.");
        }

        public override async Task OnExit()
        {
        }
        
        internal override async Task ProcessInput(string input)
        {
            // Instantiate and execute the appropriate command based on input.
            var command = CommandFactory.Instance.Instantiate(User, input, ValidCommands);
            await command.Execute(input);

            // If user has left chatroom, transition to next state.
            if (User.CurrentRoom == null)
            {
                await NextState(new JoiningState(User));
            }
        }
    }
}
