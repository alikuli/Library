using AliKuli.Extentions;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.MoneyAndCountClass
{
    /// <summary>
    /// this is the class that holds all the information about an order type so we can print it.
    /// </summary>
    public class MoneyCountItemClass
    {
        public MoneyCountItemClass()
        {

        }

        public MoneyCountItemClass(string menuName, string toolTip, decimal moneyAmount, long count)
        {
            MenuName = menuName;
            MoneyAmount = moneyAmount;
            Count = count;
        }


        public string MenuName { get; set; }
        public string MenuToolTip { get; set; }
        public string Url { get; set; }
        public decimal MoneyAmount { get; set; }
        public long Count { get; set; }

        public string MoneyAmount_Formatted
        {
            get
            {
                return MoneyAmount.ToString().ToNumCommaFormat();
            }
        }
        public string Count_Formatted
        {
            get
            {
                return Count.ToString();
            }
        }

        /// <summary>
        /// if money and count are zero
        /// </summary>
        public bool IsZero
        {
            get
            {
                return Count == 0 && MoneyAmount == 0;
            }
        }
        public bool NotZero
        {
            get
            {
                return !IsZero;
            }
        }

    }
}
