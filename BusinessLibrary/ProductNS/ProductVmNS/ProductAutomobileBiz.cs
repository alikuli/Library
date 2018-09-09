using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ViewModels;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;

namespace UowLibrary.ProductNS.ProicuytVmNS
{
    public partial class ProductAutomobileBiz : BusinessLayer<ProductAutomobileVM>
    {


        readonly ProductBiz _productBiz;
        public ProductAutomobileBiz(ProductBiz productBiz, IRepositry<ProductAutomobileVM> entityDal, MyWorkClasses myWorkClasses, UploadedFileBiz uploadedFileBiz, BreadCrumbManager breadCrumbManager)
            : base(myWorkClasses, entityDal, uploadedFileBiz, breadCrumbManager)
        {
            _productBiz = productBiz;
        }




        public override string SelectListCacheKey
        {
            get { return "ProductSelectListCache"; }
        }

        public SelectList SelectList_ForParent(ICommonWithId entity)
        {
            return _productBiz.SelectList_ForParent(entity);

        }



        public SelectList SelectList_UomPurchaseQty()
        {
            return _productBiz.SelectList_UomPurchaseQty();
        }


        public SelectList SelectList_UomVolume()
        {
            return _productBiz.SelectList_UomVolume();
        }


        public SelectList SelectList_UomShipWeight()
        {
            return _productBiz.SelectList_UomShipWeight();
        }


        public SelectList SelectList_UomWeight()
        {
            return _productBiz.SelectList_UomWeight();
        }


        public SelectList SelectList_UomLength()
        {
            return _productBiz.SelectList_UomLength();
        }


    }
}
