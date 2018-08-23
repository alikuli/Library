using System.Threading.Tasks;
using System.Web.Mvc;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using UowLibrary.PaymentMethodNS;
using UowLibrary;
using BreadCrumbsLibraryNS.Programs;
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ErrorHandlerLibrary;

namespace MarketPlace.Web6.Controllers
{
    public class PaymentMethodsController : EntityAbstractController<PaymentMethod>
    {

        PaymentMethodBiz _PaymentMethodsBiz;
        #region Constructo and initializers

        public PaymentMethodsController(PaymentMethodBiz biz, BreadCrumbManager bcm, IErrorSet err)
            : base(biz, bcm, err) 
        {
            _PaymentMethodsBiz = biz;
        }

        #endregion


    }
}