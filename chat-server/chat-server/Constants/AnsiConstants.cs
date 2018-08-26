namespace chat_server
{
    /// <summary>
    /// Constants for ANSI color codes.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/25/18
    /// </summary>
    public class AnsiColor
    {
        public const string BOLD = "\u001b[1m";
        public const string BLACK = "\u001b[30m";
        public const string RED = "\u001b[31m";
        public const string GREEN = "\u001b[32m";
        public const string YELLOW = "\u001b[33m";
        public const string BLUE = "\u001b[34m";
        public const string MAGENTA = "\u001b[35m";
        public const string CYAN = "\u001b[36m";
        public const string WHITE = "\u001b[37m";
        public const string RESET = "\u001b[0m";
    }
}
