using AliKuli.UtilitiesNS;
using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary;
using MarketPlace.Web4.Controllers;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Linq;
using System.Web.Mvc;
using UowLibrary.ParametersNS;
using UowLibrary.PageViewNS;
namespace MarketPlace.Web6.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class DashBoardController : AbstractController
    {

        #region Constructo and initializers

        public DashBoardController(AbstractControllerParameters param)
            : base(param) 
        {
        }

        public ActionResult GetCount()
        {
            CountModel cm = new CountModel();
            cm.Count = PageViewBiz.FindAll().Count();
            return View("_numberOfCounts", cm);
        }

        public ActionResult GetGroupedCount()
        {
            DateParameter model = new DateParameter();
            return View("GetBeginEndDates", model);
        }

        [HttpPost]
        public ActionResult GetGroupedCount(DateParameter dateParameter)
        {
            //DateTime _minDate = DateTime.Parse(minDate);
            //DateTime _maxDate = DateTime.Parse(maxDate);

            string dataType = "";
            var model = PageViewBiz.GetCount(dateParameter, dataType);

            return View(model);

        }
        public ActionResult GetGroupedCountView(string key, string dataType, DateTime beginDate, DateTime endDate)
        {
        //http://localhost:14038/DashBoard/GetGroupedCount#collapsemenus20182
            var dateParameter = new DateParameter();
            dateParameter.BeginDate = beginDate;
            dateParameter.EndDate = endDate;

            var dbm = PageViewBiz.GetCount(dateParameter, dataType);

            //if(Request.IsAjaxRequest())
            //{
            //    return PartialView("_dataDashBoardGroupItem", dbm);

            //}
            return View("GetGroupedCount", dbm);

        }



        public ActionResult GetAjaxData(string key, string dataType, DateTime beginDate, DateTime endDate)
        {
            var dateParameter = new DateParameter();
            dateParameter.BeginDate = beginDate;
            dateParameter.EndDate = endDate;

            var dbm = PageViewBiz.GetCount(dateParameter, dataType);

            //if (Request.IsAjaxRequest())
            //{

            //}
            return PartialView("_dataDashBoardGroupItem", dbm);

        }

        #endregion


    }
}