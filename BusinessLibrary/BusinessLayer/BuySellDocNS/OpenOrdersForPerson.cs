using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellDocBiz
    {
        //OLD
        //this is thecontroller which routes all the variables to their proper places
        public UserMoneyAccount GetMoneyAccount(string userId, bool isAdmin, UserMoneyAccount userMoneyAccount)
        {
            if (userId.IsNullOrWhiteSpace())
                return userMoneyAccount;

            Owner owner = OwnerBiz.GetPlayerFor(userId);


            if (owner.IsNull())
                return new UserMoneyAccount();

            #region Sale Orders

            decimal personOpenSaleOrders_InMoney = getSaleOrders_InMoney(userId, BuySellDocStateENUM.RequestUnconfirmed);

            double personOpenSaleOrders_InQuantity = getSaleOrders_Count(userId, BuySellDocStateENUM.RequestUnconfirmed);

            decimal personClosedSaleOrders_InMoney = getSaleOrders_InMoney(userId, BuySellDocStateENUM.Closed);
            double personClosedSaleOrders_InQuantity = getSaleOrders_Count(userId, BuySellDocStateENUM.Closed);

            decimal person_InProccessSaleOrders_InMoney = getSaleOrders_InMoney(userId, BuySellDocStateENUM.InProccess);
            double person_InProccessSaleOrders_InQuantity = getSaleOrders_Count(userId, BuySellDocStateENUM.InProccess);

            decimal personCanceledSaleOrders_InMoney = getSaleOrders_InMoney(userId, BuySellDocStateENUM.Canceled);
            double personCanceledSaleOrders_InQuantity = getSaleOrders_Count(userId, BuySellDocStateENUM.Canceled);

            decimal personBackSaleOrders_InMoney = getSaleOrders_InMoney(userId, BuySellDocStateENUM.BackOrdered);
            double personBackSaleOrderedOrders_InQuantity = getSaleOrders_Count(userId, BuySellDocStateENUM.BackOrdered);


            decimal personSaleCredits_InMoney = 0;
            double personSaleCredits_InQuantity = 0;

            decimal personSaleQuotations_InMoney = 0;
            double personSaleQuotations_InQuantity = 0;


            decimal systemOpenSaleOrders_InMoney = 0;
            double systemOpenSaleOrders_InQuantity = 0;

            decimal systemClosedSaleOrders_InMoney = 0;
            double systemClosedSaleOrders_InQuantity = 0;

            decimal system_InProccessSaleOrders_InMoney = 0;
            double system_InProccessSaleOrders_InQuantity = 0;

            decimal systemCanceledSaleOrders_InMoney = 0;
            double systemCanceledSaleOrders_InQuantity = 0;

            decimal systemBackSaleOrders_InMoney = 0;
            double systemBackSaleOrderedOrders_InQuantity = 0;

            decimal systemSaleCredits_InMoney = 0;
            double systemSaleCredits_InQuantity = 0;

            decimal systemSaleQuotations_InMoney = 0;
            double systemSaleQuotations_InQuantity = 0;


            if (isAdmin)
            {
                systemOpenSaleOrders_InMoney = getSaleOrders_InMoney("", BuySellDocStateENUM.New);
                systemOpenSaleOrders_InQuantity = getSaleOrders_Count("", BuySellDocStateENUM.New);

                systemClosedSaleOrders_InMoney = getSaleOrders_InMoney(userId, BuySellDocStateENUM.Closed);
                systemClosedSaleOrders_InQuantity = getSaleOrders_Count("", BuySellDocStateENUM.Closed);

                system_InProccessSaleOrders_InMoney = getSaleOrders_InMoney(userId, BuySellDocStateENUM.InProccess);
                system_InProccessSaleOrders_InQuantity = getSaleOrders_Count("", BuySellDocStateENUM.InProccess);

                systemCanceledSaleOrders_InMoney = getSaleOrders_InMoney(userId, BuySellDocStateENUM.Canceled);
                systemCanceledSaleOrders_InQuantity = getSaleOrders_Count("", BuySellDocStateENUM.Canceled);

                systemBackSaleOrders_InMoney = getSaleOrders_InMoney(userId, BuySellDocStateENUM.BackOrdered);
                systemBackSaleOrderedOrders_InQuantity = getSaleOrders_Count("", BuySellDocStateENUM.BackOrdered);

                systemSaleCredits_InMoney = getSaleOrders_InMoney(userId, BuySellDocStateENUM.Credit);
                systemSaleCredits_InQuantity = getSaleOrders_Count("", BuySellDocStateENUM.Credit);

                systemSaleQuotations_InMoney = getSaleOrders_InMoney(userId, BuySellDocStateENUM.Quotation);
                systemSaleQuotations_InQuantity = getSaleOrders_Count("", BuySellDocStateENUM.Quotation);
            }

            //after getting their values, they are initialized in the class
            userMoneyAccount.InitializeSalesOrders(
                personOpenSaleOrders_InMoney,
                personOpenSaleOrders_InQuantity,
                personClosedSaleOrders_InMoney,
                personClosedSaleOrders_InQuantity,
                person_InProccessSaleOrders_InMoney,
                person_InProccessSaleOrders_InQuantity,
                personCanceledSaleOrders_InMoney,
                personCanceledSaleOrders_InQuantity,
                personBackSaleOrders_InMoney,
                personBackSaleOrderedOrders_InQuantity,
                personSaleCredits_InMoney,
                personSaleCredits_InQuantity,
                personSaleQuotations_InMoney,
                personSaleQuotations_InQuantity,

                systemOpenSaleOrders_InMoney,
                systemOpenSaleOrders_InQuantity,
                systemClosedSaleOrders_InMoney,
                systemClosedSaleOrders_InQuantity,
                system_InProccessSaleOrders_InMoney,
                system_InProccessSaleOrders_InQuantity,
                systemCanceledSaleOrders_InMoney,
                systemCanceledSaleOrders_InQuantity,
                systemBackSaleOrders_InMoney,
                systemBackSaleOrderedOrders_InQuantity,
                systemSaleCredits_InMoney,
                systemSaleCredits_InQuantity,
                systemSaleQuotations_InMoney,
                systemSaleQuotations_InQuantity);
            #endregion


            #region Purchase Orders
            decimal personOpenPurchaseOrders_InMoney = getPurchaseOrders_InMoney(userId, BuySellDocStateENUM.RequestUnconfirmed);

            double personOpenPurchaseOrders_InQuantity = getPurchaseOrders_Count(userId, BuySellDocStateENUM.RequestUnconfirmed);

            decimal personClosedPurchaseOrders_InMoney = getPurchaseOrders_InMoney(userId, BuySellDocStateENUM.Closed);
            double personClosedPurchaseOrders_InQuantity = getPurchaseOrders_Count(userId, BuySellDocStateENUM.Closed);

            decimal person_InProccessPurchaseOrders_InMoney = getPurchaseOrders_InMoney(userId, BuySellDocStateENUM.InProccess);
            double person_InProccessPurchaseOrders_InQuantity = getPurchaseOrders_Count(userId, BuySellDocStateENUM.InProccess);

            decimal personCanceledPurchaseOrders_InMoney = getPurchaseOrders_InMoney(userId, BuySellDocStateENUM.Canceled);
            double personCanceledPurchaseOrders_InQuantity = getPurchaseOrders_Count(userId, BuySellDocStateENUM.Canceled);

            decimal personBackPurchaseOrders_InMoney = getPurchaseOrders_InMoney(userId, BuySellDocStateENUM.BackOrdered);
            double personBackPurchaseOrderedOrders_InQuantity = getPurchaseOrders_Count(userId, BuySellDocStateENUM.BackOrdered);


            decimal personPurchaseCredits_InMoney = 0;
            double personPurchaseCredits_InQuantity = 0;

            decimal personPurchaseQuotations_InMoney = 0;
            double personPurchaseQuotations_InQuantity = 0;


            decimal systemOpenPurchaseOrders_InMoney = 0;
            double systemOpenPurchaseOrders_InQuantity = 0;

            decimal systemClosedPurchaseOrders_InMoney = 0;
            double systemClosedPurchaseOrders_InQuantity = 0;

            decimal system_InProccessPurchaseOrders_InMoney = 0;
            double system_InProccessPurchaseOrders_InQuantity = 0;

            decimal systemCanceledPurchaseOrders_InMoney = 0;
            double systemCanceledPurchaseOrders_InQuantity = 0;

            decimal systemBackPurchaseOrders_InMoney = 0;
            double systemBackPurchaseOrderedOrders_InQuantity = 0;

            decimal systemPurchaseCredits_InMoney = 0;
            double systemPurchaseCredits_InQuantity = 0;

            decimal systemPurchaseQuotations_InMoney = 0;
            double systemPurchaseQuotations_InQuantity = 0;


            if (isAdmin)
            {
                systemOpenPurchaseOrders_InMoney = getPurchaseOrders_InMoney("", BuySellDocStateENUM.RequestUnconfirmed);
                systemOpenPurchaseOrders_InQuantity = getPurchaseOrders_Count("", BuySellDocStateENUM.RequestUnconfirmed);

                systemClosedPurchaseOrders_InMoney = getPurchaseOrders_InMoney(userId, BuySellDocStateENUM.Closed);
                systemClosedPurchaseOrders_InQuantity = getPurchaseOrders_Count("", BuySellDocStateENUM.Closed);

                system_InProccessPurchaseOrders_InMoney = getPurchaseOrders_InMoney(userId, BuySellDocStateENUM.InProccess);
                system_InProccessPurchaseOrders_InQuantity = getPurchaseOrders_Count("", BuySellDocStateENUM.InProccess);

                systemCanceledPurchaseOrders_InMoney = getPurchaseOrders_InMoney(userId, BuySellDocStateENUM.Canceled);
                systemCanceledPurchaseOrders_InQuantity = getPurchaseOrders_Count("", BuySellDocStateENUM.Canceled);

                systemBackPurchaseOrders_InMoney = getPurchaseOrders_InMoney(userId, BuySellDocStateENUM.BackOrdered);
                systemBackPurchaseOrderedOrders_InQuantity = getPurchaseOrders_Count("", BuySellDocStateENUM.BackOrdered);

                systemPurchaseCredits_InMoney = getPurchaseOrders_InMoney(userId, BuySellDocStateENUM.Credit);
                systemPurchaseCredits_InQuantity = getPurchaseOrders_Count("", BuySellDocStateENUM.Credit);

                systemPurchaseQuotations_InMoney = getPurchaseOrders_InMoney(userId, BuySellDocStateENUM.Quotation);
                systemPurchaseQuotations_InQuantity = getPurchaseOrders_Count("", BuySellDocStateENUM.Quotation);
            }


            userMoneyAccount.InitializePurchaseOrders(
                personOpenPurchaseOrders_InMoney,
                personOpenPurchaseOrders_InQuantity,
                personClosedPurchaseOrders_InMoney,
                personClosedPurchaseOrders_InQuantity,
                person_InProccessPurchaseOrders_InMoney,
                person_InProccessPurchaseOrders_InQuantity,
                personCanceledPurchaseOrders_InMoney,
                personCanceledPurchaseOrders_InQuantity,
                personBackPurchaseOrders_InMoney,
                personBackPurchaseOrderedOrders_InQuantity,
                personPurchaseCredits_InMoney,
                personPurchaseCredits_InQuantity,
                personPurchaseQuotations_InMoney,
                personPurchaseQuotations_InQuantity,

                systemOpenPurchaseOrders_InMoney,
                systemOpenPurchaseOrders_InQuantity,
                systemClosedPurchaseOrders_InMoney,
                systemClosedPurchaseOrders_InQuantity,
                system_InProccessPurchaseOrders_InMoney,
                system_InProccessPurchaseOrders_InQuantity,
                systemCanceledPurchaseOrders_InMoney,
                systemCanceledPurchaseOrders_InQuantity,
                systemBackPurchaseOrders_InMoney,
                systemBackPurchaseOrderedOrders_InQuantity,
                systemPurchaseCredits_InMoney,
                systemPurchaseCredits_InQuantity,
                systemPurchaseQuotations_InMoney,
                systemPurchaseQuotations_InQuantity);
            #endregion
            return userMoneyAccount;
        }




        #region Purchase orders OLD

        private decimal getPurchaseOrders_InMoney(string userId, BuySellDocStateENUM buySellDocStateEnum)
        {

            //get all purchase orders for Owner
            List<BuySellDoc> purchaseOrdersLst = getAllPurchaseOrders_For(userId, buySellDocStateEnum);

            //add up the purchase amoung
            if (purchaseOrdersLst.IsNullOrEmpty())
                return 0;

            decimal totalPurchase = 0;
            foreach (var item in purchaseOrdersLst)
            {
                totalPurchase += item.TotalRemaining;
            }

            return totalPurchase;
        }
        private double getPurchaseOrders_Count(string userId, BuySellDocStateENUM buySellDocStateEnum)
        {
            List<BuySellDoc> purchaseOrdersLst = getAllPurchaseOrders_For(userId, buySellDocStateEnum);

            //add up the purchase amoung
            if (purchaseOrdersLst.IsNullOrEmpty())
                return 0;

            return purchaseOrdersLst.Count();
        }



        private List<BuySellDoc> getAllPurchaseOrders_For(string userId, DateTime fromDate, DateTime toDate, BuySellDocStateENUM buySellDocStateEnum)
        {
            return getAllPurchaseOrders_For(userId, buySellDocStateEnum)
                .Where(x =>
                    x.MetaData.Created.Date >= fromDate &&
                    x.MetaData.Created.Date <= toDate)
                    .ToList();
        }

        private List<BuySellDoc> getAllPurchaseOrders_For(string userId, BuySellDocStateENUM buySellDocStateEnum)
        {
            List<BuySellDoc> purchaseOrdersLst = new List<BuySellDoc>();
            switch (buySellDocStateEnum)
            {

                case BuySellDocStateENUM.RequestUnconfirmed:
                    purchaseOrdersLst = getAllPurchaseOrders_For(userId)
                       .Where(x =>
                           x.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed)
                       .ToList();
                    break;


                case BuySellDocStateENUM.RequestConfirmed:
                    purchaseOrdersLst = getAllPurchaseOrders_For(userId)
                       .Where(x =>
                           x.BuySellDocStateEnum == BuySellDocStateENUM.RequestConfirmed)
                       .ToList();
                    break;


                case BuySellDocStateENUM.ConfirmedBySeller:
                    purchaseOrdersLst = getAllPurchaseOrders_For(userId)
                       .Where(x =>
                           x.BuySellDocStateEnum == BuySellDocStateENUM.ConfirmedBySeller)
                       .ToList();
                    break;


                case BuySellDocStateENUM.ReadyForShipment:
                    purchaseOrdersLst = getAllPurchaseOrders_For(userId)
                       .Where(x =>
                           x.BuySellDocStateEnum == BuySellDocStateENUM.ReadyForShipment)
                       .ToList();
                    break;


                case BuySellDocStateENUM.ConfirmedByCourier:
                    purchaseOrdersLst = getAllPurchaseOrders_For(userId)
                       .Where(x =>
                           x.BuySellDocStateEnum == BuySellDocStateENUM.ConfirmedByCourier)
                       .ToList();
                    break;


                case BuySellDocStateENUM.PickedUp:
                    purchaseOrdersLst = getAllPurchaseOrders_For(userId)
                       .Where(x =>
                           x.BuySellDocStateEnum == BuySellDocStateENUM.PickedUp)
                       .ToList();
                    break;


                case BuySellDocStateENUM.Delivered:
                    purchaseOrdersLst = getAllPurchaseOrders_For(userId)
                       .Where(x =>
                           x.BuySellDocStateEnum == BuySellDocStateENUM.Delivered)
                       .ToList();
                    break;


                case BuySellDocStateENUM.Rejected:
                    purchaseOrdersLst = getAllPurchaseOrders_For(userId)
                       .Where(x =>
                           x.BuySellDocStateEnum == BuySellDocStateENUM.Rejected)
                       .ToList();
                    break;


                case BuySellDocStateENUM.Problem:
                    purchaseOrdersLst = getAllPurchaseOrders_For(userId)
                       .Where(x =>
                           x.BuySellDocStateEnum == BuySellDocStateENUM.Problem)
                       .ToList();
                    break;


                case BuySellDocStateENUM.InProccess:
                    purchaseOrdersLst = getAllPurchaseOrders_For(userId)
                       .Where(x =>
                           x.BuySellDocStateEnum == BuySellDocStateENUM.RequestConfirmed ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.ConfirmedBySeller ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.ReadyForShipment ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.ConfirmedByCourier ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.PickedUp ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.Delivered)
                       .ToList();
                    break;


                case BuySellDocStateENUM.All:
                    purchaseOrdersLst = getAllPurchaseOrders_For(userId)
                       .Where(x =>
                           x.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.RequestConfirmed ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.ConfirmedBySeller ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.ReadyForShipment ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.ConfirmedByCourier ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.PickedUp ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.Delivered ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.Rejected ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.Problem)
                       .ToList();
                    break;


                case BuySellDocStateENUM.Unknown:
                default:
                    break;
            }
            //get all purchase orders for Owner
            return purchaseOrdersLst;
        }

        private IQueryable<BuySellDoc> getAllPurchaseOrders_For(string userId)
        {
            //we need to be able to get all the purchase orders here without the userid
            //userId.IsNullOrWhiteSpaceThrowArgumentException("You are not logged in.");


            //when selling, the user is the owner
            IQueryable<BuySellDoc> purchaseOrdersIq = FindAll();

            if (userId.IsNullOrWhiteSpace())
            {

                return purchaseOrdersIq;

            }
            Customer customer;

            try
            {
                customer = CustomerBiz.GetPlayerFor(userId);
                customer.IsNullThrowException("Customer");

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("No Owner is attached", MethodBase.GetCurrentMethod(), e);
                throw new Exception(ErrorsGlobal.ToString());
            }


            //get all purchase orders for Owner
            purchaseOrdersIq = purchaseOrdersIq
                .Where(x => x.CustomerId == customer.Id);

            return purchaseOrdersIq;

        }


        //private decimal getPurchaseOrders_InMoney(string userId, BuySellDocStateENUM buySellDocStateEnum)
        //{

        //    //get all purchase orders for Owner
        //    List<BuySellDoc> purchaseOrdersLst = GetAllPurchaseOrders_For(userId, buySellDocStateEnum);

        //    //add up the purchase amoung
        //    if (purchaseOrdersLst.IsNullOrEmpty())
        //        return 0;

        //    decimal totalPurchase = 0;
        //    foreach (var item in purchaseOrdersLst)
        //    {
        //        totalPurchase += item.TotalRemaining;
        //    }

        //    return totalPurchase;
        //}
        //private double getPurchaseOrders_Count(string userId, BuySellDocStateENUM buySellDocStateEnum)
        //{
        //    List<BuySellDoc> purchaseOrdersLst = GetAllPurchaseOrders_For(userId, buySellDocStateEnum);

        //    //add up the purchase amoung
        //    if (purchaseOrdersLst.IsNullOrEmpty())
        //        return 0;

        //    return purchaseOrdersLst.Count();
        //}


        //private IQueryable<BuySellDoc> getAllPurchaseOrders_For(string userId)
        //{
        //    //we need to be able to get all the purchase orders here without the userid
        //    //userId.IsNullOrWhiteSpaceThrowArgumentException("You are not logged in.");


        //    //when selling, the user is the owner
        //    IQueryable<BuySellDoc> purchaseOrdersIq = FindAll();

        //    if (userId.IsNullOrWhiteSpace())
        //    {

        //        return purchaseOrdersIq;

        //    }
        //    Customer customer;

        //    try
        //    {
        //        customer = CustomerBiz.GetEntityFor(userId);
        //        customer.IsNullThrowException("Customer");

        //    }
        //    catch (Exception e)
        //    {

        //        ErrorsGlobal.Add("No Customer is attached", MethodBase.GetCurrentMethod(), e);
        //        throw new Exception(ErrorsGlobal.ToString());
        //    }


        //    //get all purchase orders for Owner
        //    purchaseOrdersIq = purchaseOrdersIq
        //        .Where(x => x.CustomerId == customer.Id);

        //    return purchaseOrdersIq;

        //}

        //public IQueryable<BuySellDoc> getAllPurchaseOrders_For(string userId, DateTime fromDate, DateTime toDate)
        //{
        //    return getAllPurchaseOrders_For(userId)
        //        .Where(x =>
        //            x.MetaData.Created.Date >= fromDate &&
        //            x.MetaData.Created.Date <= toDate);

        //}

        //public IQueryable<BuySellDoc> getAllPurchaseOrders_For(string userId, DateTime fromDate, DateTime toDate, BuySellDocStateENUM buySellDocStateEnum)
        //{
        //    return getAllPurchaseOrders_For(userId, fromDate, toDate)
        //        .Where(x =>
        //            x.BuySellDocStateEnum == buySellDocStateEnum);
        //}

        //public List<BuySellDoc> GetAllPurchaseOrders_For(string userId)
        //{
        //    return getAllPurchaseOrders_For(userId).ToList();
        //}
        //public List<BuySellDoc> GetAllPurchaseOrders_For(string userId, DateTime fromDate, DateTime toDate)
        //{
        //    return getAllPurchaseOrders_For(userId)
        //        .Where(x =>
        //            x.MetaData.Created.Date >= fromDate &&
        //            x.MetaData.Created.Date <= toDate)
        //            .ToList();
        //}

        //public List<BuySellDoc> GetAllPurchaseOrders_For(string userId, DateTime fromDate, DateTime toDate, BuySellDocStateENUM buySellDocStateEnum)
        //{
        //    return GetAllPurchaseOrders_For(userId, fromDate, toDate)
        //        .Where(x =>
        //            x.BuySellDocStateEnum == buySellDocStateEnum)
        //            .ToList();
        //}


        //public List<BuySellDoc> GetAllPurchaseOrders_For(string userId, BuySellDocStateENUM buySellDocStateEnum)
        //{
        //    if (buySellDocStateEnum == BuySellDocStateENUM.Unknown)
        //        throw new Exception("State of document is unknown.");

        //    //get all purchase orders for Owner
        //    List<BuySellDoc> purchaseOrdersLst = getAllPurchaseOrders_For(userId)
        //        .Where(x => x.BuySellDocStateEnum == buySellDocStateEnum)
        //        .ToList();

        //    return purchaseOrdersLst;
        //}



        #endregion



        #region Sale Orders OLD






        private decimal getSaleOrders_InMoney(string userId, BuySellDocStateENUM buySellDocStateEnum)
        {

            //get all sale orders for Owner
            List<BuySellDoc> saleOrdersLst = getAllSaleOrders_For(userId, buySellDocStateEnum);

            //add up the sale amoung
            if (saleOrdersLst.IsNullOrEmpty())
                return 0;

            decimal totalSale = 0;
            foreach (var item in saleOrdersLst)
            {
                totalSale += item.TotalRemaining;
            }

            return totalSale;
        }
        private double getSaleOrders_Count(string userId, BuySellDocStateENUM buySellDocStateEnum)
        {
            List<BuySellDoc> saleOrdersLst = getAllSaleOrders_For(userId, buySellDocStateEnum);

            //add up the sale amoung
            if (saleOrdersLst.IsNullOrEmpty())
                return 0;

            return saleOrdersLst.Count();
        }



        private List<BuySellDoc> getAllSaleOrders_For(string userId, DateTime fromDate, DateTime toDate, BuySellDocStateENUM buySellDocStateEnum)
        {
            return getAllSaleOrders_For(userId, buySellDocStateEnum)
                .Where(x =>
                    x.MetaData.Created.Date >= fromDate &&
                    x.MetaData.Created.Date <= toDate)
                    .ToList();
        }

        private List<BuySellDoc> getAllSaleOrders_For(string userId, BuySellDocStateENUM buySellDocStateEnum)
        {
            List<BuySellDoc> saleOrdersLst = new List<BuySellDoc>();
            switch (buySellDocStateEnum)
            {
                case BuySellDocStateENUM.New:
                case BuySellDocStateENUM.RequestUnconfirmed:
                case BuySellDocStateENUM.InProccess:
                case BuySellDocStateENUM.Closed:
                case BuySellDocStateENUM.BackOrdered:
                case BuySellDocStateENUM.Canceled:
                case BuySellDocStateENUM.Quotation:
                case BuySellDocStateENUM.Credit:
                    saleOrdersLst = getAllSaleOrders_For(userId)
                       .Where(x => x.BuySellDocStateEnum == buySellDocStateEnum)
                       .ToList();
                    break;
                case BuySellDocStateENUM.All:
                    saleOrdersLst = getAllSaleOrders_For(userId)
                       .Where(x =>
                           x.BuySellDocStateEnum == BuySellDocStateENUM.New ||
                           x.BuySellDocStateEnum == BuySellDocStateENUM.InProccess)
                       .ToList();
                    break;
                case BuySellDocStateENUM.Unknown:
                default:
                    throw new Exception("State of document is unknown.");
            }
            //get all sale orders for Owner
            return saleOrdersLst;
        }

        private IQueryable<BuySellDoc> getAllSaleOrders_For(string userId)
        {
            //we need to be able to get all the sale orders here without the userid
            //userId.IsNullOrWhiteSpaceThrowArgumentException("You are not logged in.");


            //when selling, the user is the owner
            IQueryable<BuySellDoc> saleOrdersIq = FindAll();

            if (userId.IsNullOrWhiteSpace())
            {

                return saleOrdersIq;

            }
            Owner owner;

            try
            {
                owner = OwnerBiz.GetPlayerFor(userId);
                owner.IsNullThrowException("Owner ");

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("No Owner is attached", MethodBase.GetCurrentMethod(), e);
                throw new Exception(ErrorsGlobal.ToString());
            }


            //get all sale orders for Owner
            saleOrdersIq = saleOrdersIq
                .Where(x => x.OwnerId == owner.Id);

            return saleOrdersIq;

        }

        #endregion



        //private decimal getAllOrder_InMoney_For(BuySellDocStateENUM buySellDocStateEnum, BuySellDocumentTypeENUM buySellDocumentTypeEnum, string userId, DateTime fromDate, DateTime toDate)
        //{

        //    //get all purchase orders for Owner
        //    List<BuySellDoc> purchaseOrdersLst = getAllPurchaseOrders_For(userId, buySellDocStateEnum);

        //    //add up the purchase amoung
        //    if (purchaseOrdersLst.IsNullOrEmpty())
        //        return 0;

        //    decimal totalPurchase = 0;
        //    foreach (var item in purchaseOrdersLst)
        //    {
        //        totalPurchase += item.TotalRemaining;
        //    }

        //    return totalPurchase;
        //}

        //private List<BuySellDoc> getAllDocuments_For(string userId, BuySellDocStateENUM buySellDocStateEnum)
        //{
        //    List<BuySellDoc> purchaseOrdersLst = new List<BuySellDoc>();
        //    switch (buySellDocStateEnum)
        //    {
        //        case BuySellDocStateENUM.New:
        //        case BuySellDocStateENUM.InProccess:
        //        case BuySellDocStateENUM.Closed:
        //        case BuySellDocStateENUM.BackOrdered:
        //        case BuySellDocStateENUM.Canceled:
        //        case BuySellDocStateENUM.Quotation:
        //        case BuySellDocStateENUM.Credit:
        //            purchaseOrdersLst = getAllPurchaseOrders_For(userId)
        //               .Where(x => x.BuySellDocStateEnum == buySellDocStateEnum)
        //               .ToList();
        //            break;
        //        case BuySellDocStateENUM.All:
        //            purchaseOrdersLst = getAllPurchaseOrders_For(userId)
        //               .Where(x =>
        //                   x.BuySellDocStateEnum == BuySellDocStateENUM.New ||
        //                   x.BuySellDocStateEnum == BuySellDocStateENUM.InProccess)
        //               .ToList();
        //            break;
        //        case BuySellDocStateENUM.Unknown:
        //        default:
        //            throw new Exception("State of document is unknown.");
        //    }
        //    //get all purchase orders for Owner
        //    return purchaseOrdersLst;
        //}



        #region General Methods



        private Customer getCustomerFor(string userId)
        {
            Customer customer = CustomerBiz.GetPlayerFor(userId);
            customer.IsNullThrowException("customer");
            return customer;
        }


        bool IsAdmin
        {
            get
            {
                return UserBiz.IsAdmin(UserId);
            }
        }

        //public BuySellStatementModel Get_X_Orders_List(string userId, DateTime fromDate, DateTime toDate, bool isAdmin, BuySellDocumentTypeENUM buySellDocumentTypeEnum, BuySellDocStateENUM buySellDocStateEnum)
        //{
        //    userId.IsNullOrWhiteSpaceThrowArgumentException("You are not logged in.");
        //    List<BuySellDoc> buySellDocs = new List<BuySellDoc>();
        //    switch (buySellDocStateEnum)
        //    {
        //        case BuySellDocStateENUM.New:
        //        case BuySellDocStateENUM.InProccess:
        //        case BuySellDocStateENUM.Closed:
        //        case BuySellDocStateENUM.BackOrdered:
        //        case BuySellDocStateENUM.Canceled:
        //        case BuySellDocStateENUM.Quotation:
        //        case BuySellDocStateENUM.Credit:
        //        case BuySellDocStateENUM.All:
        //            {
        //                switch (buySellDocumentTypeEnum)
        //                {
        //                    case BuySellDocumentTypeENUM.Sale:
        //                        buySellDocs = getAllSaleOrders_For(UserId, fromDate, toDate, buySellDocStateEnum);
        //                        //buySellDocs = makeAllBuySellDocs(BuySellDocumentTypeENUM.Sale, buySellDocs);
        //                        break;

        //                    case BuySellDocumentTypeENUM.Purchase:
        //                        buySellDocs = getAllPurchaseOrders_For(UserId, fromDate, toDate, buySellDocStateEnum);
        //                        //buySellDocs = makeAllBuySellDocs(BuySellDocumentTypeENUM.Purchase, buySellDocs);
        //                        break;

        //                    case BuySellDocumentTypeENUM.Unknown:
        //                    default:
        //                        throw new Exception("Unknown statement type");
        //                }
        //            }
        //            break;
        //        case BuySellDocStateENUM.Unknown:
        //        default:
        //            throw new Exception(string.Format("Switch statement is incorrect '{0}'", buySellDocStateEnum.ToString()));
        //    }

        //    if (!buySellDocs.IsNullOrEmpty())
        //    {
        //        foreach (BuySellDoc item in buySellDocs)
        //        {
        //            item.BuySellDocumentTypeEnum = buySellDocumentTypeEnum;
        //        }
        //    }
        //    BuySellStatementModel buySellStatementModel = new BuySellStatementModel(buySellDocs, fromDate, toDate, buySellDocumentTypeEnum, isAdmin, buySellDocStateEnum);

        //    return buySellStatementModel;

        //}

  
 
        //private IQueryable<BuySellDoc> get_All_Orders_Which_Can_Be_Shipped(IQueryable<BuySellDoc> allOrders, DateTime fromDate, DateTime ToDate)
        //{
        //    IQueryable<BuySellDoc> ordersThatCanBeShipped = allOrders.Where(x =>
        //        x.BuySellDocStateEnum == BuySellDocStateENUM.BackOrdered ||
        //        x.BuySellDocStateEnum == BuySellDocStateENUM.InProccess ||
        //        x.BuySellDocStateEnum == BuySellDocStateENUM.New);

        //    IQueryable<BuySellDoc> ordersThatCanBeShipped_DateDelimited = ordersThatCanBeShipped.Where(x =>
        //        x.MetaData.Created.Date >= fromDate &&
        //        x.MetaData.Created.Date <= ToDate);

        //    return ordersThatCanBeShipped_DateDelimited;
        //}


        //public List<BuySellDoc> Get_All_Shippable_Sales_Orders(string userId, DateTime fromDate, DateTime toDate)
        //{
        //    List<BuySellDoc> shipableOrdersForUser = get_All_Orders_Which_Can_Be_Shipped(getAllSaleOrders_For(userId), fromDate, toDate).ToList();
        //    return shipableOrdersForUser;
        //}

        //public IQueryable<BuySellDoc> getAllSaleOrders_For(string userId, DateTime fromDate, DateTime toDate, BuySellDocStateENUM buySellDocStateEnum)
        //{
        //    return getAllSaleOrders_For(userId, fromDate, toDate)
        //        .Where(x =>
        //            x.BuySellDocStateEnum == buySellDocStateEnum);
        //}

        //public List<BuySellDoc> Get_All_Shippable_Purchases_Orders(string userId, DateTime fromDate, DateTime toDate)
        //{
        //    List<BuySellDoc> shipableOrdersForUser = get_All_Orders_Which_Can_Be_Shipped(getAllPurchaseOrders_For(userId), fromDate, toDate).ToList();
        //    return shipableOrdersForUser;
        //}







        //public IQueryable<BuySellDoc> GetAllPurchaseOrdersFor(string userId)
        //{
        //    IQueryable<BuySellDoc> ordersQuery = FindAll();

        //    //gets all th orders
        //    if (!userId.IsNullOrWhiteSpace())
        //    {
        //        Customer customer = getCustomerFor(userId);

        //        ordersQuery = ordersQuery.Where(
        //                x => x.CustomerId == customer.Id);

        //    }
        //    return ordersQuery;

        //}


        //public List<BuySellDoc> Get_All_Purchase_Orders_Which_Can_Be_Shipped(string userId, DateTime fromDate, DateTime ToDate)
        //{
        //    IQueryable<BuySellDoc> allPos = GetAllPurchaseOrdersFor(userId);
        //    List<BuySellDoc> lst = get_All_Orders_Which_Can_Be_Shipped(allPos, fromDate, ToDate).ToList();
        //    return lst;
        //}
        //private decimal get_Amount_For_Shipable_PurchaseOrder(string userId, DateTime fromDate, DateTime ToDate)
        //{
        //    IQueryable<BuySellDoc> allPos = GetAllPurchaseOrdersFor(userId);
        //    List<BuySellDoc> lst = get_All_Orders_Which_Can_Be_Shipped(allPos, fromDate, ToDate).ToList();

        //    decimal total = 0;

        //    if (lst.IsNullOrEmpty())
        //        return 0;

        //    foreach (var order in lst)
        //    {
        //        total += order.TotalOrdered;
        //    }
        //    return total;

        //}        
        //public IQueryable<BuySellDoc> getAllSaleOrders_For(string userId, DateTime fromDate, DateTime toDate)
        //{
        //    return getAllSaleOrders_For(userId)
        //        .Where(x =>
        //            x.MetaData.Created.Date >= fromDate &&
        //            x.MetaData.Created.Date <= toDate);

        //}
        #endregion

    }
}
