using System.Threading.Tasks;

namespace chat_server
{
    /// <summary>
    /// Handles behavior logic for displaying an error message.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/25/18
    /// </summary>
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
            await User.Write($"[{input}] not recognized -- type /help for the available commands.");
        }
    }
}
