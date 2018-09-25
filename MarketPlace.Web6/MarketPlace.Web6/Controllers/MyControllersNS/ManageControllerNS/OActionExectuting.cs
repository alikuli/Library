using AliKuli.Extentions;
using BreadCrumbsLibraryNS.Programs;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web4.Controllers;
using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using UserModelsLibrary.ModelsNS;

namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public partial class ManageController
    {

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            ManageControllerBiz.UserId = UserId;
            ManageControllerBiz.UserName = UserName;
        }
    }
}