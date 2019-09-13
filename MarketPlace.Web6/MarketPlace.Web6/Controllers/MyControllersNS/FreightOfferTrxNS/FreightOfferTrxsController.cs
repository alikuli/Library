using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FreightOffersTrxNS;
using System.Web.Mvc;
using UowLibrary.FreightOffersTrxNS;
using UowLibrary.ParametersNS;

namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public class FreightOfferTrxsController : EntityAbstractController<FreightOfferTrx>
    {

        FreightOfferTrxBiz _freightOfferTrxBiz;
        //AddressBiz _addressBiz;

        public FreightOfferTrxsController(FreightOfferTrxBiz freightOfferTrxBiz, AbstractControllerParameters param)
            : base(freightOfferTrxBiz, param)
        {
            _freightOfferTrxBiz = freightOfferTrxBiz;
        }

        public FreightOfferTrxBiz FreightOfferTrxBiz
        {
            get
            {
                return _freightOfferTrxBiz;
            }
        }

    }
}