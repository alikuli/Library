using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class UomLengthsController : EntityAbstractController<UomLength>
    {

        UomLengthBiz _uomLengthBiz;
        #region Constructo and initializers

        public UomLengthsController(UomLengthBiz uomLengthBiz, IErrorSet errorSet, UserBiz userbiz, BreadCrumbManager breadCrumbManager)
            : base(uomLengthBiz, errorSet, userbiz, breadCrumbManager)
        {
            _uomLengthBiz = uomLengthBiz;
        }

        #endregion



        public async Task<ActionResult> DeleteUploadedFile(string productCatId, string uploadedFileId)
        {
            //delete from the productCategory1
            await _uomLengthBiz.DeleteUploadedFile(uploadedFileId);
            return RedirectToAction("Edit", new { id = productCatId });
            //return RedirectToAction("DeleteConfirmed", "UploadedFiles", new { id = uploadedFileId });
        }
    }
}
