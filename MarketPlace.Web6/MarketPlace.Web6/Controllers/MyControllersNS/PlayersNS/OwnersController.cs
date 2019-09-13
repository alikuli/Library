using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.OwnerNS;

namespace MarketPlace.Web6.Controllers
{

    [Authorize]
    public class OwnersController : EntityAbstractController<Owner>
    {

        OwnerBiz _ownerBiz;
        public OwnersController(OwnerBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _ownerBiz = biz;
        }


        public override ActionResult Event_Create_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");
            Owner owner = parm.Entity as Owner;
            owner.IsNullThrowException("Unable to unbox Owner");

            owner.SelectListOwnerCategory = _ownerBiz.OwnerCategoryBiz.SelectList();

            owner.SelectListBillAddress = _ownerBiz.SelectListBillAddressesFor(UserId);
            owner.SelectListShipAddress = _ownerBiz.SelectListShipAddressesFor(UserId);

            owner.SelectListPeople = _ownerBiz.PersonBiz.SelectList();

            return base.Event_Create_ViewAndSetupSelectList_GET(parm);
        }


        public override ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            //ViewBag.UserSelectList = MailerBiz.SelectListUsers;
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");
            Owner owner = parm.Entity as Owner;
            owner.IsNullThrowException("Unable to unbox Owner");

            owner.SelectListOwnerCategory = _ownerBiz.OwnerCategoryBiz.SelectList();

            owner.SelectListBillAddress = _ownerBiz.SelectListBillAddressesFor(UserId);
            owner.SelectListShipAddress = _ownerBiz.SelectListShipAddressesFor(UserId);

            owner.SelectListPeople = _ownerBiz.PersonBiz.SelectList();
            return base.Event_Edit_ViewAndSetupSelectList_GET(parm);
        }

    }
}