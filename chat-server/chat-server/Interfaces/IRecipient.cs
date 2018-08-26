using System.Threading.Tasks;

namespace chat_server
{
    /// <summary>
    /// An object that can have messages written to it.
    /// </summary>
    public interface IRecipient
    {
        Task Message(IMessage message);
        Task Write(string message);
    }
}
