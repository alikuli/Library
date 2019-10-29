using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using UowLibrary.MenuNS.MenuStateNS;
using System.Linq;
using AliKuli.Extentions;

namespace UowLibrary.MenuNS
{
    public partial class MenuPath3Biz
    {



        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "All Menu Path 3";
            indexListVM.IsImageTiled = true;
            indexListVM.Show.EditDeleteAndCreate = true;
            //indexListVM.MenuManager.MenuState.MenuDisplayName = "Menu 3";

        }


        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, InterfacesLibrary.SharedNS.ICommonWithId icommonWithId)
        {
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithId);
            MenuPath3 mp3 = icommonWithId as MenuPath3;
            mp3.IsNullThrowException("Unable to unbox");
            //send in a MenuPathMain that is a part of this MenuPath3
            MenuPathMain mpm = mp3.MenuPathMains.FirstOrDefault();
            indexItem.MenuManager = new MenuManager(mpm, null, null, MenuENUM.EditMenuPath3, BreadCrumbManager, null, UserId, indexListVM.MenuManager.ReturnUrl, UserName);


            indexItem.MenuManager.PictureAddresses = GetCurrItemsPictureList(mp3);

        }


    }
}
