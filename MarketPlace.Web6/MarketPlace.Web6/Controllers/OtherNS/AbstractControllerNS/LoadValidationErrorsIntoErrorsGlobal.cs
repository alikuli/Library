using AliKuli.Extentions;
using System.Collections.Generic;
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
                            ErrorsGlobal.Add(modelError.Exception.Message, MethodBase.GetCurrentMethod());
                    }
                }
            }
        }




    }
}