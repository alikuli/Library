using AliKuli.Extentions;
using BreadCrumbsLibraryNS.Programs;
using EnumLibrary.EnumNS;
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
using UowLibrary.Interface;
using UowLibrary.MenuNS;
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PlayersNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class MenuPath1sController : EntityAbstractController<MenuPath1>
    {

        MenuPath1Biz _menupath1Biz;

        public MenuPath1sController(MenuPath1Biz biz, BreadCrumbManager bcm, IErrorSet err)
            : base(biz, bcm, err) 
        {
            _menupath1Biz = biz;
        }




        public async Task<ActionResult> DeleteUploadedFile(string menupathId, string uploadedFileId)
        {
            //delete from the productCategory1
            await _menupath1Biz.DeleteUploadedFile(menupathId, uploadedFileId);
            return RedirectToAction("Edit", new { id = menupathId });
        }


    
    
    }
}
