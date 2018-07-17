using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class MenuPath2sController : EntityAbstractController<MenuPath2>
    {

        MenuPath2Biz _menupath2Biz;
        #region Constructo and initializers

        public MenuPath2sController(MenuPath2Biz menupath2Biz, IErrorSet errorSet, UserBiz userbiz, BreadCrumbManager breadCrumbManager)
            : base(menupath2Biz, errorSet, userbiz, breadCrumbManager)
        {
            _menupath2Biz = menupath2Biz;
        }

        #endregion



        public async Task<ActionResult> DeleteUploadedFile(string menuPathId, string uploadedFileId)
        {
            //delete from the productCategory2
            await _menupath2Biz.DeleteUploadedFile(menuPathId, uploadedFileId);
            return RedirectToAction("Edit", new { id = menuPathId });
            //return RedirectToAction("DeleteConfirmed", "UploadedFiles", new { id = uploadedFileId });
        }
    }
}