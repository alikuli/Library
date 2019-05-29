using System.Configuration;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class SystemCashRefundableClass : IMenuItemHelper
    {
        public SystemCashRefundableClass()
        {

        }
        public SystemCashRefundableClass(decimal amount)
        {
            Amount = amount;
        }

        decimal Amount { get; set; }
        public string MenuItem
        {
            get
            {
                string content = ConfigurationManager.AppSettings["menu.system_Cash_Refundable_MenuItem"];
                string str = string.Format(content, Amount);
                return str;

            }
        }

        public string ToolTip
        {
            get
            {
                string str = ConfigurationManager.AppSettings["menu.system_Cash_Refundable_ToolTip"];
                string toolTip = string.Format(str, Amount);
                return toolTip;
            }
        }



    }
}
