using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.DeliverymanCategoryNS;

namespace MarketPlace.Web6.Controllers
{
    public class DeliverymanCategoriesController : EntityAbstractController<DeliverymanCategory>
    {

        DeliverymanCategoryBiz _customerCategoryBiz;

        public DeliverymanCategoriesController(DeliverymanCategoryBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _customerCategoryBiz = biz;
        }



    }
}