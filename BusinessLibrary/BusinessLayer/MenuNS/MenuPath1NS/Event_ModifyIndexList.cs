using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
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
        }


        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, ICommonWithId icommonWithId)
        {
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithId);
            indexItem.MenuManager.IsNullThrowException();
            MenuPath1 menuPath1 = icommonWithId as MenuPath1;
            menuPath1.IsNullThrowException("cannot unbox Menupath1");

            getPictureList(indexItem, menuPath1);


        }


    }
}
