using InterfacesLibrary.SharedNS;
using MarketPlace.Web4.Controllers;

namespace MarketPlace.Web6.Controllers
{
    public partial class ProductAutomobileVMsController : AbstractController
    {



        private void loadSelectLists(ICommonWithId icommonwithid)
        {
            ViewBag.ParentSelectList = _productBiz.SelectList_ForParent(icommonwithid);
            ViewBag.UomPurchaseSelectList = _productBiz.SelectList_UomPurchaseQty();
            ViewBag.UomVolumeSelectList = _productBiz.SelectList_UomVolume();
            ViewBag.UomShipWtSelectList = _productBiz.SelectList_UomWeight();
            ViewBag.UomWeightSelectList = _productBiz.SelectList_UomWeight();
            ViewBag.UomLengthSelectList = _productBiz.SelectList_UomLength();
            ViewBag.GearTypeSelectList = _productBiz.SelectList_AutomobileGearTypeEnum();
            ViewBag.FuelTypeSelectList = _productBiz.SelectList_FuelTypeEnum();
        }



    }
}
