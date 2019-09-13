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

            message.Name = message.Id;
            if (message.BuySellDocId.IsNullOrWhiteSpace())
                message.BuySellDocId = null;


            if (message.MenuPath1Id.IsNullOrWhiteSpace())
                message.MenuPath1Id = null;


            if (message.MenuPath2Id.IsNullOrWhiteSpace())
                message.MenuPath2Id = null;


            if (message.MenuPath3Id.IsNullOrWhiteSpace())
                message.MenuPath3Id = null;




            if (message.ProductChildId.IsNullOrWhiteSpace())
                message.ProductChildId = null;


            if (message.ProductId.IsNullOrWhiteSpace())
                message.ProductId = null;




        }
    }
}
