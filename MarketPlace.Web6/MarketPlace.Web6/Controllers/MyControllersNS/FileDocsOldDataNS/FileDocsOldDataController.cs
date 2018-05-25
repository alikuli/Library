using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FileDocsNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using UowLibrary.CountryNS;

namespace MarketPlace.Web6.Controllers
{
    public class FileDocsOldDataController : EntityAbstractController<OldFileData>
    {

        FileDocOldDataBiz _fileDocOldDataBiz;
        #region Constructo and initializers

        public FileDocsOldDataController(FileDocOldDataBiz FileDocOldDataBiz, IErrorSet errorSet)
            : base(FileDocOldDataBiz, errorSet)
        {
            _fileDocOldDataBiz = FileDocOldDataBiz;
        }

        #endregion


    }
}