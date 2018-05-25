using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class ProductCategories1Controller : EntityAbstractController<ProductCategory1>
    {

        ProductCat1Biz _productCat1Biz;
        #region Constructo and initializers

        public ProductCategories1Controller(ProductCat1Biz productCat1Biz, IErrorSet errorSet)
            : base(productCat1Biz, errorSet)
        {
            _productCat1Biz = productCat1Biz;
        }

        #endregion



        public async Task<ActionResult> DeleteUploadedFile(string productCatId, string uploadedFileId)
        {
            //delete from the productCategory1
            await _productCat1Biz.DeleteUploadedFile(productCatId, uploadedFileId);
            return RedirectToAction("Edit", new { id = productCatId });
            //return RedirectToAction("DeleteConfirmed", "UploadedFiles", new { id = uploadedFileId });
        }
    }
}
