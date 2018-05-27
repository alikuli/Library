using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary.MenuNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class MenuPath1sController : EntityAbstractController<MenuPath1>
    {

        MenuPath1Biz _menupath1Biz;
        #region Constructo and initializers

        public MenuPath1sController(MenuPath1Biz menupath1Biz, IErrorSet errorSet)
            : base(menupath1Biz, errorSet)
        {
            _menupath1Biz = menupath1Biz;
        }

        #endregion



        public async Task<ActionResult> DeleteUploadedFile(string menupathId, string uploadedFileId)
        {
            //delete from the productCategory1
            await _menupath1Biz.DeleteUploadedFile(menupathId, uploadedFileId);
            return RedirectToAction("Edit", new { id = menupathId });
            //return RedirectToAction("DeleteConfirmed", "UploadedFiles", new { id = uploadedFileId });
        }
    }
}
