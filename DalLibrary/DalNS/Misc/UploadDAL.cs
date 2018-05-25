using ModelsClassLibrary.ModelsNS.MediaNS;
using AliKuli.Extentions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DalLibrary.Utilities
{
    /// <summary>
    /// This uploads files. The main method to upload files is
    /// </summary>
    public static class UploadDAL
    {
        
        /// <summary>
        /// This will save the Id Card Front Image Image File. For the requestBase, just send the "Request" from the controller. This will also delete the old file if there is one
        /// </summary>
        public static string SaveIdCardFrontImage(HttpPostedFileBase imageHttp, HttpRequestBase requestBase, string oldFileToDeleteUrl = "")
        {

            if (!oldFileToDeleteUrl.IsNullOrEmpty())
            {
                //Delete the old file
                DeleteFile(oldFileToDeleteUrl, requestBase);
            }
            string newFileUrl = SaveIdCardFrontImageOnly(imageHttp, requestBase);
            return newFileUrl;
        }

        /// <summary>
        /// This will save the Id Card Front Image Image File. For the requestBase, just send the "Request" from the controller. The Id number should be some unique Id, like PersonId
        /// </summary>
        /// <param name="imageHttp"></param>
        /// <param name="requestBase"></param>
        /// <param name="IdNo"></param>
        private static string SaveIdCardFrontImageOnly(HttpPostedFileBase imageHttp, HttpRequestBase requestBase)
        {
            string fileEndName = "IdFrnt";
            string saveToPath = "~/images/People/IdCard/Frnt";
            string errString = "For Person's Id Card Front Picture";

            return UploadFile(imageHttp, requestBase, fileEndName, saveToPath, errString, true);
        }


        //===================================================================================================

        /// <summary>
        /// This will save the Id Card back Image Image File. For the requestBase, just send the "Request" from the controller. This will also delete the old file if there is one
        /// </summary>
        public static string SaveIdCardBackPicture(HttpPostedFileBase imageHttp, HttpRequestBase requestBase, string oldFileToDeleteUrl = "")
        {


            if (!oldFileToDeleteUrl.IsNullOrEmpty())
            {
                //Delete the old file
                DeleteFile(oldFileToDeleteUrl, requestBase);
            }
            string newFileUrl = SaveIdCardBackPictureOnly(imageHttp, requestBase);
            return newFileUrl;
        }


        /// <summary>
        /// This will save the Id Card back Image File. For the requestBase, just send the "Request" from the controller. The Id number should be some unique Id, like PersonId
        /// </summary>
        /// <param name="imageHttp"></param>
        /// <param name="requestBase"></param>
        /// <param name="IdNo"></param>
        private static string SaveIdCardBackPictureOnly(HttpPostedFileBase imageHttp, HttpRequestBase requestBase)
        {
            string fileEndName = "IdBack";
            string saveToPath = "~/images/People/IdCard/Bck";
            string errString = "For Person's Id Card Back Picture";

            return UploadFile(imageHttp, requestBase, fileEndName, saveToPath, errString, true);
        }



        //===================================================================================================



        /// <summary>
        /// This will save the Persons Front picture Image File. For the requestBase, just send the "Request" from the controller. This will also delete the old file if there is one
        /// </summary>
        public static string SavePersonPersonalFrontImage(HttpPostedFileBase imageHttp, HttpRequestBase requestBase, string oldFileToDeleteUrl = "")
        {

            if (!oldFileToDeleteUrl.IsNullOrEmpty())
            {
                //Delete the old file
                DeleteFile(oldFileToDeleteUrl, requestBase);
            }
            string newFileUrl = SavePersonPersonalFrontImageOnly(imageHttp, requestBase);
            return newFileUrl;
        }


        /// <summary>
        /// This will save the Persons Front picture Image File. For the requestBase, just send the "Request" from the controller. The Id number should be some unique Id, like PersonId
        /// </summary>
        /// <param name="imageHttp"></param>
        /// <param name="requestBase"></param>
        /// <param name="IdNo"></param>
        private static string SavePersonPersonalFrontImageOnly(HttpPostedFileBase imageHttp, HttpRequestBase requestBase)
        {
            string fileEndName = "PpFrnt";
            string saveToPath = "~/images/People/Personal/Frnt";
            string errString = "For Person's Front Face Picture";

            return UploadFile(imageHttp, requestBase, fileEndName, saveToPath, errString, true);
        }


        //===================================================================================================

        /// <summary>
        /// This will save the Id Card back Image File. For the requestBase, just send the "Request" from the controller. This will also delete the old file if there is one
        /// </summary>
        public static string SavePersonalSidePicture(HttpPostedFileBase imageHttp, HttpRequestBase requestBase, string oldFileToDeleteUrl = "")
        {

            if (!oldFileToDeleteUrl.IsNullOrEmpty())
            {
                //Delete the old file
                DeleteFile(oldFileToDeleteUrl, requestBase);
            }
            string newFileUrl = SavePersonalSidePictureOnly (imageHttp, requestBase);
            return newFileUrl;
        }




        /// <summary>
        /// This will save the Id Card back Image File. For the requestBase, just send the "Request" from the controller. The Id number should be some unique Id, like PersonId
        /// </summary>
        /// <param name="imageHttp"></param>
        /// <param name="requestBase"></param>
        /// <param name="IdNo"></param>
        private static string SavePersonalSidePictureOnly(HttpPostedFileBase imageHttp, HttpRequestBase requestBase)
        {
            string fileEndName = "PpSide";
            string errString = "For Person's Side Face Picture";
            string saveToPath = "~/images/People/Personal/Side";
            return UploadFile(imageHttp, requestBase, fileEndName, saveToPath, errString, true);
        }



        //===================================================================================================




        /// <summary>
        /// This will save the product medium Image File. For the requestBase, just send the "Request" from the controller. This will also delete the old file if there is one
        /// </summary>
        /// <param name="imageHttp"></param>
        /// <param name="requestBase"></param>
        /// <param name="IdNo"></param>
        public static string SaveProductMediumPicture(HttpPostedFileBase imageHttp, HttpRequestBase requestBase, string oldFileToDeleteUrl = "")
        {
            if (!oldFileToDeleteUrl.IsNullOrEmpty())
            {
                //Delete the old file
                DeleteFile(oldFileToDeleteUrl, requestBase);
            }
            string newFileUrl = SaveProductMediumPictureOnly(imageHttp, requestBase);
            return newFileUrl;
        }

        /// <summary>
        /// This will save the Big Image File. For the requestBase, just send the "Request" from the controller. The Id number should be some unique Id, like PersonId
        /// </summary>
        /// <param name="imageHttp"></param>
        /// <param name="requestBase"></param>
        /// <param name="IdNo"></param>
        private static string SaveProductMediumPictureOnly(HttpPostedFileBase imageHttp, HttpRequestBase requestBase)
        {
            string fileEndName = "PrdMd";
            string saveToPath = "~/images/Product/MediumPics";
            string errString = "For Product's Medium Picture";

            return UploadFile(imageHttp, requestBase, fileEndName, saveToPath, errString, true);
        }



        //===================================================================================================




        /// <summary>
        /// This will save the small/thumbnail Image File. For the requestBase, just send the "Request" from the controller. This will also delete the old file if there is one
        /// </summary>
        /// <param name="imageHttp"></param>
        /// <param name="requestBase"></param>
        /// <param name="IdNo"></param>
        public static string SaveProductSmallPicture (HttpPostedFileBase imageHttp, HttpRequestBase requestBase, string oldFileToDeleteUrl = "")
        {
            if (!oldFileToDeleteUrl.IsNullOrEmpty())
            {
                //Delete the old file
                DeleteFile(oldFileToDeleteUrl, requestBase);
            }
            string newFileUrl = SaveProductSmallPictureOnly(imageHttp, requestBase);
            return newFileUrl;
        }




        /// <summary>
        /// This will save the small/thumbnail Image File. For the requestBase, just send the "Request" from the controller. The Id number should be some unique Id, like PersonId
        /// </summary>
        /// <param name="imageHttp"></param>
        /// <param name="requestBase"></param>
        /// <param name="IdNo"></param>
        public static string SaveProductSmallPictureOnly (HttpPostedFileBase imageHttp, HttpRequestBase requestBase)
        {
            string fileEndName = "PrdSm";
            string saveToPath = "~/images/Product/SmallPics";
            string errString = "For Product's Small Picture";

            return UploadFile(imageHttp, requestBase, fileEndName, saveToPath, errString, true);
        }

        //===================================================================================================





        /// <summary>
        /// This saves a picture to the file. The errString has a partial error message so we can know which save failed if we
        /// use this multiple times
        /// </summary>
        /// <param name="httpPostedFileBase"></param>
        /// <param name="requestBase"></param>
        /// <param name="endFileName"></param>
        /// <param name="IdNo"></param>
        /// <param name="saveToPath"></param>
        /// <param name="errString"></param>
        /// <returns></returns>
        public static string UploadFile(
            HttpPostedFileBase httpPostedFileBase, 
            HttpRequestBase requestBase, 
            string endFileName,  
            string saveToPath, 
            string errString, 
            bool isImage)
        {
            if (httpPostedFileBase == null && httpPostedFileBase.ContentLength == 0)
            {
                if (!errString.IsNullOrEmpty())
                {
                    throw new Exception(string.Format("File {0} not received. Please try again. UploadDAL.UploadImage", errString));
                }
                else
                {
                    throw new Exception(string.Format("File not received. Please try again. UploadDAL.UploadImage"));

                }

            }

            if(isImage)
            {
                //is image acceptale
                IsImageErrorFree(httpPostedFileBase);
            }

            if (!endFileName.IsNullOrEmpty())
            {
                endFileName = "_" + endFileName;
            }

            //this is part of the name
            string IdNo = DateTime.Now.Ticks.ToString();
            string uploadedFileName = IdNo + endFileName + Path.GetExtension(httpPostedFileBase.FileName);

            var uploadedFilesActualServerPath = Path.Combine(requestBase.MapPath(saveToPath), uploadedFileName);
            var uploadedFileUrl = Path.Combine(saveToPath, uploadedFileName);
            httpPostedFileBase.SaveAs(uploadedFilesActualServerPath);
            return uploadedFileUrl;

            
        }



        //===================================================================================================



        /// <summary>
        /// This deletes the uploaded file at the specific URL. From the controller send the "Request" for the requestBase
        /// </summary>
        /// <param name="requestBase"></param>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        public static bool DeleteFile( string fileUrl, HttpRequestBase requestBase)
        {
            if (fileUrl.IsNullOrEmpty())
            {
                return false;
            }

            string fileServerPath = requestBase.MapPath(fileUrl);
            File.Delete(fileServerPath);

            return true;
        }

        //===================================================================================================

        /// <summary>
        /// This checks to see if it is an acceptable image file.
        /// </summary>
        /// <param name="theImageFile"></param>
        /// <returns></returns>
        private static bool IsImageErrorFree(HttpPostedFileBase theImageFile)
        {
            var validImageTypes = new string[]
                {
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
                };

            if (theImageFile == null || theImageFile.ContentLength == 0)
            {
                throw new Exception("The content length is zero. ImageHandler.SaveImageFile");
            }
            else
                if (!validImageTypes.Contains(theImageFile.ContentType))
                {
                    throw new Exception("Please choose either a GIF, JPG or PNG image.");
                }

            return true;
        }



    }
}