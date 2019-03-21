using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.MessageNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;

namespace UowLibrary.PlayersNS.MessageNS
{
    public partial class MessageBiz
    {
        public override void Fix(ModelsClassLibrary.ModelsNS.SharedNS.ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            Message message = parm.Entity as Message;
            message.IsNullThrowException("Unable to unbox message");



            //if (message.AddressDefaultShipFromId.IsNullOrWhiteSpace())
            //    message.AddressDefaultShipFromId = null;

            //if (message.MessageCategoryId.IsNullOrWhiteSpace())
            //    message.MessageCategoryId = null;

        }
    }
}
