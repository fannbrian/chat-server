using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace chat_server
{
    /// <summary>
    /// Handles behavior logic for joining a chatroom.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/25/18
    /// </summary>
    public class JoinCommand : ICommand
    {
        public CommandType Type
        {
            get
            {
                return CommandType.Join;
            }
        }

        public IUser User { get; set; }

        public JoinCommand(IUser user)
        {
            User = user;
        }

        public async Task Execute(string input)
        {
            // Group 0: /join
            // Group 1: Name
            var regex = new Regex($"{CommandPhrase.START_CHARACTER}{CommandPhrase.JOIN}(.*)");
            var match = regex.Match(input);
            var roomName = match.Groups[1].Value;

            if (roomName == "")
            {
                await User.Write($"Please enter a room name.");
            }
            else if (User.CurrentServer.Rooms.TryGetValue(roomName, out var room))
            {
                User.CurrentRoom = room;
                room.Users.Add(User);
            }
            else
            {
                await User.Write($"{roomName} does not exist.");
            }
        }
    }
}
