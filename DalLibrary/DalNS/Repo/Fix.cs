using AliKuli.Extentions;
using DalLibrary.Interfaces;
using InterfacesLibrary.SharedNS;


namespace DalLibrary.DalNS
{
    public partial class Repositry<TEntity> : IRepositry<TEntity> where TEntity : class, ICommonWithId
    {

        #region Fix...

        /// <summary>
        /// This method will be used by each and every entity to fix the record so that it can pass
        /// the error check. I will used this to load all the virtual records that have NOT been
        /// loaded by the system. I dont trust EF that much.
        /// Note. This is called in Create and other areas that it is required. We do not
        /// need to explicitly call it.
        ///         
        /// Note. Whenever you call Fix specially, you have to consider which mode its in
        /// i.e. Create/Update/Delete
        /// </summary>
        /// <param name="entity"></param>
        private void Fix(TEntity entity)
        {
            Fix_Name(entity);
            entity = Fix_Dates(entity);
        }

        /// <summary>
        /// The entity date is changing... however the referenced entity date is NOT changing.
        /// </summary>
        /// <param name="entity"></param>
        private TEntity Fix_Dates(TEntity entity)
        {
            if (IsCreating)
                entity.MetaData.Created.SetToTodaysDate("");

            if (IsUpdating)
                entity.MetaData.Modified.SetToTodaysDate("");


            if (IsDeleting)
                entity.MetaData.Deleted.SetToTodaysDate("");

            return entity;
        }

        /// <summary>
        /// Use this when user is typing in the name. Otherwise, override
        /// </summary>
        /// <param name="entity"></param>
        protected void Fix_Name(TEntity entity)
        {
            if (entity.Name.IsNullOrWhiteSpace())
                return;
            
            if(entity.IsAllowNameToBeSentanceCased)
                entity.Name = entity.Name.ToTitleCase();
        }




        #endregion



    }
}