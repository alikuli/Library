using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Linq;
using System.Reflection;
namespace UowLibrary.MenuNS
{
    public partial class MenuBiz
    {
        //public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        //{
        //    base.Event_ModifyIndexList(indexListVM, parameters);
        //    indexListVM.IsImageTiled = true;
        //    indexListVM.Heading.Main = "Menu";
        //    indexListVM.Show.Create = true;
        //    indexListVM.Menu.IsMenu = true;
        //    indexListVM.Menu.MenuLevelEnum = parameters.Menu.MenuLevel;


        //    if (!parameters.Id.IsNullOrWhiteSpace())
        //    {
        //        indexListVM.Menu.ProductCategoryMain = Dal.FindFor(parameters.Id);
        //    }
        //    else
        //    {
        //        //we need to find a dummy ProductCategoryMain which will full fill the requirements of the menu level.
        //        MenuPathMain pcm = findDummyProductCategoryMain(parameters);
        //        if (pcm.IsNull())
        //        {
        //            ErrorsGlobal.Add("PCM cannot be null. Programming Error", MethodBase.GetCurrentMethod());
        //            throw new Exception(ErrorsGlobal.ToString());
        //        }
        //    }

        //    indexListVM.Heading.Column = makeName(indexListVM);


        //}

        ////This supplies a dummy ProductCategoryMain for the Back to List in the Create.
        //private MenuPathMain findDummyProductCategoryMain(ControllerIndexParams parameters)
        //{
        //    MenuPathMain pcm;
        //    switch (parameters.Menu.MenuLevel)
        //    {
        //        case MenuLevelENUM.unknown:
        //            ErrorsGlobal.Add("Menu Level cannot be unknown here. Programming Error.", MethodBase.GetCurrentMethod());
        //            throw new Exception(ErrorsGlobal.ToString());


        //        case MenuLevelENUM.Level_1:
        //            //do nothing...
        //            return new MenuPathMain();

        //        case MenuLevelENUM.Level_2:
        //            //find one with the same productCat1
        //            pcm = Dal.FindAll().FirstOrDefault(x => x.MenuPath1Id == parameters.Menu.ProductCat1Id);
        //            return pcm;
        //        case MenuLevelENUM.Level_3:
        //            //find one with the same productCat1 & productCat2
        //            pcm = Dal.FindAll().FirstOrDefault(x =>
        //                x.MenuPath1Id == parameters.Menu.ProductCat1Id &&
        //                x.MenuPath2Id == parameters.Menu.ProductCat2Id);
        //            return pcm;
        //        case MenuLevelENUM.Level_4:
        //            //find one with the same productCat1 & productCat2 &  & productCat3
        //            pcm = Dal.FindAll().FirstOrDefault(x =>
        //                x.MenuPath1Id == parameters.Menu.ProductCat1Id &&
        //                x.MenuPath2Id == parameters.Menu.ProductCat2Id &&
        //                x.MenuPath3Id == parameters.Menu.ProductCat3Id);
        //            return pcm;
        //        default:
        //            ErrorsGlobal.Add("Menu Level cannot be unknown here. Programming Error.", MethodBase.GetCurrentMethod());
        //            throw new Exception(ErrorsGlobal.ToString());
        //    }
        //}

        //private static string makeName(IndexListVM indexListVM)
        //{
        //    string completeName = "";
        //    string nameProdCat1 = "";

        //    if (indexListVM.Menu.ProductCategoryMain.IsNull())
        //        return (completeName = "Menu Items");

        //    if (!indexListVM.Menu.ProductCategoryMain.MenuPath1.IsNull())
        //        nameProdCat1 = indexListVM.Menu.ProductCategoryMain.MenuPath1.Name;

        //    string nameProdCat2 = "";
        //    if (!indexListVM.Menu.ProductCategoryMain.MenuPath2.IsNull())
        //        nameProdCat2 = indexListVM.Menu.ProductCategoryMain.MenuPath2.Name;

        //    string nameProdCat3 = "";
        //    if (!indexListVM.Menu.ProductCategoryMain.MenuPath3.IsNull())
        //        nameProdCat3 = indexListVM.Menu.ProductCategoryMain.MenuPath3.Name;


        //    completeName = "Menu Items";


        //    return completeName;
        //}


        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, ICommonWithId icommonWithId)
        {
            //The icommonWithId comes here for the first 3 menus as a MenuPathMain item. 
            //Then on the 4th it comes as a product.
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithId);


            MenuPathMain pcm = icommonWithId as MenuPathMain;
            bool isMainMenuPath = !pcm.IsNull();

            if (isMainMenuPath)
            {
                makeNameForMenuItem(indexListVM, indexItem, pcm);

            }

            //IAmMenu iAmMenu = icommonWithId as IAmMenu;
            //bool isIAmMenu = !iAmMenu.IsNull();


            //if(isIAmMenu)
            //{
            //    indexItem.Menu.MenuPath1Id = iAmMenu.MenuPath1Id;
            //    indexItem.Menu.MenuPath2Id = iAmMenu.MenuPath2Id;
            //    indexItem.Menu.MenuPath3Id = iAmMenu.MenuPath3Id;
            //}


            //I dont believe envery item needs the information. So we do not need to add Menu Info to Item




        }
        //TODO move this to Menu
        private void makeNameForMenuItem(IndexListVM indexListVM, IndexItemVM indexItem, MenuPathMain pcm)
        {
            //The id in parameters belongs to ProductMainControler. Extract the Id from there...
            switch (indexListVM.MenuManager.MenuLevelEnum)
            {
                case MenuLevelENUM.unknown:
                    break;

                case MenuLevelENUM.Level_1:
                    if (pcm.IsNull())
                    {
                        ErrorsGlobal.Add("Unable to cast ICommonWithId to ProductCategoryMain. Programming error", MethodBase.GetCurrentMethod());
                        throw new Exception(ErrorsGlobal.ToString());
                    }


                    if (pcm.MenuPath1.IsNull())
                    {
                        indexItem.Name = "";
                        return;
                    }

                    if (pcm.MenuPath1.MiscFiles.IsNull())
                        return;

                    indexItem.Name = pcm.MenuPath1.Name;
                    if (pcm.MenuPath1.MiscFiles.FirstOrDefault().IsNull())
                        return;

                    indexItem.ImageAddressStr = pcm.MenuPath1.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted).RelativePathWithFileName();

                    break;

                case MenuLevelENUM.Level_2:
                    if (pcm.IsNull())
                    {
                        ErrorsGlobal.Add("Unable to cast ICommonWithId to ProductCategoryMain. Programming error", MethodBase.GetCurrentMethod());
                        throw new Exception(ErrorsGlobal.ToString());
                    }


                    if (pcm.MenuPath2.IsNull())
                    {
                        indexItem.Name = "";
                        return;
                    }

                    indexItem.Name = pcm.MenuPath2.Name;

                    if (pcm.MenuPath2.MiscFiles.FirstOrDefault().IsNull())
                        return;

                    indexItem.ImageAddressStr = pcm.MenuPath2.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted).RelativePathWithFileName();
                    break;
                case MenuLevelENUM.Level_3:
                    if (pcm.IsNull())
                    {
                        ErrorsGlobal.Add("Unable to cast ICommonWithId to ProductCategoryMain. Programming error", MethodBase.GetCurrentMethod());
                        throw new Exception(ErrorsGlobal.ToString());
                    }


                    if (pcm.MenuPath3.IsNull())
                    {
                        indexItem.Name = "";
                        return;
                    }

                    indexItem.Name = pcm.MenuPath3.Name;
                    if (pcm.MenuPath3.MiscFiles.FirstOrDefault().IsNull())
                        return;

                    indexItem.ImageAddressStr = pcm.MenuPath3.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted).RelativePathWithFileName();

                    break;
                case MenuLevelENUM.Level_4:
                    break;
                default:
                    break;
            }
        }

    }
}
