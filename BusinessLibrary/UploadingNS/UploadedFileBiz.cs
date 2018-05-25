using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.UploadFileNS
{
    public partial class UploadedFileBiz : BusinessLayer<UploadedFile>
    {

        public UploadedFileBiz(IRepositry<ApplicationUser> userDal, IRepositry<UploadedFile> entityDal, IMemoryMain memoryMain, IErrorSet errorSet, ApplicationDbContext db, ConfigManagerHelper configManager)
            : base(userDal, memoryMain, errorSet, entityDal, db, configManager, null)
        {

        }






    }
}
