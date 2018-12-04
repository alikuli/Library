using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PlayersNS;

namespace UowLibrary.PlayersNS.OwnerNS
{
    public partial class OwnerBiz
    {
        public override void Fix(ModelsClassLibrary.ModelsNS.SharedNS.ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            Owner owner = parm.Entity as Owner;
            owner.IsNullThrowException("Unable to unbox owner");

            //if (owner.AddressDefaultShipFromId.IsNullOrWhiteSpace())
            //    owner.AddressDefaultShipFromId = null;

            if (owner.OwnerCategoryId.IsNullOrWhiteSpace())
                owner.OwnerCategoryId = null;

        }
    }
}
