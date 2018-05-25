using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Linq;
using System.Reflection;
namespace UowLibrary.MenuNS
{
    public partial class MenuBiz : BusinessLayer<ProductCategoryMain>
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
                indexListVM.Menu.ProductCategoryMain = Dal.FindFor(parameters.Id);
            }
            else
            {
                //we need to find a dummy ProductCategoryMain which will full fill the requirements of the menu level.
                ProductCategoryMain pcm = findDummyProductCategoryMain(parameters);
                if (pcm.IsNull())
                {
                    ErrorsGlobal.Add("PCM cannot be null. Programming Error", MethodBase.GetCurrentMethod());
                    throw new Exception(ErrorsGlobal.ToString());
                }
            }

            indexListVM.Heading.Column = makeName(indexListVM);


        }

        //This supplies a dummy ProductCategoryMain for the Back to List in the Create.
        private ProductCategoryMain findDummyProductCategoryMain(ControllerIndexParams parameters)
        {
            ProductCategoryMain pcm;
            switch (parameters.Menu.MenuLevel)
            {
                case MenuLevelENUM.unknown:
                    ErrorsGlobal.Add("Menu Level cannot be unknown here. Programming Error.", MethodBase.GetCurrentMethod());
                    throw new Exception(ErrorsGlobal.ToString());


                case MenuLevelENUM.Level_1:
                    //do nothing...
                    return new ProductCategoryMain();

                case MenuLevelENUM.Level_2:
                    //find one with the same productCat1
                    pcm = Dal.FindAll().FirstOrDefault(x => x.ProductCat1Id == parameters.Menu.ProductCat1Id);
                    return pcm;
                case MenuLevelENUM.Level_3:
                    //find one with the same productCat1 & productCat2
                    pcm = Dal.FindAll().FirstOrDefault(x =>
                        x.ProductCat1Id == parameters.Menu.ProductCat1Id &&
                        x.ProductCat2Id == parameters.Menu.ProductCat2Id);
                    return pcm;
                case MenuLevelENUM.Level_4:
                    //find one with the same productCat1 & productCat2 &  & productCat3
                    pcm = Dal.FindAll().FirstOrDefault(x =>
                        x.ProductCat1Id == parameters.Menu.ProductCat1Id &&
                        x.ProductCat2Id == parameters.Menu.ProductCat2Id &&
                        x.ProductCat3Id == parameters.Menu.ProductCat3Id);
                    return pcm;
                default:
                    ErrorsGlobal.Add("Menu Level cannot be unknown here. Programming Error.", MethodBase.GetCurrentMethod());
                    throw new Exception(ErrorsGlobal.ToString());
            }
        }

        private static string makeName(IndexListVM indexListVM)
        {
            string completeName = "";
            string nameProdCat1 = "";

            if (indexListVM.Menu.ProductCategoryMain.IsNull())
                return (completeName = "Menu Items");

            if (!indexListVM.Menu.ProductCategoryMain.ProductCat1.IsNull())
                nameProdCat1 = indexListVM.Menu.ProductCategoryMain.ProductCat1.Name;

            string nameProdCat2 = "";
            if (!indexListVM.Menu.ProductCategoryMain.ProductCat2.IsNull())
                nameProdCat2 = indexListVM.Menu.ProductCategoryMain.ProductCat2.Name;

            string nameProdCat3 = "";
            if (!indexListVM.Menu.ProductCategoryMain.ProductCat3.IsNull())
                nameProdCat3 = indexListVM.Menu.ProductCategoryMain.ProductCat3.Name;


            completeName = "Menu Items";


            return completeName;
        }


        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, ICommonWithId icommonWithId)
        {
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithId);
            ProductCategoryMain pcm = icommonWithId as ProductCategoryMain;
            makeNameForMenuItem(indexListVM, indexItem, pcm);

        }

        private void makeNameForMenuItem(IndexListVM indexListVM, IndexItemVM indexItem, ProductCategoryMain pcm)
        {
            //The id in parameters belongs to ProductMainControler. Extract the Id from there...
            switch (indexListVM.Menu.MenuLevelEnum)
            {
                case MenuLevelENUM.unknown:
                    break;

                case MenuLevelENUM.Level_1:
                    if (pcm.IsNull())
                    {
                        ErrorsGlobal.Add("Unable to cast ICommonWithId to ProductCategoryMain. Programming error", MethodBase.GetCurrentMethod());
                        throw new Exception(ErrorsGlobal.ToString());
                    }


                    if (pcm.ProductCat1.IsNull())
                    {
                        indexItem.Name = "";
                        return;
                    }

                    indexItem.Name = pcm.ProductCat1.Name;
                    if (pcm.ProductCat1.MiscFiles.FirstOrDefault().IsNull())
                        return;

                    indexItem.ImageAddressStr = pcm.ProductCat1.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted).RelativePathWithFileName();

                    break;

                case MenuLevelENUM.Level_2:
                    if (pcm.IsNull())
                    {
                        ErrorsGlobal.Add("Unable to cast ICommonWithId to ProductCategoryMain. Programming error", MethodBase.GetCurrentMethod());
                        throw new Exception(ErrorsGlobal.ToString());
                    }


                    if (pcm.ProductCat2.IsNull())
                    {
                        indexItem.Name = "";
                        return;
                    }

                    indexItem.Name = pcm.ProductCat2.Name;

                    if (pcm.ProductCat2.MiscFiles.FirstOrDefault().IsNull())
                        return;

                    indexItem.ImageAddressStr = pcm.ProductCat2.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted).RelativePathWithFileName();
                    break;
                case MenuLevelENUM.Level_3:
                    if (pcm.IsNull())
                    {
                        ErrorsGlobal.Add("Unable to cast ICommonWithId to ProductCategoryMain. Programming error", MethodBase.GetCurrentMethod());
                        throw new Exception(ErrorsGlobal.ToString());
                    }


                    if (pcm.ProductCat3.IsNull())
                    {
                        indexItem.Name = "";
                        return;
                    }

                    indexItem.Name = pcm.ProductCat3.Name;
                    if (pcm.ProductCat3.MiscFiles.FirstOrDefault().IsNull())
                        return;

                    indexItem.ImageAddressStr = pcm.ProductCat3.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted).RelativePathWithFileName();

                    break;
                case MenuLevelENUM.Level_4:
                    break;
                default:
                    break;
            }
        }

    }
}
