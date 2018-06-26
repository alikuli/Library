using ApplicationDbContextNS;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DalLibrary.Interfaces
{
    public interface IRepositry<TEntity>
     where TEntity : class, ICommonWithId
    {

        void AddedState(TEntity entity);
        void Create(TEntity entity);
        void Delete(string id);
        void Delete(TEntity entity);
        void DeleteActually(TEntity entity);
        void DeleteActually(string id);
        Task DeleteAsync(string id);
        void Detach(TEntity entity);
        //void Dispose();
        void ErrorCheck(TEntity entity);

        void DetachAll();
        ErrorSet ErrorsGlobal { get; }
        ICommonWithId Factory();
        IQueryable<TEntity> FindAll(bool deleted = false);
        Task<List<TEntity>> FindAllAsync(bool deleted = false);
        IQueryable<TEntity> FindAllLight(bool deleted = false);
        IQueryable<TEntity> FindAllLightNoTracking(bool deleted = false);
        IQueryable<TEntity> FindAllNoTracking(bool deleted = false);
        Task<IList<TEntity>> FindAllNoTrackingAsync(bool deleted = false);
        TEntity FindFor(string id, bool deleted = false);
        TEntity FindFor(TEntity entity, bool deleted = false);
        Task<TEntity> FindForAsync(string id, bool deleted = false);
        TEntity FindForLight(string id, bool deleted = false);
        Task<TEntity> FindForLightAsync(string id, bool deleted = false);
        TEntity FindForLightNoTracking(string id, bool deleted = false);
        Task<TEntity> FindForLightNoTrackingAsync(string id, bool deleted = false);
        TEntity FindForName(string name);

        TEntity FindForNameNoTracking(string name);
        Task<TEntity> FindForNameNoTrackingAsync(string name);

        IEnumerable<TEntity> FindForNameAll(string name);
        Task<IEnumerable<TEntity>> FindForNameAllNoTrackingAsync(string name);
        IEnumerable<TEntity> FindForNameAllNoTracking(string name);

        Task<IEnumerable<TEntity>> FindForNameAllAsync(string name);
        Task<TEntity> FindForNameAsync(string name);
        string GetClassName { get; }
        ApplicationDbContext GetDb { get; }
        DbEntityEntry<TEntity> GetEntityEntry(TEntity entity);
        string GetSelfClassName();
        string GetSelfMethodName();
        bool NameExists(TEntity entity);
        Task<bool> NameExistsAsync(TEntity entity);
        int SaveChanges();
        Task<int> SaveChangesAsync();
        IList<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);
        Task<IList<TEntity>> SearchForAsync(Expression<Func<TEntity, bool>> predicate);
        global::System.Web.Mvc.SelectList SelectList();
        global::System.Web.Mvc.SelectList SelectList_Engine(IQueryable<TEntity> data);
        void UnChangedState(TEntity entity);
        string UserName { get; set; }
        string UserId { get; set; }
        void Update(TEntity entity);
    }
}
