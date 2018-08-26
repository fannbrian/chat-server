using System.Threading.Tasks;

namespace chat_server
{
    /// <summary>
    /// Handles behavior logic for displaying commands for user.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/25/18
    /// </summary>
    public class HelpCommand : ICommand
    {
        public CommandType Type
        {
            get
            {
                return CommandType.Help;
            }
        }

        public IUser User { get; set; }

        public HelpCommand(IUser user)
        {
            User = user;
        }

        public async Task Execute(string input)
        {
            var response = $"{AnsiColor.BOLD}{MessageConstants.HELP_MESSAGE}";
            await User.Write(response);
        }
    }
}
