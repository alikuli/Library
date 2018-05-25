using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class ProductCatMainController : EntityAbstractController<ProductCategoryMain>
    {

        ProductCatMainBiz _productCatMainBiz;

        #region Constructo and initializers

        public ProductCatMainController(ProductCatMainBiz productCatMainBiz, IErrorSet errorSet)
            : base(productCatMainBiz, errorSet)
        {
            _productCatMainBiz = productCatMainBiz;
        }

        #endregion



        public async Task<ActionResult> DeleteUploadedFile(string productCatId, string uploadedFileId)
        {
            //delete from the productCategory1
            await _productCatMainBiz.DeleteUploadedFile(uploadedFileId);
            return RedirectToAction("Edit", new { id = productCatId });
            //return RedirectToAction("DeleteConfirmed", "UploadedFiles", new { id = uploadedFileId });
        }
        public override System.Web.Mvc.ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            ViewBag.ProductCategory1SelectList = _productCatMainBiz.ProductCat1_SelectList();
            ViewBag.ProductCategory2SelectList = _productCatMainBiz.ProductCat2_SelectList();
            ViewBag.ProductCategory3SelectList = _productCatMainBiz.ProductCat3_SelectList();

            return base.Event_CreateViewAndSetupSelectList(parm);
        }
    }
}
