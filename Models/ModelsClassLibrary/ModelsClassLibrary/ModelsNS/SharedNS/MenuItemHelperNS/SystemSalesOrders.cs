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
            double systemBackSaleOrdersInQuantity)
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
                systemBackSaleOrdersInQuantity);

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
            double systemBackSaleOrdersInQuantity)
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
    }
}
