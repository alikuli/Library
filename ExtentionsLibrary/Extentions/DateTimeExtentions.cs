using System;

namespace AliKuli.Extentions
{
    public static class DateTimeExtentions
    {

        public static bool IsMinOrMax(this DateTime currDate)
        {
            return currDate == DateTime.MinValue || currDate == DateTime.MaxValue;
        }
        public static bool IsMinOrMaxOrNull(this DateTime? currDate)
        {
            return currDate == null || currDate == DateTime.MinValue || currDate == DateTime.MaxValue;
        }

        public static bool IsGreaterThan(this DateTime currDate, DateTime dateIn)
        {
            return currDate > dateIn;
        }
        public static bool IsLessThan(this DateTime currDate, DateTime dateIn)
        {
            return currDate < dateIn;
        }








        public static double ToWeeks(this TimeSpan timeSpan)
        {
            if (timeSpan.Days < 7)
                return 0;

            return timeSpan.Days / 7;
        }
        public static double ToMonths(this TimeSpan timeSpan)
        {
            if (timeSpan.Days < 365)
                return 0;

            return timeSpan.Days / 30;
        }

        public static double ToYears(this TimeSpan timeSpan)
        {
            if (timeSpan.TotalDays < 365.2425)
                return 0;

            return (timeSpan.TotalDays / 365.2425);
        }


    }
}