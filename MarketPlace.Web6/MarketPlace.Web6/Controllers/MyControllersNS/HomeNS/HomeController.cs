using AliKuli.UtilitiesNS;
using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web4.Controllers;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PageViewNS;
using UowLibrary.PlayersNS;

namespace MarketPlace.Web6.Controllers
{
    public class HomeController : AbstractController
    {
        public HomeController(BreadCrumbManager bcm, IErrorSet err, PageViewBiz pageViewBiz)
            : base(bcm, err, pageViewBiz) 
        {

        }


        public PartialViewResult AddUserPartialView()
        {
            return PartialView("AddUserPartialView", new AddUserViewModel());
        }

        [HttpPost]
        public JsonResult AddUserInfo(AddUserViewModel model)
        {
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                //isSuccess = Save data here return boolean
            }
            return Json(new { result = isSuccess, responseText = "Something wrong!" });
        }
        public ActionResult Index()
        {
            try
            {
                //UserBiz.InitializeSystem();
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