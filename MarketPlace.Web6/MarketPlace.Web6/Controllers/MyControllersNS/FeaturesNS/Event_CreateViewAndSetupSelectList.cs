using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.ProductNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UowLibrary.FeaturesNS;

namespace MarketPlace.Web6.Controllers
{
    public partial class FeaturesController : EntityAbstractController<Feature>
    {


        public override System.Web.Mvc.ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {

            ViewBag.SelectListMenupath1 = FeatureBiz.MenuPath1Biz.SelectList();
            ViewBag.SelectListMenupath2 = FeatureBiz.MenuPath2Biz.SelectList();
            ViewBag.SelectListMenupath3 = FeatureBiz.MenuPath3Biz.SelectList();
            ViewBag.SelectListProduct = FeatureBiz.ProductBiz.SelectList();
            ViewBag.SelectListProductChild = FeatureBiz.ProductChildBiz.SelectList();

            FeatureTypeENUM TypeEnum = FeatureTypeENUM.Unknown;
            ViewBag.SelectListFeaturesTypeEnum = EnumExtention.ToSelectListSorted<FeatureTypeENUM>(TypeEnum);
            //ViewBag.SelectListUser = FeatureBiz.UserBiz.SelectList();

            InitializeMenuManager(parm);

            return base.Event_CreateViewAndSetupSelectList(parm);
        }

    }
}