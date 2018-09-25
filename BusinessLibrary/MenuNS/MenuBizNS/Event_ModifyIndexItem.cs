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
            //string theUserId = indexListVM.UserId ?? "";
            string theUserId = UserId;

            switch (indexListVM.MenuManager.MenuState.MenuEnum)
            {

                case MenuENUM.IndexMenuPath1:
                    mpm.IsNullThrowException();
                    mpm.MenuPath1.IsNullThrowException();

                    uf = mpm.MenuPath1.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);

                    //we need to change the image address to image of MenuPath1
                    indexItem.ImageAddressStr = getImage(uf);
                    indexItem.Name = mpm.MenuPath1.FullName();
                    indexItem.Description = mpm.MenuPath1.DetailInfoToDisplayOnWebsite;

                    //get the likes and unlikes for MenuPath1
                    likeUnlikeCounter = _likeUnlikeBiz.Count(mpm.MenuPath1.Id, null, null, null, null,theUserId, false);
                    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.MenuENUM.IndexMenuPath1";
                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuPath1, BreadCrumbManager, likeUnlikeCounter, UserId);
                    break;

                case MenuENUM.IndexMenuPath2:
                    mpm.IsNullThrowException();
                    mpm.MenuPath2.IsNullThrowException();
                    indexItem.Description = mpm.MenuPath2.DetailInfoToDisplayOnWebsite;

                    uf = mpm.MenuPath2.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);
                    indexItem.ImageAddressStr = getImage(uf);
                    indexItem.Name = mpm.MenuPath2.FullName();
                    likeUnlikeCounter = _likeUnlikeBiz.Count(null, mpm.MenuPath2.Id, null, null, null, theUserId, false);
                    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.MenuENUM.IndexMenuPath2";
                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuPath2, BreadCrumbManager, likeUnlikeCounter, UserId);
                    break;

                case MenuENUM.IndexMenuPath3:
                    mpm.IsNullThrowException();
                    mpm.MenuPath3.IsNullThrowException();
                    indexItem.Description = mpm.MenuPath3.DetailInfoToDisplayOnWebsite;

                    uf = mpm.MenuPath3.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);
                    indexItem.ImageAddressStr = getImage(uf);
                    indexItem.Name = mpm.MenuPath3.FullName();
                    likeUnlikeCounter = _likeUnlikeBiz.Count(null, null, mpm.MenuPath3.Id, null, null, theUserId, false);
                    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.MenuENUM.IndexMenuPath3";
                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuPath3, BreadCrumbManager, likeUnlikeCounter, UserId);
                    break;

                case MenuENUM.IndexMenuProduct: //Products are coming
                    product.IsNullThrowException();
                    indexItem.Description = product.DetailInfoToDisplayOnWebsite;

                    uf = product.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);
                    indexItem.ImageAddressStr = getImage(uf);
                    //indexItem.Name = produc.FullName();

                    likeUnlikeCounter = _likeUnlikeBiz.Count(null, null, null, product.Id, null, theUserId, false);
                    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.MenuENUM.IndexMenuProduct";
                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuProduct, BreadCrumbManager, likeUnlikeCounter, UserId);

                    break;

                case MenuENUM.IndexMenuProductChild:
                    productChild.IsNullThrowException();
                    indexItem.Description = productChild.DetailInfoToDisplayOnWebsite;

                    uf = productChild.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);
                    indexItem.ImageAddressStr = getImage(uf);
                    //indexItem.Name = produc.FullName();
                    likeUnlikeCounter = _likeUnlikeBiz.Count(null, null, null, null, productChild.Id, theUserId, false);
                    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.MenuENUM.IndexMenuProductChild";
                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuProduct, BreadCrumbManager, likeUnlikeCounter, UserId);
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
                    likeUnlikeCounter = _likeUnlikeBiz.Count(null, null, null, null, null, theUserId, false);
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




    }
}
