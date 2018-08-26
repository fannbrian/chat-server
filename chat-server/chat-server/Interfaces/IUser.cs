using System.Threading.Tasks;

namespace chat_server
{
    /// <summary>
    /// Object that is inside a room and server.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/22/18
    /// </summary>
    public interface IUser : IRecipient
    {
        string Name { get; set; }
        IRoom CurrentRoom { get; set; }
        IServer CurrentServer { get; }
    }
}
