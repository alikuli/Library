using AliKuli.Extentions;
using BreadCrumbsLibraryNS.Programs;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.MenuNS;
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PageViewNS;
using UowLibrary.PlayersNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public partial class MenusController : EntityAbstractController<MenuPathMain>
    {

        MenuBiz _menuBiz;
        MenuPath1Biz _menuPath1Biz;
        MenuPath2Biz _menuPath2Biz;

        public MenusController(MenuBiz biz, BreadCrumbManager bcm, IErrorSet err, PageViewBiz pageViewBiz)
            : base(biz, bcm, err, pageViewBiz) 
        {
            _menuBiz = biz;
            _menuPath1Biz = biz.MenuPathMainBiz.MenuPath1Biz;
            _menuPath2Biz = biz.MenuPathMainBiz.MenuPath2Biz;
        }



    }





}