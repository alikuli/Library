using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.CustomerCategoryNS;

namespace MarketPlace.Web6.Controllers
{
    public class CustomerCategoriesController : EntityAbstractController<CustomerCategory>
    {

        CustomerCategoryBiz _customerCategoryBiz;

        public CustomerCategoriesController(CustomerCategoryBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _customerCategoryBiz = biz;
        }



    }
}