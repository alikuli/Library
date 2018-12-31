//using MarketPlace.Web6.Controllers.Abstract;
//using MarketPlace.Web6.ProgramsNS;
//using ModelsClassLibrary.ModelsNS.FeaturesNS;
//using ModelsClassLibrary.ModelsNS.SharedNS;
//using System;
//using System.Reflection;
//using System.Web.Mvc;
//using UowLibrary.FeatureNS.MenuFeatureNS;
//using UowLibrary.ParametersNS;
//using UowLibrary.ProductNS;
//using AliKuli.Extentions;


//namespace MarketPlace.Web6.Controllers.MyControllersNS.FeatureNS.MenuFeatureNS
//{
//    public class MenuFeaturesController : EntityAbstractController<MenuFeature>
//    {
//        MenuFeatureBiz _menuFeatureBiz;
//        MenuPathMainBiz _menuPathMainBiz;
//        public MenuFeaturesController(MenuPathMainBiz biz, AbstractControllerParameters param)
//            : base(biz.MenuFeatureBiz, param)
//        {
//            _menuFeatureBiz = biz.MenuFeatureBiz;
//            _menuPathMainBiz = biz;
//        }







//        MenuFeatureBiz MenuFeatureBiz { get { return _menuFeatureBiz; } }
//        MenuPathMainBiz MenuPathMainBiz { get { return _menuPathMainBiz; } }

        
        
        
//        public override ActionResult Event_CreateViewAndSetupSelectList(ModelsClassLibrary.ModelsNS.SharedNS.ControllerIndexParams parm)
//        {
//            ViewBag.SelectListMenuPath1 = MenuPathMainBiz.MenuPath1Biz.SelectList();
//            ViewBag.SelectListMenuPath2 = MenuPathMainBiz.MenuPath2Biz.SelectList();
//            ViewBag.SelectListMenuPath3 = MenuPathMainBiz.MenuPath3Biz.SelectList();

//            return base.Event_CreateViewAndSetupSelectList(parm);
//        }



//        /// <summary>
//        /// This redirects back to the edit.
//        /// </summary>
//        /// <param name="bc"></param>
//        /// <param name="parm"></param>
//        /// <returns></returns>
//        public override ActionResult RedirectFromCreateHttpPostTo(BreadCrumbsLibraryNS.Programs.BreadCrumbManager bc, ControllerCreateEditParameter parm)
//        {
//            MenuFeature mf = parm.Entity as MenuFeature;
//            mf.IsNullThrowException("Unable to box MenuFeature");

//            return RedirectToAction("Edit",new {id= mf.Id});
//        }



//        //-----------------------------------------------------------------------------------------


//        [HttpPost]
//        public ActionResult AddMenuPath1(string menuFeatureId, string menuPath1Id)
//        {
//            MenuFeature mf;
//            try
//            {
//                mf = MenuPathMainBiz.AddMenu1ItemTo_Save(menuFeatureId, menuPath1Id);
//                return Json(new
//                {
//                    viewString = Tools.RenderViewToString(ControllerContext, @"~\Views\MenuFeatures\_menu1List.cshtml", mf, true),

//                }, JsonRequestBehavior.DenyGet);
//            }
//            catch (Exception e)
//            {
//                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
//                ErrorsGlobal.MemorySave();
//            }
//            return RedirectToAction("index");
//        }


//        [HttpPost]
//        public ActionResult DeleteMenuPath1(string menuFeatureId, string menuPath1Id)
//        {
//            try
//            {
//                MenuFeature mf = MenuPathMainBiz.DeleteMenuPath1ItemFor(menuFeatureId, menuPath1Id);
//                return PartialView("_menu1List", mf);
//            }
//            catch (Exception e)
//            {
//                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
//                ErrorsGlobal.MemorySave();
//                MenuFeature mf = MenuFeatureBiz.Find(menuFeatureId);
//                return PartialView("_menu1List", mf);
//            }

//        }


//       //-----------------------------------------------------------------------------------------


//        [HttpPost]
//        public ActionResult AddMenuPath2(string menuFeatureId, string menuPath2Id)
//        {
//            MenuFeature mf;
//            try
//            {
//                mf = MenuPathMainBiz.AddMenu2ItemTo_Save(menuFeatureId, menuPath2Id);
//                return Json(new
//                {
//                    viewString = Tools.RenderViewToString(ControllerContext, @"~\Views\MenuFeatures\_menu2List.cshtml", mf, true),

//                }, JsonRequestBehavior.DenyGet);
//            }
//            catch (Exception e)
//            {
//                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
//                ErrorsGlobal.MemorySave();
//            }
//            return RedirectToAction("index");
//        }


//        [HttpPost]
//        public ActionResult DeleteMenuPath2(string menuFeatureId, string menuPath2Id)
//        {
//            try
//            {
//                MenuFeature mf = MenuPathMainBiz.DeleteMenuPath2ItemFor(menuFeatureId, menuPath2Id);
//                return PartialView("_menu2List", mf);
//            }
//            catch (Exception e)
//            {
//                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
//                ErrorsGlobal.MemorySave();
//                MenuFeature mf = MenuFeatureBiz.Find(menuFeatureId);
//                return PartialView("_menu2List", mf);
//            }

//        }


//        //-----------------------------------------------------------------------------------------



//        [HttpPost]
//        public ActionResult AddMenuPath3(string menuFeatureId, string menuPath3Id)
//        {
//            MenuFeature mf;
//            try
//            {
//                mf = MenuPathMainBiz.AddMenu3ItemTo_Save(menuFeatureId, menuPath3Id);
//                return Json(new
//                {
//                    viewString = Tools.RenderViewToString(ControllerContext, @"~\Views\MenuFeatures\_menu3List.cshtml", mf, true),

//                }, JsonRequestBehavior.DenyGet);
//            }
//            catch (Exception e)
//            {
//                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
//                ErrorsGlobal.MemorySave();
//            }
//            return RedirectToAction("index");
//        }


//        [HttpPost]
//        public ActionResult DeleteMenuPath3(string menuFeatureId, string menuPath3Id)
//        {
//            try
//            {
//                MenuFeature mf = MenuPathMainBiz.DeleteMenuPath3ItemFor(menuFeatureId, menuPath3Id);
//                return PartialView("_menu3List", mf);
//            }
//            catch (Exception e)
//            {
//                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
//                ErrorsGlobal.MemorySave();
//                MenuFeature mf = MenuFeatureBiz.Find(menuFeatureId);
//                return PartialView("_menu3List", mf);
//            }

//        }

//    }
//}