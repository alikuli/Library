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

namespace MarketPlace.Web6.Controllers
{
    public class RightsController : EntityAbstractController<Right>
    {

        RightBiz _rightsBiz;

        public RightsController(RightBiz rightsBiz, IErrorSet errorSet, UserBiz userbiz, BreadCrumbManager breadCrumbManager)
            : base(rightsBiz, errorSet, userbiz, breadCrumbManager) 
        {
            _rightsBiz = rightsBiz;
        }


        public override System.Web.Mvc.ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            ViewBag.UserSelectList = _rightsBiz.UserSelectList();
            return base.Event_CreateViewAndSetupSelectList(parm);
        }


    }
}