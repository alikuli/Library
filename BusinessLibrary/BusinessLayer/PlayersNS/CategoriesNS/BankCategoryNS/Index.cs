using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.PlayersNS.BankCategoryNS
{
    public partial class BankCategoryBiz : BusinessLayer<BankCategory>
    {


        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "Bank Category";
            indexListVM.Show.EditDeleteAndCreate = true;

        }





    }
}
