using ModelsClassLibrary.ModelsNS.DashBoardNS;
using ModelsClassLibrary.ModelsNS.PageViewNS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UowLibrary.PageViewNS.PageViewDataNS
{
    /// <summary>
    /// This prepares the data for the dash board.
    /// First thing it is preparing is the PageViews.
    /// </summary>
    public partial class PageViewData
    {


        public DashBoardSingle SqlController(DashBoardSingle dbs, string groupBy)
        {
            string nextDataType = DashBoardSingle.NextGroupBy(groupBy);
            dbs.DataDetail = dbs.DataDetail.OrderBy(x => x.Key).ToList();
            dbs.DataGrouped = sql(dbs, groupBy, nextDataType);

            string nextDataTypeAfterNext = DashBoardSingle.NextGroupBy(nextDataType);
            foreach (var item in dbs.DataGrouped)
            {
                item.DataGrouped = sql(item, nextDataType, nextDataTypeAfterNext);
            }
            dbs.DataGrouped = dbs.DataGrouped.OrderBy(x => x.NameCalculated).ToList();
            return dbs;
        }



    }
}
