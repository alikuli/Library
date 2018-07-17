using BreadCrumbsLibraryNS.Programs;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class MenuPathMainsController : EntityAbstractController<MenuPathMain>
    {

        MenuPathMainBiz _menupathmainBiz;

        #region Construction and initializers

        public MenuPathMainsController(MenuPathMainBiz menuPathMainBiz, IErrorSet errorSet, UserBiz userbiz, BreadCrumbManager breadCrumbManager)
            : base(menuPathMainBiz, errorSet, userbiz, breadCrumbManager)
        {
            _menupathmainBiz = menuPathMainBiz;
        }

        #endregion



        //public async Task<ActionResult> DeleteUploadedFile(string menupathId, string uploadedFileId)
        //{
        //    //delete from the productCategory1
        //    await _menupathmainBiz.DeleteUploadedFile(uploadedFileId);
        //    return RedirectToAction("Edit", new { id = menupathId });
        //    //return RedirectToAction("DeleteConfirmed", "UploadedFiles", new { id = uploadedFileId });
        //}
        public override System.Web.Mvc.ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            ViewBag.MenuPath1SelectList = _menupathmainBiz.MenuPath1_SelectList();
            ViewBag.MenuPath2SelectList = _menupathmainBiz.MenuPath2_SelectList();
            ViewBag.MenuPath3SelectList = _menupathmainBiz.MenuPath3_SelectList();

            return base.Event_CreateViewAndSetupSelectList(parm);
        }
    }
}
