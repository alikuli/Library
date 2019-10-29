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
using UowLibrary.ParametersNS;
using UowLibrary.PageViewNS;
using UowLibrary.PlayersNS;
using UowLibrary.ProductNS;
using UowLibrary.AddressNS;

namespace MarketPlace.Web6.Controllers
{
    public partial class MenusController : EntityAbstractController<MenuPathMain>
    {

        MenuBiz _menuBiz;
        MenuPath1Biz _menuPath1Biz;
        MenuPath2Biz _menuPath2Biz;
        //AddressBiz _addressBiz;

        public MenusController(MenuBiz biz, AbstractControllerParameters param)
            : base(biz, param) 
        {
            _menuBiz = biz;
            _menuPath1Biz = biz.MenuPathMainBiz.MenuPath1Biz;
            _menuPath2Biz = biz.MenuPathMainBiz.MenuPath2Biz;
            //_addressBiz = addressBiz;
        }

        //AddressBiz AddressBiz
        //{
        //    get
        //    {
        //        _addressBiz.UserId = UserId;
        //        _addressBiz.UserName = UserName;
        //        return _addressBiz;
        //    }
        //}

    }





}