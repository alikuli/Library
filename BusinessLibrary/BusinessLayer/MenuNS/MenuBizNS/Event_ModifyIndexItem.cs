using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.ViewModels;
using System;
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
        /// I want the menu's to be dimmed if they have no products. Moreover, The pictures from products need to bubble up to the top, at least 5...
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
            int returnNoOfPictures = MenuPath1.MaxNumberOfPicturesInMenu() + 1;


            MenuPathMain mpm = icommonWithId as MenuPathMain;
            ProductChild productChild = icommonWithId as ProductChild;
            Product product = icommonWithId as Product;


            LikeUnlikeParameters likeUnlikeCounter;
            //UploadedFile uf;
            List<string> pictureAddresses = new List<string>();
            List<string> currPcs = new List<string>();

            //List<string> lstOfPictures = new List<string>();
            //string theUserId = indexListVM.UserId ?? "";
            string theUserId = UserId;



            MenuEnumForDefaultPicture = indexListVM.MenuManager.MenuState.MenuEnum;
            switch (indexListVM.MenuManager.MenuState.MenuEnum)
            {

                case MenuENUM.IndexMenuPath1:
                    mpm.IsNullThrowException();
                    mpm.MenuPath1.IsNullThrowException();

                    //get the likes and unlikes for MenuPath1
                    likeUnlikeCounter = LikeUnlikeBiz.Count(mpm.MenuPath1.Id, null, null, null, null, theUserId, false);
                    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.MenuENUM.IndexMenuPath1";

                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuPath1, BreadCrumbManager, likeUnlikeCounter, UserId, indexListVM.MenuManager.ReturnUrl, UserName);

                    indexItem.PictureViews = mpm.MenuPath1.NoOfVisits.Amount;

                    //we need to change the image address to image of MenuPath1

                    currPcs = GetCurrItemsPictureList(mpm.MenuPath1 as IHasUploads);
                    pictureAddresses = picturesForMenuPath(mpm.MenuPath1);

                    //pictureAddresses = joinCurrPicsAndPictureAddresses(pictureAddresses, currPcs);

                    if (!currPcs.IsNullOrEmpty())
                        pictureAddresses = pictureAddresses.Concat(currPcs).ToList();

                    indexItem.Name = mpm.MenuPath1.FullName();
                    indexItem.Description = mpm.MenuPath1.DetailInfoToDisplayOnWebsite;
                    indexItem.HasProductsForSale = mpm.HasLiveProductChildren;
                    indexItem.NoOfItems = mpm.NoOfItems;
                    indexItem.NoOfShops = mpm.NoOfShops;
                    break;


                case MenuENUM.IndexMenuPath2:
                    mpm.IsNullThrowException();
                    mpm.MenuPath2.IsNullThrowException();
                    indexItem.Description = mpm.MenuPath2.DetailInfoToDisplayOnWebsite;

                    //uf = mpm.MenuPath2.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);
                    //getPictureList(indexItem, mpm.MenuPath2);

                    //indexItem.ImageAddressStr = getImage(uf);
                    indexItem.Name = mpm.MenuPath2.FullName();
                    likeUnlikeCounter = LikeUnlikeBiz.Count(null, mpm.MenuPath2.Id, null, null, null, theUserId, false);
                    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.MenuENUM.IndexMenuPath2";
                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuPath2, BreadCrumbManager, likeUnlikeCounter, UserId, indexListVM.MenuManager.ReturnUrl, UserName);


                    indexItem.CompleteMenuPathViews = getMp2Count(mpm);
                    indexItem.PictureViews = mpm.MenuPath2.NoOfVisits.Amount;

                    indexItem.HasProductsForSale = mpm.HasLiveProductChildren;
                    indexItem.NoOfItems = mpm.NoOfItems;
                    indexItem.NoOfShops = mpm.NoOfShops;

                    currPcs = GetCurrItemsPictureList(mpm.MenuPath2 as IHasUploads);
                    pictureAddresses = picturesForMenuPath(mpm.MenuPath1, mpm.MenuPath2);

                    //pictureAddresses = joinCurrPicsAndPictureAddresses(pictureAddresses, currPcs);


                    break;



                case MenuENUM.IndexMenuPath3:
                    mpm.IsNullThrowException();
                    //mpm.MenuPath3.IsNullThrowException(""); //this means there are no menu 3s. This is not allowed.
                    if (mpm.MenuPath3.IsNull())
                    {
                        return;
                    }


                    indexItem.Description = mpm.MenuPath3.DetailInfoToDisplayOnWebsite;
                    //uf = mpm.MenuPath3.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);

                    //indexItem.ImageAddressStr = getImage(uf);
                    //getPictureList(indexItem, mpm.MenuPath3);

                    indexItem.Name = mpm.MenuPath3.FullName();
                    likeUnlikeCounter = LikeUnlikeBiz.Count(null, null, mpm.MenuPath3.Id, null, null, theUserId, false);

                    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.MenuENUM.IndexMenuPath3";
                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuPath3, BreadCrumbManager, likeUnlikeCounter, UserId, indexListVM.MenuManager.ReturnUrl, UserName);

                    indexItem.PictureViews = mpm.MenuPath3.NoOfVisits.Amount;
                    indexItem.CompleteMenuPathViews = mpm.NoOfVisits.Amount;

                    indexItem.HasProductsForSale = mpm.HasLiveProductChildren;

                    indexItem.NoOfItems = mpm.NoOfItems;
                    indexItem.NoOfShops = mpm.NoOfShops;


                    currPcs = GetCurrItemsPictureList(mpm.MenuPath3 as IHasUploads);
                    pictureAddresses = picturesForMenuPath(mpm);
                    //pictureAddresses = joinCurrPicsAndPictureAddresses(pictureAddresses, currPcs);


                    break;



                case MenuENUM.IndexMenuProduct: //Products are coming
                    product.IsNullThrowException();
                    indexItem.Description = product.DetailInfoToDisplayOnWebsite;

                    //uf = product.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);

                    likeUnlikeCounter = LikeUnlikeBiz.Count(null, null, null, product.Id, null, theUserId, false);
                    likeUnlikeCounter.KindOfLike = "Event_ModifyIndexItem.MenuENUM.IndexMenuProduct";
                    indexItem.MenuManager = new MenuManager(mpm, product, productChild, MenuENUM.IndexMenuProduct, BreadCrumbManager, likeUnlikeCounter, UserId, indexListVM.MenuManager.ReturnUrl, UserName);

                    currPcs = GetCurrItemsPictureList(product as IHasUploads);
                    if (product.IsShop)
                    {
                        pictureAddresses = getShopPictures(product);
                    }
                    else
                    {
                        pictureAddresses = picturesForProducts(product);
                    }


                    indexItem.MenuManager.PictureAddresses = pictureAddresses;

                    indexItem.PictureViews = product.NoOfVisits.Amount;
                    indexItem.CompleteMenuPathViews = product.NoOfVisits.Amount;

                    indexItem.HasProductsForSale = product.HasLiveProductChildren;
                    indexItem.NoOfItems = product.ProductChildren_Fixed_Not_Hidden.Count;

                    markUserAsOwnerOfShop(indexItem, product);
                    indexItem.IsShop = product.IsShop;
                    if (indexItem.IsShopAndOwnerOfShop)
                    {
                        indexItem.ShopExpiresStr = ExpiryDateInNoOfDays(product.ShopExpiryDate.Date_NotNull_Max);
                    }
                    break;


                case MenuENUM.IndexMenuProductChild:
                    productChild.IsNullThrowException();
                    indexItem.Description = productChild.DetailInfoToDisplayOnWebsite;
                    indexItem.IsTokenPaymentAccepted = productChild.IsNonRefundablePaymentAccepted;
                    indexItem.IsHidden = productChild.Hide;

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
                    currPcs = GetCurrItemsPictureList(productChild);

                    ////if none are available get them from the product
                    //if (pictureAddresses.IsNullOrEmpty())
                    //{
                    //    productChild.Product.IsNullThrowException();
                    //    pictureAddresses = GetCurrItemsPictureList(productChild.Product);

                    //}

                    indexItem.PictureViews = productChild.NoOfVisits.Amount;
                    indexItem.CompleteMenuPathViews = productChild.NoOfVisits.Amount;

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



            pictureAddresses = joinCurrPicsAndPictureAddresses(pictureAddresses, currPcs);
            
            string startSort = getStartSort(indexItem,indexListVM);

            indexItem.Input1SortString = startSort + indexItem.Input1SortString;
            indexItem.Input2SortString = startSort + indexItem.Input2SortString;
            indexItem.Input3SortString = startSort + indexItem.Input3SortString;

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

        public string ExpiryDateInNoOfDays(DateTime expiryDate)
        {
            string expiryDateInNoOfDays = new DateParameter().ToNoOfDaysAway(expiryDate);
            string str = string.Format("{0} ({1:N0})", expiryDate.ToLongDateString(), expiryDateInNoOfDays);
            return str;
        }

        /// <summary>
        /// This marks the user as an owner of the shop. Basicly if the OwnerId in product has a value, then it is a shop.
        /// if ownerId of the shop matches the ownerId of the user, then the user is the owner of the shop
        /// </summary>
        /// <param name="indexItem"></param>
        /// <param name="product"></param>
        private void markUserAsOwnerOfShop(IndexItemVM indexItem, Product product)
        {
            if (!UserId.IsNullOrWhiteSpace())
            {
                Owner owner = OwnerBiz.GetPlayerFor(UserId);
                if (owner.IsNull())
                { }
                else
                {
                    indexItem.IsShopAndOwnerOfShop = product.OwnerId == owner.Id;
                }
            }
        }

        private List<string> getShopPictures(Product product)
        {
            //get all child products
            List<string> picList = new List<string>();
            List<ProductChild> allChildren = ProductChildBiz.FindAll().Where(x => x.OwnerId == product.OwnerId).ToList();
            if (allChildren.IsNullOrEmpty())
            {
                //do nothing
            }
            else
            {
                //get all the pictures of productchildren
                foreach (ProductChild pc in allChildren)
                {
                    if (pc.MiscFiles_Fixed.IsNullOrEmpty())
                        continue;
                    foreach (UploadedFile upf in pc.MiscFiles_Fixed)
                    {
                        string addy = getImageAddressOf(upf);
                        picList.Add(addy);
                    }
                }
            }

            return picList;

        }

        private List<string> joinCurrPicsAndPictureAddresses(List<string> pictureAddresses, List<string> currPcs)
        {

            if (!currPcs.IsNullOrEmpty())
                currPcs.Remove(UploadedFile.DefaultBlankPictureLocation());

            if (!pictureAddresses.IsNullOrEmpty())
                pictureAddresses.Remove(UploadedFile.DefaultBlankPictureLocation());

            if (currPcs.IsNullOrEmpty())
            {
                if (pictureAddresses.IsNullOrEmpty())
                {

                }
                else
                {
                    pictureAddresses = pictureAddresses.Concat(currPcs).ToList();
                }
            }
            else
            {
                if (pictureAddresses.IsNullOrEmpty())
                {
                    pictureAddresses = currPcs;
                }
                else
                {
                    //remove currPcs from pictureAddress
                    //remove duplicates from CurrPic
                    int returnNoOfPictures = MenuPath1.MaxNumberOfPicturesInMenu() + 1;
                    currPcs = new HashSet<string>(currPcs).ToList();

                    if (!currPcs.IsNullOrEmpty())
                    {
                        foreach (string currPic in currPcs)
                        {
                            pictureAddresses.Remove(currPic);
                        }
                    }

                    if (pictureAddresses.IsNullOrEmpty())
                        return currPcs;

                    //now currPcs has its own pics
                    //and picture address has its own


                    if (currPcs.Count >= returnNoOfPictures)
                    {
                        return currPcs.GetRange(0, returnNoOfPictures);
                    }
                    else
                    {
                        int noOfPicsRequried = returnNoOfPictures - currPcs.Count;

                        //if there are more pics in picture address than required....
                        if (pictureAddresses.Count >= noOfPicsRequried)
                        {
                            pictureAddresses = getRandomPictures(pictureAddresses);
                            pictureAddresses = pictureAddresses.GetRange(0, noOfPicsRequried);
                        }

                        pictureAddresses = pictureAddresses.Concat(currPcs).ToList();
                        pictureAddresses = new HashSet<string>(pictureAddresses).ToList();
                    }


                }

            }

            return pictureAddresses;
        }

        //private List<string> getAndFixPictures(IHasMenuPaths mp)
        //{
        //    List<string> pictureAddresses = GetCurrItemsPictureList(mp as IHasUploads);
        //    List<string> childProductPictures = GetMenuPathChildProductPicturesFor(mp);
        //    if (!childProductPictures.IsNullOrEmpty())
        //    {
        //        string blankPicAddress = UploadedFile.DefaultBlankPictureLocation();
        //        //add to pictureaddress
        //        pictureAddresses =
        //            pictureAddresses
        //            .Concat(childProductPictures)
        //            .ToList();

        //        pictureAddresses.Remove(blankPicAddress);
        //    }
        //    return pictureAddresses;
        //}

        ///// <summary>
        ///// this adds child product pictures to menu 1... about 5
        ///// </summary>
        ///// <param name="menuPath"></param>
        ///// <returns></returns>
        ////private List<string> GetMenuPathChildProductPicturesFor(MenuPath1 mp1,MenuPath1 mp2,MenuPath1 mp3, Product product)
        //{
        //    if(product.IsNull())
        //    {
        //        return picturesFromProducts(product);
        //    }

        //    if (mp3.IsNull())
        //    {
        //        mp1.IsNullThrowException();
        //        mp2.IsNullThrowException();
        //        return picturesForMenuPath(mp1, mp2, mp3);
        //    }
        //    if (mp2.IsNull())
        //    {
        //        mp1.IsNullThrowException();
        //        return picturesForMenuPath(mp1, mp2);
        //    }

        //    if (mp1.IsNull())
        //    {
        //        return picturesForMenuPath(mp1);
        //    }

        //    return null;

        //    List<string> pictureList = new List<string>();
        //    List<string> productImages = new List<string>();
        //    int noOfImages = 0;
        //    int returnNoOfPictures = MenuPath1.MaxNumberOfPicturesInMenu();
        //    int getNoOfPicturesToSelectFrom = returnNoOfPictures * 20;
        //    string defaultFileLocation = UploadedFile.DefaultBlankPictureLocation();

        //    //get child products
        //    if (menuPath.MenuPathMains_Fixed.IsNullOrEmpty())
        //        return pictureList;

        //    foreach (MenuPathMain mmp in menuPath.MenuPathMains_Fixed)
        //    {
        //        if (mmp.Products_Fixed_And_Approved.IsNullOrEmpty())
        //            continue;
        //        foreach (var product in mmp.Products_Fixed_And_Approved)
        //        {
        //            if (noOfImages == getNoOfPicturesToSelectFrom)
        //                break;
        //            if (product.ProductChildren_Fixed.IsNullOrEmpty())
        //                continue;
        //            productImages = getPicturesForProduct(product);
        //        }
        //    }

        //    if (pictureList.IsNullOrEmpty())
        //        return pictureList;

        //    //Randomize the list
        //    List<string> pictureList_Random = pictureList.OrderBy(x => Guid.NewGuid()).ToList();

        //    if (pictureList_Random.Count > returnNoOfPictures)
        //        return pictureList_Random.GetRange(0, returnNoOfPictures);

        //    return pictureList_Random;
        //}
        private List<string> getRandomPictures(List<string> picList)
        {
            string defaultFileLocation = UploadedFile.DefaultBlankPictureLocation();

            if (picList.IsNullOrEmpty())
                return null;

            picList.Remove(defaultFileLocation);

            if (picList.IsNullOrEmpty())
                return null;

            picList = picList.OrderBy(x => Guid.NewGuid()).ToList();
            return picList;


        }
        private List<string> picturesForMenuPath(MenuPath1 mp1)
        {
            //get all menupaths with mp1 and mp2
            mp1.IsNullThrowException();

            List<MenuPathMain> mpmList = MenuPathMainBiz.FindAll().Where(x => x.MenuPath1Id == mp1.Id).ToList();

            if (mpmList.IsNullOrEmpty())
                return null;

            List<string> picList = new List<string>();

            foreach (MenuPathMain mpm in mpmList)
            {
                List<string> picListFromMpm = picturesForMenuPath(mpm);
                if (!picListFromMpm.IsNullOrEmpty())
                    picList = picList.Concat(picListFromMpm).ToList();
            }

            return picList;
        }

        private List<string> picturesForMenuPath(MenuPath1 mp1, MenuPath2 mp2)
        {
            //get all menupaths with mp1 and mp2
            mp1.IsNullThrowException();
            mp2.IsNullThrowException();

            List<MenuPathMain> mpmList = MenuPathMainBiz.FindAll().Where(x => x.MenuPath1Id == mp1.Id && x.MenuPath2Id == mp2.Id).ToList();

            if (mpmList.IsNullOrEmpty())
                return null;

            List<string> picList = new List<string>();

            foreach (MenuPathMain mpm in mpmList)
            {
                List<string> picListFromMpm = picturesForMenuPath(mpm);
                if (!picListFromMpm.IsNullOrEmpty())
                    picList = picList.Concat(picListFromMpm).ToList();
            }

            return picList;

        }

        private List<string> picturesForMenuPath(MenuPathMain mpm)
        {
            mpm.IsNullThrowExceptionArgument();

            if (mpm.Products_Fixed_And_Approved.IsNullOrEmpty())
                return null;

            List<string> picList = new List<string>();

            foreach (var product in mpm.Products_Fixed_And_Approved)
            {
                if (product.ProductChildren_Fixed.IsNullOrEmpty())
                    continue;

                List<string> picListFmProducts = picturesForProducts(product);
                if (!picListFmProducts.IsNullOrEmpty())
                    picList = picList.Concat(picListFmProducts).ToList();
            }
            //first get main path
            //get all products
            return picList;
        }

        private List<string> picturesForProducts(Product product)
        {
            //get all child products
            List<string> picList = new List<string>();

            if (product.ProductChildren_Fixed.IsNullOrEmpty())
            {
                //do nothing
            }
            else
            {
                //get all the pictures of productchildren
                foreach (ProductChild pc in product.ProductChildren_Fixed)
                {
                    if (pc.MiscFiles_Fixed.IsNullOrEmpty())
                        continue;
                    foreach (UploadedFile upf in pc.MiscFiles_Fixed)
                    {
                        string addy = getImageAddressOf(upf);
                        picList.Add(addy);
                    }
                }
            }

            return picList;
        }

        //private List<string> GetProductPicturesForWrapper(Product product)
        //{

        //    //int noOfImages = 0;
        //    int returnNoOfPictures = MenuPath1.MaxNumberOfPicturesInMenu();
        //    //int getNoOfPicturesToSelectFrom = returnNoOfPictures * 20;



        //    //get products pictures
        //    List<string> pictureAddresses = GetCurrItemsPictureList(product);

        //    List<string> pictureList = getPicturesForProduct(product);

        //    if (pictureList.IsNullOrEmpty())
        //        return pictureAddresses;

        //    //Randomize the list
        //    List<string> pictureList_Random = pictureList.OrderBy(x => Guid.NewGuid()).ToList();

        //    if (pictureList_Random.Count > returnNoOfPictures)
        //        return pictureList_Random.GetRange(0, returnNoOfPictures);

        //    return pictureList_Random;
        //}
        //private List<string> getPicturesForProduct(Product product)
        //{
        //    int noOfImages = 0;
        //    int returnNoOfPictures = MenuPath1.MaxNumberOfPicturesInMenu();
        //    int getNoOfPicturesToSelectFrom = returnNoOfPictures * 20;
        //    string defaultFileLocation = UploadedFile.DefaultBlankPictureLocation();

        //    List<string> pictureList = new List<string>();
        //    foreach (ProductChild pchild in product.ProductChildren_Fixed)
        //    {
        //        if (noOfImages == getNoOfPicturesToSelectFrom)
        //            break;

        //        if (pchild.MiscFiles_Fixed.IsNullOrEmpty())
        //            continue;

        //        //getImageAddressOf()
        //        //get one from each product until we have 5
        //        foreach (UploadedFile upf in pchild.MiscFiles_Fixed)
        //        {
        //            if (noOfImages == getNoOfPicturesToSelectFrom)
        //                break;

        //            if (!upf.IsImage())
        //                continue;

        //            string imageLocation = getImageAddressOf(upf);

        //            if (imageLocation.IsNullOrWhiteSpace())
        //                continue;

        //            if (imageLocation.ToLower() == defaultFileLocation.ToLower())
        //                continue;

        //            pictureList.Add(imageLocation);
        //            noOfImages++;
        //            break;
        //        }

        //    }
        //    return pictureList;

        //}



        /// <summary>
        /// this counts the views of Mp2
        /// </summary>
        /// <param name="mpm"></param>
        /// <returns></returns>
        private long getMp2Count(MenuPathMain mpm)
        {
            if (mpm.IsNull())
                return 0;

            long count = MenuPathMainBiz
                .FindAll()
                .Where(x => x.MenuPath1Id == mpm.MenuPath1Id && x.MenuPath2Id == mpm.MenuPath2Id)
                .Sum(y => y.NoOfVisits.Amount);

            return count;
        }

        MenuENUM MenuEnumForDefaultPicture { get; set; }



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



        }

        //private static string fixSorting(IndexItemVM indexItem, IndexListVM indexListVM)
        //{
        //    string startSort = getStartSort(indexItem, indexListVM);


        //    string sortString = startSort + indexItem.Input1SortString;
        //    return sortString;
        //}

        private static string getStartSort(IndexItemVM indexItem, IndexListVM indexList)
        {
            string startSort = "";



            switch (indexList.SortOrderEnum)
            {
                case SortOrderENUM.Item1_Asc:
                case SortOrderENUM.Item2_Asc:
                case SortOrderENUM.Item3_Asc:
                    startSort = ascendingSort(indexItem, startSort);
                    break;
                case SortOrderENUM.Item1_Dsc:
                case SortOrderENUM.Item2_Dsc:
                case SortOrderENUM.Item3_Dsc:
                    startSort = decendingSort(indexItem, startSort);
                    break;
                default:
                    startSort = ascendingSort(indexItem, startSort);
                    break;
            }


            return startSort;
        }

        private static string ascendingSort(IndexItemVM indexItem, string startSort)
        {
            if (indexItem.IsShop)
            {
                startSort = "0";


            }
            else
            {
                if (indexItem.NoOfItems > 0 || indexItem.NoOfShops > 0)
                {
                    startSort = "1";
                }
                else
                {
                    startSort = "2";
                }
            }
            return startSort;
        }
        private static string decendingSort(IndexItemVM indexItem, string startSort)
        {
            if (indexItem.IsShop)
            {
                startSort = "Z";


            }
            else
            {
                if (indexItem.NoOfItems > 0 || indexItem.NoOfShops > 0)
                {
                    startSort = "Y";
                }
                else
                {
                    startSort = "X";
                }
            }
            return startSort;
        }


    }
}
