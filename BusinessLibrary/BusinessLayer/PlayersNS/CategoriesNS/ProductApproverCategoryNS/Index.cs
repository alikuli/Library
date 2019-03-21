using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.PlayersNS.ProductApproverCategoryNS
{
    public partial class ProductApproverCategoryBiz
    {


        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "Product Approver Category";
            indexListVM.Show.EditDeleteAndCreate = true;

        }





    }
}
