using ModelsClassLibrary.ModelsNS.ProductNS.ComplexClassesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    /// <summary>
    /// Note. Scratch card 16 digit serial number is placed in Name AND in ProductsOwnNumber. I believe thqt ProductsOwnNumber
    /// needs to be removed. No need for that. Name is fine because it will not duplicate intrinsically.
    /// </summary>
    public abstract partial class ProductAbstract : CommonWithId, IProductHasUploads
    {
        public ProductAbstract()
        {
            IsDisplayedOnWebsite = false;
            Buy = new CostsComplex();
            Sell = new SalePriceComplex();
            //ItemNos = new List<ProductIdentifier>();

            //Date = new ProductDateComplex();
            //IsChild = false;
            //IsUomForPackingVolRequired = false;
            //IsAllowd_Zero_MRSP = false;
        }



        //string MiscFilesLocation()
        //{
        //    return AliKuli.ConstantsNS.MyConstants.SAVE_LOCATION_PRODUCT_MAIN_CATEGORY;
        //}
    }
}