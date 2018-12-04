using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;
using UowLibrary.AddressNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.CustomerNS;


namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public class CustomersController : EntityAbstractController<Customer>
    {

        CustomerBiz _customerBiz;
        AddressBiz _addressBiz;
        public CustomersController(CustomerBiz biz, AbstractControllerParameters param, AddressBiz addressBiz)
            : base(biz, param)
        {
            _customerBiz = biz;
            _addressBiz = addressBiz;
        }

        AddressBiz AddressBiz
        {
            get
            {
                _addressBiz.IsNullThrowException();
                _addressBiz.UserId = UserId;
                _addressBiz.UserName = UserName;
                return _addressBiz;
            }
        }

        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You must be logged in");

            Customer customer = parm.Entity as Customer;
            customer.IsNullThrowException("Unable to unbox customer");

            customer.SelectListUser = _customerBiz.SelectListUser;
            customer.SelectListCustomerCategory = _customerBiz.SelectListCustomerCategory;

            customer.SelectListAddressBillTo = AddressBiz.SelectListBillAddress();
            customer.SelectListAddressInformTo = AddressBiz.SelectListInformAddress();
            customer.SelectListAddressShipTo = AddressBiz.SelectListShipAddress();
            return base.Event_CreateViewAndSetupSelectList(parm);
        }
    }
}