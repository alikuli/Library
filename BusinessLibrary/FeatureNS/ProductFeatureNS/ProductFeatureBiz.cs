using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using DalLibrary.Interfaces;
using UowLibrary.ParametersNS;

namespace UowLibrary.FeatureNS.MenuFeatureNS
{
    public class ProductFeatureBiz : BusinessLayer<ProductFeature>
    {

        public ProductFeatureBiz(IRepositry<ProductFeature> entityDal, BizParameters bizParameters)
            :base(entityDal, bizParameters)
        {

        }

        public override string SelectListCacheKey
        {
            get { return "ProductFeatureSelectListData"; }
        }
    }
}
