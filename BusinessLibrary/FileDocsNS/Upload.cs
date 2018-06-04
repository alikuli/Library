using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System;

namespace UowLibrary.FileDocNS
{
    public partial class FileDocBiz : BusinessLayer<FileDoc>
    {


        public override void AddEntityRecordIntoUpload(UploadedFile uploadFile, FileDoc filedoc, IUserHasUploadsTypeENUM iuserHasUploadsTypeEnum)
        {
            uploadFile.FileDoc = filedoc;
            uploadFile.FileDocId = filedoc.Id;
        }
    }
}
