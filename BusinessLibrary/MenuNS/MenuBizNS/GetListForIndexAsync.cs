using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

            switch (parms.Menu.MenuLevel)
            {
                case MenuLevelENUM.Level_1:

                    //All category 1s
                    lst = await Level1_DataListAsync();
                    break;

                case MenuLevelENUM.Level_2:
                    //All category 2s for selected category 1
                    //
                    lst = await Level2_DataListAsync(parms);
                    break;



                case MenuLevelENUM.Level_3:
                    //All category 3s for selected category 1 and selected category 2
                    lst = await Level3_DataListAsync(parms);
                    break;

                case MenuLevelENUM.Level_4:
                    //All Products
                    lst = await Level4_DataListAsync(parms);
                    break;

                case MenuLevelENUM.Level_5:
                    //Product children
                    lst = await Level5_ListAsync(parms);
                    break;

                default:
                    //List all the unique ProductCategory1's of ProductCategoryMain
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
        private List<string> UniqueMenuMainWithMenu1()
        {

            var cat1LstIds = FindAll()
                .Select(x => x.MenuPath1Id)
                .Distinct()
                .ToList();

            return cat1LstIds;

        }

        /// <summary>
        /// This returns a unique list of ProductCategory1Ids
        /// </summary>
        /// <returns></returns>
        private async Task<List<ICommonWithId>> Level1_DataListAsync()
        {
            List<string> listOfMenuPath1Ids = UniqueMenuMainWithMenu1();

            if (listOfMenuPath1Ids.IsNullOrEmpty())
                return null;
            //we need to return ProductCategoryMain because the Index needs it.
            List<ICommonWithId> pclst = new List<ICommonWithId>();

            var allMenuMain = await FindAllAsync();
            var allMenu1 = await _menuPath1Biz.FindAllAsync();

            foreach (var id in listOfMenuPath1Ids)
            {
                var pc = allMenuMain.FirstOrDefault(x => x.MenuPath1Id == id);
                pc.MenuPath1.MiscFiles = _uploadedFileBiz.FindAll().Where(x => x.MenuPath1Id == pc.MenuPath1Id).ToList();
                //load the uploads because they are not being uploaded
                pclst.Add(pc);
            }

            return pclst;
        }
        private List<Product> Menu1_ProductList(ControllerIndexParams parms)
        {
            parms.Menu.MenuPathMainId.IsNullOrWhiteSpaceThrowException("Menu Path Main Id Missing in parameteres.");


            //get the productCategoryMain
            MenuPathMain pcm = Find(parms.Menu.MenuPathMainId);

            if (pcm.IsNull())
                return null;

            return pcm.Products.ToList();
        }

        #endregion

        #region Level 2
        private List<string> UniqueListOfProductCategory2_IDs(string productCategory1Id)
        {

            productCategory1Id.IsNullOrWhiteSpaceThrowException("productCategory1Id is null.");


            var cat2LstIds = FindAll()
                .Where(x => x.MenuPath1Id == productCategory1Id)
                .Select(x => x.MenuPath2Id)
                .Distinct()
                .ToList();

            return cat2LstIds;

        }
        private async Task<List<ICommonWithId>> Level2_DataListAsync(ControllerIndexParams parms)
        {

            MenuPathMain mpm = await FindAsync(parms.Menu.MenuPathMainId);
            mpm.IsNullThrowException("Main path not found.");

            List<string> listOfMenuPath2Ids = UniqueListOfProductCategory2_IDs(mpm.MenuPath1Id);

            if (listOfMenuPath2Ids.IsNullOrEmpty())
                return null;

            List<ICommonWithId> mpmlst = new List<ICommonWithId>();

            var allMenuPathMain = await FindAllAsync();
            var allMenuPathMainWithMenuPath1 = allMenuPathMain.Where(x => x.MenuPath1Id == mpm.MenuPath1Id).ToList();

            foreach (var mp2Id in listOfMenuPath2Ids)
            {
                MenuPathMain mpm1 = allMenuPathMainWithMenuPath1.Where(x => x.MenuPath2Id == mp2Id).FirstOrDefault();
                if (!mpm1.IsNull())
                    mpmlst.Add(mpm1);
            }

            return mpmlst;
        }
        private List<Product> Menu2_ProductList(ControllerIndexParams parms)
        {
            parms.Menu.MenuPathMainId.IsNullOrWhiteSpaceThrowException("Menu Path 1 argument missing. Programming Error");

            //get the productCategoryMain
            MenuPathMain pcm = Find (parms.Menu.MenuPathMainId);

            if (pcm.IsNull())
                return null;

            return pcm.Products.ToList();
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
        private async Task<List<ICommonWithId>> Level3_DataListAsync(ControllerIndexParams parms)
        {

            parms.Menu.MenuPathMainId.IsNullOrWhiteSpaceThrowException("Main Menu Path Id not received.");

            MenuPathMain mpm = await FindAsync(parms.Menu.MenuPathMainId);
            mpm.IsNullThrowException("MenuPathMain not found.");

            List<MenuPathMain> uniqueListOfMainPaths = UniqueListOfMainPath_IDs(mpm.MenuPath1Id, mpm.MenuPath2Id);
            
            if (uniqueListOfMainPaths.IsNullOrEmpty())
                return null;


            List<ICommonWithId> mpmlst = uniqueListOfMainPaths.Cast<ICommonWithId>().ToList();


            return mpmlst;
        }
        

        #endregion

        #region Level 4
        private async Task<List<ICommonWithId>> Level4_DataListAsync(ControllerIndexParams parms)
        {

            //parms.Menu.MenuPath1Id.IsNullOrWhiteSpaceThrowException();
            //parms.Menu.MenuPath2Id.IsNullOrWhiteSpaceThrowException();
            //parms.Menu.MenuPath3Id.IsNullOrWhiteSpaceThrowException();

            //List<MenuPathMain> mpmLst = await _menuPathMainBiz.FindAllAsync();

            //if (mpmLst.IsNullOrEmpty())
            //    return null;

            MenuPathMain mpm = await FindAsync(parms.Id);
            mpm.IsNullThrowException("Menu Path does note exist. Something is wrong.");


            List<Product> listOfProducts = mpm.Products.ToList();
            if (listOfProducts.IsNullOrEmpty())
                return null;

            foreach (var prod in listOfProducts)
            {
                prod.MenuManager.MenuPathMain = mpm;
            }

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
        private async Task<List<ICommonWithId>> Level5_ListAsync(ControllerIndexParams parms)
        {

            parms.Menu.ProductId.IsNullOrWhiteSpaceThrowException();
            Product parentProduct = await _productBiz.FindAsync(parms.Menu.ProductId);

            //parent product cannot be null. It is some kind of programming error if it is.
            parentProduct.IsNullThrowException("Product not found.");

            if (parentProduct.ProductChildren.IsNull())
                return null;

            List<ICommonWithId> children = parentProduct.ProductChildren.Cast<ICommonWithId>().ToList();

            return children;
        }
        #endregion





    }
}
