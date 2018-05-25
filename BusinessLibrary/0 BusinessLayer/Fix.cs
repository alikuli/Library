using InterfacesLibrary.SharedNS;
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
        /// Fix any records that need fixing before applying business rules, saving and error checking here.
        /// Default. Fixes the Name To Title Case.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Fix(TEntity entity)
        {

        }


    }
}
