using MenuItemHelperNS;

namespace ModelsClassLibrary.ModelsNS.SharedNS.MenuItemHelperNS
{
    public class PersonPurchaseOrders : IOrdersInfo
    {
        public PersonPurchaseOrders(
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

            decimal personQuotationPurchaseOrdersInMoney,
            double personQuotationPurchaseOrdersInQuantity,
            decimal personCreditPurchaseOrdersInMoney,
            double personCreditPurchaseOrdersInQuantity)
        {
            Initialize(
                personOpenPurchaseOrdersInMoney,
                personOpenPurchaseOrdersInQuantity,

                personClosedPurchaseOrdersInMoney,
                personClosedPurchaseOrdersInQuantity,

                personInProccessPurchaseOrdersInMoney,
                personInProccessPurchaseOrdersInQuantity,

                personCanceledPurchaseOrdersInMoney,
                personCanceledPurchaseOrdersInQuantity,

                personBackPurchaseOrdersInMoney,
                personBackPurchaseOrdersInQuantity,

                personQuotationPurchaseOrdersInMoney,
                personQuotationPurchaseOrdersInQuantity,

                personCreditPurchaseOrdersInMoney,
                personCreditPurchaseOrdersInQuantity);

        }
        public void Initialize(
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

            decimal personQuotationPurchaseOrdersInMoney,
            double personQuotationPurchaseOrdersInQuantity,

            decimal personCreditPurchaseOrdersInMoney,
            double personCreditPurchaseOrdersInQuantity)
        {
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

            _personQuotationPurchaseOrdersInMoney = personQuotationPurchaseOrdersInMoney;
            _personQuotationPurchaseOrdersInQuantity = personQuotationPurchaseOrdersInQuantity;

            _personCreditPurchaseOrdersInMoney = personCreditPurchaseOrdersInMoney;
            _personCreditPurchaseOrdersInQuantity = personCreditPurchaseOrdersInQuantity;


        }


        static decimal _personOpenPurchasesOrdersInMoney;
        static double _personOpenPurchasesOrdersInQuantity;
        public IMenuItemHelper Open
        {
            get
            {
                PersonOpenPurchaseOrders item = new PersonOpenPurchaseOrders(_personOpenPurchasesOrdersInMoney, _personOpenPurchasesOrdersInQuantity);
                return item;
            }
        }

        //----------------------------------------------------------------

        static decimal _personClosedPurchaseOrdersInMoney { get; set; }
        static double _personClosedPurchaseOrdersInQuantity { get; set; }
        public IMenuItemHelper Closed
        {
            get
            {
                PersonClosedPurchaseOrders item = new PersonClosedPurchaseOrders(_personClosedPurchaseOrdersInMoney, _personClosedPurchaseOrdersInQuantity);
                return item;
            }
        }

        //----------------------------------------------------------------



        static decimal _personInProccessPurchaseOrdersInMoney { get; set; }
        static double _personInProccessPurchaseOrdersInQuantity { get; set; }
        public IMenuItemHelper InProccesss
        {
            get
            {
                PersonInProccessPurchaseOrders item = new PersonInProccessPurchaseOrders(_personInProccessPurchaseOrdersInMoney, _personInProccessPurchaseOrdersInQuantity);
                return item;
            }
        }


        //----------------------------------------------------------------

        static decimal _personCanceledPurchaseOrdersInMoney { get; set; }
        static double _personCanceledPurchaseOrdersInQuantity { get; set; }
        public IMenuItemHelper Canceled
        {
            get
            {
                PersonCanceledPurchaseOrders item = new PersonCanceledPurchaseOrders(_personCanceledPurchaseOrdersInMoney, _personCanceledPurchaseOrdersInQuantity);
                return item;
            }
        }


        //----------------------------------------------------------------


        static decimal _personBackPurchaseOrdersInMoney;
        static double _personBackPurchaseOrdersInQuantity;
        public IMenuItemHelper BackOrdered
        {
            get
            {
                PersonBackPurchaseOrders item = new PersonBackPurchaseOrders(_personBackPurchaseOrdersInMoney, _personBackPurchaseOrdersInQuantity);
                return item;
            }
        }

        static decimal _personQuotationPurchaseOrdersInMoney;
        static double _personQuotationPurchaseOrdersInQuantity;
        public IMenuItemHelper Quotation
        {
            get
            {
                PersonQuotationSaleOrders item = new PersonQuotationSaleOrders(_personQuotationPurchaseOrdersInMoney, _personQuotationPurchaseOrdersInQuantity);
                return item;
            }
        }

        static decimal _personCreditPurchaseOrdersInMoney;
        static double _personCreditPurchaseOrdersInQuantity;
        public IMenuItemHelper Credit
        {
            get
            {
                PersonCreditOrders item = new PersonCreditOrders(_personCreditPurchaseOrdersInMoney, _personCreditPurchaseOrdersInQuantity);
                return item;
            }
        }



        static decimal _personTotalPurchaseOrdersInMoney;
        static double _personTotalPurchaseOrdersInQuantity;
        public IMenuItemHelper Total
        {
            get
            {
                _personTotalPurchaseOrdersInMoney = _personOpenPurchasesOrdersInMoney + _personInProccessPurchaseOrdersInMoney + _personBackPurchaseOrdersInMoney;
                _personTotalPurchaseOrdersInQuantity = _personOpenPurchasesOrdersInQuantity + _personInProccessPurchaseOrdersInQuantity + _personBackPurchaseOrdersInQuantity;
                PersonTotalPurchaseOrders item = new PersonTotalPurchaseOrders(_personTotalPurchaseOrdersInMoney, _personTotalPurchaseOrdersInQuantity);
                return item;
            }
        }
    }
}
