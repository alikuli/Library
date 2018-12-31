using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySell;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary.AddressNS;
using UowLibrary.BuySellDocNS;
using UowLibrary.CashTtxNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.BankNS;
using UowLibrary.PlayersNS.CustomerNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.PlayersNS.PersonNS;

namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public class BuySellDocsController : EntityAbstractController<BuySellDoc>
    {

        BuySellDocBiz _buySellDocsBiz;
        AddressBiz _addressBiz;

        public BuySellDocsController(BuySellDocBiz buySellDocsBiz, AddressBiz addressBiz, AbstractControllerParameters param)
            : base(buySellDocsBiz, param)
        {
            _buySellDocsBiz = buySellDocsBiz;
            _addressBiz = addressBiz;
        }

        //BankBiz BankBiz
        //{
        //    get
        //    {
        //        _bankBiz.IsNullThrowException();
        //        _bankBiz.UserId = UserId;
        //        _bankBiz.UserName = UserName;

        //        return _bankBiz;
        //    }
        //}

        CustomerBiz CustomerBiz
        {
            get
            {
                return _buySellDocsBiz.CustomerBiz;
            }
        }

        OwnerBiz OwnerBiz
        {
            get
            {
                return _buySellDocsBiz.OwnerBiz;
            }
        }
        AddressBiz AddressBiz
        {
            get
            {
                _addressBiz.IsNullThrowException("_addressBiz");
                _addressBiz.UserId = UserId;
                _addressBiz.UserName = UserName;
                return _addressBiz;
            }
        }
        PersonBiz PersonBiz
        {
            get
            {
                return _buySellDocsBiz.PersonBiz;
            }
        }

        BuySellDocBiz BuySellDocBiz
        {
            get
            {

                return _buySellDocsBiz;
            }
        }

        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {

            BuySellDoc buySellDoc = parm.Entity as BuySellDoc;
            buySellDoc.IsNullThrowException("Unable to unbox buySellDoc");

            buySellDoc.SelectListOwner = OwnerBiz.SelectList();
            buySellDoc.SelectListCustomer = CustomerBiz.SelectList();
            buySellDoc.SelectListAddressInformTo = AddressBiz.SelectList();
            buySellDoc.SelectListAddressShipTo = AddressBiz.SelectList();

            return base.Event_CreateViewAndSetupSelectList(parm);
        }




    }
}