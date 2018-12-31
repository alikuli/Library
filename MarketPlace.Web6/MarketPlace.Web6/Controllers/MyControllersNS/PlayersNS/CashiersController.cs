using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.CashierNS;

namespace MarketPlace.Web6.Controllers
{

    [Authorize]
    public class CashiersController : EntityAbstractController<Cashier>
    {

        CashierBiz _cashierBiz;
        public CashiersController(CashierBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _cashierBiz = biz;
        }


        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");
            Cashier cashier = parm.Entity as Cashier;
            cashier.IsNullThrowException("Unable to unbox Cashier");

            cashier.SelectListCashierCategory = _cashierBiz.CashierCategoryBiz.SelectList();

            cashier.SelectListBillAddress = _cashierBiz.SelectListBillAddressesFor(UserId);
            cashier.SelectListShipAddress = _cashierBiz.SelectListShipAddressesFor(UserId);

            cashier.SelectListPeople = _cashierBiz.PersonBiz.SelectList();

            return base.Event_CreateViewAndSetupSelectList(parm);
        }
    }
}