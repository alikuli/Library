using ModelsClassLibrary.ModelsNS.DashBoardNS;
using System.Linq;

namespace UowLibrary.PageViewNS.PageViewDataNS
{
    /// <summary>
    /// This prepares the data for the dash board.
    /// First thing it is preparing is the PageViews.
    /// </summary>
    public partial class PageViewData
    {

        /// <summary>
        /// This first sorts the DataDetail according to it's key, then groups the main DataDetail, and then the ChildDataDetail
        /// </summary>
        /// <param name="dbs"></param>
        /// <param name="groupBy"></param>
        /// <returns></returns>
        public DashBoardSingle Controller_SQL(DashBoardSingle dbs, string groupBy)
        {

            dbs.DataDetail = dbs.DataDetail.OrderBy(x => x.Key).ToList();


            string nextDataType = DashBoardSingle.NextGroupBy(groupBy);
            dbs.DataGrouped = sql(dbs, groupBy, nextDataType);



            string nextDataTypeAfterNext = DashBoardSingle.NextGroupBy(nextDataType);
            groupTheChild(dbs, nextDataType, nextDataTypeAfterNext);

            //Now sort the Main Grouped items. (required for a good output)
            //dbs.DataGrouped = dbs.DataGrouped.OrderBy(x => x.NameCalculated).ToList();
            //dbs.DataGrouped = dbs.DataGrouped.OrderBy(x => x.NameForSorting).ToList();


            return dbs;
        }


        /// <summary>
        /// This groups the child item
        /// </summary>
        /// <param name="dbs"></param>
        /// <param name="nextDataType"></param>
        /// <param name="nextDataTypeAfterNext"></param>
        private void groupTheChild(DashBoardSingle dbs, string nextDataType, string nextDataTypeAfterNext)
        {
            foreach (var item in dbs.DataGrouped)
            {
                item.DataGrouped = sql(item, nextDataType, nextDataTypeAfterNext);
            }
        }












    }
}
