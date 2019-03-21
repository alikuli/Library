using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System.Collections.Generic;
using System.Linq;

namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellDocBiz
    {



        private int getOrdersCountFor(string userId, BuySellDocStateENUM state)
        {
            int count = getOrdersFor(userId, state).Count();
            return count;
        }

        private List<BuySellDoc> getOrdersFor(string userId, BuySellDocStateENUM state)
        {
            IQueryable<BuySellDoc> ordersQuery = FindAll().Where(x => x.BuySellDocStateEnum == state);

            //gets all th orders
            if (!userId.IsNullOrWhiteSpace())
            {
                Customer customer = CustomerBiz.GetEntityFor(userId);
                customer.IsNullThrowException("customer");

                ordersQuery = ordersQuery.Where(
                        x => x.CustomerId == customer.Id);

            }
            List<BuySellDoc> orders = ordersQuery.ToList();
            return orders;

        }


        private decimal getOrderSale(string userId, BuySellDocStateENUM state)
        {
            List<BuySellDoc> orderLst = getOrdersFor(userId, state);
            decimal total = 0;

            if (orderLst.IsNullOrEmpty())
                return 0;

            foreach (var order in orderLst)
            {
                total += order.TotalSale;
            }
            return total;

        }
        public UserMoneyAccount MoneyAccountForPerson(string userId, bool isAdmin, UserMoneyAccount userMoneyAccount)
        {
            if (userId.IsNullOrWhiteSpace())
                return userMoneyAccount;

            Customer customer = CustomerBiz.GetEntityFor(userId);
            customer.IsNullThrowException("Customer");

            string customerFullName = customer.FullName();
            string ownerFullName = "";


            decimal userOpenOrdersInMoney = getOrderSale(userId, BuySellDocStateENUM.New);

            double userOpenOrdersInQuantity = getOrdersCountFor(userId, BuySellDocStateENUM.New);

            decimal userClosedOrdersInMoney = getOrderSale(userId, BuySellDocStateENUM.Closed);
            double userClosedOrdersInQuantity = getOrdersCountFor(userId, BuySellDocStateENUM.Closed);

            decimal userInProccessOrdersInMoney = getOrderSale(userId, BuySellDocStateENUM.InProccess);
            double userInProccessOrdersInQuantity = getOrdersCountFor(userId, BuySellDocStateENUM.InProccess);

            decimal userCanceledOrdersInMoney = getOrderSale(userId, BuySellDocStateENUM.Canceled);
            double userCanceledOrdersInQuantity = getOrdersCountFor(userId, BuySellDocStateENUM.Canceled);

            decimal userBackOrdersInMoney = getOrderSale(userId, BuySellDocStateENUM.BackOrdered);
            double userBackOrderedOrdersInQuantity = getOrdersCountFor(userId, BuySellDocStateENUM.BackOrdered);

            decimal systemOpenOrdersInMoney = 0;
            double systemOpenOrdersInQuantity = 0;

            decimal systemClosedOrdersInMoney = 0;
            double systemClosedOrdersInQuantity = 0;

            decimal systemInProccessOrdersInMoney = 0;
            double systemInProccessOrdersInQuantity = 0;

            decimal systemCanceledOrdersInMoney = 0;
            double systemCanceledOrdersInQuantity = 0;

            decimal systemBackOrdersInMoney = 0;
            double systemBackOrderedOrdersInQuantity = 0;
            if (isAdmin)
            {
                systemOpenOrdersInMoney = getOrderSale("", BuySellDocStateENUM.New);
                systemOpenOrdersInQuantity = getOrdersCountFor("", BuySellDocStateENUM.New);

                systemClosedOrdersInMoney = getOrderSale(userId, BuySellDocStateENUM.Closed);
                systemClosedOrdersInQuantity = getOrdersCountFor("", BuySellDocStateENUM.Closed);

                systemInProccessOrdersInMoney = getOrderSale(userId, BuySellDocStateENUM.InProccess);
                systemInProccessOrdersInQuantity = getOrdersCountFor("", BuySellDocStateENUM.InProccess);

                systemCanceledOrdersInMoney = getOrderSale(userId, BuySellDocStateENUM.Canceled);
                systemCanceledOrdersInQuantity = getOrdersCountFor("", BuySellDocStateENUM.Canceled);

                systemBackOrdersInMoney = getOrderSale(userId, BuySellDocStateENUM.BackOrdered);
                systemBackOrderedOrdersInQuantity = getOrdersCountFor("", BuySellDocStateENUM.BackOrdered);

            }
            userMoneyAccount.InitializeSalesOrders(
                userOpenOrdersInMoney,
                userOpenOrdersInQuantity,
                userClosedOrdersInMoney,
                userClosedOrdersInQuantity,
                userInProccessOrdersInMoney,
                userInProccessOrdersInQuantity,
                userCanceledOrdersInMoney,
                userCanceledOrdersInQuantity,
                userBackOrdersInMoney,
                userBackOrderedOrdersInQuantity,
                systemOpenOrdersInMoney,
                systemOpenOrdersInQuantity,
                systemClosedOrdersInMoney,
                systemClosedOrdersInQuantity,
                systemInProccessOrdersInMoney,
                systemInProccessOrdersInQuantity,
                systemCanceledOrdersInMoney,
                systemCanceledOrdersInQuantity,
                systemBackOrdersInMoney,
                systemBackOrderedOrdersInQuantity);

            return userMoneyAccount;
        }

    }
}
