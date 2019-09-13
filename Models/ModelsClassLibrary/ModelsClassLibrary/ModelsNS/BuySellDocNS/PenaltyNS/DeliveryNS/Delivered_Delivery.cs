
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
namespace ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS.DeliverymanNS
{
 /// <summary>
 /// This is if the deliveryman cancels
 /// </summary>
    public class Delivered_Delivery : PenaltyClassAbstract
    {
        public Delivered_Delivery(BuySellDoc buySellDoc)
            : base(buySellDoc)
        {

        }

        public override decimal Percent 
        { 
            get 
            {
                return PenaltyClassAbstract.GetPenaltyPercentageForDeliverymanQuitting();
            } 
        }
        public override decimal GetAmountToBasePenaltyOn()
        {

            if (BuySellDoc.Freight_Accepted == 0)
                return 0;


            return BuySellDoc.Freight_Accepted;
        }

        public override WhoPaysWhoENUM WhoPaysWhoEnum
        {
            get
            {
                return WhoPaysWhoENUM.DeliverymanPaysOwner;
            }
        }

        public override string Text
        {
            get
            {
                if (Percent == 0)
                    return "Not Set";
                string str = string.Format("You are quitting at the last moment. Not good. Penalty will be {0:N2}% of Rs{1:N2} = Rs{2:N2}. The order will turn into a problem. Insurance and freight guarantee will be frozen.",
                    Percent,
                    GetAmountToBasePenaltyOn(),
                    PenaltyAmount());
                return str;
            }
        }

    }
}
