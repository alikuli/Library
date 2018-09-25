//using ModelsClassLibrary.ModelsNS.DashBoardNS;
//using System.Collections.Generic;
//using AliKuli.Extentions;
//using System;
//using System.Linq;

//namespace UowLibrary.DashboardNS
//{
//    public class DashBoardBiz
//    {
//        static DashBoardMain _dashboardMain;
//        public static DashBoardBiz(DashBoardMain dashboardMain)
//        {
//            _dashboardMain = dashboardMain;
//        }

//        //static const string MINDATE = "01-Jan-2010";
//        //static const string MAXDATE = "31-Dec-2050";
//        //public static long GetCount(List<DashBoardSingle> list, string minDate = MINDATE, string maxDate = MAXDATE)
//        //{
//        //    if (list.IsNullThrowException())
//        //        return 0;

//        //    DateTime _minDate = DateTime.Parse(minDate);
//        //    DateTime _maxDate = DateTime.Parse(maxDate);

//        //    long count = list.Where(x => x.DateOfTrx >= _minDate && x.DateOfTrx <= _maxDate).Count();

//        //    return list.Count;
//        //}

//        ///// <summary>
//        ///// This groups a list by name and returns the amount for each in a List<DashBoardGroupResult> ()
//        ///// </summary>
//        ///// <param name="list"></param>
//        ///// <param name="minDate"></param>
//        ///// <param name="maxDate"></param>
//        ///// <returns></returns>
//        //public static List<DashBoardResult> GetCountByGroup(List<DashBoardSingle> list, string minDate = MINDATE, string maxDate = MAXDATE)
//        //{
//        //    if (list.IsNullThrowException())
//        //        return null;

//        //    DateTime _minDate = DateTime.Parse(minDate);
//        //    DateTime _maxDate = DateTime.Parse(maxDate);

//        //    var groupedList = list.Where(x => x.DateOfTrx >= _minDate && x.DateOfTrx <= _maxDate).GroupBy(x => x.Name).Select(y => new DashBoardResult 
//        //    { 
//        //        Name = y.First().Name,
//        //        Amount = y.Count()
            
//        //    })
//        //    .ToList();

//        //    return groupedList;
//        //}
//    }
//}
