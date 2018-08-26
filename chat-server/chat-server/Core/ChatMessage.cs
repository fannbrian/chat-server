using System;

namespace chat_server
{
    /// <summary>
    /// A message containing a string sent between a sender and recipient.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/22/18
    /// </summary>
    public class ChatMessage : IMessage
    {
        public IUser Sender { get; set; }
        public IRecipient Recipient { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Content { get; set; }

        public ChatMessage(IUser sender, IRecipient recipient, string content)
        {
            Sender = sender;
            Recipient = recipient;
            Content = content;

            TimeStamp = DateTime.Now;
        }

        public override string ToString()
        {
            var timeStamp = TimeStamp.ToString(MessageConstants.TIMESTAMP_FORMAT);
            if (Recipient is IUser)
            {
                return $"[{timeStamp}] {AnsiColor.RED}{Sender.Name} whispered: {AnsiColor.RESET}{Content}";
            }
            else
            {
                return $"[{timeStamp}] {AnsiColor.CYAN}{Sender.Name}: {AnsiColor.RESET}{Content}";
            }
        }
    }
}
