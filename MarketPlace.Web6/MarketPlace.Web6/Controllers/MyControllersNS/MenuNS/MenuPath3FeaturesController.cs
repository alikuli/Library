using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using UowLibrary.FeaturesNS;
using UowLibrary.ParametersNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class MenuPath3FeaturesController : EntityAbstractController<MenuPath3Feature>
    {

        MenuPath3Biz _menupath3Biz;
        //FeatureBiz _featureBiz;
        public MenuPath3FeaturesController(MenuPath3FeatureBiz biz, MenuPath3Biz menuPath3Biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _menupath3Biz = menuPath3Biz;
        }


        //public ActionResult CreateMenuPath3Feature(string isandForSearch, MenuENUM menuEnum = MenuENUM.CreateDefault, string productChildId = "", string menuPathMainId = "", string productId = "", string returnUrl = "", SortOrderENUM sortBy = SortOrderENUM.Item3_Asc, string searchFor = "", string selectedId = "", bool print = false, bool isMenu = false, string menuPath3Id = "")
        //{
        //    return base.Create(isandForSearch, menuEnum, productChildId, menuPathMainId, productId, returnUrl, sortBy, searchFor, selectedId, print, isMenu);
        //}

        public override void AddParentIdIfChild(ModelsClassLibrary.ModelsNS.SharedNS.ControllerIndexParams parms, string parentId)
        {
            parms.Entity.IsNullThrowException("Entity is null in MenuPath3FeaturesController.AddParentIdIfChild. Programming error.");
            MenuPath3Feature mpf = parms.Entity as MenuPath3Feature;
            mpf.MenuPath3Id = parentId;
            //mpf.MenuPath3 = _menupath3Biz.Find(parentId);
            base.AddParentIdIfChild(parms, parentId);
        }


        public override System.Web.Mvc.ActionResult Event_CreateViewAndSetupSelectList(ModelsClassLibrary.ModelsNS.SharedNS.ControllerIndexParams parm)
        {
            FeatureTypeENUM TypeEnum = FeatureTypeENUM.Unknown;
            ViewBag.SelectListFeaturesTypeEnum = EnumExtention.ToSelectListSorted<FeatureTypeENUM>(TypeEnum);


            ViewBag.SelectListMenuPath3 = _menupath3Biz.SelectList();
            return base.Event_CreateViewAndSetupSelectList(parm);

        }


    }
}
