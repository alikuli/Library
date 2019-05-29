using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
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


            LikeUnlikeParameters likeUnlikeCounter;
            UploadedFile uf;
            List<string> pictureAddresses = new List<string>();
            //List<string> lstOfPictures = new List<string>();
            //string theUserId = indexListVM.UserId ?? "";
            string theUserId = UserId;

            MenuEnumForDefaultPicture = indexListVM.MenuManager.MenuState.MenuEnum;
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

                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuPath1, BreadCrumbManager, likeUnlikeCounter, UserId, indexListVM.MenuManager.ReturnUrl, UserName);

                    uf = mpm.MenuPath1.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);

                    //we need to change the image address to image of MenuPath1
                    //indexItem.ImageAddressStr = getImage(uf);
                    //indexItem.MenuManager.PictureAddresses = GetPictureList(mpm.MenuPath1);

                    pictureAddresses = GetPictureList(mpm.MenuPath1);

                    //if none are available, put a blank picture 
                    //if (pictureAddresses.IsNullOrEmpty())
                    //{

                    //    pictureAddresses = GetDefaultPicture();

                    //}


                    //indexItem.MenuManager.PictureAddresses = pictureAddresses;

                    //if (indexItem.MenuManager.PictureAddresses.IsNullOrEmpty())
                    //{
                    //    indexItem.MenuManager.PictureAddresses = GetDefaultPicture();
                    //}

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
                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuPath2, BreadCrumbManager, likeUnlikeCounter, UserId, indexListVM.MenuManager.ReturnUrl, UserName);

                    //indexItem.MenuManager.PictureAddresses = GetPictureList(mpm.MenuPath2);
                    //if (indexItem.MenuManager.PictureAddresses.IsNullOrEmpty())
                    //{
                    //    indexItem.MenuManager.PictureAddresses = GetDefaultPicture();
                    //}
                    pictureAddresses = GetPictureList(mpm.MenuPath2);

                    //if none are available, put a blank picture 
                    //if (pictureAddresses.IsNullOrEmpty())
                    //{

                    //    pictureAddresses = GetDefaultPicture();

                    //}


                    //indexItem.MenuManager.PictureAddresses = pictureAddresses;

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
                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuPath3, BreadCrumbManager, likeUnlikeCounter, UserId, indexListVM.MenuManager.ReturnUrl, UserName);
                    //indexItem.MenuManager.PictureAddresses = GetPictureList(mpm.MenuPath3);
                    //if (indexItem.MenuManager.PictureAddresses.IsNullOrEmpty())
                    //{
                    //    indexItem.MenuManager.PictureAddresses = GetDefaultPicture();
                    //}
                    pictureAddresses = GetPictureList(mpm.MenuPath3);

                    //if none are available, put a blank picture 
                    //if (pictureAddresses.IsNullOrEmpty())
                    //{

                    //    pictureAddresses = GetDefaultPicture();

                    //}


                    //indexItem.MenuManager.PictureAddresses = pictureAddresses;

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
                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuProduct, BreadCrumbManager, likeUnlikeCounter, UserId, indexListVM.MenuManager.ReturnUrl, UserName);
                    //get the pictures list from the product
                    pictureAddresses = GetPictureList(product);

                    //if none are available, put a blank picture 
                    if (pictureAddresses.IsNullOrEmpty())
                    {

                        pictureAddresses = GetDefaultPicture();

                    }


                    indexItem.MenuManager.PictureAddresses = pictureAddresses;
                    //if (indexItem.MenuManager.PictureAddresses.IsNullOrEmpty())
                    //{
                    //    indexItem.MenuManager.PictureAddresses = GetDefaultPicture();
                    //}

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
                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuProductChild, BreadCrumbManager, likeUnlikeCounter, UserId, indexListVM.MenuManager.ReturnUrl, UserName);

                    Person person = UserBiz.GetPersonFor(UserId);
                    if (!person.IsNull())
                    {
                        string userPersonId = person.Id;
                        string productChildPersonId = productChild.Owner.PersonId;
                        indexItem.MenuManager.IndexMenuVariables.updateRequiredProperties(userPersonId, productChildPersonId);
                    }

                    //get the pictures list from the productChild
                    pictureAddresses = GetPictureList(productChild);

                    //if none are available get them from the product
                    if (pictureAddresses.IsNullOrEmpty())
                    {
                        productChild.Product.IsNullThrowException();
                        pictureAddresses = GetPictureList(productChild.Product);

                    }

                    //if none are available, put a blank picture 
                    //if (pictureAddresses.IsNullOrEmpty())
                    //{

                    //    pictureAddresses = GetDefaultPicture();

                    //}


                    //indexItem.MenuManager.PictureAddresses = pictureAddresses;
                    indexItem.Price = productChild.Sell.SellPrice;
                    break;


                //case MenuENUM.IndexMenuProductChildLandingPage:
                //    productChild.IsNullThrowException();
                //    indexItem.Description = productChild.DetailInfoToDisplayOnWebsite;

                //    uf = productChild.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);

                //    //product child has no image?
                //    if (uf.IsNull())
                //        uf = productChild.Product.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);

                //    likeUnlikeCounter = LikeUnlikeBiz.Count(null, null, null, null, productChild.Id, theUserId, false);
                //    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.MenuENUM.IndexMenuProductChild";
                //    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuProductChild, BreadCrumbManager, likeUnlikeCounter, UserId);

                //    getPictureList(indexItem, productChild);
                //    indexItem.Price = productChild.Sell.SellPrice;

                //    break;

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

            if (pictureAddresses.IsNullOrEmpty())
            {

                pictureAddresses = GetDefaultPicture();

            }


            indexItem.MenuManager.PictureAddresses = pictureAddresses;


            indexItem.MenuManager.LikeUnlikesCounter = likeUnlikeCounter;
            indexItem.MenuManager.BreadCrumbManager = indexListVM.MenuManager.BreadCrumbManager;

            if (!UserId.IsNullOrWhiteSpace())
            {
                Person person = UserBiz.GetPersonFor(UserId);
                person.IsNullThrowException("person");
                indexItem.MenuManager.UserPersonId = person.Id;
            }
        }

        MenuENUM MenuEnumForDefaultPicture { get; set; }

        //ProductChild _productChildForDefaultPicture;
        //ProductChild ProductChildForDefaultPicture { get { return _productChildForDefaultPicture; } }




        //public override List<string> GetPictureList(IHasUploads ihasUploads)
        //{
        //    List<string> addresses = new List<string>();

        //    if (ihasUploads.MiscFiles.Any(x => !x.MetaData.IsDeleted))
        //    {
        //        var lstUploadedFiles = ihasUploads.MiscFiles.Where(x => !x.MetaData.IsDeleted).ToList();
        //        lstUploadedFiles.IsNullOrEmptyThrowException("Something went worng. This list cannot be empty.");
        //        foreach (UploadedFile uploadFile in lstUploadedFiles)
        //        {
        //            string pictureAddy = getImageAddressOf(uploadFile);
        //            if (!pictureAddy.IsNullOrWhiteSpace())
        //            {
        //                addresses.Add(pictureAddy);

        //            }
        //        }
        //    }

        //    //if (addresses.IsNullOrEmpty())
        //    //{
        //    //    //ProductChild pc = ihasUploads as ProductChild;
        //    //    return GetDefaultPicture(ihasUploads);
        //    //}

        //    return addresses;
        //}




        /// <summary>
        /// This returns the pictures of the ProductChild OR all the pictures of the Product.
        /// </summary>
        /// <param name="iHasUploads"></param>
        /// <returns></returns>
        public List<string> GetDefaultPicture(IHasUploads iHasUploads)
        {
            iHasUploads.IsNullThrowException("productChildForDefaultPicture");
            List<string> lst = new List<string>();

            foreach (UploadedFile uploadedFile in iHasUploads.MiscFiles)
            {
                string pictureAddy = getImageAddressOf(uploadedFile);
                if (pictureAddy.IsNullOrWhiteSpace())
                    continue;
                lst.Add(pictureAddy);
            }

            return lst;

            //if (iHasUploads.MiscFiles.IsNullOrEmpty())
            //{
            //    lst = base.GetDefaultPicture();

            //}
            //else
            //{
            //    foreach (UploadedFile uploadedFile in iHasUploads.MiscFiles)
            //    {
            //        string pictureAddy = getImageAddressOf(uploadedFile);
            //        if (pictureAddy.IsNullOrWhiteSpace())
            //            continue;
            //        lst.Add(pictureAddy);
            //    }
            //}


        }




    }
}
