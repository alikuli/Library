using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ShopNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.BusinessLayer.ProductNS.ShopNS;
using UowLibrary.FeatureNS.MenuFeatureNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.CustomerNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.ProductNS;
using UowLibrary.SuperLayerNS;

namespace MarketPlace.Web6.Controllers
{
    public class ShopsController : EntityAbstractController<Product>
    {

        SuperBiz _superBiz;
        //ProductApproverBiz _productApproverBiz;
        //MenuFeatureBiz _menuFeatureBiz;
        //OwnerBiz _ownerBiz;
        public ShopsController(SuperBiz superbiz, AbstractControllerParameters param)
            : base(superbiz.ShopBiz, param)
        {
            _superBiz = superbiz;
        }
        SuperBiz SuperBiz
        {
            get
            {
                _superBiz.UserId = UserId;
                _superBiz.UserName = UserName;
                return _superBiz;
            }
        }
        ShopBiz ShopBiz
        {
            get
            {
                return SuperBiz.ShopBiz;
            }
        }
        OwnerBiz OwnerBiz
        {
            get
            {

                return ShopBiz.OwnerBiz; ;
            }

        }
        MenuFeatureBiz MenuFeatureBiz
        {
            get
            {

                return ShopBiz.MenuFeatureBiz;
            }
        }
        UserBiz UserBiz
        {
            get
            {
                return ShopBiz.UserBiz;
            }
        }

        ProductBiz ProductBiz
        {
            get
            {

                return ShopBiz.ProductBiz;
            }
        }

        CustomerBiz CustomerBiz
        {
            get
            {
                return SuperBiz.CustomerBiz;
            }
        }

        MenuPathMainBiz MenuPathMainBiz
        {
            get
            {
                return ProductBiz.MenuPathMainBiz;
            }
        }
        public override ActionResult Event_Create_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            UserId.IsNullOrWhiteSpaceThrowArgumentException("You must be logged in.");
            Product product = parm.Entity as Product;
            product.IsNullThrowException("Unable to unbox");

            ShopBiz.LoadMenuPathCheckedBoxes(parm);
            ShopBiz.FixProductFeatures(product);


            set_selectLists(product);

            if (!parm.ReturnUrl.IsNullOrWhiteSpace())
            {
                product.MenuManager.ReturnUrl = parm.ReturnUrl;
            }

            //if (!UserId.IsNullOrWhiteSpace())
            //{
            //    product.ShowApproveButton = ProductApproverBiz.IsApprover(UserId);

            //}

            if (parm.Entity.MenuManager.IsCreate)
            {
                //this controls where the product Create returns.
                //product.MenuManager.ReturnUrl = Url.Action("Index", "Products", new { menuEnum = MenuENUM.IndexMenuProduct });

                //the return URl is correct when in the Menu

                if (product.MenuManager.ReturnUrl.IsNullOrWhiteSpace())
                    product.MenuManager.ReturnUrl = Url.Action("Index", "Menus", new { menuEnum = MenuENUM.IndexMenuPath1 });

                ShopBiz.FixMenuPaths(parm);
                Owner owner = OwnerBiz.GetPlayerFor(UserId);
                owner.IsNullThrowException("You are not authourized to sell");
                product.OwnerId = owner.Id;


            }


            return base.Event_Create_ViewAndSetupSelectList_GET(parm);

        }

        private void set_selectLists(Product product)
        {
            //product.ParentSelectList = _productBiz.SelectList_ForParent(parm.Entity);
            product.SelectListUomPurchase = ShopBiz.SelectList_UomPurchaseQty();
            product.SelectListUomVolume = ShopBiz.SelectList_UomVolume();
            product.SelectListUomShipWeight = ShopBiz.SelectList_UomWeight();
            product.SelectListUomWeight = ShopBiz.SelectList_UomWeight();
            product.SelectListUomLength = ShopBiz.SelectList_UomLength();
            product.SelectListUomDimensionsLength = ShopBiz.SelectList_UomLength();
            product.SelectListOwners = OwnerBiz.SelectList();
        }

        public override ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            Product product = parm.Entity as Product;
            product.IsNullThrowException("Unable to unbox");

            ShopBiz.LoadMenuPathCheckedBoxes(parm);
            ShopBiz.FixProductFeatures(product);



            set_selectLists(product);

            if (!parm.ReturnUrl.IsNullOrWhiteSpace())
            {
                product.MenuManager.ReturnUrl = parm.ReturnUrl;
            }

            //if (!UserId.IsNullOrWhiteSpace())
            //{
            //    product.ShowApproveButton = ProductApproverBiz.IsApprover(UserId);

            //}

            if (parm.Entity.MenuManager.IsCreate)
            {
                //this controls where the product Create returns.
                //product.MenuManager.ReturnUrl = Url.Action("Index", "Products", new { menuEnum = MenuENUM.IndexMenuProduct });

                //the return URl is correct when in the Menu

                if (product.MenuManager.ReturnUrl.IsNullOrWhiteSpace())
                    product.MenuManager.ReturnUrl = Url.Action("Index", "Menus", new { menuEnum = MenuENUM.IndexMenuPath1 });

                ProductBiz.FixMenuPaths(parm);

            }
            return base.Event_Edit_ViewAndSetupSelectList_GET(parm);
        }


        [HttpGet]
        public ActionResult EditShop(string returnUrl, string productId)
        {
            try
            {

                ShopVM shopCreate = SuperBiz.Setup_Shop_For_Edit_Get(returnUrl, productId);
                return View("Shop", shopCreate);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), ex);
                if (returnUrl.IsNullOrWhiteSpace())
                    return RedirectToAction("Index", "Menus", null);
            }
            return Redirect(returnUrl);
        }

        [HttpPost]
        public async Task<ActionResult> EditShop(ShopVM shopCreate, string button, HttpPostedFileBase[] httpMiscUploadedFiles = null)
        {
            try
            {
                shopCreate.IsNullThrowExceptionArgument("shopCreate");
                button.IsNullOrWhiteSpaceThrowArgumentException("button");
                ShopCreatedSuccessfullyVM scsVM = await SuperBiz.Setup_To_Edit_Shop_Post_Async(shopCreate, button, httpMiscUploadedFiles);
                
                if (scsVM.IsNull())
                    return Redirect(shopCreate.ReturnUrl);

                return View("ShopUpdatedSuccessfully", scsVM);

            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), ex);
                return RedirectToAction("EditShop", new { returnUrl = shopCreate.ReturnUrl, menuPathMainId = shopCreate.MenuPathMainId });

            }
        }


        [HttpGet]
        public ActionResult CreateShop(string returnUrl, string menuPathMainId)
        {
            try
            {

                ShopVM shopCreate = SuperBiz.Setup_To_Create_Shop_Get(returnUrl, menuPathMainId);
                return View("Shop", shopCreate);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), ex);
                if (returnUrl.IsNullOrWhiteSpace())
                    return RedirectToAction("Index", "Menus", null);
            }
            return Redirect(returnUrl);
        }


        [HttpPost]
        public async Task<ActionResult> CreateShop(ShopVM shopCreate, string button, System.Web.HttpPostedFileBase[] httpMiscUploadedFiles = null)
        {

            try
            {
                ShopCreatedSuccessfullyVM scsVM = await SuperBiz.Setup_To_Create_Shop_Post_Async(shopCreate, button, httpMiscUploadedFiles);
                return View("ShopCreatedSuccessfully", scsVM);

            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), ex);
                return RedirectToAction("CreateShop", new { returnUrl = shopCreate.ReturnUrl, menuPathMainId = shopCreate.MenuPathMainId });

            }


        }



    }
}
