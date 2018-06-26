using DalLibrary.Interfaces;
using InterfacesLibrary.SharedNS;
using System;


namespace DalLibrary.DalNS
{
    public partial class Repositry<TEntity> : IRepositry<TEntity> where TEntity : class, ICommonWithId
    {

        public virtual ICommonWithId Factory()
        {
            TEntity entity = Activator.CreateInstance<TEntity>();
            return entity as ICommonWithId;
        }



    }
}