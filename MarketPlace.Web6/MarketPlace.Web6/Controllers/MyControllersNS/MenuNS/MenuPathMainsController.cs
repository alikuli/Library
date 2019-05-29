using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;
using UowLibrary.ParametersNS;
using UowLibrary.ProductChildNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class MenuPathMainsController : EntityAbstractController<MenuPathMain>
    {

        MenuPathMainBiz _menupathmainBiz;
        ProductChildBiz _productChildBiz;

        public MenuPathMainsController(MenuPathMainBiz biz, AbstractControllerParameters param, ProductChildBiz productChildBiz)
            : base(biz, param)
        {
            _menupathmainBiz = biz;
            _productChildBiz = productChildBiz;
        }

        ProductChildBiz ProductChildBiz
        {
            get
            {
                _productChildBiz.UserId = UserId;
                _productChildBiz.UserName = UserName;
                return _productChildBiz;
            }
        }
        MenuPathMainBiz MenuPathMainBiz
        {
            get
            {
                _menupathmainBiz.UserId = UserId;
                _menupathmainBiz.UserName = UserName;
                return _menupathmainBiz;
            }
        }

        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            ViewBag.MenuPath1SelectList = _menupathmainBiz.MenuPath1_SelectList();
            ViewBag.MenuPath2SelectList = _menupathmainBiz.MenuPath2_SelectList();
            ViewBag.MenuPath3SelectList = _menupathmainBiz.MenuPath3_SelectList();

            return base.Event_CreateViewAndSetupSelectList(parm);
        }

        //public ActionResult CreateMenuPathMain(string isandForSearch, MenuENUM menuEnum = MenuENUM.CreateDefault, string productChildId = "", string menuPathMainId = "", string productId = "", string returnUrl = "", SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false, bool isMenu = false, string parentId = "")
        //{
        //    return base.Create(isandForSearch, menuEnum, productChildId, menuPathMainId, productId, returnUrl, sortBy, searchFor, selectedId, print, isMenu, parentId);
        //}
        //public async System.Threading.Tasks.Task<ActionResult> CreateMenuPathMain(string MenuPath1Id, string MenuPath2Id, string MenuPath3Id, string returnUrl, System.Web.HttpPostedFileBase[] httpMiscUploadedFiles = null, System.Web.HttpPostedFileBase[] httpSelfieUploads = null, System.Web.HttpPostedFileBase[] httpIdCardFrontUploads = null, System.Web.HttpPostedFileBase[] httpIdCardBackUploads = null, System.Web.HttpPostedFileBase[] httpPassportFrontUploads = null, System.Web.HttpPostedFileBase[] httpPassportVisaUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseFrontUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseBackUploads = null, EnumLibrary.EnumNS.SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false, string isandForSearch = "", EnumLibrary.EnumNS.MenuENUM menuEnum = MenuENUM.CreateDefault, FormCollection fc = null)
        //{
        //    MenuPathMain entity = MenuPathMainBiz.Factory() as MenuPathMain;
        //    entity.MenuPath1Id = MenuPath1Id;
        //    entity.MenuPath2Id = MenuPath2Id;
        //    entity.MenuPath3Id = MenuPath3Id;

        //    return await base.Create(entity, returnUrl, httpMiscUploadedFiles, httpSelfieUploads, httpIdCardFrontUploads, httpIdCardBackUploads, httpPassportFrontUploads, httpPassportVisaUploads, httpLiscenseFrontUploads, httpLiscenseBackUploads, sortBy, searchFor, selectedId, print, isandForSearch, menuEnum, fc);
        //}

    }
}
