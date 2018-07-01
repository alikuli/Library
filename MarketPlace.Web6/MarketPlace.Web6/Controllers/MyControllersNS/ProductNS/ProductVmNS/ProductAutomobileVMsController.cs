using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using MarketPlace.Web4.Controllers;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ViewModels;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.MenuNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class ProductAutomobileVMsController : AbstractController
    {

        ProductBiz _productBiz;
        MenuPath1Biz _menuPath1Biz;
        UserBiz _userBiz;
        MenuPathMainBiz _menuPathMainBiz;
        public ProductAutomobileVMsController(IErrorSet errorSet, UserBiz userbiz, ProductBiz productBiz, MenuPath1Biz menuPath1Biz, UserBiz userBiz, MenuPathMainBiz menuPathMainBiz)
            : base(errorSet, userBiz)
        {
            _productBiz = productBiz;
            _menuPath1Biz = menuPath1Biz;
            _userBiz = userBiz;
            _menuPathMainBiz = menuPathMainBiz;
        }






        public virtual async Task<ActionResult> Index(string id, string searchFor, string isandForSearch, string selectedId, string returnUrl, string productId, string menuPathMainId, string productChildId, MenuLevelENUM menuLevelEnum = MenuLevelENUM.unknown, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false)
        {
            return RedirectToAction("Index", "Products", new { id = id, searchFor = searchFor, isandForSearch = isandForSearch, selectedId = selectedId, returnUrl = returnUrl, productId = productId, menuPathMainId = menuPathMainId, productChildId = productChildId, menuLevelEnum = menuLevelEnum, sortBy = sortBy, print = print });

        }

        public virtual ActionResult Create()
        {
            ProductAutomobileVM pvm = createProductAutomobileAndloadCheckBoxes();
            loadSelectLists(pvm as ICommonWithId);
            return View(pvm);
        }

        private ProductAutomobileVM createProductAutomobileAndloadCheckBoxes()
        {
            ProductAutomobileVM pvm = new ProductAutomobileVM();

            //now before sending it to LoadMenuPathCheckedBoxes you need to add the MenuPath1 for this
            MenuPath1 mp1 = _menuPath1Biz.FindByMenuPath1EnumFor(MenuPath1ENUM.Automobiles);
            mp1.IsNullThrowException("Unable to find Menu Path 1");

            //now find the MenuPathMain with this MenuPath1
            MenuPathMain mpm = _menuPathMainBiz.FindByMenuPath1Id(mp1.Id);
            mpm.IsNullThrowException("Unable to find Menu Menu Path");

            pvm.MenuManager.MenuPathMain = mpm;

            _productBiz.LoadMenuPathCheckedBoxes(pvm as IProduct);

            return pvm;
        }





        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(ProductAutomobileVM entity, HttpPostedFileBase[] httpMiscUploadedFiles = null, HttpPostedFileBase[] httpSelfieUploads = null, HttpPostedFileBase[] httpIdCardFrontUploads = null, HttpPostedFileBase[] httpIdCardBackUploads = null, HttpPostedFileBase[] httpPassportFrontUploads = null, HttpPostedFileBase[] httpPassportVisaUploads = null, HttpPostedFileBase[] httpLiscenseFrontUploads = null, HttpPostedFileBase[] httpLiscenseBackUploads = null, string searchFor = "", string isandForSearch = "", string selectedId = "", string returnUrl = "", string productId = "", string menuPathMainId = "", string productChildId = "", MenuLevelENUM menuLevelEnum = MenuLevelENUM.unknown, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false)
        {
            try
            {


                Product product = entity.SetupAndMakeProduct(menuPathMainId, entity);
                LoadUserIntoEntity(product);

                ControllerCreateEditParameter parm = new ControllerCreateEditParameter(
                    product,
                    httpMiscUploadedFiles,
                    httpSelfieUploads,
                    httpIdCardFrontUploads,
                    httpIdCardBackUploads,
                    httpPassportFrontUploads,
                    httpPassportVisaUploads,
                    httpLiscenseFrontUploads,
                    httpLiscenseBackUploads,
                    MenuLevelENUM.unknown,
                    User.Identity.Name,
                    menuPathMainId,
                    productId,
                    productChildId, 
                    returnUrl);

                await _productBiz.CreateAndSaveAsync(parm);

                if (returnUrl.IsNullOrWhiteSpace())
                {
                    return RedirectToAction("Index", "Products", new { selectedId = entity.Id.ToString(), menuPathMainId = menuPathMainId, productId = productId, productChildId = productChildId });
                }
                return Redirect(returnUrl);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add(string.Format("'{0}' Not saved!", ((ICommonWithId)entity).Name), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", "Products", new { selectedId = entity.Id.ToString(), menuPathMainId = menuPathMainId, productId = productId, productChildId = productChildId });
            }
        }








        private void loadSelectLists(ICommonWithId icommonwithid)
        {
            ViewBag.ParentSelectList = _productBiz.SelectList_ForParent(icommonwithid);
            ViewBag.UomPurchaseSelectList = _productBiz.SelectList_UomPurchaseQty();
            ViewBag.UomVolumeSelectList = _productBiz.SelectList_UomVolume();
            ViewBag.UomShipWtSelectList = _productBiz.SelectList_UomWeight();
            ViewBag.UomWeightSelectList = _productBiz.SelectList_UomWeight();
            ViewBag.UomLengthSelectList = _productBiz.SelectList_UomLength();
            ViewBag.GearTypeSelectList = _productBiz.SelectList_AutomobileGearTypeEnum();
            ViewBag.FuelTypeSelectList = _productBiz.SelectList_FuelTypeEnum();
        }


        #region Edit
        // GET: Countries/Edit/5
        /// <summary>
        /// For Menus, you must pass the menuPath1Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isandForSearch"></param>
        /// <param name="menuPath1Id"></param>
        /// <param name="menuPath2Id"></param>
        /// <param name="menuPath3Id"></param>
        /// <param name="productId"></param>
        /// <param name="menuLevelEnum"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public virtual async Task<ActionResult> Edit(string id, string selectedId = "", string searchFor = "", string isandForSearch = "", string menuPathMainId = "", string productChildId = "", string productId = "", MenuLevelENUM menuLevelEnum = MenuLevelENUM.unknown, string returnUrl = "", SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false)
        {

            try
            {
                //the id here is a product Id
                ProductAutomobileVM productAutomobileVM = await makeTheVm(id, menuPathMainId, returnUrl, menuLevelEnum, selectedId, searchFor, ActionNameENUM.Edit);
                return View(productAutomobileVM);

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add(string.Format("Not Saved!"), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", "Home", new { selectedId = id.ToString(), returnUrl = returnUrl });
            }
        }

        private async Task<ProductAutomobileVM> makeTheVm(string id, string menuPathMainId, string returnUrl, MenuLevelENUM menuLevelEnum, string selectId, string searchString, ActionNameENUM actionNameEnum)
        {
            id.IsNullThrowExceptionArgument("Id not received. Bad Request");
            menuPathMainId.IsNullOrWhiteSpaceThrowException("Menu path not defined.");
            returnUrl.IsNullOrWhiteSpaceThrowException("Return URL not defined.");

            Product product = await _productBiz.FindAsync(id);
            product.IsNullThrowException("Product not found.");


            MenuPathMain mpm = await _menuPathMainBiz.FindAsync(menuPathMainId);
            mpm.IsNullThrowException("MenuPathMain not found.");

            //Not Id is ProductId
            product.MenuManager = new MenuManager(id, mpm, product, null, menuLevelEnum, returnUrl, false, "", selectId, searchString, SortOrderENUM.Item1_Asc, actionNameEnum);

            //convert to the derived class.
            ProductAutomobileVM productAutomobileVM = ProductAutomobileVM.MakeThisClassFrom(product);

            //load 
            productAutomobileVM.RestoreNameFields();
            loadSelectLists(productAutomobileVM as ICommonWithId);
            ViewBag.ReturnUrl = returnUrl;
            _productBiz.LoadMenuPathCheckedBoxes(productAutomobileVM as IProduct);

            return productAutomobileVM;
        }


        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(ProductAutomobileVM entity, HttpPostedFileBase[] httpMiscUploadedFiles, HttpPostedFileBase[] httpSelfieUploads, HttpPostedFileBase[] httpIdCardFrontUploads, HttpPostedFileBase[] httpIdCardBackUploads, HttpPostedFileBase[] httpPassportFrontUploads, HttpPostedFileBase[] httpPassportVisaUploads, HttpPostedFileBase[] httpLiscenseFrontUploads, HttpPostedFileBase[] httpLiscenseBackUploads, FormCollection fc, string returnUrl = "", string menuPathMainId = "", string productId = "", string productChildId = "", string searchFor = "", string isandForSearch = "", MenuLevelENUM menuLevelEnum = MenuLevelENUM.unknown, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false, string selectedId = "")
        {
            //var req = Request;
            //string fileNameOnly = Path.GetFileNameWithoutExtension(files[0].FileName);
            //string extention = Path.GetExtension(files[0].FileName);


            try
            {

                entity.IsNullThrowExceptionArgument("No Entity received!");
                //Save extra fields into storage.
                entity.SaveNameFields();

                //LoadUserIntoEntity(entity);
                Product entityConvertedToProduct = entity.MakeProductFromThis();

                //get the Db Entity for this...
                Product dbEntity = _productBiz.Find(entity.Id);
                dbEntity.IsNullThrowException("Entity not found!");

                dbEntity.UpdatePropertiesDuringModify(entityConvertedToProduct);

                ControllerCreateEditParameter parm = new ControllerCreateEditParameter(
                    entity,
                    httpMiscUploadedFiles,
                    httpSelfieUploads,
                    httpIdCardFrontUploads,
                    httpIdCardBackUploads,
                    httpPassportFrontUploads,
                    httpPassportVisaUploads,
                    httpLiscenseFrontUploads,
                    httpLiscenseBackUploads,
                    MenuLevelENUM.unknown,
                    User.Identity.Name,
                    menuPathMainId,
                    productId,
                    productChildId,
                    returnUrl);

                await _productBiz.UpdateAndSaveAsync(parm);

                if (entity.ReturnUrl.IsNullOrWhiteSpace())
                    return RedirectToAction("Index", "Home", new { selectedId = entity.Id.ToString(), returnUrl = returnUrl });

                return Redirect(entity.ReturnUrl);
            }
            catch (Exception e)
            {
                ErrorsGlobal.Add(string.Format("Not saved!"), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", "Home", new { selectedId = entity.Id.ToString(), returnUrl = returnUrl });
            }
        }

        #endregion




    }
}
