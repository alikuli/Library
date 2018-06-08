using ModelsClassLibrary.ModelsNS.ProductNS.ComplexClassesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    /// <summary>
    /// This will not be saved in the product because the product is being used like a header. The transactional products below will control the sell price etc.
    /// Here we will only track the prices
    /// </summary>
    public abstract partial class ProductAbstract 
    {
        public SalePriceComplex Sell { get; set; }

        public CostsComplex Buy { get; set; }


    }
}