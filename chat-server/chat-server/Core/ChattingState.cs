using System.Threading.Tasks;

namespace chat_server
{
    public class ChattingState : BaseUserState
    {
        public ChattingState(ChatUser context) : base(context) { }

        private readonly CommandType[] ValidCommands = { CommandType.Help, CommandType.Quit, CommandType.Leave, CommandType.Chat, CommandType.Whisper };

        public override async Task OnEnter()
        {
            await User.CurrentRoom.Write($"{User.Name} has joined {User.CurrentRoom.Name}.\n");
        }

        public override async Task OnExit()
        {
            await User.CurrentRoom.Write($"{User.Name} has left {User.CurrentRoom.Name}.\n");
        }

        internal override async Task ProcessInput(string input)
        {
            var command = CommandFactory.Instance.Instantiate(User, input, ValidCommands);
            await command.Execute(input);
        }
    }
}
