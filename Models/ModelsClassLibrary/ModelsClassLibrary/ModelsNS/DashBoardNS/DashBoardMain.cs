using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PageViewNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ModelsClassLibrary.ModelsNS.DashBoardNS
{
    /// <summary>
    /// This prepares the data for the dash board.
    /// First thing it is preparing is the PageViews.
    /// </summary>
    public class DashBoardMain
    {

        public DashBoardMain()
        {

        }




        /// <summary>
        /// This is to be used for getting data from the PageViews
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="browserType"></param>
        /// <param name="isCrawler"></param>
        /// <param name="isMobileDevice"></param>
        /// <param name="urlRefererHost"></param>
        /// <param name="userAgent"></param>
        /// <param name="userHostAddress"></param>
        /// <param name="userHostName"></param>
        /// <param name="userLanguages"></param>
        /// <param name="userName"></param>
        public void InitializePageViews(
            string controller,
            string action,
            string browserType,
            bool isCrawler,
            bool isMobileDevice,
            string urlRefererHost,
            string userAgent,
            string userHostAddress,
            string userHostName,
            string userLanguages,
            string userName,
            DateTime dateCreated,
            DateParameter dateParam,
            string dataType)
        {
            AddControllerActions(controller, action, dateCreated, isCrawler, dateParam, dataType);
            AddBrowserType(browserType, dateCreated, isCrawler, dateParam, dataType);
            AddIsCrawler(isCrawler, dateCreated, userAgent, dateParam, dataType);
            AddIsMobileDevice(isMobileDevice, dateCreated, userAgent, isCrawler, dateParam, dataType);
            AddUrlReferHost(urlRefererHost, dateCreated, isCrawler, dateParam, dataType);
            AddUserAgent(userAgent, dateCreated, isCrawler, dateParam, dataType);
            AddUserHostAddress(userHostAddress, dateCreated, isCrawler, dateParam, dataType);
            AddUserHostName(userHostName, dateCreated, isCrawler, dateParam, dataType);
            AddUserLanguages(userLanguages, dateCreated, isCrawler, dateParam, dataType);
            AddUserName(userName, dateCreated, isCrawler, dateParam, dataType);



        }


        public void InitializePageViews(List<PageView> pageViewList, DateParameter dateParam, string dataType)
        {
            if (pageViewList.IsNullOrEmpty())
                return;

            foreach (PageView pv in pageViewList)
            {
                InitializePageViews(
                    pv.ControllerName,
                    pv.ActionName,
                    pv.BrowserType,
                    pv.IsCrawler,
                    pv.IsMobileDevice,
                    pv.UrlRefererrerHost,
                    pv.UserAgent,
                    pv.UserHostAddress,
                    pv.UserHostName,
                    pv.UserLanguages,
                    pv.UserName,
                    pv.MetaData.Created.Date ?? DateTime.MinValue,
                    dateParam, dataType);
            }
        }


        public string Heading { get; set; }
        public string TotalHits
        {
            get
            {
                long noOfHits = 0;
                if (ControllerActions.IsNull())
                {

                }
                else
                {
                    noOfHits = ControllerActions.DataDetail.Count();

                }
                return string.Format("Total Hits: {0}", noOfHits);
            }
        }

        private void AddUserName(string userName, DateTime dateCreated, bool isCrawler, DateParameter dateParam, string dataType)
        {
            UserNames = addRecord(userName, dateCreated, isCrawler, dateParam, dataType, UserNames);

        }


        private void AddUserLanguages(string userLanguages, DateTime dateCreated, bool isCrawler, DateParameter dateParam, string dataType)
        {

            if (userLanguages.IsNullOrWhiteSpace())
                return;

            if (dataType.IsNullOrWhiteSpace())
                return;

            if (UserLanguages.IsNull())
            {
                UserLanguages = new DashBoardSingle("All Is UserLanguages  Actions", dateCreated, isCrawler, userLanguages, 0, dateParam.BeginDate, dateParam.EndDate, dataType);
            }

            var _languages = userLanguages.Concat_NowSplitStringWithSeperator(";");
            if (_languages.IsNullOrEmpty())
                return;

            foreach (var item in _languages)
            {
                if (item.IsNullOrWhiteSpace())
                    continue;

                string itemSpacesRemovedAndUpperCased = item.RemoveAllSpaces().ToUpper();
                UserLanguages = addRecord(item, dateCreated, isCrawler, dateParam, dataType, UserLanguages);

                //DashBoardSingle ds = new DashBoardSingle(itemSpacesRemovedAndUpperCased, dateCreated, isCrawler, itemSpacesRemovedAndUpperCased, 0, dateParam.BeginDate, dateParam.EndDate, dataType);
                //UserLanguages.DataDetail.Add(ds);

            }


        }

        #region Add Date
        //all the methods below add the data to lists from a list of data which has been filtered within date.

        private void AddUserHostName(string userHostName, DateTime dateCreated, bool isCrawler, DateParameter dateParam, string dataType)
        {
            UserHostNames = addRecord(userHostName, dateCreated, isCrawler, dateParam, dataType, UserHostNames);

        }

        private void AddUserHostAddress(string userHostAddress, DateTime dateCreated, bool isCrawler, DateParameter dateParam, string dataType)
        {
            UserHostAddresses = addRecord(userHostAddress, dateCreated, isCrawler, dateParam, dataType, UserHostAddresses);


        }

        private void AddUserAgent(string userAgent, DateTime dateCreated, bool isCrawler, DateParameter dateParam, string dataType)
        {
            UserAgents = addRecord(userAgent, dateCreated, isCrawler, dateParam, dataType, UserAgents);

        }

        private void AddUrlReferHost(string urlRefererHost, DateTime dateCreated, bool isCrawler, DateParameter dateParam, string dataType)
        {
            ReferalHosts = addRecord(urlRefererHost, dateCreated, isCrawler, dateParam, dataType, ReferalHosts);

        }

        private void AddIsMobileDevice(bool isMobileDevice, DateTime dateCreated, string userAgent, bool isCrawler, DateParameter dateParam, string dataType)
        {
            if (!isMobileDevice)
                return;
            addRecord(isMobileDevice.ToString(), dateCreated, isCrawler, dateParam, dataType, Crawlers);
        }

        private void AddIsCrawler(bool isCrawler, DateTime dateCreated, string userAgent, DateParameter dateParam, string dataType)
        {
            if (!isCrawler)
                return;

            Crawlers = addRecord(isCrawler.ToString(), dateCreated, isCrawler, dateParam, dataType, Crawlers);

        }

        private void AddBrowserType(string browserType, DateTime dateCreated, bool isCrawler, DateParameter dateParam, string dataType)
        {
            addRecord(browserType, dateCreated, isCrawler, dateParam, dataType, BrowserTypes);
        }

        private void AddControllerActions(string controller, string action, DateTime dateCreated, bool isCrawler, DateParameter dateParam, string dataType)
        {
            ControllerActions = addRecord(controller, dateCreated, isCrawler, dateParam, dataType, ControllerActions);
        }


        private DashBoardSingle addRecord(string userName, DateTime dateCreated, bool isCrawler, DateParameter dateParam, string dataType, DashBoardSingle dbsMain)
        {
            if (dataType.IsNullOrWhiteSpace())
                dataType = DashBoardConstants.YEAR;

            string _userName = string.Format("{0}", userName);
            DashBoardSingle ds = new DashBoardSingle(_userName, dateCreated, isCrawler, userName, 0, dateParam.BeginDate, dateParam.EndDate, dataType);
            if (dbsMain.IsNull())
            {
                dbsMain = new DashBoardSingle(string.Format("All"), dateCreated, isCrawler, _userName, 0, dateParam.BeginDate, dateParam.EndDate, dataType);
            }
            dbsMain.DataDetail.Add(ds);
            return dbsMain;

        }

        #endregion



        #region Access Data
        DashBoardSingle _controllerActions;
        public DashBoardSingle ControllerActions 
        {
            get
            {
                
                return _controllerActions;
            }
            set
            {
                _controllerActions = value;
            } 
        }
        public DashBoardSingle ControllerActionsGroupedByYear { get { return GetDataGroupedMain(ControllerActions, DashBoardConstants.YEAR); } }
        public DashBoardSingle ControllerActionsGroupedByYearMonth { get { return GetDataGroupedMain(ControllerActions, DashBoardConstants.YEAR_MONTH); } }
        //public DashBoardSingle ControllerActionsGroupedByYearMonthDay { get { return GetDataGroupedMain(ControllerActions, DashBoardConstants.YEAR_MONTH_D); } }



        public DashBoardSingle BrowserTypes { get; set; }
        public DashBoardSingle BrowserTypesGrouped { get { return GetDataGroupedMain(BrowserTypes, DashBoardConstants.YEAR); } }


        public DashBoardSingle Crawlers { get; set; }
        public DashBoardSingle CrawlersTypeGrouped { get { return GetDataGroupedMain(Crawlers, DashBoardConstants.YEAR); } }




        public DashBoardSingle MobileDeviceConnections { get; set; }
        public DashBoardSingle MobileDeviceConnectionsGrouped { get { return GetDataGroupedMain(MobileDeviceConnections, DashBoardConstants.YEAR); } }




        public DashBoardSingle ReferalHosts { get; set; }
        public DashBoardSingle ReferalHostsGrouped { get { return GetDataGroupedMain(ReferalHosts, DashBoardConstants.YEAR); } }



        public DashBoardSingle UserAgents { get; set; }
        public DashBoardSingle UserAgentsGrouped { get { return GetDataGroupedMain(UserAgents, DashBoardConstants.YEAR); } }


                                                                                                                                                                                                        
        public DashBoardSingle UserHostAddresses { get; set; }
        public DashBoardSingle UserHostAddressesGrouped { get { return GetDataGroupedMain(UserHostAddresses, DashBoardConstants.YEAR); } }




        public DashBoardSingle UserHostNames { get; set; }
        public DashBoardSingle UserHostNamesGrouped { get { return GetDataGroupedMain(UserHostNames, DashBoardConstants.YEAR); } }




        public DashBoardSingle UserLanguages { get; set; }
        public DashBoardSingle UserLanguagesGrouped { get { return GetDataGroupedMain(UserLanguages, DashBoardConstants.YEAR); } }


        public DashBoardSingle UserNames { get; set; }
        public DashBoardSingle UserNamesGrouped { get { return GetDataGroupedMain(UserNames, DashBoardConstants.YEAR); } }



        #endregion


        #region Reccurssion


        /// <summary>
        /// This is the entery point to the reccurssion
        /// </summary>
        /// <param name="dashboradSingleList"></param>
        /// <param name="thisDataType"></param>
        /// <returns></returns>
        public DashBoardSingle GetDataGroupedMain(DashBoardSingle dashboradSingleList, string thisDataType)
        {

            if (dashboradSingleList.IsNull())
                return null;

            return getDataGrouped(dashboradSingleList);
        }


        private DashBoardSingle getDataGrouped(DashBoardSingle dbs)
        {
            ++dbs.ReccursionNum;

            if (dbs.ReccursionNum > DashBoardConstants.MAX_NO_OF_RECCURSIONS)
                return dbs;

            if (dbs.DataDetail.IsNullOrEmpty())
                return dbs;



            if (!dbs.DataType.IsNullOrWhiteSpace())
            {
                switch (dbs.DataType)
                {
                    case DashBoardConstants.YEAR:
                        fixDataForYear(dbs);
                        dbs.DataType = DashBoardConstants.YEAR;

                        //Now group the data by the year
                        dbs.DataGrouped = getDataGrouped_Helper(dbs, DashBoardConstants.YEAR_MONTH);

                        if (!dbs.DataDetail.IsNullOrEmpty())
                        {
                            foreach (var item in dbs.DataGrouped)
                            {
                                item.DataType = DashBoardConstants.YEAR_MONTH;
                                item.DataGrouped = getDataGrouped_Helper(item, DashBoardConstants.YEAR_MONTH_DAY);

                            }
                        }
                        //if you have reached here, now you have all the detail for all the years in
                        //datadetail... i.e. all the data and you have DataGrouped, which contains detailed

                        break;


                    case DashBoardConstants.YEAR_MONTH:
                        fixDataForYearMonth(dbs);
                        getDataGrouped_Helper(dbs, DashBoardConstants.YEAR_MONTH_DAY);

                        break;


                    //case DashBoardConstants.YEAR_MONTH_DAY:
                    //    dataDetail = fixDataForYearMonthDay(dbg.DataDetail);

                    //    dbg.DataGrouped = getDataGrouped_Helper(
                    //        dataDetail, 
                    //        DashBoardConstants.YEAR_MONTH_DAY_HOUR,
                    //        dbg.ReccursionNum,
                    //        DashBoardConstants.YEAR_MONTH_DAY);
                    //    break;


                    //case DashBoardConstants.YEAR_MONTH_DAY_HOUR:
                    //    dataDetail = fixDataForYearMonthDayHour(dbg.DataDetail);
                    //    dbg.DataGrouped = getDataGrouped_Helper(
                    //        dataDetail, 
                    //        DashBoardConstants.YEAR_MONTH_DAY_HOUR_MINUTE,
                    //        dbg.ReccursionNum,
                    //        DashBoardConstants.YEAR_MONTH_DAY_HOUR);
                    //    break;


                    //case DashBoardConstants.YEAR_MONTH_DAY_HOUR_MINUTE:
                    //    dataDetail = fixDataForYearMonthDayHourMinute(dbg.DataDetail);
                    //    dbg.DataGrouped = getDataGrouped_Helper(
                    //        dataDetail, 
                    //        DashBoardConstants.YEAR_MONTH_DAY_HOUR_MINUTE_SECOND,
                    //        dbg.ReccursionNum,
                    //        DashBoardConstants.YEAR_MONTH_DAY_HOUR_MINUTE);
                    //    break;


                    //case DashBoardConstants.YEAR_MONTH_DAY_HOUR_MINUTE_SECOND:
                    //    dataDetail = fixDataForYearMonthDayHourMinuteSecond(dbg.DataDetail);
                    //    dbg.DataGrouped = getDataGrouped_Helper(
                    //        dataDetail, 
                    //        DashBoardConstants.DETAIL,
                    //        dbg.ReccursionNum,
                    //        DashBoardConstants.YEAR_MONTH_DAY_HOUR_MINUTE_SECOND);
                    //    break;


                    default:
                        break;
                }
            }

            return dbs;
        }




        /// <summary>
        /// Note. After this, the DataGroup will contain the groups of DataDetail
        /// </summary>
        /// <param name="fixedData"></param>
        /// <param name="nextGrouping"></param>
        /// <param name="noOfReccursions"></param>
        /// <param name="thisGroupDataType">Eg. Year, Year-Month etc the constants listed to identify where you are coming from</param>
        /// <returns></returns>
        private List<DashBoardSingle> getDataGrouped_Helper(DashBoardSingle dbs, string nextGrouping)
        {
            //Dont allow more than MAX_NO_OF_RECCURSIONS deep recursions.
            if (dbs.ReccursionNum > DashBoardConstants.MAX_NO_OF_RECCURSIONS)
                return null;

            long totalCount = dbs.DataDetail.Count();
            var listOfDataGrouped = dbs
                .DataDetail
                .OrderBy(x => x.DateOfTrx.Year)
                .ThenBy(x => x.DateOfTrx.Month)
                .ThenBy(x => x.DateOfTrx.Month)
                .ThenBy(x => x.DateOfTrx.Day)
                .ThenBy(x => x.DateOfTrx.Hour)
                .ThenBy(x => x.DateOfTrx.Minute)
                .ThenBy(x => x.DateOfTrx.Second)
                .GroupBy(x => x.Key)
                .Select(y =>
                new DashBoardSingle
                {
                    Amount = y.Count(),
                    Key = y.Key,  //The same key
                    Name = makeName(y.First().DateOfTrx, dbs.DataType, y.First().Name),
                    Percent = calculatePct(y.Count(), totalCount),
                    //Percent = y.Count() == 0 ? 0 : (y.Count() / count) & 100,
                    DataDetail = detailForThis(y.Key, dbs.DataDetail), //this is all the detail for this particular year.
                    BeginDate = dbs.BeginDate,
                    EndDate = dbs.EndDate,
                    DataType = DashBoardSingle.NextDataType(y.First().DataType),
                })
                .OrderByDescending(x => x.Percent)
                .ToList();


            return listOfDataGrouped;

        }

        private double calculatePct(double currCount, double totalCount)
        {
            if (currCount == 0)
                return 0;

            double ans = currCount / totalCount * 100;
            return ans;
        }

        private List<DashBoardSingle> detailForThis(string key, List<DashBoardSingle> fixedData)
        {
            var data = fixedData.Where(x => x.Key == key).ToList();
            return data;
        }

        //private List<DashBoardSingle> fixDataForDetail(List<DashBoardSingle> theAction)
        //{
        //    if (!theAction.IsNullOrEmpty())
        //    {
        //        foreach (var item in theAction)
        //        {
        //            string key = DashBoardConstants.DETAIL;

        //        }
        //    }
        //}

        #endregion


        #region Fix Data




        //private List<DashBoardSingle> fixDataForYearMonthDayHourMinuteSecond(List<DashBoardSingle> theAction)
        //{
        //    List<DashBoardSingle> fixedData = new List<DashBoardSingle>();
        //    if (!theAction.IsNullOrEmpty())
        //    {
        //        foreach (var item in theAction)
        //        {
        //            string key = item.DateOfTrx.Year.ToString() +
        //                item.DateOfTrx.Month.ToString() +
        //                item.DateOfTrx.Day.ToString() +
        //                item.DateOfTrx.Hour.ToString() +
        //                item.DateOfTrx.Minute.ToString() +
        //                item.DateOfTrx.Second.ToString();

        //            DashBoardSingle dbs = new DashBoardSingle(item.Name, item.DateOfTrx, item.IsCrawler, key);
        //            fixedData.Add(dbs);
        //        }
        //    }
        //    return fixedData;
        //}

        //private List<DashBoardSingle> fixDataForYearMonthDayHourMinute(List<DashBoardSingle> theAction)
        //{
        //    List<DashBoardSingle> fixedData = new List<DashBoardSingle>();
        //    if (!theAction.IsNullOrEmpty())
        //    {
        //        foreach (var item in theAction)
        //        {
        //            string key = item.DateOfTrx.Year.ToString() +
        //                item.DateOfTrx.Month.ToString() +
        //                item.DateOfTrx.Day.ToString() +
        //                item.DateOfTrx.Hour.ToString() +
        //                item.DateOfTrx.Minute.ToString();

        //            DashBoardSingle dbs = new DashBoardSingle(item.Name, item.DateOfTrx, item.IsCrawler, key);
        //            fixedData.Add(dbs);
        //        }

        //    }
        //    return fixedData;
        //}

        //private List<DashBoardSingle> fixDataForYearMonthDayHour(List<DashBoardSingle> theAction)
        //{
        //    List<DashBoardSingle> fixedData = new List<DashBoardSingle>();
        //    if (!theAction.IsNullOrEmpty())
        //    {
        //        foreach (var item in theAction)
        //        {
        //            string key = item.DateOfTrx.Year.ToString() +
        //                item.DateOfTrx.Month.ToString() +
        //                item.DateOfTrx.Day.ToString() +
        //                item.DateOfTrx.Hour.ToString();
        //            DashBoardSingle dbs = new DashBoardSingle(item.Name, item.DateOfTrx, item.IsCrawler, key);
        //            fixedData.Add(dbs);
        //        }

        //    }
        //    return fixedData;
        //}

        //private List<DashBoardSingle> fixDataForYearMonthDay(List<DashBoardSingle> theAction)
        //{
        //    List<DashBoardSingle> fixedData = new List<DashBoardSingle>();
        //    if (!theAction.IsNullOrEmpty())
        //    {
        //        foreach (var item in theAction)
        //        {
        //            string key = item.DateOfTrx.Year.ToString() +
        //                item.DateOfTrx.Month.ToString() +
        //                item.DateOfTrx.Day.ToString();
        //            //This is the detail
        //            DashBoardSingle dbs = new DashBoardSingle(item.Name, item.DateOfTrx, item.IsCrawler, key);
        //            fixedData.Add(dbs);
        //        }

        //    }
        //    return fixedData;
        //}

        private void fixDataForYearMonth(DashBoardSingle theData)
        {
            if (theData.DataDetail.IsNullOrEmpty())
                return;

            foreach (var item in theData.DataDetail)
            {
                string key = item.DateOfTrx.Year.ToString() +
                    item.DateOfTrx.Month.ToString();
            }

        }


        //we are creating the detailed data. The in is the raw data which is in DataDetail
        //the raw data key is adjusted so that it has the year in it.
        private void fixDataForYear(DashBoardSingle theData)
        {
            if (theData.DataDetail.IsNullOrEmpty())
                return;

            foreach (var item in theData.DataDetail)
            {
                string key = item.DateOfTrx.Year.ToString();
            }

        }


        #endregion



        //private List<DashBoardSingle> GetDetailedDataByYear(List<DashBoardSingle> theAction, string name)
        //{
        //    //remove the time from all the dates

        //    List<DashBoardSingleItem> groupedDateList = new List<DashBoardSingleItem>();
        //    if (!theAction.IsNullOrEmpty())
        //    {
        //        List<DashBoardSingle> fixedAction = new List<DashBoardSingle>();

        //        foreach (var item in theAction.Where(x => x.Name == name).AsEnumerable())
        //        {
        //            DashBoardSingle dbs = new DashBoardSingle(item.Name, new DateTime(item.DateOfTrx.Year, 0, 0, 0, 0, 0), item.IsCrawler);   
        //        }

        //        var _ttlCountOfFixedAction = fixedAction.Count();
        //        //now group by date

        //    }
        //    return groupedDateList;
        //}


        #region Helpers


        private List<DashBoardSingle> getDetailedData(List<DashBoardSingle> theAction, string name)
        {
            return theAction.Where(x => x.Name == name)
                .OrderByDescending(z => z.DateOfTrx)
                .ToList();
        }
        private double getPercentage(double amount, double totalCount)
        {
            double amt = amount;
            double ttl = totalCount;

            if (amt == 0)
                return 0;
            return amt / ttl * 100;
        }
        private string makeName(DateTime dateTime, string dataType, string oldName)
        {
            string name;
            switch (dataType)
            {
                case DashBoardConstants.YEAR:
                    name = string.Format("{0}- {1}", oldName, dateTime.Year.ToString());
                    break;
                case DashBoardConstants.YEAR_MONTH:
                    name = string.Format("{0}- {1} - {2}", oldName, dateTime.Year.ToString(), dateTime.Month.ToString());

                    break;
                case DashBoardConstants.YEAR_MONTH_DAY:
                    name = string.Format("({0} - {1}) - {2}", dateTime.Year.ToString(), dateTime.Month.ToString(), dateTime.Day.ToString());
                    break;
                case DashBoardConstants.YEAR_MONTH_DAY_HOUR:
                    name = string.Format("{0} - {1} - {2} -Hr: {3}", dateTime.Year.ToString(), dateTime.Month.ToString(), dateTime.Day.ToString(), dateTime.Hour.ToString());
                    break;
                case DashBoardConstants.YEAR_MONTH_DAY_HOUR_MINUTE:
                    name = string.Format("{0} - {1} - {2} -Hr: {3}:{4}", dateTime.Year.ToString(), dateTime.Month.ToString(), dateTime.Day.ToString(), dateTime.Hour.ToString(), dateTime.Minute.ToString());
                    break;
                case DashBoardConstants.YEAR_MONTH_DAY_HOUR_MINUTE_SECOND:
                    name = string.Format("{0} - {1} - {2} -Hr: {3}:{4}:{5}", dateTime.Year.ToString(), dateTime.Month.ToString(), dateTime.Day.ToString(), dateTime.Hour.ToString(), dateTime.Minute.ToString(), dateTime.Second.ToString());
                    break;
                default:
                    name = dateTime.ToShortDateString();
                    break;
            }
            return name;

        }
        #endregion


        //private string makeFinalName(int noOfReccursions, string thisGroupDataType, IGrouping<string, DashBoardSingle> y)
        //{
        //    return (noOfReccursions == MAX_NO_OF_RECCURSIONS ? y.First().Name : makeName(y.First().DateOfTrx, thisGroupDataType));
        //}


    }
}
