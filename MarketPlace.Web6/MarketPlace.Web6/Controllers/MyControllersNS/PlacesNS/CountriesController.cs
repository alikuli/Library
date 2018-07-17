using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using UowLibrary;
namespace MarketPlace.Web6.Controllers
{
    public class CountriesController : EntityAbstractController<Country>
    {

        #region Constructo and initializers

        public CountriesController(CountryBiz countryBiz, IErrorSet errorSet, UserBiz userbiz, BreadCrumbManager breadCrumbManager)
            : base(countryBiz, errorSet, userbiz, breadCrumbManager) { }

        #endregion


    }
}