
using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using UowLibrary.ParametersNS;
namespace UowLibrary.FeatureNS.MenuFeatureNS
{
    public class ProductChildFeatureBiz : BusinessLayer<ProductChildFeature>
    {
        public ProductChildFeatureBiz(IRepositry<ProductChildFeature> entityDal, BizParameters bizParameters)
            :base(entityDal, bizParameters)
        {

        }
        public override string SelectListCacheKey
        {
            get { return "ProductChildFeatureSelectListData"; }
        }

    }
}
