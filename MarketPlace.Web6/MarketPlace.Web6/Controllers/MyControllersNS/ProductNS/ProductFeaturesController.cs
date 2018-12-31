using AliKuli.Extentions;
using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.MenuNS;
using UowLibrary.ParametersNS;
using UowLibrary.PageViewNS;
using UowLibrary.PlayersNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using UowLibrary.FeatureNS.MenuFeatureNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class ProductFeaturesController : EntityAbstractController<ProductFeature>
    {

        ProductFeatureBiz _productFeatureBiz;
        ProductBiz _productBiz;
        public ProductFeaturesController(AbstractControllerParameters param, ProductBiz productBiz)
            : base(productBiz.ProductFeatureBiz, param)
        {
            _productFeatureBiz = productBiz.ProductFeatureBiz;
            _productBiz = productBiz;
        }

        ProductBiz ProductBiz
        {
            get
            {
                _productBiz.UserId = UserId;
                _productBiz.UserName = UserName;
                return _productBiz;
            }
        }
        ProductFeatureBiz ProductFeatureBiz
        {
            get
            {
                return _productFeatureBiz;
            }
        }
        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            ProductFeature productFeature = parm.Entity as ProductFeature;
            productFeature.IsNullThrowException("Unable to unbox product feature");

            productFeature.SelectListMenuFeatures = ProductFeatureBiz.SelectList();
            productFeature.SelectListProducts = ProductBiz.SelectList();

            return base.Event_CreateViewAndSetupSelectList(parm);

        }





    }
}
