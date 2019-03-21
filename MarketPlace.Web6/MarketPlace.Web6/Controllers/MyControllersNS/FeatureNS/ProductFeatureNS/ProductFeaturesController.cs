using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS.MenuFeatureNS;
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
        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            ProductFeature productFeature = parm.Entity as ProductFeature;
            productFeature.IsNullThrowException("Unable to unbox product feature");

            //get the product and load its name in
            parm.ProductId.IsNullOrWhiteSpaceThrowException("ProductId did nto get value in view");

            Product product = ProductBiz.Find(parm.ProductId);
            product.IsNullThrowException("Product not found.");
            productFeature.Product = product;
            productFeature.ProductId = product.Id;

            //In Create and edit the model will be ProductFeature so we need to more the returnUrl to it
            //productFeature.ReturnUrl = parm.ReturnUrl;

            productFeature.SelectListFeature = MenuFeatureBiz.SelectList();
            productFeature.SelectListProducts = ProductBiz.SelectList();

            return base.Event_CreateViewAndSetupSelectList(parm);

        }

        //public override ActionResult Create(string isandForSearch, MenuENUM menuEnum = MenuENUM.CreateDefault, string productChildId = "", string menuPathMainId = "", string productId = "", string returnUrl = "", EnumLibrary.EnumNS.SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false, bool isMenu = false, string parentId = "")
        //{
        //    return  base.Create(isandForSearch, menuEnum, productChildId, menuPathMainId, productId, returnUrl, sortBy, searchFor, selectedId, print, isMenu, parentId);
        //}
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
            productId.IsNullOrWhiteSpaceThrowArgumentException("productId");
            returnUrl.IsNullOrWhiteSpaceThrowArgumentException("returnUrl");

            CreateNewFeatureModel createNewFeatureModel = new CreateNewFeatureModel();
            createNewFeatureModel.ParentId = productId;
            createNewFeatureModel.ReturnUrl = returnUrl;

            return View(createNewFeatureModel);

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

        //public ActionResult AddFeature(string productFeatureId, string parentName, string returnUrl)
        //{
        //    productFeatureId.IsNullOrWhiteSpaceThrowArgumentException("productFeatureId");
        //    parentName.IsNullOrWhiteSpaceThrowArgumentException("parentName");
        //    returnUrl.IsNullOrWhiteSpaceThrowArgumentException("returnUrl");

        //    MenuFeatureModel menuFeatureModel = new MenuFeatureModel();
        //    menuFeatureModel.ParentId = productFeatureId;
        //    menuFeatureModel.ParentName = parentName;
        //    menuFeatureModel.SelectListFeature = MenuFeatureBiz.SelectList();
        //    menuFeatureModel.ReturnUrl = returnUrl;
        //    return View(menuFeatureModel);
        //}

        //[HttpPost]
        //public ActionResult AddFeature(MenuFeatureModel menuFeatureModel)
        //{
        //    try
        //    {
        //        //MenuPath1Biz.AddFeature(menuFeatureModel);
        //    }
        //    catch (System.Exception e)
        //    {
        //        ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), e);
        //    }
        //    if (menuFeatureModel.ReturnUrl.IsNullOrWhiteSpace())
        //        return View("Index");

        //    return Redirect(menuFeatureModel.ReturnUrl);
        //}
    }
}
