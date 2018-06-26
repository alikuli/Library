using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ViewModels;
using System.Web.Mvc;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.ProductNS.ProicuytVmNS
{
    public partial class ProductAutomobileBiz : BusinessLayer<ProductAutomobileVM>
    {


        readonly ProductBiz _productBiz;
        public ProductAutomobileBiz(IRepositry<ApplicationUser> userDal, IRepositry<ProductAutomobileVM> entityDal, IMemoryMain memoryMain, IErrorSet errorSet, ApplicationDbContext db, ConfigManagerHelper configManager, UploadedFileBiz uploadedFileBiz, ProductBiz productBiz)
            : base(userDal, memoryMain, errorSet, entityDal, db, configManager, uploadedFileBiz)
        {
            _productBiz = productBiz;
        }




        public override string SelectListCacheKey
        {
            get { return "ProductSelectListCache"; }
        }

        //private void makeNameAndSaveFields(ProductAutomobileVM entity)
        //{
        //    entity.SaveName();
        //    entity.saveNameFields();

        //}

        //public override void CreateEntity(ProductAutomobileVM entity)
        //{
        //    makeNameAndSaveFields(entity);
        //    Product product = entity as Product;
        //    _productBiz.CreateEntity(product);
        //}

        //public override void UpdateEntity(ProductAutomobileVM entity)
        //{
        //    makeNameAndSaveFields(entity);
        //    Product product = entity as Product;
        //    _productBiz.UpdateEntity(product);
        //}

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
