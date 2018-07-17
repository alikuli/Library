using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS;
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
        public void LoadMenuPathCheckedBoxes(IProduct iproduct)
        {
            IProduct product = iproduct;
            product.MenuManager.MenuState.MenuPath1Id.IsNullOrWhiteSpaceThrowException("Menu Path 1 is null.");

            MenuPath1 mp1 = _menuPath1Biz.Find(product.MenuManager.MenuState.MenuPath1Id);
            mp1.IsNullThrowException("Menu path 1 not found");


            var allMenuPaths = MenuPathMainBiz.FindAllMenuPathMainsFor(mp1.MenuPath1Enum);


            if (allMenuPaths.IsNullOrEmpty())
                return;

            //Now create all the check boxes
            List<CheckBoxItem> checkedboxes = createAllCheckBoxes(allMenuPaths);
            markProductSelectedCheckBoxesTrue(product, checkedboxes);

            product.CheckedBoxesList = checkedboxes;
        }

        /// <summary>
        /// This marks the check boxes that have been selected in the product true.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="checkedboxes"></param>
        private static void markProductSelectedCheckBoxesTrue(IProduct product, List<CheckBoxItem> checkedboxes)
        {
            //Here we are going to get the marked menupaths
            if (!product.MenuPathMains.IsNullOrEmpty())
            {
                //Now mark all the ones contained in this product as true
                //Now initialize the values in the check boxes
                foreach (var menuPaths in product.MenuPathMains)
                {
                    CheckBoxItem cbi = checkedboxes.FirstOrDefault(x => x.Id == menuPaths.Id);
                    if (cbi.IsNull())
                        continue;
                    cbi.IsTrue = true;
                }
            }
        }

        private static List<CheckBoxItem> createAllCheckBoxes(IQueryable<MenuPathMain> allMenuPaths)
        {
            List<CheckBoxItem> checkedboxes = new List<CheckBoxItem>();
            List<MenuPathMain> lst = allMenuPaths.ToList();
            if (!lst.IsNullOrEmpty())
            {
                foreach (var menupath in allMenuPaths)
                {
                    CheckBoxItem chk = new CheckBoxItem(menupath.Id, menupath.Name, true);
                    checkedboxes.Add(chk);
                }
            }
            return checkedboxes;
        }

        public void GetDataFromMenuPathCheckBoxes(IProduct product)
        {
            product.IsNullThrowExceptionArgument("product is null");


            if (product.CheckedBoxesList.IsNullOrEmpty())
                return;//Nothing to do.


            removeUnselectedMenuPaths(product);
            //Now we will add all new paths to the product

            addSelectedMenuPathMain(product);

        }

        /// <summary>
        /// This adds all the selected Menu Paths.
        /// </summary>
        /// <param name="product"></param>
        private void addSelectedMenuPathMain(IProduct product)
        {
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


                mpm = _menuPathMainBiz.FindAll().FirstOrDefault(x => x.Id == cbi.Id);
                mpm.IsNullThrowException("Main path not found! Programming error.");

                product.MenuPathMains.Add(mpm);
                mpm.Products.Add((Product)product);

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
    }
}
