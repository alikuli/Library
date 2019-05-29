using System.Configuration;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class PersonClosedPurchaseOrders : IMenuItemHelper
    {
        public PersonClosedPurchaseOrders()
        {

        }
        public PersonClosedPurchaseOrders(decimal money, double quantity)
        {
            Money = money;
            Quantity = quantity;
        }

        decimal Money { get; set; }
        double Quantity { get; set; }
        public string MenuItem
        {
            get
            {
                string content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Closed.MenuItem"];
                string str = string.Format(content, Money, Quantity);
                return str;

            }
        }

        public string ToolTip
        {
            get
            {
                string content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Closed.ToolTip"];
                string str = string.Format(content, Money, Quantity);
                return str;
            }
        }



    }
}

