using AliKuli.Extentions;
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

        public MenusController(MenuBiz menuBiz, MenuPath1Biz menuPath1Biz, MenuPath2Biz menuPath2Biz, IErrorSet errorSet, UserBiz userbiz)
            : base(menuBiz, errorSet, userbiz)
        {
            _menuBiz = menuBiz;
            _menuPath1Biz = menuPath1Biz;
            _menuPath2Biz = menuPath2Biz;
        }



        #region Index

        public override async Task<ActionResult> Index(string id, string searchFor, string isandForSearch, string selectedId, string returnUrl, string productId, string menuPathMainId, string productChildId, MenuLevelENUM menuLevelEnum = MenuLevelENUM.unknown, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false)
        {
            switch (menuLevelEnum)
            {
                case MenuLevelENUM.unknown:
                    menuLevelEnum = MenuLevelENUM.Level_1;  //MenuPathMain
                    if (!id.IsNullOrWhiteSpace())
                        menuPathMainId = id;
                    break;

                case MenuLevelENUM.Level_1:
                    menuLevelEnum = MenuLevelENUM.Level_2; //MenuPathMain
                    if (!id.IsNullOrWhiteSpace())
                        menuPathMainId = id;

                    break;

                case MenuLevelENUM.Level_2:
                    menuLevelEnum = MenuLevelENUM.Level_3; //MenuPathMain
                    if (!id.IsNullOrWhiteSpace())
                        menuPathMainId = id;
                    break;

                case MenuLevelENUM.Level_3:
                    menuLevelEnum = MenuLevelENUM.Level_4; //Products Level
                    if (!id.IsNullOrWhiteSpace())
                        menuPathMainId = id;

                    break;

                case MenuLevelENUM.Level_4:
                    menuLevelEnum = MenuLevelENUM.Level_5; //Products Child Level
                    if (!id.IsNullOrWhiteSpace())
                        productId = id;
                    break;

                case MenuLevelENUM.Level_5:
                default:
                    //menuLevelEnum = MenuLevelENUM.unknown;
                    if (!id.IsNullOrWhiteSpace())
                        productChildId = id;
                    break;
            }
            return await base.Index(id, searchFor, isandForSearch, selectedId, returnUrl, productId, menuPathMainId, productChildId, menuLevelEnum, sortBy, print);
        }




        private void loadTheMenuPathMain(ControllerIndexParams parm)
        {




            ////create the select list depending on the menu level
            //switch (parm.Menu.MenuLevel)
            //{
            //    case MenuLevelENUM.unknown:
            //        break;

            //    case MenuLevelENUM.Level_1:

            //    case MenuLevelENUM.Level_2:


            //    case MenuLevelENUM.Level_3:
            //        parm.Menu.MenuPathMainId.IsNullOrWhiteSpaceThrowException("Menu Path Main is not loaded.");
            //        MenuPathMain pcm = _icrudBiz.Find(parm.Menu.MenuPathMainId);

            //        break;

            //    case MenuLevelENUM.Level_4: //Product level
            //        break;

            //    case MenuLevelENUM.Level_5: //Product Child level
            //        throw new NotImplementedException();

            //    default:
            //        break;
            //}
            //return pcm;
        }
        #endregion


        public override async Task<ActionResult> Create(MenuPathMain entity, string returnUrl, string menuPathMainId, string productId, string productChildId, string MenuPath1Id, string MenuPath2Id, string MenuPath3Id, System.Web.HttpPostedFileBase[] httpMiscUploadedFiles = null, System.Web.HttpPostedFileBase[] httpSelfieUploads = null, System.Web.HttpPostedFileBase[] httpIdCardFrontUploads = null, System.Web.HttpPostedFileBase[] httpIdCardBackUploads = null, System.Web.HttpPostedFileBase[] httpPassportFrontUploads = null, System.Web.HttpPostedFileBase[] httpPassportVisaUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseFrontUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseBackUploads = null, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false, string isandForSearch = "", MenuLevelENUM menuLevelEnum = MenuLevelENUM.unknown, FormCollection fc = null)
        {

            try
            {
                fixTheMenuPath123IdValues(entity, MenuPath1Id, MenuPath2Id, MenuPath3Id);

                return await base.Create(entity, returnUrl, menuPathMainId, productId, productChildId, MenuPath1Id, MenuPath2Id, MenuPath3Id, httpMiscUploadedFiles, httpSelfieUploads, httpIdCardFrontUploads, httpIdCardBackUploads, httpPassportFrontUploads, httpPassportVisaUploads, httpLiscenseFrontUploads, httpLiscenseBackUploads, sortBy, searchFor, selectedId, print, isandForSearch, menuLevelEnum, fc);

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("Error in Menu Controller.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();

                return RedirectToAction("Index", new { id = "", searchFor = searchFor, isandForSearch = isandForSearch, selectedId = selectedId, returnUrl = returnUrl, productId = productId, menuPathMainId = menuPathMainId, productChildId = productChildId, menuLevelEnum = menuLevelEnum, sortBy = sortBy, print = print });

            }
        }

        private static void fixTheMenuPath123IdValues(MenuPathMain entity, string menuPath1Id, string menuPath2Id, string menuPath3Id)
        {
            //fix the menupath1, 2 and 3
            if (entity.MenuPath1Id.IsNullOrWhiteSpace())
            {
                menuPath1Id.IsNullOrWhiteSpaceThrowException();
                entity.MenuPath1Id = menuPath1Id;
            }
            if (entity.MenuPath2Id.IsNullOrWhiteSpace())
            {
                menuPath2Id.IsNullOrWhiteSpaceThrowException();
                entity.MenuPath2Id = menuPath2Id;
            }
            if (entity.MenuPath3Id.IsNullOrWhiteSpace())
            {
                menuPath3Id.IsNullOrWhiteSpaceThrowException();
                entity.MenuPath3Id = menuPath3Id;
            }
        }




        //public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        //{

        //    //I have made it a convention to pass the correct id in its own named Id Path. So we are going to use that.
        //    //MenuPathMain entity = Biz.EntityFactoryForHttpGet() as MenuPathMain;

        //    //This is where the dropdown boxes are loaded with their initial values in the create for Menus
        //    loadMp1AndMp2Ids(parm);
        //    loadSelectLists();





        //    ((MenuPathMain) parm.DudEntity).MenuManager = new MenuManager(
        //        "",
        //        parm.DudEntity as MenuPathMain, 
        //        null, 
        //        null, 
        //        parm.Menu.MenuLevelEnum, 
        //        parm.Menu.ReturnUrl, 
        //        true, 
        //        "", 
        //        parm.SelectedId, 
        //        parm.SearchFor, 
        //        parm.SortBy, 
        //        parm.ActionNameEnum);

        //    return View(parm.DudEntity);


        //}

        //private void loadSelectLists()
        //{
        //    ViewBag.MenuPath2SelectList = _menuBiz.MenuPath2_SelectList();
        //    ViewBag.MenuPath1SelectList = _menuBiz.MenuPath1_SelectList();
        //    ViewBag.MenuPath3SelectList = _menuBiz.MenuPath3_SelectList();
        //}

        //private void loadMp1AndMp2Ids(ControllerIndexParams parm)
        //{
        //    MenuPathMain mpm = null;
        //    if (!parm.Menu.MenuPathMainId.IsNullOrWhiteSpace())
        //    {
        //        mpm = Biz.Find(parm.Menu.MenuPathMainId);
        //        mpm.IsNullThrowException();
        //        _menuBiz.Detach(mpm);
        //        MenuPathMain dudMenuPathMain = parm.DudEntity as MenuPathMain;

        //        //Which mpm is this?
        //        switch (parm.Menu.MenuLevelEnum)
        //        {
        //            case MenuLevelENUM.Level_1:
        //                break;
        //            case MenuLevelENUM.Level_2:

        //                //This mpm contains the correct:
        //                // Menupath1
        //                // MenuPath2 is a dummy.
        //                // MenuPath 3 is a dummy.


        //                mpm.MenuPath1Id.IsNullOrWhiteSpaceThrowException();
        //                mpm.MenuPath1.IsNullThrowException();

        //                dudMenuPathMain.MenuPath1 = mpm.MenuPath1;
        //                dudMenuPathMain.MenuPath1Id = mpm.MenuPath1Id;

        //            //  Note: mpm.MenuPath2Id = string.Empty; .MenuPath3Id = string.Empty;
        //                dudMenuPathMain.MenuManager.MenuPathMain = dudMenuPathMain;

        //                break;

        //            case MenuLevelENUM.Level_3:
        //                //This mpm contains the correct:
        //                // Menupath1
        //                // MenuPath2
        //                // MenuPath 3 is a dummy.
        //                mpm.MenuPath1Id.IsNullOrWhiteSpaceThrowException();
        //                mpm.MenuPath1.IsNullThrowException();

        //                mpm.MenuPath2Id.IsNullOrWhiteSpaceThrowException();
        //                mpm.MenuPath2.IsNullThrowException();

        //                dudMenuPathMain.MenuPath1 = mpm.MenuPath1;
        //                dudMenuPathMain.MenuPath1Id = mpm.MenuPath1Id;

        //                dudMenuPathMain.MenuPath2 = mpm.MenuPath2;
        //                dudMenuPathMain.MenuPath2Id = mpm.MenuPath2Id;

        //            //  Note: mpm.MenuPath2Id = string.Empty; .MenuPath3Id = string.Empty;
        //                dudMenuPathMain.MenuManager.MenuPathMain = dudMenuPathMain;

        //                break;

        //            case MenuLevelENUM.Level_4:
        //            case MenuLevelENUM.Level_5:
        //            case MenuLevelENUM.Level_6:
        //            case MenuLevelENUM.unknown:

        //            default:
        //                throw new Exception("Programming error in Menu Event_CreateViewAndSetupSelectList");
        //        }

        //        //if (menupath2SelectList.IsNull())
        //        //    menupath2SelectList = _menuBiz.MenuPath2_SelectList();

        //    }
        //}

    }





}