

using ModelsClassLibrary.ModelsNS.DocumentsNS.GlobalObjectNS;
namespace ModelsClassLibrary.ModelsNS.GlobalObjectNS
{

    /// <summary>
    /// This is the class that will controll how data is added it. It carries all the data as well for
    /// all the Document State types
    /// </summary>
    public class GlobalObject
    {
        public GlobalObject()
        {
            Money_User = new MoneyInfo();
            Money_System = new MoneyInfo();
            Sale = new OrderTypes();
            Sale_Admin = new OrderTypes();
            Purchase = new OrderTypes();
            Purchase_Admin = new OrderTypes();
            Delivery = new OrderTypes();
            Delivery_Admin = new OrderTypes();
            Salesman = new OrderTypes();
            Salesman_Admin = new OrderTypes();

        }

        public GlobalObject(string userId, bool isAdmin, bool isCustomer, bool isOwner, bool isDeliveryman, bool isSalesman, bool isLoggedIn)
            : this()
        {
            UserId = userId;
            _isAdmin = isAdmin;
            _isOwner = isOwner;
            _isCustomer = isCustomer;
            _isDeliveryman = isDeliveryman;
            _isSalesman = isSalesman;
            _isLoggedIn = isLoggedIn;
        }

        bool _isLoggedIn;
        public bool IsLoggedIn { get { return _isLoggedIn; } }

        //This is the userId that is being used
        string UserId { get; set; }

        //True if admin
        bool _isAdmin;
        public bool IsAdmin { get { return _isAdmin; } }

        bool _isCustomer;
        public bool IsCustomer { get { return _isCustomer; } }

        bool _isOwner;
        public bool IsOwner { get { return _isOwner; } }

        bool _isDeliveryman;
        public bool IsDeliveryman { get { return _isDeliveryman; } }

        bool _isSalesman;
        public bool IsSalesman { get { return _isSalesman; } }

        public MoneyInfo Money_User { get; set; }
        public MoneyInfo Money_System { get; set; }

        public OrderTypes Sale { get; set; }
        public OrderTypes Sale_Admin { get; set; }

        public OrderTypes Purchase { get; set; }
        public OrderTypes Purchase_Admin { get; set; }

        public OrderTypes Delivery { get; set; }
        public OrderTypes Delivery_Admin { get; set; }

        public OrderTypes Salesman { get; set; }
        public OrderTypes Salesman_Admin { get; set; }


    }

}
