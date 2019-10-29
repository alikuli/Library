using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using UowLibrary.MenuNS.MenuStateNS;
using System.Linq;

namespace UowLibrary.MenuNS

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



        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, InterfacesLibrary.SharedNS.ICommonWithId icommonWithId)
        {
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithId);
            MenuPath2 mp2 = icommonWithId as MenuPath2;
            mp2.IsNullThrowException("Unable to unbox");
            //send in a MenuPathMain that is a part of this MenuPath2
            MenuPathMain mpm = mp2.MenuPathMains.FirstOrDefault();
            indexItem.MenuManager = new MenuManager(mpm, null, null, MenuENUM.EditMenuPath2, BreadCrumbManager, null, UserId, indexListVM.MenuManager.ReturnUrl, UserName);


            indexItem.MenuManager.PictureAddresses = GetCurrItemsPictureList(mp2);

        }
    }
}
