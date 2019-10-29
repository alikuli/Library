using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS.DeliverymanNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS.PurchaseNS;
using ModelsClassLibrary.ModelsNS.CashNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;

namespace ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS
{
    public class PenaltyController
    {
        public static IPenaltyClass GetPenalty(BuySellDoc bsd, out PersonPayingPenalty ppp)
        {
            ppp = new PersonPayingPenalty(bsd);
            switch (bsd.BuySellDocumentTypeEnum)
            {
                case BuySellDocumentTypeENUM.Unknown:
                    break;

                case BuySellDocumentTypeENUM.Sale:
                    ppp.IsOwner = true;
                    switch (bsd.BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.RequestUnconfirmed:
                            if (bsd.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.SeeAddress)
                            {
                                return new SeeAddress_Sale(bsd);
                            }
                            break;
                        case BuySellDocStateENUM.RequestConfirmed:
                            if (bsd.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.SeeAddress)
                            {
                                return new SeeAddress_Sale(bsd);
                            }
                            if (bsd.OptedOutOfSystem.IsSelected)
                            {

                            }
                            break;
                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                            break;

                        case BuySellDocStateENUM.ReadyForPickup:
                            break;

                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                            break;
                        case BuySellDocStateENUM.CourierComingToPickUp:
                            return new CourierComingToPickUp_Sale(bsd);

                        case BuySellDocStateENUM.PickedUp:
                            break;
                        case BuySellDocStateENUM.Enroute:
                            break;
                        case BuySellDocStateENUM.Delivered:
                            break;
                        case BuySellDocStateENUM.Rejected:
                            break;
                        case BuySellDocStateENUM.Problem:
                            break;
                        default:
                            break;
                    }
                    break;

                case BuySellDocumentTypeENUM.Purchase:
                    {
                        ppp.IsCustomer = true;
                        switch (bsd.BuySellDocStateEnum)
                        {
                            case BuySellDocStateENUM.RequestUnconfirmed:
                                if (bsd.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.OptOutOfSystem)
                                {

                                    return new OptOutOfSystem_Purchase(bsd);
                                }


                                break;
                            case BuySellDocStateENUM.RequestConfirmed:
                                break;
                            case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:


                                return new CourierAcceptedByBuyerAndSeller_Purchase(bsd);

                            case BuySellDocStateENUM.ReadyForPickup:
                                break;
                            case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:

                                break;
                            case BuySellDocStateENUM.CourierComingToPickUp:
                                break;
                            case BuySellDocStateENUM.PickedUp:
                                break;
                            case BuySellDocStateENUM.Enroute:
                                break;
                            case BuySellDocStateENUM.Delivered:
                                return new Delivered_Purchase(bsd);

                            case BuySellDocStateENUM.Rejected:
                                break;
                            case BuySellDocStateENUM.Problem:
                                break;
                            //case BuySellDocStateENUM.OptedOutOfSystem:
                            //    //reset the opt_out
                            //    bsd.OptedOutOfSystem = new BoolDateAndByComplex();
                            //    return new Opt
                            default:
                                break;
                        }
                    }
                    break;

                case BuySellDocumentTypeENUM.Delivery:
                    ppp.IsDeliveryman = true;
                    switch (bsd.BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.RequestUnconfirmed:
                            break;
                        case BuySellDocStateENUM.RequestConfirmed:
                            break;
                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                            break;

                        case BuySellDocStateENUM.ReadyForPickup:
                            break;

                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                            break;
                        case BuySellDocStateENUM.CourierComingToPickUp:
                            return new CourierComingToPickUp_Delivery(bsd);

                        case BuySellDocStateENUM.PickedUp:
                            break;
                        case BuySellDocStateENUM.Enroute:
                            break;
                        case BuySellDocStateENUM.Delivered:
                            break;
                        case BuySellDocStateENUM.Rejected:
                            break;
                        case BuySellDocStateENUM.Problem:
                            break;
                        default:
                            break;
                    }
                    break;

                case BuySellDocumentTypeENUM.Salesman:
                    ppp.IsSalesman = true;
                    break;

                default:
                    break;
            }

            return null;
        }
    }

}
