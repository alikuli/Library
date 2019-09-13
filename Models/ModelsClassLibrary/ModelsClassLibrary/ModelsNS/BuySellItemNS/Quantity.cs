using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS
{
    [ComplexType]
    public class Quantity
    {

        public Quantity()
        {

        }

        public Quantity(double ordered /*, double shipped */)
        {
            Order = ordered;
            Order_Original = Order;
            //Ship = shipped;
        }
        public double Order { get; set; }
        
        
        [NotMapped]
        public string OrderStr { get; set; }


        /// <summary>
        /// This is the original order received from the cart
        /// </summary>
        [Display(Name = "Original Order")]
        public double Order_Original { get; set; }

        public bool IsOrderSameAsOriginal { get { return Order == Order_Original; } }

        public decimal OrderedAsDecimal
        {
            get
            {
                decimal dec;
                bool success = decimal.TryParse(Order.ToString(), out dec);
                if (!success)
                    throw new Exception("Unable to convert double to decimal");
                return dec;
            }
        }

        public string OrderedAsDecimal_Formatted
        {
            get
            {
                return string.Format("{0:N2}", OrderedAsDecimal);
            }
        }


    }
}
