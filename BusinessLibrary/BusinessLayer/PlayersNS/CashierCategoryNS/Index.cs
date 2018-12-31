using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.PlayersNS.CashierCategoryNS
{
    public partial class CashierCategoryBiz : BusinessLayer<CashierCategory>
    {


        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "Cashier Category";
            indexListVM.Show.EditDeleteAndCreate = true;

        }





    }
}
