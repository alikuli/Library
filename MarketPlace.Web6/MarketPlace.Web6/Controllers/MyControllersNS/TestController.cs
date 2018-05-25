using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using System.Threading;
using System.Web.Mvc;
using UowLibrary.CountryNS;

namespace MarketPlace.Web6.Controllers
{
    public class TestController : Controller
    {


        [HttpPost]
        public ActionResult Index()
        {
            Thread.Sleep(15000);
            return View();
        }
    }
}