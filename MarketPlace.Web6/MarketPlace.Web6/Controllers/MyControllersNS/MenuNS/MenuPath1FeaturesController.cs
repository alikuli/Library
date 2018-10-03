using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using UowLibrary.FeaturesNS;
using UowLibrary.ParametersNS;

namespace MarketPlace.Web6.Controllers
{
    public class MenuPath1FeaturesController : EntityAbstractController<MenuPath1Feature>
    {

        //MenuPath1Biz _menupath1Biz;
        //FeatureBiz _featureBiz;
        public MenuPath1FeaturesController(MenuPath1FeatureBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
        }


        //public ActionResult CreateMenuPath1Feature(string isandForSearch, MenuENUM menuEnum = MenuENUM.CreateDefault, string productChildId = "", string menuPathMainId = "", string productId = "", string returnUrl = "", SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false, bool isMenu = false, string menuPath1Id = "")
        //{
        //    return base.Create(isandForSearch, menuEnum, productChildId, menuPathMainId, productId, returnUrl, sortBy, searchFor, selectedId, print, isMenu);
        //}

        public override void AddParentIdIfChild(ModelsClassLibrary.ModelsNS.SharedNS.ControllerIndexParams parms, string parentId)
        {
            parms.Entity.IsNullThrowException("Entity is null in MenuPath1FeaturesController.AddParentIdIfChild. Programming error.");
            MenuPath1Feature mpf = parms.Entity as MenuPath1Feature;
            mpf.MenuPath1Id = parentId;
            base.AddParentIdIfChild(parms, parentId);
        }



        public override System.Web.Mvc.ActionResult Event_CreateViewAndSetupSelectList(ModelsClassLibrary.ModelsNS.SharedNS.ControllerIndexParams parm)
        {
            FeatureTypeENUM TypeEnum = FeatureTypeENUM.Unknown;
            ViewBag.SelectListFeaturesTypeEnum = EnumExtention.ToSelectListSorted<FeatureTypeENUM>(TypeEnum);

            
            
            return base.Event_CreateViewAndSetupSelectList(parm);

        }
    }
}
