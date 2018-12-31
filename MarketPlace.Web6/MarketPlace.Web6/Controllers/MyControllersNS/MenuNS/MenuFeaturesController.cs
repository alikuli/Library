using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
//using ModelsClassLibrary.ModelsNS.FeaturesNS.MenuFeatureNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;
using UowLibrary.FeatureNS.MenuFeatureNS;
//using UowLibrary.FeaturesNS;
using UowLibrary.ParametersNS;

namespace MarketPlace.Web6.Controllers
{
    public class MenuFeaturesController : EntityAbstractController<MenuFeature>
    {

        //MenuPath1Biz _menupath1Biz;
        //FeatureBiz _featureBiz;
        //MenuFeatureBiz _menuFeatureBiz;
        //MenuPath1FeatureBiz _menuPath1FeatureBiz;
        public MenuFeaturesController(MenuFeatureBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            //_menuPath1FeatureBiz = biz;
            //_menuFeatureBiz = menuFeatureBiz;
        }

        //MenuFeatureBiz MenuFeatureBiz
        //{
        //    get
        //    {
        //        _menuFeatureBiz.UserId = UserId;
        //        _menuFeatureBiz.UserName = UserName;
        //        return _menuFeatureBiz; 
        //    }
        //}

        //MenuPath1FeatureBiz MenuPath1FeatureBiz
        //{
        //    get
        //    {
        //        return _menuPath1FeatureBiz;
        //    }
        //}



        //public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        //{
        //    //FeatureTypeENUM TypeEnum = FeatureTypeENUM.Unknown;
        //    //ViewBag.SelectListFeaturesTypeEnum = EnumExtention.ToSelectListSorted<FeatureTypeENUM>(TypeEnum);
        //    MenuPath1Feature menuPath1Feature = parm.Entity as MenuPath1Feature;

        //    menuPath1Feature.IsNullThrowException("Unable to unbox menuPath1Feature");
        //    menuPath1Feature.SelectListMenuFeature = MenuFeatureBiz.SelectList();
        //    menuPath1Feature.SelectListMenuPath1 = MenuPath1FeatureBiz.MenuPath1Biz.SelectList();

        //    return base.Event_CreateViewAndSetupSelectList(parm);

        //}
    }
}
