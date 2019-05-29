using System.Configuration;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class PersonInProccessPurchaseOrders : IMenuItemHelper
    {
        public PersonInProccessPurchaseOrders()
        {

        }
        public PersonInProccessPurchaseOrders(decimal money, double quantity)
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
                string content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.InProccess.MenuItem"];
                string str = string.Format(content, Money, Quantity);
                return str;

            }
        }

        public string ToolTip
        {
            get
            {
                string content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.InProccess.ToolTip"];
                string str = string.Format(content, Money, Quantity);
                return str;
            }
        }



    }
}

