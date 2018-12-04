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
using UowLibrary.PhoneNS;
using ModelsClassLibrary.ModelsNS.PlacesNS.PhoneNS;
using AliKuli.Extentions;

namespace MarketPlace.Web6.Controllers
{
    public class PhonesController : EntityAbstractController<Phone>
    {
        CountryBiz _countryBiz;
        public PhonesController(PhoneBiz biz, AbstractControllerParameters param, CountryBiz countryBiz)
            : base(biz, param) 
        {
            _countryBiz = countryBiz;
        }

        CountryBiz CountryBiz
        {
            get
            {
                _countryBiz.IsNullThrowException();
                _countryBiz.UserId = UserId;
                _countryBiz.UserName = UserName;

                return _countryBiz;
            }
        }

        public override System.Web.Mvc.ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            Phone phone = parm.Entity as Phone;
            phone.IsNullThrowException("Unable to unbox phone");

            phone.SelectListCountry = CountryBiz.SelectList();
            //phone.Name = phone.PhoneNo;
            return base.Event_CreateViewAndSetupSelectList(parm);
        }


    }
}