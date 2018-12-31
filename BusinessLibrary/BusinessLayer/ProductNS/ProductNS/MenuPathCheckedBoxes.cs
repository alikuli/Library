using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.CheckBoxItemNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.SharedNS;
using System.Collections.Generic;
using System.Linq;

namespace UowLibrary.ProductNS
{
    public partial class ProductBiz
    {

        //this returns a list of checked boxes marked true
        //public List<CheckBoxItem> LoadMenuPathCheckedBoxes(IProduct product, MenuPath1ENUM menuPath1Enum = MenuPath1ENUM.Unknown)
        //Note, the MenuPath1 of the Product should have the correct Menupath or a MenuPath1 has to be passed.
        public void LoadMenuPathCheckedBoxes(ControllerIndexParams parm)
        {
            Product product = parm.Entity as Product;
            product.IsNullThrowException("Product was not boxed/unboxed. ProductBiz.LoadMenuPathCheckedBoxes");


            addMenuPathMainSentFromView(parm, product);


            List<CheckBoxItem> checkedboxes = CreateCheckBoxes(product);
            markProductSelectedCheckBoxesTrue(product, checkedboxes);

            //new code to create CheckBoxList
            //this creates the check box list which are in the view.
            //helps to make the chk boxes collapsable
            product.Mp1List = createCheckBoxListFrom(checkedboxes);
            product.CheckedBoxesList = checkedboxes;
        }

        /// <summary>
        /// When the Product is created from the productMenu, then its; menuPathMainId is sent along with it
        /// This will be its first MenuPathMain. User can add more as required.
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="product"></param>
        private void addMenuPathMainSentFromView(ControllerIndexParams parm, Product product)
        {
            if (!parm.MenuPathMainId.IsNullOrWhiteSpace())
            {
                if (product.MenuPathMains.IsNull())
                    product.MenuPathMains = new List<MenuPathMain>();
                MenuPathMain mpm = MenuPathMainBiz.Find(parm.MenuPathMainId);
                mpm.IsNullThrowException("Menu Path main not found");
                product.MenuPathMains.Add(mpm);
            }
        }

        private List<CheckBoxItem> CreateCheckBoxes(Product product)
        {
            List<CheckBoxItem> checkedboxes = new List<CheckBoxItem>();

            if (product.MenuPathMains.IsNull())
                product.MenuPathMains = new List<MenuPathMain>();

            MenuPathMain mpm = product.MenuPathMains.FirstOrDefault();

            if (mpm.IsNull())
                mpm = new MenuPathMain();

            string mp1Id = mpm.MenuPath1Id;

            MenuPath1 mp1;
            if (!mp1Id.IsNullOrWhiteSpace())
                mp1 = MenuPath1Biz.Find(mp1Id);
            else
                mp1 = new MenuPath1();


            List<MenuPathMain> allMenuPaths = MenuPathMainBiz.FindAll().ToList();

            //Now create all the check boxes
            checkedboxes = createAllCheckBoxes(allMenuPaths);
            return checkedboxes;
        }
        private void GetDataFromMenuPathCheckBoxes(IProduct iproduct)
        {
            iproduct.IsNullThrowExceptionArgument("product is null");


            if (iproduct.CheckedBoxesList.IsNullOrEmpty())
                return;//Nothing to do.


            removeUnselectedMenuPaths(iproduct);
            //Now we will add all new paths to the product

            addSelectedMenuPathMain(iproduct);


        }
        private List<CheckBoxListTree> createCheckBoxListFrom(List<CheckBoxItem> checkedboxes)
        {
            List<CheckBoxListTree> chkboxList = new List<CheckBoxListTree>();
            if (!checkedboxes.IsNullOrEmpty())
            {
                //we expect that the list to be sorted.
                foreach (CheckBoxItem chkBoxItem in checkedboxes)
                {
                    CheckBoxListTree mp1InTree;
                    chkBoxItem.Label = chkBoxItem.Mp3Name;

                    //check here if the main list contains MP1
                    if (checkBoxListTreeContainsMp1Name(chkBoxItem.Mp1Name, chkboxList, out mp1InTree))
                    {
                        //Mp1 exist....we have a value in mp2ChkBoxLstTree
                        mp1InTree.IsNullThrowException("Programming error. this should never be null.");

                        //does the MP2 exist in this MP1?
                        CheckBoxListTree mp2List;
                        if (checkBoxListTreeContainsMp1Name(chkBoxItem.Mp2Name, mp1InTree.Mp2List, out mp2List))
                        {
                            //Mp2List has been found.
                            //since the original list contains unique values and this is a new item,
                            //we can assume MP3 is not a part of this list... add it.
                            mp2List.CheckedBoxesList.Add(chkBoxItem);
                        }
                        else
                        {
                            //Mp2List has not been found.
                            //create a new Mp2List
                            mp2List = new CheckBoxListTree();
                            mp2List.Name = chkBoxItem.Mp2Name;

                            //now since this is a new list... add the MP3 to cblt_MP2
                            mp2List.CheckedBoxesList.Add(chkBoxItem);
                            mp1InTree.Mp2List.Add(mp2List);
                        }
                        if (chkBoxItem.IsTrue)
                        {
                            mp1InTree.IsActve = true;
                            mp2List.IsActve = true;
                        }
                        continue;

                    }

                    CheckBoxListTree cblt_MP1 = new CheckBoxListTree();
                    cblt_MP1.Name = chkBoxItem.Mp1Name;

                    //create the Mp2 as well, since it is the first
                    CheckBoxListTree cblt_MP2 = new CheckBoxListTree();
                    cblt_MP2.Name = chkBoxItem.Mp2Name;

                    //now since this is a new list... add the MP3 to cblt_MP2
                    cblt_MP2.CheckedBoxesList.Add(chkBoxItem);

                    if (chkBoxItem.IsTrue)
                    {
                        cblt_MP1.IsActve = true;
                        cblt_MP2.IsActve = true;
                    }

                    cblt_MP1.Mp2List.Add(cblt_MP2);
                    chkboxList.Add(cblt_MP1);
                }
            }

            return chkboxList;

        }


        bool checkBoxListTreeContainsMp1Name(string name, List<CheckBoxListTree> checkBoxListTree, out CheckBoxListTree chkboxTree)
        {
            chkboxTree = null;
            if (!checkBoxListTree.IsNullOrEmpty())
            {

                foreach (CheckBoxListTree item in checkBoxListTree)
                {
                    //this is checking for the MP name
                    if (item.Name == name)
                    {
                        chkboxTree = item;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// This marks the check boxes that have been selected in the product true.
        /// </summary>
        /// <param name="iproduct"></param>
        /// <param name="checkedboxes"></param>
        private static void markProductSelectedCheckBoxesTrue(IProduct iproduct, List<CheckBoxItem> checkedboxes)
        {
            //Here we are going to get the marked menupaths
            if (!iproduct.MenuPathMains.IsNullOrEmpty())
            {
                //Now mark all the ones contained in this product as true
                //Now initialize the values in the check boxes
                foreach (var menuPaths in iproduct.MenuPathMains)
                {
                    CheckBoxItem cbi = checkedboxes.FirstOrDefault(x => x.Id == menuPaths.Id);
                    if (cbi.IsNull())
                        continue;
                    cbi.IsTrue = true;
                }
            }
        }

        /// <summary>
        /// This creates a list of all check boxes
        /// </summary>
        /// <param name="allMenuPaths"></param>
        /// <returns></returns>
        private static List<CheckBoxItem> createAllCheckBoxes(List<MenuPathMain> allMenuPaths)
        {
            List<CheckBoxItem> checkedboxes = new List<CheckBoxItem>();

            if (!allMenuPaths.IsNullOrEmpty())
            {
                foreach (var menupath in allMenuPaths)
                {
                    CheckBoxItem chk = new CheckBoxItem(menupath.Id, menupath.Name, menupath.MenuPath1.Name, menupath.MenuPath2.Name, menupath.MenuPath3.Name, true);
                    checkedboxes.Add(chk);
                }
            }
            return checkedboxes;
        }

        /// <summary>
        /// This adds all the selected Menu Paths.
        /// </summary>
        /// <param name="iproduct"></param>
        private void addSelectedMenuPathMain(IProduct iproduct)
        {
            Product product = iproduct as Product;
            product.IsNullThrowException("Product could not be boxed.");

            List<CheckBoxItem> selectedPaths = product.CheckedBoxesList.Where(x => x.IsTrue == true).ToList();

            //there are none to add
            if (selectedPaths.IsNullOrEmpty())
                return;

            if (product.MenuPathMains.IsNull())
                product.MenuPathMains = new List<MenuPathMain>();


            //Now add the selected paths to the product.
            foreach (var cbi in selectedPaths)
            {
                //first check to see if the selected path is part of the product.
                MenuPathMain mpm = product.MenuPathMains.FirstOrDefault(x => x.Id == cbi.Id);

                //Path has been found. It is already a part of the product.
                if (!mpm.IsNull())
                    continue;


                mpm = MenuPathMainBiz.FindAll().FirstOrDefault(x => x.Id == cbi.Id);
                mpm.IsNullThrowException("Main path not found! Programming error.");



                product.MenuPathMains.Add(mpm);
                mpm.Products.Add(product);

            }
        }

        /// <summary>
        /// This removes all the unselected Menupaths from the product.
        /// </summary>
        /// <param name="product"></param>
        private static void removeUnselectedMenuPaths(IProduct product)
        {
            //Nothing to remove.
            if (product.MenuPathMains.IsNullOrEmpty())
                return;


            //get all the ids of the unselected MenuPaths
            var emptyPaths = product
                .CheckedBoxesList
                .Where(x => x.IsTrue == false)
                .Select(x => x.Id)
                .ToList();

            //get all the ids of the current MenuPaths
            var currMenuPathsInProduct = product.MenuPathMains.Select(x => x.Id);

            //there is something to delete
            if (!currMenuPathsInProduct.IsNull())
            {
                List<string> currMpmLst = new List<string>();
                foreach (string s in currMenuPathsInProduct)
                {
                    currMpmLst.Add(s);
                }

                foreach (var id in currMpmLst)
                {
                    //If found in the unselected... remove it.
                    if (emptyPaths.Contains(id))
                    {
                        MenuPathMain mpm = product.MenuPathMains.FirstOrDefault(x => x.Id == id);
                        mpm.IsNullThrowException("Menu Path was not found. Programming error.");
                        product.MenuPathMains.Remove(mpm);
                        mpm.Products.Remove((Product)product);

                    }
                }
            }

        }


        //private void fixProductFeatures(IProduct iproduct)
        //{
        //    List<ProductFeature> allcurrentProductFeautres = getAllCurrentProductFeaturesFor(iproduct);
        //    List<MenuFeature> allfeautresAsPerMenuPath = getAllMenuFeaturesAsPerProductsMenuPath(iproduct);
        //    List<ProductFeature> productFeaturesToBeRemoved = getProductFeaturesThatNeedToBeRemoved(allcurrentProductFeautres, allfeautresAsPerMenuPath);
        //    List<ProductFeature> productFeaturesToBeAdded = addProductFeaturesWhichAreInMenuPathButNotInProduct(allcurrentProductFeautres, allfeautresAsPerMenuPath);
        //    removeProductFeaturesFrom(iproduct);
        //    addProductFeaturesTo(iproduct);

        //}

        //private void addProductFeaturesTo(IProduct iproduct)
        //{
        //    throw new System.NotImplementedException();
        //}

        //private void removeProductFeaturesFrom(IProduct iproduct)
        //{
        //    throw new System.NotImplementedException();
        //}

        //private List<ProductFeature> addProductFeaturesWhichAreInMenuPathButNotInProduct(List<ProductFeature> allcurrentProductFeautres, List<MenuFeature> allfeautresAsPerMenuPath)
        //{
        //    throw new System.NotImplementedException();
        //}

        //private List<ProductFeature> getProductFeaturesThatNeedToBeRemoved(List<ProductFeature> allcurrentProductFeautres, List<MenuFeature> allfeautresAsPerMenuPath)
        //{
        //    throw new System.NotImplementedException();
        //}



        //private List<MenuFeature> getAllMenuFeaturesAsPerProductsMenuPath(IProduct iproduct)
        //{
        //    Product product = iproduct as Product;
        //    product.IsNullThrowException("Product is null");

        //    if (product.MenuPathMains.IsNullOrEmpty())
        //        return null;

        //    List<MenuPathMain> menuPathList = product.MenuPathMains.ToList();
        //    menuPathList.IsNullOrEmptyThrowException("menuPathList is empty");

        //    HashSet<MenuFeature> menuFeaturesAsPerMenuPaths = new HashSet<MenuFeature>();
        //    foreach (MenuPathMain mp in menuPathList)
        //    {
        //        HashSet<MenuFeature> currCollection = getAllCurrentFeaturesFor(mp);

        //        if (currCollection.IsNullOrEmpty())
        //            continue;

        //        foreach (MenuFeature mf in currCollection)
        //        {
        //            try
        //            {
        //                menuFeaturesAsPerMenuPaths.Add(mf);
        //            }
        //            catch
        //            {

        //            }
        //        }

        //    }
        //    return menuFeaturesAsPerMenuPaths.ToList();
        //}



        //private List<ProductFeature> getAllCurrentProductFeaturesFor(IProduct iproduct)
        //{
        //    iproduct.IsNullThrowException("Product is null");
        //    if (iproduct.ProductFeatures.IsNullOrEmpty())
        //        return null;

        //    List<ProductFeature> lst = iproduct.ProductFeatures.ToList();
        //    return lst;
        //}
        //private void addFeatures(Product product, HashSet<MenuFeature> menuFeaturesHashSet)
        //{

        //    if (!menuFeaturesHashSet.IsNullOrEmpty())
        //    {
        //        //make sure the features being added are not already a part of the product
        //        foreach (MenuFeature mf in menuFeaturesHashSet)
        //        {
        //            ProductFeature pf;
        //            if (!product.ProductFeatures.IsNull())
        //            {
        //                pf = product.ProductFeatures.FirstOrDefault(x => x.Name == mf.Name);
        //                if (!pf.IsNull())
        //                {
        //                    //feature is already a part of Products, Dont add...
        //                    continue;
        //                }
        //            }

        //            //add
        //            pf = new ProductFeature();
        //            pf.Name = mf.Name;
        //            product.ProductFeatures.Add(pf);

        //        }
        //    }
        //}


        //private void RemoveFeatures(Product product, List<ProductFeature> productFeatureList)
        //{
        //    //remove those features that are now not existant in any of the menu paths.

        //    if (productFeatureList.IsNullOrEmpty())
        //        return;

        //    if (product.ProductFeatures.IsNullOrEmpty())
        //        return;

        //    List<string> nameOfMenuFeaturesToRemoveLst = new List<string>();

        //    //first remove any feature that is not a part of menuFeaturesHashSet.
        //    //this means that some meap path has been removed, and then the feature must go.
        //    foreach (ProductFeature pf in productFeatureList)
        //    {

        //        ProductFeature pfFound = ProductFeatureBiz.FindAll().FirstOrDefault(x => x.Name == mf.Name);
        //        if (pfFound.IsNull())
        //        {
        //            //found, this menu feature no longer exists.
        //            nameOfMenuFeaturesToRemoveLst.Add(mf.Name);
        //        }
        //    }

        //    //now remove them from the product
        //    if (!nameOfMenuFeaturesToRemoveLst.IsNullOrEmpty())
        //    {
        //        foreach (string name in nameOfMenuFeaturesToRemoveLst)
        //        {
        //            ProductFeature pf = product.ProductFeatures.FirstOrDefault(x => x.Name == name);

        //            if (pf.IsNull())
        //                continue;

        //            product.ProductFeatures.Remove(pf);
        //        }
        //    }
        //}


        //private static HashSet<MenuFeature> getAllCurrentFeaturesFor(MenuPathMain mpm)
        //{
        //    mpm.MenuPath1.IsNullThrowException("Menu Path 1 is null. Programming error.");
        //    List<MenuFeature> menuFeatures1 = mpm.MenuPath1.MenuFeatures.ToList();
        //    List<MenuFeature> menuFeatures2 = mpm.MenuPath2.MenuFeatures.ToList();
        //    List<MenuFeature> menuFeatures3 = mpm.MenuPath3.MenuFeatures.ToList();

        //    HashSet<MenuFeature> menuFeaturesHashSet = new HashSet<MenuFeature>();

        //    if (!menuFeatures1.IsNullOrEmpty())
        //    {
        //        foreach (MenuFeature mf in menuFeatures1)
        //        {
        //            menuFeaturesHashSet.Add(mf);
        //        }
        //    }

        //    if (!menuFeatures2.IsNullOrEmpty())
        //    {
        //        foreach (MenuFeature mf in menuFeatures2)
        //        {
        //            menuFeaturesHashSet.Add(mf);
        //        }
        //    }

        //    if (!menuFeatures3.IsNullOrEmpty())
        //    {
        //        foreach (MenuFeature mf in menuFeatures3)
        //        {
        //            menuFeaturesHashSet.Add(mf);
        //        }
        //    }
        //    return menuFeaturesHashSet;
        //}

    }
}
