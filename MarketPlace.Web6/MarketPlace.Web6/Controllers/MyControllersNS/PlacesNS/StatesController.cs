using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UowLibrary;
using UowLibrary.StateNS;

namespace MarketPlace.Web6.Controllers
{
    public class StatesController : EntityAbstractController<State>
    {

        #region Constructo and initializers

        StateBiz _stateBiz;
        public StatesController(StateBiz stateBiz, IErrorSet errorSet, UserBiz userbiz)
            : base(stateBiz, errorSet,  userbiz)
        {
            _stateBiz = stateBiz;
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