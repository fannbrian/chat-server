using System.Threading.Tasks;

namespace chat_server
{
    /// <summary>
    /// Object that is able to receive messages.
    /// </summary>
    public interface IRecipient
    {
        Task<bool> QueueMessage(string message);
    }
}
