using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using UowLibrary.FeaturesNS;
using UowLibrary.ParametersNS;

namespace MarketPlace.Web6.Controllers
{
    public class MenuPath2FeaturesController : EntityAbstractController<MenuPath2Feature>
    {

        //MenuPath2Biz _menupath2Biz;
        //FeatureBiz _featureBiz;
        public MenuPath2FeaturesController(MenuPath2FeatureBiz biz, FeatureBiz featureBiz, AbstractControllerParameters param)
            : base(biz, param)
        {
        }



        public override void AddParentIdIfChild(ModelsClassLibrary.ModelsNS.SharedNS.ControllerIndexParams parms, string parentId)
        {
            parms.Entity.IsNullThrowException("Entity is null in MenuPath2FeaturesController.AddParentIdIfChild. Programming error.");
            MenuPath2Feature mpf = parms.Entity as MenuPath2Feature;
            mpf.MenuPath2Id = parentId;
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
