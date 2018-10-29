using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.AddressNS
{
    public partial class AddressBiz
    {

        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "All Addresses for User";
            indexListVM.Show.EditDeleteAndCreate = true;
            indexListVM.Show.VerificationIcon = true;

        }


    }
}
