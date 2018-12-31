using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.BankNS;

namespace MarketPlace.Web6.Controllers
{

    [Authorize(Roles="Administrator")]
    public class BanksController : EntityAbstractController<Bank>
    {

        BankBiz _bankBiz;
        public BanksController(BankBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _bankBiz = biz;
        }


        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");
            Bank bank = parm.Entity as Bank;
            bank.IsNullThrowException("Unable to unbox Bank");

            bank.SelectListBankCategory = _bankBiz.BankCategoryBiz.SelectList();

            bank.SelectListBillAddress = _bankBiz.SelectListBillAddressesFor(UserId);
            bank.SelectListShipAddress = _bankBiz.SelectListShipAddressesFor(UserId);

            bank.SelectListPeople = _bankBiz.PersonBiz.SelectList();

            return base.Event_CreateViewAndSetupSelectList(parm);
        }
    }
}