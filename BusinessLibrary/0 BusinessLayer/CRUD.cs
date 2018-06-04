using AliKuli.Extentions;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UowLibrary.Abstract;
using UowLibrary.Interface;

namespace UowLibrary
{
    /// <summary>
    /// This is where all the Fix, bussiness Rules and ErrorChecks are implemented
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class BusinessLayer<TEntity> : AbstractBiz, IBusinessLayer<TEntity> where TEntity : class, ICommonWithId
    {


        #region Create

        public virtual void Create(TEntity entity)
        {
            createEngineSimple(entity);
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

        public virtual void CreateAndSave(TEntity entity)
        {
            createEngineSimple(entity);
            SaveChanges();
        }

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
                CreateAndSave(entity);
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
            fixEntityAndBussinessRulesAndErrorCheck_Helper(parm.Entity as TEntity);
            handleRelatedFilesIfExist(parm);
            createEngineSimple(parm.Entity as TEntity);
        }

        private void createEngineSimple(TEntity entity)
        {
            fixEntityAndBussinessRulesAndErrorCheck_Helper(entity);
            Dal.Create(entity);
            ClearSelectListInCache(SelectListCacheKey);
        }


        private void fixEntityAndBussinessRulesAndErrorCheck_Helper(TEntity entity)
        {
            Fix(entity);
            BusinessRulesFor(entity);
            ErrorCheck(entity);
        }

        #endregion

        #region Update

        public void UpdateAndSave(ControllerCreateEditParameter param)
        {
            updateEngine(param);
            Dal.SaveChanges();

        }



        public async Task UpdateAndSaveAsync(ControllerCreateEditParameter parm)
        {
            updateEngine(parm);
            await Dal.SaveChangesAsync();

        }

        private void updateEngine(ControllerCreateEditParameter parm)
        {

            fixEntityAndBussinessRulesAndErrorCheck_Helper(parm.Entity as TEntity);
            handleRelatedFilesIfExist(parm);
            Dal.Update(parm.Entity as TEntity);
            ClearSelectListInCache(SelectListCacheKey);

        }




        #endregion

        #region Delete

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
        public virtual bool Delete(string id)
        {
            TEntity entity = Dal.FindFor(id);
            try
            {
                if (entity.IsNull())
                {
                    ErrorsGlobal.Add(string.Format("Unable to find the {0} record", typeof(TEntity).Name.ToUpper()), "Delete");
                    ErrorsGlobal.AddMessage(string.Format("*** NOT Deleted ***"));
                    return false;
                }
                _nameOfItemBeingDeleted = entity.Name; //NameOfItemBeingDeleted is updated here
                Dal.Delete(id);
                Dal.SaveChanges();
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
                await Dal.SaveChangesAsync();
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



        /// <summary>
        /// This automatically deletes all related records for IHasUploads. 
        /// It also removes the uploaded files themselves.
        /// </summary>
        /// <param name="entity"></param>
        private void deleteRelatedRecordsForIHasUploadsAndSave(ICommonWithId entity)
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

            Dal.SaveChanges();



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
                _uploadedFileBiz.DeleteActuallyAndSave(id);

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
            deleteActuallyEngine(entity);
            await SaveChangesAsync();
        }

        public void DeleteActuallyAndSave(TEntity entity)
        {
            deleteActuallyEngine(entity);
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
                deleteActuallyEngine(entity);
            }
        }


        private void deleteActuallyEngine(TEntity entity)
        {
            entity.IsNullThrowException("Argument Exception.");
            deleteRelatedRecordsForIHasUploadsAndSave(entity);
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


        public TEntity FindByName(string name)
        {
            return Dal.FindForName(name);

        }
        #endregion

        #region Save

        public virtual int SaveChanges()
        {
            return Dal.SaveChanges();
        }
        public virtual async Task<int> SaveChangesAsync()
        {
            return await Dal.SaveChangesAsync();
        }


        #endregion

    }
}
