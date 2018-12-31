using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UowLibrary.ParametersNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class MenuPathMainsController : EntityAbstractController<MenuPathMain>
    {

        MenuPathMainBiz _menupathmainBiz;


        public MenuPathMainsController(MenuPathMainBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _menupathmainBiz = biz;
        }

        public override System.Web.Mvc.ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            ViewBag.MenuPath1SelectList = _menupathmainBiz.MenuPath1_SelectList();
            ViewBag.MenuPath2SelectList = _menupathmainBiz.MenuPath2_SelectList();
            ViewBag.MenuPath3SelectList = _menupathmainBiz.MenuPath3_SelectList();

            return base.Event_CreateViewAndSetupSelectList(parm);
        }
    }
}
