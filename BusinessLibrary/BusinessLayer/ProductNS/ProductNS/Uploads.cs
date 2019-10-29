using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using AliKuli.Extentions;
using System.Collections.Generic;


namespace UowLibrary.ProductNS
{
    public partial class ProductBiz
    {
        //public override void AddEntityRecordIntoUpload(UploadedFile uploadedFile, Product entity, IUserHasUploadsTypeENUM iuserHasUploadsTypeEnum)
        //{
        //    ///    uploadFile.FileDoc = filedoc;
        //    ///    uploadFile.Id = filedoc.Id;

        //    uploadedFile.Product = entity;
        //    uploadedFile.ProductId = entity.Id;
        //}
        public override void AddEntityRecordIntoUpload(UploadedFile uploadedFile, Product entity, IUserHasUploadsTypeENUM iuserHasUploadsTypeEnum)
        {
            //uploadedFile.Product = entity;
            uploadedFile.ProductId = entity.Id;

            if (entity.MiscFiles.IsNull())
                entity.MiscFiles = new List<UploadedFile>();

            entity.MiscFiles.Add(uploadedFile);
        }

    }
}
