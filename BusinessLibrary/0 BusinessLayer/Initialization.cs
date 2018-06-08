using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using UowLibrary.Abstract;
using UowLibrary.Interface;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using InterfacesLibrary.SharedNS.FeaturesNS;

namespace UowLibrary
{
    /// <summary>
    /// This is where all the data initialization takes place. There is a default built version which accepts data as string[].
    /// To send data to this, override GetDataForStringArrayFormat;
    /// To send more complicated data, override GetData.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class BusinessLayer<TEntity> : AbstractBiz, IBusinessLayer<TEntity> where TEntity : class, ICommonWithId
    {
        #region overrideable methods for controlling initialization

        //Sometimes if you need to save after every addition, make this true
        //public bool IsSaveAfterEveryAddition { get; set; }

        /// <summary>
        /// Use this to inject data into GetData. It is only used if you have a simple string array format for data.
        /// </summary>
        public virtual string[] GetDataForStringArrayFormat { get { throw new NotImplementedException("DataStringArrayFormat"); } }

        /// <summary>
        /// Override this poperty if you want to change the save initialized message; Sometimes you have to change the message because it gives an ugly one.
        /// </summary>
        public virtual string SaveMessage { get { return string.Format("{0} initialized", typeof(TEntity).Name.ToTitleSentance()); } }

        /// <summary>
        /// Override this to give values to entities before saving
        /// </summary>
        /// <param name="tentity"></param>
        public virtual void Event_DoSpecialInitializationStuff(TEntity tentity)
        {
            //if the entity is IHasImage, then this looks for an initialization image and loads it if it is found
            //it also copies the image from the initialization folder, but does not delete.
            initialization_LoadImage(tentity);
            tentity.DetailInfoToDisplayOnWebsite = getInitializationDescription(tentity);

        }


        ///// <summary>
        ///// entity.MetaData.IsEditLocked = true;
        ///// </summary>
        ///// <param name="entity"></param>
        //public virtual bool Event_LockEditDuringInitialization()
        //{
        //    //entity.MetaData.IsEditLocked = false;
        //    return false;
        //}

        /// <summary>
        /// Override this to add more complicated data than an array string format.
        /// You must add all your data to the system using it's CRUD. There is no need to save.
        /// The system will save automatically for you.
        /// Note, if you want a new save message override SaveMessage
        /// Note. If you want to make minor changes when adding string[] data, override Event_ChangeStringFormatInitializationDataBeforeAdding
        /// </summary>
        public virtual void AddInitData()
        {
            addInitData_Helper(GetDataForStringArrayFormat);
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// this is the save message
        /// </summary>
        private void addSaveMessage()
        {
            ErrorsGlobal.AddMessage(SaveMessage);
        }



        /// <summary>
        /// This adds the data if it is in the simple string array format. For more complicated stuff, override GetData.
        /// </summary>
        /// <param name="dataList"></param>
        private void addInitData_Helper(string[] dataList)
        {

            if (dataList.IsNullOrEmpty())
            {
                ErrorsGlobal.Add("No initialization data.", MethodBase.GetCurrentMethod());
            }


            for (int i = 0; i < dataList.Length; i++)
            {
                bool recordExists = !FindByName(dataList[i]).IsNull();
                bool recordIsUnkown = dataList[i].ToLower() == "unknown";

                if (recordIsUnkown)
                    continue;

                if (recordExists)
                    continue;

                TEntity tentity = Factory();
                tentity.Name = dataList[i];
                CreateSave_ForInitializeOnly(tentity);
            }

        }




        #endregion

        /// <summary>
        /// This is where all the data initialization takes place. There is a default built version which accepts data as string[].
        /// To send data to this, override GetDataForStringArrayFormat;
        /// To send more complicated data, override GetData.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        public void InitializationData()
        {
            try
            {
                AddInitData();
                //if (!IsSaveAfterEveryAddition)
                //{
                //    //await SaveChangesAsync();
                //}
                addSaveMessage();
            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Error during initialization", MethodBase.GetCurrentMethod(), e);
            }
        }




        //public void SaveAfterEveryAddition(bool isSavingAfterEveryAddition)
        //{
        //    IsSaveAfterEveryAddition = isSavingAfterEveryAddition;
        //}




        /// <summary>
        /// This loads the image file. The initialize image file must have the same name as the category. It can have any extention belonging to image. 
        /// It checks for the folloing image files 
        /// .jpg .png .tiff .jpeg .bmp .pdf
        /// This also copies the file from its location to the new location.
        /// </summary>
        /// <param name="entity"></param>
        protected void initialization_LoadImage(TEntity entity)
        {

            IHasUploads entityHasUploads = entity as IHasUploads;
            if (entityHasUploads.IsNull())
                return;

            ICommonWithId icommonwithid = entity as ICommonWithId;
            icommonwithid.IsNullThrowException("Unable to cast entity to ICommonWithId. Programming Error.");


            string originalname = icommonwithid.Name.RemoveAllSpaces().ToString();
            string relative_SrcPath = entityHasUploads.MiscFilesLocation_Initialization();
            string relative_targetPath = entityHasUploads.MiscFilesLocation();

            string filenameNoExtention = getFileNameWithoutExtention(relative_SrcPath, originalname);

            
            if (!imageFileExists(filenameNoExtention))
                return;


            saveAndCopyUploadedFile(entity, entityHasUploads, originalname, relative_SrcPath, relative_targetPath);


        }

        /// <summary>
        /// This saves and copies the file.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityHasUploads"></param>
        /// <param name="originalnameWithoutExtention"></param>
        /// <param name="relative_SrcPath"></param>
        /// <param name="relative_targetPath"></param>
        protected void  saveAndCopyUploadedFile (TEntity entity, IHasUploads entityHasUploads, string originalnameWithoutExtention, string relative_SrcPath, string relative_targetPath)
        {
            List<UploadedFile> lst = new List<UploadedFile>();

            //copy the actual file to the new spot. We need to do it here so we can get it's new name
            //== COPY FILE
            string newNameWithMappedPathPlusExtention = CopyFile(relative_SrcPath, relative_targetPath, Path.ChangeExtension(originalnameWithoutExtention, ExtentionFound));

            //string newPath = Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, entity.ClassNameRaw);
            //UploadedFile uf = new UploadedFile(originalnameWithoutExtention, newNameWithMappedPathPlusExtention, newPath);
            //UploadedFile uf = new UploadedFile(originalnameWithoutExtention, newNameWithMappedPathPlusExtention, relative_targetPath);
            
            UploadedFile uf = new UploadedFile(
                originalnameWithoutExtention,
                Path.GetFileNameWithoutExtension(newNameWithMappedPathPlusExtention),
                ExtentionFound, 
                relative_targetPath);

            lst.Add(uf);

            SaveUploadedFiles(lst,
                entity,
                entityHasUploads.MiscFiles,
                IUserHasUploadsTypeENUM.None);

        }

        /// <summary>
        /// This makes the file name along with the path, but no extention
        /// </summary>
        /// <param name="originalname"></param>
        /// <param name="relative_SrcPath"></param>
        /// <returns></returns>
        protected static string getFileNameWithoutExtention(string relative_SrcPath, string originalname )
        {
            string lookingForFileWithoutExtention = HttpContext.Current.Server.MapPath(Path.Combine(relative_SrcPath, originalname));
            return lookingForFileWithoutExtention;
        }

        private bool isContinue<T>(T entity)
        {
            if (!IsHasUploads)
                return false;

            if (entity.IsNull())
            {
                ErrorsGlobal.Add("Entity is null", MethodBase.GetCurrentMethod());
                throw new ArgumentNullException(ErrorsGlobal.ToString());
            }

            return true;
        }

        //You can only initialize descriptions if the product has an image i.e. is IHasUploads
        protected string getInitializationDescription(TEntity entity)
        {
            if (!isContinue(entity))
                return "";

            IHasUploads entityHasUploads = entity as IHasUploads;

            string originalname = entity.Name.RemoveAllSpaces().ToString();
            string relative_SrcPath = entityHasUploads.MiscFilesLocation_Initialization();
            string relative_targetPath = entityHasUploads.MiscFilesLocation();

            string filenameNoExtention = getFileNameWithoutExtention(relative_SrcPath, originalname);


            if (!txtFileExists(filenameNoExtention))
                return "";


            //if txt file exists...
            string completeFileName = Path.ChangeExtension(filenameNoExtention, ExtentionFound);

            try
            {
                string text = File.ReadAllText(completeFileName);
                return text;
            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Exception while reading Text File.", MethodBase.GetCurrentMethod(), e);
                throw new Exception(ErrorsGlobal.ToString());
            }




        }
    }
}
