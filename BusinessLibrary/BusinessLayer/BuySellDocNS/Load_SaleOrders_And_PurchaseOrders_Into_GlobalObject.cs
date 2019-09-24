using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellDocBiz
    {


        public OrderTypes GetOrderTypesFor(string userId, BuySellDocumentTypeENUM buySellDocumentTypeEnum, DateTime fromDate, DateTime toDate)
        {
            OrderTypes orderType = new OrderTypes();

            orderType.InProccess = GetMoneyCountItemFor(userId, BuySellDocStateENUM.InProccess, buySellDocumentTypeEnum, fromDate, toDate);
            //orderType.BackOrdered = GetMoneyCountItemFor(userId, BuySellDocStateENUM.BackOrdered, buySellDocumentTypeEnum, fromDate, toDate);
            orderType.All = GetMoneyCountItemFor(userId, BuySellDocStateENUM.All, buySellDocumentTypeEnum, fromDate, toDate);

            orderType.RequestUnconfirmed = GetMoneyCountItemFor(userId, BuySellDocStateENUM.RequestUnconfirmed, buySellDocumentTypeEnum, fromDate, toDate);
            orderType.RequestConfirmed = GetMoneyCountItemFor(userId, BuySellDocStateENUM.RequestConfirmed, buySellDocumentTypeEnum, fromDate, toDate);

            orderType.BeingPreparedForShipmentBySeller = GetMoneyCountItemFor(userId, BuySellDocStateENUM.BeingPreparedForShipmentBySeller, buySellDocumentTypeEnum, fromDate, toDate);

            orderType.ReadyForPickup = GetMoneyCountItemFor(userId, BuySellDocStateENUM.ReadyForPickup, buySellDocumentTypeEnum, fromDate, toDate);



            orderType.CourierAcceptedByBuyerAndSeller = GetMoneyCountItemFor(userId, BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller, buySellDocumentTypeEnum, fromDate, toDate);
            //todo
            //orderType.Cou= GetMoneyCountItemFor(userId, BuySellDocStateENUM.CourierComingToPickUp, buySellDocumentTypeEnum, fromDate, toDate);
            orderType.CourierComingToPickUp = GetMoneyCountItemFor(userId, BuySellDocStateENUM.CourierComingToPickUp, buySellDocumentTypeEnum, fromDate, toDate);
            orderType.PickedUp = GetMoneyCountItemFor(userId, BuySellDocStateENUM.PickedUp, buySellDocumentTypeEnum, fromDate, toDate);
            orderType.Enroute = GetMoneyCountItemFor(userId, BuySellDocStateENUM.Enroute, buySellDocumentTypeEnum, fromDate, toDate);
            orderType.Delivered = GetMoneyCountItemFor(userId, BuySellDocStateENUM.Delivered, buySellDocumentTypeEnum, fromDate, toDate);
            orderType.Rejected = GetMoneyCountItemFor(userId, BuySellDocStateENUM.Rejected, buySellDocumentTypeEnum, fromDate, toDate);
            orderType.Problem = GetMoneyCountItemFor(userId, BuySellDocStateENUM.Problem, buySellDocumentTypeEnum, fromDate, toDate);

            return orderType;
        }


        MoneyCountItemClass GetMoneyCountItemFor(string userId, BuySellDocStateENUM buySellDocStateEnum, BuySellDocumentTypeENUM buySellDocumentTypeEnum, DateTime fromDate, DateTime toDate)
        {
            List<BuySellDoc> alldocuments = getBuySellDocs_For(userId, buySellDocStateEnum, buySellDocumentTypeEnum, fromDate, toDate);
            
            MoneyCountItemClass mcic = new MoneyCountItemClass();
            mcic.MoneyAmount = get_Money_For(alldocuments, buySellDocumentTypeEnum);
            mcic.Count = get_Count_For(alldocuments, buySellDocumentTypeEnum, buySellDocStateEnum);

            if (userId.IsNullOrWhiteSpace())
            {
                mcic.MenuName = BuySellDoc.GetMenuItem_Admin(buySellDocStateEnum, buySellDocumentTypeEnum, mcic.MoneyAmount_Formatted, mcic.Count_Formatted);
                mcic.MenuToolTip = BuySellDoc.GetMenuToolTip_Admin(buySellDocStateEnum, buySellDocumentTypeEnum, mcic.MoneyAmount_Formatted, mcic.Count_Formatted);

            }
            else
            {
                //this is for the individual user
                mcic.MenuName = BuySellDoc.GetMenuItem(buySellDocStateEnum, buySellDocumentTypeEnum, mcic.MoneyAmount_Formatted, mcic.Count_Formatted);
                mcic.MenuToolTip = BuySellDoc.GetMenuToolTip(buySellDocStateEnum, buySellDocumentTypeEnum, mcic.MoneyAmount_Formatted, mcic.Count_Formatted);
            }
            return mcic;
        }


        /// <summary>
        /// This gets a list from getAllDocuments_For.
        /// </summary>
        /// <param name="orderListDateDelimitedByDocumentTypeAndState"></param>
        /// <returns></returns>
        decimal get_Money_For(List<BuySellDoc> orderListDateDelimitedByDocumentTypeAndState, BuySellDocumentTypeENUM buySellDocumentTypeEnum)
        {

            //get all purchase orders for Owner

            //add up the purchase amoung
            if (orderListDateDelimitedByDocumentTypeAndState.IsNullOrEmpty())
                return 0;

            //The deliveryman is only counted once selected.
            if (buySellDocumentTypeEnum == BuySellDocumentTypeENUM.Delivery)
            {
                Deliveryman deliveryman = DeliverymanBiz.GetPlayerFor(UserId);
                if (deliveryman.IsNull())
                    return 0;
            }

            decimal ttlOrdered = orderListDateDelimitedByDocumentTypeAndState.Sum(x => x.TotalOrdered);
            //foreach (var item in orderListDateDelimitedByDocumentTypeAndState)
            //{
            //    ttlOrdered += item.TotalOrdered;
            //}

            return ttlOrdered;
        }



        /// <summary>
        /// This gets a list from getAllDocuments_For.
        /// </summary>
        /// <param name="orderListDateDelimitedByDocumentTypeAndState"></param>
        /// <returns></returns>
        long get_Count_For(List<BuySellDoc> orderListDateDelimitedByDocumentTypeAndState, BuySellDocumentTypeENUM buySellDocumentTypeEnum, BuySellDocStateENUM buySellDocStateEnum)
        {

            //get all purchase orders for Owner

            //add up the purchase amoung
            if (orderListDateDelimitedByDocumentTypeAndState.IsNullOrEmpty())
                return 0;


            IQueryable<BuySellDoc> iq_orderListDateDelimitedByDocumentTypeAndState = orderListDateDelimitedByDocumentTypeAndState.AsQueryable<BuySellDoc>();

            IQueryable<BuySellDoc> orderListDuplicate = BuySellDoc.IQuerable_Orders_For(
                buySellDocStateEnum,
                iq_orderListDateDelimitedByDocumentTypeAndState);

            long count = 0;
            //if (buySellDocumentTypeEnum == BuySellDocumentTypeENUM.Delivery)
            //{
            //    if (buySellDocStateEnum == BuySellDocStateENUM.ReadyForPickup || buySellDocStateEnum == BuySellDocStateENUM.All)
            //    { }
            //    else
            //    {
            //        Deliveryman deliveryman = DeliverymanBiz.GetPlayerFor(UserId);

            //        if (deliveryman.IsNull())
            //            return 0;

            //        count = orderListDuplicate
            //            .Where(x => x.DeliverymanId == deliveryman.Id)
            //            .Count();

            //        return count;
            //    }
            //}
            //for the deliveryman, it should return all items as per its query
            count = orderListDuplicate.Count();
            return count;
        }


        //public BuySellStatementModel GetDeliveryOrders(string userId, string id)
        //{
        //    userId.IsNullOrWhiteSpaceThrowArgumentException("You are not logged in.");

        //    List<BuySellDoc> lstbuySellDocs = new List<BuySellDoc>(); ;

        //    BuySellDoc buySellDoc = Find(id);
        //    buySellDoc.IsNullThrowException();

        //    lstbuySellDocs.Add(buySellDoc);

        //    decimal customerBalanceRefundable = 0;
        //    decimal customerBalanceNonRefundable = 0;

        //    BuySellStatementModel buySellStatementModel = new BuySellStatementModel(lstbuySellDocs, DateTime.MinValue, DateTime.MaxValue, BuySellDocumentTypeENUM.Delivery, false, customerBalanceRefundable, customerBalanceNonRefundable, BuySellDocStateENUM.ReadyForPickup);

        //    return buySellStatementModel;

        //}


        /// <summary>
        /// This gets the order list of Sale or Purchase documents date delimited.
        /// This also figures out if it is a sale or a purchase
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="buySellDocumentTypeEnum"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>

        public BuySellStatementModel GetBuySellStatementModel(string userId, DateTime fromDate, DateTime toDate, bool isAdmin, BuySellDocumentTypeENUM buySellDocumentTypeEnum, BuySellDocStateENUM buySellDocStateEnum)
        {
            //userId.IsNullOrWhiteSpaceThrowArgumentException("You are not logged in.");


            List<BuySellDoc> buySellDocs = getBuySellDocs_For(userId, buySellDocStateEnum, buySellDocumentTypeEnum, fromDate, toDate).ToList();

            decimal customerBalanceRefundable = 0, customerBalanceNonRefundable = 0;
            Deliveryman deliveryman = DeliverymanBiz.GetPlayerFor(userId);



            BuySellStatementModel buySellStatementModel = new BuySellStatementModel(buySellDocs, fromDate, toDate, buySellDocumentTypeEnum, isAdmin, customerBalanceRefundable, customerBalanceNonRefundable, deliveryman, buySellDocStateEnum);

            return buySellStatementModel;

        }


        /// <summary>
        /// This is the one that returns a list of a sale or purchase documents for a specific state within a date period. This is the one that joins the operations.
        /// We will use this data to get the money amount and counts
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="buySellDocStateEnum"></param>
        /// <param name="buySellDocumentTypeEnum"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        List<BuySellDoc> getBuySellDocs_For(string userId, BuySellDocStateENUM buySellDocStateEnum, BuySellDocumentTypeENUM buySellDocumentTypeEnum, DateTime fromDate, DateTime toDate)
        {

            IQueryable<BuySellDoc> allDocsWithinDate = FindAll().Where(x => x.MetaData.Created.Date >= fromDate && x.MetaData.Created.Date <= toDate);
            List<BuySellDoc> allDocsWithinDateDEBUG = allDocsWithinDate.ToList();

            IQueryable<BuySellDoc> allDocsOfDocumentType = getByDocumentType_For(userId, allDocsWithinDate, buySellDocumentTypeEnum);
            List<BuySellDoc> allDocsOfDocumentTypeDEBUG = getByDocumentType_For(userId, allDocsWithinDate, buySellDocumentTypeEnum).ToList();

            IQueryable<BuySellDoc> IQueryable_allDocsOfDocState = BuySellDoc.IQuerable_Orders_For(buySellDocStateEnum, allDocsOfDocumentType);

            List<BuySellDoc> allFilteredDocs = IQueryable_allDocsOfDocState.ToList();

            //fill in the Complex Addresses
            if (!allFilteredDocs.IsNullOrEmpty())
            {
                foreach (BuySellDoc buysellDoc in allFilteredDocs)
                {
                    loadAddressShipTo(buysellDoc);
                    loadAddressBillTo(buysellDoc);
                    loadAddressShipFrom(buysellDoc);
                    buysellDoc.BuySellDocumentTypeEnum = buySellDocumentTypeEnum;
                }
            }

            return allFilteredDocs;
        }



        private void loadAddressShipFrom(BuySellDoc buysellDoc)
        {
            if (buysellDoc.AddressShipFrom.IsNull())
            {
                if (buysellDoc.AddressShipFromId.IsNullOrWhiteSpace())
                {
                    //do nothing
                }
                else
                {
                    buysellDoc.AddressShipFrom = AddressBiz.Find(buysellDoc.AddressShipFromId);
                    buysellDoc.AddressShipFromComplex = buysellDoc.AddressShipFrom.ToAddressComplex();
                }
            }
            else
            {
                buysellDoc.AddressShipFromComplex = buysellDoc.AddressShipFrom.ToAddressComplex();

            }
        }


        private void loadAddressBillTo(BuySellDoc buysellDoc)
        {
            if (buysellDoc.AddressBillTo.IsNull())
            {
                if (buysellDoc.AddressBillToId.IsNullOrWhiteSpace())
                {
                    //do nothing
                }
                else
                {
                    buysellDoc.AddressBillTo = AddressBiz.Find(buysellDoc.AddressBillToId);
                    buysellDoc.AddressBillToComplex = buysellDoc.AddressBillTo.ToAddressComplex();
                }

            }
            else
            {
                buysellDoc.AddressBillToComplex = buysellDoc.AddressBillTo.ToAddressComplex();

            }
        }


        private void loadAddressShipTo(BuySellDoc buysellDoc)
        {
            if (buysellDoc.AddressShipTo.IsNull())
            {
                if (buysellDoc.AddressShipToId.IsNullOrWhiteSpace())
                {
                    //do nothing
                }
                else
                {
                    buysellDoc.AddressShipTo = AddressBiz.Find(buysellDoc.AddressShipToId);
                    buysellDoc.AddressShipToComplex = buysellDoc.AddressShipTo.ToAddressComplex();
                }
            }
            else
            {
                buysellDoc.AddressShipToComplex = buysellDoc.AddressShipTo.ToAddressComplex();

            }
        }

        IQueryable<BuySellDoc> getByDocumentType_For(string userId, IQueryable<BuySellDoc> iq_allOrders, BuySellDocumentTypeENUM buySellDocumentTypeEnum)
        {
            //userId.IsNullOrWhiteSpaceThrowArgumentException("You are not logged in.");


            switch (buySellDocumentTypeEnum)
            {

                case BuySellDocumentTypeENUM.Sale:
                    {

                        try
                        {
                            //all orders are returned because this is the admin
                            //if (userId.IsNullOrWhiteSpace())
                            //    return iq_allOrders;
                            if(userId.IsNullOrWhiteSpace())
                                return BuySellDoc.IQueryable_GetSaleDocs(iq_allOrders, "");

                            Owner owner = OwnerBiz.GetPlayerFor(userId);
                            owner.IsNullThrowException("No Owner");
                            return BuySellDoc.IQueryable_GetSaleDocs(iq_allOrders, owner.Id);

                        }
                        catch (Exception e)
                        {

                            ErrorsGlobal.Add("Problem getting sales.", MethodBase.GetCurrentMethod(), e);
                            throw new Exception(ErrorsGlobal.ToString());
                        }
                    }

                case BuySellDocumentTypeENUM.Purchase:
                    {
                        try
                        {
                            //all orders are returned because this is the admin
                            //if (userId.IsNullOrWhiteSpace())
                            //    return iq_allOrders;
                            if(userId.IsNullOrWhiteSpace())
                                return BuySellDoc.IQueryable_GetPurchaseDocs(iq_allOrders, "");
                            
                            Customer customer = CustomerBiz.GetPlayerFor(userId);
                            customer.IsNullThrowException("Customer");
                            return BuySellDoc.IQueryable_GetPurchaseDocs(iq_allOrders, customer.Id);

                        }
                        catch (Exception e)
                        {

                            ErrorsGlobal.Add("Problem getting purchases", MethodBase.GetCurrentMethod(), e);
                            throw new Exception(ErrorsGlobal.ToString());
                        }

                    }

                case BuySellDocumentTypeENUM.Delivery:
                    {
                        try
                        {
                            //all orders are returned because this is the admin
                            if (userId.IsNullOrWhiteSpace())
                                return BuySellDoc.IQueryable_GetDeliveryDocs(iq_allOrders, "");


                            Deliveryman deliveryMan = DeliverymanBiz.GetPlayerFor(userId);
                            deliveryMan.IsNullThrowException("deliveryMan");

                            return BuySellDoc.IQueryable_GetDeliveryDocs(iq_allOrders, deliveryMan.Id);

                        }
                        catch (Exception e)
                        {

                            ErrorsGlobal.Add("Problem getting purchases", MethodBase.GetCurrentMethod(), e);
                            throw new Exception(ErrorsGlobal.ToString());
                        }
                    }

                case BuySellDocumentTypeENUM.Salesman:
                    {
                        try
                        {
                            if (userId.IsNullOrWhiteSpace())
                                return BuySellDoc.IQueryable_GetSalesmanDocs(iq_allOrders, "");

                            Salesman salesman = SalesmanBiz.GetPlayerFor(userId);
                            salesman.IsNullThrowException("Salesman");

                            return BuySellDoc.IQueryable_GetSalesmanDocs(iq_allOrders, salesman.Id);

                        }
                        catch (Exception e)
                        {

                            ErrorsGlobal.Add("Problem getting purchases", MethodBase.GetCurrentMethod(), e);
                            throw new Exception(ErrorsGlobal.ToString());
                        }
                    }
                case BuySellDocumentTypeENUM.Unknown:
                default:
                    throw new Exception("Unknown document type");
            }




        }



        #region General Methods



        private Customer getCustomerFor(string userId)
        {
            Customer customer = CustomerBiz.GetPlayerFor(userId);
            customer.IsNullThrowException("customer");
            return customer;
        }


        bool IsAdmin { get { return UserBiz.IsAdmin(UserId); } }


        #endregion
    }
}
