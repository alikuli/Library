using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PageViewNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Linq;
using System.Web.Mvc;
using UowLibrary.ParametersNS;
using UowLibrary.PageViewNS;
namespace MarketPlace.Web6.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PageViewsController : EntityAbstractController<PageView>
    {

        PageViewBiz _PageViewsBiz;
        #region Constructo and initializers

        public PageViewsController(AbstractControllerParameters param)
            : base(param.PageViewBiz, param) 
        {
            _PageViewsBiz = param.PageViewBiz;
        }

        public ActionResult GetCount()
        {
            CountModel cm = new CountModel();
            cm.Count = _PageViewsBiz.FindAll().Count();
            return View("_numberOfCounts", cm);
        }

        //public ActionResult GetGroupedCount()
        //{
        //    DateParameter model = new DateParameter();
        //    return View("GetBeginEndDates", model);
        //}
        //[HttpPost]
        //public ActionResult GetGroupedCount(DateParameter dateParameter)
        //{
        //    string dataType = "";
        //    var dbm = PageViewBiz.GetCount(dateParameter, dataType);

        //    return View("GetGroupedCount", dbm);

        //}

        //public ActionResult GetGroupedCountView(string key, string dataType, DateTime beginDate, DateTime endDate)
        //{
        //    var dateParameter = new DateParameter();
        //    dateParameter.BeginDate = beginDate;
        //    dateParameter.EndDate = endDate;

        //    var dbm = PageViewBiz.GetCount(dateParameter, dataType);
        //    return View("GetGroupedCount", dbm);

        //}

        #endregion


    }
}