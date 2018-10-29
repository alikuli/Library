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
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {


            ErrorsGlobal.MemorySave();

            base.OnActionExecuted(filterContext);
            
            if(!ErrorsGlobal.HasErrors)
                PageViewBiz.SavePageView(filterContext, Request);

        }


        [AllowAnonymous]
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //get any errors that are in the memory and save them in the list
            ErrorsGlobal.MemoryRetrieve();
            LoadValidationErrorsIntoErrorsGlobal();
            LoadErrorsForDisplay();

            //If we load messages, we casue the ModelState.IsValid to become false. 
            //use some other way to transport messages.

            LoadMessagesIntoAlerts();

            base.OnActionExecuting(filterContext);

        }




    }
}