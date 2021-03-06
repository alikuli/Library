﻿using AliKuli.Extentions;
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
                    list = list.Where(x => x.MetaData.Created.Date_NotNull_Min.Year == dateOfTrx.Year)
                                            .ToList();
                    break;


                case GroupByConstants.YEAR_MONTH_DAY:
                    list = list.Where(x => x.MetaData.Created.Date_NotNull_Min.Year == dateOfTrx.Year &&
                                            x.MetaData.Created.Date_NotNull_Min.Month == dateOfTrx.Month)
                                            .ToList();
                    break;


                case GroupByConstants.YEAR_MONTH_DAY_HOUR:
                    list = list.Where(x => x.MetaData.Created.Date_NotNull_Min.Year == dateOfTrx.Year &&
                                            x.MetaData.Created.Date_NotNull_Min.Month == dateOfTrx.Month &&
                                            x.MetaData.Created.Date_NotNull_Min.Day == dateOfTrx.Day )
                                            .ToList();
                    break;


                case GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE:
                    list = list.Where(x => x.MetaData.Created.Date_NotNull_Min.Year == dateOfTrx.Year &&
                                            x.MetaData.Created.Date_NotNull_Min.Month == dateOfTrx.Month &&
                                            x.MetaData.Created.Date_NotNull_Min.Day == dateOfTrx.Day &&
                                            x.MetaData.Created.Date_NotNull_Min.Hour == dateOfTrx.Hour)
                                            .ToList();
                    break;


                case GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE_SECOND:
                    list = list.Where(x => x.MetaData.Created.Date_NotNull_Min.Year == dateOfTrx.Year &&
                                            x.MetaData.Created.Date_NotNull_Min.Month == dateOfTrx.Month &&
                                            x.MetaData.Created.Date_NotNull_Min.Day == dateOfTrx.Day &&
                                            x.MetaData.Created.Date_NotNull_Min.Hour == dateOfTrx.Hour &&
                                            x.MetaData.Created.Date_NotNull_Min.Minute == dateOfTrx.Minute)
                                            .ToList();
                    break;
                case GroupByConstants.DETAIL:
                    list = list.Where(x => x.MetaData.Created.Date_NotNull_Min.Year == dateOfTrx.Year &&
                                            x.MetaData.Created.Date_NotNull_Min.Month == dateOfTrx.Month &&
                                            x.MetaData.Created.Date_NotNull_Min.Day == dateOfTrx.Day &&
                                            x.MetaData.Created.Date_NotNull_Min.Hour == dateOfTrx.Hour &&
                                            x.MetaData.Created.Date_NotNull_Min.Minute == dateOfTrx.Minute &&
                                            x.MetaData.Created.Date_NotNull_Min.Second == dateOfTrx.Second)
                                            .ToList();
                    break;
                
                default:
                    break;

            }
            return list;
        }




    }
}
