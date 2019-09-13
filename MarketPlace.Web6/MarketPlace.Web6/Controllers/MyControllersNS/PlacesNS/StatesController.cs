using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UowLibrary;
using UowLibrary.ParametersNS;
using UowLibrary.StateNS;

namespace MarketPlace.Web6.Controllers
{
    public class StatesController : EntityAbstractController<State>
    {


        StateBiz _stateBiz;
        //CountryBiz _countryBiz;
        public StatesController(StateBiz biz, AbstractControllerParameters param, CountryBiz countryBiz)
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

        //CountryBiz CountryBiz
        //{
        //    get
        //    {
        //        return _countryBiz;
        //    }
        //}

        //public override System.Web.Mvc.ActionResult Event_Create_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        //{
        //    ViewBag.CountrySelectList = CountryBiz.SelectList();
        //    return base.Event_Create_ViewAndSetupSelectList_GET(parm);
        //}

        //public override System.Web.Mvc.ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        //{
        //    ViewBag.CountrySelectList = CountryBiz.SelectList();
        //    return base.Event_Edit_ViewAndSetupSelectList_GET(parm);
        //}

    }
}