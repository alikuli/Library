using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.MoneyAndCountClass;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellDocBiz
    {

        /// <summary>
        /// This is the entry point.
        /// This loads up all the account for the user or admin.  
        /// if User is the Customer,then it is a purchase;
        /// if user is the Owner/Seller, then it is a sale;
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="buySellDocumentTypeEnum"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        public MoneyItemParent GetMoneyItemParent(string userId, bool isAdmin, DateTime fromDate, DateTime toDate)
        {
            MoneyItemParent mip = new MoneyItemParent();

            //if we let this through, thr program will think we have an admin.
            //anyway, without userId we fail.
            if (userId.IsNullOrEmpty())
                return mip;

            mip.Sale = getOrderTypesFor(userId, BuySellDocumentTypeENUM.Sale, fromDate, toDate);
            mip.Purchase = getOrderTypesFor(userId, BuySellDocumentTypeENUM.Purchase, fromDate, toDate);
            if (isAdmin)
            {
                mip.Sale_Admin = getOrderTypesFor("", BuySellDocumentTypeENUM.Sale, fromDate, toDate);
                mip.Purchase_Admin = getOrderTypesFor("", BuySellDocumentTypeENUM.Purchase, fromDate, toDate);

            }
            return mip;
        }


        OrderTypes getOrderTypesFor(string userId, BuySellDocumentTypeENUM buySellDocumentTypeEnum, DateTime fromDate, DateTime toDate)
        {
            OrderTypes orderType = new OrderTypes();

            orderType.InProccess = GetMoneyCountItemFor(userId, BuySellDocStateENUM.InProccess, buySellDocumentTypeEnum, fromDate, toDate);
            //orderType.BackOrdered = GetMoneyCountItemFor(userId, BuySellDocStateENUM.BackOrdered, buySellDocumentTypeEnum, fromDate, toDate);
            orderType.All = GetMoneyCountItemFor(userId, BuySellDocStateENUM.All, buySellDocumentTypeEnum, fromDate, toDate);

            orderType.RequestUnconfirmed = GetMoneyCountItemFor(userId, BuySellDocStateENUM.RequestUnconfirmed, buySellDocumentTypeEnum, fromDate, toDate);
            orderType.RequestConfirmed = GetMoneyCountItemFor(userId, BuySellDocStateENUM.RequestConfirmed, buySellDocumentTypeEnum, fromDate, toDate);

            orderType.ConfirmedBySeller = GetMoneyCountItemFor(userId, BuySellDocStateENUM.ConfirmedBySeller, buySellDocumentTypeEnum, fromDate, toDate);
            orderType.ReadyForShipment = GetMoneyCountItemFor(userId, BuySellDocStateENUM.ReadyForShipment, buySellDocumentTypeEnum, fromDate, toDate);
            orderType.ConfirmedByCourier = GetMoneyCountItemFor(userId, BuySellDocStateENUM.ConfirmedByCourier, buySellDocumentTypeEnum, fromDate, toDate);
            orderType.PickedUp = GetMoneyCountItemFor(userId, BuySellDocStateENUM.PickedUp, buySellDocumentTypeEnum, fromDate, toDate);
            orderType.Delivered = GetMoneyCountItemFor(userId, BuySellDocStateENUM.Delivered, buySellDocumentTypeEnum, fromDate, toDate);
            orderType.Rejected = GetMoneyCountItemFor(userId, BuySellDocStateENUM.Rejected, buySellDocumentTypeEnum, fromDate, toDate);
            orderType.Problem = GetMoneyCountItemFor(userId, BuySellDocStateENUM.Problem, buySellDocumentTypeEnum, fromDate, toDate);

            return orderType;
        }


        MoneyCountItemClass GetMoneyCountItemFor(string userId, BuySellDocStateENUM buySellDocStateEnum, BuySellDocumentTypeENUM buySellDocumentTypeEnum, DateTime fromDate, DateTime toDate)
        {
            List<BuySellDoc> alldocuments = getAllDocuments_For(userId, buySellDocStateEnum, buySellDocumentTypeEnum, fromDate, toDate);
            MoneyCountItemClass mcic = new MoneyCountItemClass();
            mcic.MoneyAmount = get_Money_For(alldocuments);
            mcic.Count = get_Count_For(alldocuments);
            mcic.MenuName = getMenuItem(buySellDocStateEnum, buySellDocumentTypeEnum, mcic.MoneyAmount_Formatted, mcic.Count_Formatted);
            mcic.MenuToolTip = getMenuToolTip(buySellDocStateEnum, buySellDocumentTypeEnum, mcic.MoneyAmount_Formatted, mcic.Count_Formatted);
            return mcic;
        }

        string getMenuItem(BuySellDocStateENUM buySellDocStateEnum, BuySellDocumentTypeENUM buySellDocumentTypeEnum, string money, string count)
        {

            string content = "Error in OperOrdersForPerson -Copy.getMenuItem";
            string str = content;

            switch (buySellDocumentTypeEnum)
            {
                case BuySellDocumentTypeENUM.Sale:
                    {

                        switch (buySellDocStateEnum)
                        {
                            case BuySellDocStateENUM.New:
                            case BuySellDocStateENUM.Closed:
                            case BuySellDocStateENUM.BackOrdered:
                            case BuySellDocStateENUM.Quotation:
                            case BuySellDocStateENUM.Credit:
                                throw new NotImplementedException();

                            case BuySellDocStateENUM.Canceled:
                                break;

                            case BuySellDocStateENUM.InProccess:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.InProccess.MenuItem"];
                                break;


                            case BuySellDocStateENUM.All:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.Total.MenuItem"];
                                break;


                            case BuySellDocStateENUM.RequestUnconfirmed:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.RequestUnconfirmed.MenuItem"];
                                break;

                            case BuySellDocStateENUM.RequestConfirmed:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.RequestConfirmed.MenuItem"];
                                break;


                            case BuySellDocStateENUM.ConfirmedBySeller:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.ConfirmedBySeller.MenuItem"];
                                break;


                            case BuySellDocStateENUM.ReadyForShipment:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.ReadyForShipment.MenuItem"];
                                break;

                            case BuySellDocStateENUM.ConfirmedByCourier:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.ConfirmedByCourier.MenuItem"];
                                break;

                            case BuySellDocStateENUM.PickedUp:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.PickedUp.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Delivered:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.Delivered.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Rejected:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.Rejected.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Problem:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.Problem.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Unknown:
                            default:
                                break;
                        }
                        str = string.Format(content, money, count);
                        return str;

                    }
                case BuySellDocumentTypeENUM.Purchase:
                    {
                        switch (buySellDocStateEnum)
                        {
                            case BuySellDocStateENUM.New:
                            case BuySellDocStateENUM.Closed:
                            case BuySellDocStateENUM.BackOrdered:
                            case BuySellDocStateENUM.Quotation:
                            case BuySellDocStateENUM.Credit:
                                throw new NotImplementedException();

                            case BuySellDocStateENUM.Canceled:
                                break;

                            case BuySellDocStateENUM.InProccess:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.InProccess.MenuItem"];
                                break;


                            case BuySellDocStateENUM.All:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Total.MenuItem"];
                                break;


                            case BuySellDocStateENUM.RequestUnconfirmed:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.RequestUnconfirmed.MenuItem"];
                                break;

                            case BuySellDocStateENUM.RequestConfirmed:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.RequestConfirmed.MenuItem"];
                                break;


                            case BuySellDocStateENUM.ConfirmedBySeller:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.ConfirmedBySeller.MenuItem"];
                                break;


                            case BuySellDocStateENUM.ReadyForShipment:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.ReadyForShipment.MenuItem"];
                                break;

                            case BuySellDocStateENUM.ConfirmedByCourier:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.ConfirmedByCourier.MenuItem"];
                                break;

                            case BuySellDocStateENUM.PickedUp:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.PickedUp.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Delivered:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Delivered.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Rejected:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Rejected.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Problem:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Problem.MenuItem"];
                                break;

                            case BuySellDocStateENUM.Unknown:
                            default:
                                break;

                        }
                        str = string.Format(content, money, count);
                        return str;
                    }
                case BuySellDocumentTypeENUM.Unknown:
                default:
                    throw new Exception("Unknown Document type");
            }

        }


        string getMenuToolTip(BuySellDocStateENUM buySellDocStateEnum, BuySellDocumentTypeENUM buySellDocumentTypeEnum, string money, string count)
        {
            string content = "Error in OperOrdersForPerson -Copy.getMenuToolTip";
            string str = content;

            switch (buySellDocumentTypeEnum)
            {
                case BuySellDocumentTypeENUM.Unknown:
                    break;
                case BuySellDocumentTypeENUM.Sale:
                    {

                        switch (buySellDocStateEnum)
                        {
                            case BuySellDocStateENUM.New:
                            case BuySellDocStateENUM.Closed:
                            case BuySellDocStateENUM.BackOrdered:
                            case BuySellDocStateENUM.Quotation:
                            case BuySellDocStateENUM.Credit:
                                throw new NotImplementedException();

                            case BuySellDocStateENUM.Canceled:
                                break;

                            case BuySellDocStateENUM.InProccess:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.InProccess.ToolTip"];
                                break;


                            case BuySellDocStateENUM.All:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.Total.ToolTip"];
                                break;


                            case BuySellDocStateENUM.RequestUnconfirmed:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.RequestUnconfirmed.ToolTip"];
                                break;

                            case BuySellDocStateENUM.RequestConfirmed:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.RequestConfirmed.ToolTip"];
                                break;


                            case BuySellDocStateENUM.ConfirmedBySeller:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.ConfirmedBySeller.ToolTip"];
                                break;


                            case BuySellDocStateENUM.ReadyForShipment:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.ReadyForShipment.ToolTip"];
                                break;

                            case BuySellDocStateENUM.ConfirmedByCourier:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.ConfirmedByCourier.ToolTip"];
                                break;

                            case BuySellDocStateENUM.PickedUp:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.PickedUp.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Delivered:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.Delivered.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Rejected:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.Rejected.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Problem:
                                content = ConfigurationManager.AppSettings["menu.person.SaleOrders.Problem.ToolTip"];
                                break;


                            case BuySellDocStateENUM.Unknown:
                            default:
                                break;
                        }

                        str = string.Format(content, count, money);
                        return str;

                    }
                case BuySellDocumentTypeENUM.Purchase:
                    {
                        switch (buySellDocStateEnum)
                        {
                            case BuySellDocStateENUM.New:
                            case BuySellDocStateENUM.Closed:
                            case BuySellDocStateENUM.BackOrdered:
                            case BuySellDocStateENUM.Quotation:
                            case BuySellDocStateENUM.Credit:
                                throw new NotImplementedException();

                            case BuySellDocStateENUM.Canceled:
                                break;

                            case BuySellDocStateENUM.InProccess:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.InProccess.ToolTip"];
                                break;


                            case BuySellDocStateENUM.All:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Total.ToolTip"];
                                break;


                            case BuySellDocStateENUM.RequestUnconfirmed:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.RequestUnconfirmed.ToolTip"];
                                break;


                            case BuySellDocStateENUM.ConfirmedBySeller:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.ConfirmedBySeller.ToolTip"];
                                break;


                            case BuySellDocStateENUM.ReadyForShipment:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.ReadyForShipment.ToolTip"];
                                break;

                            case BuySellDocStateENUM.ConfirmedByCourier:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.ConfirmedByCourier.ToolTip"];
                                break;

                            case BuySellDocStateENUM.PickedUp:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.PickedUp.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Delivered:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Delivered.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Rejected:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Rejected.ToolTip"];
                                break;

                            case BuySellDocStateENUM.Problem:
                                content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Problem.ToolTip"];
                                break;


                            case BuySellDocStateENUM.Unknown:
                            default:
                                break;

                        }
                        break;
                    }
                default:
                    break;
            }

            string docState = buySellDocStateEnum.ToString().ToTitleCase();
            string docType = buySellDocumentTypeEnum.ToString().ToTitleCase();

            string docName = string.Format("{0} {1}", docType, docState);
            return docName;
        }



        /// <summary>
        /// This gets a list from getAllDocuments_For.
        /// </summary>
        /// <param name="orderListDateDelimitedByDocumentTypeAndState"></param>
        /// <returns></returns>
        private decimal get_Money_For(List<BuySellDoc> orderListDateDelimitedByDocumentTypeAndState)
        {

            //get all purchase orders for Owner

            //add up the purchase amoung
            if (orderListDateDelimitedByDocumentTypeAndState.IsNullOrEmpty())
                return 0;

            decimal totalPurchase = 0;
            foreach (var item in orderListDateDelimitedByDocumentTypeAndState)
            {
                totalPurchase += item.TotalRemaining;
            }

            return totalPurchase;
        }


        /// <summary>
        /// This gets a list from getAllDocuments_For.
        /// </summary>
        /// <param name="orderListDateDelimitedByDocumentTypeAndState"></param>
        /// <returns></returns>
        private long get_Count_For(List<BuySellDoc> orderListDateDelimitedByDocumentTypeAndState)
        {

            //get all purchase orders for Owner

            //add up the purchase amoung
            if (orderListDateDelimitedByDocumentTypeAndState.IsNullOrEmpty())
                return 0;

            long count = orderListDateDelimitedByDocumentTypeAndState.Count();
            return count;
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
        private List<BuySellDoc> getAllDocuments_For(string userId, BuySellDocStateENUM buySellDocStateEnum, BuySellDocumentTypeENUM buySellDocumentTypeEnum, DateTime fromDate, DateTime toDate)
        {
            //first get all documents in which the userId is a customer.
            //first we need to find the customer for the UserId

            List<BuySellDoc> orderList = getBuySellDocTypeFor(userId, buySellDocumentTypeEnum, fromDate, toDate);


            if (orderList.IsNullOrEmpty())
                return null;

            orderList = getDocumentTypeForStateFor(buySellDocStateEnum, orderList);
            //get all purchase orders for Owner
            return orderList;
        }

        /// <summary>
        /// from a list of sale or Purchase documents this gets a specific document state and returns the document list
        /// </summary>
        /// <param name="buySellDocStateEnum"></param>
        /// <param name="orderList"></param>
        /// <returns></returns>
        private static List<BuySellDoc> getDocumentTypeForStateFor(BuySellDocStateENUM buySellDocStateEnum, List<BuySellDoc> orderList)
        {
            //this gets the specific type of document
            switch (buySellDocStateEnum)
            {
                case BuySellDocStateENUM.Unknown:
                    throw new Exception("Unknown item");

                case BuySellDocStateENUM.InProccess:
                    orderList = orderList.Where(x =>
                        x.BuySellDocStateEnum == BuySellDocStateENUM.ConfirmedBySeller ||
                        x.BuySellDocStateEnum == BuySellDocStateENUM.ReadyForShipment ||
                        x.BuySellDocStateEnum == BuySellDocStateENUM.ConfirmedByCourier ||
                        x.BuySellDocStateEnum == BuySellDocStateENUM.PickedUp ||
                        x.BuySellDocStateEnum == BuySellDocStateENUM.Problem).ToList();
                    break;

                case BuySellDocStateENUM.BackOrdered:
                    throw new NotImplementedException();

                case BuySellDocStateENUM.RequestUnconfirmed:
                    orderList = orderList.Where(x => x.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed).ToList();
                    break;
                case BuySellDocStateENUM.RequestConfirmed:
                    orderList = orderList.Where(x => x.BuySellDocStateEnum == BuySellDocStateENUM.RequestConfirmed).ToList();
                    break;

                case BuySellDocStateENUM.ConfirmedBySeller:
                    orderList = orderList.Where(x => x.BuySellDocStateEnum == BuySellDocStateENUM.ConfirmedBySeller).ToList();
                    break;

                case BuySellDocStateENUM.ReadyForShipment:
                    orderList = orderList.Where(x => x.BuySellDocStateEnum == BuySellDocStateENUM.ReadyForShipment).ToList();
                    break;
                case BuySellDocStateENUM.ConfirmedByCourier:
                    orderList = orderList.Where(x => x.BuySellDocStateEnum == BuySellDocStateENUM.ConfirmedByCourier).ToList();
                    break;
                case BuySellDocStateENUM.PickedUp:
                    orderList = orderList.Where(x => x.BuySellDocStateEnum == BuySellDocStateENUM.PickedUp).ToList();
                    break;
                case BuySellDocStateENUM.Delivered:
                    orderList = orderList.Where(x => x.BuySellDocStateEnum == BuySellDocStateENUM.Delivered).ToList();
                    break;
                case BuySellDocStateENUM.Rejected:
                    orderList = orderList.Where(x => x.BuySellDocStateEnum == BuySellDocStateENUM.Rejected).ToList();
                    break;
                case BuySellDocStateENUM.Problem:
                    orderList = orderList.Where(x => x.BuySellDocStateEnum == BuySellDocStateENUM.Problem).ToList();
                    break;
                case BuySellDocStateENUM.All:
                    orderList = orderList.Where(x =>
                        x.BuySellDocStateEnum == BuySellDocStateENUM.ConfirmedBySeller ||
                        x.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed ||   //this is only part different from inProccess
                        x.BuySellDocStateEnum == BuySellDocStateENUM.RequestConfirmed ||   //this is only part different from inProccess
                        x.BuySellDocStateEnum == BuySellDocStateENUM.ReadyForShipment ||
                        x.BuySellDocStateEnum == BuySellDocStateENUM.ConfirmedByCourier ||
                        x.BuySellDocStateEnum == BuySellDocStateENUM.PickedUp ||
                        x.BuySellDocStateEnum == BuySellDocStateENUM.Delivered ||
                        x.BuySellDocStateEnum == BuySellDocStateENUM.Problem).ToList();
                    break;

                default:
                    break;
            }
            return orderList;
        }



        /// <summary>
        /// This gets the order list of Sale or Purchase documents date delimited.
        /// This also figures out if it is a sale or a purchase
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="buySellDocumentTypeEnum"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>

        private List<BuySellDoc> getBuySellDocTypeFor(string userId, BuySellDocumentTypeENUM buySellDocumentTypeEnum, DateTime fromDate, DateTime toDate)
        {
            List<BuySellDoc> orderList = FindAll().Where(x => x.MetaData.Created.Date >= fromDate && x.MetaData.Created.Date <= toDate).ToList();

            if (orderList.IsNullOrEmpty())
                return null;

            //this gets all the purchases or sales together.

            switch (buySellDocumentTypeEnum)
            {
                case BuySellDocumentTypeENUM.Purchase:
                    //When user is the customer, then it is a purchase

                    if (userId.IsNullOrEmpty())
                    {
                        //this is for the admin
                        orderList = FindAll().ToList();
                    }
                    else
                    {
                        Customer customer = CustomerBiz.GetPlayerFor(userId);
                        if (customer.IsNull())
                            return null;

                        orderList = FindAll().Where(x => x.CustomerId == customer.Id).ToList();

                    }
                    break;


                case BuySellDocumentTypeENUM.Sale:
                    //when user is the owner then it is a sale
                    if (userId.IsNullOrEmpty())
                    {
                        //this is for the admin
                        orderList = FindAll().ToList();
                    }
                    else
                    {
                        Owner owner = OwnerBiz.GetPlayerFor(userId);
                        if (owner.IsNull())
                            return null;
                        orderList = FindAll().Where(x => x.OwnerId == owner.Id).ToList();
                    }
                    break;


                case BuySellDocumentTypeENUM.Unknown:


                default:
                    throw new Exception("Document type is unknown");
            }
            return orderList;
        }

    }
}
