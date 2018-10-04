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
        /// This filters the PageView data reduces the size
        /// </summary>
        /// <param name="list"></param>
        /// <param name="groupBy"></param>
        /// <param name="dateOfTrx"></param>
        /// <returns></returns>
        private List<PageView> filterDataForTrxDates(List<PageView> list, string groupBy, DateTime dateOfTrx)
        {
            if (dateOfTrx == DateTime.MinValue)
                return list;

            switch (groupBy)
            {
                case GroupByConstants.MAIN:

                case GroupByConstants.NAME:
                case GroupByConstants.YEAR:
                    break;


                case GroupByConstants.YEAR_MONTH:
                    list = list.Where(x => x.MetaData.Created.Date_NotNull.Year == dateOfTrx.Year)
                                            .ToList();
                    break;


                case GroupByConstants.YEAR_MONTH_DAY:
                    list = list.Where(x => x.MetaData.Created.Date_NotNull.Year == dateOfTrx.Year &&
                                            x.MetaData.Created.Date_NotNull.Month == dateOfTrx.Month)
                                            .ToList();
                    break;


                case GroupByConstants.YEAR_MONTH_DAY_HOUR:
                    list = list.Where(x => x.MetaData.Created.Date_NotNull.Year == dateOfTrx.Year &&
                                            x.MetaData.Created.Date_NotNull.Month == dateOfTrx.Month &&
                                            x.MetaData.Created.Date_NotNull.Day == dateOfTrx.Day )
                                            .ToList();
                    break;


                case GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE:
                    list = list.Where(x => x.MetaData.Created.Date_NotNull.Year == dateOfTrx.Year &&
                                            x.MetaData.Created.Date_NotNull.Month == dateOfTrx.Month &&
                                            x.MetaData.Created.Date_NotNull.Day == dateOfTrx.Day &&
                                            x.MetaData.Created.Date_NotNull.Hour == dateOfTrx.Hour)
                                            .ToList();
                    break;


                case GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE_SECOND:
                    list = list.Where(x => x.MetaData.Created.Date_NotNull.Year == dateOfTrx.Year &&
                                            x.MetaData.Created.Date_NotNull.Month == dateOfTrx.Month &&
                                            x.MetaData.Created.Date_NotNull.Day == dateOfTrx.Day &&
                                            x.MetaData.Created.Date_NotNull.Hour == dateOfTrx.Hour &&
                                            x.MetaData.Created.Date_NotNull.Minute == dateOfTrx.Minute)
                                            .ToList();
                    break;
                case GroupByConstants.DETAIL:
                    list = list.Where(x => x.MetaData.Created.Date_NotNull.Year == dateOfTrx.Year &&
                                            x.MetaData.Created.Date_NotNull.Month == dateOfTrx.Month &&
                                            x.MetaData.Created.Date_NotNull.Day == dateOfTrx.Day &&
                                            x.MetaData.Created.Date_NotNull.Hour == dateOfTrx.Hour &&
                                            x.MetaData.Created.Date_NotNull.Minute == dateOfTrx.Minute &&
                                            x.MetaData.Created.Date_NotNull.Second == dateOfTrx.Second)
                                            .ToList();
                    break;
                
                default:
                    break;

            }
            return list;
        }
        ////const string MINDATE = "01-Jan-2010";
        ////const string MAXDATE = "31-Dec-2050";

        //List<PageView> _pageViewList;

        //List<PageView> PageViewList
        //{
        //    get
        //    {
        //        return _pageViewList ?? (_pageViewList = FindAll().ToList());
        //    }
        //}



        ///// <summary>
        ///// This shapes the data. It basically adds all the dates that fall within begin date and end date
        ///// </summary>
        ///// <param name="dp"></param>
        ///// <returns></returns>
        //private List<PageView> filterForBeginEndDate(DateParameter dp)
        //{

        //    dp.ErrorCheck();

        //    List<PageView> lstWithinDate = new List<PageView>();

        //    if (lstWithinDate.IsNullOrEmpty())
        //    {
        //        lstWithinDate = new List<PageView>();

        //        foreach (PageView pageView in PageViewList)
        //        {
        //            DateTime date =
        //                pageView.MetaData.Created.Date ??
        //                DateTime.MinValue;

        //            if (dp.IsDateWithinBeginAndEndDatesInclusive(date))
        //                lstWithinDate.Add(pageView);
        //        }

        //      }
        //    return lstWithinDate;


        //}







    }
}
