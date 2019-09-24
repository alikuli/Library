using AliKuli.Extentions;
using AliKuli.ToolsNS;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FreightOffersTrxNS;
using ModelsClassLibrary.ModelsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Complex;
using System;
using System.Linq;
namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellDocBiz
    {


        public override void BusinessRulesFor(ControllerCreateEditParameter parm)
        {
            base.BusinessRulesFor(parm);

            execute_BusinessRules(parm);
        }

        private void execute_BusinessRules(ControllerCreateEditParameter parm)
        {
            BuySellDoc buySellDoc = parm.Entity as BuySellDoc;

            switch (buySellDoc.BuySellDocumentTypeEnum)
            {



                case BuySellDocumentTypeENUM.Delivery:
                    switch (buySellDoc.BuySellDocStateEnum)
                    {

                        case BuySellDocStateENUM.ReadyForPickup:
                            throwExceptionIfVehicalTypeNotAdded(buySellDoc);
                            not_Within_Shipping_Window_Throw_Exception(buySellDoc);
                            deliveryman_Makes_Bid_To_Pickup(buySellDoc);
                            break;

                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                            not_Within_Shipping_Window_Throw_Exception(buySellDoc);
                            deliveryman_Accepts_Request_To_Pickup(buySellDoc);
                            break;


                        case BuySellDocStateENUM.CourierComingToPickUp:
                            not_Within_Shipping_Window_Throw_Exception(buySellDoc);
                            cancel_Pickup_of(buySellDoc);

                            break;

                        case BuySellDocStateENUM.PickedUp:
                            break;


                        case BuySellDocStateENUM.Enroute:
                            deliveryman_Delivers_Product(buySellDoc);
                            canceled_Becomes_A_Problem(buySellDoc);
                            break;

                        case BuySellDocStateENUM.Delivered:
                        case BuySellDocStateENUM.Rejected:
                        case BuySellDocStateENUM.Problem:
                        case BuySellDocStateENUM.Unknown:
                        case BuySellDocStateENUM.InProccess:
                        case BuySellDocStateENUM.BackOrdered:
                        case BuySellDocStateENUM.All:
                        case BuySellDocStateENUM.RequestUnconfirmed:
                        case BuySellDocStateENUM.RequestConfirmed:
                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                        default:
                            break;
                    }
                    break;


                case BuySellDocumentTypeENUM.Sale:
                    switch (buySellDoc.BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.Unknown:
                        case BuySellDocStateENUM.InProccess:
                        case BuySellDocStateENUM.BackOrdered:
                        case BuySellDocStateENUM.All:
                        case BuySellDocStateENUM.RequestUnconfirmed:
                            not_Within_Shipping_Window_Throw_Exception(buySellDoc);
                            break;

                        case BuySellDocStateENUM.RequestConfirmed:
                            not_Within_Shipping_Window_Throw_Exception(buySellDoc);
                            seller_Accepts_Buyers_RequestConfirmed(buySellDoc);

                            break;

                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                            not_Within_Shipping_Window_Throw_Exception(buySellDoc);
                            seller_Has_Completed_Preparing_Parcel_For_Shipment(buySellDoc);
                            break;

                        case BuySellDocStateENUM.ReadyForPickup:
                            not_Within_Shipping_Window_Throw_Exception(buySellDoc);
                            seller_Cancels_Deliveryman(buySellDoc);
                            seller_Selects_Deliveryman_For_Shipment(buySellDoc);
                            seller_Cancels_And_Goes_Back_To_BeingPreparedForShipmentBySeller(buySellDoc);
                            break;

                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                            not_Within_Shipping_Window_Throw_Exception(buySellDoc);
                            cancel_Pickup_of(buySellDoc);

                            break;

                        case BuySellDocStateENUM.CourierComingToPickUp:
                            not_Within_Shipping_Window_Throw_Exception(buySellDoc);
                            deliveryman_PicksUp_From_Seller(buySellDoc);
                            cancel_Pickup_of(buySellDoc);

                            break;

                        case BuySellDocStateENUM.PickedUp:
                        case BuySellDocStateENUM.Enroute:
                            canceled_Becomes_A_Problem(buySellDoc);
                            break;

                        case BuySellDocStateENUM.Delivered:
                            break;

                        case BuySellDocStateENUM.Rejected:
                            //this is during BuySellController.CancelOrder

                            break;
                        case BuySellDocStateENUM.Problem:
                            break;
                        default:
                            break;
                    }
                    break;


                case BuySellDocumentTypeENUM.Purchase:
                    switch (buySellDoc.BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.Unknown:
                        case BuySellDocStateENUM.InProccess:
                        case BuySellDocStateENUM.BackOrdered:
                        case BuySellDocStateENUM.All:
                            break;

                        case BuySellDocStateENUM.RequestUnconfirmed:
                            buyer_Confirms_His_PurchaseOrder(buySellDoc, parm);
                            //calculate_CustomerSalesman_Commission(buySellDoc);
                            //calculate_OwnerSalesman_Commission(buySellDoc);
                            //calculate_System_Commission(buySellDoc);

                            break;

                        case BuySellDocStateENUM.RequestConfirmed:
                            not_Within_Shipping_Window_Throw_Message(buySellDoc);

                            break;

                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                            break;

                        case BuySellDocStateENUM.ReadyForPickup:
                            Buyer_Selects_Courier_For_Shipment(buySellDoc);
                            break;

                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                            break;
                        case BuySellDocStateENUM.CourierComingToPickUp:
                            break;
                        case BuySellDocStateENUM.PickedUp:
                            break;
                        case BuySellDocStateENUM.Enroute:
                            canceled_Becomes_A_Problem(buySellDoc);
                            break;

                        case BuySellDocStateENUM.Delivered:
                            break;

                        case BuySellDocStateENUM.Rejected:
                            Buyer_Converts_BuySell_To_RequestUnconfirmed(buySellDoc);
                            break;

                        case BuySellDocStateENUM.Problem:
                            break;

                        default:
                            break;
                    }
                    break;



                default:
                    throw new Exception("Unknown Document Type.");
            }
            //    cancel_With_Penalty(buySellDoc);
            ThrowExceptionIfBillToIdOrShipToIdIsEmptyOrNull(buySellDoc);
            not_Within_Shipping_Window_Throw_Message(buySellDoc);
            order_Canceled(buySellDoc);
            order_Rejected(buySellDoc);
            set_Commissions(buySellDoc);
            reset_Commissions_And_Fields(buySellDoc);
            CreateBuySellHistory(buySellDoc);

        }

        private void canceled_Becomes_A_Problem(BuySellDoc buySellDoc)
        {
            if (buySellDoc.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Cancel)
            {
                buySellDoc.BuySellDocStateEnum = BuySellDocStateENUM.Problem;
                buySellDoc.BuySellDocStateModifierEnum = BuySellDocStateModifierENUM.Unknown;
            }
        }


        private void seller_Cancels_And_Goes_Back_To_BeingPreparedForShipmentBySeller(BuySellDoc buySellDoc)
        {
            if (buySellDoc.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Cancel)
            {
                reset_DeliverymansCommissionAndFields(buySellDoc);

                buySellDoc.BuySellDocStateEnum = BuySellDocStateENUM.BeingPreparedForShipmentBySeller;
                buySellDoc.BuySellDocStateModifierEnum = BuySellDocStateModifierENUM.Unknown;

            }

        }


        private void cancel_Pickup_of(BuySellDoc buySellDoc)
        {
            if (buySellDoc.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Cancel)
            {
                reset_DeliverymansCommissionAndFields(buySellDoc);
                buySellDoc.BuySellDocStateEnum = BuySellDocStateENUM.ReadyForPickup;
                buySellDoc.BuySellDocStateModifierEnum = BuySellDocStateModifierENUM.Unknown;
            }
        }

        private void CreateBuySellHistory(BuySellDoc buySellDoc)
        {
            BuySellDocHistory buySellDocHistory = BuySellDocHistory.UnBox(BuySellDocHistoryBiz.Factory());
            buySellDocHistory.Init(buySellDoc);
            BuySellDocHistoryBiz.Create(buySellDocHistory);


        }



        //**********************************************************************************************************
        // Commission for Sales
        //**********************************************************************************************************

        /// <summary>
        /// Use this so that all the amounts are in sync.
        /// </summary>
        /// <param name="buySellDoc"></param>
        private void set_Commissions(BuySellDoc buySellDoc)
        {
            if (buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed)
                return;

            if (buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.RequestConfirmed)
                return;

            if (buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                return;

            set_Owners_MaxCommission(buySellDoc);

            set_OwnersSalesMan_Commission(buySellDoc);
            set_Super_OwnersSalesMan_Commission(buySellDoc);
            set_Super_Super_OwnersSalesMan_Commission(buySellDoc);

            set_CustomersSalesMan_Commission(buySellDoc);
            set_Super_CustomersSalesMan_Commission(buySellDoc);
            set_Super_Super_CustomersSalesMan_Commission(buySellDoc);

            set_Deliveryman_Commission(buySellDoc);

            set_DeliverymansSalesMan_Commission(buySellDoc);
            set_Super_DeliverymansSalesMan_Commission(buySellDoc);
            set_Super_Super_DeliverymansSalesMan_Commission(buySellDoc);

            set_System_Commission_Freight(buySellDoc);
            set_System_Commission_SaleWithoutFreight(buySellDoc);

        }

        private void set_Super_Super_DeliverymansSalesMan_Commission(BuySellDoc bsd)
        {
            if (bsd.DeliverymanSalesmanId.IsNullOrWhiteSpace())
            {
                bsd.SuperSuperDeliverymanSalesmanCommission = new CommissionComplex();
                return;
            }

            if (bsd.SuperDeliverymanSalesmanId.IsNullOrWhiteSpace())
            {
                bsd.SuperSuperDeliverymanSalesmanCommission = new CommissionComplex();
                return;
            }

            //check to see if Super Deliveryman Salesman Exists
            if (bsd.SuperSuperDeliverymanSalesmanId.IsNullOrWhiteSpace())
            {
                bsd.SuperSuperDeliverymanSalesmanCommission = new CommissionComplex();
                return;
            }



            decimal pct = SalesCommissionClass.CommissionPct_Deliveryman_Super_Super_Salesman;
            bsd.SuperSuperDeliverymanSalesmanCommission.Percent = pct;

            if (bsd.TotalInvoiceLessFreight * pct == 0)
                return;

            bsd.SuperSuperDeliverymanSalesmanCommission.Amount = bsd.TotalInvoiceLessFreight * pct / 100;

        }

        private void set_Super_DeliverymansSalesMan_Commission(BuySellDoc bsd)
        {


            if (bsd.DeliverymanSalesmanId.IsNullOrWhiteSpace())
            {
                bsd.SuperDeliverymanSalesmanCommission = new CommissionComplex();
                return;
            }

            //check to see if Super Deliveryman Salesman Exists
            if (bsd.SuperDeliverymanSalesmanId.IsNull())
            {
                bsd.SuperDeliverymanSalesmanCommission = new CommissionComplex();
                return;
            }

            //reset if ownerSalesmanId is null

            decimal pct = SalesCommissionClass.CommissionPct_Deliveryman_Super_Salesman;
            bsd.SuperDeliverymanSalesmanCommission.Percent = pct;
            if (bsd.TotalInvoiceLessFreight * pct == 0)
                return;

            bsd.SuperDeliverymanSalesmanCommission.Amount = bsd.TotalInvoiceLessFreight * pct / 100;

        }

        private void set_Super_Super_CustomersSalesMan_Commission(BuySellDoc bsd)
        {
            //check to see if Customer Salesman Exists
            if (bsd.CustomerSalesmanId.IsNullOrWhiteSpace())
            {
                bsd.SuperSuperCustomerSalesmanCommission = new CommissionComplex();
                return;
            }

            //check to see if Super Customer Salesman Exists
            if (bsd.SuperCustomerSalesmanId.IsNull())
            {
                bsd.SuperSuperCustomerSalesmanCommission = new CommissionComplex();
                return;
            }

            if (bsd.SuperSuperCustomerSalesmanId.IsNull())
            {
                bsd.SuperSuperCustomerSalesmanCommission = new CommissionComplex();
                return;
            }

            decimal pct = SalesCommissionClass.CommissionPct_Customer_Super_Super_Salesman;
            bsd.SuperSuperCustomerSalesmanCommission.Percent = pct;

            if (bsd.TotalInvoiceLessFreight * pct == 0)
                return;

            bsd.SuperSuperCustomerSalesmanCommission.Amount = bsd.TotalInvoiceLessFreight * pct / 100;

        }

        private void set_Super_CustomersSalesMan_Commission(BuySellDoc bsd)
        {
            if (bsd.CustomerSalesmanId.IsNullOrWhiteSpace())
            {
                bsd.SuperCustomerSalesmanCommission = new CommissionComplex();
                return;
            }

            //check to see if Super Customer Salesman Exists
            if (bsd.SuperCustomerSalesman.IsNull())
            {
                bsd.SuperCustomerSalesmanCommission = new CommissionComplex();
                return;
            }

            //reset if ownerSalesmanId is null

            decimal pct = SalesCommissionClass.CommissionPct_Customer_Super_Salesman;
            bsd.SuperCustomerSalesmanCommission.Percent = pct;
            if (bsd.TotalInvoiceLessFreight * pct == 0)
                return;

            bsd.SuperCustomerSalesmanCommission.Amount = bsd.TotalInvoiceLessFreight * pct / 100;

        }

        private void set_Super_Super_OwnersSalesMan_Commission(BuySellDoc bsd)
        {
            //check to see if Super Owner Salesman Exists
            if (bsd.OwnerSalesmanId.IsNullOrWhiteSpace())
            {
                bsd.SuperSuperOwnerSalesmanCommission = new CommissionComplex();
                return;
            }

            //check to see if Super Owner Salesman Exists
            if (bsd.SuperOwnerSalesmanId.IsNull())
            {
                bsd.SuperSuperOwnerSalesmanCommission = new CommissionComplex();
                return;
            }

            if (bsd.SuperSuperOwnerSalesmanId.IsNull())
            {
                bsd.SuperSuperOwnerSalesmanCommission = new CommissionComplex();
                return;
            }


            decimal pct = SalesCommissionClass.CommissionPct_Owner_Super_Super_Salesman;
            bsd.SuperSuperOwnerSalesmanCommission.Percent = pct;

            if (bsd.TotalInvoiceLessFreight * pct == 0)
                return;

            bsd.SuperSuperOwnerSalesmanCommission.Amount = bsd.TotalInvoiceLessFreight * pct / 100;
        }

        private void set_Super_OwnersSalesMan_Commission(BuySellDoc bsd)
        {
            if (bsd.OwnerSalesmanId.IsNullOrWhiteSpace())
            {
                bsd.SuperOwnerSalesmanCommission = new CommissionComplex();
                return;
            }

            //check to see if Super Owner Salesman Exists
            if (bsd.SuperOwnerSalesmanId.IsNull())
            {
                bsd.SuperOwnerSalesmanCommission = new CommissionComplex();
                return;
            }

            //reset if ownerSalesmanId is null

            decimal pct = SalesCommissionClass.CommissionPct_Owner_Super_Salesman;
            bsd.SuperOwnerSalesmanCommission.Percent = pct;
            if (bsd.TotalInvoiceLessFreight * pct == 0)
                return;

            bsd.SuperOwnerSalesmanCommission.Amount = bsd.TotalInvoiceLessFreight * pct / 100;

        }

        private void reset_Commissions_And_Fields(BuySellDoc buySellDoc)
        {
            if (buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed ||
                buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.RequestConfirmed ||
                buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
            {
                reset_CustomersCommissionAndFields(buySellDoc);
                reset_OwnersCommissionAndFields(buySellDoc);
                reset_DeliverymansCommissionAndFields(buySellDoc);
                reset_SystemsCommission(buySellDoc);
            }

        }


        private void reset_CustomersCommissionAndFields(BuySellDoc bsd)
        {
            //buySellDoc.CustomerSalesmanId_TEMP = null;
            bsd.CustomerSalesmanId = null;
            bsd.CustomerSalesman = null;
            bsd.CustomerSalesmanCommission = new CommissionComplex();

            bsd.SuperCustomerSalesmanId = null;
            bsd.SuperCustomerSalesman = null;
            bsd.SuperCustomerSalesmanCommission = new CommissionComplex();

            bsd.SuperSuperCustomerSalesmanId = null;
            bsd.SuperSuperCustomerSalesman = null;
            bsd.SuperSuperCustomerSalesmanCommission = new CommissionComplex();
        }


        private void reset_OwnersCommissionAndFields(BuySellDoc bsd)
        {

            //buySellDoc.OwnerSalesmanId_TEMP = null;
            bsd.OwnerSalesmanId = null;
            bsd.OwnerSalesman = null;

            bsd.OwnerSalesmanCommission = new CommissionComplex();

            bsd.SuperOwnerSalesmanId = null;
            bsd.SuperOwnerSalesman = null;
            bsd.SuperOwnerSalesmanCommission = new CommissionComplex();

            bsd.SuperSuperOwnerSalesmanId = null;
            bsd.SuperSuperOwnerSalesman = null;
            bsd.SuperSuperOwnerSalesmanCommission = new CommissionComplex();

            bsd.Total_Charged_To_Owner = new CommissionComplex();


        }


        private void reset_DeliverymansCommissionAndFields(BuySellDoc bsd)
        {
            if (!bsd.FreightOfferTrxAcceptedId.IsNullOrWhiteSpace())
            {
                FreightOfferTrx freightOfferTrx = FreightOfferTrxBiz.Find(bsd.FreightOfferTrxAcceptedId);
                freightOfferTrx.IsNullThrowException();
                //freightOfferTrx.OfferAcceptedByDeliveryman = new BoolDateAndByComplex();
                freightOfferTrx.OfferAcceptedByOwner = new BoolDateAndByComplex();
                FreightOfferTrxBiz.Update(freightOfferTrx);

                bsd.FreightOfferTrxAcceptedId = null;
                bsd.FreightOfferTrxAccepted = null;

            }

            bsd.DeliverymanSalesman = null;
            bsd.DeliverymanSalesmanId = null;
            bsd.DeliverymanSalesmanCommission = new CommissionComplex();

            bsd.SuperDeliverymanSalesmanId = null;
            bsd.SuperDeliverymanSalesman = null;
            bsd.SuperDeliverymanSalesmanCommission = new CommissionComplex();

            bsd.SuperSuperDeliverymanSalesmanId = null;
            bsd.SuperSuperDeliverymanSalesman = null;
            bsd.SuperSuperDeliverymanSalesmanCommission = new CommissionComplex();

            bsd.Total_Charged_To_Deliveryman = new CommissionComplex();


            bsd.CourierAcceptedByBuyerAndSeller = new BoolDateAndByComplex();
            bsd.CourierComingToPickUp = new BoolDateAndByComplex();


        }

        private void reset_SystemsCommission(BuySellDoc buySellDoc)
        {
            buySellDoc.System_Commission_For_SaleWithoutFreight = new CommissionComplex();
            buySellDoc.System_Commission_For_Freight = new CommissionComplex();


        }



        //**********************************************************************************************************
        // Commission for Sales
        //**********************************************************************************************************


        //this will always be the max commission chargeable
        //use calculate_Commissions
        private void set_Owners_MaxCommission(BuySellDoc buySellDoc)
        {
            //this should be on the TotalSaleLessFreight
            buySellDoc.Total_Charged_To_Owner.Amount = buySellDoc.Get_Maximum_Commission_Chargeable_On_TotalSaleLessFreight_Amount();
            buySellDoc.Total_Charged_To_Owner.Percent = buySellDoc.Get_Maximum_Commission_Chargeable_On_TotalSaleLessFreight_Based_On_TotalSale_Percent();
            //buySellDoc.Total_Charged_To_Owner.Percent = buySellDoc.Get_Maximum_Commission_Chargeable_On_TotalSaleLessFreight_Based_On_TotalSale_Percent();

        }


        //use calculate_Commissions

        private void set_OwnersSalesMan_Commission(BuySellDoc buySellDoc)
        {
            if (buySellDoc.OwnerSalesmanId.IsNullOrWhiteSpace())
            {
                buySellDoc.OwnerSalesmanCommission = new CommissionComplex();
                return;
            }

            decimal ownersCommissionPct = SalesCommissionClass.CommissionPct_OwnerSalesman;
            buySellDoc.OwnerSalesmanCommission.Percent = SalesCommissionClass.CommissionPct_OwnerSalesman;
            if (buySellDoc.TotalInvoiceLessFreight * ownersCommissionPct == 0)
                return;

            buySellDoc.OwnerSalesmanCommission.Amount = buySellDoc.TotalInvoiceLessFreight * ownersCommissionPct / 100;

            //buySellDoc.OwnerSalesmanCommission.Amount = buySellDoc.Get_Maximum_Commision_On_TotalSaleLessFreight_For_OwnerSalesman_Amount();
            //buySellDoc.OwnerSalesmanCommission.Percent = buySellDoc.Get_Maximum_Commision_On_TotalSaleLessFreight_For_OwnerSalesman_Percent();
        }





        //use calculate_Commissions

        private void set_CustomersSalesMan_Commission(BuySellDoc buySellDoc)
        {
            if (buySellDoc.CustomerSalesmanId.IsNullOrWhiteSpace())
            {
                buySellDoc.CustomerSalesmanCommission = new CommissionComplex();
                return;
            }

            decimal commissionPct = SalesCommissionClass.CommissionPct_OwnerSalesman;
            buySellDoc.CustomerSalesmanCommission.Percent = commissionPct;

            if (commissionPct * buySellDoc.TotalInvoiceLessFreight == 0)
                return;

            buySellDoc.CustomerSalesmanCommission.Amount = commissionPct * buySellDoc.TotalInvoiceLessFreight / 100;
        }


        //------------------------------------------------- This ends the Commission on Sales





        //**********************************************************************************************************
        // Commission for Freight
        //**********************************************************************************************************



        /// <summary>
        /// This is always max.
        /// Not the percent is of the deliveryman's sale amount
        /// use calculate_Commissions
        /// </summary>
        /// <param name="buySellDoc"></param>
        private void set_Deliveryman_Commission(BuySellDoc buySellDoc)
        {
            buySellDoc.Total_Charged_To_Deliveryman.Amount = buySellDoc.Get_Maximum_Commission_Chargeable_On_Freight_Amount();
            buySellDoc.Total_Charged_To_Deliveryman.Percent = buySellDoc.Get_Maximum_Commission_Chargeable_On_Freight_TO_SalesPeople_And_System_Percent();

        }




        /// use calculate_Commissions
        private void set_DeliverymansSalesMan_Commission(BuySellDoc buySellDoc)
        {
            if (buySellDoc.DeliverymanSalesmanId.IsNullOrWhiteSpace())
            {
                buySellDoc.DeliverymanSalesmanCommission = new CommissionComplex();
                return;

            }

            decimal commissionPct = SalesCommissionClass.CommissionPct_DeliverymanSalesman;
            buySellDoc.DeliverymanSalesmanCommission.Percent = commissionPct;

            if (commissionPct * buySellDoc.Freight_Accepted == 0)
                return;
            buySellDoc.DeliverymanSalesmanCommission.Amount = commissionPct * buySellDoc.Freight_Accepted / 100;
        }




        //------------------------------------------------- This ends the Commission on Freight

        //**********************************************************************************************************
        // Commission for Freight and sales for System
        //**********************************************************************************************************


        /// use calculate_Commissions
        private void set_System_Commission_SaleWithoutFreight(BuySellDoc buySellDoc)
        {
            decimal commissionPct = SalesCommissionClass.CommissionPct_System;

            buySellDoc.System_Commission_For_SaleWithoutFreight.Percent = commissionPct;
            if (commissionPct * buySellDoc.TotalInvoiceLessFreight == 0)
                return;
            buySellDoc.System_Commission_For_SaleWithoutFreight.Amount = commissionPct * buySellDoc.TotalInvoiceLessFreight / 100;

        }






        private void set_System_Commission_Freight(BuySellDoc buySellDoc)
        {

            decimal commissionPct = SalesCommissionClass.CommissionPct_System;

            buySellDoc.System_Commission_For_Freight.Percent = commissionPct;
            if (commissionPct * buySellDoc.Freight_Accepted == 0)
                return;

            buySellDoc.System_Commission_For_Freight.Amount = commissionPct * buySellDoc.Freight_Accepted / 100;

        }


        //------------------------------------------------- This ends the Commission for Freight and sales for System




        private void order_Canceled(BuySellDoc buySellDoc)
        {
            if (buySellDoc.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Cancel)
            {
                buySellDoc.BuySellDocStateEnum = BuySellDocStateENUM.RequestUnconfirmed;
                buySellDoc.RequestConfirmed.Clear();
                reset_Commissions_And_Fields(buySellDoc);
            }
        }


        private void order_Rejected(BuySellDoc buySellDoc)
        {
            if (buySellDoc.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Reject)
            {
                buySellDoc.BuySellDocStateEnum = BuySellDocStateENUM.Rejected;
                reset_Commissions_And_Fields(buySellDoc);

            }
        }

        #region Deliveryman rules

        private void deliveryman_Delivers_Product(BuySellDoc bsd)
        {
            if (bsd.DeliveryCode_Deliveryman == bsd.DeliveryCode_Customer)
            {
                bsd.BuySellDocStateEnum = BuySellDocStateENUM.Delivered;
                bsd.Delivered.IsTrue = true;

                bsd.Delivered.SetToTodaysDate(UserName, UserId);
            }
        }

        private void deliveryman_Accepts_Request_To_Pickup(BuySellDoc bsd)
        {
            if (bsd.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Accept)
            {
                bsd.CourierComingToPickUp.SetToTodaysDate(UserName, UserId);
                bsd.DeliveryCode_Customer = GetRandomCode();
                bsd.PickupCode_Deliveryman = GetRandomCode();
                bsd.BuySellDocStateEnum = BuySellDocStateENUM.CourierComingToPickUp;
            }
        }

        private void deliveryman_Makes_Bid_To_Pickup(BuySellDoc buySellDoc)
        {
            if (UserId.IsNullOrWhiteSpace())
                return;


            if (buySellDoc.FreightOfferDecimal > 0)
            {
                //we have an offer
                DateParameter dp = new DateParameter();
                dp.BeginDate = buySellDoc.PleasePickupOnDate_Start;
                dp.EndDate = buySellDoc.PleasePickupOnDate_End;

                if (dp.IsDateWithinBeginAndEndDatesInclusive(buySellDoc.OfferedPickupOnDate))
                {
                    string buySellDocId = buySellDoc.Id;

                    buySellDoc = Find(buySellDocId);
                    buySellDoc.IsNullThrowException();

                    //the pick up is today or later.
                    //the user is the deliveryman here
                    Deliveryman deliveryman = DeliverymanBiz.GetPlayerFor(UserId);
                    deliveryman.IsNullThrowException();


                    //create an offer
                    FreightOfferTrx frtOff = new FreightOfferTrx(
                        buySellDoc.Id,
                        deliveryman.Id,
                        buySellDoc.FreightOfferDecimal,
                        buySellDoc.OfferedPickupOnDate,
                        buySellDoc.ExpectedDeliveryDate,
                        buySellDoc.CommentByDeliveryman,
                        buySellDoc.VehicalTypeOfferedId);

                    //used later
                    frtOff.Deliveryman = deliveryman;
                    frtOff.BuySellDoc = buySellDoc;


                    //we will only create if there is not one bid from this deliveryman
                    //for this document. If there is already a bid, then all fields will be updated.
                    //the change will only be allowed if the status of the buyselldoc is

                    //check to see if a bid already exists.
                    //this locates an earlier bid and then updates it.

                    FreightOfferTrx frtOffTrxFound = FreightOfferTrxBiz.FindAll().FirstOrDefault(x => x.BuySellDocId == buySellDocId && x.DeliverymanId == deliveryman.Id);
                    if (frtOffTrxFound.IsNull())
                    {
                        //create a new bid.
                        buySellDoc.FreightOfferTrxs.Add(frtOff);
                        FreightOfferTrxBiz.Create(frtOff);
                    }
                    else
                    {
                        //update the old bid.
                        frtOffTrxFound.UpdatePropertiesDuringModify(frtOff as ICommonWithId);
                        FreightOfferTrxBiz.Update(frtOffTrxFound);
                    }

                }
            }
        }


        #endregion




        #region Buyer Rules

        private static void Buyer_Converts_BuySell_To_RequestUnconfirmed(BuySellDoc buySellDoc)
        {
            buySellDoc.BuySellDocStateEnum = BuySellDocStateENUM.RequestUnconfirmed;
        }

        private static void Buyer_Selects_Courier_For_Shipment(BuySellDoc buySellDoc)
        {
            if (buySellDoc.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Accept)
            {
                buySellDoc.BuySellDocStateEnum = BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller;
            }
        }

        private void buyer_Confirms_His_PurchaseOrder(BuySellDoc bsd, ControllerCreateEditParameter parm)
        {
            if (bsd.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Accept)
            {

                check_BillTo_Address_Is_filled(bsd);
                check_ShipTo_Address_Is_filled(bsd);
                check_BillFrom_Address_Is_filled(bsd);
                check_ThereAreFundsAvailable(bsd, parm.GlobalObject);

                update_Customer_Person_Bill_To_Default_Address_If_empty(bsd);

                //we get the salesmen in superbiz at the event fired by the BuySellDocsController.

                //get_CustomerSalesman(bsd);
                //get_OwnerSalesman(bsd);
                bsd.RequestConfirmed.SetToTodaysDate(UserName, UserId);
                bsd.BuySellDocStateEnum = BuySellDocStateENUM.RequestConfirmed;
                bsd.RequestConfirmed.SetToTodaysDate(UserName, UserId);
            }
        }

        //private void get_CustomerSalesman(BuySellDoc buySellDoc)
        //{
        //    if (!buySellDoc.CustomerSalesmanId_TEMP.IsNullOrWhiteSpace())
        //        buySellDoc.CustomerSalesmanId = buySellDoc.CustomerSalesmanId_TEMP;
        //}
        //private void get_OwnerSalesman(BuySellDoc buySellDoc)
        //{
        //    if (!buySellDoc.OwnerSalesmanId_TEMP.IsNullOrWhiteSpace())
        //        buySellDoc.OwnerSalesmanId = buySellDoc.OwnerSalesmanId_TEMP;
        //}



        #endregion


        #region Seller Rules


        private void deliveryman_PicksUp_From_Seller(BuySellDoc bsd)
        {
            if (bsd.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Cancel)
                return;


            if (bsd.PickupCode_Seller == bsd.PickupCode_Deliveryman)
            {
                string err = string.Format("Delivery code {0} Accepted!", bsd.PickupCode_Deliveryman);
                ErrorsGlobal.AddMessage(err);
                bsd.BuySellDocStateEnum = BuySellDocStateENUM.Enroute;
                bsd.Enroute.SetToTodaysDate(UserName, UserId);

            }
            else
            {
                string err = string.Format("The code {0} given by delivery man is incorrect!", bsd.PickupCode_Seller);
                throw new Exception(err);
            }
        }

        private void seller_Selects_Deliveryman_For_Shipment(BuySellDoc bsd)
        {
            if (bsd.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Accept)
            {
                bsd.BuySellDocStateEnum = BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller;
                bsd.CourierAcceptedByBuyerAndSeller.SetToTodaysDate(UserName, UserId);
            }
        }

        private void seller_Has_Completed_Preparing_Parcel_For_Shipment(BuySellDoc bsd)
        {
            if (bsd.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Accept)
            {
                bsd.BuySellDocStateEnum = BuySellDocStateENUM.ReadyForPickup;
                bsd.ReadyForPickup.SetToTodaysDate(UserName, UserId);
            }
        }


        private void seller_Accepts_Buyers_RequestConfirmed(BuySellDoc bsd)
        {
            if (bsd.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Accept)
            {
                //make sure the seller has given some Expected delivery address
                if (bsd.ExpectedDeliveryDate == DateTime.MinValue || bsd.ExpectedDeliveryDate == DateTime.MaxValue)
                {
                    throw new Exception("You must fill in the expected date the customer will receive the product.");
                }
                bsd.BuySellDocStateEnum = BuySellDocStateENUM.BeingPreparedForShipmentBySeller;
                bsd.BeingPreparedForShipmentBySeller.SetToTodaysDate(UserName, UserId);
            }
        }

        private void seller_Cancels_Deliveryman(BuySellDoc bsd)
        {
            if (bsd.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Cancel)
            {
                //bsd.FreightOfferTrxAcceptedId = null;
                //bsd.FreightOfferTrxAccepted = null;
                reset_DeliverymansCommissionAndFields(bsd);
                bsd.BuySellDocStateEnum = BuySellDocStateENUM.ReadyForPickup;
                bsd.ReadyForPickup.SetToTodaysDate(UserName, UserId);
            }


        }


        #endregion



        /// <summary>
        /// this only fires once user has accepted to move on
        /// </summary>
        /// <param name="buySellDoc"></param>
        private void update_Customer_Person_Bill_To_Default_Address_If_empty(BuySellDoc buySellDoc)
        {
            buySellDoc.IsNullThrowException();
            buySellDoc.CustomerId.IsNullOrWhiteSpaceThrowException();
            buySellDoc.AddressBillToId.IsNullOrWhiteSpaceThrowException();
            Person customerPerson = CustomerBiz.GetPersonForPlayer(buySellDoc.CustomerId);
            customerPerson.IsNullThrowException();

            if (customerPerson.DefaultBillAddressId.IsNullOrEmpty())
            {
                customerPerson.DefaultBillAddressId = buySellDoc.AddressBillToId;
                PersonBiz.Update(customerPerson);
            }

            //Now update the Customer Default Address if required.
            //get the customer.
            Customer customer = buySellDoc.Customer;
            if (customer.IsNull())
            {
                buySellDoc.CustomerId.IsNullOrWhiteSpaceThrowException();
                customer = CustomerBiz.Find(buySellDoc.CustomerId);
                customer.IsNullThrowException();

                if (customer.DefaultBillAddressId.IsNullOrWhiteSpace())
                {
                    customer.DefaultBillAddressId = customerPerson.DefaultBillAddressId;
                    CustomerBiz.Update(customer);
                }
            }
        }

        /// <summary>
        /// Note. This value is fresh as it has just been calculated this round.
        /// </summary>
        /// <param name="buySellDoc"></param>
        /// <param name="globalObject"></param>
        private void check_ThereAreFundsAvailable(BuySellDoc buySellDoc, GlobalObject globalObject)
        {

            if (UserId.IsNullOrWhiteSpace())
            {

            }
            else
            {
                globalObject.IsNullThrowException();
                decimal reqrdAmount = buySellDoc.TotalInvoice + 1000m;
                if (globalObject.Money_User.Refundable.MoneyAmount < (reqrdAmount))
                {
                    decimal shortfall = reqrdAmount - globalObject.Money_User.Refundable.MoneyAmount;
                    string err = string.Format("You do not have sufficent funds. You have {0} and you need {1} + 1000 (to cover freight). Therefore, you have a shortfall of {2}",
                        globalObject.Money_User.Refundable.MoneyAmount.ToString("N2"),
                        reqrdAmount.ToString("N2"),
                        shortfall.ToString("N2"));
                    throw new Exception(err);
                }
            }


        }

        //private FreightOfferTrx getFreightOfferTrx(BuySellDoc buysellDoc)
        //{
        //    if (buysellDoc.FreightOfferTrxs.IsNull())
        //        return null;

        //    FreightOfferTrx frtTrx = buysellDoc.FreightOfferTrxAccepted;
        //    frtTrx.IsNullThrowException();
        //    return frtTrx;
        //}

        //private void acceptCourier(BuySellDoc buySellDoc)
        //{
        //    if (buySellDoc.AcceptRejectOrEmpty == ConstantsLibrary.BuySellConstants.ACCEPT)
        //    {

        //        FreightOfferTrx frtTrx  = getFreightOfferTrx(buySellDoc);
        //        frtTrx.IsNullThrowException();

        //        Deliveryman deliveryman = frtTrx.Deliveryman;
        //        deliveryman.IsNullThrowException();
        //        buySellDoc.OwnerId.IsNullOrWhiteSpaceThrowArgumentException();

        //        Person personDeliveryman = DeliverymanBiz.GetPersonForPlayer(deliveryman.Id);
        //        Person personSeller = OwnerBiz.GetPersonForPlayer(buySellDoc.OwnerId);

        //        personDeliveryman.IsNullThrowException();
        //        personSeller.IsNullThrowException();

        //        if (personDeliveryman.Id == personSeller.Id)
        //        {
        //            string err = string.Format("You are the seller. Please ask the customer to accept you as a Delivery person by going to their Purchase screen. You cannot do it.");
        //            throw new Exception(err);
        //        }

        //        buySellDoc.BuySellDocStateEnum = BuySellDocStateENUM.CourierAccepted;

        //        //add the offer
        //    }
        //}


        private void check_BillTo_Address_Is_filled(BuySellDoc buySellDoc)
        {
            if (buySellDoc.AddressBillToComplex.ErrorCheck())
            {
                throw new Exception("** In Bill To Address ** " + buySellDoc.AddressBillToComplex.Error);
            }
        }
        private void check_BillFrom_Address_Is_filled(BuySellDoc buySellDoc)
        {
            if (buySellDoc.AddressShipFromComplex.ErrorCheck())
            {
                throw new Exception("** In Bill From Address ** " + buySellDoc.AddressShipFromComplex.Error);
            }
        }
        private void check_ShipTo_Address_Is_filled(BuySellDoc buySellDoc)
        {
            if (buySellDoc.AddressShipToComplex.ErrorCheck())
            {
                throw new Exception("** In Ship To Address ** " + buySellDoc.AddressShipToComplex.Error);
            }
        }


        private void throwExceptionIfVehicalTypeNotAdded(BuySellDoc buySellDoc)
        {
            if (buySellDoc.VehicalTypeOfferedId.IsNullOrWhiteSpace())
                throw new Exception("No Vehical type added. Please add one");
        }

        private static void ThrowExceptionIfBillToIdOrShipToIdIsEmptyOrNull(BuySellDoc buySellDoc)
        {
            if (buySellDoc.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Accept)
            {
                buySellDoc.AddressBillToId.IsNullOrWhiteSpaceThrowException("You must fill in the bill to address");
                buySellDoc.AddressShipToId.IsNullOrWhiteSpaceThrowException("You must fill in the ship to address");
            }
        }

        private void saleToSelfNotAllowed(BuySellDoc buySellDoc)
        {

            string personCustomerId = buySellDoc.Customer.PersonId;
            string personSellerId = buySellDoc.Owner.PersonId;
            if (personCustomerId == personSellerId)
            {
                ErrorsGlobal.Add(string.Format("This product belongs to you! You cannot sell to you self!"), "saleToSelfNotAllowed");
                throw new Exception(ErrorsGlobal.ToString());
            }
        }

        //private BuySellDocStateENUM Move_BuySellDocStateEnum_To_Next_Level_Buyer(BuySellDoc buySellDoc)
        //{
        //    //buySellDoc.BuySellDocStateEnum
        //    BuySellDocStateENUM buySellDocStateEnum = buySellDoc.BuySellDocStateEnum;

        //    switch (buySellDoc.BuySellDocStateEnum)
        //    {
        //        case BuySellDocStateENUM.InProccess:
        //        case BuySellDocStateENUM.BackOrdered:
        //        case BuySellDocStateENUM.All:
        //        case BuySellDocStateENUM.Delivered:
        //        case BuySellDocStateENUM.Problem:
        //            break;


        //        case BuySellDocStateENUM.Unknown:
        //            buySellDocStateEnum = BuySellDocStateENUM.RequestUnconfirmed;
        //            break;


        //        case BuySellDocStateENUM.RequestUnconfirmed:
        //            if (buySellDoc.AcceptRejectOrEmpty == ConstantsLibrary.BuySellConstants.ACCEPT)
        //            {
        //                buySellDocStateEnum = BuySellDocStateENUM.RequestConfirmed;
        //            }
        //            break;


        //        case BuySellDocStateENUM.RequestConfirmed:
        //            if (buySellDoc.AcceptRejectOrEmpty == ConstantsLibrary.BuySellConstants.ACCEPT)
        //            {
        //                buySellDocStateEnum = BuySellDocStateENUM.BeingPreparedForShipmentBySeller;
        //            }
        //            break;


        //        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
        //            if (buySellDoc.AcceptRejectOrEmpty == ConstantsLibrary.BuySellConstants.ACCEPT)
        //            {
        //                buySellDocStateEnum = BuySellDocStateENUM.ReadyForPickup;
        //            }
        //            break;


        //        case BuySellDocStateENUM.ReadyForPickup:
        //            if (buySellDoc.AcceptRejectOrEmpty == ConstantsLibrary.BuySellConstants.ACCEPT)
        //            {
        //                buySellDocStateEnum = BuySellDocStateENUM.CourierAccepted;
        //            }
        //            break;


        //        case BuySellDocStateENUM.CourierAccepted:
        //            if (buySellDoc.AcceptRejectOrEmpty == ConstantsLibrary.BuySellConstants.ACCEPT)
        //            {
        //                buySellDocStateEnum = BuySellDocStateENUM.ConfirmedByCourier;
        //            }
        //            break;

        //        case BuySellDocStateENUM.ConfirmedByCourier:
        //            if (buySellDoc.AcceptRejectOrEmpty == ConstantsLibrary.BuySellConstants.ACCEPT)
        //            {
        //                buySellDocStateEnum = BuySellDocStateENUM.PickedUp;
        //            }
        //            break;


        //        case BuySellDocStateENUM.PickedUp:
        //            if (buySellDoc.AcceptRejectOrEmpty == ConstantsLibrary.BuySellConstants.ACCEPT)
        //            {
        //                buySellDocStateEnum = BuySellDocStateENUM.Delivered;
        //            }
        //            break;


        //        case BuySellDocStateENUM.Rejected:
        //            if (buySellDoc.AcceptRejectOrEmpty == buySellDoc.AcceptRejectOrEmpty_Reject)
        //            {
        //                buySellDocStateEnum = BuySellDocStateENUM.Problem;
        //            }
        //            break;


        //        default:
        //            break;
        //    }
        //    return buySellDocStateEnum;
        //}


        //private BuySellDocStateENUM Move_BuySellDocStateEnum_To_Next_Level_Seller(BuySellDoc buySellDoc)
        //{
        //    //buySellDoc.BuySellDocStateEnum
        //    BuySellDocStateENUM buySellDocStateEnum = BuySellDocStateENUM.Unknown;
        //    switch (buySellDoc.BuySellDocStateEnum)
        //    {

        //        case BuySellDocStateENUM.RequestConfirmed:
        //            if (buySellDoc.AcceptRejectOrEmpty == ConstantsLibrary.BuySellConstants.ACCEPT)
        //            {

        //                buySellDocStateEnum = BuySellDocStateENUM.BeingPreparedForShipmentBySeller;
        //            }
        //            break;

        //        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
        //            if (buySellDoc.AcceptRejectOrEmpty == ConstantsLibrary.BuySellConstants.ACCEPT)
        //            {
        //                buySellDocStateEnum = BuySellDocStateENUM.ReadyForPickup;
        //            }
        //            break;

        //        case BuySellDocStateENUM.ReadyForPickup:
        //            if (buySellDoc.AcceptRejectOrEmpty == ConstantsLibrary.BuySellConstants.ACCEPT)
        //            {

        //                buySellDocStateEnum = BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller;
        //            }
        //            break;

        //        //these have to be handled by the courer
        //        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
        //            if (buySellDoc.AcceptRejectOrEmpty == ConstantsLibrary.BuySellConstants.ACCEPT)
        //            {

        //                buySellDocStateEnum = BuySellDocStateENUM.CourierComingToPickUp;
        //            }
        //            break;
        //        case BuySellDocStateENUM.CourierComingToPickUp:
        //            if (buySellDoc.AcceptRejectOrEmpty == ConstantsLibrary.BuySellConstants.ACCEPT)
        //            {

        //                buySellDocStateEnum = BuySellDocStateENUM.PickedUp;
        //            }
        //            break;

        //        case BuySellDocStateENUM.PickedUp:
        //            if (buySellDoc.AcceptRejectOrEmpty == ConstantsLibrary.BuySellConstants.ACCEPT)
        //            {
        //                buySellDocStateEnum = BuySellDocStateENUM.Enroute;
        //            }

        //            break;

        //        //after being delivered, item remains delivered.


        //        case BuySellDocStateENUM.Enroute:
        //            if (buySellDoc.AcceptRejectOrEmpty == ConstantsLibrary.BuySellConstants.ACCEPT)
        //            {
        //                buySellDocStateEnum = BuySellDocStateENUM.Delivered;
        //            }

        //            break;

        //        case BuySellDocStateENUM.Unknown:
        //        case BuySellDocStateENUM.InProccess:
        //        case BuySellDocStateENUM.BackOrdered:
        //        case BuySellDocStateENUM.All:
        //        case BuySellDocStateENUM.Delivered:
        //        case BuySellDocStateENUM.RequestUnconfirmed:
        //        case BuySellDocStateENUM.Rejected:
        //        case BuySellDocStateENUM.Problem:
        //        default:
        //            break;
        //    }
        //    return buySellDocStateEnum;
        //}

        //private void update_Customer_Default_BillTo_Address_If_Empty(BuySellDoc buySellDoc)
        //{
        //    buySellDoc.IsNullThrowException();
        //    buySellDoc.CustomerId.IsNullOrWhiteSpaceThrowException();
        //    buySellDoc.AddressBillToId.IsNullOrWhiteSpaceThrowException();

        //}
    }



}
