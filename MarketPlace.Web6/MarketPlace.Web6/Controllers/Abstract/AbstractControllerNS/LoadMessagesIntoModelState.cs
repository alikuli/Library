using AliKuli.UtilitiesNS;
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


    }
}