using System.Threading.Tasks;
using System.Web.Mvc;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using UowLibrary.PaymentMethodNS;
using UowLibrary;

namespace MarketPlace.Web6.Controllers
{
    public class PaymentMethodsController : EntityAbstractController<PaymentMethod>
    {

        PaymentMethodBiz _PaymentMethodsBiz;
        #region Constructo and initializers

        public PaymentMethodsController(PaymentMethodBiz PaymentMethodsBiz, IErrorSet errorSet, UserBiz userbiz)
            : base(PaymentMethodsBiz, errorSet,  userbiz) 
        {
            _PaymentMethodsBiz = PaymentMethodsBiz;
        }

        #endregion

        //public async Task<ActionResult>  InitializeDb()
        //{
        //    await _PaymentMethodsBiz.InitializationDataAsync();
        //    ErrorsGlobal.Add("*** PaymentMethods Initialized", "");
        //    ErrorsGlobal.MemorySave();
        //    return RedirectToAction("Index");
        //}
        //public override void EventBeforeIndexView(ModelsClassLibrary.ViewModels.IndexListVM data)
        //{
        //    data.ShowEditDeleteAndCreate();
        //    base.EventBeforeIndexView(data);
        //}

    }
}