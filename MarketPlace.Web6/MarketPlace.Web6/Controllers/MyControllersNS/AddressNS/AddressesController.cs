using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.AddressNS;
using System.Web.Mvc;
using UowLibrary.AddressNS;
using UowLibrary.ParametersNS;

namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public partial class AddressesController : EntityAbstractController<AddressWithId>
    {

        AddressBiz _addressBiz;

        public AddressesController(AddressBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _addressBiz = biz;
        }

        public AddressBiz AddressBiz
        {
            get
            {
                return _addressBiz;
            }
        }

    }
}