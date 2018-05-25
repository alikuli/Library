using DalLibrary.Interfaces;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;


namespace DalLibrary.DalNS
{
    /// <summary>
    /// Always Create using factory otherwise you will have problems due to IsEncrypt.
    /// Override entity.MakeName() to change the name of the entity
    /// To create the index data for a class you will need to override
    ///    IndexListVM GetIndexList(EnumLibrary.EnumNS.SortOrderENUM sortBy = SortOrderENUM.Item1_Asc) and
    ///    Task<IndexListVM> GetIndexListAsync(EnumLibrary.EnumNS.SortOrderENUM sortBy = SortOrderENUM.Item1_Asc)
    /// if you want to change the headings. If the heading is empty, it will not show in the index.
    /// you will only need to change the following fields as needed 
    ///    indexListVM.NameInput1
    ///    indexListVM.NameInput2
    ///    indexListVM.NameInput3
    /// Then it the View will work inshallah.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class Repositry<TEntity> : IRepositry<TEntity> where TEntity :class, ICommonWithId
    {


        #region EntityFramework -Detach UnChangedState AddedState


        public void Detach(TEntity entity)
        {
            //_db.Entry(entity).State = EntityState.Detached;
            //same as above...
            GetEntityEntry(entity).State = EntityState.Detached;

        }

        public void Attach(TEntity entity)
        {
            //https://stackoverflow.com/questions/30987806/dbset-attachentity-vs-dbcontext-entryentity-state-entitystate-modified
            //When you do context.Entry(entity).State = EntityState.Modified;, you are not only attaching the entity to the DbContext,
            //you are also marking the whole entity as dirty. This means that when you do context.SaveChanges(), EF will generate an update 
            //statement that will update all the fields of the entity.

            //This is not always desired.

            //On the other hand, DbSet.Attach(entity) attaches the entity to the context without marking it dirty. 
            //It is equivalent to doing context.Entry(entity).State = EntityState.Unchanged;
            DbSet.Attach(entity);

        }

        //https://stackoverflow.com/questions/15015975/entity-framework-objectstatemanager-not-defined
        public ObjectStateManager GetObjectStateManager()
        {
            return ((IObjectContextAdapter)_db).ObjectContext.ObjectStateManager;
        }
        public void UnChangedState(TEntity entity)
        {
            //_db.Entry(entity).State = EntityState.Unchanged;
            //same as above...
            GetEntityEntry(entity).State = EntityState.Unchanged;

        }

        public void AddedState(TEntity entity)
        {
            //_db.Entry(entity).State = EntityState.Added;
            //same as above...
            GetEntityEntry(entity).State = EntityState.Added;

        }

        /// <summary>
        /// DbContext. Page 112. Use DbEntityEntry to  get access to the change tracking information for an
        /// entity using Entry.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DbEntityEntry<TEntity> GetEntityEntry(TEntity entity)
        {
            DbEntityEntry<TEntity> dbEntityEntry = _db.Entry(entity);

            return dbEntityEntry;
        }

        public MetadataWorkspace GetMetadataWorkspace()
        {
            return GetObjectStateManager().MetadataWorkspace;
        }

        /// <summary>
        /// All tracked entities will show here. If you need just one type, you can filter.
        /// P 601 EntityFramework
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ObjectStateEntry> GetObjectStateEntries()
        {
            return GetObjectStateManager().GetObjectStateEntries(
                EntityState.Added |
                EntityState.Deleted |
                EntityState.Modified |
                EntityState.Unchanged);
        }



        //public List<MetadataProperty> GetDataRecordInfo()
        //{
        //    List<MetadataProperty> lst = new List<MetadataProperty>();
        //    ObjectSet<TEntity> os = new ObjectSet<TEntity>();
        //    var objectContext = ((IObjectContextAdapter)_db).ObjectContext;
        //    var container = objectContext.MetadataWorkspace.GetEntityContainer(objectContext.DefaultContainerName, DataSpace.CSpace);
        //    foreach (var set in container.BaseEntitySets)
        //    {
        //        // set.ElementType.
        //        foreach (var metaproperty in set.MetadataProperties)
        //        {
        //            // metaproperty.
        //            lst.Add(metaproperty);
        //        }
        //    }

        //    return lst;
        //}

        public IEnumerable<string> GetCurrentEntries(TEntity entity)
        {
            var entry = GetEntityEntry(entity);
            var namesOfCurrentProp = entry.CurrentValues.PropertyNames;
            return namesOfCurrentProp;
        }
        #endregion

    }
}

