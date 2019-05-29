using System.Configuration;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class SystemClosedPurchaseOrders : IMenuItemHelper
    {
        public SystemClosedPurchaseOrders()
        {

        }
        public SystemClosedPurchaseOrders(decimal money, double quantity)
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
                string content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.Closed.MenuItem"];
                string str = string.Format(content, Money, Quantity);
                return str;

            }
        }

        public string ToolTip
        {
            get
            {
                string content = ConfigurationManager.AppSettings["menu.system.PurchaseOrders.Closed.ToolTip"];
                string str = string.Format(content, Money, Quantity);
                return str;
            }
        }



    }
}
