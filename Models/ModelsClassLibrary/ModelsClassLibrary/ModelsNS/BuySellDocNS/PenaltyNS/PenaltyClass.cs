
using System.Configuration;
namespace ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS
{
    using AliKuli.Extentions;
    using EnumLibrary.EnumNS;
    using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
    public abstract class PenaltyClassAbstract : IPenaltyClass
    {
        public PenaltyClassAbstract(BuySellDoc buySellDoc)
        {
            BuySellDoc = buySellDoc;
        }

        protected BuySellDoc BuySellDoc { get; set; }
        //public decimal SaleAmount { get; set; }
        public virtual decimal Percent { get { return 0; } }
        public virtual string Text
        {
            get
            {
                if (Percent == 0)
                    return "Not Set";
                string str = string.Format("Your Penalty will be {0:N2}% of Rs{1:N2} = Rs{2:N2}",
                    Percent,
                    GetAmountToBasePenaltyOn(),
                    PenaltyAmount());
                return str;
            }
        }



        public decimal PenaltyAmount()
        {
            decimal amount = GetAmountToBasePenaltyOn() * Percent / 100;
            return amount;
        }

        public virtual WhoPaysWhoENUM WhoPaysWhoEnum { get { return WhoPaysWhoENUM.Unknown; } }


        public abstract decimal GetAmountToBasePenaltyOn();



        public static decimal GetPenaltyPercentageForPurchaserQuitting()
        {
            string pctAmountString = ConfigurationManager.AppSettings["Purchase.PenaltyForQuitting.Percent"];
            decimal pctAmount = pctAmountString.ToDecimal();

            return pctAmount;
        }

        public static decimal GetPenaltyPercentageForDeliverymanForDeliveringLateOrEarly()
        {
            string pctAmountString = ConfigurationManager.AppSettings["Deliveryman.PenaltyForDeliveringEarlyOrLate.Percent"];
            decimal pctAmount = pctAmountString.ToDecimal();

            return pctAmount;
        }

        public static decimal GetPenaltyPercentageForDeliverymanQuitting()
        {
            string pctAmountString = ConfigurationManager.AppSettings["Deliveryman.PenaltyForQuitting.Percent"];
            decimal pctAmount = pctAmountString.ToDecimal();

            return pctAmount;
        }


        public static int GetNoOfDaysCashBackGuarantee()
        {
            string noOfDaysString = ConfigurationManager.AppSettings["MoneyBackGuarantee.NumberOfDays"];
            noOfDaysString.IsNullOrWhiteSpaceThrowException();
            int noOfDays = noOfDaysString.ToInt();
            return noOfDays;
        }

        public static decimal GetPenaltyPercentageForDeliverymanCancelingLate()
        {
            string pctAmountString = ConfigurationManager.AppSettings["Deliveryman.PenaltyForCanceling.Late.Percent"];
            decimal pctAmount = pctAmountString.ToDecimal();

            return pctAmount;
        }
        public static decimal GetPenaltyPercentageForDeliverymanCancelingOnTime()
        {
            string pctAmountString = ConfigurationManager.AppSettings["Deliveryman.PenaltyForCanceling.WithinTime.Percent"];
            decimal pctAmount = pctAmountString.ToDecimal();

            return pctAmount;
        }
        public static decimal GetPenaltyPercentageForCancelingDeliveryman()
        {
            string pctAmountString = ConfigurationManager.AppSettings["PenaltyForCancelingDeliveryman.Percent"];
            decimal pctAmount = pctAmountString.ToDecimal();

            return pctAmount;
        }
        //public static decimal GetPenaltyPercentageForSalesmanCanceling_Late_Deliveryman()
        //{
        //    string pctAmountString = ConfigurationManager.AppSettings["Salesman.PenaltyForCancelingDeliveryman.Late.Percent"];
        //    decimal pctAmount = pctAmountString.ToDecimal();

        //    return pctAmount;
        //}





    }
}
