using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using UowLibrary;
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PageViewNS;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;

namespace MarketPlace.Web6.Controllers
{
    public class UploadedFilesController : EntityAbstractController<UploadedFile>
    {

        UploadedFileBiz _uploadedfilesBiz;
        #region Constructo and initializers

        public UploadedFilesController(UploadedFileBiz biz, BreadCrumbManager bcm, IErrorSet err, PageViewBiz pageViewBiz)
            : base(biz, bcm, err, pageViewBiz) 
        {
            _uploadedfilesBiz = biz;
        }

        #endregion


    }
}