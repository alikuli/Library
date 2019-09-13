using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DocumentsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using System;
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






        private void getUserMoneyAccount()
        {



            GlobalObject globalObject = SuperCashBiz.GetGlobalObject();
            //ViewBag.MoneyItemParent = globalObject;
            ViewBag.GlobalObject = globalObject;
            string customerMsg = string.Format("You can Buy. Your balance is {0:N2}", globalObject.Money_User.Refundable.MoneyAmount);
            ErrorsGlobal.AddMessage(customerMsg);

            ErrorsGlobal.MemorySave();
        }
    }
}