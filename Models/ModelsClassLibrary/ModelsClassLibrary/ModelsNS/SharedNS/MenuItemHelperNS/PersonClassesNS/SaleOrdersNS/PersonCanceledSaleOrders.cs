using System.Configuration;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class PersonCanceledSaleOrders : IMenuItemHelper
    {
        public PersonCanceledSaleOrders()
        {

        }
        public PersonCanceledSaleOrders(decimal money, double quantity)
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
                string content = ConfigurationManager.AppSettings["menu.personCanceledSaleOrders_MenuItem"];
                string str = string.Format(content, Money, Quantity);
                return str;

            }
        }

        public string ToolTip
        {
            get
            {
                string content = ConfigurationManager.AppSettings["menu.personCanceledSaleOrders_ToolTip"];
                string str = string.Format(content, Money, Quantity);
                return str;
            }
        }



    }
}
