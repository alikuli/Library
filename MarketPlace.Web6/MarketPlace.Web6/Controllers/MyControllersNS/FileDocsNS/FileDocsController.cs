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

namespace MarketPlace.Web6.Controllers
{
    public class FileDocsController : EntityAbstractController<FileDoc>
    {
        FileDocBiz _fileDocBiz;
        UserBiz _userBiz;

        public FileDocsController(FileDocBiz FileDocsBiz, IErrorSet errorSet, UserBiz userBiz, BreadCrumbManager breadCrumbManager)
            : base(FileDocsBiz, errorSet, userBiz, breadCrumbManager)
        {
            _fileDocBiz = FileDocsBiz;
            _userBiz = userBiz;
        }


        public override System.Web.Mvc.RedirectToRouteResult Event_UpdateCreateRedicrectToAction(ControllerCreateEditParameter parm)
        {
            return RedirectToAction("Index", new { selectedId = parm.Entity.Id.ToString(), sortBy = SortOrderENUM.Item2_Dsc });

        }

        //public override void Event_LoadUserIntoEntity(FileDoc entity)
        //{
        //    if (entity.UserId.IsNullOrWhiteSpace())
        //    {
        //        ErrorsGlobal.Add("The Owner is required. The Id is missing.", MethodBase.GetCurrentMethod());
        //        throw new Exception(ErrorsGlobal.ToString());
        //    }

        //    entity.User = _userBiz.Find(entity.UserId);

        //    if (entity.User.IsNull())
        //    {
        //        ErrorsGlobal.Add("The User was not found.", MethodBase.GetCurrentMethod());
        //        throw new Exception(ErrorsGlobal.ToString());
        //    }

        //}


    }
}