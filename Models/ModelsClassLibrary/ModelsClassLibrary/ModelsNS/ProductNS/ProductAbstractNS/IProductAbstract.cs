using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ComplexClassesNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Collections.Generic;
namespace ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ProductAbstractNS
{
    public interface IProductAbstract : ICommonWithId
    {
        CostsComplex Buy { get; set; }
        Dimensions Dimensions { get; set; }
        bool IsDisplayedOnWebsite { get; set; }
        ICollection<UploadedFile> MiscFiles { get; set; }
        string MiscFilesLocation();
        string MiscFilesLocation_Initialization();
        SalePriceComplex Sell { get; set; }
        //bool IsChild { get; }
        //bool IsSaleable { get; set; }

        //Product Parent { get; set; }
        //string ParentId { get; set; }
        //Quantity Qty { get; set; }
        //void SelfErrorCheck();
        //UomLength UomDimensions { get; set; }
        //string UomDimensionsId { get; set; }
        //UomQty UomPurchase { get; set; }
        //string UomPurchaseId { get; set; }
        //UomQty UomSale { get; set; }
        //string UomSaleId { get; set; }
        //UomVolume UomVolume { get; set; }
        //string UomVolumeId { get; set; }
        //UomWeight UomWeightActual { get; set; }
        //string UomWeightActualId { get; set; }
        //UomWeight UomWeightListed { get; set; }
        //string UomWeightListedId { get; set; }
        //double Volume { get; set; }
        //double WeightActual { get; set; }
        //double WeightListed { get; set; }
    }
}
