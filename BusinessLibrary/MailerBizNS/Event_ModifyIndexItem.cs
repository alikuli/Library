using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.MailerNS
{
    public partial class MailerBiz
    {


        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, ICommonWithId icommonWithId)
        {
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithId);
            indexListVM.Show.EditDeleteAndCreate = true;

        }

    }
}
