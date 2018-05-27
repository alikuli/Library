using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.Interfaces;
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
using UowLibrary.ProductNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;
namespace UowLibrary.MenuNS
{
    public partial class MenuBiz : BusinessLayer<MenuPathMain>
    {
        protected MenuPathMainBiz _productCatMainBiz;
        protected ProductBiz _productBiz;
        public MenuBiz(IRepositry<ApplicationUser> userDal, MenuPathMainBiz productCatMainBiz, ProductBiz productBiz, IRepositry<MenuPathMain> entityDal, IMemoryMain memoryMain, IErrorSet errorSet, ApplicationDbContext db, ConfigManagerHelper configManager, UploadedFileBiz uploadedFileBiz)
            : base(userDal, memoryMain, errorSet, entityDal, db, configManager, uploadedFileBiz)
        {

            _productCatMainBiz = productCatMainBiz;
            _productBiz = productBiz;
        }



        public override string SelectListCacheKey
        {
            get { throw new System.NotImplementedException(); }
        }


        public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parms)
        {
            //if (parms.Menu.MenuLevel == MenuLevelENUM.unknown)
            //    parms.Menu.MenuLevel = MenuLevelENUM.Level_1;


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
                    ;

                case MenuLevelENUM.Level_4:
                    //All Products
                    break;

                default:
                    //List all the unique ProductCategory1's of ProductCategoryMain
                    break;
            }

            return null;
        }


        private IQueryable<MenuPathMain> AllProductCatMainIQ()
        {
            return Dal.FindAll();
        }


        #region Menu 1



        /// <summary>
        /// This returns a unique list of ProductCategory1 found in ProductCategoryMain
        /// </summary>
        /// <param name="cat1Id"></param>
        /// <returns></returns>
        private List<string> UniqueListOfProductCategory1_IDs()
        {

            var cat1LstIds = AllProductCatMainIQ()
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
            List<string> listOfProductCat1Ids = UniqueListOfProductCategory1_IDs();
            if (listOfProductCat1Ids.IsNullOrEmpty())
                return null;
            //we need to return ProductCategoryMain because the Index needs it.
            List<ICommonWithId> pclst = new List<ICommonWithId>();
            var allProductCatMain = await Dal.FindAllAsync();
            foreach (var id in listOfProductCat1Ids)
            {
                var pc = allProductCatMain.FirstOrDefault(x => x.MenuPath1Id == id);
                pclst.Add(pc);
            }

            return pclst;
        }


        private List<Product> Menu1_ProductList(ControllerIndexParams parms)
        {
            if (parms.Menu.ProductCat1Id.IsNullOrEmpty())
            {
                ErrorsGlobal.Add("Category 1 argument missing. Programming Error", MethodBase.GetCurrentMethod());
                throw new ArgumentNullException(ErrorsGlobal.ToString());
            }



            //get the productCategoryMain
            MenuPathMain pcm = Dal.FindAll().FirstOrDefault(x =>
                x.MenuPath1Id == parms.Menu.ProductCat1Id);

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

            var cat2LstIds = AllProductCatMainIQ()
                .Where(x => x.MenuPath1Id == productCategory1Id)
                .Select(x => x.MenuPath2Id)
                .Distinct()
                .ToList();

            return cat2LstIds;

        }
        private async Task<List<ICommonWithId>> Menu2_CategoryListAsync(ControllerIndexParams parms)
        {
            if(parms.Id.IsNullOrWhiteSpace())
            {
                //this is coming from Edit or Create -Back to list.
                //we need to find a dummy productCategoryMain with the same product 1
                if(parms.Menu.ProductCat1Id.IsNullOrWhiteSpace())
                {
                    //oops something has gone wrong
                    ErrorsGlobal.Add("No Product Category 1 received.", MethodBase.GetCurrentMethod());
                    throw new NoDataException(ErrorsGlobal.ToString());
                }

                var pcmDummy = Dal.FindAll().ToList().FirstOrDefault(x => x.MenuPath1Id == parms.Menu.ProductCat1Id);

                if(pcmDummy.IsNull())
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
            if (parms.Menu.ProductCat1Id.IsNullOrEmpty())
            {
                ErrorsGlobal.Add("Category 1 argument missing. Programming Error", MethodBase.GetCurrentMethod());
                throw new ArgumentNullException(ErrorsGlobal.ToString());
            }

            if (parms.Menu.ProductCat2Id.IsNullOrEmpty())
            {
                ErrorsGlobal.Add("Category 2 argument missing. Programming Error", MethodBase.GetCurrentMethod());
                throw new ArgumentNullException(ErrorsGlobal.ToString());
            }


            //get the productCategoryMain
            MenuPathMain pcm = Dal.FindAll().FirstOrDefault(x =>
                x.MenuPath1Id == parms.Menu.ProductCat1Id &&
                x.MenuPath2Id == parms.Menu.ProductCat2Id);

            if (pcm.IsNull())
                return null;

            return pcm.Products.ToList();
        }



        #endregion





        #region Menu 3
        private List<string> UniqueListOfProductCategory3_IDs(string productCategory1Id, string productCategory2Id)
        {
            if (productCategory1Id.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.Add("productCategory1Id is null.", MethodBase.GetCurrentMethod());
                throw new ArgumentNullException(ErrorsGlobal.ToString());
            }
            if (productCategory2Id.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.Add("productCategory2Id is null.", MethodBase.GetCurrentMethod());
                throw new ArgumentNullException(ErrorsGlobal.ToString());
            }

            var cat3LstIds = AllProductCatMainIQ()
                .Where(x => x.MenuPath1Id == productCategory1Id && x.MenuPath2Id == productCategory2Id)
                .Select(x => x.MenuPath3Id)
                .Distinct()
                .ToList();

            return cat3LstIds;

        }
        private async Task<List<ICommonWithId>> Menu3_CategoryListAsync(ControllerIndexParams parms)
        {

            if (parms.Id.IsNullOrWhiteSpace())
            {
                //this is coming from Edit or Create -Back to list.
                //we need to find a dummy productCategoryMain with the same product 1
                if (parms.Menu.ProductCat1Id.IsNullOrWhiteSpace())
                {
                    //oops something has gone wrong
                    ErrorsGlobal.Add("No Product Category 1 received.", MethodBase.GetCurrentMethod());
                    throw new NoDataException(ErrorsGlobal.ToString());
                }

                if (parms.Menu.ProductCat2Id.IsNullOrWhiteSpace())
                {
                    //oops something has gone wrong
                    ErrorsGlobal.Add("No Product Category 2 received.", MethodBase.GetCurrentMethod());
                    throw new NoDataException(ErrorsGlobal.ToString());
                }

                var pcmDummy = Dal.FindAll().ToList().FirstOrDefault(x => x.MenuPath1Id == parms.Menu.ProductCat1Id && x.MenuPath2Id == parms.Menu.ProductCat2Id);

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
            List<string> listOfProductCat3Ids = UniqueListOfProductCategory3_IDs(pcm.MenuPath1Id, pcm.MenuPath2Id);
            if (listOfProductCat3Ids.IsNullOrEmpty())
                return null;

            List<ICommonWithId> pclst = new List<ICommonWithId>();

            var allProductCatMain = await Dal.FindAllAsync();

            var allProductCatMainWithCategory1 = allProductCatMain.Where(x => x.MenuPath1Id == pcm.MenuPath1Id && x.MenuPath2Id == pcm.MenuPath2Id).ToList();

            foreach (var id in listOfProductCat3Ids)
            {
                MenuPathMain pc = allProductCatMainWithCategory1.FirstOrDefault(x => x.MenuPath3Id == id);
                if (!pc.IsNull())
                    pclst.Add(pc);
            }

            return pclst;
        }

        private List<Product> Menu3_ProductList(ControllerIndexParams parms)
        {
            if (parms.Menu.ProductCat1Id.IsNullOrEmpty())
            {
                ErrorsGlobal.Add("Category 1 argument missing. Programming Error", MethodBase.GetCurrentMethod());
                throw new ArgumentNullException(ErrorsGlobal.ToString());
            }

            if (parms.Menu.ProductCat2Id.IsNullOrEmpty())
            {
                ErrorsGlobal.Add("Category 2 argument missing. Programming Error", MethodBase.GetCurrentMethod());
                throw new ArgumentNullException(ErrorsGlobal.ToString());
            }

            if (parms.Menu.ProductCat3Id.IsNullOrEmpty())
            {
                ErrorsGlobal.Add("Category 3 argument missing. Programming Error", MethodBase.GetCurrentMethod());
                throw new ArgumentNullException(ErrorsGlobal.ToString());
            }


            //get the productCategoryMain
            MenuPathMain pcm = Dal.FindAll().FirstOrDefault(x =>
                x.MenuPath1Id == parms.Menu.ProductCat1Id &&
                x.MenuPath2Id == parms.Menu.ProductCat2Id &&
                x.MenuPath3Id == parms.Menu.ProductCat3Id);
            if (pcm.IsNull())
                return null;

            return pcm.Products.ToList();
        }



        #endregion


    }
}
