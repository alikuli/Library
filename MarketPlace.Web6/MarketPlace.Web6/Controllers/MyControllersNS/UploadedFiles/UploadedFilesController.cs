using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using UowLibrary.UploadFileNS;

namespace MarketPlace.Web6.Controllers
{
    public class UploadedFilesController : EntityAbstractController<UploadedFile>
    {

        UploadedFileBiz _uploadedfilesBiz;
        #region Constructo and initializers

        public UploadedFilesController(UploadedFileBiz UploadedFilesBiz, IErrorSet errorSet)
            : base(UploadedFilesBiz, errorSet)
        {
            _uploadedfilesBiz = UploadedFilesBiz;
        }

        #endregion


    }
}