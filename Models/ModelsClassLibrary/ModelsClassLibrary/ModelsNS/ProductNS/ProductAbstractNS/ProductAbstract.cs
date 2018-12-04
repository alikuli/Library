using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ComplexClassesNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ProductAbstractNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    /// <summary>
    /// This is the head product. This will carry information about this particular product. The debate in my mind at this time
    /// is if to make this into a general product representing the real or a specific product. I am inclining towards specific because
    /// we will be invoicing this products children. 
    /// This product will contain the name, weight, the prices, the costs, stock picture. The buy price 
    /// The children will contain the actual picture, the sale price, the buy price. 
    /// The children will be owned by the Seller for sale, By Buyer for purchase. Only the owner will be able to edit them.
    /// 
    /// </summary>
    public abstract partial class ProductAbstract : CommonWithId, IProductHasUploads, IProductAbstract
    {
        public ProductAbstract()
        {
            IsDisplayedOnWebsite = false;
            Buy = new CostsComplex();
            Sell = new SalePriceComplex();
            Dimensions = new Dimensions();

        }

       
    }
}