using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PlayersNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class UomLengthsController : EntityAbstractController<UomLength>
    {

        UomLengthBiz _uomLengthBiz;
        #region Constructo and initializers

        public UomLengthsController(UomLengthBiz biz, BreadCrumbManager bcm, IErrorSet err)
            : base(biz, bcm, err) 
        {
            _uomLengthBiz = biz;
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
