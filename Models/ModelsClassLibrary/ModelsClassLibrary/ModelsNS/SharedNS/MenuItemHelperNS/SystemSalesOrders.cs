using MenuItemHelperNS;

namespace ModelsClassLibrary.ModelsNS.SharedNS.MenuItemHelperNS
{
    public class SystemSalesOrders : IOrdersInfo
    {
        public SystemSalesOrders(
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
            decimal systemQuotationsInMoney,
            double systemQoutationsInQuantity,
            decimal systemCreditsInMoney,
            double systemCreditsInQuantity)
        {
            Initialize(
                systemOpenSaleOrdersInMoney,
                systemOpenSaleOrdersInQuantity,

                systemClosedSaleOrdersInMoney,
                systemClosedSaleOrdersInQuantity,

                systemInProccessSaleOrdersInMoney,
                systemInProccessSaleOrdersInQuantity,

                systemCanceledSaleOrdersInMoney,
                systemCanceledSaleOrdersInQuantity,

                systemBackSaleOrdersInMoney,
                systemBackSaleOrdersInQuantity,

                systemQuotationsInMoney,
                systemQoutationsInQuantity,

                systemCreditsInMoney,
                systemCreditsInQuantity);

        }
        public void Initialize(
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
            decimal systemQuotationsInMoney,
            double systemQoutationsInQuantity,
            decimal systemCreditsInMoney,
            double systemCreditsInQuantity)
        {
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

            _systemQuotationsInMoney = systemQuotationsInMoney;
            _systemQuotationsInQuantity = systemQoutationsInQuantity;

            _systemSaleOrderCreditsInMoney = systemCreditsInMoney;
            _systemSaleOrderCreditsInQuantity = systemCreditsInQuantity;

        }

        static decimal _systemOpenSalesOrdersInMoney;
        static double _systemOpenSalesOrdersInQuantity;
        public IMenuItemHelper Open
        {
            get
            {
                SystemOpenSaleOrders item = new SystemOpenSaleOrders(_systemOpenSalesOrdersInMoney, _systemOpenSalesOrdersInQuantity);
                return item;
            }
        }

        //----------------------------------------------------------------

        static decimal _systemClosedSaleOrdersInMoney { get; set; }
        static double _systemClosedSaleOrdersInQuantity { get; set; }
        public IMenuItemHelper Closed
        {
            get
            {
                SystemClosedSaleOrders item = new SystemClosedSaleOrders(_systemClosedSaleOrdersInMoney, _systemClosedSaleOrdersInQuantity);
                return item;
            }
        }

        //----------------------------------------------------------------



        static decimal _systemInProccessSaleOrdersInMoney { get; set; }
        static double _systemInProccessSaleOrdersInQuantity { get; set; }
        public IMenuItemHelper InProccesss
        {
            get
            {
                SystemInProccessSaleOrders item = new SystemInProccessSaleOrders(_systemInProccessSaleOrdersInMoney, _systemInProccessSaleOrdersInQuantity);
                return item;
            }
        }


        //----------------------------------------------------------------

        static decimal _systemCanceledSaleOrdersInMoney { get; set; }
        static double _systemCanceledSaleOrdersInQuantity { get; set; }
        public IMenuItemHelper Canceled
        {
            get
            {
                SystemCanceledSaleOrders item = new SystemCanceledSaleOrders(_systemCanceledSaleOrdersInMoney, _systemCanceledSaleOrdersInQuantity);
                return item;
            }
        }


        //----------------------------------------------------------------


        static decimal _systemBackSaleOrdersInMoney;
        static double _systemBackSaleOrdersInQuantity;
        public IMenuItemHelper BackOrdered
        {
            get
            {
                SystemBackSaleOrders item = new SystemBackSaleOrders(_systemBackSaleOrdersInMoney, _systemBackSaleOrdersInQuantity);
                return item;
            }    
        }
        //---------------------------------------------------      
        static decimal _systemQuotationsInMoney;
        static double _systemQuotationsInQuantity;  
        public IMenuItemHelper Quotation
        {
            get
            {
                SystemQuotationOrders item = new SystemQuotationOrders(_systemQuotationsInMoney, _systemQuotationsInQuantity);
                return item;
            }
        }

        static decimal _systemSaleOrderCreditsInMoney;
        static double _systemSaleOrderCreditsInQuantity;
        public IMenuItemHelper Credit
        {
            get
            {
                SystemCreditSaleOrders item = new SystemCreditSaleOrders(_systemSaleOrderCreditsInMoney, _systemSaleOrderCreditsInQuantity);
                return item;
            }
        }
        static decimal _systemSaleOrderTotalnMoney;
        static double _systemSaleOrderTotalQuantity;
        public IMenuItemHelper Total
        {
            get
            {
                _systemSaleOrderTotalnMoney = _systemBackSaleOrdersInMoney + _systemInProccessSaleOrdersInMoney + _systemOpenSalesOrdersInMoney;
                _systemSaleOrderTotalQuantity = _systemBackSaleOrdersInQuantity + _systemInProccessSaleOrdersInQuantity + _systemOpenSalesOrdersInQuantity;
                SystemTotalSaleOrders item = new SystemTotalSaleOrders(
                    _systemSaleOrderTotalnMoney,
                    _systemSaleOrderTotalQuantity);
                return item;
            }
        }
    }
}
