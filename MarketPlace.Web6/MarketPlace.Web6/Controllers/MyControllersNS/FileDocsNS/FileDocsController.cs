using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using System.Web.Mvc;
using UowLibrary.FileDocNS;
using UowLibrary.ParametersNS;

namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public partial class FileDocsController : EntityAbstractController<FileDoc>
    {
        FileDocBiz _fileDocBiz;

        public FileDocsController(FileDocBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _fileDocBiz = biz;

        }


        //public ActionResult MoveFilesFromUserToPerson()
        //{
        //    try
        //    {
        //        _fileDocBiz.MoveFilesFromUserToPerson();
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception e)
        //    {
        //        ErrorsGlobal.Add("Files not moved!", MethodBase.GetCurrentMethod(), e);
        //        throw;
        //    }
        //}
        public FileDocBiz FileDocBiz
        {
            get
            {
                return _fileDocBiz;
            }
        }


    }
}