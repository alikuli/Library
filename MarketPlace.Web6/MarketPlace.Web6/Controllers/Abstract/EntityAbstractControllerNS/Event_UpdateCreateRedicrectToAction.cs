using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using MarketPlace.Web4.Controllers;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UserModels;

namespace MarketPlace.Web6.Controllers.Abstract
{
    /// <summary>
    /// This needs to know which Uow to call. It has to be hard pr
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class EntityAbstractController<TEntity> : AbstractController where TEntity : class, ICommonWithId
    {

        /// <summary>
        /// I needed to add this because I would like the return from FileDoc Create to be sorted by file number.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual RedirectToRouteResult Event_UpdateCreateRedicrectToAction(ControllerCreateEditParameter parm)
        {
            return RedirectToAction("Index", new { id = parm.Entity.Id, selectedId = parm.Entity.Id, returnUrl = parm.Menu.ReturnUrl, menuEnum = parm.Menu.MenuEnum });

        }


    }
}