using System.Threading.Tasks;

namespace chat_server
{
    /// <summary>
    /// Handles behavior logic for displaying all rooms to a player.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/25/18
    /// </summary>
    public class RoomsCommand : ICommand
    {
        public CommandType Type
        {
            get
            {
                return CommandType.Rooms;
            }
        }

        public IUser User { get; set; }

        public RoomsCommand(IUser user)
        {
            User = user;
        }

        public async Task Execute(string input)
        {
            var response = $"Active rooms are:\n-------------------";

            foreach (var room in User.CurrentServer.Rooms.Values)
            {
                response += $"\n* {room.Name} ({room.Users.Count})";
            }

            response += $"\n-------------------";

            await User.Write(response);
        }
    }
}
