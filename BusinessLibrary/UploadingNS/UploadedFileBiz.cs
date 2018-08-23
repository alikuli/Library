using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.RightsNS;
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PlayersNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.UploadFileNS
{
    public partial class UploadedFileBiz : BusinessLayer<UploadedFile>
    {

        public UploadedFileBiz(IRepositry<UploadedFile> entityDal, MyWorkClasses myWorkClasses,  BreadCrumbManager breadCrumbManager)
            : base(myWorkClasses, entityDal, null, breadCrumbManager)
        {
            _uploadedFileBiz = this as UploadedFileBiz;
        }






    }
}
