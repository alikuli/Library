using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using MarketPlace.Web4.Controllers;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;
using UowLibrary.MenuNS.MenuStateNS;

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
            if (entity.MenuManager.IsNull())
            {
                switch (parm.Menu.MenuEnum)
                {
                    case MenuENUM.IndexMenuPath1:
                    case MenuENUM.IndexMenuPath2:
                    case MenuENUM.IndexMenuPath3:
                        //Item is MenuPathMain
                        entity.MenuManager = new MenuManager(parm.Entity as MenuPathMain, null, null,parm.Menu.MenuEnum, BreadCrumbManager, parm.LikeUnlikeCounter);
                        break;

                    case MenuENUM.IndexMenuProduct:
                        //item is product
                        entity.MenuManager = new MenuManager(null, parm.Entity as Product, null, parm.Menu.MenuEnum, BreadCrumbManager, parm.LikeUnlikeCounter);
                        break;

                    case MenuENUM.IndexMenuProductChild:
                        //item is productChild
                        entity.MenuManager = new MenuManager(null, null, parm.Entity as ProductChild, parm.Menu.MenuEnum, BreadCrumbManager, parm.LikeUnlikeCounter);
                        break;

                    default:
                        entity.MenuManager = new MenuManager(null, null, null, parm.Menu.MenuEnum, BreadCrumbManager, parm.LikeUnlikeCounter);
                        break;
                }
            }


            return View(entity);
        }



    }
}