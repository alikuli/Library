using AliKuli.Extentions;
using AliKuli.ToolsNS;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS;
//using ModelsClassLibrary.ModelsNS.BuySellDocNS.NonReturnableNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FreightOffersTrxNS;
using ModelsClassLibrary.ModelsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
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
            BuySellDoc bsd = BuySellDoc.UnBox(parm.Entity);
            GlobalObject globalObject = parm.GlobalObject;
            globalObject.IsNullThrowException();

            switch (bsd.BuySellDocumentTypeEnum)
            {

                case BuySellDocumentTypeENUM.Delivery:
                    switch (bsd.BuySellDocStateEnum)
                    {

                        case BuySellDocStateENUM.ReadyForPickup:
                            throwExceptionIfVehicalTypeNotAdded(bsd);
                            not_Within_Shipping_Window_Throw_Exception(bsd);
                            //todo check this the freight variable may be wrong
                            IsEnoughBalanceForUser(bsd, parm, bsd.InsuranceRequired + bsd.FreightOfferDecimal, 0);
                            deliveryman_Makes_Bid_To_Pickup(bsd);
                            break;

                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                            //todo check this the freight variable may be wrong
                            IsEnoughBalanceForUser(bsd, parm, bsd.InsuranceRequired + bsd.FreightOfferDecimal, 0);
                            not_Within_Shipping_Window_Throw_Exception(bsd);
                            deliveryman_Accepts_Request_To_Pickup(bsd);
                            break;


                        case BuySellDocStateENUM.CourierComingToPickUp:
                            not_Within_Shipping_Window_Throw_Exception(bsd);
                            cancel_Pickup_of(bsd);
                            //todo check this the freight variable may be wrong
                            IsEnoughBalanceForUser(bsd, parm, bsd.InsuranceRequired + bsd.FreightOfferDecimal, 0);
                            break;

                        case BuySellDocStateENUM.PickedUp:
                            break;


                        case BuySellDocStateENUM.Enroute:
                            deliveryman_Delivers_Product(bsd);
                            canceled_Becomes_A_Problem(bsd);
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
                        case BuySellDocStateENUM.OptedOutOfSystem:
                        default:
                            break;
                    }
                    break;


                case BuySellDocumentTypeENUM.Sale:
                    switch (bsd.BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.Unknown:
                        case BuySellDocStateENUM.InProccess:
                        case BuySellDocStateENUM.BackOrdered:
                        case BuySellDocStateENUM.All:
                        case BuySellDocStateENUM.RequestUnconfirmed:
                            not_Within_Shipping_Window_Throw_Exception(bsd);
                            break;

                        case BuySellDocStateENUM.RequestConfirmed:
                            not_Within_Shipping_Window_Throw_Exception(bsd);
                            //set_System_Opt_Out_of_System_Payment(bsd, globalObject);
                            seller_Accepts_Buyers_RequestConfirmed(bsd, parm);

                            break;

                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                            not_Within_Shipping_Window_Throw_Exception(bsd);
                            seller_Has_Completed_Preparing_Parcel_For_Shipment(bsd);
                            break;

                        case BuySellDocStateENUM.ReadyForPickup:
                            not_Within_Shipping_Window_Throw_Exception(bsd);
                            seller_Selects_Deliveryman_For_Shipment(bsd);
                            seller_Cancels_Deliveryman(bsd);
                            seller_Cancels_And_Goes_Back_To_BeingPreparedForShipmentBySeller(bsd);
                            break;

                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                            not_Within_Shipping_Window_Throw_Exception(bsd);
                            set_Payment_For_Deliveryman(bsd, parm);
                            set_Commissions_For_DeliverySalesman(bsd, parm);
                            cancel_Pickup_of(bsd);

                            break;

                        case BuySellDocStateENUM.CourierComingToPickUp:
                            not_Within_Shipping_Window_Throw_Exception(bsd);

                            deliveryman_PicksUp_From_Seller(bsd);
                            cancel_Pickup_of(bsd);

                            break;

                        case BuySellDocStateENUM.PickedUp:
                        case BuySellDocStateENUM.Enroute:
                            canceled_Becomes_A_Problem(bsd);
                            break;

                        case BuySellDocStateENUM.Delivered:
                            break;

                        case BuySellDocStateENUM.Rejected:
                            //this is during BuySellController.CancelOrder
                            break;

                        case BuySellDocStateENUM.OptedOutOfSystem:
                            //gets reset in GetPenalty
                            break;

                        case BuySellDocStateENUM.Problem:
                            break;
                        default:
                            break;
                    }
                    break;


                case BuySellDocumentTypeENUM.Purchase:
                    switch (bsd.BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.Unknown:
                        case BuySellDocStateENUM.InProccess:
                        case BuySellDocStateENUM.BackOrdered:
                        case BuySellDocStateENUM.All:
                            break;

                        case BuySellDocStateENUM.RequestUnconfirmed:
                            buyer_Confirms_His_PurchaseOrder(bsd, parm);
                            //calculate_CustomerSalesman_Commission(buySellDoc);
                            //calculate_OwnerSalesman_Commission(buySellDoc);
                            //calculate_System_Commission(buySellDoc);

                            break;

                        case BuySellDocStateENUM.RequestConfirmed:
                            not_Within_Shipping_Window_Throw_Message(bsd);

                            break;

                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                            break;

                        case BuySellDocStateENUM.ReadyForPickup:
                            Buyer_Selects_Courier_For_Shipment(bsd);
                            break;

                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                            break;
                        case BuySellDocStateENUM.CourierComingToPickUp:
                            break;
                        case BuySellDocStateENUM.PickedUp:
                            break;
                        case BuySellDocStateENUM.Enroute:
                            canceled_Becomes_A_Problem(bsd);
                            break;

                        case BuySellDocStateENUM.Delivered:
                            break;

                        case BuySellDocStateENUM.Rejected:
                            Buyer_Converts_BuySell_To_RequestUnconfirmed(bsd);
                            break;

                        case BuySellDocStateENUM.OptedOutOfSystem:
                        //gets reset in GetPenalty
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
            ThrowExceptionIfBillToIdOrShipToIdIsEmptyOrNull(bsd);
            not_Within_Shipping_Window_Throw_Message(bsd);
            order_Canceled(bsd);
            order_Rejected(bsd);
            set_Commissions(bsd, parm);
            if_Reject_reset_Commissions_And_Fields(bsd);
            CreateBuySellHistory(bsd);

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



        private void order_Canceled(BuySellDoc bsd)
        {
            if (bsd.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Cancel)
            {
                if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.Delivered)
                {
                    if (bsd.ShopId.IsNullOrWhiteSpace())
                    {
                        bsd.BuySellDocStateEnum = BuySellDocStateENUM.Problem;
                        bsd.Problem.SetToTodaysDate(UserName, UserId);
                    }
                    else
                    {
                        ifShopPurchaseThenDeleteShopAndMarkReject(bsd);
                    }
                }
                else
                {
                    clearAllStatus(bsd);
                    bsd.BuySellDocStateEnum = BuySellDocStateENUM.RequestUnconfirmed;

                    //reset_Commissions_And_Fields(bsd);
                }
            }
        }

        private void ifShopPurchaseThenDeleteShopAndMarkReject(BuySellDoc bsd)
        {
            //is this a shop BuySellDoc
            if (bsd.ShopId.IsNullOrWhiteSpace())
                return;

            //locate the shop in products and make it deleted
            Product shop = ProductBiz.Find(bsd.ShopId);
            shop.IsNullThrowException();
            shop.MetaData.IsDeleted = true;
            shop.MetaData.Deleted.SetToTodaysDate(UserName, UserId);
            clearAllStatus(bsd);
            bsd.BuySellDocStateEnum = BuySellDocStateENUM.Rejected;
            bsd.Rejected.SetToTodaysDate(UserName, UserId);

            ProductBiz.Update(shop);
        }

        private void clearAllStatus(BuySellDoc bsd)
        {
            bsd.RequestUnconfirmed.SetToTodaysDate(UserName, UserId);
            bsd.RequestConfirmed.Clear();
            bsd.OptedOutOfSystem.Clear();
            bsd.BeingPreparedForShipmentBySeller.Clear();
            bsd.ReadyForPickup.Clear();
            bsd.CourierAcceptedByBuyerAndSeller.Clear();
            bsd.CourierComingToPickUp.Clear();
            bsd.Enroute.Clear();
            bsd.Delivered.Clear();
        }


        private void order_Rejected(BuySellDoc buySellDoc)
        {
            if (buySellDoc.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Reject)
            {
                buySellDoc.BuySellDocStateEnum = BuySellDocStateENUM.Rejected;
                if_Reject_reset_Commissions_And_Fields(buySellDoc);

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
                if (bsd.IsShop)
                {
                    bsd.BuySellDocStateEnum = BuySellDocStateENUM.Delivered;
                    bsd.Delivered.SetToTodaysDate(UserName, UserId);

                }
                else
                {
                    check_BillTo_Address_Is_filled(bsd);
                    check_ShipTo_Address_Is_filled(bsd);
                    check_BillFrom_Address_Is_filled(bsd);
                    update_Customer_Person_Bill_To_Default_Address_If_empty(bsd);

                    bsd.BuySellDocStateEnum = BuySellDocStateENUM.RequestConfirmed;
                    bsd.RequestConfirmed.SetToTodaysDate(UserName, UserId);
                }

                set_Payment_For_Product(bsd, parm);
            }
        }




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


        private void seller_Accepts_Buyers_RequestConfirmed(BuySellDoc bsd, ControllerCreateEditParameter parm)
        {
            if (bsd.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Accept)
            {
                //make sure the seller has given some Expected delivery Date
                if (bsd.ExpectedDeliveryDate == DateTime.MinValue || bsd.ExpectedDeliveryDate == DateTime.MaxValue)
                {
                    throw new Exception("You must fill in the expected date the customer will receive the product.");
                }
                if (bsd.OptedOutOfSystem.IsSelected)
                {
                    bsd.BuySellDocStateEnum = BuySellDocStateENUM.OptedOutOfSystem;
                    bsd.OptedOutOfSystem.SetToTodaysDate(UserName, UserId);
                }
                else
                {
                    bsd.BuySellDocStateEnum = BuySellDocStateENUM.BeingPreparedForShipmentBySeller;
                    bsd.BeingPreparedForShipmentBySeller.SetToTodaysDate(UserName, UserId);
                }
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
        /// <param name="bsd"></param>
        /// <param name="globalObject"></param>
        private void check_ThereAreFundsAvailable(BuySellDoc bsd, GlobalObject globalObject)
        {
            if (bsd.OptedOutOfSystem.IsSelected)
                return;

            globalObject.IsNullThrowException();
            decimal reqrdAmount = bsd.TotalInvoice_Refundable + 1000m;
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

        private static void ThrowExceptionIfBillToIdOrShipToIdIsEmptyOrNull(BuySellDoc bsd)
        {
            if (bsd.IsShop)
                return;

            if (bsd.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Accept)
            {
                bsd.AddressBillToId.IsNullOrWhiteSpaceThrowException("You must fill in the bill to address");
                bsd.AddressShipToId.IsNullOrWhiteSpaceThrowException("You must fill in the ship to address");
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
