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
                    string hrs = convertHoursToAmAndPm(dateTime.Hour);
                    calculatedName = string.Format("{1}/{2}/{3} at {4}", name, dateTime.Year.ToString(), dateTime.Month.ToString(), dateTime.Day.ToString(), hrs);
                    break;


                case GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE:
                    calculatedName = string.Format("{1}/{2}/{3} at {4} and {5} Minutes", name, dateTime.Year.ToString(), dateTime.Month.ToString(), dateTime.Day.ToString(), convertHoursToAmAndPm(dateTime.Hour), dateTime.Minute.ToString(), dateTime.Second.ToString());
                    break;


                case GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE_SECOND:
                    calculatedName = string.Format("{1}/{2}/{3} at {4} {5} Minutes {6} Seconds",
                        name,
                        dateTime.Year.ToString(),
                        dateTime.Month.ToString(),
                        dateTime.Day.ToString(),
                        convertHoursToAmAndPm(dateTime.Hour),
                        dateTime.Minute.ToString(),
                        dateTime.Second.ToString());
                    break;


                default:
                    calculatedName = dataOwner;
                    break;
            }
            return calculatedName;

        }

        private static string makeNameForSorting(DateTime dateTime, string groupBy, string dataOwner, string nameRaw)
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
                case GroupByConstants.YEAR_MONTH:
                case GroupByConstants.YEAR_MONTH_DAY:
                case GroupByConstants.YEAR_MONTH_DAY_HOUR:
                case GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE:
                case GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE_SECOND:
                    calculatedName = string.Format("{0}", dateTime.Year.ToString());
                    break;


                default:
                    calculatedName = dataOwner;
                    break;
            }
            return calculatedName;

        }
        private static string convertHoursToAmAndPm(int p)
        {
            string hrs = "";
            switch (p)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:

                    if (p == 0)
                        p = 12;
                    hrs = string.Format("{0} AM", p);
                    break;


                default:
                    int fixedHrs = p - 12;
                    if (fixedHrs == 0)
                        fixedHrs = 12;

                    hrs = string.Format("{0} PM", fixedHrs);
                    break;
            }

            return hrs;
        }
        #endregion


    }
}
