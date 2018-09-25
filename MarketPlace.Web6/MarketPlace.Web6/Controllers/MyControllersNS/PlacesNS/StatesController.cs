using BreadCrumbsLibraryNS.Programs;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UowLibrary;
using UowLibrary.ParametersNS;
using UowLibrary.PageViewNS;
using UowLibrary.PlayersNS;
using UowLibrary.StateNS;

namespace MarketPlace.Web6.Controllers
{
    public class StatesController : EntityAbstractController<State>
    {

        #region Constructo and initializers

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

        #endregion


        public override System.Web.Mvc.ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            ViewBag.CountrySelectList = StateBiz.CountrySelectList;
            return base.Event_CreateViewAndSetupSelectList(parm);
        }

    }
}