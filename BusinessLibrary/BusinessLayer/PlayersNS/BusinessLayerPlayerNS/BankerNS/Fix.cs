using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace UowLibrary.PlayersNS.BankNS
{
    public partial class BankBiz
    {


        public override void Fix(ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            Bank bank = parm.Entity as Bank;
            bank.IsNullThrowException("Unable to unbox bank");

            //we need to add this because the DefaultBillAddressId is returning as a blank
            //and then thesystme thinks that there is a record with a blank Id
            //and the system does not save.
            //if (bank.DefaultBillAddressId.IsNullOrWhiteSpace())
            //    bank.DefaultBillAddressId = null;

            if (bank.BankCategoryId.IsNullOrWhiteSpace())
                bank.BankCategoryId = null;
        }



    }
}
