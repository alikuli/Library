using AliKuli.Extentions;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using UowLibrary.Abstract;
using UowLibrary.Interface;
using System.Linq;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace UowLibrary
{
    /// <summary>
    /// This is where all the Fix, bussiness Rules and ErrorChecks are implemented
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class BusinessLayer<TEntity> : AbstractBiz, IBusinessLayer<TEntity> where TEntity : class, ICommonWithId
    {



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

            TEntity entityFound = Dal.FindAll().FirstOrDefault(x =>
                x.Name.ToLower() == entity.Name.ToLower() &&
                x.Id != entity.Id);

            bool found = !entityFound.IsNull();

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



    }
}
