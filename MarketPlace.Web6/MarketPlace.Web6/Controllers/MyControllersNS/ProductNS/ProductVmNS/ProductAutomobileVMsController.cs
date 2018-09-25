using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using BreadCrumbsLibraryNS.Programs;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using MarketPlace.Web4.Controllers;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ViewModels;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.MenuNS;
using UowLibrary.ParametersNS;
using UowLibrary.PageViewNS;
using UowLibrary.PlayersNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public partial class ProductAutomobileVMsController : AbstractController
    {

        ProductBiz _productBiz;
        MenuPath1Biz _menuPath1Biz;
        //UserBiz _userBiz;
        MenuPathMainBiz _menuPathMainBiz;
        public ProductAutomobileVMsController(ProductBiz productBiz, MenuPathMainBiz menuPathMainBiz, AbstractControllerParameters param)
            : base(param) 
        {
            _productBiz = productBiz;
            _menuPath1Biz = menuPathMainBiz.MenuPath1Biz;
            //_userBiz = rightBiz.UserBiz;
            _menuPathMainBiz = menuPathMainBiz;
        }

    }
}
