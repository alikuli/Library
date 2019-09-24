using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using UowLibrary.BusinessLayer.Abstract;
namespace UowLibrary.BusinessLayer.ServiceRequestNS.ServiceRequestTrxNS
{
    public interface IServiceRequestTrxBiz : IAbstractBiz
    {
        void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters);
        string SelectListCacheKey { get; }
    }
}
