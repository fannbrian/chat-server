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

            TimeStamp = DateTime.UtcNow;
        }

        public override string ToString()
        {
            if (Recipient is ChatUser)
            {
                return $"{Sender.Name} whispered: {Content}";
            }
            else
            {
                return $"{Sender.Name}: {Content}";
            }
        }
    }
}
