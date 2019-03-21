using System.Configuration;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class PersonCanceledPurchaseOrders : IMenuItemHelper
    {
        public PersonCanceledPurchaseOrders()
        {

        }
        public PersonCanceledPurchaseOrders(decimal money, double quantity)
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
                string content = ConfigurationManager.AppSettings["menu.person_CanceledPurchaseOrders_MenuItem"];
                string str = string.Format(content, Money, Quantity);
                return str;

            }
        }

        public string ToolTip
        {
            get
            {
                string content = ConfigurationManager.AppSettings["menu.person_CanceledPurchaseOrders_ToolTip"];
                string str = string.Format(content, Money, Quantity);
                return str;
            }
        }



    }
}
