using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.ViewModels;
using System.Collections.Generic;
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
            //List<string> lstOfPictures = new List<string>();
            //string theUserId = indexListVM.UserId ?? "";
            string theUserId = UserId;
            
            menuEnumForDefaultPicture = indexListVM.MenuManager.MenuState.MenuEnum;
            switch (indexListVM.MenuManager.MenuState.MenuEnum)
            {

                case MenuENUM.IndexMenuPath1:
                    mpm.IsNullThrowException();
                    mpm.MenuPath1.IsNullThrowException();

                    //uf = mpm.MenuPath1.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);
                    ////we need to change the image address to image of MenuPath1
                    //indexItem.ImageAddressStr = getImage(uf);
                    //getPictureList(indexItem, mpm.MenuPath1);


                    //indexItem.Name = mpm.MenuPath1.FullName();
                    //indexItem.Description = mpm.MenuPath1.DetailInfoToDisplayOnWebsite;

                    //get the likes and unlikes for MenuPath1
                    likeUnlikeCounter = LikeUnlikeBiz.Count(mpm.MenuPath1.Id, null, null, null, null, theUserId, false);
                    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.MenuENUM.IndexMenuPath1";

                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuPath1, BreadCrumbManager, likeUnlikeCounter, UserId);

                    uf = mpm.MenuPath1.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);

                    //we need to change the image address to image of MenuPath1
                    //indexItem.ImageAddressStr = getImage(uf);
                    getPictureList(indexItem, mpm.MenuPath1);


                    indexItem.Name = mpm.MenuPath1.FullName();
                    indexItem.Description = mpm.MenuPath1.DetailInfoToDisplayOnWebsite;
                    break;


                case MenuENUM.IndexMenuPath2:
                    mpm.IsNullThrowException();
                    mpm.MenuPath2.IsNullThrowException();
                    indexItem.Description = mpm.MenuPath2.DetailInfoToDisplayOnWebsite;

                    uf = mpm.MenuPath2.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);
                    //getPictureList(indexItem, mpm.MenuPath2);

                    //indexItem.ImageAddressStr = getImage(uf);
                    indexItem.Name = mpm.MenuPath2.FullName();
                    likeUnlikeCounter = LikeUnlikeBiz.Count(null, mpm.MenuPath2.Id, null, null, null, theUserId, false);
                    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.MenuENUM.IndexMenuPath2";
                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuPath2, BreadCrumbManager, likeUnlikeCounter, UserId);

                    getPictureList(indexItem, mpm.MenuPath2);

                    break;



                case MenuENUM.IndexMenuPath3:
                    mpm.IsNullThrowException();
                    //mpm.MenuPath3.IsNullThrowException(""); //this means there are no menu 3s. This is not allowed.
                    if (mpm.MenuPath3.IsNull())
                    {
                        return;
                    }


                    indexItem.Description = mpm.MenuPath3.DetailInfoToDisplayOnWebsite;
                    uf = mpm.MenuPath3.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);

                    //indexItem.ImageAddressStr = getImage(uf);
                    //getPictureList(indexItem, mpm.MenuPath3);

                    indexItem.Name = mpm.MenuPath3.FullName();
                    likeUnlikeCounter = LikeUnlikeBiz.Count(null, null, mpm.MenuPath3.Id, null, null, theUserId, false);

                    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.MenuENUM.IndexMenuPath3";
                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuPath3, BreadCrumbManager, likeUnlikeCounter, UserId);
                    getPictureList(indexItem, mpm.MenuPath3);
                    break;



                case MenuENUM.IndexMenuProduct: //Products are coming
                    product.IsNullThrowException();
                    indexItem.Description = product.DetailInfoToDisplayOnWebsite;

                    uf = product.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);
                    //indexItem.ImageAddressStr = getImage(uf);
                    //getPictureList(indexItem, product);
                    //indexItem.Name = produc.FullName();

                    likeUnlikeCounter = LikeUnlikeBiz.Count(null, null, null, product.Id, null, theUserId, false);
                    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.MenuENUM.IndexMenuProduct";
                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuProduct, BreadCrumbManager, likeUnlikeCounter, UserId);
                    getPictureList(indexItem, product);
                    break;


                case MenuENUM.IndexMenuProductChild:
                    productChild.IsNullThrowException();
                    indexItem.Description = productChild.DetailInfoToDisplayOnWebsite;

                    uf = productChild.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);

                    //product child has no image?
                    if (uf.IsNull())
                        uf = productChild.Product.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);

                    //indexItem.ImageAddressStr = getImage(uf);
                    //getPictureList(indexItem, productChild);

                    //indexItem.Name = produc.FullName();
                    likeUnlikeCounter = LikeUnlikeBiz.Count(null, null, null, null, productChild.Id, theUserId, false);
                    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.MenuENUM.IndexMenuProductChild";
                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuProductChild, BreadCrumbManager, likeUnlikeCounter, UserId);

                    getPictureList(indexItem, productChild);
                    indexItem.Price = productChild.Sell.SellPrice;

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
                    likeUnlikeCounter = LikeUnlikeBiz.Count(null, null, null, null, null, theUserId, false);
                    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.Default";

                    break;
            }

            indexItem.MenuManager.LikeUnlikesCounter = likeUnlikeCounter;
            indexItem.MenuManager.BreadCrumbManager = indexListVM.MenuManager.BreadCrumbManager;

        }

        MenuENUM menuEnumForDefaultPicture { get; set; }

        public override void GetDefaultPicture(IndexItemVM indexItem)
        {
            switch (menuEnumForDefaultPicture)
            {
                case MenuENUM.IndexMenuProductChild:
                    //get the products picture.
                    indexItem.MenuManager.IsNullThrowException("Menu Manager");
                    indexItem.MenuManager.ProductChild.IsNullThrowException("Product Child");
                    indexItem.MenuManager.ProductChild.Product.IsNullThrowException("Product");
                    Product product = indexItem.MenuManager.ProductChild.Product;;

                    if(product.MiscFiles.IsNullOrEmpty())
                    {
                        base.GetDefaultPicture(indexItem);

                    }
                    else
                    {
                        foreach (UploadedFile uploadedFile in product.MiscFiles)
                        {
                            string pictureAddy = getImageAddressOf(uploadedFile);
                            if(pictureAddy.IsNullOrWhiteSpace())
                                continue;
                            indexItem.MenuManager.PictureAddresses.Add(pictureAddy);
                        }   
                    }
                    break;

                default:
                    base.GetDefaultPicture(indexItem);
                    break;
            }
        }



    }
}
