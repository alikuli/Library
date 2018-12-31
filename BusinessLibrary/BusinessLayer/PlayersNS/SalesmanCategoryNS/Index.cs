using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.PlayersNS.SalesmanCategoryNS
{
    public partial class SalesmanCategoryBiz
    {


        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "Salesman Category";
            indexListVM.Show.EditDeleteAndCreate = true;

        }





    }
}
