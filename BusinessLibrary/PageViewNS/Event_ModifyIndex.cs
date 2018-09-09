using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace UowLibrary.PageViewNS
{
    public partial class PageViewBiz
    {

        public override void Event_ModifyIndexItem(ModelsClassLibrary.ViewModels.IndexListVM indexListVM, ModelsClassLibrary.ViewModels.IndexItemVM indexItem, InterfacesLibrary.SharedNS.ICommonWithId icommonWithId)
        {
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithId);
            //indexItem.IsEditLocked = true;
        }


    }
}
