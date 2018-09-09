using AliKuli.Extentions;
using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary;
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
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PageViewNS;
using UowLibrary.PlayersNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class ProductsController : EntityAbstractController<Product>
    {

        ProductBiz _productBiz;
        public ProductsController(ProductBiz biz, BreadCrumbManager bcm, IErrorSet err, PageViewBiz pageViewBiz)
            : base(biz, bcm, err, pageViewBiz) 
        {
            _productBiz = biz;
        }

        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            loadSelectLists(parm);
            _productBiz.LoadMenuPathCheckedBoxes(parm.Entity as IProduct);
            return base.Event_CreateViewAndSetupSelectList(parm);

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
