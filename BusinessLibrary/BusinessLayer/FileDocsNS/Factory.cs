using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System.Linq;
using UserModels;


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
            UserId.IsNullOrWhiteSpaceThrowException("UserId");
            Person person = PersonBiz.GetPersonForUserId(UserId);
            person.IsNullThrowException("person");
            string personId = person.Id;
            var fileDocs = FindAll().Where(x => x.PersonId == personId).ToList();

            long nextFileNumber = 0;

            if (fileDocs.IsNullOrEmpty())
                nextFileNumber = 1;
            else
                nextFileNumber = fileDocs.Max(x => x.FileNumber) + 1;

            return nextFileNumber;
        }

    }
}
