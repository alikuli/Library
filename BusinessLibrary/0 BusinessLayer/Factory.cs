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
        public ICommonWithId EntityFactoryForHttpGet()
        {
            ICommonWithId entity = Factory() as ICommonWithId;
            entity.MetaData.Created.SetToTodaysDateStart();
            return entity;
        }

        //public virtual TEntity EntityFactoryForHttpGet(FactoryParameters fp)
        //{
        //    return EntityFactoryForHttpGet();
        //}

        public virtual ICommonWithId Factory()
        {
            return Dal.Factory();
        }



    }
}
