using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS.MenuFeatureNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS.ProductFeatureNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary.FeatureNS.MenuFeatureNS;
using UowLibrary.ParametersNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class ProductFeaturesController : EntityAbstractController<ProductFeature>
    {

        //ProductFeatureBiz _productFeatureBiz;
        ProductBiz _productBiz;
        MenuFeatureBiz _menuFeatureBiz;
        public ProductFeaturesController(AbstractControllerParameters param, ProductBiz productBiz, MenuFeatureBiz menuFeatureBiz)
            : base(productBiz.ProductFeatureBiz, param)
        {
            //_productFeatureBiz = productBiz.ProductFeatureBiz;
            _menuFeatureBiz = menuFeatureBiz;
            _productBiz = productBiz;

        }

        ProductFeatureBiz ProductFeatureBiz
        {
            get
            {
                return ProductBiz.ProductFeatureBiz;
            }
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
        public override ActionResult Event_Create_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            ProductFeature productFeature = parm.Entity as ProductFeature;
            productFeature.IsNullThrowException("Unable to unbox product feature");

            //get the product and load its name in
            productFeature.ProductId.IsNullOrWhiteSpaceThrowException("ProductId did nto get value in view");

            if (productFeature.Product.IsNull())
            {
                if (productFeature.ProductId.IsNull())
                {
                    throw new Exception("Product has not been loaded.");
                }
            }

            productFeature.SelectListFeature = MenuFeatureBiz.SelectList();
            productFeature.SelectListProducts = ProductBiz.SelectList();

            return base.Event_Create_ViewAndSetupSelectList_GET(parm);

        }

        public override ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            ProductFeature productFeature = parm.Entity as ProductFeature;
            productFeature.IsNullThrowException("Unable to unbox product feature");

            //get the product and load its name in
            productFeature.ProductId.IsNullOrWhiteSpaceThrowException("ProductId did nto get value in view");

            if (productFeature.Product.IsNull())
            {
                if (productFeature.ProductId.IsNull())
                {
                    throw new Exception("Product has not been loaded.");
                }
            }

            productFeature.SelectListFeature = MenuFeatureBiz.SelectList();
            productFeature.SelectListProducts = ProductBiz.SelectList();

            return base.Event_Edit_ViewAndSetupSelectList_GET(parm);
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



        public ActionResult CreateNewFeature(string productId, string returnUrl)
        {
            try
            {
                productId.IsNullOrWhiteSpaceThrowArgumentException("productId");
                returnUrl.IsNullOrWhiteSpaceThrowArgumentException("returnUrl");

                CreateNewFeatureModel createNewFeatureModel = new CreateNewFeatureModel();
                createNewFeatureModel.ParentId = productId;
                createNewFeatureModel.ReturnUrl = returnUrl;

                return View(createNewFeatureModel);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), ex);
            }
            return Redirect(returnUrl);

        }
        [HttpPost]
        public ActionResult CreateNewFeature(CreateNewFeatureModel createNewFeatureModel)
        {

            try
            {
                createNewFeatureModel.IsNullThrowException("createNewFeatureModel");
                ProductFeatureBiz.CreateNewFeature(createNewFeatureModel);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), e);
            }
            if (createNewFeatureModel.ReturnUrl.IsNullOrWhiteSpace())
                return View("Index");

            return Redirect(createNewFeatureModel.ReturnUrl);

        }

        public ActionResult AddFeature(string productId, string parentName, string returnUrl,string description)
        {
            try
            {
                productId.IsNullOrWhiteSpaceThrowArgumentException("productId");
                parentName.IsNullOrWhiteSpaceThrowArgumentException("parentName");
                returnUrl.IsNullOrWhiteSpaceThrowArgumentException("returnUrl");

                ProductFeatureModel productFeatureModel = new ProductFeatureModel();
                productFeatureModel.ParentId = productId;
                productFeatureModel.ParentName = parentName;
                productFeatureModel.SelectListFeature = MenuFeatureBiz.SelectList();
                productFeatureModel.ReturnUrl = returnUrl;
                productFeatureModel.Description = description;

                return View(productFeatureModel);

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                return Redirect(returnUrl);
            }
        }

        [HttpPost]
        public ActionResult AddFeature(ProductFeatureModel productFeatureModel)
        {
            try
            {
                ProductBiz.AddFeature(productFeatureModel);
            }
            catch (System.Exception e)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), e);
            }
            if (productFeatureModel.ReturnUrl.IsNullOrWhiteSpace())
                return View("Index");

            return Redirect(productFeatureModel.ReturnUrl);
        }
    }
}
