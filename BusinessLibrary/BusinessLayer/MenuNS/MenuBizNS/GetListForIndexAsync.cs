using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UowLibrary.MenuNS
{
    /// <summary>
    /// This is where all the data is created for the menu depending on the menu level.
    /// </summary>
    public partial class MenuBiz
    {
        public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parms)
        {


            List<ICommonWithId> lst = new List<ICommonWithId>();

            switch (parms.MenuEnum)
            {
                case MenuENUM.IndexMenuPath1:
                    lst = await IndexMenuPath1_DataListAsync();
                    break;
                case MenuENUM.IndexMenuPath2:

                    lst = await indexMenuPath2_DataListAsync(parms);
                    break;
                case MenuENUM.IndexMenuPath3:
                    lst = await indexMenuPath3_DataListAsync(parms);
                    break;
                case MenuENUM.IndexMenuProduct:
                    lst = await indexProduct_DataListAsync(parms);
                    break;
                case MenuENUM.IndexMenuProductChild:
                    lst = await indexProductChild_DataListAsync(parms);
                    break;
                //case MenuENUM.IndexMenuProductChildLandingPage:
                //    lst = await indexProductChildLandingPage_DataListAsync(parms);
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
                    break;
            }



            return lst;
        }

        #region Level 1

        /// <summary>
        /// This returns a unique list of ProductCategory1 found in ProductCategoryMain
        /// </summary>
        /// <param name="cat1Id"></param>
        /// <returns></returns>
        private List<string> UniqueMenuMainWithMenu1(List<MenuPathMain> lstOfMpm)
        {

            var mp1LstIds = lstOfMpm
                .Select(x => x.MenuPath1Id)
                .Distinct()
                .ToList();

            return mp1LstIds;

        }

        /// <summary>
        /// This returns a unique list of ProductCategory1Ids
        /// </summary>
        /// <returns></returns>
        private async Task<List<ICommonWithId>> IndexMenuPath1_DataListAsync()
        {



            //find all live product children
            //get their products
            //get all shops that are live
            //get all menupaths of shops and products
            //get all menupaths where MenuPath1Id == id


            List<MenuPathMain> mpms_Distinct_With_Live_Products_And_Shops = get_All_Live_Products_And_Shops();
            List<string> mp1List_Live = UniqueLiveMpm1List(mpms_Distinct_With_Live_Products_And_Shops);
            //=====================================


            //we need to return MenuPathMain List because the Index needs it.
            List<ICommonWithId> pclst = new List<ICommonWithId>();

            List<MenuPathMain> allMenuMain = await FindAllAsync();

            if (allMenuMain.IsNullOrEmpty())
                return null;

            List<string> listOfMenuPath1Ids = UniqueMenuMainWithMenu1(allMenuMain);

            if (listOfMenuPath1Ids.IsNullOrEmpty())
                return null;

            foreach (var id in listOfMenuPath1Ids)
            {
                List<MenuPathMain> mpmList = allMenuMain.Where(x => x.MenuPath1Id == id).ToList();
                MenuPathMain mpm = mpmList.FirstOrDefault(x => x.MenuPath1Id == id);

                if (mpm.MenuPath1Id.IsNullOrWhiteSpace())
                    continue;

                if (mp1List_Live.Contains(mpm.MenuPath1Id))
                    mpm.HasLiveProductChildren = true;

                if (mpm.HasLiveProductChildren)
                {
                    if (!mpmList.IsNullOrEmpty())
                    {
                        foreach (var mpm_counter in mpmList)
                        {
                            mpm.NoOfItems += mpm_counter.ProductChildren_Fixed_Not_Hidden.Count;
                            mpm.NoOfShops += mpm_counter.Product_Shops_Not_Expired.Count;
                        }
                    }
                }
                pclst.Add(mpm);
            }

            return pclst;
        }

        private List<MenuPathMain> get_All_Live_Products_And_Shops()
        {
            List<Product> productList = get_Live_Products();
            if (productList.IsNullOrEmpty())
                return null;

            List<Product> shopList_Live = get_Live_Shops();
            List<Product> concatedList_Distinct = concatTheLists(productList, shopList_Live);

            if (concatedList_Distinct.IsNullOrEmpty())
                return null;

            List<MenuPathMain> mpms_Distinct_With_Live_Products = getUniqueMenuPaths(concatedList_Distinct);
            return mpms_Distinct_With_Live_Products;
        }

        private List<string> UniqueLiveMpm1List(List<MenuPathMain> mpms_Distinct_With_Live_Products)
        {
            if (mpms_Distinct_With_Live_Products.IsNullOrEmpty())
                return null;

            List<string> allMp1 = new List<string>();
            foreach (MenuPathMain mpm in mpms_Distinct_With_Live_Products)
            {
                if (mpm.MenuPath1Id.IsNullOrWhiteSpace())
                    continue;
                allMp1.Add(mpm.MenuPath1Id);
            }

            if (allMp1.IsNullOrEmpty())
                return null;

            List<string> allMp1Distinct = new HashSet<string>(allMp1).ToList();
            return allMp1Distinct;


        }

        private List<MenuPathMain> getUniqueMenuPaths(List<Product> concatedList_Distinct)
        {
            List<MenuPathMain> mpms = new List<MenuPathMain>();

            foreach (Product prod in concatedList_Distinct)
            {
                if (prod.MenuPathMains.IsNullOrEmpty())
                {
                    //                    throw new Exception("Product has no paths = " + prod.FullName());
                    //system product has no path
                    continue;

                }
                foreach (MenuPathMain mpm in prod.MenuPathMains)
                {
                    mpms.Add(mpm);
                }
            }

            //if (mpms.IsNullOrEmpty())
            //    return null;

            List<MenuPathMain> mpms_distinct = new HashSet<MenuPathMain>(mpms).ToList();
            return mpms_distinct;
        }

        private List<Product> get_Live_Products()
        {
            List<ProductChild> liveChldren = ProductChildBiz.FindAll()
                .Where(x => x.Hide == false)
                .ToList();
            if (liveChldren.IsNullOrEmpty())
                return null;

            List<Product> productList = new List<Product>();
            foreach (ProductChild pc in liveChldren)
            {
                if (pc.Product.IsNull())
                    throw new Exception("Product child has no parent");
                productList.Add(pc.Product);
            }
            return productList;
        }

        private List<Product> get_Live_Shops()
        {
            //get all live shops
            //if product has an owner it is a shop.
            List<Product> shopList_All = ProductBiz.FindAll()
                .Where(x => x.OwnerId != null || x.OwnerId.Trim() != "")
                .ToList();

            List<Product> shopList_Live = new List<Product>();

            foreach (Product shop in shopList_All)
            {
                if (shop.IsShopExpired)
                    continue;
                shopList_Live.Add(shop);
            }
            return shopList_Live;
        }

        private static List<Product> concatTheLists(List<Product> productList, List<Product> shopList_Live)
        {
            //concat the two lists...
            List<Product> concatedList = new List<Product>();
            if (productList.IsNullOrEmpty())
            {
                if (shopList_Live.IsNullOrEmpty())
                {
                    //do nothing
                }
                else
                {
                    concatedList = shopList_Live;
                }
            }
            else
            {
                if (shopList_Live.IsNullOrEmpty())
                {
                    concatedList = productList;
                }
                else
                {
                    concatedList = productList.Concat(shopList_Live).ToList();
                }
            }

            if (concatedList.IsNullOrEmpty())
                return null;

            List<Product> concatedList_Distinct = new HashSet<Product>(concatedList).ToList();

            return concatedList_Distinct;
        }

        #endregion


        #region Level 2

        /// <summary>
        /// This produces a unique list of MenuPathMain where MenuPath1Id is == id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<string> UniqueListOfMenuPath2_IDs(List<MenuPathMain> allMpm)
        {
            var mp2LstIds = allMpm
                .Select(x => x.MenuPath2Id)
                .Distinct()
                .ToList();

            return mp2LstIds;

        }
        private async Task<List<ICommonWithId>> indexMenuPath2_DataListAsync(ControllerIndexParams parms)
        {

            parms.MenuPathMainId.IsNullOrWhiteSpaceThrowException();
            MenuPathMain mpm = await FindAsync(parms.MenuPathMainId);
            mpm.IsNullThrowException("Main path not found.");

            //===========================================
            List<MenuPathMain> mpms_Distinct_With_Live_Products_And_Shops = get_All_Live_Products_And_Shops();
            //===========================================

            var allMenuPathMain = await FindAllAsync();
            var allMenuPathMainWithMenuPath1 = allMenuPathMain.Where(x => x.MenuPath1Id == mpm.MenuPath1Id).ToList();


            //update the count of Menu1Path
            MenuPath1 mp1 = MenuPathMainBiz.MenuPath1Biz.FindAll().FirstOrDefault(x => x.Id == mpm.MenuPath1Id);
            if (!mp1.IsNull())
            {
                if (!UserId.IsNullOrWhiteSpace())
                {

                    mp1.NoOfVisits.AddOne(UserId, UserName);
                    await MenuPathMainBiz.MenuPath1Biz.UpdateAndSaveAsync(mp1);
                }
            }

            List<string> listOfMenuPath2Ids = UniqueListOfMenuPath2_IDs(allMenuPathMainWithMenuPath1);

            if (listOfMenuPath2Ids.IsNullOrEmpty())
                return null;

            List<ICommonWithId> mpmlst = new List<ICommonWithId>();

            foreach (var mp2Id in listOfMenuPath2Ids)
            {
                MenuPathMain mpmInner = allMenuPathMainWithMenuPath1
                    .Where(x => x.MenuPath2Id == mp2Id)
                    .FirstOrDefault();

                if (!mpmInner.IsNull())
                {
                    if (!mpms_Distinct_With_Live_Products_And_Shops.IsNullOrEmpty())
                    {
                        MenuPathMain mpm_liveOne = mpms_Distinct_With_Live_Products_And_Shops
                            .FirstOrDefault(x => x.MenuPath1Id == mpmInner.MenuPath1Id && x.MenuPath2Id == mpmInner.MenuPath2Id);
                        if (!mpm_liveOne.IsNull())
                        {
                            mpmInner.HasLiveProductChildren = true;
                            mpmInner.NoOfItems = mpm_liveOne.ProductChildren_Fixed_Not_Hidden.Count;
                            mpmInner.NoOfShops = mpm_liveOne.Product_Shops_Not_Expired.Count;
                        }
                    }

                    mpmlst.Add(mpmInner);

                }
            }

            if (mpmlst.IsNullOrEmpty())
                return null;

            return mpmlst;
        }

        #endregion


        #region Level 3
        private List<MenuPathMain> UniqueListOfMainPath_IDs(string productCategory1Id, string productCategory2Id)
        {
            productCategory1Id.IsNullOrWhiteSpaceThrowException();
            productCategory2Id.IsNullOrWhiteSpaceThrowException();


            List<MenuPathMain> mainPathIdsLst = FindAll()
                .Where(x => x.MenuPath1Id == productCategory1Id && x.MenuPath2Id == productCategory2Id)
                .Distinct()
                .ToList();

            return mainPathIdsLst;

        }
        private async Task<List<ICommonWithId>> indexMenuPath3_DataListAsync(ControllerIndexParams parms)
        {

            parms.MenuPathMainId.IsNullOrWhiteSpaceThrowException("Main Menu Path Id not received.");

            MenuPathMain mpm = await FindAsync(parms.MenuPathMainId);
            mpm.IsNullThrowException("MenuPathMain not found.");

            List<MenuPathMain> uniqueListOfMainPaths = UniqueListOfMainPath_IDs(mpm.MenuPath1Id, mpm.MenuPath2Id);

            //===========================================
            List<MenuPathMain> mpms_Distinct_With_Live_Products_And_Shops = get_All_Live_Products_And_Shops();
            //===========================================

            //update the count of Menu2Path
            MenuPath2 mp2 = MenuPathMainBiz.MenuPath2Biz.FindAll().FirstOrDefault(x => x.Id == mpm.MenuPath2Id);
            if (!mp2.IsNull())
            {
                if (!UserId.IsNullOrWhiteSpace())
                {
                    //these can be wrng if they also describe other paths
                    //this is how many times this particular picture has been clicked
                    mp2.NoOfVisits.AddOne(UserId, UserName);
                    await MenuPathMainBiz.MenuPath2Biz.UpdateAndSaveAsync(mp2);
                }
            }


            if (uniqueListOfMainPaths.IsNullOrEmpty())
                return null;


            foreach (MenuPathMain mpm2 in uniqueListOfMainPaths)
            {
                //check to see if these items have any sale items
                if (mpms_Distinct_With_Live_Products_And_Shops.Contains(mpm2))
                {
                    mpm2.HasLiveProductChildren = true;
                    mpm2.NoOfItems = mpm2.ProductChildren_Fixed_Not_Hidden.Count;
                    mpm2.NoOfShops = mpm2.Product_Shops_Not_Expired.Count;

                }
            }
            List<ICommonWithId> mpmlst = uniqueListOfMainPaths.Cast<ICommonWithId>().ToList();


            return mpmlst;
        }


        #endregion


        #region Level 4
        private async Task<List<ICommonWithId>> indexProduct_DataListAsync(ControllerIndexParams parms)
        {


            //get the menupathMain
            MenuPathMain mpm = await FindAsync(parms.MenuPathMainId);
            mpm.IsNullThrowException("Menu Path does note exist. Something is wrong.");

            //Get all the products listed by it
            List<Product> listOfProducts = mpm.Products_Fixed_And_Approved;

            //update the count of Menu3Path
            MenuPath3 mp3 = MenuPathMainBiz.MenuPath3Biz.FindAll().FirstOrDefault(x => x.Id == mpm.MenuPath3Id);

            if (!mp3.IsNull())
            {
                if (!UserId.IsNullOrWhiteSpace())
                {

                    mpm.NoOfVisits.AddOne(UserId, UserName);
                    MenuPathMainBiz.Update(mpm);

                    //these can be wrng if they also describe other paths
                    //this is how many times this particular picture has been clicked

                    mp3.NoOfVisits.AddOne(UserId, UserName);
                    await MenuPathMainBiz.MenuPath3Biz.UpdateAndSaveAsync(mp3);
                }
            }



            if (listOfProducts.IsNullOrEmpty())
                return null;


            List<ICommonWithId> pclst = listOfProducts.Cast<ICommonWithId>().ToList();

            return pclst;
        }

        #endregion


        #region Level 5

        /// <summary>
        /// This will return Product Children.
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        private async Task<List<ICommonWithId>> indexProductChild_DataListAsync(ControllerIndexParams parms)
        {
            //Get the product

            parms.ProductId.IsNullOrWhiteSpaceThrowException();
            Product product = await ProductBiz.FindAsync(parms.ProductId);
            //parent product cannot be null. It is some kind of programming error if it is.
            product.IsNullThrowException(string.Format("Product not found. Id ='{0}'", parms.ProductId));


            //update the number of visits
            if (!UserId.IsNullOrWhiteSpace())
            {

                product.NoOfVisits.AddOne(UserId, UserName);
                await ProductBiz.UpdateAndSaveAsync(product);
            }
            //get all its child products and display them

            List<ProductChild> children;
            //see if the product is a shop
            if (product.OwnerId.IsNullOrWhiteSpace())
            {
                //this is not a shop.
                children = productChildren(product);

            }
            else
            {
                //this is a shop
                children = shopChildren(product);
            }

            if (children.IsNullOrEmpty())
                return null;

            List<ICommonWithId> childrenAsIcommonLst = children.Cast<ICommonWithId>().ToList();

            return childrenAsIcommonLst;
        }

        private List<ProductChild> productChildren(Product product)
        {
            if (product.ProductChildren.IsNullOrEmpty())
                return null;

            List<ProductChild> children = product.ProductChildren.Where(x => x.MetaData.IsDeleted == false && x.Hide == false).ToList();

            return children;
        }


        private List<ProductChild> shopChildren(Product product)
        {
            if (product.IsShopExpired)
                return null;

            List<ProductChild> children = ProductChildBiz.FindAll().Where(x => x.OwnerId == product.OwnerId && x.Hide == false).ToList();


            return children;
        }





        #endregion





    }
}
