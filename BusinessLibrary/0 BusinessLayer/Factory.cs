using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;
using UowLibrary.Abstract;
using UowLibrary.Interface;
using UowLibrary.MenuNS.MenuStateNS;

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

            return Factory();
        }

        //public virtual TEntity EntityFactoryForHttpGet(FactoryParameters fp)
        //{
        //    return EntityFactoryForHttpGet();
        //}

        public virtual ICommonWithId Factory()
        {
            ICommonWithId entity = Dal.Factory();
            entity.MetaData.Created.SetToTodaysDateStart();

            Product p = entity as Product;
            MenuPathMain mpm = entity as MenuPathMain;

            entity.MenuManager = new MenuManager(mpm, p, null, MenuENUM.CreateDefault, BreadCrumbManager, new LikeUnlikeParameter(0,0,"Initialization in Factory"), UserId);

            return entity;
        }



    }
}
