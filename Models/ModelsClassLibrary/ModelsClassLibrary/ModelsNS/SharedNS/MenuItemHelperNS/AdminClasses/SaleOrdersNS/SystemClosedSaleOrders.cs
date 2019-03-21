using System.Configuration;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class SystemClosedSaleOrders : IMenuItemHelper
    {
        public SystemClosedSaleOrders()
        {

        }
        public SystemClosedSaleOrders(decimal money, double quantity)
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
                string content = ConfigurationManager.AppSettings["menu.system_ClosedOrders_MenuItem"];
                string str = string.Format(content, Money, Quantity);
                return str;

            }
        }

        public string ToolTip
        {
            get
            {
                string content = ConfigurationManager.AppSettings["menu.system_ClosedOrders_ToolTip"];
                string str = string.Format(content, Money, Quantity);
                return str;
            }
        }



    }
}
