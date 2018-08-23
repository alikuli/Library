using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UowLibrary;
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PlayersNS;
namespace MarketPlace.Web6.Controllers
{
    public class CountriesController : EntityAbstractController<Country>
    {

        #region Constructo and initializers

        public CountriesController(CountryBiz biz, BreadCrumbManager bcm, IErrorSet err)
            : base(biz, bcm, err) { }

        #endregion


    }
}