using AliKuli.Extentions;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace DalLibrary.DalNS
{
    public partial class Repositry<TEntity> : IRepositry<TEntity> where TEntity : class, ICommonWithId
    {

        #region Find




        #region SearchForAsync and SearchFor
        public virtual async Task<IList<TEntity>> SearchForAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            //return await (FindAll().Where(predicate).Where(x => x.MetaData.IsDeleted == false)).ToListAsync();
            return await SearchForIQueriable(predicate).ToListAsync();
        }


        private IQueryable<TEntity> SearchForIQueriable(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            var zAll = FindAll();
            var zList = zAll.Where(predicate).Where(x => x.MetaData.IsDeleted == false);
            return zList.AsQueryable();
        }


        public virtual IList<TEntity> SearchFor(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            //var zAll = FindAll();
            //var zList = zAll.Where(predicate).Where(x => x.MetaData.IsDeleted == false).ToList();
            //return zList as IList<TEntity>;
            return SearchForIQueriable(predicate).ToList();
        }


        #endregion

        #region FindForName and FindForNameAsync

        public virtual TEntity FindForName(string name)
        {

            if (name.IsNullOrWhiteSpace())
            {
                name = "";
            }

            var foundall = FindForNameAll(name);

            if (foundall.IsNull())
                return null;


            TEntity foundIt = foundall
                .FirstOrDefault(x => x.Name.ToLower() == name.ToLower());


            return foundIt;

        }


        public virtual async Task<TEntity> FindForNameAsync(string name)
        {

            if (name.IsNullOrWhiteSpace())
                return null;

            var foundall = await FindForNameAllAsync(name);

            if (foundall.IsNull())
                return null;

            TEntity foundIt = foundall.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
            return foundIt;

        }

        #endregion

        #region FindForLight and FindForLightAsync

        public virtual TEntity FindForLight(string id, bool deleted = false)
        {
            if (id.IsNullOrEmpty())
                throw new ErrorHandlerLibrary.ExceptionsNS.NoDataException("Missing parameter: id. FindFor.Repository");

            var allItems = FindAll().ToList();
            
            if(allItems.IsNull())
                return null;

            var item = allItems.FirstOrDefault(x => x.Id == id);

            return item ;

            //if (itemList.IsNullOrEmpty())
            //    return default(TEntity);

            //return itemList.FirstOrDefault(x => x.Id == id) as TEntity;

        }

        public virtual async Task<TEntity> FindForLightAsync(string id, bool deleted = false)
        {
            if (id.IsNullOrEmpty())
                throw new ErrorHandlerLibrary.ExceptionsNS.NoDataException("Missing parameter: id. FindFor.Repository");

            var itemList = await FindAllAsync();
            if (itemList.IsNullOrEmpty())
                return default(TEntity);

            return itemList.FirstOrDefault(x => x.Id == id) as TEntity;

        }



        #endregion

        #region FindFor and FindForAsync


        /// <summary>
        /// This finds a record for the Entity. Checks for a zero value being passed. Then finds the record. 
        /// It defaults to non deleted records, however if you pass a true value in the 2nd parameter you can find deleted records as well.
        /// Exceptions
        ///     AliKuli.Exceptions.MiscNS.NoDataException -Missing parameter: id
        /// </summary>
        /// <param name="id">id, deleted=false</param>
        /// <returns>T</returns>
        public virtual TEntity FindFor(string id, bool deleted = false)
        {
            var item = FindForLight(id, deleted);

            if (item != null)
            {
                Fix(item);
            }

            return item;
        }

        public virtual async Task<TEntity> FindForAsync(string id, bool deleted = false)
        {
            var item = await FindForLightAsync(id, deleted);

            if (item != null)
            {
                Fix(item);
            }

            return item;
        }


        //--------------------------------------------------------------------------------------------
        /// <summary>
        /// This finds a record for the Entity. Checks for a zero value being passed. Then finds the record. 
        /// It defaults to non deleted records, however if you pass a true value in the 2nd parameter you can find deleted records as well.
        /// </summary>
        /// <param name="id">id, deleted=false</param>
        /// <returns>T</returns>

        public virtual TEntity FindFor(TEntity entity, bool deleted = false)
        {
            return this.FindFor(entity.Id, deleted);
        }


        #endregion

        #region FindForNameAll and FindForNameAllAsync

        public virtual IEnumerable<TEntity> FindForNameAll(string name)
        {
            if (name.IsNullOrEmpty())
                return null;

            var allT = FindAll().Where(x => x.Name.ToLower() == name.ToLower()).ToList();

            if (allT.IsNullOrEmpty())
                return null;

            //var foundIt = allT.Where(x => x.Name.ToLower() == name.ToLower()).ToList();

            return allT.AsEnumerable();

        }

        public virtual async Task<IEnumerable<TEntity>> FindForNameAllAsync(string name)
        {
            if (name.IsNullOrEmpty())
                return null;

            var allT = await FindAllAsync();

            if (allT.IsNullOrEmpty())
                return null;

            var foundIt = allT.Where(x => x.Name.ToLower() == name.ToLower());

            return (IEnumerable<TEntity>)foundIt;

        }


        #endregion

        #region NameExistsAsync and NameExists

        /// <summary>
        /// This checks to see if the name exists. If it exists, then it deattaches the entity
        /// so that it does not mess with the saving of the one for which a test is being carried
        /// out.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>

        public virtual bool NameExists(TEntity entity)
        {
            var entity2 = FindForName(entity.Name);
            return nameExists(entity, entity2);
        }


        public async virtual Task<bool> NameExistsAsync(TEntity entity)
        {
            var entity2 = await FindForNameAsync(entity.Name);
            return nameExists(entity, entity2);
        }


        private bool nameExists(TEntity entity, TEntity entity2)
        {
            if (entity2.IsNull())
                return false;

            // we dont want to track this object
            _db.Entry(entity2).State = EntityState.Detached;

            return !(entity.Id.Equals(entity2.Id));
        }




        #endregion

        #region Find No Tracking

        public virtual IQueryable<TEntity> FindAllLightNoTracking(bool deleted = false)
        {
            //var queryListOfItems = SearchFor(x => x.MetaData.IsDeleted == deleted);
            //var queryListOfItems = _db.Set<TEntity>().Where(x => x.MetaData.IsDeleted == deleted);
            var query = from b in _db.Set<TEntity>().AsNoTracking()
                        where b.MetaData.IsDeleted == deleted
                        orderby b.Name
                        select b;

            return query.AsQueryable();
        }

        public virtual IQueryable<TEntity> FindAllNoTracking(bool deleted = false)
        {
            var listOfItems = FindAllLightNoTracking(deleted);
            return listOfItems;
        }


        public virtual TEntity FindForLightNoTracking(string id, bool deleted = false)
        {
            if (id.IsNullOrEmpty())
                throw new ErrorHandlerLibrary.ExceptionsNS.NoDataException("Missing parameter: id. FindFor.Repository");

            return FindAllNoTracking().FirstOrDefault(x => x.Id == id);

            //if (itemList.IsNullOrEmpty())
            //    return default(TEntity);

            //return itemList.FirstOrDefault(x => x.Id == id) as TEntity;

        }

        public virtual async Task<TEntity> FindForLightNoTrackingAsync(string id, bool deleted = false)
        {
            if (id.IsNullOrEmpty())
                throw new ErrorHandlerLibrary.ExceptionsNS.NoDataException("Missing parameter: id. FindFor.Repository");

            var itemList = await FindAllNoTrackingAsync();
            if (itemList.IsNullOrEmpty())
                return default(TEntity);

            return itemList.FirstOrDefault(x => x.Id == id) as TEntity;

        }

        public virtual async Task<IList<TEntity>> FindAllNoTrackingAsync(bool deleted = false)
        {
            return await FindAllLightNoTracking(deleted).ToListAsync();

        }


        #endregion

        #region FindAll


        /// <summary>
        /// This retrieves all the values where deleted is false.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> FindAll(bool deleted = false)
        {
            var listOfItems = FindAllLight(deleted);
            return listOfItems;
        }

        public virtual async Task<IList<TEntity>> FindAllAsync(bool deleted = false)
        {


            return await FindAllLight(deleted).ToListAsync();


        }



        /// <summary>
        /// This does not fix the records, it only finds them
        /// </summary>
        /// <param name="deleted"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> FindAllLight(bool deleted = false)
        {

            if (canRetrieve())
            {
                var query = from b in _db.Set<TEntity>()
                            where b.MetaData.IsDeleted == deleted
                            orderby b.Name
                            select b;

                return query.AsQueryable();
            }
            else
            {
                //the code will never get here because error is thrown in isUserAuthourizedToRetrieve()
                ErrorsGlobal.Add("User not autourized to receive.", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }
        }





        #endregion

        #endregion

    }
}