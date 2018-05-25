using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.DiscountNS
{
    /// <summary>
    /// This holds the return amount from Discount
    /// </summary>
    [NotMapped]
    public class DiscountAmount
    {
        public DiscountAmount(decimal actualAmount, decimal discountAmountFound,bool isPct)
        {

        }
        public decimal ActualAmount { get; set; }
        public decimal Discount { get; set; }
        public bool IsPct { get; set; }

        public decimal FinalCalculatedAmount
        {
            get
            {
                decimal calcAmnt = ActualAmount * (1 - Discount);
                if (!IsPct)
                {
                    if (Discount > ActualAmount)
                        throw new Exception("Discount amount is greater than the sale amount. DiscountAmount.");
                    calcAmnt = ActualAmount - Discount;
                }

                if (calcAmnt < 0)
                    throw new Exception("Discount amount is greater than the sale amount. Sale is Negative! DiscountAmount.");

                return calcAmnt;
            }
        }
    }
}
