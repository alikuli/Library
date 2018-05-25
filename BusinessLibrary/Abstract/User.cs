using Microsoft.AspNet.Identity;
using UowLibrary.Interface;
using AliKuli.Extentions;
using System;
using System.Reflection;

namespace UowLibrary.Abstract
{
    /// <summary>
    /// Note, for this to work properly you need to send the UserIdString into this from the Controller or any other program that is on top
    /// To see how a controller sends it see the OnActionExecuting(ActionExecutingContext filterContext) method in the Abstract Controller.
    /// and then the properties Uow and UowManagment/UowAccounts in ManagementController and AccountController 
    /// </summary>
    public abstract partial class AbstractBiz : IBiz
    {

        //private string _userId;






        //public string UserId
        //{
        //    get
        //    {
        //        return _userId ?? (_userId = HttpContextBase.ApplicationInstance.Context.User.Identity.GetUserId());
        //    }
        //    set
        //    {
        //        _userId = value;
        //    }
        //}

        //protected string GetUserIdOrThrowErrorIfNull()
        //{
        //    if (UserId.IsNullOrWhiteSpace())
        //    {
        //        ErrorsGlobal.Add("Unable to create a file. User is not logged in.", MethodBase.GetCurrentMethod().Name);
        //        throw new Exception(ErrorsGlobal.ToString());
        //    }
        //    string userId = UserId;
        //    return userId;
        //}



    }


}



