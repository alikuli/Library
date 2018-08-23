using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Linq;
using UowLibrary.MenuNS.MenuStateNS;
namespace UowLibrary.MenuNS
{
    public partial class MenuBiz
    {



        /// <summary>
        /// The Menu will always run the MenuBiz.
        /// We do not care about the MenuManager in the ICommonWithId level. That is used in Create/Edit
        /// We need to load the MenuManager with the correct MainMenuPath so that it can be used in the _IndexMiddlePart - TiledPictures.cshtml
        /// We cant use the one in the list because that is a single one and the one that is used in the itme needs to have the 
        /// MenuPath1 id. Moreover, the Id of the IndexItem is MenuPathMainId, so it is of no use.
        /// </summary>
        /// <param name="indexListVM"></param>
        /// <param name="indexItem"></param>
        /// <param name="icommonWithId"></param>
        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, ICommonWithId icommonWithId)
        {
            //The icommonWithId is the item that is running in the Forloop in the calling procedure
            //The icommonWithId comes here for the first 3 menus as a MenuPathMain item. 
            //Then on the 4th it comes as a product.
            //You must select the correct MenuState Here as well
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithId);

            MenuPathMain mpm = icommonWithId as MenuPathMain;
            ProductChild productChild = icommonWithId as ProductChild;
            Product product = icommonWithId as Product;


            LikeUnlikeParameter likeUnlikeCounter;
            UploadedFile uf;
            string theUserId = indexListVM.UserId ?? "";
            switch (indexListVM.MenuManager.MenuState.MenuEnum)
            {

                case MenuENUM.IndexMenuPath1:
                    mpm.IsNullThrowException();
                    mpm.MenuPath1.IsNullThrowException();

                    uf = mpm.MenuPath1.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);

                    //we need to change the image address to image of MenuPath1
                    indexItem.ImageAddressStr = getImage(uf);
                    indexItem.Name = mpm.MenuPath1.FullName();

                    //get the likes and unlikes for MenuPath1
                    likeUnlikeCounter = _likeUnlikeBiz.Count(mpm.MenuPath1.Id, null, null, null, null,theUserId);
                    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.MenuENUM.IndexMenuPath1";
                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuPath1, _breadCrumbManager, likeUnlikeCounter);
                    break;

                case MenuENUM.IndexMenuPath2:
                    mpm.IsNullThrowException();
                    mpm.MenuPath2.IsNullThrowException();

                    uf = mpm.MenuPath2.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);
                    indexItem.ImageAddressStr = getImage(uf);
                    indexItem.Name = mpm.MenuPath2.FullName();
                    likeUnlikeCounter = _likeUnlikeBiz.Count(null, mpm.MenuPath2.Id, null, null, null, theUserId);
                    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.MenuENUM.IndexMenuPath2";
                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuPath2, _breadCrumbManager, likeUnlikeCounter);
                    break;

                case MenuENUM.IndexMenuPath3:
                    mpm.IsNullThrowException();
                    mpm.MenuPath3.IsNullThrowException();

                    uf = mpm.MenuPath3.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);
                    indexItem.ImageAddressStr = getImage(uf);
                    indexItem.Name = mpm.MenuPath3.FullName();
                    likeUnlikeCounter = _likeUnlikeBiz.Count(null, null, mpm.MenuPath3.Id, null, null, theUserId);
                    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.MenuENUM.IndexMenuPath3";
                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuPath3, _breadCrumbManager, likeUnlikeCounter);
                    break;

                case MenuENUM.IndexMenuProduct: //Products are coming
                    product.IsNullThrowException();
                    uf = product.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);
                    indexItem.ImageAddressStr = getImage(uf);
                    //indexItem.Name = produc.FullName();
                    
                    likeUnlikeCounter = _likeUnlikeBiz.Count(null, null, null, product.Id, null, theUserId);
                    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.MenuENUM.IndexMenuProduct";
                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuProduct, _breadCrumbManager, likeUnlikeCounter);

                    break;

                case MenuENUM.IndexMenuProductChild:
                    productChild.IsNullThrowException();
                    uf = productChild.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);
                    indexItem.ImageAddressStr = getImage(uf);
                    //indexItem.Name = produc.FullName();
                    likeUnlikeCounter = _likeUnlikeBiz.Count(null, null, null, null, productChild.Id, theUserId);
                    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.MenuENUM.IndexMenuProductChild";
                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuProduct, _breadCrumbManager, likeUnlikeCounter);
                    break;

                case MenuENUM.EditMenuPath1:
                case MenuENUM.EditMenuPath2:
                case MenuENUM.EditMenuPath3:
                case MenuENUM.EditMenuPathMain:
                case MenuENUM.EditMenuProduct:
                case MenuENUM.EditMenuProductChild:
                case MenuENUM.CreateMenuPath1:
                case MenuENUM.CreateMenuPath2:
                case MenuENUM.CreateMenuPath3:
                case MenuENUM.CreateMenuPathMenuPathMain:
                case MenuENUM.CreateMenuProduct:
                case MenuENUM.CreateMenuProductChild:
                default:
                    likeUnlikeCounter = _likeUnlikeBiz.Count(null, null, null, null, null, theUserId);
                    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.Default";

                    break;
            }

            indexItem.MenuManager.LikeUnlikesCounter = likeUnlikeCounter;
            indexItem.MenuManager.BreadCrumbManager = indexListVM.MenuManager.BreadCrumbManager;

        }

        private string getImage(UploadedFile _uf)
        {
            if (_uf.IsNull())
                _uf = new UploadedFile();

            return _uf.RelativePathWithFileName();
        }


        //TODO move this to Menu
        private void makeNameForMenuItem(IndexListVM indexListVM, IndexItemVM indexItem, MenuPathMain pcm)
        {
            //The id in parameters belongs to ProductMainControler. Extract the Id from there...
            //switch (indexListVM.MenuManager.MenuLevelEnum)
            //{
            //    case MenuLevelENUM.unknown:
            //        break;

            //    case MenuLevelENUM.Level_1:
            //        if (pcm.IsNull())
            //        {
            //            ErrorsGlobal.Add("Unable to cast ICommonWithId to ProductCategoryMain. Programming error", MethodBase.GetCurrentMethod());
            //            throw new Exception(ErrorsGlobal.ToString());
            //        }


            //        if (pcm.MenuPath1.IsNull())
            //        {
            //            indexItem.Name = "";
            //            return;
            //        }

            //        if (pcm.MenuPath1.MiscFiles.IsNull())
            //            return;

            //        indexItem.Name = pcm.MenuPath1.Name;
            //        if (pcm.MenuPath1.MiscFiles.FirstOrDefault().IsNull())
            //            return;

            //        indexItem.ImageAddressStr = pcm.MenuPath1.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted).RelativePathWithFileName();

            //        break;

            //    case MenuLevelENUM.Level_2:
            //        if (pcm.IsNull())
            //        {
            //            ErrorsGlobal.Add("Unable to cast ICommonWithId to ProductCategoryMain. Programming error", MethodBase.GetCurrentMethod());
            //            throw new Exception(ErrorsGlobal.ToString());
            //        }


            //        if (pcm.MenuPath2.IsNull())
            //        {
            //            indexItem.Name = "";
            //            return;
            //        }

            //        indexItem.Name = pcm.MenuPath2.Name;

            //        if (pcm.MenuPath2.MiscFiles.FirstOrDefault().IsNull())
            //            return;

            //        indexItem.ImageAddressStr = pcm.MenuPath2.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted).RelativePathWithFileName();
            //        break;
            //    case MenuLevelENUM.Level_3:
            //        if (pcm.IsNull())
            //        {
            //            ErrorsGlobal.Add("Unable to cast ICommonWithId to ProductCategoryMain. Programming error", MethodBase.GetCurrentMethod());
            //            throw new Exception(ErrorsGlobal.ToString());
            //        }


            //        if (pcm.MenuPath3.IsNull())
            //        {
            //            indexItem.Name = "";
            //            return;
            //        }

            //        indexItem.Name = pcm.MenuPath3.Name;
            //        if (pcm.MenuPath3.MiscFiles.FirstOrDefault().IsNull())
            //            return;

            //        indexItem.ImageAddressStr = pcm.MenuPath3.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted).RelativePathWithFileName();

            //        break;
            //    case MenuLevelENUM.Level_4:
            //        break;
            //    default:
            //        break;
            //}
            throw new NotImplementedException();
        }

    }
}
