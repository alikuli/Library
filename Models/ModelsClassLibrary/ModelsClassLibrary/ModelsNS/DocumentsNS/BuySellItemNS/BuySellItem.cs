using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS
{
    public class BuySellItem : CommonWithId
    {
        public BuySellItem()
        {

        }

        public BuySellItem(string buySellDocId, string productChildId, double ordered, double shipped, decimal salePrice)
        {
            BuySellDocId = buySellDocId;
            ProductChildId = productChildId;
            Quantity = new Quantity(ordered, shipped);
            SalePrice = salePrice;
        }
        public string BuySellDocId { get; set; }
        public BuySellDoc BuySellDoc { get; set; }


        public string ProductChildId { get; set; }
        public ProductChild ProductChild { get; set; }

        public Quantity Quantity { get; set; }
        public decimal SalePrice { get; set; }

        [Display(Name = "Ordered (Rs)")]
        public decimal Ordered
        {
            get
            {
                return SalePrice * Quantity.OrderedAsDecimal;
            }
        }

        [Display(Name = "Shipped (Rs)")]
        public decimal Shipped
        {
            get
            {
                return SalePrice * Quantity.ShippedAsDecimal;
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
                return Quantity.Backordered > 0;
            }
        }
    }
}
