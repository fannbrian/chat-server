using System.Threading.Tasks;

namespace chat_server
{
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
