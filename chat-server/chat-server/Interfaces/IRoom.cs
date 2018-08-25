using System.Collections.Generic;

namespace chat_server
{
    public interface IRoom : IRecipient
    {
        string Name { get; }
        List<IUser> Users { get; }
    }
}
