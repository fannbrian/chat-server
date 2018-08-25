using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System;

namespace chat_server
{
    public class JoinCommand : ICommand
    {
        public CommandType Type
        {
            get
            {
                return CommandType.Whisper;
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
            var regex = new Regex("/join (.*)");
            var match = regex.Match(input);
            var roomName = match.Groups[1].Value;

            Console.WriteLine($"Input is: {input}");
            Console.Write($"[{roomName.Length}]");

            if (User.CurrentServer.Rooms.TryGetValue(roomName, out var room))
            {
                Console.WriteLine(roomName);
                User.CurrentRoom = room;
                room.Users.Add(User);
            }
        }
    }
}
