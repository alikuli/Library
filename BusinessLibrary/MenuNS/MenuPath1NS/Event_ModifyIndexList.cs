using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.MenuNS
{
    public partial class MenuPath1Biz
    {



        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "All Menu Path 1";
            indexListVM.IsImageTiled = true;
            indexListVM.Show.EditDeleteAndCreate = false;

            //indexListVM.MainHeading = "Product Category 1";


            //int webClicksCount = _pageViewBiz.GetClickCount();
            //string recordStr = (webClicksCount == 1 ? "view" : "views");
            //indexListVM.MenuManager.WebClicksCount = string.Format("{0:n0} {1}",
            //    webClicksCount,
            //    recordStr);


            //we need to make  indexListVM.Show.EditDeleteAndCreate false because we want to control
            //the view models that are created to make the product and that will be based on the
            //industry that the product belongs to, i.e. MenuPath1
            //indexListVM.MenuManager.MenuState.MenuDisplayName = "Menu 1";


        }



    }
}
