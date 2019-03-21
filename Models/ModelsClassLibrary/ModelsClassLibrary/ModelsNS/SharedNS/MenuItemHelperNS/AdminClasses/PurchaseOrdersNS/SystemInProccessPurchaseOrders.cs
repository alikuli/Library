using System.Configuration;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class SystemInProccessPurchaseOrders : IMenuItemHelper
    {
        public SystemInProccessPurchaseOrders()
        {

        }
        public SystemInProccessPurchaseOrders(decimal money, double quantity)
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
                string content = ConfigurationManager.AppSettings["menu.system_InProccessPurchaseOrders_MenuItem"];
                string str = string.Format(content, Money, Quantity);
                return str;

            }
        }

        public string ToolTip
        {
            get
            {
                string content = ConfigurationManager.AppSettings["menu.system_InProccessPurchaseOrders_ToolTip"];
                string str = string.Format(content, Money, Quantity);
                return str;
            }
        }



    }
}
