
using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewStateController
    {
        public BuySellDocViewStateController(BuySellDocStateENUM buySellDocStateEnum, BuySellDocumentTypeENUM buySellDocumentTypeEnum, Customer customer, Owner owner, decimal customerBalanceRefundable, decimal customerBalanceNonRefundable)
        {
            BuySellDocStateEnum = buySellDocStateEnum;
            BuySellDocumentTypeEnum = buySellDocumentTypeEnum;
            Customer = customer;
            Owner = owner;
            CustomerBalanceNonRefundable = customerBalanceNonRefundable;
            CustomerBalanceRefundable = customerBalanceRefundable;
        }

        decimal CustomerBalanceNonRefundable { get; set; }
        decimal CustomerBalanceRefundable { get; set; }
        Customer Customer { get; set; }
        Owner Owner { get; set; }

        BuySellDocStateENUM BuySellDocStateEnum { get; set; }
        BuySellDocumentTypeENUM BuySellDocumentTypeEnum { get; set; }




        public IBuySellDocViewState GetBuySellDocStateController()
        {
            string customerPersonId = "";
            string ownerPersonId = "";

            if (!Owner.IsNull())
                ownerPersonId = Owner.PersonId;

            if (!Customer.IsNull())
                customerPersonId = Customer.PersonId;

            return GetBuySellDocStateController(BuySellDocStateEnum, BuySellDocumentTypeEnum, customerPersonId, ownerPersonId);
        }
        public IBuySellDocViewState GetBuySellDocStateController(BuySellDocStateENUM buySellDocStateEnum, BuySellDocumentTypeENUM buySellDocumentTypeEnum, string customerPersonId, string sellerPersonId)
        {
            //default
            IBuySellDocViewState viewState = null;


            switch (buySellDocumentTypeEnum)
            {
                case BuySellDocumentTypeENUM.Unknown:  // ******** UNKNOWN
                    break;




                case BuySellDocumentTypeENUM.Sale:     // ******** SALE
                    switch (buySellDocStateEnum)
                    {

                        case BuySellDocStateENUM.RequestUnconfirmed:
                            return new BuySellDocViewState_RequestUnconfirmed_Sale(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.RequestConfirmed:
                            return new BuySellDocViewState_RequestConfirmed_Sale(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                            return new BuySellDocViewState_BeingPreparedForShipmentBySeller_Sale(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.ReadyForPickup:
                            return new BuySellDocViewState_ReadyForPickup_Sale(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                            return new BuySellDocViewState_CourierAcceptedByBuyerAndSeller_Sale(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.CourierComingToPickUp:
                            return new BuySellDocViewState_CourierComingToPickUp_Sale(customerPersonId, sellerPersonId);

                        //case BuySellDocStateENUM.PickedUp:
                        //    return new BuySellDocViewState_PickedUp_Sale(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.Enroute:
                            return new BuySellDocViewState_Enroute_Sale(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.Delivered:
                            return new BuySellDocViewState_Delivered_Sale(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.Rejected:
                            return new BuySellDocViewState_Rejected_Sale(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.InProccess:

                        case BuySellDocStateENUM.All:
                        case BuySellDocStateENUM.Problem:

                        case BuySellDocStateENUM.BackOrdered:
                        case BuySellDocStateENUM.Unknown:
                        default:
                            return new BuySellDocViewState_Problem_Sale(customerPersonId, sellerPersonId);
                    }




                case BuySellDocumentTypeENUM.Purchase:     // ******** PURCHASE
                    switch (buySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.RequestUnconfirmed:
                            return new BuySellDocViewState_RequestUnconfirmed_Purchase(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.RequestConfirmed:
                            return new BuySellDocViewState_RequestConfirmed_Purchase(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                            return new BuySellDocViewState_BeingPreparedForShipmentBySeller_Purchase(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.ReadyForPickup:
                            return new BuySellDocViewState_ReadyForPickup_Purchase(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                            return new BuySellDocViewState_CourierAcceptedByBuyerAndSeller_Purchase(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.CourierComingToPickUp:
                            return new BuySellDocViewState_CourierComingToPickUp_Purchase(customerPersonId, sellerPersonId);

                        //case BuySellDocStateENUM.PickedUp:
                        //    return new BuySellDocViewState_PickedUp_Purchase(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.Enroute:
                            return new BuySellDocViewState_Enroute_Purchase(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.Delivered:
                            return new BuySellDocViewState_Delivered_Purchase(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.Rejected:
                            return new BuySellDocViewState_Rejected_Purchase(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.InProccess:

                        case BuySellDocStateENUM.All:
                        case BuySellDocStateENUM.Problem:

                        case BuySellDocStateENUM.BackOrdered:
                        case BuySellDocStateENUM.Unknown:
                        default:
                            return new BuySellDocViewState_Problem_Purchase(customerPersonId, sellerPersonId);
                    }


                case BuySellDocumentTypeENUM.Salesman:     // ******** PURCHASE
                    switch (buySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.RequestUnconfirmed:
                            return new BuySellDocViewState_RequestUnconfirmed_Salesman(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.RequestConfirmed:
                            return new BuySellDocViewState_RequestConfirmed_Salesman(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                            return new BuySellDocViewState_BeingPreparedForShipmentBySeller_Salesman(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.ReadyForPickup:
                            return new BuySellDocViewState_ReadyForPickup_Salesman(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                            return new BuySellDocViewState_CourierAcceptedByBuyerAndSeller_Salesman(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.CourierComingToPickUp:
                            return new BuySellDocViewState_CourierComingToPickUp_Salesman(customerPersonId, sellerPersonId);

                        //case BuySellDocStateENUM.PickedUp:
                        //    return new BuySellDocViewState_PickedUp_Salesman(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.Enroute:
                            return new BuySellDocViewState_Enroute_Salesman(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.Delivered:
                            return new BuySellDocViewState_Delivered_Salesman(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.Rejected:
                            return new BuySellDocViewState_Rejected_Salesman(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.InProccess:

                        case BuySellDocStateENUM.All:
                        case BuySellDocStateENUM.Problem:

                        case BuySellDocStateENUM.BackOrdered:
                        case BuySellDocStateENUM.Unknown:
                        default:
                            return new BuySellDocViewState_Problem_Salesman(customerPersonId, sellerPersonId);
                    }


                case BuySellDocumentTypeENUM.Delivery:     // ******** DELIVERY
                    switch (buySellDocStateEnum)
                    {

                        case BuySellDocStateENUM.RequestUnconfirmed:
                            return new BuySellDocViewState_RequestUnconfirmed_Delivery(customerPersonId, sellerPersonId);


                        case BuySellDocStateENUM.RequestConfirmed:
                            return new BuySellDocViewState_RequestConfirmed_Delivery(customerPersonId, sellerPersonId);


                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                            return new BuySellDocViewState_BeingPreparedForShipmentBySeller_Delivery(customerPersonId, sellerPersonId);


                        case BuySellDocStateENUM.ReadyForPickup:
                            return new BuySellDocViewState_ReadyForPickup_Delivery(customerPersonId, sellerPersonId);


                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                            return new BuySellDocViewState_CourierAcceptedByBuyerAndSeller_Delivery(customerPersonId, sellerPersonId);


                        case BuySellDocStateENUM.CourierComingToPickUp:
                            return new BuySellDocViewState_CourierComingToPickUp_Delivery(customerPersonId, sellerPersonId);


                        //case BuySellDocStateENUM.PickedUp:
                        //    return new BuySellDocViewState_PickedUp_Delivery(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.Enroute:
                            return new BuySellDocViewState_Enroute_Delivery(customerPersonId, sellerPersonId);


                        case BuySellDocStateENUM.Delivered:
                            return new BuySellDocViewState_Delivered_Delivery(customerPersonId, sellerPersonId);


                        case BuySellDocStateENUM.Rejected:
                            return new BuySellDocViewState_Rejected_Delivery(customerPersonId, sellerPersonId);


                        case BuySellDocStateENUM.Problem:
                            return new BuySellDocViewState_Problem_Delivery(customerPersonId, sellerPersonId);

                        case BuySellDocStateENUM.BackOrdered:
                        case BuySellDocStateENUM.InProccess:
                        case BuySellDocStateENUM.All:
                        case BuySellDocStateENUM.Unknown:
                        
                        default:
                            return new BuySellDocViewState_Problem_Delivery(customerPersonId, sellerPersonId);

                    }


                default:
                    throw new Exception("In switch: switch (buySellDocumentTypeEnum)");
            }
            return viewState;
        }
    }
}
