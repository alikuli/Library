using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.ProductNS

{
    public partial class ProductCat2Biz : BusinessLayer<ProductCategory2>
    {


        public override string SelectListCacheKey
        {
            get { return "ProductCat2SelectListData"; }
        }




    }
}
