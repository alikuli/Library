using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}