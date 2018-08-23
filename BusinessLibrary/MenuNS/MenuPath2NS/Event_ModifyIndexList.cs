using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
namespace UowLibrary.ProductNS
{
    public partial class MenuPath2Biz
    {



        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "All Menu Paths 2";
            //indexListVM.MainHeading = "Product Category 2";
            indexListVM.IsImageTiled = true;
            indexListVM.Show.EditDeleteAndCreate = true;
            //indexListVM.MenuManager.MenuState.MenuDisplayName = "Menu 2";


        }




    }
}
