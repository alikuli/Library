using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS
{
    public class BuySellItem : CommonWithId
    {
        public BuySellItem()
        {
            Quantity = new Quantity();
            LastOffer_Buyer = new DecimalWithDateComplex();
            LastOffer_Seller = new DecimalWithDateComplex();
        }



        public BuySellItem(string buySellDocId, string productChildId, double ordered, double shipped, decimal salePrice, string name)
        {
            BuySellDocId = buySellDocId;
            ProductChildId = productChildId;
            Quantity = new Quantity(ordered, shipped);
            SalePrice = salePrice;
            OriginalPrice = salePrice;//this is the original listed price.
            Name = name;
        }


        [NotMapped]
        public bool IsUserOwned { get; set; }

        [NotMapped]
        public BuySellDocumentTypeENUM BuySellDocumentTypeEnum { get; set; }

        public string BuySellDocId { get; set; }
        public virtual BuySellDoc BuySellDoc { get; set; }


        public string ProductChildId { get; set; }
        public virtual ProductChild ProductChild { get; set; }

        public Quantity Quantity { get; set; }

        [Display(Name = "Sale Price")]
        public decimal SalePrice { get; set; }

        /// <summary>
        /// This is the original price the item was listed at
        /// </summary>
        [Display(Name = "Original Price")]
        public decimal OriginalPrice { get; set; }
        public string OriginalPrice_Formatted
        {
            get
            {
                return string.Format("{0:N2}", OriginalPrice);
            }
        }

        [Display(Name = "Difference")]
        public string Difference_Formatted
        {
            get
            {
                return string.Format("{0:N2}", Difference);
            }
        }
        public decimal Difference
        {
            get
            {
                return SalePrice - OriginalPrice;
            }
        }


        public bool IsSalePriceSame
        {
            get
            {
                return Difference == 0;
            }
        }
        public string SalePrice_Formatted
        {
            get
            {
                return string.Format("{0:N2}", SalePrice);
            }
        }

        [Display(Name = "Ordered (Rs)")]
        public decimal OrderedRs
        {
            get
            {
                return SalePrice * Quantity.OrderedAsDecimal;
            }
        }


        public string OrderedRs_Formatted
        {
            get
            {
                return string.Format("{0:N2}", OrderedRs);
            }
        }


        /// <summary>
        /// The last offer of the buyer is saved here for reference purposes.
        /// </summary>
        [Display(Name = "Last offer to buy at")]
        public DecimalWithDateComplex LastOffer_Buyer { get; set; }

        //the last offer of the seller is saved here for reference purposes
        [Display(Name = "Last offer to sell at")]
        public DecimalWithDateComplex LastOffer_Seller { get; set; }


        [Display(Name = "Shipped (Rs)")]
        public decimal ShippedRs
        {
            get
            {
                return SalePrice * Quantity.ShippedAsDecimal;
            }
        }
        public string ShippedRs_Formatted
        {
            get
            {
                return string.Format("{0:N2}", ShippedRs);
            }
        }



        public decimal TotalBackOrderedMoney
        {
            get
            {
                return SalePrice * Quantity.BackOrderedAsDecimal;
            }
        }

        public bool HasBackOrder
        {
            get
            {
                return Quantity.Remaining > 0;
            }
        }

        [Display(Name = "Remaining (Rs)")]
        public decimal RemainingRs
        {
            get
            {
                return Quantity.RemainingDecimal * SalePrice;
            }
        }
        public string RemainingRs_Formatted
        {
            get
            {
                return string.Format("{0:N2}", RemainingRs);
            }
        }

        public override EnumLibrary.EnumNS.ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.BuySellDoc;
        }

        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            BuySellItem buySellItem = icommonWithId as BuySellItem;
            buySellItem.IsNullThrowException();

            Quantity = buySellItem.Quantity;
            //BuySellDocId = buySellItem.BuySellDocId; //this is blank
            //ProductChildId = buySellItem.ProductChildId;

            //if the sale price being saved is different then update the last offers
            if (SalePrice != buySellItem.SalePrice)
            {
                switch (buySellItem.BuySellDocumentTypeEnum)
                {
                    case BuySellDocumentTypeENUM.Unknown:
                        break;

                    case BuySellDocumentTypeENUM.Sale:
                        LastOffer_Buyer.Add(SalePrice, MetaData.Modified.ByUserId, MetaData.Modified.By);
                        break;

                    case BuySellDocumentTypeENUM.Purchase:
                        LastOffer_Seller.Add(SalePrice, MetaData.Modified.ByUserId, MetaData.Modified.By);
                        break;

                    default:
                        break;
                }
            }

            SalePrice = buySellItem.SalePrice;
        }


    }
}
