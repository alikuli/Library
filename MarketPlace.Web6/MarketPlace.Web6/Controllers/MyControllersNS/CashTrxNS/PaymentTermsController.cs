using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using UowLibrary.ParametersNS;
using UowLibrary.PaymentTermNS;

namespace MarketPlace.Web6.Controllers
{
    public class PaymentTermsController : EntityAbstractController<PaymentTerm>
    {

        PaymentTermBiz _paymentTermsBiz;

        public PaymentTermsController(PaymentTermBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _paymentTermsBiz = biz;
        }



    }
}