using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace UowLibrary.PlayersNS.CustomerNS
{
    public partial class CustomerBiz
    {


        public override void Fix(ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            Customer customer = parm.Entity as Customer;
            customer.IsNullThrowException("Unable to unbox customer");

            //we need to add this because the DefaultBillAddressId is returning as a blank
            //and then thesystme thinks that there is a record with a blank Id
            //and the system does not save.

            //if (customer.DefaultInformToAddressId.IsNullOrWhiteSpace())
            //    customer.DefaultInformToAddressId = null;



            if (customer.CustomerCategoryId.IsNullOrWhiteSpace())
                customer.CustomerCategoryId = null;
            if (customer.DefaultInformToAddressId.IsNullOrWhiteSpace())
                customer.DefaultInformToAddressId = null;

        }



    }
}
