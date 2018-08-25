using System;
namespace chat_server
{
    /// <summary>
    /// An object representing communication between a sender and receiver.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 8/22/18
    /// </summary>
    public interface IMessage
    {
        IUser Sender { get; }
        IRecipient Recipient { get; }
        DateTime TimeStamp { get; }
    }
}
