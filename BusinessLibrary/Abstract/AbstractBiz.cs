using ErrorHandlerLibrary.ExceptionsNS;
using System;
using UowLibrary.Interface;
using WebLibrary.Programs;
using AliKuli.Extentions;
using System.Reflection;
using Microsoft.AspNet.Identity;

namespace UowLibrary.Abstract
{
    /// <summary>
    /// Note, for this to work properly you need to send the UserIdString into this from the Controller or any other program that is on top
    /// To see how a controller sends it see the OnActionExecuting(ActionExecutingContext filterContext) method in the Abstract Controller.
    /// and then the properties Uow and UowManagment/UowAccounts in ManagementController and AccountController 
    /// </summary>
    public abstract partial class AbstractBiz : IBiz
    {



        public AbstractBiz(IMemoryMain memoryMain, IErrorSet errorsGlobal)
        {
            //Get parameters
            _imemoryMain = memoryMain;
            _ierrorsGlobal = errorsGlobal;
            _ierrorsGlobal.SetLibAndClass("Uow Library", "UOW_Abstract");

        }


        /// <summary>
        /// This only works in an Internet enviroment.
        /// </summary>
        private string _userIdBiz;
        public string UserIdBiz
        {
            get
            {
                return _userIdBiz ?? (_userIdBiz = HttpContextBaseBiz.ApplicationInstance.Context.User.Identity.GetUserId());
            }
        }


        private string _userNameBiz;
        public string UserNameBiz
        {
            get
            {
                return _userNameBiz ?? (_userNameBiz = HttpContextBaseBiz.ApplicationInstance.Context.User.Identity.GetUserName());
            }
        }


        protected string GetUserIdOrThrowErrorIfNull()
        {
            if (UserIdBiz.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.Add("Unable to create a file. User is not logged in.", MethodBase.GetCurrentMethod().Name);
                throw new Exception(ErrorsGlobal.ToString());
            }
            string userId = UserIdBiz;
            return userId;
        }

        public virtual void EncryptDecrypt()
        {
            throw new NotImplementedException();
        }

    }


}



