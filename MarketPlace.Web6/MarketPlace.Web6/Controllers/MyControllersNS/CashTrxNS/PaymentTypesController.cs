using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DocumentsNS.PaymentsNS;
using UowLibrary.ParametersNS;
using UowLibrary.PaymentTypeNS;

namespace MarketPlace.Web6.Controllers
{
    public class PaymentTypesController : EntityAbstractController<PaymentType>
    {

        PaymentTypeBiz _PaymentTypesBiz;

        public PaymentTypesController(PaymentTypeBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _PaymentTypesBiz = biz;
        }



    }
}

