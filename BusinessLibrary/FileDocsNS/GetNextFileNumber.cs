using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using System.Linq;

namespace UowLibrary.FileDocNS
{
    public partial class FileDocBiz : BusinessLayer<FileDoc>
    {

        private long GetNextFileNumber(FileDoc entity)
        {
            string userId = GetUserIdOrThrowErrorIfNull();
            var allDataForuser = FindAllNoTracking().Where(x => x.UserId == userId).ToList();

            long nextFileNumber = 0;

            if (allDataForuser.IsNullOrEmpty())
                nextFileNumber = 1;
            else
                nextFileNumber = allDataForuser.Max(x => x.FileNumber) + 1;

            return nextFileNumber;
        }

    }
}
