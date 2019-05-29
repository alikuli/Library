using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS
{
    [ComplexType]
    public class Quantity
    {

        public Quantity()
        {

        }

        public Quantity(double ordered, double shipped)
        {
            Ordered = ordered;
            Shipped = shipped;
        }
        public double Ordered { get; set; }
        public double Shipped { get; set; }

        public double Remaining
        {
            get
            {
                return Ordered - Shipped;
            }
        }
        public decimal RemainingDecimal
        {
            get
            {
                decimal dec;
                bool success = decimal.TryParse(Remaining.ToString(), out dec);
                if (!success)
                    throw new Exception("Unable to convert double to decimal");
                return dec;
            }
        }

        public string RemainingDecimal_Formatted
        {
            get
            {
                return string.Format("{0:N2}", RemainingDecimal);
            }
        }






        public decimal OrderedAsDecimal
        {
            get
            {
                decimal dec;
                bool success = decimal.TryParse(Ordered.ToString(), out dec);
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



        public decimal ShippedAsDecimal
        {
            get
            {
                decimal dec;
                bool success = decimal.TryParse(Shipped.ToString(), out dec);
                if (!success)
                    throw new Exception("Unable to convert double to decimal");
                return dec;
            }
        }
        public decimal BackOrderedAsDecimal
        {
            get
            {
                decimal dec;
                bool success = decimal.TryParse(Remaining.ToString(), out dec);
                if (!success)
                    throw new Exception("Unable to convert double to decimal");
                return dec;
            }
        }
    }
}
