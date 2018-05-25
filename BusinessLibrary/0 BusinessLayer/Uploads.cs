using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UowLibrary.Abstract;
using UowLibrary.Interface;

namespace UowLibrary
{
    /// <summary>
    /// The file UploadObject has all the machinery to handle the upload and save of the file. It then also creates
    /// the UploadFile Class, puts all the files in a list and we can then us it to save it into the db. To
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class BusinessLayer<TEntity> : AbstractBiz, IBusinessLayer<TEntity> where TEntity : class, ICommonWithId
    {




        public void SaveUploadedFiles(List<UploadedFile> lstUploadedFile, TEntity entity, ICollection<UploadedFile> navigation, IUserHasUploadsTypeENUM iuserHasUploadsTypeEnum = IUserHasUploadsTypeENUM.None)
        {
            if (!lstUploadedFile.IsNullOrEmpty())
            {
                foreach (UploadedFile file in lstUploadedFile)
                {
                    file.MetaData.Created.SetToTodaysDate(UserNameBiz);


                    //initializes navigation if it is null
                    if (navigation.IsNull())
                        navigation = new List<UploadedFile>(); //intializing

                    navigation.Add(file);

                    //You need to add a refrence here to save the file in the UploadedFile as well.
                    AddEntityRecordIntoUpload(file, entity, iuserHasUploadsTypeEnum);

                    _uploadedFileBiz.Create(file);

                }
            }
        }


        //private string selectNavigationId(UploadedFile file)
        //{
        //    switch (FileUploadTypeENUM )
        //    {
        //        case FileUploadTypeENUM.MiscUploads:
        //            return file.Id
        //            break;
        //        case FileUploadTypeENUM.UserSelfie:
        //            break;
        //        case FileUploadTypeENUM.UserIdCardFront:
        //            break;
        //        case FileUploadTypeENUM.UserIdCardBack:
        //            break;
        //        case FileUploadTypeENUM.UserLiscenceFront:
        //            break;
        //        case FileUploadTypeENUM.UserLiscenceBack:
        //            break;
        //        case FileUploadTypeENUM.UserPassportFirstPage:
        //            break;
        //        case FileUploadTypeENUM.UserPassPortVisa:
        //            break;
        //        default:
        //            break;
        //    }
        //}








        //private void uploadFile(ControllerCreateEditParameter parm, IHasUploads entity, HttpPostedFileBase[] files, string locationToSave)
        //{
        //    ///Deal with IHasUploads
        //    if (!files[0].IsNull())
        //    {
        //        //Save the files if they exist.
        //        UploadObject uploadObj = new UploadObject(files, locationToSave);

        //        //If you are here and files exist, they have been saved, now save their pointers
        //        //to the db.
        //        //if we have saved some files... they will be in Files.
        //        if (uploadObj.NumberOfFiles > 0)
        //        {
        //            IHasUploads hasUploads = entity as IHasUploads;

        //            if (hasUploads.IsNull())
        //                return;

        //            getUploads(hasUploads, uploadObj);
        //        }
        //    }
        //}
        /// <summary>
        /// This deletes/Removes the uploaded file.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uploadedObjectId"></param>

        public async Task DeleteUploadedFile(string entityId, string uploadId)
        {

            if (uploadId.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.Add(string.Format("Upload ID not received!."), MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }
            if (entityId.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.Add(string.Format("Entity ID not received!."), MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }

            TEntity entity = await FindAsync(entityId);

            if (entity.IsNull())
            {
                ErrorsGlobal.Add(string.Format("{0} record was not found.", GetClassName().ToTitleSentance()), MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }
            IHasUploads entityHasUploads = entity as IHasUploads;

            if (entityHasUploads.IsNull())
            {
                ErrorsGlobal.Add(string.Format("{0} is not uploadable. Programming error.", GetClassName().ToTitleSentance()), MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }


            if (entityHasUploads.MiscFiles.IsNullOrEmpty())
                return;


            UploadedFile uploadedFile = entityHasUploads.MiscFiles.FirstOrDefault(x => x.Id == uploadId);

            if (uploadedFile.IsNull())
            {
                ErrorsGlobal.Add(string.Format("Upload file not found."), MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }

            entityHasUploads.MiscFiles.Remove(uploadedFile);
            var f = new System.IO.FileInfo(uploadedFile.AbsolutePathWithFileName());
            f.Delete();



        }

        //private void getUploads(ControllerCreateEditParameter parm, ICollection<UploadedFile> uploadedFiles, IHasUploads ihasUploads, UploadObject uploadObj)
        //{

        //    if (uploadObj.NumberOfFiles > 0)
        //    {
        //        foreach (var file in uploadObj.FileList)
        //        {
        //            file.MetaData.Created.SetToTodaysDate(UserNameBiz);

        //            if (uploadedFiles.IsNull())
        //                uploadedFiles = new List<UploadedFile>();

        //            uploadedFiles.Add(file);
        //            //You need to add a refrence here to save the file in the UploadedFile as well.

        //            TEntity entity = ihasUploads as TEntity;
        //            AddEntityRecordIntoUpload(file, entity);
        //            _uploadedFileBiz.Create(file);

        //        }
        //    }
        //}

        //private void getUploads(ICollection<UploadedFile> uploadedFiles,IHasUploads ihasUploads, UploadObject uploadObj)
        //{
        //    if (uploadObj.NumberOfFiles > 0)
        //    {
        //        foreach (var file in uploadObj.FilesSavedList)
        //        {
        //            file.MetaData.Created.SetToTodaysDate(UserNameBiz);

        //            if (uploadedFiles.IsNull())
        //                uploadedFiles = new List<UploadedFile>();

        //            uploadedFiles.Add(file);
        //            //You need to add a refrence here to save the file in the UploadedFile as well.

        //            TEntity entity = ihasUploads as TEntity;
        //            AddEntityRecordIntoUpload(file, entity);
        //            _uploadedFileBiz.Create(file);

        //        }
        //    }
        //}

        /// <summary>
        /// Example
        ///    uploadFile.FileDoc = filedoc;
        ///    uploadFile.Id = filedoc.Id;

        /// </summary>
        /// <param name="uploadFile"></param>
        /// <param name="entity"></param>
        public virtual void AddEntityRecordIntoUpload(UploadedFile uploadedFile, TEntity entity, IUserHasUploadsTypeENUM iuserHasUploadsTypeEnum)
        {
            ErrorsGlobal.Add(string.Format("AddEntityRecordIntoUpload not implimented in {0}.", entity.MetaData.GetSelfClassName()), MethodBase.GetCurrentMethod());
            throw new NotImplementedException(ErrorsGlobal.ToString());
        }

        /// <summary>
        /// Override this to add uploaded files to the Entity.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Event_AddUploadedFileInfoIntoDb(IHasUploads entity, UploadObject uploadObj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This brings together all the uploading.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="files"></param>
        private void handleUploadedFilesIfExist(ControllerCreateEditParameter parm)
        {
            getFilesForIhasUploads(parm);
            getFilesForIUserHasUploads(parm);


        }

        //private void uploadFile(ControllerCreateEditParameter parm)
        //{
        //    ///Deal with IHasUploads
        //    //uploadFileHelperForIHasUploads(
        //    //    parm.MiscUploadedFiles,
        //    //    parm.Entity as TEntity);
        //    getFilesForIhasUploads(parm);
        //    getFilesForIUserHasUploads(parm);
        //}

        private void getFilesForIUserHasUploads(ControllerCreateEditParameter parm)
        {


            if (parm.IsIUserHasUploads)
            {
                uploadFilesHelperGeneral(
                    parm.SelfieUpload,
                    parm.Entity as TEntity,
                    parm.Entity_IUserHasUploads.SelfieUploads,
                    IUserHasUploadsTypeENUM.Selfie);

                uploadFilesHelperGeneral(
                    parm.IdCardFront,
                    parm.Entity as TEntity,
                    parm.Entity_IUserHasUploads.IdCardFrontUploads,
                    IUserHasUploadsTypeENUM.IdCardFront);


                uploadFilesHelperGeneral(
                    parm.IdCardBack,
                    parm.Entity as TEntity,
                    parm.Entity_IUserHasUploads.IdCardBackUploads,
                    IUserHasUploadsTypeENUM.IdCardBack);


                uploadFilesHelperGeneral(
                    parm.PassportFront,
                    parm.Entity as TEntity,
                    parm.Entity_IUserHasUploads.PassportFrontUploads,
                    IUserHasUploadsTypeENUM.PassportFront);



                uploadFilesHelperGeneral(
                    parm.PassportVisa,
                    parm.Entity as TEntity,
                    parm.Entity_IUserHasUploads.PassportVisaUploads,
                    IUserHasUploadsTypeENUM.PassportVisa);


                uploadFilesHelperGeneral(
                    parm.LiscenseFront,
                    parm.Entity as TEntity,
                    parm.Entity_IUserHasUploads.LiscenseFrontUploads,
                    IUserHasUploadsTypeENUM.LiscenseFront);


                uploadFilesHelperGeneral(
                    parm.LiscenseBack,
                    parm.Entity as TEntity,
                    parm.Entity_IUserHasUploads.LiscenseBackUploads,
                    IUserHasUploadsTypeENUM.LiscenseBack);
            }
        }

        private void getFilesForIhasUploads(ControllerCreateEditParameter parm)
        {
            if (parm.IsIHasUploads)
            {


                uploadFilesHelperGeneral(
                    parm.MiscUploadedFiles,
                    parm.Entity as TEntity,
                    parm.Entity_IHasUploads.MiscFiles,
                    IUserHasUploadsTypeENUM.Misc);
            }
        }


        private void uploadFilesHelperGeneral(ControllerCreateEditParameterDetail filesIn,
            TEntity entity, ICollection<UploadedFile> navigation, IUserHasUploadsTypeENUM iuserHasUploadsTypeEnum)
        {
            if (filesIn.IsNull())
                return;

            if (filesIn.IsHttpBaseNull)
                return;

            if (filesIn.HttpBase.Count() == 0)
                return;

            UploadObject uploadObj = new UploadObject(filesIn.HttpBase, filesIn.FileLocationConst);

            retrieveAnyMessages(uploadObj);

            bool numberOfFilesAreZero = uploadObj.NumberOfFilesInHttpPostedFileBase == 0;

            if (numberOfFilesAreZero)
                return;

            //Now add the uploads to the entity
            SaveUploadedFiles(uploadObj.FileList, entity, navigation, iuserHasUploadsTypeEnum);



        }

        private void retrieveAnyMessages(UploadObject uploadObjIhasUploads)
        {
            bool messagesAreThere = !uploadObjIhasUploads.GetMessages().IsNullOrEmpty();

            if (messagesAreThere)
            {
                foreach (var msg in uploadObjIhasUploads.GetMessages())
                {
                    ErrorsGlobal.AddMessage(msg);
                }
            }

        }

    }
}
