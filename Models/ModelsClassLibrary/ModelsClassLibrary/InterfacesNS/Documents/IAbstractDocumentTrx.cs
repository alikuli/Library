using ModelsClassLibrary.Interfaces;
using System;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
namespace ModelsClassLibrary.Models.Documents.Abstract

{
    public interface IAbstractDocumentTrx: ICommonWithId
    {
        decimal CurrBuyPrice { get; set; }
        DateTime? DateToShipBegin { get; set; }
        DateTime? DateToShipEnd { get; set; }
        string Description { get; set; }
        decimal DiscountPct { get; set; }
        CounterClass FinalSalesPrice { get; set; }
        string GetProductNameFromProduct();

        bool IsDateToShiped_BeforeOrEqualTo_DateExpectedEnd { get; }
        bool IsForcedSale { get; set; }
        CounterClass LineTotal_Money_Ordered { get; set; }
        CounterClass LineTotal_Money_Ship { get; set; }
        decimal ListedPrice { get; set; }
        decimal OrderedQty { get; set; }
        Product Product { get; set; }
        Guid ProductID { get; set; }
        decimal SalePrice { get; set; }
        decimal ShipQty { get; set; }
        CounterClass TotalShippedQty { get; set; }

    }
}
