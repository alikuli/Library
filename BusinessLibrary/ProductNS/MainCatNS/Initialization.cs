using AliKuli.Extentions;
using CountryDATA.ProductNS.CategoryNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;

namespace UowLibrary.ProductNS
{
    public partial class ProductCatMainBiz : BusinessLayer<ProductCategoryMain>
    {

        public override bool Event_LockEditDuringInitialization()
        {
            return false;
        }


        //public override string[] GetDataForStringArrayFormat
        //{
        //    get
        //    {
        //        return ProductCategoryMainArray.DataArray();
        //    }
        //}


        public override void AddInitData()
        {
            //get the data
            List<ProductCatMainHelper> dataList = new DatastoreNS.ProductCatMainInitilizingDataList().DataList();

            if (!dataList.IsNullOrEmpty())
            {
                foreach (var item in dataList)
                {

                    if (item.Cat1.IsNullOrWhiteSpace())
                    {
                        ErrorsGlobal.Add(string.Format("Category 1 '{0}' not found", item.Cat1),MethodBase.GetCurrentMethod());
                        throw new Exception(ErrorsGlobal.ToString());
                    }

                    ProductCategory3 cat3 = _productCat3Biz.FindByName(item.Cat3);
                    ProductCategoryMain pcm = new ProductCategoryMain();
                    
                    addCat1(item, pcm);
                    addCat2(item, cat3, pcm);
                    Create(pcm);
                }
                

            }
                SaveChanges();
        }

        private void addCat2(ProductCatMainHelper item, ProductCategory3 cat3, ProductCategoryMain pcm)
        {
            //cat2 starts here
            ProductCategory2 cat2 = _productCat2Biz.FindByName(item.Cat2);
            if (!item.Cat2.IsNullOrWhiteSpace())
            {
                if (cat2.IsNull())
                {
                    ErrorsGlobal.Add(string.Format("Category 2 '{0}' not found", item.Cat2), MethodBase.GetCurrentMethod());
                    throw new Exception(ErrorsGlobal.ToString());
                }

                pcm.ProductCat2 = cat2;
                pcm.ProductCat2Id = cat2.Id;
                if (cat2.ProductCategoryMains.IsNullOrEmpty())
                {
                    cat2.ProductCategoryMains = new List<ProductCategoryMain>();
                }
                cat2.ProductCategoryMains.Add(pcm);

                addCat3(item, cat3, pcm);
            }
        }

        private  void addCat3(ProductCatMainHelper item, ProductCategory3 cat3, ProductCategoryMain pcm)
        {
            //Cat3 starts here....
            if (!item.Cat3.IsNullOrWhiteSpace())
            {
                if (cat3.IsNull())
                {
                    ErrorsGlobal.Add(string.Format("Category 3 '{0}' not found", item.Cat3), MethodBase.GetCurrentMethod());
                    throw new Exception(ErrorsGlobal.ToString());
                    throw new Exception(string.Format("Category 3 '{0}' not found", item.Cat3));
                }
                pcm.ProductCat3 = cat3;
                pcm.ProductCat3Id = cat3.Id;
                if (cat3.ProductCategoryMains.IsNullOrEmpty())
                {
                    cat3.ProductCategoryMains = new List<ProductCategoryMain>();
                }
                cat3.ProductCategoryMains.Add(pcm);
            }
        }

        private void addCat1(ProductCatMainHelper item, ProductCategoryMain pcm)
            {
                                ProductCategory1 cat1 = _productCat1Biz.FindByName(item.Cat1);
                                pcm.ProductCat1 = cat1;
                                pcm.ProductCat1Id = cat1.Id;
                                if (cat1.ProductCategoryMains.IsNullOrEmpty())
                                {
                                    cat1.ProductCategoryMains = new List<ProductCategoryMain>();
                                }
                                cat1.ProductCategoryMains.Add(pcm);
            }
    }

}

