using AliKuli.Extentions;
using System.Web.Mvc;

namespace MarketPlace.Web6.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors
        public ActionResult Index(string message)
        {
            if (message.IsNullOrWhiteSpace())
                message = "There was an error!";
            return View("Index", null, message);
        }

        public ActionResult FileTooBig()
        {
            string message = "Your file is too big";
            return View("Index", null, message);

        }
    }
}