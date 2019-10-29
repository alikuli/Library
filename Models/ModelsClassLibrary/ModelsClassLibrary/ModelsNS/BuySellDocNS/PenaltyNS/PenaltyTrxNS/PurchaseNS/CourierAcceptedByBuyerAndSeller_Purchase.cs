
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
namespace ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS.PurchaseNS
{
    public class CourierAcceptedByBuyerAndSeller_Purchase : PenaltyClassAbstract
    {
        public CourierAcceptedByBuyerAndSeller_Purchase(BuySellDoc buySellDoc)
            : base(buySellDoc)
        {

        }
  
        public override decimal Percent 
        { 
            get 
            {
                if (BuySellDoc.IsDeliveryLate)
                    return 0;

                return PenaltyClassAbstract.GetPenaltyPercentageForPurchaserQuitting(); 
            } 
        }
        public override decimal GetAmountToBasePenaltyOn()
        {

            if (BuySellDoc.IsDeliveryLate)
                return 0;

            return BuySellDoc.TotalInvoice_Refundable;
        }

        public override WhoPaysWhoENUM WhoPaysWhoEnum
        {
            get
            {
                if (BuySellDoc.IsDeliveryLate)
                    return WhoPaysWhoENUM.Unknown;
                
                return WhoPaysWhoENUM.CustomerPaysOwner;
            }
        }

        public override string Text
        {
            get
            {
                if (BuySellDoc.IsDeliveryLate)
                    return string.Format("The seller '{0}' was supposed to deliver on {1}. Therefore, {0} is late. You will not be penalized.", 
                        BuySellDoc.Owner.FullName(), 
                        BuySellDoc.ExpectedDeliveryDate.ToShortDateString());

                return base.Text;
            }
        }
    }
}
