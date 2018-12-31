using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using UowLibrary;
using UowLibrary.ParametersNS;
namespace MarketPlace.Web6.Controllers
{
    public class CountriesController : EntityAbstractController<Country>
    {


        public CountriesController(CountryBiz biz, AbstractControllerParameters param)
            : base(biz, param) { }



    }
}