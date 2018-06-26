using AliKuli.UtilitiesNS;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web4.Controllers;
using System;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary;

namespace MarketPlace.Web6.Controllers
{
    public class HomeController : AbstractController
    {
        UserBiz _userBiz;
        ConfigManagerHelper _configManager;
        public HomeController(IErrorSet errorSet, UserBiz userBiz)
            : base(errorSet, userBiz)
        {
            _userBiz = userBiz;
            _configManager = new ConfigManagerHelper();

        }

        public ActionResult Index()
        {
            try
            {
                _userBiz.InitializeSystem();
                return View();
            }
            catch (Exception e)
            {


                ErrorsGlobal.Add(string.Format("'{0}' Something went wrong while creating Index in Home.", "Home"), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                string responseCode = Response.StatusCode.ToString();
                string message = string.Format("Response Code: {0}. {1}", responseCode, ErrorsGlobal.ToString());
                return RedirectToAction("Index", "Errors", new { message = message });
            }
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {

            try
            {
                ViewBag.Message = "Your contact page.";
                return View();
            }
            catch (Exception e)
            {


                ErrorsGlobal.Add(string.Format("'{0}' Something went wrong in About.", "Home"), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Print()
        {
            return View();
        }
    }
}