using System.Configuration;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class PersonCashNonRefundableClass : IMenuItemHelper
    {
        public PersonCashNonRefundableClass()
        {

        }
        public PersonCashNonRefundableClass(decimal amount)
        {
            Amount = amount;
        }

        decimal Amount { get; set; }
        public string MenuItem
        {
            get
            {
                string content = ConfigurationManager.AppSettings["menu.person_CashNonRefundable_MenuItem"];
                string str = string.Format(content, Amount);
                return str;

            }
        }

        public string ToolTip
        {
            get
            {
                string str = ConfigurationManager.AppSettings["menu.person_CashNonRefundable_ToolTip"];
                string toolTip = string.Format(str, Amount);
                return toolTip;
            }
        }



    }
}
