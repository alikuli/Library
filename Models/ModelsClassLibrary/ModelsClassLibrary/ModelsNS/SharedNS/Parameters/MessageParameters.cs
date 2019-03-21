
using System.Collections.Generic;
using UserModels;
namespace ModelsClassLibrary.ModelsNS.SharedNS.Parameters
{
    public class MessageParameters
    {
        public MessageParameters(int numberOfMessages, string kindOf, List<ParticipatingPeople> messages)
        {
            NumberOfMessages = numberOfMessages;
            KindOf = kindOf;
            Messages = messages;
        }
        public int NumberOfMessages { get; set; }
        
        /// <summary>
        /// This is the kind of message. Product, POroductChile, Menupath1, Menupath2, Menupath3
        /// </summary>
        public string KindOf { get; set; }
        public List<ParticipatingPeople> Messages { get; set; }
    }
}
