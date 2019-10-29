
using AliKuli.Extentions;
using System.Configuration;
namespace ModelsClassLibrary.ModelsNS.ServiceRequestNS.ServiceRequest_StatePatternNS
{
    public abstract class ServiceRequestDetail_Aabstract : IServiceRequestDetail
    {
        public abstract decimal Amount { get; }
        public abstract string Text { get; }


        public static decimal GetPhoneNumberFee()
        {
            string amountStr = ConfigurationManager.AppSettings["getphonenumber.Amount"];
            decimal amount = amountStr.ToDecimal();

            return amount;
        }
        public static decimal BecomeSalesmanFee()
        {
            string amountStr = ConfigurationManager.AppSettings["BecomeSalesman.Amount"];
            decimal amount = amountStr.ToDecimal();

            return amount;
        }

        public static decimal BecomeSellerFee()
        {
            string amountStr = ConfigurationManager.AppSettings["BecomeSeller.Amount"];
            decimal amount = amountStr.ToDecimal();

            return amount;
        }

        public static decimal BecomeMailerFee()
        {
            string amountStr = ConfigurationManager.AppSettings["BecomeMailer.Amount"];
            decimal amount = amountStr.ToDecimal();

            return amount;
        }


    }
}
