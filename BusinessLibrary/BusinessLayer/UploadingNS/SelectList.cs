using ModelsClassLibrary.ModelsNS.UploadedFileNS;

namespace UowLibrary.UploadFileNS
{
    public partial class UploadedFileBiz : BusinessLayer<UploadedFile>
    {


        public override string SelectListCacheKey
        {
            get { return "UploadedFileBizSelectListData"; }
        }


    }
}
