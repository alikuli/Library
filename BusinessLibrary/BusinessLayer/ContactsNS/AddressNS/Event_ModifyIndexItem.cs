using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ViewModels;
using AliKuli.Extentions;

namespace UowLibrary.AddressNS
{
    public partial class AddressBiz
    {

        //public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        //{
        //    base.Event_ModifyIndexList(indexListVM, parameters);

        //    indexListVM.Heading.Column = "All Addresses for User";
        //    indexListVM.Show.EditDeleteAndCreate = true;
        //    indexListVM.Show.VerificationIcon = true;
        //    indexListVM.Show.MakeDefaultIcon = true;

        //}

        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, InterfacesLibrary.SharedNS.ICommonWithId icommonWithId)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithId);

            //get the current Person from user
            Person person = UserBiz.GetPersonFor(UserId);
            person.IsNullThrowException("Person");

            if (indexItem.Id == person.DefaultBillAddressId)
                indexItem.IsDefault = true;

        }

    }
}
