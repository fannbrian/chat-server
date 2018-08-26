using System.Collections.Generic;

namespace chat_server
{
    /// <summary>
    /// A room that contains users in it.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/22/18
    /// </summary>
    public interface IRoom : IRecipient
    {
        string Name { get; }
        List<IUser> Users { get; }
        bool IsPersistent { get; }
    }
}
