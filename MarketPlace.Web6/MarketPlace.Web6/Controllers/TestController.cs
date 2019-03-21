using MarketPlace.Web6.Models;
using ModelsClassLibrary.DelegatesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UowLibrary.ProductNS;
namespace MarketPlace.Web6.Controllers
{
    public delegate void Print(int value);
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            return View();

        }


    }
}