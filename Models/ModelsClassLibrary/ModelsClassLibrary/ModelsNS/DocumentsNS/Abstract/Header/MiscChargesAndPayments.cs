using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS
{
    [ComplexType]
    public class MiscChargesAndPayments
    {
        /// <summary>
        /// This is the misc payment amount. Note: This amount is added into the sale amount, later if we want to extract this amount for the ledger
        /// we will need this document to breakup the sale amount. Also, this amount can be posted seperately through setup.
        /// </summary>
        [Display(Name = "Misc Charges")]
        public decimal MiscCharges { get; set; }



        /// <summary>
        /// This is the Shipping amount.
        /// </summary>
        [Display(Name = "Shipping Charge")]
        public decimal ShippingAmount { get; set; }

        /// <summary>
        /// This is the tax amount.
        /// </summary>
        [Display(Name = "Tax Charge")]
        public decimal TaxAmount { get; set; }
        public decimal TotalMiscCharges
        {
            get
            {
                return MiscCharges + ShippingAmount + TaxAmount;
            }
        }

    }
}