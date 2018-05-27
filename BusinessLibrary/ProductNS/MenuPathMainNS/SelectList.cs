using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ViewModels;
using System.Web.Mvc;

namespace UowLibrary.ProductNS

{
    public partial class ProductCatMainBiz 
    {


        public override string SelectListCacheKey
        {
            get { return "ProductCategoryMainSelectListData"; }
        }

        public SelectList ProductCat1_SelectList()
        {
            return _productCat1Biz.SelectList();
        }
        
        public SelectList ProductCat2_SelectList()
        {
            return _productCat2Biz.SelectList();
        }

        public SelectList ProductCat3_SelectList()
        {
            return _productCat3Biz.SelectList();
        }


    }
}
