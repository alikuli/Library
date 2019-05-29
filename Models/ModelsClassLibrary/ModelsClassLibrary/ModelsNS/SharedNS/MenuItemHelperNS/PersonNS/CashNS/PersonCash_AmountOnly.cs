using System.Configuration;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class PersonCash_AmountOnly : IMenuItemHelper
    {
        public PersonCash_AmountOnly()
        {

        }
        public PersonCash_AmountOnly(decimal refundableAmount, decimal nonRefundableAmount)
        {
            RefundableAmount = refundableAmount;
            NonRefundableAmount = nonRefundableAmount;
        }

        decimal RefundableAmount { get; set; }
        decimal NonRefundableAmount { get; set; }
        public string MenuItem
        {
            get
            {
                string content = ConfigurationManager.AppSettings["menu.person_TotalCash_AmountOnly"];
                string str = string.Format(content, RefundableAmount + NonRefundableAmount);
                return str;

            }
        }

        public string ToolTip
        {
            get
            {
                string str = ConfigurationManager.AppSettings["menu.person_TotalCash_ToolTip"];
                string toolTip = string.Format(str, RefundableAmount, NonRefundableAmount);
                return toolTip;
            }
        }



    }
}
