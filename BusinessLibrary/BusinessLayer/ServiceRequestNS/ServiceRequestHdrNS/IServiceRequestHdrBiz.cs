using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using UowLibrary.BusinessLayer.Abstract;
//using UowLibrary.PlayersNS.ServiceRequestTrxNS;
namespace UowLibrary.BusinessLayer.ServiceRequestNS.ServiceRequestHdrNS
{
    public interface IServiceRequestHdrBiz : IAbstractBiz
    {
        void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters);
        string SelectListCacheKey { get; }
        //ServiceRequestTrxBiz ServiceRequestTrxBiz { get; }
    }
}
