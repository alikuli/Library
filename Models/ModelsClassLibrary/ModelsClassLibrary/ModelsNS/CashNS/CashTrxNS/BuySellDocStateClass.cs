using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnumLibrary.EnumNS;

namespace ModelsClassLibrary.ModelsNS.CashNS.CashTrxNS
{
    public class BuySellDocStateClass
    {
        BuySellDocStateENUM BuySellDocStateEnum { get; set; }
        BuySellDocumentTypeENUM BuySellDocumentTypeEnum { get; set; }
        long DocumentNumber { get; set; }
        public string GetDescription(string ownerName, string customerName)
        {
            string desc = "Error";
            switch (BuySellDocumentTypeEnum)
            {
                case BuySellDocumentTypeENUM.Sale:
                    switch (BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.RequestConfirmed:
                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                        case BuySellDocStateENUM.ReadyForPickup:
                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                        case BuySellDocStateENUM.CourierComingToPickUp:
                        case BuySellDocStateENUM.PickedUp:
                        case BuySellDocStateENUM.Enroute:
                        case BuySellDocStateENUM.Problem:
                            desc = string.Format("Sale Order #{2} from {0} to {1}", ownerName, customerName);

                            break;
                        case BuySellDocStateENUM.Delivered:
                        case BuySellDocStateENUM.Rejected:
                        case BuySellDocStateENUM.Unknown:
                        case BuySellDocStateENUM.InProccess:
                        case BuySellDocStateENUM.BackOrdered:
                        case BuySellDocStateENUM.All:
                        case BuySellDocStateENUM.RequestUnconfirmed:
                        default:
                            break;
                    }
                    break;
                case BuySellDocumentTypeENUM.Purchase:
                    switch (BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.RequestConfirmed:
                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                        case BuySellDocStateENUM.ReadyForPickup:
                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                        case BuySellDocStateENUM.CourierComingToPickUp:
                        case BuySellDocStateENUM.PickedUp:
                        case BuySellDocStateENUM.Enroute:
                        case BuySellDocStateENUM.Problem:
                            desc = string.Format("Purchase Order #{2} from {0} to {1}", customerName, ownerName);
                            break;
                        case BuySellDocStateENUM.Delivered:
                        case BuySellDocStateENUM.Rejected:
                        case BuySellDocStateENUM.Unknown:
                        case BuySellDocStateENUM.InProccess:
                        case BuySellDocStateENUM.BackOrdered:
                        case BuySellDocStateENUM.All:
                        case BuySellDocStateENUM.RequestUnconfirmed:
                        default:
                            break;
                    }
                    break;
                case BuySellDocumentTypeENUM.Delivery:
                    switch (BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.RequestConfirmed:
                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                        case BuySellDocStateENUM.ReadyForPickup:
                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                        case BuySellDocStateENUM.CourierComingToPickUp:
                        case BuySellDocStateENUM.PickedUp:
                        case BuySellDocStateENUM.Enroute:
                        case BuySellDocStateENUM.Problem:
                        case BuySellDocStateENUM.Delivered:
                        case BuySellDocStateENUM.Rejected:
                        case BuySellDocStateENUM.Unknown:
                        case BuySellDocStateENUM.InProccess:
                        case BuySellDocStateENUM.BackOrdered:
                        case BuySellDocStateENUM.All:
                        case BuySellDocStateENUM.RequestUnconfirmed:
                        default:
                            break;
                    }
                    break;
                case BuySellDocumentTypeENUM.Unknown:
                default:
                    break;
            }
            return desc;
        }
    }
}
