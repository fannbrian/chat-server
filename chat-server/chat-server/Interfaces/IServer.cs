using System.Collections.Generic;

namespace chat_server
{
    /// <summary>
    /// A server that contains rooms in it.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/22/18
    /// </summary>
    public interface IServer
    {
        List<IUser> Users { get; }
        Dictionary<string, IRoom> Rooms { get; }
    }
}
