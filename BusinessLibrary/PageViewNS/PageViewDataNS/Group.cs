//using AliKuli.Extentions;
//using ModelsClassLibrary.ModelsNS.DashBoardNS;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//namespace UowLibrary.PageViewNS.PageViewDataNS
//{
//    /// <summary>
//    /// This prepares the data for the dash board.
//    /// First thing it is preparing is the PageViews.
//    /// </summary>
//    public partial class PageViewData
//    {





//        #region Old Code.


//        /// <summary>
//        /// This is the entery point to the reccurssion
//        /// </summary>
//        /// <param name="dashboradSingleList"></param>
//        /// <param name="forDataType"></param>
//        /// <returns></returns>
//        //public DashBoardSingle GetDataGroupedMain(DashBoardSingle dashboradSingleList, string forDataGroup)
//        //{

//        //    if (dashboradSingleList.IsNull())
//        //        return null;

//        //    return getDataGrouped(dashboradSingleList, forDataGroup);
//        //}


//        //private DashBoardSingle getDataGrouped(DashBoardSingle dbs, string forDataGroup)
//        //{
//        //    dbs = sql(dbs, DataTypeConstants.MAIN);
//        //    return dbs;
//        //}


//        //private DashBoardSingle groupData(DashBoardSingle dbs, string dataType)
//        //{
//        //    //all the data has been initialized once you are here.
//        //    if (!dbs.DataDetail.IsNullOrEmpty())
//        //    {
//        //        //this groups data by dataType. Eg. Controller, next is Year
//        //        //dbs.DataGrouped is grouped by DataType/MainList
//        //        groupDataByMainList(dbs);

//        //        //now we walk through all the groups
//        //        foreach (DashBoardSingle dataTypeItem in dbs.DataGrouped)
//        //        {
//        //            //a single DataType Data is passed through
//        //            groupDataByYear(dataTypeItem);
//        //            //now within the DataGroup/MainList the data is grouped by the year

//        //            //this will ensure that when we are doing the main, we only get detailed
//        //            //data up to the year.
//        //            if (dataType == DataTypeConstants.MAIN)
//        //                continue; //go to the next one

//        //            if (dataTypeItem.DataGrouped.IsNullOrEmpty())
//        //                continue;


//        //            foreach (DashBoardSingle yearData in dataTypeItem.DataGrouped)
//        //            {
//        //                //a single year data is sent
//        //                groupDataByMonth(yearData);
//        //                //Now within the year, the Data is grouped by the month

//        //                //this will ensure that when we are doing the main, we only get detailed
//        //                //data up to the month.
//        //                if (dataType == DataTypeConstants.YEAR)
//        //                    continue; //go to the next one

//        //                if (yearData.DataGrouped.IsNullOrEmpty())
//        //                    continue;

//        //                foreach (DashBoardSingle monthData in yearData.DataGrouped)
//        //                {
//        //                    //a single month data is sent
//        //                    groupDataByDay(monthData);
//        //                    //now with the month, the data is grouped by the day

//        //                    //this will ensure that when we are doing the main, we only get detailed
//        //                    //data up to the Day.
//        //                    if (dataType == DataTypeConstants.YEAR_MONTH)
//        //                        continue; //go to the next one

//        //                    if (monthData.DataGrouped.IsNullOrEmpty())
//        //                        continue;

//        //                    foreach (DashBoardSingle dayData in monthData.DataGrouped)
//        //                    {
//        //                        //a single day data is sent
//        //                        groupDataByHour(dayData);
//        //                        //now within the day, the data is grouped by the hour

//        //                        //this will ensure that when we are doing the main, we only get detailed
//        //                        //data up to the Hour.
//        //                        if (dataType == DataTypeConstants.YEAR_MONTH_DAY)
//        //                            continue; //go to the next one

//        //                        if (dayData.DataGrouped.IsNullOrEmpty())
//        //                            continue;

//        //                        foreach (DashBoardSingle hourData in dayData.DataGrouped)
//        //                        {
//        //                            //a single hours data is sent
//        //                            groupDataByMinute(hourData);
//        //                            //now within the Hour the data is grouped by the minute

//        //                            //this will ensure that when we are doing the main, we only get detailed
//        //                            //data up to the Minute.
//        //                            if (dataType == DataTypeConstants.YEAR_MONTH_DAY_HOUR)
//        //                                continue; //go to the next one

//        //                            if (hourData.DataGrouped.IsNullOrEmpty())
//        //                                continue;

//        //                            foreach (DashBoardSingle minuteData in hourData.DataGrouped)
//        //                            {
//        //                                //a single minutes data is sent
//        //                                groupDataBySecond(minuteData);
//        //                                //Now within the minute the data is grouped by the second

//        //                                //this will ensure that when we are doing the main, we only get detailed
//        //                                //data up to the Minute.
//        //                                if (dataType == DataTypeConstants.YEAR_MONTH_DAY_HOUR_MINUTE)
//        //                                    continue; //go to the next one


//        //                                if (hourData.DataGrouped.IsNullOrEmpty())
//        //                                    continue;

//        //                                foreach (DashBoardSingle secondData in minuteData.DataGrouped)
//        //                                {
//        //                                    //a single minutes data is sent
//        //                                    groupDataBySecond(secondData);
//        //                                    //Now within the minute the data is grouped by the second

//        //                                    //this will ensure that when we are doing the main, we only get detailed
//        //                                    //data up to the Minute.


//        //                                    if (hourData.DataGrouped.IsNullOrEmpty())
//        //                                        continue;
//        //                                }
//        //                            }
//        //                        }

//        //                    }
//        //                }

//        //            }
//        //        }
//        //    }
//        //    return dbs;
//        //}

//        private void groupDataBySecond(DashBoardSingle rawData)
//        {
//            rawData.DataGrouped = sql(
//                rawData,
//                DataTypeConstants.YEAR_MONTH_DAY_HOUR_MINUTE_SECOND,
//                DataTypeConstants.DETAIL);
//        }

//        private void groupDataByMinute(DashBoardSingle rawData)
//        {
//            rawData.DataGrouped = sql(
//                rawData,
//                DataTypeConstants.YEAR_MONTH_DAY_HOUR_MINUTE,
//                DataTypeConstants.YEAR_MONTH_DAY_HOUR_MINUTE_SECOND);
//        }

//        private void groupDataByHour(DashBoardSingle rawData)
//        {
//            rawData.DataGrouped = sql(
//                rawData,
//                DataTypeConstants.YEAR_MONTH_DAY_HOUR,
//                DataTypeConstants.YEAR_MONTH_DAY_HOUR_MINUTE);
//        }

//        private void groupDataByDay(DashBoardSingle rawData)
//        {
//            rawData.DataGrouped = sql(
//                rawData,
//                DataTypeConstants.YEAR_MONTH_DAY,
//                DataTypeConstants.YEAR_MONTH_DAY_HOUR);
//        }

//        private void groupDataByMonth(DashBoardSingle rawData)
//        {
//            rawData.DataGrouped = sql(
//                rawData,
//                DataTypeConstants.YEAR_MONTH,
//                DataTypeConstants.YEAR_MONTH_DAY);
//        }

//        private void groupDataByMainList(DashBoardSingle rawData)
//        {
//            rawData.DataGrouped = sql(rawData, " ", DataTypeConstants.YEAR);
//        }

//        private void groupDataByYear(DashBoardSingle rawData)
//        {
//            rawData.DataGrouped = sql(
//                rawData,
//                DataTypeConstants.YEAR,
//                DataTypeConstants.YEAR_MONTH);
//        }


//        /// <summary>
//        /// This gets data for the main screen. Eg Controller, browser etc
//        /// </summary>
//        private void getForDataType(DashBoardSingle rawData)
//        {
//            fixDataForYear(rawData);
//            rawData.DataGrouped = sql(
//                rawData,
//                DataTypeConstants.MAIN,
//                DataTypeConstants.YEAR);
//        }

//        #endregion

//        /// <summary>
//        /// Note. After this, the DataGroup will contain the groups of DataDetail
//        /// </summary>
//        /// <param name="fixedData"></param>
//        /// <param name="nextGrouping"></param>
//        /// <param name="noOfReccursions"></param>
//        /// <param name="thisGroupDataType">Eg. Year, Year-Month etc the constants listed to identify where you are coming from</param>
//        /// <returns></returns>
//        //private List<DashBoardSingle> sql(DashBoardSingle dbs, string currGrouping, string nextGrouping)
//        //{

//        //    long totalCount = dbs.DataDetail.Count();
//        //    var listOfDataGrouped = dbs
//        //        .DataDetail
//        //        .OrderBy(x => x.Key)
//        //        .ThenBy(x => x.DateOfTrx.Year)
//        //        .ThenBy(x => x.DateOfTrx.Month)
//        //        .ThenBy(x => x.DateOfTrx.Month)
//        //        .ThenBy(x => x.DateOfTrx.Day)
//        //        .ThenBy(x => x.DateOfTrx.Hour)
//        //        .ThenBy(x => x.DateOfTrx.Minute)
//        //        .ThenBy(x => x.DateOfTrx.Second)
//        //        .GroupBy(x => x.Key)
//        //        .Select(y =>
//        //        new DashBoardSingle
//        //        {
//        //            Amount = GetDataFor(
//        //                                y.First().Key,
//        //                                dbs.DataDetail)
//        //                                .Count(),

//        //            //we need to give a new key each time
//        //            Key = makeKey(
//        //                                y.First().DataType,
//        //                                y.First().DateOfTrx,
//        //                                nextGrouping),

//        //            Name = makeName(
//        //                                y.First().DateOfTrx,
//        //                                dbs.DataType,
//        //                                y.First().Name,
//        //                                y.Key),

//        //            Percent = calculatePct(
//        //                                y.Count(),
//        //                                totalCount),
//        //            //Percent = y.Count() == 0 ? 0 : (y.Count() / count) & 100,
//        //            DataDetail = detailForThis(
//        //                                y.Key,
//        //                                dbs.DataDetail,
//        //                                nextGrouping), //this is all the detail for this particular year.

//        //            BeginDate = dbs.BeginDate,
//        //            EndDate = dbs.EndDate,
//        //            DataType = currGrouping,
//        //        })
//        //        .OrderByDescending(x => x.Percent)
//        //        .ToList();

//        //    return listOfDataGrouped;

//        //}



//        //private List<DashBoardSingle> getDetailedData(List<DashBoardSingle> theAction, string name)
//        //{
//        //    return theAction.Where(x => x.Name == name)
//        //        .OrderByDescending(z => z.DateOfTrx)
//        //        .ToList();
//        //}

//        //private double getPercentage(double amount, double totalCount)
//        //{
//        //    double amt = amount;
//        //    double ttl = totalCount;

//        //    if (amt == 0)
//        //        return 0;
//        //    return amt / ttl * 100;
//        //}



//    }
//}
