using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS.MenuFeatureNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.FeatureNS.MenuFeatureNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.ProductApproverNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class ProductsController : EntityAbstractController<Product>
    {

        ProductBiz _productBiz;
        ProductApproverBiz _productApproverBiz;
        MenuFeatureBiz _menuFeatureBiz;
        public ProductsController(ProductBiz biz, AbstractControllerParameters param, ProductApproverBiz productApproverBiz, MenuFeatureBiz menuFeatureBiz)
            : base(biz, param)
        {
            _productBiz = biz;
            _productApproverBiz = productApproverBiz;
            _menuFeatureBiz = menuFeatureBiz;
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
        UserBiz UserBiz
        {
            get
            {
                return _productBiz.UserBiz;
            }
        }

        ProductBiz ProductBiz
        {
            get
            {

                return _productBiz;
            }
        }

        ProductApproverBiz ProductApproverBiz
        {
            get
            {
                _productApproverBiz.UserId = UserId;
                _productApproverBiz.UserName = UserName;
                return _productApproverBiz;
            }
        }
        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            Product product = parm.Entity as Product;
            product.IsNullThrowException("Unable to unbox");

            _productBiz.LoadMenuPathCheckedBoxes(parm);
            _productBiz.FixProductFeatures(product);


            //product.ParentSelectList = _productBiz.SelectList_ForParent(parm.Entity);
            product.SelectListUomPurchase = _productBiz.SelectList_UomPurchaseQty();
            product.SelectListUomVolume = _productBiz.SelectList_UomVolume();
            product.SelectListUomShipWeight = _productBiz.SelectList_UomWeight();
            product.SelectListUomWeight = _productBiz.SelectList_UomWeight();
            product.SelectListUomLength = _productBiz.SelectList_UomLength();
            product.SelectListUomDimensionsLength = _productBiz.SelectList_UomLength();

            if (!parm.ReturnUrl.IsNullOrWhiteSpace())
            {
                product.MenuManager.ReturnUrl = parm.ReturnUrl;
            }

            if (!UserId.IsNullOrWhiteSpace())
            {
                product.ShowApproveButton = ProductApproverBiz.IsApprover(UserId);

            }

            if (parm.Entity.MenuManager.IsCreate)
            {
                //this controls where the product Create returns.
                //product.MenuManager.ReturnUrl = Url.Action("Index", "Products", new { menuEnum = MenuENUM.IndexMenuProduct });

                //the return URl is correct when in the Menu

                if (product.MenuManager.ReturnUrl.IsNullOrWhiteSpace())
                    product.MenuManager.ReturnUrl = Url.Action("Index", "Menus", new { menuEnum = MenuENUM.IndexMenuPath1 });

                ProductBiz.FixMenuPaths(parm);

            }
            return base.Event_CreateViewAndSetupSelectList(parm);

        }


        [Authorize]
        public async Task<ActionResult> UnapprovedProducts(string id, string searchFor, string isandForSearch, string selectedId, string returnUrl, MenuENUM menuEnum = MenuENUM.IndexDefault, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false, bool isMenu = false, string menuPathMainId = "")
        {

            try
            {
                if (UserId.IsNullOrWhiteSpace())
                    throw new Exception("You are not authourized.");

                if (!ProductApproverBiz.IsApprover(UserId))
                    throw new Exception("You are not authourized.");
                //This works in the Index when the GetListForIndex is invoked. It is overridden and this bool comes into play
                ProductBiz.IsShowUnApproved = true;
                return await base.Index(id, searchFor, isandForSearch, selectedId, returnUrl, menuEnum, sortBy, print, isMenu, menuPathMainId);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", "Menus");

            }
        }



        [Authorize(Roles = "Administrator")]
        public ActionResult MarkAllProductsApprovedUtility()
        {
            string returnUrl = Request.Url.PathAndQuery;
            YesParameter yesParam = new YesParameter("Do you want to approve all the products?", returnUrl, false);
            return View(yesParam);
        }



        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult MarkAllProductsApprovedUtility(YesParameter yes)
        {
            try
            {
                if (yes.IsYes)
                {
                    ProductBiz.ApproveAllProductsAndSaveUtility();
                    ErrorsGlobal.AddMessage("All products approved");
                    ErrorsGlobal.MemorySave();
                }

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
            }
            return RedirectToAction("Index", "Home");
        }


        public ActionResult AddFeature(string id, string parentName, string returnUrl)
        {
            id.IsNullOrWhiteSpaceThrowArgumentException("Id");
            parentName.IsNullOrWhiteSpaceThrowArgumentException("parentName");
            returnUrl.IsNullOrWhiteSpaceThrowArgumentException("returnUrl");


            ProductFeature productFeature = new ProductFeature();

            //MenuFeatureModel menuFeatureModel = new MenuFeatureModel();
            //menuFeatureModel.ParentId = id;
            //menuFeatureModel.ParentName = parentName;
            //menuFeatureModel.SelectListFeature = MenuFeatureBiz.SelectList();
            //menuFeatureModel.ReturnUrl = returnUrl;
            return View(productFeature);
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
                ProductBiz.CreateNewFeature(createNewFeatureModel);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), e);
            }
            if (createNewFeatureModel.ReturnUrl.IsNullOrWhiteSpace())
                return View("Index");

            return Redirect(createNewFeatureModel.ReturnUrl);

        }



        public override void Event_BeforeSaveInCreateAndEdit(ControllerCreateEditParameter parm)
        {
            base.Event_BeforeSaveInCreateAndEdit(parm);
            string orignalReturnUrl = parm.ReturnUrl;
            parm.ReturnUrl = Url.Action("Edit", "Products", new { id = parm.Entity.Id, returnUrl = orignalReturnUrl });
        }




        //public ActionResult AddFeature(string id, string parentName, string returnUrl)
        //{
        //    id.IsNullOrWhiteSpaceThrowArgumentException("Id");
        //    parentName.IsNullOrWhiteSpaceThrowArgumentException("parentName");
        //    returnUrl.IsNullOrWhiteSpaceThrowArgumentException("returnUrl");

        //    MenuFeatureModel menuFeatureModel = new MenuFeatureModel();
        //    menuFeatureModel.ParentId = id;
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
        //        MenuPath1Biz.AddFeature(menuFeatureModel);
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
