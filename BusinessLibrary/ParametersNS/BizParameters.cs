using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UowLibrary.UploadFileNS;
using WebLibrary.Programs;
using ErrorHandlerLibrary;
using AliKuli.UtilitiesNS;
using BreadCrumbsLibraryNS.Programs;
using UowLibrary.PageViewNS;

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
                    :base(
                        memoryMain,
                        errorSet,
                        configManager,
                        breadCrumbManager,
                        pageViewBiz)
        {
            _uploadedFileBiz =  uploadedFileBiz;

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
