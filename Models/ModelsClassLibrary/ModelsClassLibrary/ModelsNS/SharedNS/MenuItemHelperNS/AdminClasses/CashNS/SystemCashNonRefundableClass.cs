using System.Configuration;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class SystemCashNonRefundableClass : IMenuItemHelper
    {
        public SystemCashNonRefundableClass()
        {

        }
        public SystemCashNonRefundableClass(decimal amount)
        {
            Amount = amount;
        }

        decimal Amount { get; set; }
        public string MenuItem
        {
            get
            {
                string content = ConfigurationManager.AppSettings["menu.system_Cash_NonRefundable_MenuItem"];
                string str = string.Format(content, Amount);
                return str;

            }
        }

        public string ToolTip
        {
            get
            {
                string str = ConfigurationManager.AppSettings["menu.system_Cash_NonRefundable_ToolTip"];
                string toolTip = string.Format(str, Amount);
                return toolTip;
            }
        }




    }
}
