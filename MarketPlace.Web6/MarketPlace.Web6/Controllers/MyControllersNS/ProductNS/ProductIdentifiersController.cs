using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.ProductNS;
using UowLibrary;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class ProductIdentifiersController : EntityAbstractController<ProductIdentifier>
    {
        ProductBiz _productBiz;

        public ProductIdentifiersController(ProductIdentifierBiz productIdentifierBiz, IErrorSet errorSet, ProductBiz productBiz)
            : base(productIdentifierBiz, errorSet)
        {
            _productBiz = productBiz;
        }


        public override System.Web.Mvc.ActionResult Event_CreateViewAndSetupSelectList(ModelsClassLibrary.ModelsNS.SharedNS.ControllerIndexParams parm)
        {

            ViewBag.ProductSelectList = _productBiz.SelectList();
            return base.Event_CreateViewAndSetupSelectList(parm);
        }

    }
}
