using System.Collections.Generic;

namespace MarketPlace.Web4.Controllers
{
    /// <summary>
    /// Note. The Error_Controller and the Error_Uow are the same
    /// After exectuing an action, the AbstractController automaticlly adds the errors.
    /// You must wire up tye Uow in the Controllers so that the Uow goes into Uow and then
    /// you can create one from that which is for that particular controller if you need it.
    /// 
    /// This is causing a problem as it causes the modelState.IsValid to become invalid.
    /// </summary>
    public abstract partial class AbstractController
    {

        private void LoadMessagesIntoAlerts()
        {
            if (ConfigManagerHelper.IsVerbose)
            {
                if (ErrorsGlobal.HasMessages)
                {
                    List<string> messages = new List<string>();
                    messages.Add(" *** Verbose Set To True. You will see messages ***");

                    foreach (var item in ErrorsGlobal.ToList_Messages())
                        messages.Add(item);

                    @ViewBag.ListOfMessages = messages;
                    ErrorsGlobal.Messages.Clear();
                }
            }


            //if (ConfigManagerHelper.IsVerbose)
            //{
            //    bool UowHasMessages = ErrorsGlobal.HasMessages;

            //    if (UowHasMessages)
            //    {
            //        ModelState.AddModelError("", " *** Verbose Set To True. Messages are listed below this ***");


            //        if (UowHasMessages)
            //        {
            //            foreach (var item in ErrorsGlobal.ToList_Messages())
            //                ModelState.AddModelError("", item);

            //            ErrorsGlobal.Messages.Clear();
            //        }
            //    }
            //}
        }


    }
}