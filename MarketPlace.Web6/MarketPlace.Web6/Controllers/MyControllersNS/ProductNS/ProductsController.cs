using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.ProductNS;
using UowLibrary;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class ProductsController : EntityAbstractController<Product>
    {


        public ProductsController(ProductBiz productBiz, IErrorSet errorSet)
            : base(productBiz, errorSet)
        {
        }

    }
}
