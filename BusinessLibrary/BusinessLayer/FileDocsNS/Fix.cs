using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace UowLibrary.FileDocNS
{
    public partial class FileDocBiz : BusinessLayer<FileDoc>
    {

        public override void Fix(ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            parm.Entity.Name = parm.Entity.Name.ToTitleCase();

            FileDoc fileDoc = parm.Entity as FileDoc;
            fileDoc.IsNullThrowException("Unable to unbox file doc.");
            if(fileDoc.PersonId.IsNullOrWhiteSpace())
                attachPerson(fileDoc);

            addUploadFileLocation(parm);
        }

        private void addUploadFileLocation(ControllerCreateEditParameter parm)
        {
            //check if a Misc File is uploaded.
            if (parm.MiscUploadedFiles.IsNull())
                return;

            FileDoc fd = parm.Entity as FileDoc;
            parm.MiscUploadedFiles.FileLocationConst = fd.MiscFilesLocation_Initialization();

        }

        private void attachPerson(FileDoc entity)
        {
            UserId.IsNullOrWhiteSpaceThrowException();
            entity.Person = UserBiz.GetPersonFor(UserId);
            entity.Person.IsNullThrowException("No attached person to user");
            entity.PersonId = entity.Person.Id;
        }


    }
}
