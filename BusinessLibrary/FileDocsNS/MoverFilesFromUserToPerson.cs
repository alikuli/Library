using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UserModels;

namespace UowLibrary.FileDocNS
{
    public partial class FileDocBiz : BusinessLayer<FileDoc>
    {

        public void MoveFilesFromUserToPerson()
        {

            //get all the files for the User
            List<FileDoc> lstFileDocs = FindAll()
                .ToList();

            if (lstFileDocs.IsNullOrEmpty())
                return;

            foreach (FileDoc fileInUser in lstFileDocs)
            {
                if (fileInUser.UserId.IsNullOrWhiteSpace())
                    continue;

                fileInUser.User.IsNullThrowException("No user loaded. Programming error.");
                //fileInUser.User.PersonId.IsNullOrWhiteSpaceThrowException(string.Format("User {0} does not have a person.",fileInUser.User.UserName));
                if (fileInUser.User.PersonId.IsNullOrWhiteSpace())
                    continue;

                fileInUser.PersonId = fileInUser.User.PersonId;
                Update(fileInUser);
            }
            SaveChanges();
        }

    }
}
