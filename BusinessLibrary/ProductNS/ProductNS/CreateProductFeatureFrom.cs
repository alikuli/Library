

using ModelsClassLibrary.ModelsNS.FeaturesNS;
namespace UowLibrary.ProductNS
{
    public partial class ProductBiz
    {
        public ProductFeature CreateProductFeatureFrom(MenuFeature mf)
        {
            ProductFeature pf = ProductFeatureBiz.Factory() as ProductFeature;
            pf.Name = mf.Name;
            return pf;

        }

    }
}
