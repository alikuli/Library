using DalLibrary.Interfaces;
using InterfacesLibrary.SharedNS;
using System;
using System.Data.Entity;


namespace DalLibrary.DalNS
{
    public partial class Repositry<TEntity> : IRepositry<TEntity> where TEntity : class, ICommonWithId
    {


        /// <summary>
        /// This adds to the Entity. Updates CreateDate to NowUTC, CreatedUser to current User,Deleted to false, Active to True. Then changes the EntityState to Added.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Create(TEntity entity)
        {
            //canCreate();

            //Initialize();
            IsCreating = true;

            Fix(entity);
            ErrorCheck(entity);
            _db.Set<TEntity>().Add(entity);
            _db.Entry(entity).State = EntityState.Added;

        }


    }
}