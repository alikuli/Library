using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.ServiceRequestNS.ServiceRequestTrxNS;
using UowLibrary.BusinessLayer.ServiceRequestNS.ServiceRequestTrxNS;
using UowLibrary.ParametersNS;
using AliKuli.Extentions;

namespace UowLibrary.PlayersNS.ServiceRequestTrxNS
{
    public partial class ServiceRequestTrxBiz : BusinessLayer<ServiceRequestTrx>, IServiceRequestTrxBiz
    {
        public ServiceRequestTrxBiz(IRepositry<ServiceRequestTrx> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {

        }

        public static ServiceRequestTrxBiz Unbox(IServiceRequestTrxBiz iserviceRequestTrxBiz)
        {
            ServiceRequestTrxBiz serviceRequestTrxBiz = iserviceRequestTrxBiz as ServiceRequestTrxBiz;
            serviceRequestTrxBiz.IsNullThrowException();
            return serviceRequestTrxBiz;
        }

        public override string SelectListCacheKey
        {

            get { return "SelectListCacheKeyServiceRequestTrx"; }
        }



        public override void Event_ModifyIndexList(ModelsClassLibrary.ViewModels.IndexListVM indexListVM, ModelsClassLibrary.ModelsNS.SharedNS.ControllerIndexParams parameters)
        {

            base.Event_ModifyIndexList(indexListVM, parameters);
            indexListVM.Show.EditDeleteAndCreate = true;
        }
    }
}
