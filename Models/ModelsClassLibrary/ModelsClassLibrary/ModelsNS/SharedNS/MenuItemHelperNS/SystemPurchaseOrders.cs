using MenuItemHelperNS;

namespace ModelsClassLibrary.ModelsNS.SharedNS.MenuItemHelperNS
{
    public class SystemPurchaseOrders : IOrdersInfo
    {
        public SystemPurchaseOrders(
            decimal systemOpenPurchaseOrdersInMoney,
            double systemOpenPurchaseOrdersInQuantity,

            decimal systemClosedPurchaseOrdersInMoney,
            double systemClosedPurchaseOrdersInQuantity,

            decimal systemInProccessPurchaseOrdersInMoney,
            double systemInProccessPurchaseOrdersInQuantity,

            decimal systemCanceledPurchaseOrdersInMoney,
            double systemCanceledPurchaseOrdersInQuantity,

            decimal systemBackPurchaseOrdersInMoney,
            double systemBackPurchaseOrdersInQuantity)
        {
            Initialize(
                systemOpenPurchaseOrdersInMoney,
                systemOpenPurchaseOrdersInQuantity,

                systemClosedPurchaseOrdersInMoney,
                systemClosedPurchaseOrdersInQuantity,

                systemInProccessPurchaseOrdersInMoney,
                systemInProccessPurchaseOrdersInQuantity,

                systemCanceledPurchaseOrdersInMoney,
                systemCanceledPurchaseOrdersInQuantity,

                systemBackPurchaseOrdersInMoney,
                systemBackPurchaseOrdersInQuantity);

        }
        public void Initialize(
            decimal systemOpenPurchaseOrdersInMoney,
            double systemOpenPurchaseOrdersInQuantity,

            decimal systemClosedPurchaseOrdersInMoney,
            double systemClosedPurchaseOrdersInQuantity,

            decimal systemInProccessPurchaseOrdersInMoney,
            double systemInProccessPurchaseOrdersInQuantity,

            decimal systemCanceledPurchaseOrdersInMoney,
            double systemCanceledPurchaseOrdersInQuantity,

            decimal systemBackPurchaseOrdersInMoney,
            double systemBackPurchaseOrdersInQuantity)
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
        }

        static decimal _systemOpenPurchasesOrdersInMoney;
        static double _systemOpenPurchasesOrdersInQuantity;
        public IMenuItemHelper Open
        {
            get
            {
                SystemOpenPurchaseOrders item = new SystemOpenPurchaseOrders(_systemOpenPurchasesOrdersInMoney, _systemOpenPurchasesOrdersInQuantity);
                return item;
            }
        }

        //----------------------------------------------------------------

        static decimal _systemClosedPurchaseOrdersInMoney { get; set; }
        static double _systemClosedPurchaseOrdersInQuantity { get; set; }
        public IMenuItemHelper Closed
        {
            get
            {
                SystemClosedPurchaseOrders item = new SystemClosedPurchaseOrders(_systemClosedPurchaseOrdersInMoney, _systemClosedPurchaseOrdersInQuantity);
                return item;
            }
        }

        //----------------------------------------------------------------



        static decimal _systemInProccessPurchaseOrdersInMoney { get; set; }
        static double _systemInProccessPurchaseOrdersInQuantity { get; set; }
        public IMenuItemHelper InProccesss
        {
            get
            {
                SystemInProccessPurchaseOrders item = new SystemInProccessPurchaseOrders(_systemInProccessPurchaseOrdersInMoney, _systemInProccessPurchaseOrdersInQuantity);
                return item;
            }
        }


        //----------------------------------------------------------------

        static decimal _systemCanceledPurchaseOrdersInMoney { get; set; }
        static double _systemCanceledPurchaseOrdersInQuantity { get; set; }
        public IMenuItemHelper Canceled
        {
            get
            {
                SystemCanceledPurchaseOrders item = new SystemCanceledPurchaseOrders(_systemCanceledPurchaseOrdersInMoney, _systemCanceledPurchaseOrdersInQuantity);
                return item;
            }
        }


        //----------------------------------------------------------------


        static decimal _systemBackPurchaseOrdersInMoney;
        static double _systemBackPurchaseOrdersInQuantity;
        public IMenuItemHelper BackOrdered
        {
            get
            {
                SystemBackPurchaseOrders item = new SystemBackPurchaseOrders(_systemBackPurchaseOrdersInMoney, _systemBackPurchaseOrdersInQuantity);
                return item;
            }
        }
    }
}
