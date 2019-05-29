using System.Configuration;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class SystemTotalCash_AmountOnlyClass : IMenuItemHelper
    {
        public SystemTotalCash_AmountOnlyClass()
        {

        }
        public SystemTotalCash_AmountOnlyClass(decimal refundableAmount, decimal nonRefundableAmount)
        {
            Refundable_Amount = refundableAmount;
            NonRefundable_Amount = nonRefundableAmount;
        }

        decimal Refundable_Amount { get; set; }
        decimal NonRefundable_Amount { get; set; }
        public string MenuItem
        {
            get
            {
                string content = ConfigurationManager.AppSettings["menu.systemTotalCash_AmountOnly"];
                string str = string.Format(content, Refundable_Amount);
                return str;

            }
        }

        public string ToolTip
        {
            get
            {
                string str = ConfigurationManager.AppSettings["menu.personTotalCash_AmountOnly_ToolTip"];
                string toolTip = string.Format(str, Refundable_Amount, NonRefundable_Amount);
                return toolTip;
            }
        }



    }
}
