using System.Configuration;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class PersonCashRefundable_AmountOnlyClass : ModelsClassLibrary.ModelsNS.SharedNS.IMenuItemHelper
    {
        public PersonCashRefundable_AmountOnlyClass()
        {

        }
        public PersonCashRefundable_AmountOnlyClass(decimal amount)
        {
            Amount = amount;
        }

        decimal Amount { get; set; }
        public string MenuItem
        {
            get
            {
                string content = ConfigurationManager.AppSettings["menu.personRefundable_AmountOnly"];
                string str = string.Format(content, Amount);
                return str;

            }
        }

        public string ToolTip
        {
            get
            {
                string str = ConfigurationManager.AppSettings["menu.personRefundable_ToolTip"];
                string toolTip = string.Format(str, Amount);
                return toolTip;
            }
        }



    }
}
