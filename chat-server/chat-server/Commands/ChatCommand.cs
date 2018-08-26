using System.Threading.Tasks;

namespace chat_server
{
    /// <summary>
    /// Handles behavior logic for propogating messages to users in a chatroom.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/25/18
    /// </summary>
    public class ChatCommand : ICommand
    {
        public CommandType Type {
            get
            {
                return CommandType.Chat;
            }
        }

        public IUser User { get; set; }

        public ChatCommand(IUser user)
        {
            User = user;
        }

        public async Task Execute(string input)
        {
            var room = User.CurrentRoom;
            var message = new ChatMessage(User, room, input);

            await room.Message(message);
        }
    }
}
