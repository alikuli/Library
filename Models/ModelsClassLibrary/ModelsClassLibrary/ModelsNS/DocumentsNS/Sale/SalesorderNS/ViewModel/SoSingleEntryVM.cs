using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS
{
 /// <summary>
 /// This VM was created to pass information into view
 /// </summary>
 /// 
    [NotMapped]
    public class SoSingleEntryVM:CommonWithId
    {
        /// <summary>
        /// This is the owner/Seller of the goods            
        /// </summary>
        public long OwnerID { get; set; }


        /// <summary>
        /// This is the buyer of the goods
        /// </summary>
        public long ConsigneeID { get; set; }


        /// <summary>
        /// This is the salesman, he can get commission
        /// </summary>
        public long SalesmanID { get; set; }


        /// <summary>
        /// This is the product.
        /// </summary>
        public long ProductID { get; set; }


        /// <summary>
        /// This is the quantity sold.
        /// </summary>
        public double Quantity{get;set;}

        /// <summary>
        /// This is the sell price
        /// </summary>
        public double SellPrice{get;set;}

        /// <summary>
        /// This is the user who created the record.
        /// </summary>
        public string User{get;set;}

        /// <summary>
        /// This is the delvery method
        /// </summary>
        public long DeliveryMethodID { get; set; }



        /// <summary>
        /// This is the payment method.
        /// </summary>
        public long PaymentMethodID { get; set; }

        /// <summary>
        /// This is the payment terms.
        /// </summary>
        public long PaymentTermsID { get; set; }


        /// <summary>
        /// This is the expected date of deliver
        /// </summary>
        public DateTime ExpectedDateDeliver{get;set;}
        
        /// <summary>
        /// This is the seller purchase order number
        /// </summary>
        [Display(Name = "Seller PO #")]
        public string SellerPO { get; set; }

        /// <summary>
        /// This is the cost of shipping
        /// </summary>
        [Display(Name = "Shipping Cost")]
        public decimal ShippingCost { get; set; }

        /// <summary>
        /// This is some misc cost
        /// </summary>
        /// 
        [Display(Name="Misc Cost")]
        public decimal MiscCost{get;set;}

        /// <summary>
        /// This is the tax cost.
        /// </summary>
        [Display(Name = "Tax Cost")]
        public decimal TaxCost { get; set; }

        /// <summary>
        /// This is the scratch card number that will be used for payment
        /// </summary>
        [Display(Name = "Voucher Number")]

        public long PaymentVoucherNumberID { get; set; }
    }
}