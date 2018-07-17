using AliKuli.Extentions;
using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.Interface;
using UserModels;

namespace MarketPlace.Web4.Controllers
{
    /// <summary>
    /// Note. The Error_Controller and the Error_Uow are the same
    /// After exectuing an action, the AbstractController automaticlly adds the errors.
    /// You must wire up tye Uow in the Controllers so that the Uow goes into Uow and then
    /// you can create one from that which is for that particular controller if you need it.
    /// </summary>
    public abstract partial class AbstractController : Controller
    {

        private string _userId;
        private IErrorSet _ierrorSet;
        private UserBiz _userBiz;
        private BreadCrumbManager _breadCrumbManager;

        /// <summary>
        /// All errorsets taken from DI point to the same reference.
        /// </summary>
        /// <param name="errorSet"></param>

        public AbstractController(IErrorSet errorSet, UserBiz userBiz, BreadCrumbManager breadCrumbManager)
        {
            _ierrorSet = errorSet;
            _userBiz = userBiz;
            _breadCrumbManager = breadCrumbManager;
        }

        /// <summary>
        /// There is a complication with UserStringId. It gets initialized here in the OnActionExecuting(ActionExecutingContext filterContext).
        /// Thereafter, the Uow UserStringId in the Uow initializes over here. However, to use this, the Uow must be initialized in the constructor
        /// of the calling controller.
        /// </summary>

        private IBiz _uow;

        protected IBiz Uow
        {
            get
            {
                if (_uow.IsNull())
                {
                    ErrorsGlobal.Add("Uow is not initialized.", MethodBase.GetCurrentMethod());
                    throw new Exception(ErrorsGlobal.ToString());
                }

                return _uow;
            }
            set
            {
                _uow = value;
            }
        }

        protected BreadCrumbManager BreadCrumbManager{
            get
            {
                _breadCrumbManager.IsNullThrowException();
                return _breadCrumbManager;
            }
        }

        public ErrorSet ErrorsGlobal
        {
            get
            {
                return (ErrorSet)_ierrorSet;
            }
        }

        public string HomeUrl
        {
            get
            {
                var req = new System.Web.Routing.RequestContext();
                string home = UrlHelper.GenerateUrl("Default", "Index", "Home", null, null, req, false);
                return home;
            }
        }

        #region User Properties
        protected bool IsUserAdmin(ApplicationUser user)
        {
            bool isUserAdmin = _userBiz.IsUserAdmin(user);
            return isUserAdmin;
        }

        protected ApplicationUser GetApplicationUser()
        {
            ApplicationUser user = _userBiz.FindByUserName_UserManager(UserName);
            return user;
        }


        protected string UserId
        {
            get
            {
                if (!User.IsNull())
                    return (_userId = User.Identity.GetUserId());

                return "";
            }
        }


        protected string UserName
        {

            get
            {
                if (!UserId.IsNullOrWhiteSpace())
                    return (User.Identity.GetUserName());
                return "";

            }
        }

        protected void LoadUserIntoEntity(ICommonWithId entity)
        {
            IHasUser iuser = entity as IHasUser;

            if (iuser.IsNull())
                return;

            //is user loggerd in
            UserName.IsNullOrWhiteSpaceThrowException("User is not logged in");

            iuser.User = _userBiz.FindAll().FirstOrDefault(x => x.UserName.ToLower() == UserName.ToLower());

            if (iuser.User.IsNull())
            {
                ErrorsGlobal.Add("The User was not found.", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }

            iuser.UserId = iuser.User.Id;

        }

        #endregion






    }
}