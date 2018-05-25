using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.ProductNS;
using UowLibrary;

namespace MarketPlace.Web6.Controllers
{
    public class UomQuantitiesController : EntityAbstractController<UomQty>
    {


        public UomQuantitiesController(UomQuantityBiz uomQtyBiz, IErrorSet errorSet)
            : base(uomQtyBiz, errorSet)
        {
        }




    //    public async Task<ActionResult> DeleteUploadedFile(string productCatId, string uploadedFileId)
    //    {
    //        //delete from the productCategory1
    //        await _uomLengthBiz.DeleteUploadedFile(uploadedFileId);
    //        return RedirectToAction("Edit", new { id = productCatId });
    //        //return RedirectToAction("DeleteConfirmed", "UploadedFiles", new { id = uploadedFileId });
    //    }
    }
}
