using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlacesNS.PhoneNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UowLibrary;
using UowLibrary.ParametersNS;
using UowLibrary.PhoneNS;

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

        public override System.Web.Mvc.ActionResult Event_Create_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            Phone phone = parm.Entity as Phone;
            phone.IsNullThrowException("Unable to unbox phone");

            phone.SelectListCountry = CountryBiz.SelectList();
            //phone.Name = phone.PhoneNo;
            return base.Event_Create_ViewAndSetupSelectList_GET(parm);
        }

        public override System.Web.Mvc.ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            Phone phone = parm.Entity as Phone;
            phone.IsNullThrowException("Unable to unbox phone");

            phone.SelectListCountry = CountryBiz.SelectList();
            //phone.Name = phone.PhoneNo;
            return base.Event_Edit_ViewAndSetupSelectList_GET(parm);
        }


    }
}