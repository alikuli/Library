using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
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

            switch (parms.Menu.MenuEnum)
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
        private List<string> UniqueMenuMainWithMenu1()
        {

            var mp1LstIds = FindAll()
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
            List<string> listOfMenuPath1Ids = UniqueMenuMainWithMenu1();

            if (listOfMenuPath1Ids.IsNullOrEmpty())
                return null;

            //we need to return MenuPathMain List because the Index needs it.
            List<ICommonWithId> pclst = new List<ICommonWithId>();

            var allMenuMain = await FindAllAsync();
            var allMenu1 = await MenuPathMainBiz.MenuPath1Biz.FindAllAsync();

            foreach (var id in listOfMenuPath1Ids)
            {
                var pc = allMenuMain.FirstOrDefault(x => x.MenuPath1Id == id);
                pclst.Add(pc);
            }

            return pclst;
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
            parms.Menu.MenuPathMainId.IsNullOrWhiteSpaceThrowException();
            MenuPathMain mpm = await FindAsync(parms.Menu.MenuPathMainId);
            mpm.IsNullThrowException("Main path not found.");

            var allMenuPathMain = await FindAllAsync();
            var allMenuPathMainWithMenuPath1 = allMenuPathMain.Where(x => x.MenuPath1Id == mpm.MenuPath1Id).ToList();
            List<string> listOfMenuPath2Ids = UniqueListOfMenuPath2_IDs(allMenuPathMainWithMenuPath1);

            if (listOfMenuPath2Ids.IsNullOrEmpty())
                return null;


            List<ICommonWithId> mpmlst = new List<ICommonWithId>();

            foreach (var mp2Id in listOfMenuPath2Ids)
            {
                MenuPathMain mpmInner = allMenuPathMainWithMenuPath1.Where(x => x.MenuPath2Id == mp2Id).FirstOrDefault();
                if (!mpmInner.IsNull())
                    mpmlst.Add(mpmInner);


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

            parms.Menu.MenuPathMainId.IsNullOrWhiteSpaceThrowException("Main Menu Path Id not received.");

            MenuPathMain mpm = await FindAsync(parms.Menu.MenuPathMainId);
            mpm.IsNullThrowException("MenuPathMain not found.");

            List<MenuPathMain> uniqueListOfMainPaths = UniqueListOfMainPath_IDs(mpm.MenuPath1Id, mpm.MenuPath2Id);

            if (uniqueListOfMainPaths.IsNullOrEmpty())
                return null;

            //in case the menu has not been set we need to check if there are any items.
            //if there are no menu 3 items, then there will be no MenuPath3
            bool thereAreNoItems = true;
            foreach (var item in uniqueListOfMainPaths)
            {
                if (!item.MenuPath3.IsNull())
                {
                    thereAreNoItems = false;
                    break;
                }
            }

            if (thereAreNoItems)
                return null;

            List<ICommonWithId> mpmlst = uniqueListOfMainPaths.Cast<ICommonWithId>().ToList();


            return mpmlst;
        }


        #endregion


        #region Level 4
        private async Task<List<ICommonWithId>> indexProduct_DataListAsync(ControllerIndexParams parms)
        {


            //get the menupathMain
            MenuPathMain mpm = await FindAsync(parms.Menu.MenuPathMainId);
            mpm.IsNullThrowException("Menu Path does note exist. Something is wrong.");

            //Get all the products listed by it
            List<Product> listOfProducts = mpm.Products.ToList();
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

            parms.Menu.ProductId.IsNullOrWhiteSpaceThrowException();
            Product parentProduct = await ProductBiz.FindAsync(parms.Menu.ProductId);
            //parent product cannot be null. It is some kind of programming error if it is.
            parentProduct.IsNullThrowException(string.Format("Product not found. Id ='{0}'", parms.Menu.ProductId));

            //get all its child products and display them

            if (parentProduct.ProductChildren.IsNullOrEmpty())
                return null;

            List<ProductChild> children = parentProduct.ProductChildren.ToList();
            children.IsNullOrEmptyThrowException("Something went wrong, No Product Children found.");
            List<ICommonWithId> childrenAsIcommonLst = children.Cast<ICommonWithId>().ToList();

            return childrenAsIcommonLst;
        }
        #endregion





    }
}
