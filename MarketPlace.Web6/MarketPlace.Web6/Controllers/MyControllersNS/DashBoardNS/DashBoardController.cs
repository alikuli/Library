using MarketPlace.Web4.Controllers;
using ModelsClassLibrary.ModelsNS.DashBoardNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary.PageViewNS;
using UowLibrary.ParametersNS;
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
        public ActionResult GetGroupedCount(DateParameter dateParameter, string name)
        {
            //DateTime _minDate = DateTime.Parse(minDate);
            //DateTime _maxDate = DateTime.Parse(maxDate);
            try
            {
                string showDataBelongingTo = GroupByConstants.ALL;
                //dummy
                DateTime dateOfTrx_Dummy = dateParameter.BeginDate;

                var model = PageViewBiz.GetFinalData(
                                                        dateParameter,
                                                        dateOfTrx_Dummy,
                                                        showDataBelongingTo,
                                                        GroupByConstants.MAIN,
                                                        name);

                return View(model.DataGrouped);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong during DashBoardController.GetGroupedCount", MethodBase.GetCurrentMethod(), e);
                throw new Exception(ErrorsGlobal.ToString());
            }

        }

        public ActionResult GetAjax(string showDataBelongingTo, string groupBy, string name, DateTime beginDate, DateTime endDate, DateTime dateOfTrx)
        {
            try
            {
                var model = PageViewBiz.AjaxData(beginDate, endDate, dateOfTrx, showDataBelongingTo, groupBy, name);
                if (groupBy == GroupByConstants.DETAIL)
                    return PartialView("_dataDashboardSingle", model.DataGrouped.First().DataDetail);

                return PartialView("GetGroupedCount", model.DataGrouped);

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("Something went wrong during DashBoardController.GetAjax", MethodBase.GetCurrentMethod(), e);
                throw new Exception(ErrorsGlobal.ToString());
            }
        }
        public ActionResult GetGroupedCountView(string key, string dataType, DateTime beginDate, DateTime endDate, string dataOwner, string groupDataBy)
        {
            ////http://localhost:14038/DashBoard/GetGroupedCount#collapsemenus20182
            //var dateParameter = new DateParameter();
            //dateParameter.BeginDate = beginDate;
            //dateParameter.EndDate = endDate;

            //var dbm = PageViewBiz.GetAllData(dateParameter, dataType, key, dataOwner, groupDataBy);

            ////if(Request.IsAjaxRequest())
            ////{
            ////    return PartialView("_dataDashBoardGroupItem", dbm);

            ////}
            //return View("GetGroupedCount", dbm);
            throw new NotImplementedException();

        }



        public ActionResult GetAjaxData(string key, string dataType, DateTime beginDate, DateTime endDate, string dataOwner, string groupDataBy)
        {
            //var dateParameter = new DateParameter();
            //dateParameter.BeginDate = beginDate;
            //dateParameter.EndDate = endDate;
            ////how do we know if this is controller?
            ////we need to send key + name.
            ////all the data for eg controller can be found using key, but, after that it is
            ////the name
            //var pvd = PageViewBiz.GetAllData(dateParameter, dataType, key, dataOwner, groupDataBy);


            //return PartialView("_dataDashBoardGroupItem", pvd);

            throw new NotImplementedException();


        }

        #endregion


    }
}