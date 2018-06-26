using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.MenuNS;

namespace MarketPlace.Web6.Controllers
{
    public class MenusController : EntityAbstractController<MenuPathMain>
    {

        MenuBiz _menuBiz;
        public MenusController(MenuBiz menuBiz, IErrorSet errorSet, UserBiz userbiz)
            : base(menuBiz, errorSet, userbiz)
        {
            _menuBiz = menuBiz;
        }
        #region Index

        public override async Task<ActionResult> Index(string id, string searchFor, string isandForSearch, string selectedId, string returnUrl, string productId, string menuPathMainId, string productChildId, MenuLevelENUM menuLevelEnum = MenuLevelENUM.unknown, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false)
        {
            switch (menuLevelEnum)
            {
                case MenuLevelENUM.unknown:
                    menuLevelEnum = MenuLevelENUM.Level_1;  //MenuPathMain
                    menuPathMainId = id;
                    break;

                case MenuLevelENUM.Level_1:
                    menuLevelEnum = MenuLevelENUM.Level_2; //MenuPathMain
                    menuPathMainId = id;

                    break;

                case MenuLevelENUM.Level_2:
                    menuLevelEnum = MenuLevelENUM.Level_3; //MenuPathMain
                    menuPathMainId = id;
                    break;

                case MenuLevelENUM.Level_3:
                    menuLevelEnum = MenuLevelENUM.Level_4; //Products Level
                    menuPathMainId = id;

                    break;

                case MenuLevelENUM.Level_4:
                    menuLevelEnum = MenuLevelENUM.Level_5; //Products Child Level
                    productId = id;
                    break;

                case MenuLevelENUM.Level_5:
                default:
                    //menuLevelEnum = MenuLevelENUM.unknown;
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

        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            ViewBag.MenuPath1SelectList = _menuBiz.MenuPath1_SelectList();
            ViewBag.MenuPath2SelectList = _menuBiz.MenuPath2_SelectList();
            ViewBag.MenuPath3SelectList = _menuBiz.MenuPath3_SelectList();

            ViewBag.IsMenu = true;

            return View(parm.Entity);
        }



    }





}