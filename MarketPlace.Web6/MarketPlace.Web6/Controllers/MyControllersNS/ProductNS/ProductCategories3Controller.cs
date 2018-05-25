using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class ProductCategories3Controller : EntityAbstractController<ProductCategory3>
    {

        ProductCat3Biz _productCat3Biz;
        #region Constructo and initializers

        public ProductCategories3Controller(ProductCat3Biz productCat3Biz, IErrorSet errorSet)
            : base(productCat3Biz, errorSet)
        {
            _productCat3Biz = productCat3Biz;
        }

        #endregion



        public async Task<ActionResult> DeleteUploadedFile(string productCatId, string uploadedFileId)
        {
            //delete from the ProductCategoryMain
            await _productCat3Biz.DeleteUploadedFile(uploadedFileId);
            return RedirectToAction("Edit", new { id = productCatId });
            //return RedirectToAction("DeleteConfirmed", "UploadedFiles", new { id = uploadedFileId });
        }
    }
}