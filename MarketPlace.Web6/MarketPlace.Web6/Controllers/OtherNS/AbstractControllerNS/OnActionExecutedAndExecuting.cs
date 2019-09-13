using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using System.Reflection;
using System.Web.Mvc;

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





        [AllowAnonymous]
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                //get any errors that are in the memory and save them in the list
                ErrorsGlobal.MemoryRetrieve();

                LoadValidationErrorsIntoErrorsGlobal();


                LoadErrorsForDisplay();

                //If we load messages, we casue the ModelState.IsValid to become false. 
                //use some other way to transport messages.

                SuperCashBiz.UserId = UserId;
                SuperCashBiz.UserName = UserName;
                //this is shown in the _LoginPartial.cshtml
                getUserMoneyAccount();


                LoadMessagesIntoAlerts();

            }
            catch (System.Exception e)
            {

                ErrorsGlobal.Add("Something wrong.", MethodBase.GetCurrentMethod(), e);
            }


        }

        [AllowAnonymous]
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {


            ErrorsGlobal.MemorySave();

            base.OnActionExecuted(filterContext);

            if (!ErrorsGlobal.HasErrors)
                PageViewBiz.SavePageView(filterContext, Request);

        }

    }
}