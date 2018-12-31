using AliKuli.UtilitiesNS;
using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary;
using UowLibrary.PageViewNS;
using UowLibrary.SuperLayerNS.AccountsNS;
using UowLibrary.UploadFileNS;
using WebLibrary.Programs;

namespace UowLibrary.ParametersNS
{
    public class BizParameters : AbstractControllerParameters
    {

        UploadedFileBiz _uploadedFileBiz;

        public BizParameters(
            UploadedFileBiz uploadedFileBiz,
            IMemoryMain memoryMain,
            IErrorSet errorSet,
            ConfigManagerHelper configManager,
            BreadCrumbManager breadCrumbManager,
            PageViewBiz pageViewBiz)
            : base(
                memoryMain,
                errorSet,
                configManager,
                breadCrumbManager,
                pageViewBiz)
        {
            _uploadedFileBiz = uploadedFileBiz;


        }

        public UploadedFileBiz UploadedFileBiz
        {
            get
            {
                return _uploadedFileBiz;
            }
        }








    }
}
