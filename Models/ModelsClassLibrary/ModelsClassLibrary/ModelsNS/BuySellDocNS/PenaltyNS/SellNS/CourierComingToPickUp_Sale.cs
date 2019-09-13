
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
namespace ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS.DeliverymanNS
{
    /// <summary>
    /// This is if the deliveryman cancels
    /// </summary>
    public class CourierComingToPickUp_Sale : PenaltyClassAbstract
    {
        public CourierComingToPickUp_Sale(BuySellDoc buySellDoc)
            : base(buySellDoc)
        {

        }

        public override decimal Percent
        {
            get
            {
                DateParameter dp = new DateParameter();
                if (dp.DatesAreEqual(DateTime.Now, BuySellDoc.FreightOfferTrxAccepted.PickupDate))
                {
                    //Deliveryman has canceled within pickup time.

                    return PenaltyClassAbstract.GetPenaltyPercentageForCancelingDeliveryman();
                }
                else
                {
                    //seller has canceled when his pickup is late/or early
                    if (dp.Date1BeforeDate2(DateTime.Now, BuySellDoc.FreightOfferTrxAccepted.PickupDate))
                    {
                        //cancelled early... do nothing
                        return 0;
                    }
                    else
                    {
                        //deliveryman is late.

                        return PenaltyClassAbstract.GetPenaltyPercentageForCancelingDeliveryman();

                    }

                }

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
                DateParameter dp = new DateParameter();
                if (dp.DatesAreEqual(BuySellDoc.FreightOfferTrxAccepted.PickupDate, DateTime.Now))
                {
                    //Deliveryman has canceled within pickup time.
                    return WhoPaysWhoENUM.OwnerPaysDeliveryMan; ;
                }
                else
                {
                    //seller has canceled when his pickup is late/or early
                    if (dp.Date1BeforeDate2(DateTime.Now, BuySellDoc.FreightOfferTrxAccepted.PickupDate))
                    {
                        //cancelled early... do nothing
                        return WhoPaysWhoENUM.Unknown;
                    }
                    else
                    {
                        //deliveryman is late.
                        return WhoPaysWhoENUM.DeliverymanPaysOwner;

                    }

                }
            }
        }


        public override string Text
        {
            get
            {


                DateParameter dp = new DateParameter();
                if (dp.DatesAreEqual(BuySellDoc.FreightOfferTrxAccepted.PickupDate, DateTime.Now))
                {
                    //Deliveryman has canceled within pickup time.
                    string str = string.Format("Deliveryman is within his time. If you cancel now YOU will be charged {0:N2}% of Rs{1:N2} = Rs{2:N2}",
                        Percent,
                        GetAmountToBasePenaltyOn(),
                        PenaltyAmount());
                    return str;
                }
                else
                {
                    //seller has canceled when his pickup is late/or early
                    if (dp.Date1BeforeDate2(DateTime.Now, BuySellDoc.FreightOfferTrxAccepted.PickupDate))
                    {
                        return "You have canceled early. No charges.";
                    }
                    else
                    {
                        //deliveryman is late.
                        string str = string.Format("Deliveryman is late. Deliveryman will be charged {0:N2}% of Rs{1:N2} = Rs{2:N2}",
                            Percent,
                            GetAmountToBasePenaltyOn(),
                            PenaltyAmount());
                        return str;

                    }

                }
            }
        }

    }
}
