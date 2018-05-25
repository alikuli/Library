using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UowLibrary.Abstract;
using UowLibrary.Interface;

namespace UowLibrary
{
    /// <summary>
    /// This is where all the Fix, bussiness Rules and ErrorChecks are implemented
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class BusinessLayer<TEntity> : AbstractBiz, IBusinessLayer<TEntity> where TEntity : class, ICommonWithId
    {


        /// <summary>
        /// This sets up the item to be entered. It enters the start date and time.
        /// This is added in the GET part of Create.
        /// </summary>
        /// <returns></returns>
        public virtual TEntity EntityFactoryForHttpGet()
        {
            TEntity entity = Dal.Factory();
            entity.MetaData.Created.SetToTodaysDateStart();
            Event_ApplyChangesAfterCreate(entity);
            return entity;
        }

        public TEntity Factory()
        {
            return Dal.Factory();
        }



    }
}
