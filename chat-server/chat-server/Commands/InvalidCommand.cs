using System.Threading.Tasks;

namespace chat_server
{
    public class InvalidCommand : ICommand
    {
        public CommandType Type
        {
            get
            {
                return CommandType.Invalid;
            }
        }

        public IUser User { get; set; }

        public InvalidCommand(IUser user)
        {
            User = user;
        }

        public async Task Execute(string input)
        {
            await User.Write("Command not recognized -- type /help for the available commands.\n");
        }
    }
}
