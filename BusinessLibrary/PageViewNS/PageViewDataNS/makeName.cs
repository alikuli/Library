using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DashBoardNS;
using System;

namespace UowLibrary.PageViewNS.PageViewDataNS
{
    /// <summary>
    /// This prepares the data for the dash board.
    /// First thing it is preparing is the PageViews.
    /// </summary>
    public partial class PageViewData
    {

        #region Helpers


        private static string makeName(DateTime dateTime, string groupBy, string dataOwner, string nameRaw)
        {
            string calculatedName;
            string name = nameRaw.ToTitleSentance();
            switch (groupBy)
            {

                case GroupByConstants.NAME:
                    //calculatedName = string.Format("{0}  {1}", name, dateTime.Year.ToString(), dateTime.Month.ToString());
                    string nameToDisplay = "(Blank)";
                    if (!nameRaw.IsNullOrWhiteSpace())
                        nameToDisplay = nameRaw.ToTitleSentance();
                    calculatedName = string.Format("{0}", nameToDisplay);
                    break;

                case GroupByConstants.YEAR:
                    //calculatedName = string.Format("{0}  {1}", name, dateTime.Year.ToString(), dateTime.Month.ToString());
                    calculatedName = string.Format("{0}", dateTime.Year.ToString());
                    break;


                case GroupByConstants.YEAR_MONTH:
                    calculatedName = string.Format("{1}/{2} ", name, dateTime.Year.ToString(), dateTime.Month.ToString(), dateTime.Day.ToString());
                    break;


                case GroupByConstants.YEAR_MONTH_DAY:
                    calculatedName = string.Format("{1}/{2}/{3}", name, dateTime.Year.ToString(), dateTime.Month.ToString(), dateTime.Day.ToString(), dateTime.Hour.ToString());
                    break;


                case GroupByConstants.YEAR_MONTH_DAY_HOUR:
                    calculatedName = string.Format("{1}/{2}/{3} at {4} Hours", name, dateTime.Year.ToString(), dateTime.Month.ToString(), dateTime.Day.ToString(), dateTime.Hour.ToString(), dateTime.Minute.ToString());
                    break;


                case GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE:
                    calculatedName = string.Format("{1}/{2}/{3} at {4}:{5} Hours", name, dateTime.Year.ToString(), dateTime.Month.ToString(), dateTime.Day.ToString(), dateTime.Hour.ToString(), dateTime.Minute.ToString(), dateTime.Second.ToString());
                    break;


                case GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE_SECOND:
                    calculatedName = string.Format("{1}/{2}/{3} at {4}:{5}:{6} Hours",
                        name,
                        dateTime.Year.ToString(),
                        dateTime.Month.ToString(),
                        dateTime.Day.ToString(),
                        dateTime.Hour.ToString(),
                        dateTime.Minute.ToString(),
                        dateTime.Second.ToString());
                    break;


                default:
                    calculatedName = dataOwner;
                    break;
            }
            return calculatedName;

        }
        #endregion


    }
}
