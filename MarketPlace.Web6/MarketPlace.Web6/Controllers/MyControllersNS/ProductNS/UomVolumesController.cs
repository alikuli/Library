using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.ProductNS;
using UowLibrary;

namespace MarketPlace.Web6.Controllers
{
    public class UomVolumesController : EntityAbstractController<UomVolume>
    {


        public UomVolumesController(UomVolumeBiz uomVolumeBiz, IErrorSet errorSet, UserBiz userbiz, BreadCrumbManager breadCrumbManager)
            : base(uomVolumeBiz, errorSet, userbiz, breadCrumbManager)
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
