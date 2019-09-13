using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationTrxNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;
using UowLibrary.AddressNS;
using UowLibrary.ParametersNS;

namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public partial class AddressVerificationTrxsController : EntityAbstractController<AddressVerificationTrx>
    {

        AddressVerificationTrxBiz _addressVerificationTrxBiz;
        AddressBiz _addressBiz;
        AddressVerificationHdrBiz _addressVerificationHdrBiz;

        public AddressVerificationTrxsController(AddressVerificationHdrBiz biz, AbstractControllerParameters param, AddressBiz addressBiz)
            : base(biz.AddressVerificationTrxBiz, param)
        {
            _addressVerificationTrxBiz = biz.AddressVerificationTrxBiz;
            _addressBiz = addressBiz;
            _addressVerificationHdrBiz = biz;
        }

        AddressVerificationHdrBiz AddressVerificationHdrBiz
        {
            get
            {
                return _addressVerificationHdrBiz;
            }
        }
        public AddressBiz AddressBiz
        {
            get
            {
                return _addressBiz;
            }
        }

        public override ActionResult Event_Create_ViewAndSetupSelectList_GET(ModelsClassLibrary.ModelsNS.SharedNS.ControllerIndexParams parm)
        {

            ViewBag.SelectListAddress = AddressBiz.SelectList();
            ViewBag.SelectListAddressVerificationHdr = AddressVerificationHdrBiz.SelectList();
            return base.Event_Create_ViewAndSetupSelectList_GET(parm);
        }

        public override ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            ViewBag.SelectListAddress = AddressBiz.SelectList();
            ViewBag.SelectListAddressVerificationHdr = AddressVerificationHdrBiz.SelectList();
            return base.Event_Edit_ViewAndSetupSelectList_GET(parm);
        }

    }
}