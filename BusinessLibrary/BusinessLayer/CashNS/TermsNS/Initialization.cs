using DatastoreNS;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using System.Web;

namespace UowLibrary.PaymentTermNS
{
    public partial class PaymentTermBiz : BusinessLayer<PaymentTerm>
    {

        #region InitializationData and InitializationDataAsync

        public override string[] GetDataForStringArrayFormat
        {
            get
            {
                return PaymentTermsData.DataArray();
            }
        }

        public override void Event_DoSpecialInitializationStuff(PaymentTerm x)
        {
            string paymentTermNameWithoutSpace = x.Name;


            switch (paymentTermNameWithoutSpace)
            {
                case "Net 30":
                    x.NoOfDaysCredit = 30;
                    break;

                case "Net 30 2":
                    x.NoOfDaysCredit = 30;
                    x.NoOfDaysEarlyPayment = 0;
                    x.EarlyPaymentDiscount = 0.02M;
                    break;

                case "Net 45":
                    x.NoOfDaysCredit = 45;
                    break;

            }
        }

        //public void GetData()
        //{
        //    InitializationData(PaymentTermsData.DataArray().ToArray());
        //}



        //public override async Task InitializationDataAsync()
        //{
        //    GetData();
        //    await Dal.SaveChangesAsync();
        //    ErrorsGlobal.AddMessage("All PaymentTerms added to database");
        //}

        #endregion

    }
}
