using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace chat_server
{
    public class WhisperCommand : ICommand
    {
        public CommandType Type
        {
            get
            {
                return CommandType.Whisper;
            }
        }

        public IUser User { get; set; }

        public WhisperCommand(IUser user)
        {
            User = user;
        }

        public async Task Execute(string input)
        {
            // Group 0: /w
            // Group 1: Name
            // Group 2: Message
            var regex = new Regex("/w ([^\\s]+) (.*)");
            var matches = regex.Match(input);
            var room = User.CurrentRoom as ChatRoom;
            var message = new ChatMessage(User, room, input);

            await room.Message(message);
        }
    }
}
