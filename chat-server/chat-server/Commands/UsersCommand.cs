using System.Threading.Tasks;

namespace chat_server
{
    /// <summary>
    /// Handles behavior logic for showing all users in a user's current room.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/25/18
    /// </summary>
    public class UsersCommand : ICommand
    {
        public CommandType Type
        {
            get
            {
                return CommandType.Rooms;
            }
        }

        public IUser User { get; set; }

        public UsersCommand(IUser user)
        {
            User = user;
        }

        public async Task Execute(string input)
        {
            var response = $"{AnsiColor.BOLD}Users in {AnsiColor.RED}{User.CurrentRoom.Name}{AnsiColor.RESET} {AnsiColor.BOLD}are:{AnsiColor.RESET}\n-------------------{AnsiColor.BLUE}{AnsiColor.BOLD}";

            foreach (var user in User.CurrentRoom.Users)
            {
                response += $"\n* {user.Name}";
            }

            response += $"{AnsiColor.RESET}\n-------------------";

            await User.Write(response);
        }
    }
}
