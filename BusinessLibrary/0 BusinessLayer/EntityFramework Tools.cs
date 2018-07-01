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



        public void Detach(TEntity entity)
        {
            Dal.Detach(entity);
        }

        public void Attach(TEntity entity)
        {
            Dal.Attach(entity);
        }

    }
}
