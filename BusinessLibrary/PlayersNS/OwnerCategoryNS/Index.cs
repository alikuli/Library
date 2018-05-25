using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.PlayersNS.OwnerCategoryNS
{
    public partial class OwnerCategoryBiz : BusinessLayer<OwnerCategory>
    {


        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "Owner Category";
            indexListVM.Show.EditDeleteAndCreate = true;

        }





    }
}
