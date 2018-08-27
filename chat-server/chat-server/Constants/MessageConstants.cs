namespace chat_server
{
    /// <summary>
    /// Constants for messages sent to players.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/25/18
    /// </summary>
    public class MessageConstants
    {
        public const string TIMESTAMP_FORMAT = "HH:mm";
        public const string HELP_MESSAGE =
            "/join <chatroom> - Adds you to the specified chatroom. Cannot be used while in a room.\n" +
            "/createroom <chatroom> - Creates a chatroom and adds you to it. Cannot be used while in a room.\n" +
            "/w <user> - Privately messages specified user (must be in same chatroom)\n" +
            "/rooms - Displays all rooms currently open as well as the number of users\n" +
            "/users - Displays all users currently in your chatroom. Must be in a chatroom to use.\n" +
            "/leave - Leaves the current chatroom\n" +
            "/quit - Disconnects from chat server";
    }
}
