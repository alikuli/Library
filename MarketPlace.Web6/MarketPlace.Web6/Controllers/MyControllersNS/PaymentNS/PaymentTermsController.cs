using System.Threading.Tasks;
using System.Web.Mvc;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using UowLibrary.PaymentTermNS;
using UowLibrary;
using BreadCrumbsLibraryNS.Programs;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ErrorHandlerLibrary;
using UowLibrary.PageViewNS;

namespace MarketPlace.Web6.Controllers
{
    public class PaymentTermsController : EntityAbstractController<PaymentTerm>
    {

        PaymentTermBiz _paymentTermsBiz;
        #region Constructo and initializers

        public PaymentTermsController(PaymentTermBiz biz,  AbstractControllerParameters param)
            : base(biz, param) 
        {
            _paymentTermsBiz = biz;
        }

        #endregion

        
    }
}