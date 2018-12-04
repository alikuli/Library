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
        ProductBiz _productBiz;
        public TestController(ProductBiz productBiz)
        {
            _productBiz = productBiz;
        }
        public ActionResult Index()
        {
            FeaturesListModel model = new FeaturesListModel();
            loadDataIntoVm(model);
            model.SetupDirtyFlags();

            return View(model);

        }

        private static void loadDataIntoVm(FeaturesListModel model)
        {
            model.Features = new List<FeaturesModel>
            {
                new FeaturesModel{Name = "No of Seats", FeatureValue = "4"},
                new FeaturesModel{FeatureName = "Automatic", FeatureValue = "No"},
                new FeaturesModel{FeatureName = "Engine", FeatureValue = "2500CC"}
            };
        }


        public ActionResult Delete(int index, FeaturesListModel model)
        {
            deleteFromDb(model.Features[index].Id);
            model.Delete(index);
            return Json(model);
        }

        private void deleteFromDb(string p)
        {
            //throw new System.NotImplementedException();
        }

        public ActionResult Add(FeaturesListModel model)
        {
            model.Add();




            return Json(model);
        }
        public ActionResult Save(FeaturesListModel model)
        {

            model.Save();
            return Json(model);
        }

    }
}