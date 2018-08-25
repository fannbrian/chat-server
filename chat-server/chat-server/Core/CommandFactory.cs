using System.Collections.Generic;
using System;
namespace chat_server
{
    public class CommandFactory
    {
        private static CommandFactory _instance;

        public static CommandFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CommandFactory();

                return _instance;
            }
        }

        private static readonly IDictionary<string, CommandType> PhraseToCommandDictionary =
            PhraseToCommandDictionary = new Dictionary<string, CommandType>()
            {
                { CommandPhrase.START_CHARACTER + CommandPhrase.WHISPER, CommandType.Whisper },
                { CommandPhrase.START_CHARACTER + CommandPhrase.HELP, CommandType.Help },
                { CommandPhrase.START_CHARACTER + CommandPhrase.JOIN, CommandType.Join },
                { CommandPhrase.START_CHARACTER + CommandPhrase.LEAVE, CommandType.Leave },
                { CommandPhrase.START_CHARACTER + CommandPhrase.QUIT, CommandType.Quit }
            };

        private static readonly List<string> ParameteredCommands =
            ParameteredCommands = new List<string>()
            {
                CommandPhrase.START_CHARACTER + CommandPhrase.WHISPER,
                CommandPhrase.START_CHARACTER + CommandPhrase.JOIN
            };

        public ICommand Instantiate(IUser user, string input, CommandType[] allowedCommands)
        {
            var commandList = new List<CommandType>(allowedCommands);

            // Default to a chat command
            var resultType = CommandType.Chat;
            
            if (PhraseToCommandDictionary.TryGetValue(input, out var type))
            {
                resultType = type;
            }
            else
            {
                // Check input against parametered commands
                foreach(var cmd in ParameteredCommands)
                {
                    if (input.Length < cmd.Length) break;

                    if (input.Substring(0,cmd.Length) == cmd)
                    {
                        resultType = PhraseToCommandDictionary[cmd];
                    }
                }
            }

            // If the specified command is not allowed, set to invalid type.
            if (!commandList.Contains(resultType))
            {
                resultType = CommandType.Invalid;
            }

            Console.WriteLine($"Created {resultType.ToString()}");

            switch(resultType)
            {
                case CommandType.Whisper:
                    return new WhisperCommand(user);

                case CommandType.Help:
                    break;

                case CommandType.Join:
                    return new JoinCommand(user);

                case CommandType.Leave:
                    break;

                case CommandType.Quit:
                    break;

                case CommandType.Chat:
                    return new ChatCommand(user);

                default:
                    return new InvalidCommand(user);
            }

            return new InvalidCommand(user);
        }
    }
}
