using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DashBoardNS;
using ModelsClassLibrary.ModelsNS.PageViewNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UowLibrary.PageViewNS
{
    public partial class PageViewBiz
    {

        //const string MINDATE = "01-Jan-2010";
        //const string MAXDATE = "31-Dec-2050";

        List<PageView> _pageViewList;

        List<PageView> PageViewList { get { return _pageViewList ?? (_pageViewList = FindAll().ToList()); } }



        /// <summary>
        /// This groups a list by name and returns the amount for each in a List<DashBoardGroupResult> ()
        /// </summary>
        /// <param name="list"></param>
        /// <param name="minDate"></param>
        /// <param name="maxDate"></param>
        /// <returns></returns>
        public DashBoardMain GetCount(DateParameter dateParam, string dataType)
        {
            DashBoardMain dbm = new DashBoardMain();

            dbm.Heading = dateParam.Heading;
            var lstWithinDate = getPageViewWithinDate(dateParam);
            dbm.InitializePageViews(lstWithinDate, dateParam, dataType);

            return dbm;
        }
        /// <summary>
        /// This shapes the data. It basically adds all the dates that fall within begin date and end date
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        private List<PageView> getPageViewWithinDate(DateParameter dp)
        {

            dp.ErrorCheck();

            //var lstWithinDate = PageViewList
            //    .Where(x =>
            //        DateTime.Compare(dp.BeginDate, x.MetaData.Created.Date ?? DateTime.MinValue) <= 0
            //    && DateTime.Compare(x.MetaData.Created.Date ?? DateTime.MinValue, dp.EndDate) >= 0
            //        )
            //    .ToList();
            //return lstWithinDate;

            if (PageViewList.IsNull())
                return null;

            var lstWithinDate = new List<PageView>();

            foreach (PageView pageView in PageViewList)
            {
                DateTime date =
                    pageView.MetaData.Created.Date ??
                    DateTime.MinValue;

                if (dp.IsDateWithinBeginAndEndDatesInclusive(date))
                    lstWithinDate.Add(pageView);
            }

            return lstWithinDate;


        }




    }
}
