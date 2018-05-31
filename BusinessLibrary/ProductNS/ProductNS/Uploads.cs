using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;


namespace UowLibrary.ProductNS
{
    public partial class ProductBiz
    {
        public override void AddEntityRecordIntoUpload(UploadedFile uploadedFile, Product entity, IUserHasUploadsTypeENUM iuserHasUploadsTypeEnum)
        {
            ///    uploadFile.FileDoc = filedoc;
            ///    uploadFile.Id = filedoc.Id;

            uploadedFile.Product = entity;
            uploadedFile.ProductId = entity.Id;
        }


    }
}
