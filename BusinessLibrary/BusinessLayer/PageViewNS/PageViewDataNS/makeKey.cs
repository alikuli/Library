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

        private static string makeKey(string groupBy, DateTime dateOfTrx, string belongsToGroup, string name)
        {
            string key = belongsToGroup;
            switch (groupBy)
            {
                case GroupByConstants.MAIN:
                    break;

                case GroupByConstants.NAME:
                    key += name;

                    break;
                case GroupByConstants.YEAR:
                    key += name + "Y" + dateOfTrx.Year.ToString();
                    break;


                case GroupByConstants.YEAR_MONTH:
                    key += name + "Y" + dateOfTrx.Year.ToString() +
                    "M" + dateOfTrx.Month.ToString();

                    break;


                case GroupByConstants.YEAR_MONTH_DAY:
                    key += name + "Y" + dateOfTrx.Year.ToString() +
                    "M" + dateOfTrx.Month.ToString() +
                    "D" + dateOfTrx.Day.ToString();

                    break;


                case GroupByConstants.YEAR_MONTH_DAY_HOUR:
                    key += name + "Y" + dateOfTrx.Year.ToString() +
                    "M" + dateOfTrx.Month.ToString() +
                    "D" + dateOfTrx.Day.ToString() +
                    "H " + dateOfTrx.Hour.ToString();
                    break;


                case GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE:
                    key += name + "Y" + dateOfTrx.Year.ToString() +
                    "M" + dateOfTrx.Month.ToString() +
                    "D" + dateOfTrx.Day.ToString() +
                    "H " + dateOfTrx.Hour.ToString() +
                    "M" + dateOfTrx.Minute.ToString();

                    break;


                case GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE_SECOND:
                    key += name + "Y" + dateOfTrx.Year.ToString() +
                    "M" + dateOfTrx.Month.ToString() +
                    "D" + dateOfTrx.Day.ToString() +
                    "H " + dateOfTrx.Hour.ToString() +
                    "M" + dateOfTrx.Minute.ToString() +
                    "S" + dateOfTrx.Second.ToString(); ;

                    break;

                default:
                    break;

            }
            return key;
        }




    }
}
