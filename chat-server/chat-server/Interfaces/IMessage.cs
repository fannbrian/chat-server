using System;
namespace chat_server
{
    /// <summary>
    /// An object representing communication between a sender and receiver.
    /// </summary>
    public interface IMessage
    {
        ISender Sender { get; set; }
        IRecipient Recipient { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
