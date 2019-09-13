using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.PersonNS;
using UserModels;

namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public class UsersController : EntityAbstractController<ApplicationUser>
    {
        UserBiz _userBiz;
        PersonBiz _personBiz;
        //StateBiz _stateBiz;
        public UsersController(PersonBiz personBiz, AbstractControllerParameters param)
            : base(personBiz.UserBiz, param)
        {
            _personBiz = personBiz;

            //  _stateBiz = stateBiz;
            _userBiz = personBiz.UserBiz;

        }

        public PersonBiz PersonBiz
        {
            get
            {
                _personBiz.IsNullThrowException();
                _personBiz.UserId = UserId;
                _personBiz.UserName = UserName;

                return _personBiz;
            }
        }

        public override ActionResult Event_Create_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            //ViewBag.CountrySelectList = _stateBiz.CountrySelectList;
            //addresses are added from addresses. If there is no address, it will be added during purchaing.
            ApplicationUser userIn = parm.Entity as ApplicationUser;
            userIn.IsNullThrowException("Unable to unbox user");
            userIn.SelectListPeople = _personBiz.SelectList();
            return base.Event_Create_ViewAndSetupSelectList_GET(parm);
        }


        public override ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            //ViewBag.CountrySelectList = _stateBiz.CountrySelectList;
            //addresses are added from addresses. If there is no address, it will be added during purchaing.
            ApplicationUser userIn = parm.Entity as ApplicationUser;
            userIn.IsNullThrowException("Unable to unbox user");
            userIn.SelectListPeople = _personBiz.SelectList();
            return base.Event_Edit_ViewAndSetupSelectList_GET(parm);
        }

        [AllowAnonymous]
        public override ActionResult InitializeDb()
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