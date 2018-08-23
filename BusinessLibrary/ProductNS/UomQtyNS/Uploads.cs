using AliKuli.UtilitiesNS;
using InterfacesLibrary.SharedNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Threading.Tasks;

namespace UowLibrary
{
    public partial class UomQuantityBiz : BusinessLayer<UomQty>
    {
        //private readonly IRepositry<UploadedFile> _uploadFileDAL;

        /// <summary>
        /// This is where all the uploaded Files will be saved
        /// </summary>
        /// <returns></returns>
        //public override string Event_SaveLocationForUploadedFiles()
        //{
        //    return MyConstants.SAVE_LOCATION_PRODUCT_UOM_WEIGHT;
        //}


        public override void Event_AddUploadedFileInfoIntoDb(IHasUploads entity, UploadObject uploadObj)
        {
            if (uploadObj.NumberOfFilesInHttpPostedFileBase > 0)
            {
                foreach (var file in uploadObj.FileList)
                {
                    //todo move this to create please!
                    file.MetaData.Created.SetToTodaysDate("");

                    file.MenuPath1 = entity as MenuPath1;
                    file.MenuPath1Id = entity.Id;

                    //add the owner of the file here....
                    entity.MiscFiles.Add(file);
                    _uploadedFileBiz.Create(CreateControllerCreateEditParameter(file as ICommonWithId));

                }
            }
        }



        /// <summary>
        /// This deletes/Removes the uploaded file.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uploadedObjectId"></param>

        public async Task DeleteUploadedFile(string uploadedObjectId)
        {

            //Delete the upload
            await _uploadedFileBiz.DeleteAsync(uploadedObjectId);

        }




    }
}
