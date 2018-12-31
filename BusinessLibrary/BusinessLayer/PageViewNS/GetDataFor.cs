using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DashBoardNS;
using ModelsClassLibrary.ModelsNS.PageViewNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using UowLibrary.PageViewNS.PageViewDataNS;

namespace UowLibrary.PageViewNS
{
    public partial class PageViewBiz
    {



        /// <summary>
        /// This will take the dateParameters and return a list of all the GroupDataTypes with the year data
        /// </summary>
        /// <param name="list"></param>
        /// <param name="minDate"></param>
        /// <param name="maxDate"></param>
        /// <returns></returns>
        public DashBoardSingle GetFinalData(DateParameter dateParam,
                                            DateTime dateOfTrx,
                                            string showDataBelongingTo,
                                            string groupBy,
                                            string name)
        {
            //DashBoardSingle dbs = new DashBoardSingle();
            PageViewData pvd = new PageViewData();
            pvd.Heading = dateParam.Heading;

            //this gets the data filtered by the date
            List<PageView> lstWithinDate = filterForBeginEndDate(dateParam);

            lstWithinDate = fix_RawData(dateOfTrx, groupBy, name, lstWithinDate);

            //string dataType = GroupByConstants.MAIN;
            //now insert all the data in pvd.AllData
            var dbs = pvd.InitializePageViews(lstWithinDate, dateParam, groupBy, showDataBelongingTo);

            long noOfPageViews = lstWithinDate.Count();

            //if name has been passed... trim the overall data
            noOfPageViews = trimDataForNameAndBelongingTo(showDataBelongingTo, name, dbs, noOfPageViews);


            var dbsFixed = pvd.Controller_FixKey(dbs, groupBy);
            var dbsGrouped = pvd.Controller_SQL(dbsFixed, groupBy);
            var dbsGroupedFixed = pvd.FixDateInGroupAndChild(dbsGrouped, groupBy);
            
            dbs.Amount = noOfPageViews;
            dbs.TotalAmount = noOfPageViews;

            return dbsGroupedFixed;
        }






        private static long trimDataForNameAndBelongingTo(string showDataBelongingTo, string name, DashBoardSingle dbs, long noOfPageViews)
        {
            var iquiriable = dbs.DataDetail.AsQueryable();
            if(showDataBelongingTo != GroupByConstants.ALL)
                iquiriable = iquiriable.Where(x => x.BelongsToGroup == showDataBelongingTo);

            if (!name.IsNullOrWhiteSpace())
                iquiriable = iquiriable.Where(x => x.Name == name);

            dbs.DataDetail = iquiriable.ToList();
            noOfPageViews = dbs.DataDetail.Count();

            return noOfPageViews;
        }

        private List<PageView> fix_RawData(DateTime dateOfTrx, string groupBy, string name, List<PageView> lstWithinDate)
        {

            if (!name.IsNullOrWhiteSpace())
            {
                //filter data for the time period as well, as we drill down, the time period narrows.
                //Now we have less data to work with!
                lstWithinDate = filterDataForTrxDates(lstWithinDate, groupBy, dateOfTrx);
            }
            return lstWithinDate;
        }




        public DashBoardSingle AjaxData(DateTime beginDate, DateTime endDate, DateTime dateOfTrx, string showDataBelongingTo, string groupBy, string name)
        {

            DateParameter dateParam = new DateParameter();
            dateParam.BeginDate = beginDate;
            dateParam.EndDate = endDate;

            DashBoardSingle dbs = GetFinalData(dateParam, dateOfTrx, showDataBelongingTo, groupBy, name);
            return dbs;
        }




    }
}
