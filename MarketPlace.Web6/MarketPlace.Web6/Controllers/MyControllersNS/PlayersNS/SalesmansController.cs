using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.SalesmanNS;

namespace MarketPlace.Web6.Controllers
{

    [Authorize]
    public class SalesmansController : EntityAbstractController<Salesman>
    {

        SalesmanBiz _salesmanBiz;
        public SalesmansController(SalesmanBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _salesmanBiz = biz;
        }


        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");
            Salesman salesman = parm.Entity as Salesman;
            salesman.IsNullThrowException("Unable to unbox Salesman");

            salesman.SelectListSalesmanCategory = _salesmanBiz.SalesmanCategoryBiz.SelectList();
            salesman.SelectListBillAddress = _salesmanBiz.SelectListBillAddressesFor(UserId);
            salesman.SelectListShipAddress = _salesmanBiz.SelectListShipAddressesFor(UserId);

            salesman.SelectListPeople = _salesmanBiz.PersonBiz.SelectList(); ;

            return base.Event_CreateViewAndSetupSelectList(parm);
        }
    }
}