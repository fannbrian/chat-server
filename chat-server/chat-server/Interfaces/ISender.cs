using System.Threading.Tasks;

namespace chat_server
{
    /// <summary>
    /// Object that can send messages to a recipient.
    /// </summary>
    public interface ISender
    {
        Task<bool> SendMessage(IRecipient recipient, string message);
    }
}
