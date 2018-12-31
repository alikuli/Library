using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using UowLibrary.ParametersNS;

namespace UowLibrary.FeatureNS.MenuFeatureNS
{
    public partial class MenuFeatureBiz : BusinessLayer<MenuFeature>
    {

        public MenuFeatureBiz(IRepositry<MenuFeature> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {

        }

        public override string SelectListCacheKey
        {
            get { return "MenuFeatureSelectListData"; }
        }



    }
}
