using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System.Linq;
using UowLibrary.MenuNS.MenuStateNS;

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
            MenuPath1 mp1 = icommonWithId as MenuPath1;
            mp1.IsNullThrowException("Unable to unbox");
            //send in a MenuPathMain that is a part of this MenuPath1
            MenuPathMain mpm = mp1.MenuPathMains.FirstOrDefault();
            //mpm.IsNullThrowException("mpm");

            indexItem.MenuManager = new MenuManager(mpm, null, null, MenuENUM.EditMenuPath1, BreadCrumbManager, null, UserId, indexListVM.MenuManager.ReturnUrl, UserName);


            indexItem.MenuManager.PictureAddresses = GetCurrItemsPictureList(mp1);



        }


    }
}
