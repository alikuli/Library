using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using AliKuli.Extentions;
using System.Collections.Generic;

namespace UowLibrary.ProductChildNS
{
    public partial class ProductChildBiz
    {
        public override void AddEntityRecordIntoUpload(UploadedFile uploadedFile, ProductChild entity, IUserHasUploadsTypeENUM iuserHasUploadsTypeEnum)
        {
            ///    uploadFile.FileDoc = filedoc;
            ///    uploadFile.Id = filedoc.Id;

            uploadedFile.ProductChild = entity;
            uploadedFile.ProductChildId = entity.Id;
            if (entity.MiscFiles.IsNull())
                entity.MiscFiles = new List<UploadedFile>();
            entity.MiscFiles.Add(uploadedFile);
        }


    }
}
