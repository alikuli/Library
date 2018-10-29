using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.ParametersNS;
using UowLibrary.StateNS;
using UserModels;

namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public class UsersController : EntityAbstractController<ApplicationUser>
    {
        UserBiz _userBiz;
        StateBiz _stateBiz;
        public UsersController(StateBiz stateBiz, UserBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _stateBiz = stateBiz;
            _userBiz = biz;

        }


        public override System.Web.Mvc.ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            ViewBag.CountrySelectList = _stateBiz.CountrySelectList;

            return base.Event_CreateViewAndSetupSelectList(parm);
        }

        public override System.Web.Mvc.ActionResult InitializeDb()
        {
            try
            {

                _userBiz.InitializeSystem();
            }
            catch (System.Exception e)
            {

                ErrorsGlobal.Add("Error during initializing Admin", MethodBase.GetCurrentMethod(), e);

            }
            return RedirectToAction("Index", "Home");

        }
        public ActionResult CreateMailer()
        {
            return View();
        }
    }
}