using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UowLibrary.Interface
{
    /// <summary>
    /// This is the basic CRUD all UOW except Users will follow.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBusinessLayer<TEntity> where TEntity : class, ICommonWithId
    {
        void Create(ControllerCreateEditParameter parm);
        //Task CreateAsync(TEntity entity);
        //void Create(ControllerCreateEditParameter param);
        //Task CreateAsync(TEntity entity, HttpPostedFileBase[] files);


        //void CreateAndSave(TEntity entity);
        //Task CreateAndSaveAsync(TEntity entity);
        void CreateAndSave(ControllerCreateEditParameter param);
        Task CreateAndSaveAsync(ControllerCreateEditParameter param);


        ICommonWithId EntityFactoryForHttpGet();
        void BusinessRulesFor(ControllerCreateEditParameter parm);

        bool DeleteAndSave(string id);
        Task<bool> DeleteAsync(string id);
        void DeleteActuallyAllAndSave();
        Task DeleteActuallyAllAndSaveAsync();

        void ErrorCheck(ControllerCreateEditParameter parm);
        ICommonWithId Factory();

        TEntity Find(string id);
        Task<TEntity> FindAsync(string id);

        void Fix(ControllerCreateEditParameter parm);
        void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters);
        //IndexListVM Index(ControllerIndexParams parameters);
        Task<IndexListVM> IndexAsync(ControllerIndexParams parameters);
        //void SaveAfterEveryAddition(bool isSavingAfterEveryAddition);

        System.Web.Mvc.SelectList SelectList();
        dynamic SelectListJson();

        void UpdateAndSave(ControllerCreateEditParameter param);
        Task UpdateAndSaveAsync(ControllerCreateEditParameter param);

        //string UserNameBiz { get; }
        //string UserIdFromBiz { get; }

        void InitializationData();

        void Detach(TEntity entity);
        void Attach(TEntity entity);

        SelectList SelectList_Engine(IQueryable<TEntity> data);
        void FixChildEntityForCreate(TEntity entity);

    }
}
