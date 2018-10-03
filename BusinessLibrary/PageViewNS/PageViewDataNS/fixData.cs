using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DashBoardNS;
namespace UowLibrary.PageViewNS.PageViewDataNS
{
    /// <summary>
    /// This prepares the data for the dash board.
    /// First thing it is preparing is the PageViews.
    /// </summary>
    public partial class PageViewData
    {
        //GroupName. Eg Controller
        private DashBoardSingle FixDataForYearMonthDayHourMinuteSecond(DashBoardSingle theData)
        {
            if (!theData.DataDetail.IsNullOrEmpty())
            {
                foreach (var item in theData.DataDetail)
                {
                    item.Key = makeKey(GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE_SECOND, item.DateOfTrx, item.BelongsToGroup, item.Name);
                }
            }

            return theData;
        }

        private DashBoardSingle FixDataForYearMonthDayHourMinute(DashBoardSingle theData)
        {
            if (!theData.DataDetail.IsNullOrEmpty())
            {
                foreach (var item in theData.DataDetail)
                {
                    item.Key = makeKey(GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE, item.DateOfTrx, item.BelongsToGroup, item.Name);
                }
            }

            return theData;

        }

        private DashBoardSingle FixDataForYearMonthDayHour(DashBoardSingle theData)
        {

            if (!theData.DataDetail.IsNullOrEmpty())
            {
                foreach (var item in theData.DataDetail)
                {
                    item.Key = makeKey(GroupByConstants.YEAR_MONTH_DAY_HOUR, item.DateOfTrx, item.BelongsToGroup, item.Name); ;
                }
            }

            return theData;

        }

        private DashBoardSingle FixDataForYearMonthDay(DashBoardSingle theData)
        {
            if (!theData.DataDetail.IsNullOrEmpty())
            {
                foreach (var item in theData.DataDetail)
                {
                    item.Key = makeKey(GroupByConstants.YEAR_MONTH_DAY, item.DateOfTrx, item.BelongsToGroup, item.Name);
                }
            }

            return theData;

        }

        private DashBoardSingle FixDataForYearMonth(DashBoardSingle theData)
        {
            if (!theData.DataDetail.IsNullOrEmpty())
            {
                foreach (var item in theData.DataDetail)
                {
                    item.Key = makeKey(GroupByConstants.YEAR_MONTH, item.DateOfTrx, item.BelongsToGroup, item.Name);
                }
            }

            return theData;

        }

        //we are creating the detailed data. The in is the raw data which is in DataDetail
        //the raw data key is adjusted so that it has the year in it.
        private DashBoardSingle FixDataForYear(DashBoardSingle theData)
        {
            if (!theData.DataDetail.IsNullOrEmpty())
            {
                foreach (var item in theData.DataDetail)
                {
                    item.Key = makeKey(GroupByConstants.YEAR, item.DateOfTrx, item.BelongsToGroup, item.Name);
                }
            }

            return theData;

        }

        private DashBoardSingle FixDataForMain(DashBoardSingle theData)
        {
            if (!theData.DataDetail.IsNullOrEmpty())
            {
                foreach (var item in theData.DataDetail)
                {
                    item.Key = makeKey(GroupByConstants.MAIN, item.DateOfTrx, item.ShowDataFor, item.Name);
                }
            }
            return theData;
        }


        public DashBoardSingle FixDataController(DashBoardSingle dbs, string dataType)
        {
            if (dbs.IsNull())
                return dbs;

            if (dbs.DataDetail.IsNull())
                return dbs;

            switch (dataType)
            {
                case GroupByConstants.MAIN:
                    dbs = FixDataForMain(dbs);
                    break;

                case GroupByConstants.YEAR:
                    dbs = FixDataForYear(dbs);
                    break;

                case GroupByConstants.YEAR_MONTH:
                    dbs = FixDataForYearMonth(dbs);
                    break;

                case GroupByConstants.YEAR_MONTH_DAY:
                    dbs = FixDataForYearMonthDay(dbs);
                    break;

                case GroupByConstants.YEAR_MONTH_DAY_HOUR:
                    dbs = FixDataForYearMonthDayHour(dbs);
                    break;

                case GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE:
                    dbs = FixDataForYearMonthDayHourMinute(dbs);
                    break;

                case GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE_SECOND:
                    dbs = FixDataForYearMonthDayHourMinuteSecond(dbs);
                    break;


                default:
                    break;
            }

            return dbs;
        }
    }
}
