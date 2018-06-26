using AliKuli.Extentions;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ViewModels;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.MenuNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class ProductsController : EntityAbstractController<Product>
    {

        ProductBiz _productBiz;
        MenuPath1Biz _menuPath1Biz;
        public ProductsController(ProductBiz productBiz, IErrorSet errorSet, UserBiz userbiz, MenuPath1Biz menuPath1Biz)
            : base(productBiz, errorSet, userbiz)
        {
            _productBiz = productBiz;
            _menuPath1Biz = menuPath1Biz;
        }

        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            loadSelectLists(parm);
            _productBiz.LoadMenuPathCheckedBoxes(parm.Entity as IProduct);
            return base.Event_CreateViewAndSetupSelectList(parm);

        }



        public override RedirectToRouteResult Event_UpdateCreateRedicrectToAction(ControllerCreateEditParameter parm)
        {
            return base.Event_UpdateCreateRedicrectToAction(parm);
        }

        private void loadSelectLists(ControllerIndexParams parm)
        {
            ViewBag.ParentSelectList = _productBiz.SelectList_ForParent(parm.Entity);
            ViewBag.UomPurchaseSelectList = _productBiz.SelectList_UomPurchaseQty();
            ViewBag.UomVolumeSelectList = _productBiz.SelectList_UomVolume();
            ViewBag.UomShipWtSelectList = _productBiz.SelectList_UomWeight();
            ViewBag.UomWeightSelectList = _productBiz.SelectList_UomWeight();
            ViewBag.UomLengthSelectList = _productBiz.SelectList_UomLength();
            //ViewBag.GearTypeSelectList = _productBiz.SelectList_AutomobileGearTypeEnum();
            //ViewBag.FuelTypeSelectList = _productBiz.SelectList_FuelTypeEnum();
        }

    }
}
