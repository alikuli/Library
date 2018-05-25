using AliKuli.ConstantsNS;
using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace UowLibrary.ProductNS
{
    public partial class ProductCatMainBiz : BusinessLayer<ProductCategoryMain>
    {

        /// <summary>
        /// This is where all the uploaded Files will be saved
        /// </summary>
        /// <returns></returns>
        //public override string Event_SaveLocationForUploadedFiles()
        //{
        //    return MyConstants.SAVE_LOCATION_PRODUCT_CATEGORY3;
        //}


        //public override void Event_AddUploadedFileInfoIntoDb(IHasUploads entity, UploadObject uploadObj)
        //{
        //    if (uploadObj.NumberOfFiles > 0)
        //    {
        //        foreach (var file in uploadObj.FilesSavedList)
        //        {

        //            file.MetaData.Created.SetToTodaysDate("");

        //            file.ProductCategory3 = entity as ProductCategory3;
        //            file.ProductCategory3Id = entity.Id;

        //            //add the owner of the file here....
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

        }


    }
}
