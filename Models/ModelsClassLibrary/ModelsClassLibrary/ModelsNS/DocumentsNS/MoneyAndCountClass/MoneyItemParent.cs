

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.MoneyAndCountClass
{

    /// <summary>
    /// This is the class that will controll how data is added it. It carries all the data as well for
    /// all the Document State types
    /// </summary>
    public class MoneyItemParent
    {
        public MoneyItemParent()
        {
            Money_User = new MoneyType();
            Money_System = new MoneyType();
            Sale = new OrderTypes();
            Sale_Admin = new OrderTypes();
            Purchase = new OrderTypes();
            Purchase_Admin = new OrderTypes();
        }

        public MoneyItemParent(string userId, string isAdmin):this()
        {
            UserId = userId;
            IsAdmin = isAdmin;
        }

        //This is the userId that is being used
        public string UserId { get; private set; }

        //True if admin
        public string IsAdmin { get; private set; }

        public MoneyType Money_User { get; set; }
        public MoneyType Money_System { get; set; }

        public OrderTypes Sale { get; set; }
        public OrderTypes Sale_Admin { get; set; }

        public OrderTypes Purchase { get; set; }
        public OrderTypes Purchase_Admin { get; set; }



    }

}
