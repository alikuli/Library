using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.ParametersNS;
using UowLibrary.PageViewNS;
using UowLibrary.PlayersNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class MenuPath3sController : EntityAbstractController<MenuPath3>
    {

        MenuPath3Biz _menupath3Biz;
        #region Constructo and initializers

        public MenuPath3sController(MenuPath3Biz biz, AbstractControllerParameters param)
            : base(biz, param) 
        {
            _menupath3Biz = biz;
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