using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS
{
    /// <summary>
    /// During sale the vendor / seller cannot update the CustomerId or the SellerId
    /// What will happen if the seller and the delivery man are the same. They could cheat the customer.
    /// The customer can always create a problem.
    /// </summary>
    public partial class BuySellDoc
    {
        /// <summary>
        /// This is only concerned with updating information
        /// do not add any logic here.
        /// Add logic in Business rules.
        /// </summary>
        /// <param name="icommonWithId"></param>
        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            BuySellDoc bsd = BuySellDoc.UnBox(icommonWithId);


            update_DocumentTypeAndState(bsd);

            switch (bsd.BuySellDocumentTypeEnum)
            {

                //*******************************************************************************
                //*     DELIVERY
                //*******************************************************************************
                case BuySellDocumentTypeENUM.Delivery:
                    switch (bsd.BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.ReadyForPickup:
                            //update the courier offer
                            update_OfferVariables(bsd);
                            break;

                        case BuySellDocStateENUM.RequestUnconfirmed:
                        case BuySellDocStateENUM.RequestConfirmed:
                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                        case BuySellDocStateENUM.CourierComingToPickUp:
                            break;
                        case BuySellDocStateENUM.Enroute:

                            DeliveryCode_Deliveryman_AsEntered = bsd.DeliveryCode_Deliveryman_AsEntered;
                            DeliveryCode_Deliveryman = parse_DeliveryCode_Deliveryman(DeliveryCode_Deliveryman_AsEntered);

                            break;

                        case BuySellDocStateENUM.Delivered:
                        case BuySellDocStateENUM.Rejected:
                        case BuySellDocStateENUM.Problem:
                            globalUpdate(bsd);
                            break;
                        case BuySellDocStateENUM.Unknown:
                            break;
                        default:
                            break;
                    }
                    break;


                //*******************************************************************************
                //*     SALE
                //*******************************************************************************
                case BuySellDocumentTypeENUM.Sale:
                    switch (bsd.BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.RequestConfirmed:
                        //Insurance can be added.
                        //the quantity can be reduced.
                        //a message to the purchaser can be sent.
                            ExpectedDeliveryDate = bsd.ExpectedDeliveryDate;
                            break;

                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                            globalUpdate(bsd);
                            break;

                        case BuySellDocStateENUM.CourierComingToPickUp:
                            PickupCode_Seller = bsd.PickupCode_Seller;
                            break;



                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                            FreightOfferTrxAcceptedId = bsd.FreightOfferTrxAcceptedId;
                            //CourierSelected = buySellDoc.CourierSelected;
                            break;

                        case BuySellDocStateENUM.ReadyForPickup:
                            InsuranceRequired = bsd.InsuranceRequired;
                            break;
                        case BuySellDocStateENUM.Rejected:
                        case BuySellDocStateENUM.Problem:
                        case BuySellDocStateENUM.RequestUnconfirmed:
                        case BuySellDocStateENUM.Unknown:
                        case BuySellDocStateENUM.Enroute:
                        case BuySellDocStateENUM.Delivered:
                        default:
                            break;
                    }
                    break;


                //*******************************************************************************
                //*     PURCHASE
                //*******************************************************************************

                case BuySellDocumentTypeENUM.Purchase:
                    switch (bsd.BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.RequestUnconfirmed:
                            //update_Accept(buySellDoc);
                            update_Freight_Request_Variables(bsd);
                            update_VehicalType(bsd);
                            update_Customer_Comment(bsd);
                            update_BillToAddress(bsd);
                            update_ShipToAddress(bsd);
                            //update_Opt_Out_Of_System(bsd);
                            //update_CustomerBalanceRefundable(buySellDoc);
                            //update_InsuranceRequired(buySellDoc);
                            //update_OfferVariables(buySellDoc);

                            break;

                        case BuySellDocStateENUM.RequestConfirmed:
                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                        case BuySellDocStateENUM.ReadyForPickup:

                            update_Customer_Comment(bsd);
                            update_Freight_Request_Variables(bsd);
                            break;

                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                            update_freightOfferAccepted(bsd);
                            break;

                        case BuySellDocStateENUM.Delivered:
                        case BuySellDocStateENUM.Rejected:
                        case BuySellDocStateENUM.Problem:
                        case BuySellDocStateENUM.CourierComingToPickUp:
                        case BuySellDocStateENUM.Enroute:
                        case BuySellDocStateENUM.Unknown:
                        default:
                            break;
                    }
                    break;

                //*******************************************************************************
                //*     PURCHASE
                //*******************************************************************************

                case BuySellDocumentTypeENUM.Salesman:
                    switch (bsd.BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.RequestUnconfirmed:
                            break;
                        case BuySellDocStateENUM.RequestConfirmed:
                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                        case BuySellDocStateENUM.ReadyForPickup:
                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                        case BuySellDocStateENUM.Delivered:
                        case BuySellDocStateENUM.Rejected:
                        case BuySellDocStateENUM.Problem:
                        case BuySellDocStateENUM.CourierComingToPickUp:
                        case BuySellDocStateENUM.Enroute:
                        case BuySellDocStateENUM.Unknown:
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }



        }

        //private void update_Opt_Out_Of_System(BuySellDoc bsd)
        //{
            
        //    if (bsd.OptedOutOfSystem.IsSelected)
        //    {
        //        //the user names will be added in the "Fix" section
        //        //of the BusinessLayer
        //        OptedOutOfSystem.MarkTrue("", "");

        //    }

        //}


        private void update_Customer_Comment(BuySellDoc buySellDoc)
        {
            Comment = buySellDoc.Comment;

        }

        private void update_DocumentTypeAndState(BuySellDoc buySellDoc)
        {
            BuySellDocStateEnum = buySellDoc.BuySellDocStateEnum;
            BuySellDocumentTypeEnum = buySellDoc.BuySellDocumentTypeEnum;
        }

        private void update_BillToAddress(BuySellDoc buySellDoc)
        {
            AddressBillToComplex = buySellDoc.AddressBillToComplex;
            AddressBillToId = buySellDoc.AddressBillToId;
        }

        private void update_ShipToAddress(BuySellDoc buySellDoc)
        {
            AddressShipToId = buySellDoc.AddressShipToId;
            AddressShipToComplex = buySellDoc.AddressShipToComplex;
        }

        private void update_OfferVariables(BuySellDoc buySellDoc)
        {
            OfferedPickupOnDate = buySellDoc.OfferedPickupOnDate;
            VehicalTypeOfferedId = buySellDoc.VehicalTypeOfferedId;
            FreightOffer = buySellDoc.FreightOffer;
            CommentByDeliveryman = buySellDoc.CommentByDeliveryman;
            //InsuranceRequired = buySellDoc.InsuranceRequired;
        }
        private void update_InsuranceRequired(BuySellDoc buySellDoc)
        {
            InsuranceRequired = buySellDoc.InsuranceRequired;
        }

        private void globalUpdate(BuySellDoc buySellDoc)
        {
            update_DocumentTypeAndState(buySellDoc);
            update_OfferVariables(buySellDoc);
            update_InsuranceRequired(buySellDoc);
            update_CustomerBalanceRefundable(buySellDoc);

        }


        private void update_CustomerBalanceRefundable(BuySellDoc buySellDoc)
        {
            //todo
            //DANGEROUS... why are we doing this? This can be solved by Superbiz.
            CustomerBalanceRefundable = buySellDoc.CustomerBalanceRefundable;
        }

    }



}
