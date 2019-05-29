using System.Configuration;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class PersonBackSaleOrders : IMenuItemHelper
    {
        public PersonBackSaleOrders()
        {

        }
        public PersonBackSaleOrders(decimal money, double quantity)
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
                string content = ConfigurationManager.AppSettings["menu.person.SaleOrders.Back.MenuItem"];
                string str = string.Format(content, Money, Quantity);
                return str;

            }
        }

        public string ToolTip
        {
            get
            {
                string content = ConfigurationManager.AppSettings["menu.person.SaleOrders.Back.ToolTip"];
                string str = string.Format(content, Money, Quantity);
                return str;
            }
        }



    }
}
