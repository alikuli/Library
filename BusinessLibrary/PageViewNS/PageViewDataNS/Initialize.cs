using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DashBoardNS;
using ModelsClassLibrary.ModelsNS.PageViewNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
namespace UowLibrary.PageViewNS.PageViewDataNS
{
    /// <summary>
    /// This prepares the data for the dash board.
    /// First thing it is preparing is the PageViews.
    /// </summary>
    public partial class PageViewData
    {
        public PageViewData()
        {
            DataAll = new DashBoardSingle();
        }

        public DashBoardSingle DataAll { get; set; }


        /// <summary>
        /// This is to be used for getting data from the PageViews. Filtered data by date is to be passed here.
        /// If DataType is not blank, then only that item's data is received.
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
        private void InitializePageViews(
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
            string groupBy,
            string showDataFor)
        {

            if (showDataFor.IsNullOrWhiteSpace())
                showDataFor = GroupByConstants.ALL;

            //DataAll is the one that holds the data, the data here is just a dummy.
            DataAll.BeginDate = dateParam.BeginDate;
            DataAll.EndDate = dateParam.EndDate;
            DataAll.ShowDataFor = showDataFor;
            DataAll.GroupBy = groupBy;


            switch (groupBy)
            {
                case DataOwner.BROWSER:


                    AddBrowserType(browserType, dateCreated, isCrawler, dateParam, groupBy);
                    break;



                case DataOwner.CONTROLLER:
                    AddControllerActions(controller, action, dateCreated, isCrawler, dateParam, groupBy);
                    break;



                case DataOwner.CRAWLER:
                    AddIsCrawler(isCrawler, dateCreated, userAgent, dateParam, groupBy);
                    break;


                case DataOwner.MOBILE_DEVICE:
                    AddIsMobileDevice(isMobileDevice, dateCreated, userAgent, isCrawler, dateParam, groupBy);
                    break;


                case DataOwner.REFERAL_HOST:
                    AddUrlReferHost(urlRefererHost, dateCreated, isCrawler, dateParam, groupBy);
                    break;


                case DataOwner.USER_AGENT:
                    AddUserAgent(userAgent, dateCreated, isCrawler, dateParam, groupBy);
                    break;


                case DataOwner.USER_HOST_ADDRESS:
                    AddUserHostAddress(userHostAddress, dateCreated, isCrawler, dateParam, groupBy);
                    break;


                case DataOwner.USER_HOST_NAMES:
                    AddUserHostName(userHostName, dateCreated, isCrawler, dateParam, groupBy);
                    break;


                case DataOwner.USER_LANGUAGES:
                    AddUserLanguages(userLanguages, dateCreated, isCrawler, dateParam, groupBy);
                    break;


                case DataOwner.USER_NAMES:
                    AddUserName(userName, dateCreated, isCrawler, dateParam, groupBy);
                    break;
                
                case GroupByConstants.ALL:
                default:

                    //if blank... do all
                    AddControllerActions(controller, action, dateCreated, isCrawler, dateParam, groupBy);
                    AddBrowserType(browserType, dateCreated, isCrawler, dateParam, groupBy);
                    AddIsCrawler(isCrawler, dateCreated, userAgent, dateParam, groupBy);
                    AddIsMobileDevice(isMobileDevice, dateCreated, userAgent, isCrawler, dateParam, groupBy);
                    AddUrlReferHost(urlRefererHost, dateCreated, isCrawler, dateParam, groupBy);
                    AddUserAgent(userAgent, dateCreated, isCrawler, dateParam, groupBy);
                    AddUserHostAddress(userHostAddress, dateCreated, isCrawler, dateParam, groupBy);
                    AddUserHostName(userHostName, dateCreated, isCrawler, dateParam, groupBy);
                    AddUserLanguages(userLanguages, dateCreated, isCrawler, dateParam, groupBy);
                    AddUserName(userName, dateCreated, isCrawler, dateParam, groupBy);
                    break;
            }



        }


        public DashBoardSingle InitializePageViews(
            List<PageView> pageViewList,
            DateParameter dateParam,
            string groupBy,
            string showDataFor)
        {
            if (pageViewList.IsNullOrEmpty())
                return null;




            //this is running through the raw list and converting it to its type.
            //Data is saved in a property DataAll
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
                    dateParam,
                    groupBy,
                    showDataFor);
            }

            return DataAll;

        }


        //all the methods below add the data to lists from a list of data which has been filtered within date.
        private void AddUserName(
            string userName,
            DateTime dateCreated,
            bool isCrawler,
            DateParameter dateParam,
            string groupBy)
        {
            addRecord(
                userName,
                dateCreated,
                isCrawler,
                dateParam,
                groupBy,
                DataOwner.USER_NAMES,
                DataOwner.USER_NAMES);
        }


        private void AddUserLanguages(
            string userLanguages,
            DateTime dateCreated,
            bool isCrawler,
            DateParameter dateParam,
            string groupBy)
        {

            if (userLanguages.IsNullOrWhiteSpace())
                return;

            if (groupBy.IsNullOrWhiteSpace())
                return;


            var _languages = userLanguages.Concat_NowSplitStringWithSeperator(";");
            if (_languages.IsNullOrEmpty())
                return;


            foreach (var item in _languages)
            {
                if (item.IsNullOrWhiteSpace())
                    continue;

                string itemSpacesRemovedAndUpperCased = item.RemoveAllSpaces().ToUpper();

                if (itemSpacesRemovedAndUpperCased.IsNullOrWhiteSpace())
                    continue;

                addRecord(
                    itemSpacesRemovedAndUpperCased,
                    dateCreated,
                    isCrawler,
                    dateParam,
                    groupBy,
                    DataOwner.USER_HOST_NAMES,
                    DataOwner.USER_HOST_NAMES);

            }
        }



        private void AddUserHostName(
            string userHostName,
            DateTime dateCreated,
            bool isCrawler,
            DateParameter dateParam,
            string groupBy)
        {
            addRecord(
                userHostName,
                dateCreated,
                isCrawler,
                dateParam,
                groupBy,
                DataOwner.USER_HOST_NAMES,
                DataOwner.USER_HOST_NAMES);

        }

        private void AddUserHostAddress(
            string userHostAddress,
            DateTime dateCreated,
            bool isCrawler,
            DateParameter dateParam,
            string groupBy)
        {
            addRecord(
                userHostAddress,
                dateCreated,
                isCrawler,
                dateParam,
                groupBy,
                DataOwner.USER_HOST_ADDRESS,
                DataOwner.USER_HOST_ADDRESS);


        }

        private void AddUserAgent(string userAgent,
            DateTime dateCreated,
            bool isCrawler,
            DateParameter dateParam,
            string groupBy)
        {
            addRecord(
                userAgent,
                dateCreated,
                isCrawler,
                dateParam,
                groupBy,
                DataOwner.USER_AGENT,
                DataOwner.USER_AGENT);

        }

        private void AddUrlReferHost(
            string urlRefererHost,
            DateTime dateCreated,
            bool isCrawler,
            DateParameter dateParam,
            string groupBy)
        {
            addRecord(
                urlRefererHost,
                dateCreated,
                isCrawler,
                dateParam,
                groupBy,
                DataOwner.REFERAL_HOST,
                DataOwner.REFERAL_HOST);

        }

        private void AddIsMobileDevice(
            bool isMobileDevice,
            DateTime dateCreated,
            string userAgent,
            bool isCrawler,
            DateParameter dateParam,
            string groupBy)
        {
            if (!isMobileDevice)
                return;

            addRecord(
                isMobileDevice.ToString(),
                dateCreated,
                isCrawler,
                dateParam,
                groupBy,
                DataOwner.MOBILE_DEVICE,
                DataOwner.MOBILE_DEVICE);


        }

        private void AddIsCrawler(
            bool isCrawler,
            DateTime dateCreated,
            string userAgent,
            DateParameter dateParam,
            string groupBy)
        {

            if (!isCrawler)
                return;


            addRecord(
                isCrawler.ToString(),
                dateCreated,
                isCrawler,
                dateParam,
                groupBy,
                DataOwner.CRAWLER,
                DataOwner.CRAWLER);


        }

        private void AddBrowserType(
            string browserType,
            DateTime dateCreated,
            bool isCrawler,
            DateParameter dateParam,
            string groupBy)
        {
            addRecord(
                browserType,
                dateCreated,
                isCrawler, dateParam,
                groupBy,
                DataOwner.BROWSER,
                DataOwner.BROWSER);

        }

        private void AddControllerActions(
            string controller,
            string action,
            DateTime dateCreated,
            bool isCrawler,
            DateParameter dateParam,
            string groupBy)
        {
            addRecord(
                controller,
                dateCreated,
                isCrawler,
                dateParam,
                groupBy,
                DataOwner.CONTROLLER,
                DataOwner.CONTROLLER);

        }



        private void addRecord(
                string name,
                DateTime dateCreated,
                bool isCrawler,
                DateParameter dateParam,
                string groupBy,
                string showDataFor,
                string belongsToGroup)
        {
            //if (groupBy.IsNullOrWhiteSpace())
            //    groupBy = DashBoardConstants.YEAR;

            string _userName = string.Format("{0}", name);

            DashBoardSingle ds = new DashBoardSingle(
                _userName,
                dateCreated,
                isCrawler,
                showDataFor,
                0,
                dateParam.BeginDate,
                dateParam.EndDate,
                groupBy,
                showDataFor,
                belongsToGroup);

            DataAll.DataDetail.Add(ds);
        }





    }
}
