using System.Collections.Generic;

namespace chat_server
{
    public enum CommandType
    {
        Chat,
        Whisper,
        Join,
        Help,
        Leave,
        Quit,
        Invalid
    }

    public class CommandPhrase
    {
        public const char START_CHARACTER = '/';
        public const char SPACE_CHARACTER = ' ';
        public const string WHISPER = "w ";
        public const string HELP = "help";
        public const string JOIN = "join "; 
        public const string LEAVE = "leave";
        public const string QUIT = "quit";
    }

}
