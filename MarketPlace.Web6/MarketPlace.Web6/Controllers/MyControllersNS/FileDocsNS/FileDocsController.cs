using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using UowLibrary.FileDocNS;

namespace MarketPlace.Web6.Controllers
{
    public class FileDocsController : EntityAbstractController<FileDoc>
    {
        FileDocBiz _fileDocBiz;
        public FileDocsController(FileDocBiz FileDocsBiz, IErrorSet errorSet)
            : base(FileDocsBiz, errorSet)
        {
            _fileDocBiz = FileDocsBiz;
        }


        public override System.Web.Mvc.RedirectToRouteResult Event_UpdateCreateRedicrectToAction(FileDoc entity)
        {
            return RedirectToAction("Index", new { selectedId = entity.Id.ToString(), sortBy = SortOrderENUM.Item2_Dsc });

        }


    }
}