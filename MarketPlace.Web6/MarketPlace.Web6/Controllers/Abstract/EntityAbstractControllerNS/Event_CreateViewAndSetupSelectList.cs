using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using MarketPlace.Web4.Controllers;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;

namespace MarketPlace.Web6.Controllers.Abstract
{
    /// <summary>
    /// This needs to know which Uow to call. It has to be hard pr
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class EntityAbstractController<TEntity> : AbstractController where TEntity : class, ICommonWithId
    {





        /// <summary>
        /// This method makes sure that both SetupSelectList and CreateViewEditAndCreateActions happen together and in the correct
        /// order
        /// </summary>
        /// <param name="iCommonWithId"></param>
        /// <returns></returns>
        public virtual ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            if (parm.Entity.IsNull())
                return View(Biz.EntityFactoryForHttpGet());

            TEntity entity = (TEntity)parm.Entity;
            return View(entity);
        }



    }
}