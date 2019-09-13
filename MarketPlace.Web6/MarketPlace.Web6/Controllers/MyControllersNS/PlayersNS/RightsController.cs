using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;

namespace MarketPlace.Web6.Controllers
{
    public class RightsController : EntityAbstractController<Right>
    {
        RightBiz _biz;
        public RightsController(RightBiz biz, UserBiz userBiz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _biz = biz;
            _userBiz = userBiz;
        }

        UserBiz _userBiz;
        UserBiz UserBiz
        {
            get
            {
                return _userBiz;
            }
        }
        RightBiz RightBiz
        {
            get { return _biz; }
        }
        public override System.Web.Mvc.ActionResult Event_Create_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            ViewBag.UserSelectList = UserBiz.SelectList();
            return base.Event_Create_ViewAndSetupSelectList_GET(parm);
        }
        public override ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            ViewBag.UserSelectList = UserBiz.SelectList();
            return base.Event_Edit_ViewAndSetupSelectList_GET(parm);
        }


    }
}