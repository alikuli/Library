
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
  
        public override decimal Percent { get { return PenaltyClassAbstract.GetPenaltyPercentageForPurchaserQuitting(); } }
        public override decimal GetAmountToBasePenaltyOn()
        {

            return BuySellDoc.TotalInvoice;
        }

        public override WhoPaysWhoENUM WhoPaysWhoEnum
        {
            get
            {
                return WhoPaysWhoENUM.Unknown;
            }
        }
    }
}
