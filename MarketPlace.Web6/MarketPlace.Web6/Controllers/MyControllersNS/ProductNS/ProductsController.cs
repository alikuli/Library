using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.ProductNS;
using UowLibrary;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class ProductsController : EntityAbstractController<Product>
    {

        ProductBiz _productBiz;
        public ProductsController(ProductBiz productBiz, IErrorSet errorSet, UserBiz userbiz)
            : base(productBiz, errorSet, userbiz)
        {
            _productBiz = productBiz;
        }

        public override System.Web.Mvc.ActionResult Event_CreateViewAndSetupSelectList(ModelsClassLibrary.ModelsNS.SharedNS.ControllerIndexParams parm)
        {
            ViewBag.ParentSelectList = _productBiz.SelectList_ForParent(parm.Entity);
            ViewBag.UomPurchaseSelectList = _productBiz.UomQuantityBiz.SelectList();
            ViewBag.UomVolumeSelectList = _productBiz.UomVolumeBiz.SelectList();
            ViewBag.UomShipWtSelectList = _productBiz.UomWeightBiz.SelectList();
            ViewBag.UomWeightSelectList = _productBiz.UomWeightBiz.SelectList();
            ViewBag.UomLengthSelectList = _productBiz.UomLengthBiz.SelectList();

            return base.Event_CreateViewAndSetupSelectList(parm);

        }
    }
}
