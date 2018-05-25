//using ApplicationDbContextNS;
//using ErrorHandlerLibrary.ExceptionsNS;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading.Tasks;
//using System.Web.Mvc;

//namespace DalLibrary.Interfaces
//{
//    public interface IRepositry<T>
//        where T : class
//    {
//        string GetClassName { get; }
//        T Factory();
//        ApplicationDbContext GetDb { get; }

//        #region Create

//        void Create(T entity);

//        #endregion

//        #region Find
//        T FindFor(Guid? id, bool deleted = false);
//        Task<T> FindForAsync(Guid? id, bool deleted = false);

//        //Task<T> FindForAsync(Guid? id);


//        T FindForLight(Guid? id, bool deleted = false);
//        Task<T> FindForLightAsync(Guid? id, bool deleted = false);


//        T FindFor(T entity, bool deleted = false);


//        IQueryable<T> FindAll(bool deleted = false);
//        IQueryable<T> FindAllLight(bool deleted = false);
//        Task<IList<T>> FindAllAsync(bool deleted = false);
//        //Task<IList<T>> FindAllLightAsync(bool deleted = false);

//        T FindForName(string name);
//        Task<T> FindForNameAsync(string name);
//        IEnumerable<T> FindForNameAll(string name);

//        #endregion


//        void Update(T entity);
//        //Task UpdateAsync(T entity);



//        bool NameExists(T entity);
//        Task<bool> NameExistsAsync(T entity);
//        #region Delete
//        void Delete(Guid? id);
//        //Task DeleteAsync(Guid? id);

//        void DeleteActually(T entity);

//        void Delete(T entity);
//        //Task DeleteAsync(T entity);

//        #endregion

//        #region SearchFOr
//        IList<T> SearchFor(Expression<Func<T, bool>> predicate);
//        Task<IList<T>> SearchForAsync(Expression<Func<T, bool>> predicate);

//        #endregion

//        void UnChangedState(T entity);
//        void AddedState(T entity);
//        SelectList SelectList();
//        //Task <SelectList> Selec

//        #region SaveChanges
//        int SaveChanges();
//        Task<int> SaveChangesAsync();

//        #endregion

//        //void Fix(T entity);
//        //IndexListVM GetIndexList(ControllerIndexParams parameters);
//        //Task<IndexListVM> GetIndexListAsync(ControllerIndexParams parameters);
//        ErrorSet ErrorsGlobal { get; }
//        //void EncryptDecryptAll();

//        //string Initialize();

//    }
//}
