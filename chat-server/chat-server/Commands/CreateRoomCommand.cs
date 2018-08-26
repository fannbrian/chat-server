using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace chat_server
{
    /// <summary>
    /// Handles behavior logic for creating a chatroom.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/25/18
    /// </summary>
    public class CreateRoomCommand : ICommand
    {
        public CommandType Type
        {
            get
            {
                return CommandType.CreateRoom;
            }
        }

        public IUser User { get; set; }

        public CreateRoomCommand(IUser user)
        {
            User = user;
        }

        public async Task Execute(string input)
        {
            // Group 0: /join
            // Group 1: Name
            var regex = new Regex($"{CommandPhrase.START_CHARACTER}{CommandPhrase.CREATE_ROOM} (.*)");
            var match = regex.Match(input);
            var roomName = match.Groups[1].Value;

            // Handle edge case of no room name.
            if (roomName == "")
            {
                await User.Write($"{AnsiColor.RED}{AnsiColor.BOLD}Please enter a room name.");
            }
            // If room does not exist, create it and add user to it.
            else if (!User.CurrentServer.Rooms.TryGetValue(roomName, out var room))
            {
                var newRoom = new ChatRoom(roomName, false);
                User.CurrentServer.Rooms.Add(roomName, newRoom);
                User.CurrentRoom = newRoom;
                newRoom.Users.Add(User);
            }
            // Send error message if room exists.
            else
            {
                await User.Write($"{AnsiColor.RED}{AnsiColor.BOLD}{roomName} already exists.");
            }
        }
    }
}
