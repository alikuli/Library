using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.ProductApproverCategoryNS;

namespace MarketPlace.Web6.Controllers
{
    public class ProductApproverCategoriesController : EntityAbstractController<ProductApproverCategory>
    {

        ProductApproverCategoryBiz _ProductApproverCategoryBiz;

        public ProductApproverCategoriesController(ProductApproverCategoryBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _ProductApproverCategoryBiz = biz;
        }

    }
}