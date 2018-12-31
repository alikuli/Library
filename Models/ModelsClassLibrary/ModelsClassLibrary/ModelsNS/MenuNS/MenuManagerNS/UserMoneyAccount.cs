
namespace ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS
{

    /// <summary>
    /// This holds the users Money Amounts to display on the screen
    /// </summary>
    public class UserMoneyAccount
    {
        public UserMoneyAccount()
        {

        }
        public UserMoneyAccount(decimal amountRefundable, decimal amountNonRefundable)
        {
            AmountRefundable = amountRefundable;
            AmountNonRefundable = amountNonRefundable;
        }
        public decimal AmountRefundable { get; set; }
        public decimal AmountNonRefundable { get; set; }
        public decimal AmountTotal { get { return AmountNonRefundable + AmountRefundable; } }


        public string AmountRefundableStr 
        { 
            get 
            {
                string str = string.Format("Rs{0:n2}", AmountRefundable);
                return str;
            } 
        }
        public string AmountNonRefundableStr
        {
            get
            {
                string str = string.Format("Rs{0:n2}", AmountNonRefundable);
                return str;
            }
        }
        public string AmountTotalStr
        {
            get
            {
                string str = string.Format("Rs{0:n2}", AmountTotal);
                return str;
            }
        }
    }
}
