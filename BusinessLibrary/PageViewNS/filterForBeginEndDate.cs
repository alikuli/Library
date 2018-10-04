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


        List<PageView> _pageViewList;

        List<PageView> PageViewList
        {
            get
            {
                return _pageViewList ?? (_pageViewList = FindAll().ToList());
            }
        }

        /// <summary>
        /// This shapes the data. It basically adds all the dates that fall within begin date and end date
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        private List<PageView> filterForBeginEndDate(DateParameter dp)
        {

            dp.ErrorCheck();

            List<PageView> lstWithinDate = new List<PageView>();

            if (lstWithinDate.IsNullOrEmpty())
            {
                lstWithinDate = new List<PageView>();

                foreach (PageView pageView in PageViewList)
                {
                    DateTime date =
                        pageView.MetaData.Created.Date ??
                        DateTime.MinValue;

                    if (dp.IsDateWithinBeginAndEndDatesInclusive(date))
                        lstWithinDate.Add(pageView);
                }

              }
            return lstWithinDate;


        }







    }
}
