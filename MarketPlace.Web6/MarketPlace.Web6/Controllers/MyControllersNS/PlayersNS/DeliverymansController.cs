using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.DeliverymanNS;


namespace MarketPlace.Web6.Controllers
{
    [Authorize]

    public class DeliverymansController : EntityAbstractController<Deliveryman>
    {

        DeliverymanBiz _deliverymanBiz;

        public DeliverymansController(DeliverymanBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _deliverymanBiz = biz;
        }


        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You must be logged in");

            Deliveryman deliveryman = parm.Entity as Deliveryman;
            deliveryman.IsNullThrowException("Unable to unbox deliveryman");

            deliveryman.SelectListPeople = _deliverymanBiz.PersonBiz.SelectList();
            deliveryman.SelectListDeliverymanCategory = _deliverymanBiz.SelectListDeliverymanCategory;
            deliveryman.SelectListAddressBillTo = _deliverymanBiz.AddressBiz.SelectListBillAddressCurrentUser();

            return base.Event_CreateViewAndSetupSelectList(parm);
        }
    }
}