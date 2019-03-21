using System.Configuration;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class SystemTotalCashClass : IMenuItemHelper
    {
        public SystemTotalCashClass()
        {

        }
        public SystemTotalCashClass(decimal refundableAmount, decimal nonRefundableAmount)
        {
            _refundableAmount = refundableAmount;
            _total = _refundableAmount + _nonRefundableAmount;
        }

        decimal _refundableAmount { get; set; }
        decimal _nonRefundableAmount { get; set; }
        decimal _total { get; set; }
        public string MenuItem
        {
            get
            {
                string content = ConfigurationManager.AppSettings["menu.system_TotalCash_MenuItem"];
                string str = string.Format(content, _total);
                return str;

            }
        }

        public string ToolTip
        {
            get
            {
                string str = ConfigurationManager.AppSettings["menu.system_TotalCash_ToolTip"];
                string toolTip = string.Format(str, _refundableAmount, _nonRefundableAmount);
                return toolTip;
            }
        }



    }
}
