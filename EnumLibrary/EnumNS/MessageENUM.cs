
namespace EnumLibrary.EnumNS
{
    /// <summary>
    /// This controls the payments of messages.
    /// If message is
    ///     Paid: Sender has paid for the message
    ///     Free: Send is sending a free message
    ///     Reply:
    ///         Paid: Receiver has replied to a paid message
    ///         Free: User has replied to a free message.
    /// </summary>
    public enum MessageENUM
    {
        Unknown, //Message is unknow
        Paid,    //This is a paid message
        Free,    //This is a free message
        Reply,   //This is a reply to a message
    }
}
