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
    public class MenuPath3sController : EntityAbstractController<MenuPath3>
    {

        MenuPath3Biz _menupath3Biz;
        #region Constructo and initializers

        public MenuPath3sController(MenuPath3Biz menupath3Biz, IErrorSet errorSet)
            : base(menupath3Biz, errorSet)
        {
            _menupath3Biz = menupath3Biz;
        }

        #endregion



        public async Task<ActionResult> DeleteUploadedFile(string menupathId, string uploadedFileId)
        {
            //delete from the ProductCategoryMain
            await _menupath3Biz.DeleteUploadedFile(uploadedFileId);
            return RedirectToAction("Edit", new { id = menupathId });
            //return RedirectToAction("DeleteConfirmed", "UploadedFiles", new { id = uploadedFileId });
        }
    }
}