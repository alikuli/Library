using EnumLibrary.EnumNS;
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
        /// This stores the current menu level for the system. It is used todeterminethe list to be supplied in GetListForIndex in the Menu System
        /// </summary>
        protected MenuLevelENUM CurrentMenuLevel { get; set; }

    }
}
