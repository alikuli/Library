using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using MarketPlace.Web4.Controllers;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ViewModels;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UowLibrary.MenuNS.MenuStateNS;

namespace MarketPlace.Web6.Controllers
{
    public partial class ProductAutomobileVMsController : AbstractController
    {

        //ProductBiz _productBiz;
        //MenuPath1Biz _menuPath1Biz;
        //UserBiz _userBiz;
        //MenuPathMainBiz _menuPathMainBiz;
        //public ProductAutomobileVMsController(IErrorSet errorSet, UserBiz userbiz, ProductBiz productBiz, MenuPath1Biz menuPath1Biz, UserBiz userBiz, MenuPathMainBiz menuPathMainBiz)
        //    : base(errorSet, userBiz)
        //{
        //    _productBiz = productBiz;
        //    _menuPath1Biz = menuPath1Biz;
        //    _userBiz = userBiz;
        //    _menuPathMainBiz = menuPathMainBiz;
        //}






        //public virtual async Task<ActionResult> Index(string id, string searchFor, string isandForSearch, string selectedId, string returnUrl, string productId, string menuPathMainId, string productChildId, MenuLevelENUM menuLevelEnum = MenuLevelENUM.unknown, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false)
        //{
        //    return RedirectToAction("Index", "Products", new { id = id, searchFor = searchFor, isandForSearch = isandForSearch, selectedId = selectedId, returnUrl = returnUrl, productId = productId, menuPathMainId = menuPathMainId, productChildId = productChildId, menuLevelEnum = menuLevelEnum, sortBy = sortBy, print = print });

        //}

        public virtual ActionResult Create(string menuPathMainId, string returnUrl)
        {
            ProductAutomobileVM pvm = createProductAutomobileAndloadCheckBoxes(menuPathMainId, returnUrl);
            loadSelectLists(pvm as ICommonWithId);
            return View(pvm);
        }

        private ProductAutomobileVM createProductAutomobileAndloadCheckBoxes(string menuPathMainId, string returnUrl)
        {
            menuPathMainId.IsNullOrWhiteSpaceThrowException();
            MenuPathMain mpm = _menuPathMainBiz.Find(menuPathMainId);
            mpm.IsNullThrowException("Unable to find Menu Menu Path");
            //we will make this mpm the default starting MenuPath

            ProductAutomobileVM pvm = new ProductAutomobileVM();
            if (pvm.MenuPathMains.IsNull())
                pvm.MenuPathMains = new List<MenuPathMain>();

            pvm.MenuPathMains.Add(mpm);//this is the default MenuPathMain
            pvm.MenuManager = new MenuManager(mpm, pvm as Product, null, MenuENUM.CreateMenuProduct, BreadCrumbManager, null);
            _productBiz.LoadMenuPathCheckedBoxes(pvm as IProduct);

            return pvm;
        }





        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(ProductAutomobileVM entity, HttpPostedFileBase[] httpMiscUploadedFiles = null, MenuENUM menuEnum = MenuENUM.CreateDefault, string returnUrl = "")
        {
            try
            {


                Product product = entity.SetupAndMakeProduct(entity);
                //LoadUserIntoEntity(product);

                ControllerCreateEditParameter parm = new ControllerCreateEditParameter(
                    product,
                    httpMiscUploadedFiles,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    MenuENUM.CreateDefault,
                    User.Identity.Name,
                    returnUrl);

                await _productBiz.CreateAndSaveAsync(parm);

                if (returnUrl.IsNullOrWhiteSpace())
                {
                    return RedirectToAction("Index", "Products", new { selectedId = entity.Id.ToString() });
                }
                return Redirect(returnUrl);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add(string.Format("'{0}' Not saved!", ((ICommonWithId)entity).Name), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", "Products", new { selectedId = entity.Id.ToString() });
            }
        }












    }
}
