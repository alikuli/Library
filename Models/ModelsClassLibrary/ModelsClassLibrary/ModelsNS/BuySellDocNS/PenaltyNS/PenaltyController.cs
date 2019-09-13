using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS.DeliverymanNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS.PurchaseNS;
using ModelsClassLibrary.ModelsNS.CashNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;

namespace ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS
{
    public class PenaltyController
    {
        public static IPenaltyClass GetPenalty(BuySellDoc buySellDoc, out PersonPayingPenalty personPayingPenalty)
        {
            personPayingPenalty = new PersonPayingPenalty(buySellDoc);
            switch (buySellDoc.BuySellDocumentTypeEnum)
            {
                case BuySellDocumentTypeENUM.Unknown:
                    break;

                case BuySellDocumentTypeENUM.Sale:
                    personPayingPenalty.IsOwner = true;
                    switch (buySellDoc.BuySellDocStateEnum)
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
                            return new CourierComingToPickUp_Sale(buySellDoc);

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
                        personPayingPenalty.IsCustomer = true;
                        switch (buySellDoc.BuySellDocStateEnum)
                        {
                            case BuySellDocStateENUM.RequestUnconfirmed:
                                break;
                            case BuySellDocStateENUM.RequestConfirmed:
                                break;
                            case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                                
                                if (buySellDoc.IsPickupLate)
                                    return null;

                                return new CourierAcceptedByBuyerAndSeller_Purchase(buySellDoc);

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
                                break;
                            case BuySellDocStateENUM.Rejected:
                                break;
                            case BuySellDocStateENUM.Problem:
                                break;
                            default:
                                break;
                        }
                    }
                    break;

                case BuySellDocumentTypeENUM.Delivery:
                    personPayingPenalty.IsDeliveryman = true;
                        switch (buySellDoc.BuySellDocStateEnum)
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
                                return new CourierComingToPickUp_Delivery(buySellDoc);

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
                    personPayingPenalty.IsSalesman = true;
                    break;

                default:
                    break;
            }

            return null;
        }
    }

}
