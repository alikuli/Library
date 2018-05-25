using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.ProductNS

{
    public partial class ProductCat1Biz : BusinessLayer<ProductCategory1>
    {


        public override string SelectListCacheKey
        {
            get { return "ProductCat1SelectListData"; }
        }




    }
}
