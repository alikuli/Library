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
using ModelsClassLibrary.ModelsNS.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class ProductFeaturesController : EntityAbstractController<ProductFeature>
    {

        //ProductFeatureBiz _productFeatureBiz;
        ProductBiz _productBiz;
        MenuFeatureBiz _menuFeatureBiz;
        public ProductFeaturesController(AbstractControllerParameters param , ProductBiz productBiz , MenuFeatureBiz menuFeatureBiz)
            : base(productBiz.ProductFeatureBiz, param)
        {
            //_productFeatureBiz = productBiz.ProductFeatureBiz;
            _menuFeatureBiz = menuFeatureBiz;
            _productBiz = productBiz;

        }
        MenuFeatureBiz MenuFeatureBiz
        {
            get
            {
                _menuFeatureBiz.UserId = UserId;
                _menuFeatureBiz.UserName = UserName;
                return _menuFeatureBiz;
            }

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
        //ProductFeatureBiz ProductFeatureBiz
        //{
        //    get
        //    {
        //        return _productFeatureBiz;
        //    }
        //}
        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            ProductFeature productFeature = parm.Entity as ProductFeature;
            productFeature.IsNullThrowException("Unable to unbox product feature");

            productFeature.SelectListMenuFeatures = MenuFeatureBiz.SelectList();
            productFeature.SelectListProducts = ProductBiz.SelectList();

            return base.Event_CreateViewAndSetupSelectList(parm);

        }

        public override void Event_BeforeSaveInCreateAndEdit(ControllerCreateEditParameter parm)
        {
            ProductFeature productFeature = parm.Entity as ProductFeature;
            productFeature.IsNullThrowException("Unable to unbox product feature");

            //add the product
            if (productFeature.Product.IsNull())
            {
                productFeature.ProductId.IsNullOrWhiteSpaceThrowException("ProductId");
                Product product = ProductBiz.Find(productFeature.ProductId);
                product.IsNullThrowException("Product not found!");
                productFeature.Product = product;
            }
            
            //add the MenuFeature
            if (productFeature.MenuFeature.IsNull())
            {
                productFeature.MenuFeatureId.IsNullOrWhiteSpaceThrowException("MenuFeatureId");
                MenuFeature menuFeature = MenuFeatureBiz.Find(productFeature.MenuFeatureId);
                menuFeature.IsNullThrowException("MenuFeature not found!");
                productFeature.MenuFeature = menuFeature;
            }
        }





    }
}
