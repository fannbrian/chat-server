namespace chat_server
{
    public enum CommandType
    {
        Chat,
        Whisper,
        Join,
        CreateRoom,
        SetName,
        Help,
        Rooms,
        Users,
        Leave,
        Quit,
        Invalid
    }

    /// <summary>
    /// Constants for command strings.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/25/18
    /// </summary>
    public class CommandPhrase
    {
        public const char START_CHARACTER = '/';
        public const char SPACE_CHARACTER = ' ';
        public const string WHISPER = "w ";
        public const string HELP = "help";
        public const string ROOMS = "rooms";
        public const string USERS = "users";
        public const string JOIN = "join ";
        public const string CREATE_ROOM = "createroom ";
        public const string SET_NAME = "setname ";
        public const string LEAVE = "leave";
        public const string QUIT = "quit";
    }
}
