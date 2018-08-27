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
            var response = $"Users in {User.CurrentRoom.Name} are:\n-------------------";

            foreach (var user in User.CurrentRoom.Users)
            {
                response += $"\n* {user.Name}";
            }

            response += $"\n-------------------";

            await User.Write(response);
        }
    }
}
