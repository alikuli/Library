using ApplicationDbContextNS;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace DalLibrary.Interfaces
{
    public interface IRepositry<TEntity>
     where TEntity : class, ICommonWithId
    {
        void Attach(TEntity entity);
        void AddedState(TEntity entity);
        void Create(TEntity entity);
        //void Delete(string id);
        void Delete(TEntity entity);
        void DeleteActually(TEntity entity);
        void Detach(TEntity entity);
        //void Dispose();
        void ErrorCheck(TEntity entity);

        void DetachAll();
        ErrorSet ErrorsGlobal { get; }
        ICommonWithId Factory();
        IQueryable<TEntity> FindAllFor(bool deleted = false, bool isInactive = false);
        string GetClassName { get; }
        ApplicationDbContext GetDb { get; }
        DbEntityEntry<TEntity> GetEntityEntry(TEntity entity);
        string GetSelfClassName();
        string GetSelfMethodName();
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void UnChangedState(TEntity entity);
        string UserName { get; set; }
        string UserId { get; set; }
        void Update(TEntity entity);
    }
}
