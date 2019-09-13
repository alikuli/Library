using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using System.Web.Mvc;
using UowLibrary.ParametersNS;
using UowLibrary.PaymentMethodNS;
using UowLibrary.SuperLayerNS;


namespace MarketPlace.Web6.Controllers
{
    public class PaymentMethodsController : EntityAbstractController<PaymentMethod>
    {

        PaymentMethodBiz _PaymentMethodsBiz;
        SuperBiz _superBiz;


        public PaymentMethodsController(PaymentMethodBiz biz, AbstractControllerParameters param, SuperBiz superBiz)
            : base(biz, param)
        {
            _PaymentMethodsBiz = biz;
            _superBiz = superBiz;
        }

        SuperBiz SuperBiz
        {
            get
            {
                _superBiz.UserId = UserId;
                _superBiz.UserName = UserName;
                return _superBiz;
            }
        }
        PaymentMethodBiz PaymentMethodBiz
        {
            get
            {
                return _PaymentMethodsBiz;
            }
        }




        public ActionResult GetCommentFromPaymentMethod(string id)
        {
            string comment = "";
            if (!id.IsNullOrEmpty())
            {
                PaymentMethod paymentMethod = PaymentMethodBiz.Find(id);
                paymentMethod.IsNullThrowException();
                comment = paymentMethod.DetailInfoToDisplayOnWebsite;
            }
            return Json(
                new
                {
                    message = comment
                },
                JsonRequestBehavior.AllowGet);

        }



    }
}