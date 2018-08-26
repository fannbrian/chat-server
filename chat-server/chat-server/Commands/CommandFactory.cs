using System.Collections.Generic;

namespace chat_server
{
    /// <summary>
    /// Factory for instantiating commands from user input.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/25/18
    /// </summary>
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

        // Mapping between a string command to a command enum.
        private static readonly IDictionary<string, CommandType> PhraseToCommandDictionary =
            PhraseToCommandDictionary = new Dictionary<string, CommandType>()
            {
                { CommandPhrase.START_CHARACTER + CommandPhrase.WHISPER, CommandType.Whisper },
                { CommandPhrase.START_CHARACTER + CommandPhrase.HELP, CommandType.Help },
                { CommandPhrase.START_CHARACTER + CommandPhrase.JOIN, CommandType.Join },
                { CommandPhrase.START_CHARACTER + CommandPhrase.CREATE_ROOM, CommandType.CreateRoom },
                { CommandPhrase.START_CHARACTER + CommandPhrase.SET_NAME, CommandType.SetName },
                { CommandPhrase.START_CHARACTER + CommandPhrase.ROOMS, CommandType.Rooms },
                { CommandPhrase.START_CHARACTER + CommandPhrase.USERS, CommandType.Users },
                { CommandPhrase.START_CHARACTER + CommandPhrase.LEAVE, CommandType.Leave },
                { CommandPhrase.START_CHARACTER + CommandPhrase.QUIT, CommandType.Quit }
            };


        // List of commands that require additional parameters.
        private static readonly List<string> ParameteredCommands =
            ParameteredCommands = new List<string>()
            {
                CommandPhrase.START_CHARACTER + CommandPhrase.WHISPER,
                CommandPhrase.START_CHARACTER + CommandPhrase.JOIN,
                CommandPhrase.START_CHARACTER + CommandPhrase.CREATE_ROOM,
                CommandPhrase.START_CHARACTER + CommandPhrase.SET_NAME
            };

        public ICommand Instantiate(IUser user, string input, CommandType[] allowedCommands)
        {
            var commandList = new List<CommandType>(allowedCommands);

            // Default to a chat command.
            var resultType = CommandType.Chat;

            // If input starts with a '/', default to invalid command.
            if (input.Length > 0)
            {
                if (input[0] == CommandPhrase.START_CHARACTER)
                {
                    resultType = CommandType.Invalid;
                }
            }
            
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

            switch(resultType)
            {
                case CommandType.Whisper:
                    return new WhisperCommand(user);

                case CommandType.Help:
                    return new HelpCommand(user);

                case CommandType.Join:
                    return new JoinCommand(user);

                case CommandType.CreateRoom:
                    return new CreateRoomCommand(user);

                case CommandType.SetName:
                    return new SetNameCommand(user);

                case CommandType.Rooms:
                    return new RoomsCommand(user);

                case CommandType.Users:
                    return new UsersCommand(user);

                case CommandType.Leave:
                    return new LeaveCommand(user);

                case CommandType.Quit:
                    return new QuitCommand(user);

                case CommandType.Chat:
                    return new ChatCommand(user);

                default:
                    return new InvalidCommand(user);
            }
        }
    }
}
