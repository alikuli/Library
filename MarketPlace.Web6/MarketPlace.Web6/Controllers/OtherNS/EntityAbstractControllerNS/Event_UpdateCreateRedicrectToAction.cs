using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Web.Mvc;

namespace MarketPlace.Web6.Controllers.Abstract
{
    /// <summary>
    /// This needs to know which Uow to call. It has to be hard pr
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class EntityAbstractController<TEntity>
    {

        /// <summary>
        /// I needed to add this because I would like the return from FileDoc Create to be sorted by file number.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual RedirectToRouteResult Event_UpdateCreateRedicrectToAction(ControllerCreateEditParameter parm)
        {
            //            return RedirectToAction("Index", new { id = parm.Entity.Id, selectedId = parm.Entity.Id,  menuEnum = parm.Menu.MenuEnum });
            throw new NotImplementedException();

        }


    }
}