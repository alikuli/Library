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
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;
using AliKuli.Extentions;

namespace UowLibrary.FileDocNS
{
    public partial class FileDocBiz : BusinessLayer<FileDoc>
    {
        PersonBiz _personBiz;
        public FileDocBiz(IRepositry<FileDoc> entityDal, PersonBiz personBiz, AbstractControllerParameters myWorkClasses, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {
            _personBiz = personBiz;
        }


        UserBiz UserBiz
        {
            get
            {
                return PersonBiz.UserBiz;
            }
        }

        PersonBiz PersonBiz
        {
            get
            {
                _personBiz.IsNullThrowException("PersonBiz");
                _personBiz.UserId = UserId;
                _personBiz.UserName = UserName;
                return _personBiz;
            }
        }
    }
}
