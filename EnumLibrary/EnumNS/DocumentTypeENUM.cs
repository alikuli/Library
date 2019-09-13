
namespace EnumLibrary.EnumNS
{
    /// <summary>
    /// All document types are relative to the user.
    /// Relative to the user a:
    ///     Invoice: This is the invoice from the vendor. This means the vendor has confirmed the sale.
    ///     Credit: This is a reverse invoice from the vendor.
    ///     PurchaseOrder: This is a purchase order to the vendor from the user. A purchase order gets created when the vendor confirms the order.
    ///     
    /// The following are opposites
    /// 
    /// If you are Not User | If you are user
    /// --------------------|--------------------------------------------------------------------------------------------
    /// Invoice             | PurchaseOrder         : A confirmed sale or purchase. An accepted offer or sale
    /// Credit              | Debit                 : Receiving payment / Making a payment
    /// Debit               | Credit                : Making a payment / receivng a payment
    /// Delivered           | Received             : Make delivery / Received Good
    /// Receiving           | Delivery              : Receive Goods / Make Delivery
    /// Request             | Offer                 : Request to buy/ offered for purchase
    /// OnDelivery          | OnDelivery            : Product is being delivered
    /// Unknown             | Unknown               : A starting value.
    /// 
    /// </summary>
    public enum DocumentTypeENUM
    {
        Unknown,
        Invoice,
        Credit,
        Debit,
        PurchaseOrder,//
        Receiving,
        Offer,
        Request,
        Delivered,//
        Received,
        OnDelivery

    }
}
