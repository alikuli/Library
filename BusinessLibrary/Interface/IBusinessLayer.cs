using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Threading.Tasks;
using System.Web;

namespace UowLibrary.Interface
{
    /// <summary>
    /// This is the basic CRUD all UOW except Users will follow.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBusinessLayer<TEntity> where TEntity : class, ICommonWithId
    {
        void Create(TEntity entity);
        //Task CreateAsync(TEntity entity);
        void Create(ControllerCreateEditParameter param);
        //Task CreateAsync(TEntity entity, HttpPostedFileBase[] files);


        void CreateAndSave(TEntity entity);
        Task CreateAndSaveAsync(TEntity entity);
        void CreateAndSave(ControllerCreateEditParameter param);
        Task CreateAndSaveAsync(ControllerCreateEditParameter param);


        TEntity EntityFactoryForHttpGet();
        void BusinessRulesFor(TEntity entity);
        bool Delete(string id);
        Task<bool> DeleteAsync(string id);
        void DeleteAll();
        Task DeleteAllAsync();

        void ErrorCheck(TEntity entity);
        TEntity Factory();
        TEntity Find(string id);
        Task<TEntity> FindAsync(string id);
        void Fix(TEntity entity);
        void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters);
        //IndexListVM Index(ControllerIndexParams parameters);
        Task<IndexListVM> IndexAsync(ControllerIndexParams parameters);
        void SaveAfterEveryAddition(bool isSavingAfterEveryAddition);
        System.Web.Mvc.SelectList SelectList();
        dynamic SelectListJson();
        void UpdateAndSave(ControllerCreateEditParameter param);
        Task UpdateAndSaveAsync(ControllerCreateEditParameter param);
        string UserNameBiz { get; }
        string UserIdBiz { get;  }
        Task InitializationDataAsync();

    }
}
