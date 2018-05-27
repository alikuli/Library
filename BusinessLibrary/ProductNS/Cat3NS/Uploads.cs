using AliKuli.ConstantsNS;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Threading.Tasks;

namespace UowLibrary.ProductNS
{
    public partial class ProductCat3Biz 
    {
        //private readonly IRepositry<UploadedFile> _uploadFileDAL;

        /// <summary>
        /// This is where all the uploaded Files will be saved
        /// </summary>
        /// <returns></returns>
        //public override string Event_SaveLocationForUploadedFiles()
        //{
        //    return MyConstants.SAVE_LOCATION_PRODUCT_CATEGORY3;
        //}

        public override void AddEntityRecordIntoUpload(UploadedFile uploadFile, ProductCategory3 entity, IUserHasUploadsTypeENUM iuserHasUploadsTypeEnum)
        {
            uploadFile.ProductCategory3Id = entity.Id;
            uploadFile.ProductCategory3 = entity;
        }
        //public override void Event_AddUploadedFileInfoIntoDb(IHasUploads entity, UploadObject uploadObj)
        //{
        //    if (uploadObj.NumberOfFiles > 0)
        //    {
        //        foreach (var file in uploadObj.FilesSavedList)
        //        {

        //            file.MetaData.Created.SetToTodaysDate("");

        //            file.ProductCategory3 = entity as ProductCategory3;
        //            file.ProductCategory3Id = entity.Id;

        //            entity.UploadedFiles.Add(file);
        //            _uploadedFileBiz.Create(file);

        //        }
        //    }
        //}

        /// <summary>
        /// This deletes/Removes the uploaded file.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uploadedObjectId"></param>

        public async Task DeleteUploadedFile(string uploadedObjectId)
        {

            //Delete the upload
            await _uploadedFileBiz.DeleteAsync(uploadedObjectId);

            //Note. this just marks delete as true.




            //first locate the object
            //ProductCategory3 pc3 = await FindAsync(id);

            //if (pc3.IsNull())
            //{
            //    ErrorsGlobal.Add("Unable to locate the Product Category", MethodBase.GetCurrentMethod());
            //    return;
            //}

            ////remove the object from Category 3
            //UploadedFile upf = pc3.UploadedFiles.FirstOrDefault(x => x.Id == uploadedObjectId);

            //if (!upf.IsNull())
            //{
            //    pc3.UploadedFiles.Remove(upf);

            //}
        }


    }
}
