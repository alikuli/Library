namespace UowLibrary.MenuNS
{
    public partial class MenuBiz
    {




        //public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, ICommonWithId icommonWithId)
        //{
        //    base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithId);
        //    MenuPathMain pcm = icommonWithId as MenuPathMain;
        //    makeNameForMenuItem(indexListVM, indexItem, pcm);

        //}

        //private void makeNameForMenuItem(IndexListVM indexListVM, IndexItemVM indexItem, MenuPathMain pcm)
        //{
        //    //The id in parameters belongs to ProductMainControler. Extract the Id from there...
        //    switch (indexListVM.Menu.MenuLevelEnum)
        //    {
        //        case MenuLevelENUM.unknown:
        //            break;

        //        case MenuLevelENUM.Level_1:
        //            if (pcm.IsNull())
        //            {
        //                ErrorsGlobal.Add("Unable to cast ICommonWithId to ProductCategoryMain. Programming error", MethodBase.GetCurrentMethod());
        //                throw new Exception(ErrorsGlobal.ToString());
        //            }


        //            if (pcm.MenuPath1.IsNull())
        //            {
        //                indexItem.Name = "";
        //                return;
        //            }

        //            if (pcm.MenuPath1.MiscFiles.IsNull())
        //                return;

        //            indexItem.Name = pcm.MenuPath1.Name;
        //            if (pcm.MenuPath1.MiscFiles.FirstOrDefault().IsNull())
        //                return;

        //            indexItem.ImageAddressStr = pcm.MenuPath1.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted).RelativePathWithFileName();

        //            break;

        //        case MenuLevelENUM.Level_2:
        //            if (pcm.IsNull())
        //            {
        //                ErrorsGlobal.Add("Unable to cast ICommonWithId to ProductCategoryMain. Programming error", MethodBase.GetCurrentMethod());
        //                throw new Exception(ErrorsGlobal.ToString());
        //            }


        //            if (pcm.MenuPath2.IsNull())
        //            {
        //                indexItem.Name = "";
        //                return;
        //            }

        //            indexItem.Name = pcm.MenuPath2.Name;

        //            if (pcm.MenuPath2.MiscFiles.FirstOrDefault().IsNull())
        //                return;

        //            indexItem.ImageAddressStr = pcm.MenuPath2.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted).RelativePathWithFileName();
        //            break;
        //        case MenuLevelENUM.Level_3:
        //            if (pcm.IsNull())
        //            {
        //                ErrorsGlobal.Add("Unable to cast ICommonWithId to ProductCategoryMain. Programming error", MethodBase.GetCurrentMethod());
        //                throw new Exception(ErrorsGlobal.ToString());
        //            }


        //            if (pcm.MenuPath3.IsNull())
        //            {
        //                indexItem.Name = "";
        //                return;
        //            }

        //            indexItem.Name = pcm.MenuPath3.Name;
        //            if (pcm.MenuPath3.MiscFiles.FirstOrDefault().IsNull())
        //                return;

        //            indexItem.ImageAddressStr = pcm.MenuPath3.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted).RelativePathWithFileName();

        //            break;
        //        case MenuLevelENUM.Level_4:
        //            break;
        //        default:
        //            break;
        //    }
        //}

    }
}
