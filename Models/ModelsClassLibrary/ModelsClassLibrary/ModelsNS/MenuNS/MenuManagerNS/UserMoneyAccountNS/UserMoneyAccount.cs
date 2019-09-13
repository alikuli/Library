
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.SharedNS.MenuItemHelperNS;
using ModelsClassLibrary.ModelsNS.SharedNS.MenuItemHelperNS.PersonClassesNS;

namespace ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS
{

    /// <summary>
    /// This holds the users Money Amounts to display on the screen
    /// </summary>
    public class UserMoneyAccount
    {
        public UserMoneyAccount()
        {

        }
        //public UserMoneyAccount(
        //    decimal amountRefundable,
        //    decimal amountNonRefundable,
        //    decimal totalCashCreated_Refundable,
        //    decimal totalCashCreated_NonRefundable,
        //    decimal totalOpenOrdersInMoney,
        //    double totalOpenOrdersInQuantity,
        //    decimal totalClosedOrdersInMoney,
        //    double totalClosedOrdersInQuantity,
        //    decimal totalInProccessOrdersInMoney,
        //    double totalInProccessOrdersInQuantity,
        //    decimal totalCanceledOrdersInMoney,
        //    double totalCanceledOrdersInQuantity,
        //    decimal totalBackOrdersInMoney,
        //    double totalBackOrderedOrdersInQuantity
        //    )
        //{
        //    AmountRefundable = amountRefundable;
        //    AmountNonRefundable = amountNonRefundable;

        //    TotalCashCreated_Refundable = totalCashCreated_Refundable;
        //    TotalCashCreated_NonRefundable = totalCashCreated_NonRefundable;
        //    TotalOpenOrdersInMoney = totalOpenOrdersInMoney;
        //    TotalOpenOrdersInQuantity = totalOpenOrdersInQuantity;
        //    TotalClosedOrdersInQuantity = totalClosedOrdersInQuantity;
        //    TotalInProccessOrdersInMoney = totalInProccessOrdersInMoney;
        //    TotalInProccessOrdersInQuantity = totalInProccessOrdersInQuantity;
        //    TotalCanceledOrdersInMoney = totalCanceledOrdersInMoney;
        //    TotalCanceledOrdersInQuantity = totalCanceledOrdersInQuantity;
        //    TotalBackOrdersInMoney = totalBackOrdersInMoney;
        //    TotalBackOrdersInQuantity = totalBackOrderedOrdersInQuantity;
        //}

        public void InitializeCash(
            decimal cashRefundable,
            decimal cashNonRefundable,
            decimal totalCashCreated_Refundable,
            decimal totalCashCreated_NonRefundable
            )
        {
            _personCashRefundable = cashRefundable;
            _personCashNonRefundable = cashNonRefundable;
            _systemCash_Refundable = totalCashCreated_Refundable;
            _systemCash_NonRefundable = totalCashCreated_NonRefundable;
        }


        public void InitializeSalesOrders(
            decimal personOpenSaleOrdersInMoney,
            double personOpenSaleOrdersInQuantity,

            decimal personClosedSaleOrdersInMoney,
            double personClosedSaleOrdersInQuantity,

            decimal personInProccessSaleOrdersInMoney,
            double personInProccessSaleOrdersInQuantity,

            decimal personCanceledSaleOrdersInMoney,
            double personCanceledSaleOrdersInQuantity,

            decimal personBackSaleOrdersInMoney,
            double personBackSaleOrdersInQuantity,

            decimal personCreditSaleOrdersInMoney,
            double personCreditSaleOrdersInQuantity,

            decimal personQuotationSaleOrdersInMoney,
            double personQuotationSaleOrdersInQuantity,


            decimal systemOpenSaleOrdersInMoney,
            double systemOpenSaleOrdersInQuantity,

            decimal systemClosedSaleOrdersInMoney,
            double systemClosedSaleOrdersInQuantity,

            decimal systemInProccessSaleOrdersInMoney,
            double systemInProccessSaleOrdersInQuantity,

            decimal systemCanceledSaleOrdersInMoney,
            double systemCanceledSaleOrdersInQuantity,

            decimal systemBackSaleOrdersInMoney,
            double systemBackSaleOrdersInQuantity,

            decimal systemCreditSaleOrdersInMoney,
            double systemCreditSaleOrdersInQuantity,

            decimal systemQuotationSaleOrdersInMoney,
            double systemQuotationSaleOrdersInQuantity


            )
        {
            _personOpenSalesOrdersInMoney = personOpenSaleOrdersInMoney;
            _personOpenSalesOrdersInQuantity = personOpenSaleOrdersInQuantity;

            _personClosedSaleOrdersInMoney = personClosedSaleOrdersInMoney;
            _personClosedSaleOrdersInQuantity = systemClosedSaleOrdersInQuantity;

            _personInProccessSaleOrdersInMoney = personInProccessSaleOrdersInMoney;
            _personInProccessSaleOrdersInQuantity = personInProccessSaleOrdersInQuantity;

            _personCanceledSaleOrdersInMoney = personCanceledSaleOrdersInMoney;
            _personCanceledSaleOrdersInQuantity = personCanceledSaleOrdersInQuantity;

            _personBackSaleOrdersInMoney = personBackSaleOrdersInMoney;
            _personBackSaleOrdersInQuantity = personBackSaleOrdersInQuantity;

            _personCreditSaleOrdersInMoney = personCreditSaleOrdersInMoney;
            _personCreditSaleOrdersInQuantity = personCreditSaleOrdersInQuantity;

            _personQuotationSaleOrdersInMoney = personQuotationSaleOrdersInMoney;
            _personQuotationSaleOrdersInQuantity = personQuotationSaleOrdersInQuantity;

            _systemOpenSalesOrdersInMoney = systemOpenSaleOrdersInMoney;
            _systemOpenSalesOrdersInQuantity = systemOpenSaleOrdersInQuantity;

            _systemClosedSaleOrdersInMoney = systemClosedSaleOrdersInMoney;
            _systemClosedSaleOrdersInQuantity = systemClosedSaleOrdersInQuantity;

            _systemInProccessSaleOrdersInMoney = systemInProccessSaleOrdersInMoney;
            _systemInProccessSaleOrdersInQuantity = systemInProccessSaleOrdersInQuantity;

            _systemCanceledSaleOrdersInMoney = systemCanceledSaleOrdersInMoney;
            _systemCanceledSaleOrdersInQuantity = systemCanceledSaleOrdersInQuantity;

            _systemBackSaleOrdersInMoney = systemBackSaleOrdersInMoney;
            _systemBackSaleOrdersInQuantity = systemBackSaleOrdersInQuantity;

            _systemCreditSaleOrdersInMoney = systemCreditSaleOrdersInMoney;
            _systemCreditSaleOrdersInQuantity = systemCreditSaleOrdersInQuantity;

            _systemQuotationSaleOrdersInMoney = systemQuotationSaleOrdersInMoney;
            _systemQuotationSaleOrdersInQuantity = systemQuotationSaleOrdersInQuantity;
        }

        public void InitializePurchaseOrders(
            decimal personOpenPurchaseOrdersInMoney,
            double personOpenPurchaseOrdersInQuantity,

            decimal personClosedPurchaseOrdersInMoney,
            double personClosedPurchaseOrdersInQuantity,

            decimal personInProccessPurchaseOrdersInMoney,
            double personInProccessPurchaseOrdersInQuantity,

            decimal personCanceledPurchaseOrdersInMoney,
            double personCanceledPurchaseOrdersInQuantity,

            decimal personBackPurchaseOrdersInMoney,
            double personBackPurchaseOrdersInQuantity,

            decimal personCreditPurchaseOrdersInMoney,
            double personCreditPurchaseOrdersInQuantity,

            decimal personQuotationPurchaseOrdersInMoney,
            double personQuotationPurchaseOrdersInQuantity,

            decimal systemOpenPurchaseOrdersInMoney,
            double systemOpenPurchaseOrdersInQuantity,

            decimal systemClosedPurchaseOrdersInMoney,
            double systemClosedPurchaseOrdersInQuantity,

            decimal systemInProccessPurchaseOrdersInMoney,
            double systemInProccessPurchaseOrdersInQuantity,

            decimal systemCanceledPurchaseOrdersInMoney,
            double systemCanceledPurchaseOrdersInQuantity,

            decimal systemBackPurchaseOrdersInMoney,
            double systemBackPurchaseOrdersInQuantity,

            decimal systemCreditPurchaseOrdersInMoney,
            double systemCreditPurchaseOrdersInQuantity,

            decimal systemQuotationPurchaseOrdersInMoney,
            double systemQuotationPurchaseOrdersInQuantity



            )
        {

            _systemOpenPurchasesOrdersInMoney = systemOpenPurchaseOrdersInMoney;
            _systemOpenPurchasesOrdersInQuantity = systemOpenPurchaseOrdersInQuantity;

            _systemClosedPurchaseOrdersInMoney = systemClosedPurchaseOrdersInMoney;
            _systemClosedPurchaseOrdersInQuantity = systemClosedPurchaseOrdersInQuantity;

            _systemInProccessPurchaseOrdersInMoney = systemInProccessPurchaseOrdersInMoney;
            _systemInProccessPurchaseOrdersInQuantity = systemInProccessPurchaseOrdersInQuantity;

            _systemCanceledPurchaseOrdersInMoney = systemCanceledPurchaseOrdersInMoney;
            _systemCanceledPurchaseOrdersInQuantity = systemCanceledPurchaseOrdersInQuantity;

            _systemBackPurchaseOrdersInMoney = systemBackPurchaseOrdersInMoney;
            _systemBackPurchaseOrdersInQuantity = systemBackPurchaseOrdersInQuantity;

            _systemCreditPurchaseOrdersInMoney = systemCreditPurchaseOrdersInMoney;
            _systemCreditPurchaseOrdersInQuantity = systemCreditPurchaseOrdersInQuantity;

            _systemQuotationPurchaseOrdersInMoney = systemQuotationPurchaseOrdersInMoney;
            _systemQuotationPurchaseOrdersInQuantity = systemQuotationPurchaseOrdersInQuantity;

            _personOpenPurchasesOrdersInMoney = personOpenPurchaseOrdersInMoney;
            _personOpenPurchasesOrdersInQuantity = personOpenPurchaseOrdersInQuantity;

            _personClosedPurchaseOrdersInMoney = personClosedPurchaseOrdersInMoney;
            _personClosedPurchaseOrdersInQuantity = personClosedPurchaseOrdersInQuantity;

            _personInProccessPurchaseOrdersInMoney = personInProccessPurchaseOrdersInMoney;
            _personInProccessPurchaseOrdersInQuantity = personInProccessPurchaseOrdersInQuantity;

            _personCanceledPurchaseOrdersInMoney = personCanceledPurchaseOrdersInMoney;
            _personCanceledPurchaseOrdersInQuantity = personCanceledPurchaseOrdersInQuantity;

            _personBackPurchaseOrdersInMoney = personBackPurchaseOrdersInMoney;
            _personBackPurchaseOrdersInQuantity = personBackPurchaseOrdersInQuantity;

            _personCreditPurchaseOrdersInMoney = personCreditPurchaseOrdersInMoney;
            _personCreditPurchaseOrdersInQuantity = personCreditPurchaseOrdersInQuantity;

            _personQuotationPurchaseOrdersInMoney = personQuotationPurchaseOrdersInMoney;
            _personQuotationPurchaseOrdersInQuantity = personQuotationPurchaseOrdersInQuantity;
        }

        public void InitializeMisc(bool isAdmin, bool isBank /*, string customerFullName, string ownerFullName */)
        {
            IsAdmin = isAdmin;
            IsBank = isBank;
            //CustomerFullName = customerFullName;
            //OwnerFullName = ownerFullName;
        }

        public bool IsAdmin { get; set; }
        public bool IsBank { get; set; }


        //string CustomerFullName { get; set; }
        //string OwnerFullName { get; set; }

        #region SystemCash
        static decimal _systemCash_NonRefundable;
        static decimal _systemCash_Refundable;


        SystemCashPosition _systemCash;
        public SystemCashPosition SystemCash
        {
            get
            {
                if (_systemCash.IsNull())
                    return (_systemCash = new SystemCashPosition(_systemCash_Refundable, _systemCash_NonRefundable));

                return _systemCash;
            }
        }


        #endregion

        #region Person Cash
        static decimal _personCashRefundable;
        static decimal _personCashNonRefundable;



        PersonCashPosition _personCash;
        public PersonCashPosition PersonCash
        {
            get
            {
                if (_personCash.IsNull())
                    return (_personCash = new PersonCashPosition(_personCashRefundable, _personCashNonRefundable));

                return _personCash;
            }
        }
        #endregion

        #region Person Open Sales Orders

        static decimal _personOpenSalesOrdersInMoney;
        static double _personOpenSalesOrdersInQuantity;
        static decimal _personClosedSaleOrdersInMoney;
        static double _personClosedSaleOrdersInQuantity;
        static decimal _personInProccessSaleOrdersInMoney;
        static double _personInProccessSaleOrdersInQuantity;
        static decimal _personCanceledSaleOrdersInMoney;
        static double _personCanceledSaleOrdersInQuantity;
        static decimal _personBackSaleOrdersInMoney;
        static double _personBackSaleOrdersInQuantity;

        static decimal _personQuotationSaleOrdersInMoney;
        static double _personQuotationSaleOrdersInQuantity;
        static decimal _personCreditSaleOrdersInMoney;
        static double _personCreditSaleOrdersInQuantity;

        public PersonSalesOrders PersonSalesOrders
        {
            get
            {
                return new PersonSalesOrders(
                    _personOpenSalesOrdersInMoney,
                    _personOpenSalesOrdersInQuantity,
                    _personClosedSaleOrdersInMoney,
                    _personClosedSaleOrdersInQuantity,
                    _personInProccessSaleOrdersInMoney,
                    _personInProccessSaleOrdersInQuantity,
                    _personCanceledSaleOrdersInMoney,
                    _personCanceledSaleOrdersInQuantity,
                    _personBackSaleOrdersInMoney,
                    _personBackSaleOrdersInQuantity,
                    _personQuotationSaleOrdersInMoney,
                    _personQuotationSaleOrdersInQuantity,
                    _personCreditSaleOrdersInMoney,
                    _personCreditSaleOrdersInQuantity);
            }
        }

        #endregion

        #region System Sales Orders

        static decimal _systemOpenSalesOrdersInMoney;
        static double _systemOpenSalesOrdersInQuantity;
        static decimal _systemClosedSaleOrdersInMoney;
        static double _systemClosedSaleOrdersInQuantity;
        static decimal _systemInProccessSaleOrdersInMoney;
        static double _systemInProccessSaleOrdersInQuantity;
        static decimal _systemCanceledSaleOrdersInMoney;
        static double _systemCanceledSaleOrdersInQuantity;
        static decimal _systemBackSaleOrdersInMoney;
        static double _systemBackSaleOrdersInQuantity;

        static decimal _systemQuotationSaleOrdersInMoney;
        static double _systemQuotationSaleOrdersInQuantity;
        static decimal _systemCreditSaleOrdersInMoney;
        static double _systemCreditSaleOrdersInQuantity;
        public SystemSalesOrders SystemSalesOrders
        {
            get
            {
                return new SystemSalesOrders(
                    _systemOpenSalesOrdersInMoney,
                    _systemOpenSalesOrdersInQuantity,
                    _systemClosedSaleOrdersInMoney,
                    _systemClosedSaleOrdersInQuantity,
                    _systemInProccessSaleOrdersInMoney,
                    _systemInProccessSaleOrdersInQuantity,
                    _systemCanceledSaleOrdersInMoney,
                    _systemCanceledSaleOrdersInQuantity,
                    _systemBackSaleOrdersInMoney,
                    _systemBackSaleOrdersInQuantity,
                    _systemQuotationSaleOrdersInMoney,
                    _systemQuotationSaleOrdersInQuantity,
                    _systemCreditSaleOrdersInMoney,
                    _systemCreditSaleOrdersInQuantity);

            }
        }

        #endregion

        #region Person Purchase Orders

        static decimal _personOpenPurchasesOrdersInMoney;
        static double _personOpenPurchasesOrdersInQuantity;
        static decimal _personClosedPurchaseOrdersInMoney;
        static double _personClosedPurchaseOrdersInQuantity;
        static decimal _personInProccessPurchaseOrdersInMoney;
        static double _personInProccessPurchaseOrdersInQuantity;
        static decimal _personCanceledPurchaseOrdersInMoney;
        static double _personCanceledPurchaseOrdersInQuantity;
        static decimal _personBackPurchaseOrdersInMoney;
        static double _personBackPurchaseOrdersInQuantity;

        static decimal _personQuotationPurchaseOrdersInMoney;
        static double _personQuotationPurchaseOrdersInQuantity;
        static decimal _personCreditPurchaseOrdersInMoney;
        static double _personCreditPurchaseOrdersInQuantity;

        public PersonPurchaseOrders PersonPurchaseOrders
        {
            get
            {
                return new PersonPurchaseOrders(
                    _personOpenPurchasesOrdersInMoney,
                    _personOpenPurchasesOrdersInQuantity,
                    _personClosedPurchaseOrdersInMoney,
                    _personClosedPurchaseOrdersInQuantity,
                    _personInProccessPurchaseOrdersInMoney,
                    _personInProccessPurchaseOrdersInQuantity,
                    _personCanceledPurchaseOrdersInMoney,
                    _personCanceledPurchaseOrdersInQuantity,
                    _personBackPurchaseOrdersInMoney,
                    _personBackPurchaseOrdersInQuantity,
                    _personQuotationPurchaseOrdersInMoney,
                    _personQuotationPurchaseOrdersInQuantity,
                    _personCreditPurchaseOrdersInMoney,
                    _personCreditPurchaseOrdersInQuantity);
            }
        }

        #endregion

        #region System Purchase Orders
        static decimal _systemOpenPurchasesOrdersInMoney;
        static double _systemOpenPurchasesOrdersInQuantity;
        static decimal _systemClosedPurchaseOrdersInMoney;
        static double _systemClosedPurchaseOrdersInQuantity;
        static decimal _systemInProccessPurchaseOrdersInMoney;
        static double _systemInProccessPurchaseOrdersInQuantity;
        static decimal _systemCanceledPurchaseOrdersInMoney;
        static double _systemCanceledPurchaseOrdersInQuantity;
        static decimal _systemBackPurchaseOrdersInMoney;
        static double _systemBackPurchaseOrdersInQuantity;

        static decimal _systemQuotationPurchaseOrdersInMoney;
        static double _systemQuotationPurchaseOrdersInQuantity;
        static decimal _systemCreditPurchaseOrdersInMoney;
        static double _systemCreditPurchaseOrdersInQuantity;

        public SystemPurchaseOrders SystemPurchaseOrders
        {
            get
            {
                return new SystemPurchaseOrders(
                    _systemOpenPurchasesOrdersInMoney,
                    _systemOpenPurchasesOrdersInQuantity,
                    _systemClosedPurchaseOrdersInMoney,
                    _systemClosedPurchaseOrdersInQuantity,
                    _systemInProccessPurchaseOrdersInMoney,
                    _systemInProccessPurchaseOrdersInQuantity,
                    _systemCanceledPurchaseOrdersInMoney,
                    _systemCanceledPurchaseOrdersInQuantity,
                    _systemBackPurchaseOrdersInMoney,
                    _systemBackPurchaseOrdersInQuantity);
            }
        }

        #endregion

    }
}
