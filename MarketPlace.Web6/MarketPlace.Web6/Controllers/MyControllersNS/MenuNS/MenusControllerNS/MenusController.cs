using AliKuli.Extentions;
using BreadCrumbsLibraryNS.Programs;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.MenuNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public partial class MenusController : EntityAbstractController<MenuPathMain>
    {

        MenuBiz _menuBiz;
        MenuPath1Biz _menuPath1Biz;
        MenuPath2Biz _menuPath2Biz;

        public MenusController(MenuBiz menuBiz, MenuPath1Biz menuPath1Biz, MenuPath2Biz menuPath2Biz, IErrorSet errorSet, UserBiz userbiz, BreadCrumbManager breadCrumbManager)
            : base(menuBiz, errorSet, userbiz, breadCrumbManager)
        {
            _menuBiz = menuBiz;
            _menuPath1Biz = menuPath1Biz;
            _menuPath2Biz = menuPath2Biz;
        }



        //#region Index

        //public override async Task<ActionResult> Index(string id, string searchFor, string isandForSearch, string selectedId, string returnUrl, MenuENUM menuEnum = MenuENUM.IndexMenuPath1, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false)
        //{

        //    return await base.Index(id, searchFor, isandForSearch, selectedId, returnUrl, menuEnum, sortBy, print);
        //}




        //private void loadTheMenuPathMain(ControllerIndexParams parm)
        //{




        //    ////create the select list depending on the menu level
        //    //switch (parm.Menu.MenuLevel)
        //    //{
        //    //    case MenuLevelENUM.unknown:
        //    //        break;

        //    //    case MenuLevelENUM.Level_1:

        //    //    case MenuLevelENUM.Level_2:


        //    //    case MenuLevelENUM.Level_3:
        //    //        parm.Menu.MenuPathMainId.IsNullOrWhiteSpaceThrowException("Menu Path Main is not loaded.");
        //    //        MenuPathMain pcm = _icrudBiz.Find(parm.Menu.MenuPathMainId);

        //    //        break;

        //    //    case MenuLevelENUM.Level_4: //Product level
        //    //        break;

        //    //    case MenuLevelENUM.Level_5: //Product Child level
        //    //        throw new NotImplementedException();

        //    //    default:
        //    //        break;
        //    //}
        //    //return pcm;
        //}
        //#endregion


        //public override async Task<ActionResult> Create(MenuPathMain entity, string returnUrl, string menuPathMainId, string productId, string productChildId, string MenuPath1Id, string MenuPath2Id, string MenuPath3Id, System.Web.HttpPostedFileBase[] httpMiscUploadedFiles = null, System.Web.HttpPostedFileBase[] httpSelfieUploads = null, System.Web.HttpPostedFileBase[] httpIdCardFrontUploads = null, System.Web.HttpPostedFileBase[] httpIdCardBackUploads = null, System.Web.HttpPostedFileBase[] httpPassportFrontUploads = null, System.Web.HttpPostedFileBase[] httpPassportVisaUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseFrontUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseBackUploads = null, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false, string isandForSearch = "", MenuENUM menuEnum = MenuENUM.unknown, FormCollection fc = null)
        //{

        //    try
        //    {
        //        fixTheMenuPath123IdValues(entity, MenuPath1Id, MenuPath2Id, MenuPath3Id);

        //        return await base.Create(entity, returnUrl, menuPathMainId, productId, productChildId, MenuPath1Id, MenuPath2Id, MenuPath3Id, httpMiscUploadedFiles, httpSelfieUploads, httpIdCardFrontUploads, httpIdCardBackUploads, httpPassportFrontUploads, httpPassportVisaUploads, httpLiscenseFrontUploads, httpLiscenseBackUploads, sortBy, searchFor, selectedId, print, isandForSearch, menuEnum, fc);

        //    }
        //    catch (Exception e)
        //    {

        //        ErrorsGlobal.Add("Error in Menu Controller.", MethodBase.GetCurrentMethod(), e);
        //        ErrorsGlobal.MemorySave();

        //        return RedirectToAction("Index", new { id = "", searchFor = searchFor, isandForSearch = isandForSearch, selectedId = selectedId, returnUrl = returnUrl, productId = productId, menuPathMainId = menuPathMainId, productChildId = productChildId, menuLevelEnum = menuEnum, sortBy = sortBy, print = print });

        //    }
        //}

        //private static void fixTheMenuPath123IdValues(MenuPathMain entity, string menuPath1Id, string menuPath2Id, string menuPath3Id)
        //{
        //    //fix the menupath1, 2 and 3
        //    if (entity.MenuPath1Id.IsNullOrWhiteSpace())
        //    {
        //        menuPath1Id.IsNullOrWhiteSpaceThrowException();
        //        entity.MenuPath1Id = menuPath1Id;
        //    }
        //    if (entity.MenuPath2Id.IsNullOrWhiteSpace())
        //    {
        //        menuPath2Id.IsNullOrWhiteSpaceThrowException();
        //        entity.MenuPath2Id = menuPath2Id;
        //    }
        //    if (entity.MenuPath3Id.IsNullOrWhiteSpace())
        //    {
        //        menuPath3Id.IsNullOrWhiteSpaceThrowException();
        //        entity.MenuPath3Id = menuPath3Id;
        //    }
        //}






        //public override async Task<ActionResult> Edit(MenuPathMain entity, string returnUrl, string menuPathMainId, string productId, string productChildId, string MenuPath1Id, string MenuPath2Id, string MenuPath3Id, System.Web.HttpPostedFileBase[] httpMiscUploadedFiles = null, System.Web.HttpPostedFileBase[] httpSelfieUploads = null, System.Web.HttpPostedFileBase[] httpIdCardFrontUploads = null, System.Web.HttpPostedFileBase[] httpIdCardBackUploads = null, System.Web.HttpPostedFileBase[] httpPassportFrontUploads = null, System.Web.HttpPostedFileBase[] httpPassportVisaUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseFrontUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseBackUploads = null, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false, string isandForSearch = "", MenuENUM menuEnum = MenuENUM.unknown, FormCollection fc = null)
        //{
        //    return await base.Edit(entity, returnUrl, menuPathMainId, productId, productChildId, MenuPath1Id, MenuPath2Id, MenuPath3Id, httpMiscUploadedFiles, httpSelfieUploads, httpIdCardFrontUploads, httpIdCardBackUploads, httpPassportFrontUploads, httpPassportVisaUploads, httpLiscenseFrontUploads, httpLiscenseBackUploads, sortBy, searchFor, selectedId, print, isandForSearch, menuEnum, fc);
        //}
    }





}