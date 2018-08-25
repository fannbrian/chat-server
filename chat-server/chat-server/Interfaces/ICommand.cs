using System.Threading.Tasks;

namespace chat_server
{
    public interface ICommand
    {
        CommandType Type { get; }
        IUser User { get; set; }
        Task Execute(string input);
    }
}
