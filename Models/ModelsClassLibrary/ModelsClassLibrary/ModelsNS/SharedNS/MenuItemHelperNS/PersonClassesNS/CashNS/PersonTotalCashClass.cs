using System.Configuration;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class PersonTotalCashClass : IMenuItemHelper
    {
        public PersonTotalCashClass()
        {

        }
        public PersonTotalCashClass(decimal refundableAmount, decimal nonRefundableAmount)
        {
            _refundableAmount = refundableAmount;
            _refundableAmount = nonRefundableAmount;
            _total = refundableAmount + nonRefundableAmount;
        }

        decimal _refundableAmount { get; set; }
        decimal _nonRefundableAmount { get; set; }
        decimal _total { get; set; }

        public string MenuItem
        {
            get
            {
                string content = ConfigurationManager.AppSettings["menu.person_TotalCash_MenuItem"];
                string str = string.Format(content, _total);
                return str;

            }
        }

        public string ToolTip
        {
            get
            {
                string str = ConfigurationManager.AppSettings["menu.person_TotalCash_ToolTip"];
                string toolTip = string.Format(str, _refundableAmount, _nonRefundableAmount);
                return toolTip;
            }
        }



    }
}
