using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Linq;
using System.Reflection;

namespace UowLibrary.FileDocNS
{
    public partial class FileDocBiz : BusinessLayer<FileDoc>
    {

        public override void Fix(ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            parm.Entity.Name = parm.Entity.Name.ToTitleCase();

            
            AttachUser(parm.Entity as FileDoc);
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

        private void AttachUser(FileDoc entity)
        {
            UserId.IsNullOrWhiteSpaceThrowException(); 
            entity.User =  UserBiz.Find(UserId);

            if(entity.User.IsNull())
            {
                ErrorsGlobal.Add("User not found!", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }
        }


    }
}
