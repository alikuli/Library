using ModelsClassLibrary.ModelsNS.DocumentsNS.FileDocsNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;

namespace UowLibrary.CountryNS
{
    public partial class FileDocOldDataBiz : BusinessLayer<OldFileData>
    {
        public override string SelectListCacheKey
        {
            get { return "OldDataFileDocsSelectListData"; }
        }

    }
}
