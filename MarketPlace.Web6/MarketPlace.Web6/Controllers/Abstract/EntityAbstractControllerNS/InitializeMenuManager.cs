using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using MarketPlace.Web4.Controllers;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary.MenuNS.MenuStateNS;
using UserModels;

namespace MarketPlace.Web6.Controllers.Abstract
{
    /// <summary>
    /// This needs to know which Uow to call. It has to be hard pr
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class EntityAbstractController<TEntity> : AbstractController where TEntity : class, ICommonWithId
    {
        protected void InitializeMenuManager(ControllerIndexParams parm)
        {
            

            TEntity entity = (TEntity)parm.Entity;
            if (entity.MenuManager.IsNull())
            {
                switch (parm.Menu.MenuEnum)
                {
                    case MenuENUM.IndexMenuPath1:
                    case MenuENUM.IndexMenuPath2:
                    case MenuENUM.IndexMenuPath3:
                        //Item is MenuPathMain
                        entity.MenuManager = new MenuManager(parm.Entity as MenuPathMain, null, null, parm.Menu.MenuEnum, BreadCrumbManager, parm.LikeUnlikeCounter, UserId);
                        break;

                    case MenuENUM.IndexMenuProduct:
                        //item is product
                        entity.MenuManager = new MenuManager(null, parm.Entity as Product, null, parm.Menu.MenuEnum, BreadCrumbManager, parm.LikeUnlikeCounter, UserId);
                        break;

                    case MenuENUM.IndexMenuProductChild:
                        //item is productChild
                        entity.MenuManager = new MenuManager(null, null, parm.Entity as ProductChild, parm.Menu.MenuEnum, BreadCrumbManager, parm.LikeUnlikeCounter, UserId);
                        break;

                    default:
                        entity.MenuManager = new MenuManager(null, null, null, parm.Menu.MenuEnum, BreadCrumbManager, parm.LikeUnlikeCounter, UserId);
                        break;
                }
            }
        }

        protected void InitializeMenuManager(ControllerCreateEditParameter parmIn)
        {
            ControllerIndexParams parm = parmIn.ConvertToControllerIndexParams();
            InitializeMenuManager(parm);
        }

    }
}