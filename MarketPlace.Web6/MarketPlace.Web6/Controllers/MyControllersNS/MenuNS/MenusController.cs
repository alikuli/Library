using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.MenuNS;

namespace MarketPlace.Web6.Controllers
{
    public class MenusController : EntityAbstractController<MenuPathMain>
    {


        public MenusController(MenuBiz menuBiz, IErrorSet errorSet, UserBiz userbiz)
            : base(menuBiz, errorSet, userbiz)
        {
        }
        #region Index

        public override async Task<ActionResult> Index(string id, string searchFor, string selectedId, MenuLevelENUM menuLevelEnum = MenuLevelENUM.unknown, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false, string menuPath1Id = "", string menuPath2Id = "", string menuPath3Id = "", string productId = "", string returnUrl = "")
        {
            switch (menuLevelEnum)
            {
                case MenuLevelENUM.unknown:
                    menuLevelEnum = MenuLevelENUM.Level_1;
                    break;

                case MenuLevelENUM.Level_1:
                    menuLevelEnum = MenuLevelENUM.Level_2;
                    //the id recieved belongs to the ProductMain
                    break;

                case MenuLevelENUM.Level_2:
                    menuLevelEnum = MenuLevelENUM.Level_3;
                    break;

                case MenuLevelENUM.Level_3:
                    menuLevelEnum = MenuLevelENUM.Level_4;
                    break;

                case MenuLevelENUM.Level_4: //Products Level
                    menuLevelEnum = MenuLevelENUM.Level_5;
                    break;

                case MenuLevelENUM.Level_5://Products Child Level
                default:
                    menuLevelEnum = MenuLevelENUM.unknown;
                    break;
            }
            return await base.Index(id, searchFor, selectedId, menuLevelEnum, sortBy, print, menuPath1Id, menuPath2Id, menuPath3Id, productId, returnUrl);
        }




        private MenuPathMain loadTheMenuPathMain(ControllerIndexParams parm)
        {
            string menuPath1Id = parm.Menu.MenuPath1Id;
            string menuPath2Id = parm.Menu.MenuPath2Id;
            string menuPath3Id = parm.Menu.MenuPath3Id;

            ViewBag.MenuPath1SelectList = ((MenuBiz)Biz).MenuPath1_SelectList();
            ViewBag.MenuPath2SelectList = ((MenuBiz)Biz).MenuPath2_SelectList();
            ViewBag.MenuPath3SelectList = ((MenuBiz)Biz).MenuPath3_SelectList();

            ViewBag.IsMenu = true;

            MenuPathMain pcm = Biz.EntityFactoryForHttpGet();


            //create the select list depending on the menu level
            switch (parm.Menu.MenuLevel)
            {
                case MenuLevelENUM.unknown:
                    break;

                case MenuLevelENUM.Level_1:
                    break;

                case MenuLevelENUM.Level_2:
                    menuPath1Id.IsNullOrWhiteSpaceThrowException("Menu Path 1 is not loaded.");

                    pcm.MenuPath1Id = menuPath1Id;
                    break;

                case MenuLevelENUM.Level_3:
                    menuPath1Id.IsNullOrWhiteSpaceThrowException("Menu Path 1 is not loaded.");
                    menuPath2Id.IsNullOrWhiteSpaceThrowException("Menu Path 2 is not loaded.");

                    pcm.MenuPath1Id = menuPath1Id;
                    pcm.MenuPath2Id = menuPath2Id;
                    break;

                case MenuLevelENUM.Level_4: //Product level
                    menuPath1Id.IsNullOrWhiteSpaceThrowException("Menu Path 1 is not loaded.");
                    menuPath2Id.IsNullOrWhiteSpaceThrowException("Menu Path 2 is not loaded.");
                    menuPath3Id.IsNullOrWhiteSpaceThrowException("Menu Path 3 is not loaded.");

                    pcm.MenuPath1Id = menuPath1Id;
                    pcm.MenuPath2Id = menuPath2Id;
                    pcm.MenuPath3Id = menuPath3Id;

                    break;

                case MenuLevelENUM.Level_5: //Product Child level
                    menuPath1Id.IsNullOrWhiteSpaceThrowException("Menu Path 1 is not loaded.");
                    menuPath2Id.IsNullOrWhiteSpaceThrowException("Menu Path 2 is not loaded.");
                    menuPath3Id.IsNullOrWhiteSpaceThrowException("Menu Path 3 is not loaded.");

                    pcm.MenuPath1Id = menuPath1Id;
                    pcm.MenuPath2Id = menuPath2Id;
                    pcm.MenuPath3Id = menuPath3Id;
                    throw new NotImplementedException();

                default:
                    break;
            }
            return pcm;
        }
        #endregion

        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            MenuPathMain pcm = loadTheMenuPathMain(parm);
            return View(pcm);
        }



    }





}