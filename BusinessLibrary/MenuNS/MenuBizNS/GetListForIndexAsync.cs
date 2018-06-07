using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;

namespace UowLibrary.MenuNS
{
    public partial class MenuBiz
    {
        public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parms)
        {


            List<ICommonWithId> lst = new List<ICommonWithId>();
            switch (parms.Menu.MenuLevel)
            {
                case MenuLevelENUM.Level_1:

                    //All category 1s
                    return await Menu1_CategoryListAsync();


                case MenuLevelENUM.Level_2:
                    //All category 2s for selected category 1
                    //
                    return await Menu2_CategoryListAsync(parms);



                case MenuLevelENUM.Level_3:
                    //All category 3s for selected category 1 and selected category 2
                    return await Menu3_CategoryListAsync(parms);

                case MenuLevelENUM.Level_4:
                    //All Products
                    return await Menu4_ProductListAsync(parms);

                case MenuLevelENUM.Level_5:
                    //Product children
                    return await Menu5_ProductChildrenListAsync(parms);
;

                default:
                    //List all the unique ProductCategory1's of ProductCategoryMain
                    break;
            }

            return null;
        }

        #region Menu 1

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
        private async Task<List<ICommonWithId>> Menu1_CategoryListAsync()
        {
            List<string> listOfProductCat1Ids = UniqueMenuMainWithMenu1();

            if (listOfProductCat1Ids.IsNullOrEmpty())
                return null;
            //we need to return ProductCategoryMain because the Index needs it.
            List<ICommonWithId> pclst = new List<ICommonWithId>();

            var allMenuMain = await FindAllAsync();
            var allMenu1 = await _menuPath1Biz.FindAllAsync();

            foreach (var id in listOfProductCat1Ids)
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
            if (parms.Menu.MenuPath1Id.IsNullOrEmpty())
            {
                ErrorsGlobal.Add("Category 1 argument missing. Programming Error", MethodBase.GetCurrentMethod());
                throw new ArgumentNullException(ErrorsGlobal.ToString());
            }



            //get the productCategoryMain
            MenuPathMain pcm = Dal.FindAll().FirstOrDefault(x =>
                x.MenuPath1Id == parms.Menu.MenuPath1Id);

            if (pcm.IsNull())
                return null;

            return pcm.Products.ToList();
        }

        #endregion

        #region Menu 2
        private List<string> UniqueListOfProductCategory2_IDs(string productCategory1Id)
        {

            if (productCategory1Id.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.Add("productCategory1Id is null.", MethodBase.GetCurrentMethod());
                throw new ArgumentNullException(ErrorsGlobal.ToString());
            }

            var cat2LstIds = FindAll()
                .Where(x => x.MenuPath1Id == productCategory1Id)
                .Select(x => x.MenuPath2Id)
                .Distinct()
                .ToList();

            return cat2LstIds;

        }
        private async Task<List<ICommonWithId>> Menu2_CategoryListAsync(ControllerIndexParams parms)
        {
            if (parms.Id.IsNullOrWhiteSpace())
            {
                //this is coming from Edit or Create -Back to list.
                //we need to find a dummy productCategoryMain with the same product 1
                if (parms.Menu.MenuPath1Id.IsNullOrWhiteSpace())
                {
                    //oops something has gone wrong
                    ErrorsGlobal.Add("No Product Category 1 received.", MethodBase.GetCurrentMethod());
                    throw new NoDataException(ErrorsGlobal.ToString());
                }

                var pcmDummy = Dal.FindAll().ToList().FirstOrDefault(x => x.MenuPath1Id == parms.Menu.MenuPath1Id);

                if (pcmDummy.IsNull())
                {
                    //oops something has gone wrong
                    ErrorsGlobal.Add("Product Category Main Not found.", MethodBase.GetCurrentMethod());
                    throw new NoDataException(ErrorsGlobal.ToString());

                }
                parms.Id = pcmDummy.Id;
            }

            string productCatMainId = parms.Id;
            MenuPathMain pcm = await Dal.FindForAsync(productCatMainId);
            if (pcm.IsNull())
            {
                ErrorsGlobal.Add("Product Category Main Not found.", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());

            }
            List<string> listOfProductCat2Ids = UniqueListOfProductCategory2_IDs(pcm.MenuPath1Id);

            if (listOfProductCat2Ids.IsNullOrEmpty())
                return null;

            List<ICommonWithId> pclst = new List<ICommonWithId>();
            var allProductCatMain = await Dal.FindAllAsync();
            var allProductCatMainWithProductCategory1 = allProductCatMain.Where(x => x.MenuPath1Id == pcm.MenuPath1Id).ToList();

            foreach (var cat2Id in listOfProductCat2Ids)
            {
                MenuPathMain pc = allProductCatMainWithProductCategory1.Where(x => x.MenuPath2Id == cat2Id).FirstOrDefault();
                if (!pc.IsNull())
                    pclst.Add(pc);
            }

            return pclst;
        }
        private List<Product> Menu2_ProductList(ControllerIndexParams parms)
        {
            if (parms.Menu.MenuPath1Id.IsNullOrEmpty())
            {
                ErrorsGlobal.Add("Category 1 argument missing. Programming Error", MethodBase.GetCurrentMethod());
                throw new ArgumentNullException(ErrorsGlobal.ToString());
            }

            if (parms.Menu.MenuPath2Id.IsNullOrEmpty())
            {
                ErrorsGlobal.Add("Category 2 argument missing. Programming Error", MethodBase.GetCurrentMethod());
                throw new ArgumentNullException(ErrorsGlobal.ToString());
            }


            //get the productCategoryMain
            MenuPathMain pcm = Dal.FindAll().FirstOrDefault(x =>
                x.MenuPath1Id == parms.Menu.MenuPath1Id &&
                x.MenuPath2Id == parms.Menu.MenuPath2Id);

            if (pcm.IsNull())
                return null;

            return pcm.Products.ToList();
        }

        #endregion

        #region Menu 3
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
        private async Task<List<ICommonWithId>> Menu3_CategoryListAsync(ControllerIndexParams parms)
        {

            if (parms.Id.IsNullOrWhiteSpace())
            {
                //this is coming from Edit or Create -Back to list.
                //we need to find a dummy productCategoryMain with the same product 1
                if (parms.Menu.MenuPath1Id.IsNullOrWhiteSpace())
                {
                    //oops something has gone wrong
                    ErrorsGlobal.Add("No Product Category 1 received.", MethodBase.GetCurrentMethod());
                    throw new NoDataException(ErrorsGlobal.ToString());
                }

                if (parms.Menu.MenuPath2Id.IsNullOrWhiteSpace())
                {
                    //oops something has gone wrong
                    ErrorsGlobal.Add("No Product Category 2 received.", MethodBase.GetCurrentMethod());
                    throw new NoDataException(ErrorsGlobal.ToString());
                }

                var pcmDummy = Dal.FindAll().ToList().FirstOrDefault(x => x.MenuPath1Id == parms.Menu.MenuPath1Id && x.MenuPath2Id == parms.Menu.MenuPath2Id);

                if (pcmDummy.IsNull())
                {
                    //oops something has gone wrong
                    ErrorsGlobal.Add("Product Category Main Not found for level 3.", MethodBase.GetCurrentMethod());
                    throw new NoDataException(ErrorsGlobal.ToString());

                }
                parms.Id = pcmDummy.Id;
            }
            string productCatMainId = parms.Id;
            MenuPathMain pcm = await Dal.FindForAsync(productCatMainId);
            if (pcm.IsNull())
            {
                ErrorsGlobal.Add("Product Category Main Not found.", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());

            }
            List<MenuPathMain> uniqueListOfMainPaths = UniqueListOfMainPath_IDs(pcm.MenuPath1Id, pcm.MenuPath2Id);
            if (uniqueListOfMainPaths.IsNullOrEmpty())
                return null;


            List<ICommonWithId> mpmlst = uniqueListOfMainPaths.Cast<ICommonWithId>().ToList();


            return mpmlst;
        }
        private List<Product> Menu3_ProductList(ControllerIndexParams parms)
        {
            if (parms.Menu.MenuPath1Id.IsNullOrEmpty())
            {
                ErrorsGlobal.Add("Category 1 argument missing. Programming Error", MethodBase.GetCurrentMethod());
                throw new ArgumentNullException(ErrorsGlobal.ToString());
            }

            if (parms.Menu.MenuPath2Id.IsNullOrEmpty())
            {
                ErrorsGlobal.Add("Category 2 argument missing. Programming Error", MethodBase.GetCurrentMethod());
                throw new ArgumentNullException(ErrorsGlobal.ToString());
            }

            if (parms.Menu.MenuPath3Id.IsNullOrEmpty())
            {
                ErrorsGlobal.Add("Category 3 argument missing. Programming Error", MethodBase.GetCurrentMethod());
                throw new ArgumentNullException(ErrorsGlobal.ToString());
            }


            //get the productCategoryMain
            MenuPathMain pcm = Dal.FindAll().FirstOrDefault(x =>
                x.MenuPath1Id == parms.Menu.MenuPath1Id &&
                x.MenuPath2Id == parms.Menu.MenuPath2Id &&
                x.MenuPath3Id == parms.Menu.MenuPath3Id);
            if (pcm.IsNull())
                return null;

            return pcm.Products.ToList();
        }

        #endregion

        #region Menu 4
        private async Task<List<ICommonWithId>> Menu4_ProductListAsync(ControllerIndexParams parms)
        {

            parms.Menu.MenuPath1Id.IsNullOrWhiteSpaceThrowException();
            parms.Menu.MenuPath2Id.IsNullOrWhiteSpaceThrowException();
            parms.Menu.MenuPath3Id.IsNullOrWhiteSpaceThrowException();

            List<MenuPathMain> mpmLst = await _menuPathMainBiz.FindAllAsync();

            if (mpmLst.IsNullOrEmpty())
                return null;

            MenuPathMain mpm = mpmLst.FirstOrDefault(x =>
                x.MenuPath1Id == parms.Menu.MenuPath1Id &&
                x.MenuPath2Id == parms.Menu.MenuPath2Id &&
                x.MenuPath3Id == parms.Menu.MenuPath3Id);

            mpm.IsNullThrowException("Menu Path does note exist. Something is wrong.");

            parms.Id = mpm.Id;

            List<Product> listOfProducts = mpm.Products.ToList();

            if (listOfProducts.IsNullOrEmpty())
                return null;


            List<ICommonWithId> pclst = listOfProducts.Cast<ICommonWithId>().ToList();

            return pclst;
        }

        #endregion

        #region Menu 5

        /// <summary>
        /// This will return Product Children.
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        private async Task<List<ICommonWithId>> Menu5_ProductChildrenListAsync(ControllerIndexParams parms)
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
