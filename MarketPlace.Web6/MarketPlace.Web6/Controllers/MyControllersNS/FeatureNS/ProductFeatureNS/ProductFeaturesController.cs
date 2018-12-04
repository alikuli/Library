using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UowLibrary.FeatureNS.MenuFeatureNS;
using UowLibrary.ParametersNS;

namespace MarketPlace.Web6.Controllers.MyControllersNS.FeatureNS.ProductFeatureNS
{
    public class ProductFeaturesController :  EntityAbstractController<ProductFeature>
    {

        public ProductFeaturesController(ProductFeatureBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {

        }

    }
}