using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.MessageNS;
using ModelsClassLibrary.ModelsNS.MessagesNS;
using ModelsClassLibrary.ModelsNS.MessagesToPeopleListNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;

namespace UowLibrary.PlayersNS.MessageToPeopleListNS
{
    public partial class MessageToPeopleListBiz
    {
        public override void Fix(ModelsClassLibrary.ModelsNS.SharedNS.ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            MessageToPeopleList messageToPeopleList = parm.Entity as MessageToPeopleList;
            messageToPeopleList.IsNullThrowException("Unable to unbox messageToPeopleList");


            //fix Name
            messageToPeopleList.Name = messageToPeopleList.PersonId + messageToPeopleList.MessageId;


        }
    }
}
