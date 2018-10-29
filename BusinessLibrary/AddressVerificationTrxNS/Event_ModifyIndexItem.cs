using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.AddressNS
{
    public partial class AddressVerificationTrxBiz
    {


        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, ICommonWithId icommonWithId)
        {
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithId);
            indexListVM.Show.EditDeleteAndCreate = true;

        }

    }
}
