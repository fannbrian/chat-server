using System.Threading.Tasks;

namespace chat_server
{
    /// <summary>
    /// Handles behavior logic for a user leaving their current chatroom.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/25/18
    /// </summary>
    public class LeaveCommand : ICommand
    {
        public CommandType Type
        {
            get
            {
                return CommandType.Leave;
            }
        }

        public IUser User { get; set; }

        public LeaveCommand(IUser user)
        {
            User = user;
        }

        public async Task Execute(string input)
        {
            var room = User.CurrentRoom;
            
            // Notify room that user has left.
            await room.Write($"{User.Name} has left {room.Name}.");
            room.Users.Remove(User);

            // Remove room if not persistent and no users remaining.
            if (!room.IsPersistent)
            {
                if (room.Users.Count == 0)
                {
                    User.CurrentServer.Rooms.Remove(room.Name);
                }
            }

            User.CurrentRoom = null;
        }
    }
}
