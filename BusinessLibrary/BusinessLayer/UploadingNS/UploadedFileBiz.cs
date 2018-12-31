using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.RightsNS;
using UowLibrary.PageViewNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.UploadFileNS
{
    public partial class UploadedFileBiz : BusinessLayer<UploadedFile>
    {

        public UploadedFileBiz(IRepositry<UploadedFile> entityDal, IMemoryMain memoryMain, IErrorSet errorSet, ConfigManagerHelper configManagerHelper, BreadCrumbManager breadCrumbManager)
            : base(entityDal, null, memoryMain, null, errorSet, configManagerHelper, breadCrumbManager)
        {
        }






    }
}
