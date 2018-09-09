using BreadCrumbsLibraryNS.Programs;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UowLibrary;
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PageViewNS;
using UowLibrary.PlayersNS;
using UowLibrary.StateNS;
using UserModels;

namespace MarketPlace.Web6.Controllers
{
    public class UsersController : EntityAbstractController<ApplicationUser>
    {

        StateBiz _stateBiz;
        public UsersController(StateBiz stateBiz, UserBiz biz, BreadCrumbManager bcm, IErrorSet err, PageViewBiz pageViewBiz)
            : base(biz, bcm, err, pageViewBiz) 
        {
            _stateBiz =stateBiz;
        }


        public override System.Web.Mvc.ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            ViewBag.CountrySelectList = _stateBiz.CountrySelectList;

            return base.Event_CreateViewAndSetupSelectList(parm);
        }

    }
}