using MenuItemHelperNS;

namespace ModelsClassLibrary.ModelsNS.SharedNS.MenuItemHelperNS
{
    public class PersonSalesOrders : IOrdersInfo
    {
        public PersonSalesOrders(
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
            decimal personQuotationsInMoney,
            double personQoutationsInQuantity,
            decimal personCreditsInMoney,
            double personCreditsInQuantity)
        {

            Initialize(
                personOpenSaleOrdersInMoney,
                personOpenSaleOrdersInQuantity,

                personClosedSaleOrdersInMoney,
                personClosedSaleOrdersInQuantity,

                personInProccessSaleOrdersInMoney,
                personInProccessSaleOrdersInQuantity,

                personCanceledSaleOrdersInMoney,
                personCanceledSaleOrdersInQuantity,

                personBackSaleOrdersInMoney,
                personBackSaleOrdersInQuantity,

                personQuotationsInMoney,
                personQoutationsInQuantity,

                personCreditsInMoney,
                personCreditsInQuantity);

        }
        public void Initialize(
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

            decimal personQuotationsInMoney,
            double personQoutationsInQuantity,

            decimal personCreditsInMoney,
            double personCreditsInQuantity)
        {
            _personOpenSalesOrdersInMoney = personOpenSaleOrdersInMoney;
            _personOpenSalesOrdersInQuantity = personOpenSaleOrdersInQuantity;

            _personClosedSaleOrdersInMoney = personClosedSaleOrdersInMoney;
            _personClosedSaleOrdersInQuantity = personClosedSaleOrdersInQuantity;

            _personInProccessSaleOrdersInMoney = personInProccessSaleOrdersInMoney;
            _personInProccessSaleOrdersInQuantity = personInProccessSaleOrdersInQuantity;

            _personCanceledSaleOrdersInMoney = personCanceledSaleOrdersInMoney;
            _personCanceledSaleOrdersInQuantity = personCanceledSaleOrdersInQuantity;

            _personBackSaleOrdersInMoney = personBackSaleOrdersInMoney;
            _personBackSaleOrdersInQuantity = personBackSaleOrdersInQuantity;



            _personQuotationsInMoney = personQuotationsInMoney;
            _personQuotationsInQuantity = personQoutationsInQuantity;

            _personCreditsInMoney = personCreditsInMoney;
            _personCreditsInQuantity = personCreditsInQuantity;

        }

        static decimal _personOpenSalesOrdersInMoney;
        static double _personOpenSalesOrdersInQuantity;
        public IMenuItemHelper Open
        {
            get
            {
                PersonOpenSaleOrders item = new PersonOpenSaleOrders(_personOpenSalesOrdersInMoney, _personOpenSalesOrdersInQuantity);
                return item;
            }
        }

        //----------------------------------------------------------------

        static decimal _personClosedSaleOrdersInMoney { get; set; }
        static double _personClosedSaleOrdersInQuantity { get; set; }
        public IMenuItemHelper Closed
        {
            get
            {
                PersonClosedSaleOrders item = new PersonClosedSaleOrders(_personClosedSaleOrdersInMoney, _personClosedSaleOrdersInQuantity);
                return item;
            }
        }

        //----------------------------------------------------------------



        static decimal _personInProccessSaleOrdersInMoney { get; set; }
        static double _personInProccessSaleOrdersInQuantity { get; set; }
        public IMenuItemHelper InProccesss
        {
            get
            {
                PersonInProccessSaleOrders item = new PersonInProccessSaleOrders(_personInProccessSaleOrdersInMoney, _personInProccessSaleOrdersInQuantity);
                return item;
            }
        }


        //----------------------------------------------------------------

        static decimal _personCanceledSaleOrdersInMoney { get; set; }
        static double _personCanceledSaleOrdersInQuantity { get; set; }
        public IMenuItemHelper Canceled
        {
            get
            {
                PersonCanceledSaleOrders item = new PersonCanceledSaleOrders(_personCanceledSaleOrdersInMoney, _personCanceledSaleOrdersInQuantity);
                return item;
            }
        }


        //----------------------------------------------------------------


        static decimal _personBackSaleOrdersInMoney;
        static double _personBackSaleOrdersInQuantity;
        public IMenuItemHelper BackOrdered
        {
            get
            {
                PersonBackSaleOrders item = new PersonBackSaleOrders(_personBackSaleOrdersInMoney, _personBackSaleOrdersInQuantity);
                return item;
            }
        }



        //---------------------------------------------------
        static decimal _personQuotationsInMoney;
        static double _personQuotationsInQuantity;
        public IMenuItemHelper Quotation
        {
            get
            {
                PersonQuotationSaleOrders item = new PersonQuotationSaleOrders(_personQuotationsInMoney, _personQuotationsInQuantity);
                return item;
            }
        }

        static decimal _personCreditsInMoney;
        static double _personCreditsInQuantity;
        public IMenuItemHelper Credit
        {
            get
            {
                PersonCreditOrders item = new PersonCreditOrders(_personCreditsInMoney, _personCreditsInQuantity);
                return item;
            }
        }

        static decimal _personTotalSalesOrdersInMoney;
        static double _personTotalSalesOrdersInQuantity;

        public IMenuItemHelper Total
        {
            get
            {
                _personTotalSalesOrdersInMoney = _personOpenSalesOrdersInMoney + _personBackSaleOrdersInMoney + _personInProccessSaleOrdersInMoney;
                _personTotalSalesOrdersInQuantity = _personOpenSalesOrdersInQuantity + _personBackSaleOrdersInQuantity + _personInProccessSaleOrdersInQuantity;
                PersonTotalSaleOrders item = new PersonTotalSaleOrders(_personTotalSalesOrdersInMoney, _personTotalSalesOrdersInQuantity);
                return item;
            }
        }
    }
}
