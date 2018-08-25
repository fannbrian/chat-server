using System.Threading.Tasks;

namespace chat_server  
{
    /// <summary>
    /// TODO Add states for being named, pre-chatroom, and in-chatroom (to handle the different commands a user can use)
    /// </summary>
    public interface IUserState
    {
        Task Handle(ChatUser User);
        Task OnEnter();
        Task OnExit();
    }
}
