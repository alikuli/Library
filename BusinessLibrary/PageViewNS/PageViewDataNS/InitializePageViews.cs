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


                    addBrowserType(browserType, dateCreated, isCrawler, dateParam, groupBy);
                    break;



                case DataOwner.CONTROLLER:
                    addControllerActions(controller, action, dateCreated, isCrawler, dateParam, groupBy);
                    break;



                case DataOwner.CRAWLER:
                    addIsCrawler(isCrawler, dateCreated, userAgent, dateParam, groupBy);
                    break;


                case DataOwner.MOBILE_DEVICE:
                    addIsMobileDevice(isMobileDevice, dateCreated, userAgent, isCrawler, dateParam, groupBy);
                    break;


                case DataOwner.REFERAL_HOST:
                    addUrlReferHost(urlRefererHost, dateCreated, isCrawler, dateParam, groupBy);
                    break;


                case DataOwner.USER_AGENT:
                    addUserAgent(userAgent, dateCreated, isCrawler, dateParam, groupBy);
                    break;


                case DataOwner.USER_HOST_ADDRESS:
                    addUserHostAddress(userHostAddress, dateCreated, isCrawler, dateParam, groupBy);
                    break;


                case DataOwner.USER_HOST_NAMES:
                    addUserHostName(userHostName, dateCreated, isCrawler, dateParam, groupBy);
                    break;


                case DataOwner.USER_LANGUAGES:
                    addUserLanguages(userLanguages, dateCreated, isCrawler, dateParam, groupBy);
                    break;


                case DataOwner.USER_NAMES:
                    addUserName(userName, dateCreated, isCrawler, dateParam, groupBy);
                    break;

                case GroupByConstants.ALL:
                default:

                    //if blank... do all
                    addControllerActions(controller, action, dateCreated, isCrawler, dateParam, groupBy);
                    addBrowserType(browserType, dateCreated, isCrawler, dateParam, groupBy);
                    addIsCrawler(isCrawler, dateCreated, userAgent, dateParam, groupBy);
                    addIsMobileDevice(isMobileDevice, dateCreated, userAgent, isCrawler, dateParam, groupBy);
                    addUrlReferHost(urlRefererHost, dateCreated, isCrawler, dateParam, groupBy);
                    addUserAgent(userAgent, dateCreated, isCrawler, dateParam, groupBy);
                    addUserHostAddress(userHostAddress, dateCreated, isCrawler, dateParam, groupBy);
                    addUserHostName(userHostName, dateCreated, isCrawler, dateParam, groupBy);
                    addUserLanguages(userLanguages, dateCreated, isCrawler, dateParam, groupBy);
                    addUserName(userName, dateCreated, isCrawler, dateParam, groupBy);
                    break;
            }



        }





        //all the methods below add the data to lists from a list of data which has been filtered within date.
        private void addUserName(
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



        private void addUserLanguages(
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



        private void addUserHostName(
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



        private void addUserHostAddress(
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



        private void addUserAgent(string userAgent,
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



        private void addUrlReferHost(
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



        private void addIsMobileDevice(
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



        private void addIsCrawler(
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



        private void addBrowserType(
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




        private void addControllerActions(
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
