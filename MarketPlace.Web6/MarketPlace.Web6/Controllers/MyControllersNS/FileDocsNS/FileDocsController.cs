using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using System;
using System.Reflection;
using UowLibrary.FileDocNS;
using AliKuli.Extentions;
using UowLibrary;
using ModelsClassLibrary.ModelsNS.SharedNS;
using BreadCrumbsLibraryNS.Programs;
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PlayersNS;
using ErrorHandlerLibrary;

namespace MarketPlace.Web6.Controllers
{
    public partial class FileDocsController : EntityAbstractController<FileDoc>
    {
        FileDocBiz _fileDocBiz;

        public FileDocsController(FileDocBiz biz, BreadCrumbManager bcm, IErrorSet err)
            : base(biz, bcm, err) 
        {
            _fileDocBiz = biz;
            
        }


        public override System.Web.Mvc.RedirectToRouteResult Event_UpdateCreateRedicrectToAction(ControllerCreateEditParameter parm)
        {
//            return RedirectToAction("Index", new { selectedId = parm.Entity.Id.ToString(), sortBy = SortOrderENUM.Item2_Dsc });
            throw new NotImplementedException();

        }


        public FileDocBiz FileDocBiz
        {
            get
            {
                return _fileDocBiz;
            }
        }


    }
}