using System;
using System.Collections;
using System.Reflection;
using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ErrorHandlerLibrary.ExceptionsNS;
using Microsoft.AspNet.Identity;
using UowLibrary.Interface;
using WebLibrary.Programs;
using ErrorHandlerLibrary;

namespace UowLibrary.Abstract
{
    /// <summary>
    /// Note, for this to work properly you need to send the UserIdString into this from the Controller or any other program that is on top
    /// To see how a controller sends it see the OnActionExecuting(ActionExecutingContext filterContext) method in the Abstract Controller.
    /// and then the properties Uow and UowManagment/UowAccounts in ManagementController and AccountController 
    /// </summary>
    public abstract partial class AbstractBiz 
    {



        /// <summary>
        /// This gets the errors from the result and adds them up
        /// </summary>
        /// <param name="result"></param>
        /// <param name="methodBase"></param>
        public void AddErrorsFrom(IdentityResult result)
        {
            if (!result.Errors.IsNull() && ((ICollection)result.Errors).Count > 0)
            {
                foreach (var error in result.Errors)
                {
                    ErrorsGlobal.Add(error, "");
                }
            }
        }



    }


}



