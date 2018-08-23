using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using System.Linq;


namespace UowLibrary.FileDocNS
{
    public partial class FileDocBiz : BusinessLayer<FileDoc>
    {

        public override ICommonWithId Factory()
        {
            ICommonWithId icommonwithid = base.Factory();
            FileDoc fileDoc = icommonwithid as FileDoc;

            fileDoc.IsNullThrowException();
            fileDoc.FileNumber = GetNextFileNumber();


            return icommonwithid;

        }
        private long GetNextFileNumber()
        {
            var fileDocs = FindAll().Where(x => x.UserId == UserId).ToList();

            long nextFileNumber = 0;

            if (fileDocs.IsNullOrEmpty())
                nextFileNumber = 1;
            else
                nextFileNumber = fileDocs.Max(x => x.FileNumber) + 1;

            return nextFileNumber;
        }

    }
}
