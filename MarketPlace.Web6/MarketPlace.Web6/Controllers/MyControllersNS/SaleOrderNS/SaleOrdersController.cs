using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary.AddressNS;
using UowLibrary.BuySellDocNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.CustomerNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.PlayersNS.PersonNS;

namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public class SaleOrdersController : EntityAbstractController<BuySellDoc>
    {

        SaleOrderBiz _saleOrderBiz;
        AddressBiz _addressBiz;

        public SaleOrdersController(SaleOrderBiz saleOrderBiz, AddressBiz addressBiz, AbstractControllerParameters param)
            : base(saleOrderBiz, param)
        {
            _saleOrderBiz = saleOrderBiz;
            _addressBiz = addressBiz;
        }

        SaleOrderBiz SaleOrderBiz
        {
            get
            {
                _saleOrderBiz.UserId = UserId;
                _saleOrderBiz.UserName = UserName;
                return _saleOrderBiz;
            }
        }
        CustomerBiz CustomerBiz
        {
            get
            {
                return SaleOrderBiz.CustomerBiz;
            }
        }

        OwnerBiz OwnerBiz
        {
            get
            {
                return SaleOrderBiz.OwnerBiz;
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
                return _saleOrderBiz.PersonBiz;
            }
        }

        BuySellDocBiz BuySellDocBiz
        {
            get
            {

                return _saleOrderBiz;
            }
        }


    }
}