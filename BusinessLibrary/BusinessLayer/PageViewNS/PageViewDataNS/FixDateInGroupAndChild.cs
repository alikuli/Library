using ModelsClassLibrary.ModelsNS.DashBoardNS;
using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;

namespace UowLibrary.PageViewNS.PageViewDataNS
{



    /// <summary>
    /// This prepares the data for the dash board.
    /// First thing it is preparing is the PageViews.
    /// </summary>
    public partial class PageViewData
    {



        public DashBoardSingle FixDateInGroupAndChild(DashBoardSingle dbsGrouped, string groupBy)
        {
            addTrxDateToDataGroupsFor(dbsGrouped.DataGrouped, groupBy);
            return dbsGrouped;
        }


        
        /// <summary>
        /// TWe have received a list of groups. Now we will update the DateOfTrx of each
        /// group according to its groupBy and the date will come from the list.DataList
        /// i.e. the data which makes the group up. If the groupBy is Year, then only the
        /// year part of the data will be taken
        /// </summary>
        /// <param name="list"></param>
        /// <param name="groupBy"></param>
        private void addTrxDateToDataGroupsFor(List<DashBoardSingle> list, string groupBy)
        {
            if (list.IsNull())
                return;

            foreach (var item in list)
            {
                item.DateOfTrx = DateTime.MinValue;

                switch (groupBy)
                {
                    case GroupByConstants.MAIN:
                    case GroupByConstants.NAME:
                        break;


                    case GroupByConstants.YEAR:
                    case GroupByConstants.YEAR_MONTH:
                    case GroupByConstants.YEAR_MONTH_DAY:
                    case GroupByConstants.YEAR_MONTH_DAY_HOUR:
                    case GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE:
                    case GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE_SECOND:

                        getDateForGroup(item);
                        break;

                    default:
                        break;

                }

            }
        }


        private static void getDateForGroup(DashBoardSingle item)
        {
            if (item.DataDetail.IsNull())
                return;

            item.DateOfTrx = item.DataDetail.First().DateOfTrx;

            //Now fix the date for the Child
            foreach (var childGroupedData in item.DataGrouped)
            {
                childGroupedData.DateOfTrx = childGroupedData.DataDetail.First().DateOfTrx;
            }
        }

    }
}
