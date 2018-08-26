using System.Threading.Tasks;

namespace chat_server
{
    /// <summary>
    /// Handles behavior logic for disconnecting from server.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/25/18
    /// </summary>
    public class QuitCommand : ICommand
    {
        public CommandType Type {
            get
            {
                return CommandType.Quit;
            }
        }

        public IUser User { get; set; }

        public QuitCommand(IUser user)
        {
            User = user;
        }

        public async Task Execute(string input)
        {
            if (!(User is ChatUser)) return;

            var chatUser = (ChatUser)User;

            if (chatUser.CurrentRoom != null)
            {
                await chatUser.CurrentRoom.Write($"{AnsiColor.BLUE}{chatUser.Name}{AnsiColor.RESET} has left {AnsiColor.BOLD}{AnsiColor.RED}{chatUser.CurrentRoom.Name}.");
                chatUser.CurrentRoom.Users.Remove(chatUser);
            }

            chatUser.CurrentServer.Users.Remove(chatUser);
            chatUser.NetworkClient.GetStream().Close();
            chatUser.NetworkClient.Close();
        }
    }
}
