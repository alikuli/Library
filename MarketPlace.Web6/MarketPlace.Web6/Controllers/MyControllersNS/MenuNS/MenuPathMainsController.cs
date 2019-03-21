using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;
using UowLibrary.ParametersNS;
using UowLibrary.ProductChildNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class MenuPathMainsController : EntityAbstractController<MenuPathMain>
    {

        MenuPathMainBiz _menupathmainBiz;
        ProductChildBiz _productChildBiz;

        public MenuPathMainsController(MenuPathMainBiz biz, AbstractControllerParameters param, ProductChildBiz productChildBiz)
            : base(biz, param)
        {
            _menupathmainBiz = biz;
            _productChildBiz = productChildBiz;
        }

        ProductChildBiz ProductChildBiz
        {
            get
            {
                _productChildBiz.UserId = UserId;
                _productChildBiz.UserName = UserName;
                return _productChildBiz;
            }
        }
        MenuPathMainBiz MenuPathMainBiz
        {
            get
            {
                return _menupathmainBiz;
            }
        }

        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            ViewBag.MenuPath1SelectList = _menupathmainBiz.MenuPath1_SelectList();
            ViewBag.MenuPath2SelectList = _menupathmainBiz.MenuPath2_SelectList();
            ViewBag.MenuPath3SelectList = _menupathmainBiz.MenuPath3_SelectList();

            return base.Event_CreateViewAndSetupSelectList(parm);
        }




    }
}
