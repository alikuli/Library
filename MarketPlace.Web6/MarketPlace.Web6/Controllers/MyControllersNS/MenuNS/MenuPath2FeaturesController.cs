using AliKuli.Extentions;
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


        //public ActionResult CreateMenuPath2Feature(string isandForSearch, MenuENUM menuEnum = MenuENUM.CreateDefault, string productChildId = "", string menuPathMainId = "", string productId = "", string returnUrl = "", SortOrderENUM sortBy = SortOrderENUM.Item2_Asc, string searchFor = "", string selectedId = "", bool print = false, bool isMenu = false, string menuPath2Id = "")
        //{
        //    return base.Create(isandForSearch, menuEnum, productChildId, menuPathMainId, productId, returnUrl, sortBy, searchFor, selectedId, print, isMenu);
        //}

        public override void AddParentIdIfChild(ModelsClassLibrary.ModelsNS.SharedNS.ControllerIndexParams parms, string parentId)
        {
            parms.Entity.IsNullThrowException("Entity is null in MenuPath2FeaturesController.AddParentIdIfChild. Programming error.");
            MenuPath2Feature mpf = parms.Entity as MenuPath2Feature;
            mpf.MenuPath2Id = parentId;
            base.AddParentIdIfChild(parms, parentId);
        }




    }
}
