using System.Threading.Tasks;

namespace chat_server
{
    public interface IRecipient
    {
        Task Message(IMessage message);
        Task Write(string message);
    }
}
