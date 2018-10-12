using ModelsClassLibrary.ViewModels;

namespace UowLibrary.AddressNS
{
    public partial class AddressBiz
    {


        //public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        //{
        //    base.Event_ModifyIndexList(indexListVM, parameters);

        //    indexListVM.Heading.Column = "All User Addresses";
        //    indexListVM.Show.EditDeleteAndCreate = true;


        //    //indexListVM.Records = "Languages";
        //}


        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, InterfacesLibrary.SharedNS.ICommonWithId icommonWithId)
        {
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithId);

        }

    }
}
