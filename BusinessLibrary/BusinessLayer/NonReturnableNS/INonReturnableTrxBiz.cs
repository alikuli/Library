using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using UowLibrary.Interface;

namespace UowLibrary.BusinessLayer.NonReturnableNS
{
    public interface INonReturnableTrxBiz : IBiz
    {
        bool Event_LockEditDuringInitialization();
        void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters);
        void Fix(ControllerCreateEditParameter parm);
        string[] GetDataForStringArrayFormat { get; }
        string SelectListCacheKey { get; }

    }
}
