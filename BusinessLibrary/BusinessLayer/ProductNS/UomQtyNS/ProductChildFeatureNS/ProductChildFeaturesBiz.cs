
using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using UowLibrary.ParametersNS;
using System.Linq;
using AliKuli.Extentions;

namespace UowLibrary.FeatureNS.MenuFeatureNS
{
    public partial class ProductChildFeatureBiz : BusinessLayer<ProductChildFeature>
    {
        public ProductChildFeatureBiz(IRepositry<ProductChildFeature> entityDal, BizParameters bizParameters)
            :base(entityDal, bizParameters)
        {

        }
        public override string SelectListCacheKey
        {
            get { return "ProductChildFeatureSelectListData"; }
        }

        public override System.Linq.IQueryable<ProductChildFeature> GetDataToCheckDuplicateName(ProductChildFeature entity)
        {
            entity.ProductChildId.IsNullOrWhiteSpaceThrowException("ProductChildId");
            return base.GetDataToCheckDuplicateName(entity).Where(x => x.ProductChildId == entity.ProductChildId);
        }

    }
}
