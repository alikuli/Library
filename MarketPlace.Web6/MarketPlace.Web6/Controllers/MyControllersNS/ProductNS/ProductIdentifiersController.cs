using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UowLibrary;
using UowLibrary.ParametersNS;
using UowLibrary.PageViewNS;
using UowLibrary.PlayersNS;
using UowLibrary.ProductNS;
using System.Web.Mvc;

namespace MarketPlace.Web6.Controllers
{
    public class ProductIdentifiersController : EntityAbstractController<ProductIdentifier>
    {
        ProductBiz _productBiz;

        public ProductIdentifiersController(ProductBiz biz, AbstractControllerParameters param)
            : base(biz.ProductIdentifierBiz, param) 
        {
            _productBiz = biz;
        }


        public override System.Web.Mvc.ActionResult Event_Create_ViewAndSetupSelectList_GET(ModelsClassLibrary.ModelsNS.SharedNS.ControllerIndexParams parm)
        {

            ViewBag.ProductSelectList = _productBiz.SelectList();
            return base.Event_Create_ViewAndSetupSelectList_GET(parm);
        }

        public override ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            ViewBag.ProductSelectList = _productBiz.SelectList();
            return base.Event_Edit_ViewAndSetupSelectList_GET(parm);
        }

    }
}
