using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationHdrNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;
using UowLibrary.AddressNS;
using UowLibrary.MailerNS;
using UowLibrary.ParametersNS;

namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public partial class AddressVerificationHdrsController : EntityAbstractController<AddressVerificationHdr>
    {

        AddressVerificationHdrBiz _addressVerificationHdrBiz;
        MailerBiz _mailerBiz;
        public AddressVerificationHdrsController(AddressVerificationHdrBiz biz, AbstractControllerParameters param, MailerBiz mailerBiz)
            : base(biz, param)
        {
            _addressVerificationHdrBiz = biz;
            _mailerBiz = mailerBiz;
        }

        public AddressVerificationHdrBiz AddressVerificationHdrBiz
        {
            get
            {
                return _addressVerificationHdrBiz;
            }
        }

        public override ActionResult Event_Create_ViewAndSetupSelectList_GET(ModelsClassLibrary.ModelsNS.SharedNS.ControllerIndexParams parm)
        {
            ViewBag.MailerSelectList = _mailerBiz.SelectList();
            return base.Event_Create_ViewAndSetupSelectList_GET(parm);
        }
        public override ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            ViewBag.MailerSelectList = _mailerBiz.SelectList();
            return base.Event_Edit_ViewAndSetupSelectList_GET(parm);
        }

    }
}