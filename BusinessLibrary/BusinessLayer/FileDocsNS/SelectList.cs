using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;

namespace UowLibrary.FileDocNS
{
    public partial class FileDocBiz : BusinessLayer<FileDoc>
    {
        public override string SelectListCacheKey
        {
            get { return "FileDocsSelectListData"; }
        }

    }
}
