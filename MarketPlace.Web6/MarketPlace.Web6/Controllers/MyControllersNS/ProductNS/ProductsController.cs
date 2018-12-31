using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;
using UowLibrary.ParametersNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class ProductsController : EntityAbstractController<Product>
    {

        ProductBiz _productBiz;
        public ProductsController(ProductBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _productBiz = biz;
        }

        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            Product product = parm.Entity as Product;
            product.IsNullThrowException("Unable to unbox");

            _productBiz.LoadMenuPathCheckedBoxes(parm);
            _productBiz.FixProductFeatures(product);

            //product.ParentSelectList = _productBiz.SelectList_ForParent(parm.Entity);
            product.SelectListUomPurchase = _productBiz.SelectList_UomPurchaseQty();
            product.SelectListUomVolume = _productBiz.SelectList_UomVolume();
            product.SelectListUomShipWeight = _productBiz.SelectList_UomWeight();
            product.SelectListUomWeight = _productBiz.SelectList_UomWeight();
            product.SelectListUomLength = _productBiz.SelectList_UomLength();
            product.SelectListUomDimensionsLength = _productBiz.SelectList_UomLength();

            if (!parm.ReturnUrl.IsNullOrWhiteSpace())
            {
                product.MenuManager.ReturnUrl = parm.ReturnUrl;
            }

            return base.Event_CreateViewAndSetupSelectList(parm);

        }





    }
}
