using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.ProductNS

{
    public partial class ProductCat3Biz : BusinessLayer<ProductCategory3>
    {


        public override string SelectListCacheKey
        {
            get { return "ProductCat3SelectListData"; }
        }




    }
}
