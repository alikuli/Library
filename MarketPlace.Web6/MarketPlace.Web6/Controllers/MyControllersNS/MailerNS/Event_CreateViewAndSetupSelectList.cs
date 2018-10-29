using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;

namespace MarketPlace.Web6.Controllers
{
    public partial class MailersController
    {

        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            ViewBag.UserSelectList = MailerBiz.SelectListUsers;
            ViewBag.TrustLevelsSelectList = MailerBiz.SelectListTrustLevel;

            return base.Event_CreateViewAndSetupSelectList(parm);
        }


    }
}