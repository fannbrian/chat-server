using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chat_server
{
    public class JoiningState : BaseUserState
    {
        public JoiningState(ChatUser context) : base(context) { }

        private readonly CommandType[] ValidCommands = { CommandType.Help, CommandType.Quit, CommandType.Join };

        public override async Task OnEnter()
        {
            await User.Write("Enter a room by typing in /join <chatroom>. E.g. /join General"+
                             "\nYou can check what rooms are available with /roomlist\n");
            foreach(var room in User.CurrentServer.Rooms)
            {
                Console.WriteLine($"* {room.Value.Name}");
            }
        }

        public override async Task OnExit()
        {
            await User.Write($"You have joined {User.CurrentRoom.Name}.\n");
        }

        internal override async Task ProcessInput(string input)
        {
            Console.WriteLine("TESTING");
            var command = CommandFactory.Instance.Instantiate(User, input, ValidCommands);
            await command.Execute(input);
            
            if (User.CurrentRoom != null)
            {
                await NextState(new ChattingState(User));
            }
        }
    }
}
