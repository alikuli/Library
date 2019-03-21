using AliKuli.Extentions;
using AliKuli.ToolsNS;
using AliKuli.UtilitiesNS;
using BreadCrumbsLibraryNS.Programs;
using ConfigManagerLibrary;
using ConstantsLibrary;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using InvoiceNS;
using MigraDocLibrary.IndexNS;
using MigraDocLibrary.InvoiceNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddessWithIdNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.ModelsNS.VerificatonNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UowLibrary.Abstract;
using UowLibrary.Interface;
using UowLibrary.MenuNS.MenuStateNS;
using UowLibrary.PageViewNS;
using UowLibrary.ParametersNS;
using UowLibrary.UploadFileNS;
using WebLibrary.Programs;
namespace UowLibrary
{
    /// <summary>
    /// This is where all the Fix, bussiness Rules and ErrorChecks are implemented
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class BusinessLayer<TEntity> : AbstractBiz, IBusinessLayer<TEntity> where TEntity : class, ICommonWithId
    {
        private IRepositry<TEntity> _dal;


        public BusinessLayer(IRepositry<TEntity> dal, BizParameters param)
            : base(param)
        {
            _dal = dal;

        }

        public BusinessLayer(IRepositry<TEntity> dal, UploadedFileBiz uploadedFileBiz, IMemoryMain memoryMain, PageViewBiz pageViewBiz, IErrorSet errorSet, ConfigManagerHelper configManagerHelper, BreadCrumbManager breadCrumbManager)
            : base(uploadedFileBiz, memoryMain, pageViewBiz, errorSet, configManagerHelper, breadCrumbManager)
        {
            _dal = dal;
            //_param = param;

            //_uploadedFileBiz = param.UploadedFileBiz;

        }


        protected IRepositry<TEntity> Dal
        {
            get
            {
                if (_dal.IsNull())
                {
                    ErrorsGlobal.Add("No CountryDAL received.", MethodBase.GetCurrentMethod());
                    throw new Exception(ErrorsGlobal.ToString());
                }
                //This is where userName is passed to the Dal from Biz
                _dal.UserName = UserName ?? "";
                return _dal;
            }
        }









        /// <summary>
        /// Enter the entity business rules over here.
        /// Default rule: No duplcaite names.
        /// If duplicate name is found it throws a NoDuplicateException;
        /// Note, if you want to forever lock a record, you can use...
        ///     entity.MetaData.IsEditLocked
        /// </summary>
        /// <param name="entity"></param>
        public virtual void BusinessRulesFor(ControllerCreateEditParameter parm)
        {
            NoDuplicateNameAllowed(parm.Entity as TEntity);

            //other defaults
            //entity.MetaData.IsEditLocked = true; This is used during initialization mainly... but can be use

        }

        private void NoDuplicateNameAllowed(TEntity entity)
        {

            if (entity.IsAllowDuplicates)
                return;


            bool found = GetDataToCheckDuplicateName(entity).Any(x =>
                x.Name.ToLower() == entity.Name.ToLower() &&
                x.Id != entity.Id);
            ;

            if (found)
            {
                //This is required otherwise all the previous entries that were found remain in the cache and get added. The Notracking does not work.
                Dal.Detach(entity);
                throw new NoDuplicateException(string.Format("{0}: '{1}' already exists in the db.", entity.GetType().Name, entity.Name));
                //throw new NoDuplicateException(string.Format("{0}: '{1}' already exists in the db.", entity.GetType().Name, entity.Name));

            }
            else
                return;
        }

        public virtual IQueryable<TEntity> GetDataToCheckDuplicateName(TEntity entity)
        {
            IQueryable<TEntity> dataSet = Dal.FindAll();
            return dataSet;
        }

        public ControllerCreateEditParameter CreateControllerCreateEditParameter(ICommonWithId iCommonWithId)
        {
            ControllerCreateEditParameter cp = new ControllerCreateEditParameter();
            cp.Entity = iCommonWithId;
            return cp;
        }

        public void Detach(TEntity entity)
        {
            Dal.Detach(entity);
        }

        public void Attach(TEntity entity)
        {
            Dal.Attach(entity);
        }


        #region Create

        public virtual void CreateSimple(ControllerCreateEditParameter parm)
        {
            createEngineSimple(parm);
        }

        private void create(TEntity entity)
        {
            Dal.Create(entity);
        }

        //public virtual void Create(ControllerCreateEditParameter parm)
        //{
        //    createEngineWithFileUpload(parm);
        //}
        //public virtual void Create(TEntity entity, HttpPostedFileBase[] files)
        //{
        //    createEngineWithFileUpload(entity, files);
        //}



        //-----------------------------------------------------------------------------------------

        //public virtual void CreateAndSaveNoFileUpload(ControllerCreateEditParameter parm)
        //{
        //    createEngineSimple(parm);
        //    SaveChanges();
        //}

        /// <summary>
        /// This creates and saves for initialization only. It AUTOMATICLY looks for an image in the initialization folder and loads it, 
        /// provided the image name is the same as the name of the product, without spaces.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void CreateSave_ForInitializeOnly(TEntity entity)
        {
            try
            {
                Event_DoSpecialInitializationStuff(entity);
                entity.MetaData.IsEditLocked = Event_LockEditDuringInitialization();
                CreateAndSave(CreateControllerCreateEditParameter(entity as ICommonWithId));
            }
            catch (NoDuplicateException)
            {

                ErrorsGlobal.AddMessage(string.Format("Item '{0}' is already initialized.", entity.Name));
            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Error while creating entity", MethodBase.GetCurrentMethod(), e);

            }
        }

        public virtual void CreateAndSave(TEntity entity)
        {
            ControllerCreateEditParameter parm = new ControllerCreateEditParameter();
            parm.Entity = entity;
            CreateAndSave(parm);
            //SaveChanges();
        }

        public virtual void CreateAndSave(ControllerCreateEditParameter parm)
        {
            createEngineWithFileUpload(parm);
            SaveChanges();
        }


        //public virtual async Task CreateAndSaveAsync(TEntity entity)
        //{
        //    createEngineSimple(entity);
        //    await SaveChangesAsync();
        //}
        public virtual async Task CreateAndSaveAsync(ControllerCreateEditParameter parm)
        {
            createEngineWithFileUpload(parm);
            await SaveChangesAsync();
        }



        //-----------------------------------------------------------------------------------------






        private void createEngineWithFileUpload(ControllerCreateEditParameter parm)
        {
            //this was removed because it shows up twice... it is also in CreateEngineSimple
            //fixEntityAndBussinessRulesAndErrorCheck_Helper(parm);
            //handleRelatedFilesIfExist(parm);
            createEngineSimple(parm);
        }

        private void createEngineSimple(ControllerCreateEditParameter parm)
        {
            //entity.IsCreating = true;
            fixEntityAndBussinessRulesAndErrorCheck_Helper(parm);
            handleRelatedFilesIfExist(parm);

            CreateEntity(parm.Entity as TEntity);
            ClearSelectListInCache(SelectListCacheKey);
        }

        /// <summary>
        /// This needs to be overridden for the Product VM. Here we will change the Entity. This will be overridden in each individual
        /// ProductVm
        /// </summary>
        /// <param name="entity"></param>
        public virtual void CreateEntity(TEntity entity)
        {
            fixEntityAndBussinessRulesAndErrorCheck_Helper(entity);
            create(entity);
        }


        private void fixEntityAndBussinessRulesAndErrorCheck_Helper(TEntity entity)
        {
            ControllerCreateEditParameter parm = new ControllerCreateEditParameter();
            parm.Entity = entity as ICommonWithId;
            fixEntityAndBussinessRulesAndErrorCheck_Helper(parm);
        }
        private void fixEntityAndBussinessRulesAndErrorCheck_Helper(ControllerCreateEditParameter parm)
        {
            Fix(parm);
            BusinessRulesFor(parm);
            ErrorCheck(parm);
            Monetize(parm);
        }

        /// <summary>
        /// This is where all monetization will take place.
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public virtual bool Monetize(ControllerCreateEditParameter parm)
        {
            return true;
        }

        public virtual void ErrorCheck(ControllerCreateEditParameter parm)
        {
            parm.Entity.SelfErrorCheck();

        }

        #endregion

        #region Update

        public void UpdateAndSave(TEntity entity)
        {
            ControllerCreateEditParameter param = new ControllerCreateEditParameter();
            param.Entity = entity as ICommonWithId;
            UpdateAndSave(param);
        }

        public void UpdateAndSave(ControllerCreateEditParameter param)
        {
            updateEngine(param);
            SaveChanges();

        }



        public async Task UpdateAndSaveAsync(ControllerCreateEditParameter parm)
        {
            updateEngine(parm);
            await SaveChangesAsync();

        }

        private void updateEngine(ControllerCreateEditParameter parm)
        {
            //parm.Entity.IsUpdating = true;
            fixEntityAndBussinessRulesAndErrorCheck_Helper(parm);
            handleRelatedFilesIfExist(parm);

            Product productTest = parm.Entity as Product;
            AddParentChildCode(parm);
            Update(parm.Entity as TEntity);
            ClearSelectListInCache(SelectListCacheKey);

        }

        public virtual void AddParentChildCode(ControllerCreateEditParameter parm)
        {

        }

        /// <summary>
        /// This will need to be updated in each individual Product VM
        /// </summary>
        /// <param name="parm"></param>
        public virtual void Update(TEntity entity)
        {

            Dal.Update(entity);
        }




        #endregion

        #region Delete

        public virtual bool Delete(string id)
        {
            try
            {
                Dal.Delete(id);
                ErrorsGlobal.AddMessage(string.Format("*** Deleted ***"));
                ClearSelectListInCache(SelectListCacheKey);
                return true;
            }
            catch (Exception e)
            {

                ErrorsGlobal.AddMessage(string.Format("*** NOT Deleted ***"));
                ErrorsGlobal.Add(string.Format("Unable to find the {0} record", typeof(TEntity).Name.ToUpper()), MethodBase.GetCurrentMethod(), e);
            }
            return false;
        }

        string _nameOfItemBeingDeleted;
        ///// <summary>
        ///// This stores the Entity name for onward use while deleting.
        ///// </summary>
        //public string NameOfItemBeingDeleted
        //{
        //    get
        //    {
        //        if (_nameOfItemBeingDeleted.IsNullOrWhiteSpace())
        //        {
        //            ErrorsGlobal.Add("Entity Name is null", MethodBase.GetCurrentMethod());
        //            throw new Exception(ErrorsGlobal.ToString());
        //        }
        //        return _nameOfItemBeingDeleted;
        //    }
        //}
        public virtual bool DeleteAndSave(string id)
        {
            try
            {
                Delete(id);
                SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                ErrorsGlobal.AddMessage(string.Format("*** NOT Deleted ***"));
                ErrorsGlobal.Add(string.Format("Unable to find the {0} record", typeof(TEntity).Name.ToUpper()), MethodBase.GetCurrentMethod(), e);
            }
            return false;
        }

        //public virtual bool DeleteActually(string id)
        //{
        //    TEntity entity = Dal.FindFor(id);
        //    try
        //    {
        //        if (entity.IsNull())
        //        {
        //            ErrorsGlobal.Add(string.Format("Unable to find the {0} record", typeof(TEntity).Name.ToUpper()), "Delete");
        //            ErrorsGlobal.AddMessage(string.Format("*** NOT Deleted ***"));
        //            return false;
        //        }
        //        _nameOfItemBeingDeleted = entity.Name; //NameOfItemBeingDeleted is updated here
        //        Dal.DeleteActually(entity);
        //        Dal.SaveChanges();
        //        ErrorsGlobal.AddMessage(string.Format("*** Deleted '{0}' ***", entity.Name));
        //        ClearSelectListInCache(SelectListCacheKey);
        //        return true;
        //    }
        //    catch (Exception e)
        //    {

        //        ErrorsGlobal.AddMessage(string.Format("*** NOT Deleted '{0}' ***", entity.Name));
        //        ErrorsGlobal.Add(string.Format("Unable to find the {0} record", typeof(TEntity).Name.ToUpper()), MethodBase.GetCurrentMethod(), e);
        //    }
        //    return false;
        //}
        public virtual async Task<bool> DeleteAsync(string id)
        {
            TEntity entity = await Dal.FindForAsync(id);
            try
            {
                if (entity.IsNull())
                {

                    ErrorsGlobal.Add(string.Format("Unable to find the record"), "Delete");
                    ErrorsGlobal.AddMessage(string.Format("*** NOT Deleted ***"));
                    return false;
                }
                _nameOfItemBeingDeleted = entity.Name; //NameOfItemBeingDeleted is updated here
                Dal.Delete(id);
                await SaveChangesAsync();
                ErrorsGlobal.AddMessage(string.Format("*** Deleted '{0}' ***", entity.Name));
                ClearSelectListInCache(SelectListCacheKey);
                return true;
            }
            catch (Exception e)
            {

                ErrorsGlobal.AddMessage(string.Format("*** NOT Deleted '{0}' ***", entity.Name));
                ErrorsGlobal.Add(string.Format("Unable to find the {0} record", typeof(TEntity).Name.ToUpper()), MethodBase.GetCurrentMethod(), e);
            }
            return false;
        }



        private void deleteRelatedRecordsForIHasUploads(ICommonWithId entity)
        {
            IHasUploads ihasuploads = entity as IHasUploads;

            if (!continueProcessing(ihasuploads))
                return;

            List<string> lstUploadIdsToDelete = new List<string>();

            //delete the actual files 
            deletePhysicalUploadedFiles(ihasuploads, lstUploadIdsToDelete);

            //now delete the upload records
            deleteRelatedUploadRecords(lstUploadIdsToDelete);
            //this just clears the navigation.
            ihasuploads.MiscFiles.Clear();

        }

        /// <summary>
        /// This automatically deletes all related records for IHasUploads. 
        /// It also removes the uploaded files themselves.
        /// </summary>
        /// <param name="entity"></param>
        private void deleteRelatedRecordsForIHasUploadsAndSave(ICommonWithId entity)
        {
            deleteRelatedRecordsForIHasUploads(entity);
            SaveChanges();



        }

        /// <summary>
        /// This deletes the uploaded Records.
        /// </summary>
        /// <param name="lstUploadIdsToDelete"></param>
        private void deleteRelatedUploadRecords(List<string> lstUploadIdsToDelete)
        {
            if (lstUploadIdsToDelete.IsNullOrEmpty())
                return;

            foreach (var id in lstUploadIdsToDelete)
            {
                UploadedFileBiz.deleteActuallyEngine(id);

            }
        }

        /// <summary>
        /// This deletes the physically uploaded files.
        /// </summary>
        /// <param name="ihasuploads"></param>
        /// <param name="lstUploadIdsToDelete"></param>
        private static void deletePhysicalUploadedFiles(IHasUploads ihasuploads, List<string> lstUploadIdsToDelete)
        {
            foreach (var upload in ihasuploads.MiscFiles)
            {
                //delete the file
                File.Delete(upload.AbsolutePathWithFileName());

                //save the id to delete later.
                lstUploadIdsToDelete.Add(upload.Id);

            }

        }

        /// <summary>
        /// Checks to see if it is worth continuing
        /// </summary>
        /// <param name="ihasuploads"></param>
        /// <returns></returns>
        private bool continueProcessing(IHasUploads ihasuploads)
        {

            if (ihasuploads.IsNull())
                return false;

            if (ihasuploads.MiscFiles.IsNullOrEmpty())
                return false;
            return true;
        }

        public virtual void EVENT_DeleteRelatedRecordsActually(ICommonWithId entity)
        {
        }



        #endregion

        #region DeleteActually

        public async Task DeleteActuallyAndSaveAsync(TEntity entity)
        {
            DeleteActuallyEngine(entity);
            await SaveChangesAsync();
        }

        public void DeleteActuallyAndSave(TEntity entity)
        {
            DeleteActuallyEngine(entity);
            SaveChanges();
        }

        //------------------------------------------------------


        public void DeleteActuallyAndSave(string id)
        {
            id.IsNullThrowException("Id is blank");
            TEntity entity = Find(id);
            entity.IsNullThrowException("No entity found!");
            DeleteActuallyAndSave(entity);
        }

        public async Task DeleteActuallyAndSaveAsync(string id)
        {
            id.IsNullThrowException("Id is blank");
            TEntity entity = await FindAsync(id);
            entity.IsNullThrowException("No entity found!");
            await DeleteActuallyAndSaveAsync(entity);
        }

        //------------------------------------------------------



        public virtual void DeleteActuallyAllAndSave()
        {
            var lst = Dal.FindAll().ToList();
            deleteActuallyAll(lst);
            SaveChanges();
        }

        public virtual async Task DeleteActuallyAllAndSaveAsync()
        {
            var lst = await Dal.FindAll().ToListAsync();
            deleteActuallyAll(lst);
            await SaveChangesAsync();
        }

        //------------------------------------------------------






        private void deleteActuallyAll(List<TEntity> lst)
        {
            if (lst.IsNullOrEmpty())
                return;

            foreach (var entity in lst)
            {
                DeleteActuallyEngine(entity);
            }
        }


        private void deleteActuallyEngine(string id)
        {
            TEntity entity = Find(id);
            entity.IsNullThrowException("Entity not found!");
            DeleteActuallyEngine(entity);
        }
        public void DeleteActuallyEngine(TEntity entity)
        {
            entity.IsNullThrowException("Argument Exception.");
            deleteRelatedRecordsForIHasUploads(entity);
            EVENT_DeleteRelatedRecordsActually(entity);
            Dal.DeleteActually(entity);
        }


        #endregion

        #region Find

        public TEntity Find(string id)
        {
            return Dal.FindFor(id) as TEntity;
        }
        public async Task<TEntity> FindAsync(string id)
        {
            return (await Dal.FindForAsync(id)) as TEntity;
        }

        public IQueryable<TEntity> FindAll(bool deleted = false)
        {
            return Dal.FindAll(deleted) as IQueryable<TEntity>;
        }
        public IQueryable<TEntity> FindAllNoTracking(bool deleted = false)
        {
            return Dal.FindAllNoTracking(deleted) as IQueryable<TEntity>;
        }


        public async Task<List<TEntity>> FindAllAsync()
        {
            return await Dal.FindAllAsync();
        }
        public TEntity FindByName(string name)
        {
            return Dal.FindForName(name);

        }
        #endregion

        #region Save

        public virtual int SaveChanges()
        {


            try
            {
                return Dal.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                ErrorsGlobal.Add("DbEntityValidationException. Data not saved.", MethodBase.GetCurrentMethod(), e);

            }

            catch (NotSupportedException e)
            {

                ErrorsGlobal.Add("NotSupportedException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }


            catch (ObjectDisposedException e)
            {

                ErrorsGlobal.Add("ObjectDisposedException. Data not saved.", MethodBase.GetCurrentMethod(), e);

            }

            catch (InvalidOperationException e)
            {
                ErrorsGlobal.Add("InvalidOperationException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }

            catch (DbUpdateConcurrencyException e)
            {
                ErrorsGlobal.Add("DbUpdateConcurrencyException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }

            catch (DbUpdateException e)
            {
                ErrorsGlobal.Add("DbUpdateException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }

            catch (EntityException e)
            {
                ErrorsGlobal.Add("EntityException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }

            catch (DataException e)
            {
                ErrorsGlobal.Add("DataException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }

            catch (Exception e)
            {
                ErrorsGlobal.Add("Exception. Data not saved.", MethodBase.GetCurrentMethod(), e);

            }
            throw new Exception("Data not saved.");
        }
        public virtual async Task<int> SaveChangesAsync()
        {

            try
            {
                return await Dal.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                ErrorsGlobal.Add("DbEntityValidationException. Data not saved.", MethodBase.GetCurrentMethod(), e);

            }

            catch (NotSupportedException e)
            {

                ErrorsGlobal.Add("NotSupportedException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }


            catch (ObjectDisposedException e)
            {

                ErrorsGlobal.Add("ObjectDisposedException. Data not saved.", MethodBase.GetCurrentMethod(), e);

            }

            catch (InvalidOperationException e)
            {
                ErrorsGlobal.Add("InvalidOperationException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }

            catch (DbUpdateConcurrencyException e)
            {
                ErrorsGlobal.Add("DbUpdateConcurrencyException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }

            catch (DbUpdateException e)
            {
                ErrorsGlobal.Add("DbUpdateException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }

            catch (EntityException e)
            {
                ErrorsGlobal.Add("EntityException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }

            catch (DataException e)
            {
                ErrorsGlobal.Add("DataException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }

            catch (Exception e)
            {
                ErrorsGlobal.Add("Exception. Data not saved.", MethodBase.GetCurrentMethod(), e);

            }
            throw new Exception("Data not saved.");

        }


        #endregion


        public virtual bool AddEntryToIndex { get; set; }

        /// <summary>
        /// Use this to add different fields... such as Image.
        /// </summary>
        /// <param name="indexItem"></param>
        public virtual void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, ICommonWithId icommonWithId)
        {
            AddEntryToIndex = true;
            IHasUploads ientityHasUploads = icommonWithId as IHasUploads;

            if (!ientityHasUploads.IsNull())
            {
                indexItem.ImageAddressStr = addressOfImageForDisplay(ientityHasUploads);


            }

            //LikeUnlikeParameter likeUnlikeCounter = _likeUnlikeBiz.Count(indexItem.Id, null null, null, null, theUserId);
            //indexItem.MenuManager = new MenuManager(null, null, null, MenuENUM.IndexDefault, _breadCrumbManager, likeUnlikeCounter);


            ////add the MenuManager and BreadCrumbManager
            //if (indexItem.MenuManager.IsNull())
            //{
            //    indexItem.MenuManager = indexListVM.MenuManager;


            //}

            //if (icommonWithId.MenuManager.IsNull())
            //{
            //    icommonWithId.MenuManager = new MenuManager()
            //}

        }

        private string addressOfImageForDisplay(IHasUploads entity)
        {

            if (entity.MiscFiles.IsNullOrEmpty())
                return new UploadedFile().RelativePathWithFileName();

            //Get a list of images for this category item.
            UploadedFile image = entity.MiscFiles.FirstOrDefault(x => x.MetaData.IsDeleted == false);

            if (image.IsNull())
                image = new UploadedFile();


            return image.RelativePathWithFileName();

        }


        public virtual void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            //note... edit is also controlled from entity.MetaData.IsEditLocked in the entity metaData controlled through 
            //Event_LockEdit which fires during Creation.
            //indexListVM.Heading_Column = "Discount Precedence Rules ( Type-Rule-Rank)";

            indexListVM.Show.MoveUpMoveDown(false);
            indexListVM.Show.EditDeleteAndCreate = false;
            indexListVM.IsImageTiled = false;
            indexListVM.NameInput1 = "Name";
            indexListVM.NameInput2 = "";
            indexListVM.NameInput3 = "";

            indexListVM.Heading.Main = string.Format("{0}", typeof(TEntity).Name.ToSentence().ToTitleCase());
            indexListVM.Heading.Column = "All Items";
            indexListVM.Heading.Small = "List";

            //indexListVM.MainHeading = string.Format("{0}", typeof(TEntity).Name.ToSentence().ToTitleCase());
            //indexListVM.Heading_Column = "All Items";
            //indexListVM.SmallHeading = "List";
            //indexListVM.SmallHeading = string.Format("{0}", typeof(TEntity).Name.ToSentence().ToTitleCase());

            indexListVM.Heading.RecordName = indexListVM.Heading.Main; //This print as Number of Records.
            indexListVM.Heading.RecordNamePlural = indexListVM.Heading.Main + "s";

            //Note. The LikeCounter in the MenuManger for the List is always null. The likes are counted in
            //the items.
            indexListVM.MenuManager = new MenuManager(null, null, null, MenuENUM.IndexDefault, BreadCrumbManager, null, UserId, parameters.ReturnUrl);



        }

        /// <summary>
        /// This sets up the item to be entered. It enters the start date and time.
        /// This is added in the GET part of Create.
        /// </summary>
        /// <returns></returns>
        public virtual ICommonWithId FactoryForHttpGet()
        {

            return Factory();
        }

        //public virtual TEntity EntityFactoryForHttpGet(FactoryParameters fp)
        //{
        //    return EntityFactoryForHttpGet();
        //}

        public virtual ICommonWithId Factory()
        {
            ICommonWithId entity = Dal.Factory();
            entity.MetaData.Created.SetToTodaysDateStart(UserName, UserId);
            entity.MetaData.Created.SetToTodaysDate(UserName, UserId);

            Product p = entity as Product;
            MenuPathMain mpm = entity as MenuPathMain;
            string returnUrl = "";
            entity.MenuManager = new MenuManager(mpm, p, null, MenuENUM.CreateDefault, BreadCrumbManager, new LikeUnlikeParameters(0, 0, "Initialization in Factory"), UserId, returnUrl);

            return entity;
        }

        public string CopyFile(string sourcePath, string targetPath, string nameOfSourceFile)
        {
            string sourcePathMapped = GetMappedPath(sourcePath);
            string targetPathMapped = GetMappedPath(targetPath);

            return AliKuli.ToolsNS.FileTools.CopyFileAndGiveNewName(sourcePathMapped, targetPathMapped, nameOfSourceFile);

        }


        /// <summary>
        /// this gets updated in checkIfFileExists
        /// </summary>
        protected string ExtentionFound { get; set; }

        /// <summary>
        /// If extention is found, it is returned in the property ExtentionFound. Supported image formats are 
        /// .jpg .png .tiff .jpeg .bmp .pdf
        /// </summary>
        /// <param name="fileNameNoExtention"></param>
        /// <param name="filenameWithoutExtention"></param>
        /// <returns></returns>
        protected bool imageFileExists(string fileNameNoExtention)
        {

            if (fileNameNoExtention.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.Add("fileNameNoExtention is null", MethodBase.GetCurrentMethod());
                throw new ArgumentException(ErrorsGlobal.ToString());
            }

            bool success = checkIfFileExists(fileNameNoExtention, ".jpg");
            if (success)
                return true;

            success = checkIfFileExists(fileNameNoExtention, ".png");
            if (success)
                return true;

            success = checkIfFileExists(fileNameNoExtention, ".TIFF");
            if (success)
                return true;

            success = checkIfFileExists(fileNameNoExtention, ".JPEG");
            if (success)
                return true;

            success = checkIfFileExists(fileNameNoExtention, ".BMP");
            if (success)
                return true;

            success = checkIfFileExists(fileNameNoExtention, ".PDF");
            if (success)
                return true;

            return false;

        }

        protected bool txtFileExists(string fileNameNoExtention)
        {
            if (fileNameNoExtention.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.Add("fileNameNoExtention is null", MethodBase.GetCurrentMethod());
                throw new ArgumentException(ErrorsGlobal.ToString());
            }

            return checkIfFileExists(fileNameNoExtention, ".txt");
        }

        /// <summary>
        /// If this returns true then it also adds a value to the property pathFileName_with_Extention;
        /// </summary>
        /// <param name="nameAndLocation_With_Extention"></param>
        /// <returns></returns>
        private bool checkIfFileExists(string pathAndname, string extention)
        {
            string nameAndLocation_With_Extention = Path.ChangeExtension(pathAndname, extention);
            if (AliKuli.ToolsNS.FileTools.IsExistFile(nameAndLocation_With_Extention))
            {
                ExtentionFound = extention;
                return true;

            }
            return false;
        }




        public virtual string GetClassName()
        {
            return Dal.GetClassName;
        }



        public string GetMappedPath(string relativePath)
        {
            //return HttpContext.Current.Server.MapPath(relativePath);
            return FileTools.GetAbsolutePath(relativePath);
        }

        public VerificationIconResult GetVerificationIconResult(VerificaionStatusENUM verificationStatus)
        {
            VerificationIconResult result = new VerificationIconResult();

            switch (verificationStatus)
            {
                case VerificaionStatusENUM.Unknown:
                case VerificaionStatusENUM.NotVerified:
                    result.IconAddress = VerificationConfig.NotVerifiedIcon;
                    result.ToolTip = VerificationToolTipConstants.NOTVERIFIED_ICON;
                    return result;

                case VerificaionStatusENUM.Failed:
                    result.IconAddress = VerificationConfig.FailedIcon;
                    result.ToolTip = VerificationToolTipConstants.FAILED_ICON;
                    return result;

                case VerificaionStatusENUM.Printed:
                    result.IconAddress = VerificationConfig.PrintedIcon;
                    result.ToolTip = VerificationToolTipConstants.PRINTED_ICON;
                    return result;

                case VerificaionStatusENUM.Mailed:
                    result.IconAddress = VerificationConfig.MailedIcon;
                    result.ToolTip = VerificationToolTipConstants.MAILED_ICON;
                    return result;

                case VerificaionStatusENUM.Requested:
                    result.IconAddress = VerificationConfig.RequestIconIcon;
                    result.ToolTip = VerificationToolTipConstants.REQUESTED_ICON;
                    return result;

                case VerificaionStatusENUM.SelectedForProcessing:
                    result.IconAddress = VerificationConfig.InproccessIcon;
                    result.ToolTip = VerificationToolTipConstants.INPROCCESS_ICON;
                    return result;

                case VerificaionStatusENUM.Verified:
                    result.IconAddress = VerificationConfig.VerifiedIcon;
                    result.ToolTip = VerificationToolTipConstants.VERIFIED_ICON;
                    return result;

                default:
                    result.IconAddress = VerificationConfig.NotVerifiedIcon;
                    result.ToolTip = VerificationConfig.NotVerifiedIcon;
                    return result;
            }
        }

        /// <summary>
        /// Sometimes we need to do different things when we are creating
        /// </summary>
        public bool IsCreate { get; set; }
        public bool IsHasUploads
        {
            get
            {
                return !IHasUploadsEntity.IsNull();

            }
        }


        public bool IsUserHasUploads
        {
            get
            {
                return !IUserHasUploadsEntity.IsNull();

            }
        }


        public IHasUploads IHasUploadsEntity
        {
            get
            {
                Type t = typeof(TEntity);
                var entity = Activator.CreateInstance(t);
                IHasUploads i = entity as IHasUploads;
                return i;
            }

        }

        public IHasUploads IUserHasUploadsEntity
        {
            get
            {
                Type t = typeof(TEntity);
                var entity = Activator.CreateInstance(t);
                IUserHasUploads i = entity as IUserHasUploads;
                return i;
            }

        }
        /// <summary>
        /// Fix any records that need fixing before applying business rules, saving and error checking here.
        /// Default. 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Fix(ControllerCreateEditParameter parm)
        {


        }



        public async Task<IndexListVM> IndexAsync(ControllerIndexParams parameters)
        {
            //this is where the error is.
            var lstEntities = await GetListForIndexAsync(parameters);

            //this is where the list is created
            IndexListVM indexListVM = createIndexListAndGiveNamesToColumns_Helper(parameters, lstEntities);

            return indexListVM;
        }



        public virtual IList<ICommonWithId> GetListForIndex()
        {
            IList<ICommonWithId> lstEntities = FindAll().ToList() as IList<ICommonWithId>;

            IList<ICommonWithId> lstIcom = lstEntities.Cast<ICommonWithId>().ToList();
            return lstIcom;
        }

        public virtual async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parameters)
        {
            var lstEntities = await FindAllAsync();
            IList<ICommonWithId> lstIcom = lstEntities.Cast<ICommonWithId>().ToList();
            return lstIcom;
        }






        /// <summary>
        /// This is the helper function for the GetIndexList and GetIndexListAsync
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="lstEntities"></param>
        /// <returns></returns>
        private IndexListVM createIndexListAndGiveNamesToColumns_Helper(ControllerIndexParams parameters, IList<ICommonWithId> lstEntities)
        {
            IndexListVM indexListVM = createIndexList(lstEntities, parameters);

            if (indexListVM.IsNull())
                indexListVM = new IndexListVM(parameters);

            return indexListVM;
        }


        /// <summary>
        /// This creates a list which is later used in the index method.
        /// The List is 
        ///     sorted as per the ControllerIndexParams.SortBy.
        ///     filtered as per the ControllerIndexParams.SearchFor. 
        /// Note. There are 3 different MenuManagers here.
        ///     1. IndexList.MenuManager: Used globally in the view.
        ///     2. IndexItem.MenuManger: Used in the index
        ///     3. entity.MenuManager: This is used in the next secreen of Edit.
        /// </summary>
        /// <param name="lstEntities"></param>
        /// <returns></returns>
        private IndexListVM createIndexList(IList<ICommonWithId> lstEntities, ControllerIndexParams parameters)
        {



            //This names the sort links. They come directly from the entity
            parameters.DudEntity = Dal.Factory();
            //if (parameters.Entity.IsNull())
            //    parameters.Entity = parameters.DudEntity;
            IndexListVM indexListVM = new IndexListVM(parameters);


            Event_ModifyIndexList(indexListVM, parameters);

            if (lstEntities.IsNullOrEmpty())
            {
                return indexListVM;
            }

            foreach (var entity in lstEntities)
            {

                try
                {
                    string id = entity.Id;
                    string fullName = entity.FullName();
                    string input1SortString = entity.Input1SortString.IsNullOrWhiteSpace() ? "" : entity.Input1SortString; ;
                    string input2SortString = entity.Input2SortString.IsNullOrWhiteSpace() ? "" : entity.Input2SortString; ;
                    string input3SortString = entity.Input3SortString.IsNullOrWhiteSpace() ? "" : entity.Input3SortString;
                    bool isEditLocked = entity.MetaData.IsEditLocked;
                    string detailInfoToDisplayOnWebsite = entity.DetailInfoToDisplayOnWebsite.IsNullOrWhiteSpace() ? "" : entity.DetailInfoToDisplayOnWebsite;
                    decimal price = 0;
                    VerificaionStatusENUM addressVerificaionEnum = doVerificationWork(entity);

                    IndexItemVM indexItem = new IndexItemVM(
                        id,
                        fullName,
                        input1SortString,
                        input2SortString,
                        input3SortString,
                        isEditLocked,
                        detailInfoToDisplayOnWebsite,
                        addressVerificaionEnum,
                        price);

                    //indexItem.MenuManager = indexListVM.MenuManager as IMenuManager;

                    indexItem.VerificationIconResult = GetVerificationIconResult(indexItem.VerificationStatus);

                    //add menuManager if not a menu
                    //if(!parameters.IsMenu)
                    //    indexItem.MenuManager = getMenuManager(parameters);
                    //this is a different entity from the one in parameters
                    if (entity.MenuManager.IsNull())
                        InitializeMenuManagerForEntity(parameters, entity);
                    
                    entity.MenuManager.ReturnUrl = parameters.ReturnUrl;
                    //image address is added in here.
                    Event_ModifyIndexItem(indexListVM, indexItem, entity);

                    //return URL is added here to the item. This will be used in the Edit / Create
                    entity.MenuManager.ReturnUrl = parameters.ReturnUrl;

                    if (AddEntryToIndex)
                        if (!indexItem.IsNull())
                            indexListVM.Add(indexItem);

                }
                catch (Exception e)
                {

                    ErrorsGlobal.Add("There was an error during creating index", MethodBase.GetCurrentMethod(), e);
                }

            }

            if (ErrorsGlobal.HasErrors)
                throw new Exception(ErrorsGlobal.ToString());


            return indexListVM;
        }


        public void InitializeMenuManagerForEntity(ControllerIndexParams parm)
        {


            TEntity entity = (TEntity)parm.Entity;
            InitializeMenuManagerForEntity(parm, entity);

        }

        //private static void fixReturnUrl(ControllerIndexParams parm, TEntity entity)
        //{

        //}

        public virtual void InitializeMenuManagerForEntity(ControllerIndexParams parm, ICommonWithId entity)
        {
            if (entity.MenuManager.IsNull())
            {
                switch (parm.Menu.MenuEnum)
                {
                    case MenuENUM.IndexMenuPath1:
                    case MenuENUM.IndexMenuPath2:
                    case MenuENUM.IndexMenuPath3:
                        //Item is MenuPathMain
                        entity.MenuManager = new MenuManager(entity as MenuPathMain, null, null, parm.Menu.MenuEnum, BreadCrumbManager, parm.LikeUnlikeCounter, UserId, parm.ReturnUrl);
                        break;

                    case MenuENUM.IndexMenuProduct:
                        //item is product
                        entity.MenuManager = new MenuManager(null, entity as Product, null, parm.Menu.MenuEnum, BreadCrumbManager, parm.LikeUnlikeCounter, UserId, parm.ReturnUrl);
                        break;

                    case MenuENUM.IndexMenuProductChild:
                        //item is productChild
                        entity.MenuManager = new MenuManager(null, null, entity as ProductChild, parm.Menu.MenuEnum, BreadCrumbManager, parm.LikeUnlikeCounter, UserId, parm.ReturnUrl);
                        break;

                    case MenuENUM.IndexMenuProductChildLandingPage:
                        //item is productChild
                        entity.MenuManager = new MenuManager(null, null, entity as ProductChild, parm.Menu.MenuEnum, BreadCrumbManager, parm.LikeUnlikeCounter, UserId, parm.ReturnUrl);
                        break;
                    default:
                        entity.MenuManager = new MenuManager(null, null, null, parm.Menu.MenuEnum, BreadCrumbManager, parm.LikeUnlikeCounter, UserId, parm.ReturnUrl);
                        break;
                }


            }
            entity.MenuManager.ReturnUrl = parm.ReturnUrl;
        }

        public void InitializeMenuManager(ControllerCreateEditParameter parmIn)
        {
            ControllerIndexParams parm = parmIn.ConvertToControllerIndexParams();
            InitializeMenuManagerForEntity(parm);
        }

        /// <summary>
        /// We need to do this for those classes which use he index which has pictures but is not
        /// a menu. Example MenuPath1, MenuPath2, MenuPath3, Product, ProductChild
        /// </summary>
        /// <param name="menuENUM"></param>
        /// <returns></returns>
        //        private IMenuManager getMenuManager(ControllerIndexParams param)
        //        {

        //            switch (param.Menu.MenuEnum)
        //            {
        //                case MenuENUM.IndexDefault:
        //                    //this is a MenuPath1
        ////                    return new MenuManager(param.me)
        //                    break;
        //                case MenuENUM.IndexMenuPath1:
        //                    break;
        //                case MenuENUM.IndexMenuPath2:
        //                    break;
        //                case MenuENUM.IndexMenuPath3:
        //                    break;
        //                case MenuENUM.IndexMenuProduct:
        //                    break;
        //                case MenuENUM.IndexMenuProductChild:
        //                    break;
        //                case MenuENUM.IndexMenuProductChildLandingPage:
        //                    break;
        //                case MenuENUM.Unknown:
        //                case MenuENUM.EditDefault:
        //                case MenuENUM.EditMenuPath1:
        //                case MenuENUM.EditMenuPath2:
        //                case MenuENUM.EditMenuPath3:
        //                case MenuENUM.EditMenuPathMain:
        //                case MenuENUM.EditMenuProduct:
        //                case MenuENUM.EditMenuProductChild:
        //                case MenuENUM.CreateDefault:
        //                case MenuENUM.CreateMenuPath1:
        //                case MenuENUM.CreateMenuPath2:
        //                case MenuENUM.CreateMenuPath3:
        //                case MenuENUM.CreateMenuPathMenuPathMain:
        //                case MenuENUM.CreateMenuProduct:
        //                case MenuENUM.CreateMenuProductChild:
        //                default:
        //                    throw new NotImplementedException("Switch statement Not implemented");
        //            }
        //        }

        private static VerificaionStatusENUM doVerificationWork(ICommonWithId entity)
        {
            IAmVerified iAmVerified = entity as IAmVerified;
            VerificaionStatusENUM addressVerificaionEnum = VerificaionStatusENUM.Unknown;

            if (iAmVerified.IsNull())
                return addressVerificaionEnum;

            return iAmVerified.VerificationStatusEnum;
        }



        /// <summary>
        /// entity.MetaData.IsEditLocked = true;
        /// </summary>
        /// <param name="entity"></param>
        public virtual bool Event_LockEditDuringInitialization()
        {
            return true;
        }


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

                TEntity tentity = Factory() as TEntity;
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
                throw new Exception(ErrorsGlobal.ToString());
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
        protected void saveAndCopyUploadedFile(TEntity entity, IHasUploads entityHasUploads, string originalnameWithoutExtention, string relative_SrcPath, string relative_targetPath)
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
        protected static string getFileNameWithoutExtention(string relative_SrcPath, string originalname)
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


        public byte[] PrintIndex(IndexListVM indexListVM)
        {
            //PdfInvoiceData data = new PdfInvoiceData();

            IndexPdfParameter parm = new IndexPdfParameter(indexListVM);
            //parm.Logo.Address = System.Web.HttpContext.Current.Server.MapPath(@"..\Content\MyImages\raddicco.jpg");

            var factory = new IndexFactory();
            byte[] pdf = factory.Build(parm);

            return pdf;


        }



        public byte[] PrintInvoice()
        {
            PdfInvoiceData data = new PdfInvoiceData();

            InvoicPdfParameter parm = data.Load(System.Web.HttpContext.Current.Server.MapPath(AliKuli.ConstantsNS.MyConstants.LOGO_LOCATION));
            //parm.Logo.Address = System.Web.HttpContext.Current.Server.MapPath(@"..\Content\MyImages\raddicco.jpg");

            var factory = new InvoiceFactory();
            byte[] pdf = factory.Build(parm);

            return pdf;


        }





        /// <summary>
        /// This loads the Language Data into the select list
        /// </summary>
        /// <returns></returns>
        public virtual SelectList SelectList()
        {
            SelectList selectList = getSelectListFromCache();
            if (selectList.IsNull())
            {
                selectList = Dal.SelectList();
                storeIntoCache(selectList);
            }
            return selectList;
        }

        //public SelectList UsersSelectList()
        //{
        //    SelectList selectList = getSelectListFromCache();
        //    if (selectList.IsNull())
        //    {
        //        selectList = _userBiz.SelectList();
        //        storeIntoCache(selectList);
        //    }
        //    return selectList;

        //}

        public abstract string SelectListCacheKey { get; }

        private SelectList getSelectListFromCache()
        {

            object obj = MemoryMain.CacheMemory.GetFrom(SelectListCacheKey);
            ErrorsGlobal.AddMessage("Select List Cache Accessed.", MethodBase.GetCurrentMethod());
            return (SelectList)obj;
        }

        public void ClearSelectListInCache(string locationName)
        {
            MemoryMain.CacheMemory.ClearFor(locationName);
        }

        private void storeIntoCache(SelectList selectList)
        {
            MemoryMain.CacheMemory.Add(SelectListCacheKey, selectList);
        }


        public dynamic SelectListJson()
        {
            throw new NotImplementedException();
        }

        public SelectList SelectList_Engine(IQueryable<TEntity> data)
        {
            return Dal.SelectList_Engine(data);
        }


        /// <summary>
        /// The file UploadObject has all the machinery to handle the upload and save of the file. It then also creates
        /// the UploadFile Class, puts all the files in a list and we can then use it to save it into the db. To
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>


        public void SaveUploadedFiles(List<UploadedFile> lstUploadedFile, TEntity entity, ICollection<UploadedFile> navigation, IUserHasUploadsTypeENUM iuserHasUploadsTypeEnum = IUserHasUploadsTypeENUM.None)
        {
            if (!lstUploadedFile.IsNullOrEmpty())
            {
                foreach (UploadedFile file in lstUploadedFile)
                {
                    file.MetaData.Created.SetToTodaysDate(UserName, UserId);


                    //initializes navigation if it is null
                    if (navigation.IsNull())
                        navigation = new List<UploadedFile>(); //intializing

                    navigation.Add(file);

                    //You need to add a refrence here to save the file in the UploadedFile as well.
                    //the code will ofcourse be in the calling class.
                    AddEntityRecordIntoUpload(file, entity as TEntity, iuserHasUploadsTypeEnum);
                    UploadedFileBiz.CreateSimple(CreateControllerCreateEditParameter(file as ICommonWithId));

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
            //var f = new System.IO.FileInfo(uploadedFile.AbsolutePathWithFileName());
            //f.Delete();

            DeletePhysicalFile(uploadedFile.AbsolutePathWithFileName());

        }

        //This removes the file from the actual system
        public void DeletePhysicalFile(string absolutePathWithFileName)
        {
            var f = new System.IO.FileInfo(absolutePathWithFileName);
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
        //            UploadedFileBiz.Create(file);

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
        //            UploadedFileBiz.Create(file);

        //        }
        //    }
        //}

        /// <summary>
        /// Example
        ///    uploadFile.FileDoc = filedoc;
        ///    uploadFile.FileDoc.Id = filedoc.FileDoc.Id;

        /// </summary>
        /// <param name="uploadFile"></param>
        /// <param name="entity"></param>
        public virtual void AddEntityRecordIntoUpload(UploadedFile uploadedFile, TEntity entity, IUserHasUploadsTypeENUM iuserHasUploadsTypeEnum)
        {

            ErrorsGlobal.Add(string.Format("AddEntityRecordIntoUpload not implimented ."), MethodBase.GetCurrentMethod());
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
        private void handleRelatedFilesIfExist(ControllerCreateEditParameter parm)
        {
            getFilesForIhasUploads(parm);
            getFilesForIUserHasUploads(parm);
            HandleRelatedRecord(parm);


        }

        /// <summary>
        /// Handle related record activity here.
        /// </summary>
        /// <param name="parm"></param>
        public virtual void HandleRelatedRecord(ControllerCreateEditParameter parm)
        {

        }


        private void getFilesForIUserHasUploads(ControllerCreateEditParameter parm)
        {


            if (parm.IsIUserHasUploads)
            {
                parm.SelfieUpload.FileLocationConst = parm.Entity_IUserHasUploads.SelfieLocationConst(UserName);

                parm.IdCardFront.FileLocationConst = parm.Entity_IUserHasUploads.IdCardFrontLocationConst(UserName);
                parm.IdCardBack.FileLocationConst = parm.Entity_IUserHasUploads.IdCardBackLocationConst(UserName);

                parm.PassportFront.FileLocationConst = parm.Entity_IUserHasUploads.PassportFrontLocationConst(UserName);
                parm.PassportVisa.FileLocationConst = parm.Entity_IUserHasUploads.PassportVisaLocationConst(UserName);

                parm.LiscenseFront.FileLocationConst = parm.Entity_IUserHasUploads.LiscenseFrontLocationConst(UserName);
                parm.LiscenseBack.FileLocationConst = parm.Entity_IUserHasUploads.LiscenseBackLocationConst(UserName);

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
                parm.MiscUploadedFiles.FileLocationConst = parm.Entity_IHasUploads.MiscFilesLocation();

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



        public Person GetPersonForUser(string userId, List<Person> people)
        {
            List<Person> found = ListOfPeopleForUser(userId, people);
            if (found.IsNullOrEmpty())
                return null;

            if (found.Count > 1)
            {
                string nameStr = "";

                foreach (var p in found)
                    nameStr += string.Format("{0}; ", p.Name);

                throw new Exception(string.Format("There are more than one person for this user! '{0}'", nameStr));
            }

            return found.FirstOrDefault();
        }




        /// <summary>
        /// This finds the list of persons with the supplied uerId. Normally, a full list of persons
        /// needs to be supplied
        /// </summary>
        /// <param name="userId">The UserId we want</param>
        /// <param name="people">The list of people, normally the full list</param>
        /// <returns></returns>
        public List<Person> ListOfPeopleForUser(string userId, List<Person> people)
        {
            try
            {
                if (people.IsNullOrEmpty())
                    return null;

                //flatten the list of people and users
                List<PersonUserFlatFile> personUserFlatFile = flattenPersonUser(people);

                if (personUserFlatFile.IsNull())
                    return null;


                //locate person from the flat file user the User's Id
                List<PersonUserFlatFile> peopleFoundFlatFile = personUserFlatFile.Where(x => x.UserId == UserId).ToList();

                if (peopleFoundFlatFile.IsNullOrEmpty())
                    return null;

                //Now list only contains those users that have this person. There should always be one or none.

                List<Person> found = new List<Person>();
                //Add the list of people found into a list
                foreach (PersonUserFlatFile personUserIds in peopleFoundFlatFile)
                {
                    Person personForUser = people.FirstOrDefault(x => x.Id == personUserIds.PersonId);
                    personForUser.IsNullThrowException("Person not found. Programming error.");
                    found.Add(personForUser);
                }
                return found;
            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Unable to continue", MethodBase.GetCurrentMethod(), e);
                throw new Exception(ErrorsGlobal.ToString());
            }

        }

        private List<PersonUserFlatFile> flattenPersonUser(List<Person> listPerson)
        {
            if (listPerson.IsNullOrEmpty())
                return null;

            List<PersonUserFlatFile> listPersonUserFlatFile = new List<PersonUserFlatFile>();
            foreach (var person in listPerson)
            {
                if (person.Users.IsNullOrEmpty())
                    continue;

                foreach (var userInPerson in person.Users)
                {
                    PersonUserFlatFile pu = new PersonUserFlatFile();
                    pu.PersonId = person.Id;
                    pu.UserId = userInPerson.Id;
                    listPersonUserFlatFile.Add(pu);

                }
            }

            return listPersonUserFlatFile;
        }


        public virtual List<string> GetPictureList(IHasUploads ihasUploads)
        {
            List<string> addresses = new List<string>();

            if (ihasUploads.MiscFiles.Any(x => !x.MetaData.IsDeleted))
            {
                var lstUploadedFiles = ihasUploads.MiscFiles.Where(x => !x.MetaData.IsDeleted).ToList();
                lstUploadedFiles.IsNullOrEmptyThrowException("Something went worng. This list cannot be empty.");
                foreach (UploadedFile uploadFile in lstUploadedFiles)
                {
                    string pictureAddy = getImageAddressOf(uploadFile);
                    if (!pictureAddy.IsNullOrWhiteSpace())
                    {
                        addresses.Add(pictureAddy);

                    }
                }
            }

            if (addresses.IsNullOrEmpty())
            {
                return GetDefaultPicture();
            }

            return addresses;
        }

        //this gets the default picture
        public List<string> GetDefaultPicture()
        {

            UploadedFile upf = new UploadedFile();
            string defaultPictureAddress = upf.RelativePathWithFileName();
            //just load the black screen here
            //menuManager.PictureAddresses.Add(defaultPictureAddress);
            List<string> lst = new List<string>();
            lst.Add(defaultPictureAddress);
            return lst;
        }

        protected string getImageAddressOf(UploadedFile _uf)
        {
            if (_uf.IsNull())
                _uf = new UploadedFile();

            return _uf.RelativePathWithFileName();
        }


    }
}
