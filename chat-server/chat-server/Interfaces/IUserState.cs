using System.Threading.Tasks;

namespace chat_server  
{
    /// <summary>
    /// State object containing specific behavior for a user.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/24/18
    /// </summary>
    public interface IUserState
    {
        Task Handle();
        Task OnEnter();
        Task OnExit();
    }
}
