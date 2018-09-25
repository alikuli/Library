using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.FileDocNS
{
    public partial class FileDocBiz : BusinessLayer<FileDoc>
    {
        UserBiz _userBiz;
        public FileDocBiz(IRepositry<FileDoc> entityDal, UserBiz userBiz, AbstractControllerParameters myWorkClasses, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {
            _userBiz = userBiz;
        }


        UserBiz UserBiz
        {
            get
            {
                return _userBiz;
            }
        }
    }
}
