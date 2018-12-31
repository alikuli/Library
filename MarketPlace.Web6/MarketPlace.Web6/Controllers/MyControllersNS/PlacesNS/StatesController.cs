using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UowLibrary.ParametersNS;
using UowLibrary.StateNS;

namespace MarketPlace.Web6.Controllers
{
    public class StatesController : EntityAbstractController<State>
    {


        StateBiz _stateBiz;
        public StatesController(StateBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _stateBiz = biz;
        }

        StateBiz StateBiz
        {
            get
            {
                return _stateBiz;
            }
        }



        public override System.Web.Mvc.ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            ViewBag.CountrySelectList = StateBiz.CountrySelectList;
            return base.Event_CreateViewAndSetupSelectList(parm);
        }

    }
}