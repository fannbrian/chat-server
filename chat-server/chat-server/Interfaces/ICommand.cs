using System.Threading.Tasks;

namespace chat_server
{
    /// <summary>
    /// A command object containing an executable behavior.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/22/18
    /// </summary>
    public interface ICommand
    {
        CommandType Type { get; }
        IUser User { get; set; }
        Task Execute(string input);
    }
}
