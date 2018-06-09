using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.SharedNS;
using System.Collections.Generic;
using System.Linq;

namespace UowLibrary.ProductNS
{
    public partial class ProductBiz
    {

        //this returns a list of checked boxes marked true
        public List<CheckBoxItem> LoadMenuPathCheckedBoxes(Product product)
        {
            var allMenuPaths = MenuPathMainBiz.FindAll();

            if (allMenuPaths.IsNullOrEmpty())
                return null;

            //Now create all the check boxes
            List<CheckBoxItem> checkedboxes = new List<CheckBoxItem>();

            foreach (var menupath in allMenuPaths)
            {
                CheckBoxItem chk = new CheckBoxItem(menupath.Id, menupath.Name, true);
                checkedboxes.Add(chk);
            }

            //Now mark all the ones contained in this product as true

            if (product.MenuPathMains.IsNullOrEmpty())
                return checkedboxes;

            //Now initialize the values in the check boxes
            foreach (var menuPaths in product.MenuPathMains)
            {
                CheckBoxItem cbi = checkedboxes.FirstOrDefault(x => x.Id == menuPaths.Id);
                if (cbi.IsNull())
                    continue;
                cbi.IsTrue = true;
            }

            return checkedboxes;
        }

        public void GetDataFromMenuPathCheckBoxes(Product product)
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
        private void addSelectedMenuPathMain(Product product)
        {
            List<CheckBoxItem> selectedPaths = product.CheckedBoxesList.Where(x => x.IsTrue == true).ToList();

            //there are none to add
            if (selectedPaths.IsNullOrEmpty())
                return;

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
                mpm.Products.Add(product);

            }
        }


        /// <summary>
        /// This removes all the unselected Menupaths from the product.
        /// </summary>
        /// <param name="product"></param>
        private static void removeUnselectedMenuPaths(Product product)
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
                        mpm.Products.Remove(product);

                    }
                }
            }

        }
    }
}
