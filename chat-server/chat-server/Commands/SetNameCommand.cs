using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace chat_server
{
    /// <summary>
    /// Handles behavior logic for setting a user's name.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/25/18
    /// </summary>
    public class SetNameCommand : ICommand
    {
        public CommandType Type
        {
            get
            {
                return CommandType.SetName;
            }
        }

        public IUser User { get; set; }

        public SetNameCommand(IUser user)
        {
            User = user;
        }

        public async Task Execute(string input)
        {
            // Group 0: /setname
            // Group 1: Name
            var regex = new Regex($"{CommandPhrase.START_CHARACTER}{CommandPhrase.SET_NAME} (.*)");
            var match = regex.Match(input);
            var name = match.Groups[1].Value;

            name = name.Replace(" ", "");

            // Handle empty name edge case
            if (name.Length <= 1)
            {
                await User.Write($"You must enter a name.");
                return;
            }

            // Check for duplicate name
            foreach (var user in User.CurrentServer.Users)
            {
                if (user.Name == name)
                {
                    await User.Write($"{name} is taken. Try again!");
                    return;
                }
            }

            // Notify either current room or user of name change
            if (User.CurrentRoom != null)
            {
                await User.CurrentRoom.Write($"{User.Name} has set their name to {name}");
            }
            else
            {
                await User.Write($"You has set your name to {name}");
            }

            User.Name = name;
        }
    }
}
