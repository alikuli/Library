using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using UowLibrary.ParametersNS;
using UowLibrary.PaymentMethodNS;

namespace MarketPlace.Web6.Controllers
{
    public class PaymentMethodsController : EntityAbstractController<PaymentMethod>
    {

        PaymentMethodBiz _PaymentMethodsBiz;

        public PaymentMethodsController(PaymentMethodBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _PaymentMethodsBiz = biz;
        }



    }
}