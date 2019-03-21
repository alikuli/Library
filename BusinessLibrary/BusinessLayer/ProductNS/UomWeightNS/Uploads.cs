using AliKuli.UtilitiesNS;
using InterfacesLibrary.SharedNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Threading.Tasks;

namespace UowLibrary
{
    public partial class UomWeightBiz : BusinessLayer<UomWeight>
    {


        public override void Event_AddUploadedFileInfoIntoDb(IHasUploads entity, UploadObject uploadObj)
        {
            if (uploadObj.NumberOfFilesInFileList > 0)
            {
                foreach (var file in uploadObj.FileList)
                {

                    file.MetaData.Created.SetToTodaysDate(UserId, UserName);

                    file.MenuPath1 = entity as MenuPath1;
                    file.MenuPath1Id = entity.Id;

                    //add the owner of the file here....
                    entity.MiscFiles.Add(file);
                    UploadedFileBiz.CreateSimple(CreateControllerCreateEditParameter(file as ICommonWithId));

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
            await UploadedFileBiz.DeleteAsync(uploadedObjectId);

        }




    }
}
