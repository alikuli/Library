using ModelsClassLibrary.ModelsNS.PlacesNS.EmailAddressNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;
using AliKuli.Extentions;

namespace MarketPlace.Web6.Controllers
{
    public partial class EmailAddressesController 
    {

        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            EmailAddress emailAddress = parm.Entity as EmailAddress;
            emailAddress.IsNullThrowException("Unable to unbox Email Address");
            emailAddress.SelectListAddressVerificationEnum = EmailAddressBiz.SelectListAddressVerificationEnum;

            return base.Event_CreateViewAndSetupSelectList(parm);
        }


    }
}