using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Linq;
using System.Reflection;
namespace UowLibrary.MenuNS
{
    public partial class MenuBiz
    {
        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);
            indexListVM.IsImageTiled = true;
            indexListVM.Heading.Main = "Menu";
            indexListVM.Show.Create = true;
            indexListVM.Menu.IsMenu = true;
            indexListVM.Menu.MenuLevelEnum = parameters.Menu.MenuLevel;


            if (!parameters.Id.IsNullOrWhiteSpace())
            {
                //in menu level 1-4 the parameters.Id that is returned is a MainMenuPath
                //in menu level 5, the parameters.Id that is returned is a Product.Id

                switch (parameters.Menu.MenuLevel)
                {
                    case MenuLevelENUM.Level_1: //Menu Level 1
                    case MenuLevelENUM.Level_2: //Menu Level 2
                    case MenuLevelENUM.Level_3: //Menu Level 3
                    case MenuLevelENUM.Level_4: //Product Level
                        indexListVM.Menu.MenuPathMain = Find(parameters.Id);
                        break;
                    case MenuLevelENUM.Level_5: //Product Children Level
                        indexListVM.Menu.MenuPathMain = FindAll().FirstOrDefault(x =>
                            x.MenuPath1Id == parameters.Menu.MenuPath1Id &&
                            x.MenuPath2Id == parameters.Menu.MenuPath2Id &&
                            x.MenuPath3Id == parameters.Menu.MenuPath3Id 
                            );
                            indexListVM.Menu.Product = _productBiz.Find(parameters.Id);

                        break;

                    case MenuLevelENUM.unknown:
                        break;
                    default:
                        break;
                }


            }
            else
            {
                //we need to find a dummy ProductCategoryMain which will full fill the requirements of the menu level.
                MenuPathMain pcm = findDummyMenuPathMain(parameters);
                if (pcm.IsNull())
                {
                    ErrorsGlobal.Add("PCM cannot be null. Programming Error", MethodBase.GetCurrentMethod());
                    throw new Exception(ErrorsGlobal.ToString());
                }
            }

            indexListVM.Heading.Column = makeName(indexListVM);


        }

        //This supplies a dummy ProductCategoryMain for the Back to List in the Create.
        private MenuPathMain findDummyMenuPathMain(ControllerIndexParams parameters)
        {
            MenuPathMain pcm;
            switch (parameters.Menu.MenuLevel)
            {
                case MenuLevelENUM.unknown:
                    ErrorsGlobal.Add("Menu Level cannot be unknown here. Programming Error.", MethodBase.GetCurrentMethod());
                    throw new Exception(ErrorsGlobal.ToString());


                case MenuLevelENUM.Level_1:
                    //do nothing...
                    return new MenuPathMain();

                case MenuLevelENUM.Level_2:
                    //find MenuPathMain with the same productCat1
                    pcm = FindAll().FirstOrDefault(x => x.MenuPath1Id == parameters.Menu.MenuPath1Id);
                    return pcm;
                case MenuLevelENUM.Level_3:
                    //find MenuPathMain with the same productCat1 & productCat2
                    pcm = FindAll().FirstOrDefault(x =>
                        x.MenuPath1Id == parameters.Menu.MenuPath1Id &&
                        x.MenuPath2Id == parameters.Menu.MenuPath2Id);
                    return pcm;
                case MenuLevelENUM.Level_4:
                    //find MenuPathMain with the same productCat1 & productCat2 &  & productCat3
                    pcm = FindAll().FirstOrDefault(x =>
                        x.MenuPath1Id == parameters.Menu.MenuPath1Id &&
                        x.MenuPath2Id == parameters.Menu.MenuPath2Id &&
                        x.MenuPath3Id == parameters.Menu.MenuPath3Id);
                    return pcm;

                case MenuLevelENUM.Level_5:
                    throw new NotImplementedException();

                default:
                    ErrorsGlobal.Add("Menu Level cannot be unknown here. Programming Error.", MethodBase.GetCurrentMethod());
                    throw new Exception(ErrorsGlobal.ToString());
            }
        }

        private static string makeName(IndexListVM indexListVM)
        {
            string completeName = "";
            string nameProdCat1 = "";

            if (indexListVM.Menu.MenuPathMain.IsNull())
                return (completeName = "Menu Items");

            if (!indexListVM.Menu.MenuPathMain.MenuPath1.IsNull())
                nameProdCat1 = indexListVM.Menu.MenuPathMain.MenuPath1.Name;

            string nameProdCat2 = "";
            if (!indexListVM.Menu.MenuPathMain.MenuPath2.IsNull())
                nameProdCat2 = indexListVM.Menu.MenuPathMain.MenuPath2.Name;

            string nameProdCat3 = "";
            if (!indexListVM.Menu.MenuPathMain.MenuPath3.IsNull())
                nameProdCat3 = indexListVM.Menu.MenuPathMain.MenuPath3.Name;


            completeName = "Menu Items";


            return completeName;
        }


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
