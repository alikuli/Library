using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UowLibrary;
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PageViewNS;
using UowLibrary.PlayersNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class ProductIdentifiersController : EntityAbstractController<ProductIdentifier>
    {
        ProductBiz _productBiz;

        public ProductIdentifiersController(ProductBiz biz, BreadCrumbManager bcm, IErrorSet err, PageViewBiz pageViewBiz)
            : base(biz.ProductIdentifierBiz, bcm, err, pageViewBiz) 
        {
            _productBiz = biz;
        }


        public override System.Web.Mvc.ActionResult Event_CreateViewAndSetupSelectList(ModelsClassLibrary.ModelsNS.SharedNS.ControllerIndexParams parm)
        {

            ViewBag.ProductSelectList = _productBiz.SelectList();
            return base.Event_CreateViewAndSetupSelectList(parm);
        }

    }
}
