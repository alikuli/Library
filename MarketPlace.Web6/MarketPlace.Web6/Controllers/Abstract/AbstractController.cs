using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.Interface;
using System.Linq;
using UserModels;
using ModelsClassLibrary.ModelsNS.SharedNS;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;

namespace MarketPlace.Web4.Controllers
{
    /// <summary>
    /// Note. The Error_Controller and the Error_Uow are the same
    /// After exectuing an action, the AbstractController automaticlly adds the errors.
    /// You must wire up tye Uow in the Controllers so that the Uow goes into Uow and then
    /// you can create one from that which is for that particular controller if you need it.
    /// </summary>
    public abstract class AbstractController : Controller
    {

        private string _userId;
        private IErrorSet _ierrorSet;
        private UserBiz _userBiz;

        /// <summary>
        /// All errorsets taken from DI point to the same reference.
        /// </summary>
        /// <param name="errorSet"></param>

        public AbstractController(IErrorSet errorSet, UserBiz userBiz)
        {
            _ierrorSet = errorSet;
            _userBiz = userBiz;

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


        public ErrorSet ErrorsGlobal
        {
            get
            {
                return (ErrorSet)_ierrorSet;
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


        protected ControllerIndexParams MakeControlParameters(string id, string searchFor, string isandForSearch, string selectedId, ICommonWithId entity, ICommonWithId dudEntity, MenuLevelENUM menuLevelEnum = MenuLevelENUM.unknown, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false, string returnUrl = "", string menuPathMainId = "", string productId = "", string productChildId = "", ActionNameENUM actionNameEnum = ActionNameENUM.Unknown)
        {
            //FactoryParameters fp = new FactoryParameters();

            //load parameters
            string logoAddress = Server.MapPath(AliKuli.ConstantsNS.MyConstants.LOGO_LOCATION);

            ApplicationUser user = GetApplicationUser();
            bool isUserAdmin = IsUserAdmin(user);

            //todo note... the company name is missing. We may need it.



            ControllerIndexParams parms = new ControllerIndexParams(
                id,
                searchFor,
                isandForSearch,
                selectedId,
                menuLevelEnum,
                sortBy,
                logoAddress,
                entity,
                dudEntity as ICommonWithId,
                user,
                isUserAdmin,
                returnUrl,
                menuPathMainId,
                productId,
                productChildId,
                actionNameEnum);

            ViewBag.ReturnUrl = returnUrl;

            return parms;
        }



        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ErrorsGlobal.MemorySave();
            base.OnActionExecuted(filterContext);

        }


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //get any errors that are in the memory and save them in the list
            ErrorsGlobal.MemoryRetrieve();
            LoadValidationErrorsIntoErrorsGlobal();
            LoadErrorsIntoModelState();
            LoadMessagesIntoModelState();
            base.OnActionExecuting(filterContext);
        }
        private void LoadMessagesIntoModelState()
        {
            if (new ConfigManagerHelper().IsVerbose)
            {
                bool UowHasMessages = ErrorsGlobal.HasMessages;

                if (UowHasMessages)
                {
                    ModelState.AddModelError("", " *** Verbose Set To True. Messages are listed below this ***");


                    if (UowHasMessages)
                    {
                        foreach (var item in ErrorsGlobal.ToList_Messages())
                            ModelState.AddModelError("", item);

                        ErrorsGlobal.Messages.Clear();
                    }
                }
            }
        }


        private void LoadErrorsIntoModelState()
        {
            bool UowHasErrors = ErrorsGlobal.HasErrors;

            if (UowHasErrors)
            {
                foreach (var item in ErrorsGlobal.ToList())
                    ModelState.AddModelError("", item);

                ErrorsGlobal.Errors.Clear();
            }
        }

        protected void LoadValidationErrorsIntoErrorsGlobal()
        {
            if (!ModelState.IsValid)
            {
                var modelErrors = new List<string>();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        if (!modelError.Exception.IsNull())
                            ErrorsGlobal.Add(modelError.Exception.Message, "*** Validation Errors ***");
                    }
                }
                // do something with the error list :)
            }
        }




    }
}