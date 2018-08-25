using System.Collections.Generic;
using System.Threading.Tasks;

namespace chat_server
{
    public interface IServer
    {
        List<IUser> Users { get; }
        Dictionary<string, IRoom> Rooms { get; }
        Task AddRoom(IRoom room);
    }
}
