using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ModelsNS.ServiceRequestNS.ServiceRequestTrxNS;
using System.Web.Mvc;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.ServiceRequestTrxNS;
using UowLibrary.SuperLayerNS;


namespace MarketPlace.Web6.Controllers
{
    public class ServiceRequestTrxsController : EntityAbstractController<ServiceRequestTrx>
    {

        ServiceRequestTrxBiz _ServiceRequestTrxsBiz;
        SuperBiz _superBiz;


        public ServiceRequestTrxsController(AbstractControllerParameters param, SuperBiz superBiz)
            : base(superBiz.ServiceRequestTrxBiz, param)
        {
            _ServiceRequestTrxsBiz = superBiz.ServiceRequestTrxBiz;
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
        ServiceRequestTrxBiz ServiceRequestTrxBiz
        {
            get
            {
                return _ServiceRequestTrxsBiz;
            }
        }



    }
}