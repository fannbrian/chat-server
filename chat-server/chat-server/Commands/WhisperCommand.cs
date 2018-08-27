using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace chat_server
{
    /// <summary>
    /// Handles behavior logic for private messaging between users.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/25/18
    /// </summary>
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
            var regex = new Regex($"{CommandPhrase.START_CHARACTER}{CommandPhrase.WHISPER}([^\\s]+) (.*)");
            var match = regex.Match(input);
            var room = User.CurrentRoom;
            var userExists = false;
            var targetUser = match.Groups[1].Value;

            foreach(var recipient in room.Users)
            {
                if (recipient.Name == targetUser)
                {
                    var message = new ChatMessage(User, recipient, match.Groups[2].Value);
                    userExists = true;
                    await recipient.Message(message);

                    var timeStamp = message.TimeStamp.ToString(MessageConstants.TIMESTAMP_FORMAT);

                    var response = $"[{timeStamp}] To {recipient.Name}: {message.Content}";
                    await User.Write(response);
                    break;
                }
            }

            if (!userExists)
            {
                var response = $"{targetUser} could not be found in this room.";
                await User.Write(response);
            }
        }
    }
}
