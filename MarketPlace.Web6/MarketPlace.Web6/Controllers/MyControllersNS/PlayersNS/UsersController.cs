using BreadCrumbsLibraryNS.Programs;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UowLibrary;
using UowLibrary.StateNS;
using UserModels;

namespace MarketPlace.Web6.Controllers
{
    public class UsersController : EntityAbstractController<ApplicationUser>
    {

        StateBiz _stateBiz;
        public UsersController(UserBiz userBiz, IErrorSet errorSet, StateBiz stateBiz, UserBiz userbiz, BreadCrumbManager breadCrumbManager)
            : base(userBiz, errorSet, userbiz, breadCrumbManager)
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