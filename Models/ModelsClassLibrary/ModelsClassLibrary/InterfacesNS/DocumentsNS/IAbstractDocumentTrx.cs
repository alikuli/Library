using System;
using InterfacesLibrary.SharedNS;
using InterfacesLibrary.ProductNS;
namespace InterfacesLibrary.DocumentsNS

{
    public interface IAbstractDocumentTrx: ICommonWithId
    {
        decimal CurrBuyPrice { get; set; }
        decimal ListedPrice { get; set; }
        decimal OrderedQty { get; set; }
        decimal SalePrice { get; set; }
        decimal ShipQty { get; set; }
        decimal DiscountPct { get; set; }
        DateTime? DateToShipBegin { get; set; }
        DateTime? DateToShipEnd { get; set; }

        string Description { get; set; }
        string GetProductNameFromProduct();

        bool IsDateToShiped_BeforeOrEqualTo_DateExpectedEnd { get; }
        bool IsForcedSale { get; set; }
 
        IProduct Product { get; set; }
        Guid ProductID { get; set; }
        ICounterClass TotalShippedQty { get; set; }
        ICounterClass FinalSalesPrice { get; set; }
        ICounterClass LineTotal_Money_Ordered { get; set; }
        ICounterClass LineTotal_Money_Ship { get; set; }

    }
}
