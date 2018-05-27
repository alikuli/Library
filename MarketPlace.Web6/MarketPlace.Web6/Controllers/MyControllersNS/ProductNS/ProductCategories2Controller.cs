using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class ProductCategories2Controller : EntityAbstractController<ProductCategory2>
    {

        ProductCat2Biz _productCat2Biz;
        #region Constructo and initializers

        public ProductCategories2Controller(ProductCat2Biz productCat2Biz, IErrorSet errorSet)
            : base(productCat2Biz, errorSet)
        {
            _productCat2Biz = productCat2Biz;
        }

        #endregion



        public async Task<ActionResult> DeleteUploadedFile(string productCatId, string uploadedFileId)
        {
            //delete from the productCategory2
            await _productCat2Biz.DeleteUploadedFile(productCatId, uploadedFileId);
            return RedirectToAction("Edit", new { id = productCatId });
            //return RedirectToAction("DeleteConfirmed", "UploadedFiles", new { id = uploadedFileId });
        }
    }
}