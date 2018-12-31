using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;


namespace UowLibrary.ProductChildNS
{
    public partial class ProductChildBiz
    {
        public override void AddEntityRecordIntoUpload (UploadedFile uploadedFile, ProductChild entity, IUserHasUploadsTypeENUM iuserHasUploadsTypeEnum)
        {
            ///    uploadFile.FileDoc = filedoc;
            ///    uploadFile.Id = filedoc.Id;

            uploadedFile.ProductChild = entity;
            uploadedFile.ProductChildId = entity.Id;
        }


    }
}
