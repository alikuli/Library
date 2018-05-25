using System.Threading.Tasks;
using System.Web.Mvc;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using UowLibrary.PaymentTermNS;

namespace MarketPlace.Web6.Controllers
{
    public class PaymentTermsController : EntityAbstractController<PaymentTerm>
    {

        PaymentTermBiz _paymentTermsBiz;
        #region Constructo and initializers

        public PaymentTermsController(PaymentTermBiz PaymentTermsBiz, IErrorSet errorSet)
            : base(PaymentTermsBiz, errorSet) 
        {
            _paymentTermsBiz = PaymentTermsBiz;
        }

        #endregion

        
    }
}