using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.PlayersNS.PersonCategoryNS
{
    public partial class PersonCategoryBiz 
    {


        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "Person Category";
            indexListVM.Show.EditDeleteAndCreate = true;

        }





    }
}
