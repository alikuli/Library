using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS;
using System.Collections.Generic;
using System.Linq;
using UowLibrary.FeatureNS.MenuFeatureNS;

namespace UowLibrary.ProductNS
{
    public partial class ProductBiz
    {



        /// <summary>
        /// This is the main entry point.
        /// </summary>
        /// <param name="iproduct"></param>

        public void FixProductFeatures(IProduct iproduct)
        {
            List<ProductFeature> allcurrentProductFeautres = getAllCurrentProductFeaturesFor(iproduct);
            List<MenuFeature> allfeautresAsPerMenuPath = getAllMenuFeaturesAsPerProductsMenuPath(iproduct);
            List<ProductFeature> productFeaturesToBeRemoved = getProductFeaturesThatNeedToBeRemoved(allcurrentProductFeautres, allfeautresAsPerMenuPath);
            List<ProductFeature> productFeaturesToBeAdded = getProductFeaturesWhichAreInMenuPathButNotInProduct(allcurrentProductFeautres, allfeautresAsPerMenuPath);

            removeProductFeaturesFrom(iproduct, productFeaturesToBeRemoved);
            addProductFeaturesTo(iproduct, productFeaturesToBeAdded);
        }



        /// <summary>
        /// This adds the missing product features
        /// </summary>
        /// <param name="iproduct"></param>
        /// <param name="productFeaturesToBeAdded"></param>
        private void addProductFeaturesTo(IProduct iproduct, List<ProductFeature> productFeaturesToBeAdded)
        {

            if (productFeaturesToBeAdded.IsNullOrEmpty())
                return;

            Product product = iproduct as Product;
            product.IsNullThrowException("could not unbox product");


            //incase ProductFeatures is null.
            if (product.ProductFeatures.IsNull())
                product.ProductFeatures = new List<ProductFeature>();


            foreach (ProductFeature pf in productFeaturesToBeAdded)
            {
                //locate feature in product... if it is not a part of it.. add it
                ProductFeature pfFound = product.ProductFeatures.FirstOrDefault(x => x.Name == pf.Name && x.MetaData.IsDeleted == false);

                if (pfFound.IsNull())
                    product.ProductFeatures.Add(pf);

            }

        }


        private void removeProductFeaturesFrom(IProduct iproduct, List<ProductFeature> productFeaturesToBeRemoved)
        {
            if (productFeaturesToBeRemoved.IsNullOrEmpty())
                return;

            Product product = iproduct as Product;
            product.IsNullThrowException("Unable to box product");

            if (product.ProductFeatures.IsNull())
                return;


            foreach (ProductFeature pf in productFeaturesToBeRemoved)
            {
                product.ProductFeatures.Remove(pf);

            }

        }


        private List<MenuFeature> getAllMenuFeaturesAsPerProductsMenuPath(IProduct iproduct)
        {
            Product product = iproduct as Product;
            product.IsNullThrowException("Product is null");

            if (product.MenuPathMains.IsNullOrEmpty())
                return null;

            List<MenuPathMain> menuPathList = product.MenuPathMains.Where(x => x.MetaData.IsDeleted == false).ToList();
            menuPathList.IsNullOrEmptyThrowException("menuPathList is empty");

            HashSet<MenuFeature> menuFeaturesAsPerMenuPaths = new HashSet<MenuFeature>();
            foreach (MenuPathMain mp in menuPathList)
            {
                HashSet<MenuFeature> currCollection = getAllCurrentFeaturesFor(mp);

                if (currCollection.IsNullOrEmpty())
                    continue;

                foreach (MenuFeature mf in currCollection)
                {
                    try
                    {
                        menuFeaturesAsPerMenuPaths.Add(mf);
                    }
                    catch
                    {

                    }
                }

            }
            return menuFeaturesAsPerMenuPaths.ToList();
        }

        private List<ProductFeature> getAllCurrentProductFeaturesFor(IProduct iproduct)
        {
            iproduct.IsNullThrowException("Product is null");
            if (iproduct.ProductFeatures.IsNullOrEmpty())
                return null;

            List<ProductFeature> lst = iproduct.ProductFeatures.Where(x => x.MetaData.IsDeleted == false).ToList();
            return lst;
        }

        /// <summary>
        /// This gets a list of all menu features that need removal.
        /// </summary>
        /// <param name="allcurrentProductFeautres"></param>
        /// <param name="allfeautresAsPerMenuPath"></param>
        /// <returns></returns>
        private List<ProductFeature> getProductFeaturesThatNeedToBeRemoved(List<ProductFeature> allcurrentProductFeautres, List<MenuFeature> allfeautresAsPerMenuPath)
        {
            if (allcurrentProductFeautres.IsNullOrEmpty())
                return null;

            if (allfeautresAsPerMenuPath.IsNullOrEmpty())
            {
                //remove all the features from the product
                return allcurrentProductFeautres;

            }

            List<ProductFeature> productFeaturesNotInMenuPath = new List<ProductFeature>();
            foreach (ProductFeature pf in allcurrentProductFeautres)
            {
                MenuFeature mfFound = allfeautresAsPerMenuPath.FirstOrDefault(x => x.Name == pf.Name);
                if (mfFound.IsNull())
                {
                    //this needs removal
                    productFeaturesNotInMenuPath.Add(pf);
                }
            }

            return productFeaturesNotInMenuPath;
        }


        private List<ProductFeature> getProductFeaturesWhichAreInMenuPathButNotInProduct(
            List<ProductFeature> allcurrentProductFeautres,
            List<MenuFeature> allfeautresAsPerMenuPath)
        {
            //if (allcurrentProductFeautres.IsNullOrEmpty())
            //    return null;

            if (allfeautresAsPerMenuPath.IsNullOrEmpty())
                return null;

            List<ProductFeature> prodFeaturesInMenuButNotInProduct = new List<ProductFeature>();






            foreach (MenuFeature mf in allfeautresAsPerMenuPath)
            {
                ProductFeature pf = null;

                //check to see if mf is part of the current features
                if (!allcurrentProductFeautres.IsNull())
                {
                    //locate if mf is part of the ProductFeatures
                    pf = allcurrentProductFeautres.FirstOrDefault(x => x.Name == mf.Name);
                }


                if (pf.IsNull())
                {
                    pf = CreateProductFeatureFrom(mf);
                    prodFeaturesInMenuButNotInProduct.Add(pf);
                }
            }

            return prodFeaturesInMenuButNotInProduct;

        }


        private void RemoveFeatures(Product product, List<ProductFeature> productFeatureList)
        {
            //remove those features that are now not existant in any of the menu paths.

            if (productFeatureList.IsNullOrEmpty())
                return;

            if (product.ProductFeatures.IsNullOrEmpty())
                return;

            List<string> nameOfMenuFeaturesToRemoveLst = new List<string>();

            //first remove any feature that is not a part of menuFeaturesHashSet.
            //this means that some meap path has been removed, and then the feature must go.
            foreach (ProductFeature pf in productFeatureList)
            {

                ProductFeature pfFound = ProductFeatureBiz.FindAll().FirstOrDefault(x => x.Name == pf.Name);
                if (pfFound.IsNull())
                {
                    //found, this menu feature no longer exists.
                    nameOfMenuFeaturesToRemoveLst.Add(pf.Name);
                }
            }

            //now remove them from the product
            if (!nameOfMenuFeaturesToRemoveLst.IsNullOrEmpty())
            {
                foreach (string name in nameOfMenuFeaturesToRemoveLst)
                {
                    ProductFeature pf = product.ProductFeatures.FirstOrDefault(x => x.Name == name);

                    if (pf.IsNull())
                        continue;

                    pf.MetaData.IsDeleted = true;
                    pf.MetaData.Deleted.SetToTodaysDate(UserId);
                    pf.Comment = "Item was deleted as it was no longer part of a menu path. BusinessLibrary.fixProductFeatures.RemoveFeatures";

                    ProductFeatureBiz.Update(pf);
                }
            }
        }


        private HashSet<MenuFeature> getAllCurrentFeaturesFor(MenuPathMain mpm)
        {

            mpm.MenuPath1.IsNullThrowException("Menu Path 1 is null. Programming error.");
            List<MenuFeature> menuFeatures1 = mpm.MenuPath1.MenuFeatures.ToList();
            List<MenuFeature> menuFeatures2 = mpm.MenuPath2.MenuFeatures.ToList();
            List<MenuFeature> menuFeatures3 = mpm.MenuPath3.MenuFeatures.ToList();

            HashSet<MenuFeature> menuFeaturesHashSet = new HashSet<MenuFeature>();

            if (!menuFeatures1.IsNullOrEmpty())
            {
                foreach (MenuFeature mf in menuFeatures1)
                {
                    menuFeaturesHashSet.Add(mf);
                }
            }

            if (!menuFeatures2.IsNullOrEmpty())
            {
                foreach (MenuFeature mf in menuFeatures2)
                {
                    menuFeaturesHashSet.Add(mf);
                }
            }

            if (!menuFeatures3.IsNullOrEmpty())
            {
                foreach (MenuFeature mf in menuFeatures3)
                {
                    menuFeaturesHashSet.Add(mf);
                }
            }
            return menuFeaturesHashSet;
        }


    }
}
