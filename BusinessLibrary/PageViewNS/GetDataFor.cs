using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DashBoardNS;
using ModelsClassLibrary.ModelsNS.PageViewNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using UowLibrary.PageViewNS.PageViewDataNS;
using System.Linq;

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
        public DashBoardSingle GetFinalData(
                                            DateParameter dateParam,  
                                            string showDataBelongingTo, 
                                            string groupBy, 
                                            string name)
        {
            //DashBoardSingle dbs = new DashBoardSingle();
            PageViewData pvd = new PageViewData();
            pvd.Heading = dateParam.Heading;

            //this gets the data filtered by the date
            List<PageView> lstWithinDate = filterDataForDates(dateParam);
            
            //string dataType = GroupByConstants.MAIN;
            //now insert all the data in pvd.AllData
            var dbs = pvd.InitializePageViews(lstWithinDate, dateParam, groupBy, showDataBelongingTo);

            //if name has been passed... trim the overall data
            if(!name.IsNullOrWhiteSpace())
            {
                dbs.DataDetail = dbs.DataDetail.Where(x => x.Name == name).ToList();
            }


            var dbsFixed = pvd.FixDataController(dbs, groupBy);
            var dbsGrouped = pvd.SqlController(dbsFixed, groupBy);
            return dbsGrouped;
        }

        public DashBoardSingle AjaxData(DateTime beginDate, DateTime endDate, string showDataBelongingTo, string groupBy, string name)
        {

            DateParameter dateParam = new DateParameter();
            dateParam.BeginDate = beginDate;
            dateParam.EndDate = endDate;

            DashBoardSingle dbs =  GetFinalData(dateParam, showDataBelongingTo,groupBy, name);
            return dbs;
        }




    }
}
