using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.MenuNS;

namespace MarketPlace.Web6.Controllers
{
    public class MenusController : EntityAbstractController<MenuPathMain>
    {


        public MenusController(MenuBiz menuBiz, IErrorSet errorSet, UserBiz userbiz)
            : base(menuBiz, errorSet,  userbiz)
        {
        }
        #region Index

        public override async Task<ActionResult> Index(string id, string searchFor, string selectedId, MenuLevelENUM menuLevelEnum = MenuLevelENUM.unknown, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false, string productCat1Id = "", string productCat2Id = "", string productCat3Id = "")
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
                case MenuLevelENUM.Level_4:
                default:
                    menuLevelEnum = MenuLevelENUM.unknown;
                    break;
            }
            return await base.Index(id, searchFor, selectedId, menuLevelEnum, sortBy, print,productCat1Id, productCat2Id , productCat3Id );
        }




        private MenuPathMain loadTheProductCategoryMain(ControllerIndexParams parm)
        {
            string cat1Id = parm.Menu.ProductCat1Id;
            string cat2Id = parm.Menu.ProductCat2Id;
            string cat3Id = parm.Menu.ProductCat3Id;

            ViewBag.ProductCategory1SelectList = ((MenuBiz)Biz).ProductCat1_SelectList();
            ViewBag.ProductCategory2SelectList = ((MenuBiz)Biz).ProductCat2_SelectList();
            ViewBag.ProductCategory3SelectList = ((MenuBiz)Biz).ProductCat3_SelectList();

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
                    if (cat1Id.IsNullOrWhiteSpace())
                    {
                        ErrorsGlobal.Add("Cat 1 is not loaded.", MethodBase.GetCurrentMethod());
                        throw new Exception(ErrorsGlobal.ToString());
                    }
                    pcm.MenuPath1Id = cat1Id;
                    break;

                case MenuLevelENUM.Level_3:
                    if (cat1Id.IsNullOrWhiteSpace())
                    {
                        ErrorsGlobal.Add("Cat 1 is not loaded.", MethodBase.GetCurrentMethod());
                        throw new Exception(ErrorsGlobal.ToString());
                    }
                    if (cat2Id.IsNullOrWhiteSpace())
                    {
                        ErrorsGlobal.Add("Cat 2 is not loaded.", MethodBase.GetCurrentMethod());
                        throw new Exception(ErrorsGlobal.ToString());
                    }
                    pcm.MenuPath1Id = cat1Id;
                    pcm.MenuPath2Id = cat2Id;
                    break;

                case MenuLevelENUM.Level_4:
                    if (cat1Id.IsNullOrWhiteSpace())
                    {
                        ErrorsGlobal.Add("Cat 1 is not loaded.", MethodBase.GetCurrentMethod());
                        throw new Exception(ErrorsGlobal.ToString());
                    }
                    if (cat2Id.IsNullOrWhiteSpace())
                    {
                        ErrorsGlobal.Add("Cat 2 is not loaded.", MethodBase.GetCurrentMethod());
                        throw new Exception(ErrorsGlobal.ToString());
                    }
                    if (cat3Id.IsNullOrWhiteSpace())
                    {
                        ErrorsGlobal.Add("Cat 3 is not loaded.", MethodBase.GetCurrentMethod());
                        throw new Exception(ErrorsGlobal.ToString());
                    }
                    pcm.MenuPath1Id = cat1Id;
                    pcm.MenuPath2Id = cat2Id;
                    pcm.MenuPath3Id = cat3Id;
                    break;

                default:
                    break;
            }
            return pcm;
        }
        #endregion

        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            MenuPathMain pcm = loadTheProductCategoryMain(parm);
            return View(pcm);
        }



    }





}