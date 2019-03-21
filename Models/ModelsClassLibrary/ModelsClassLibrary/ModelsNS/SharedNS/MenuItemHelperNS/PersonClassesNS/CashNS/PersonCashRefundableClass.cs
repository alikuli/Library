using System.Configuration;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class PersonCashRefundableClass : IMenuItemHelper
    {
        public PersonCashRefundableClass()
        {

        }
        public PersonCashRefundableClass(decimal amount)
        {
            Amount = amount;
        }

        decimal Amount { get; set; }
        public string MenuItem
        {
            get
            {
                string content = ConfigurationManager.AppSettings["menu.person_CashRefundable_MenuItem"];
                string str = string.Format(content, Amount);
                return str;

            }
        }

        public string ToolTip
        {
            get
            {
                string str = ConfigurationManager.AppSettings["menu.person_CashRefundable_ToolTip"];
                string toolTip = string.Format(str, Amount);
                return toolTip;
            }
        }



    }
}
