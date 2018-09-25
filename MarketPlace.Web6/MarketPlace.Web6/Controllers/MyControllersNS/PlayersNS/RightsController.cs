using System.Threading.Tasks;
using System.Web.Mvc;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UowLibrary.PlayersNS;
using ModelsClassLibrary.RightsNS;
using EnumLibrary.EnumNS;
using UowLibrary;
using BreadCrumbsLibraryNS.Programs;
using UowLibrary.ParametersNS;
using ErrorHandlerLibrary;
using UowLibrary.PageViewNS;

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
        public override System.Web.Mvc.ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            ViewBag.UserSelectList = UserBiz.SelectList();
            return base.Event_CreateViewAndSetupSelectList(parm);
        }


    }
}