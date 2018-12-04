using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace UowLibrary.PlayersNS.CashierNS
{
    public partial class CashierBiz
    {


        public override void Fix(ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            Cashier cashier = parm.Entity as Cashier;
            cashier.IsNullThrowException("Unable to unbox cashier");

            //we need to add this because the DefaultBillAddressId is returning as a blank
            //and then thesystme thinks that there is a record with a blank Id
            //and the system does not save.
            //if (cashier.DefaultBillAddressId.IsNullOrWhiteSpace())
            //    cashier.DefaultBillAddressId = null;

            if (cashier.CashierCategoryId.IsNullOrWhiteSpace())
                cashier.CashierCategoryId = null;
        }



    }
}
