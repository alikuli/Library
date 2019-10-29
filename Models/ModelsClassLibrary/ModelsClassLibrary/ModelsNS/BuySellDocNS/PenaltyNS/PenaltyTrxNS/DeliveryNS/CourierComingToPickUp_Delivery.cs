
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
namespace ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS.DeliverymanNS
{
 /// <summary>
 /// This is if the deliveryman cancels
 /// </summary>
    public class CourierComingToPickUp_Delivery : PenaltyClassAbstract
    {
        public CourierComingToPickUp_Delivery(BuySellDoc buySellDoc)
            : base(buySellDoc)
        {

        }

        public override decimal Percent 
        { 
            get 
            {
                DateParameter dp = new DateParameter();
                if (dp.Date1AfterDate2(BuySellDoc.FreightOfferTrxAccepted.PickupDate, DateTime.Now))
                {
                    //Deliveryman has canceled when his pickup is late
                    return PenaltyClassAbstract.GetPenaltyPercentageForDeliverymanCancelingLate();
                }
                else
                {
                    //Deliveryman has canceled within pickup time.
                    return PenaltyClassAbstract.GetPenaltyPercentageForDeliverymanCancelingOnTime();
                }

            } 
        }
        public override decimal GetAmountToBasePenaltyOn()
        {

            if (BuySellDoc.Freight_Accepted_Refundable == 0)
                return 0;


            return BuySellDoc.Freight_Accepted_Refundable;
        }

        public override WhoPaysWhoENUM WhoPaysWhoEnum
        {
            get
            {
                DateParameter dp = new DateParameter();
                if (dp.Date1AfterDate2(BuySellDoc.FreightOfferTrxAccepted.PickupDate, DateTime.Now))
                {
                    //Deliveryman has canceled when his pickup is late
                    return WhoPaysWhoENUM.DeliverymanPaysOwner;
                }
                else
                {
                    //Deliveryman has canceled within pickup time.
                    return WhoPaysWhoENUM.DeliverymanPaysOwner;
                }
            }
        }


    }
}
