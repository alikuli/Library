using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;
using AliKuli.Extentions;

namespace MarketPlace.Web6.Controllers
{
    public partial class MailersController
    {

        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            //ViewBag.UserSelectList = MailerBiz.SelectListUsers;
            Mailer mailer = parm.Entity as Mailer;
            mailer.IsNullThrowException("Unable to unbox mailer");
            mailer.SelectListTrustLevel = MailerBiz.SelectListTrustLevel;

            return base.Event_CreateViewAndSetupSelectList(parm);
        }


    }
}