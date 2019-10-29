using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS
{
    /// <summary>
    /// </summary>
    public partial class BuySellDoc
    {

        public bool IsPaymentAllowedIn_NonRefundableMoney()
        {
            if (buySellItemFixed.IsNullOrEmpty())
                return false;

            BuySellItem item = buySellItemFixed.FirstOrDefault(x => x.ProductChild.IsNonRefundablePaymentAccepted == true);
            return !item.IsNull();
        }

        public decimal MaxPaymentAllowedInNonRefundableMoney()
        {
            if (IsPaymentAllowedIn_NonRefundableMoney())
            {
                List<BuySellItem> lst = buySellItemFixed.Where(x => x.ProductChild.IsNonRefundablePaymentAccepted == true).ToList();

                if (lst.IsNullOrEmpty())
                    return 0;
                decimal ttlAllowed = 0;
                foreach (BuySellItem bsi in lst)
                {
                    decimal dtyDecimal = Decimal.Parse(bsi.Quantity.Order.ToString());
                    ttlAllowed += bsi.SalePrice * dtyDecimal;
                }


                //add also commission for bsd.

                return ttlAllowed;
            }

            return 0;
        }





    }



}
