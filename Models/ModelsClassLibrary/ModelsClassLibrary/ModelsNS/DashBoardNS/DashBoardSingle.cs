using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelsClassLibrary.ModelsNS.DashBoardNS
{
    public class DashBoardSingle
    {

        public DashBoardSingle()
        {
            //DataGrouped = new List<DashBoardSingle>();
            //DataDetail = new List<DashBoardSingle>();
        }
        public DashBoardSingle(string name, DateTime date, bool isCrawler, string key, double amount, DateTime beginDate, DateTime endDate, string groupBy, string showDataFor, string belongsToGroup)
            : this()
        {
            Key = key;

            Name = name;
            DateOfTrx = date;
            IsCrawler = isCrawler;
            Amount = amount;
            BeginDate = beginDate;
            EndDate = endDate;
            GroupBy = groupBy;
            ShowDataFor = showDataFor;
            BelongsToGroup = belongsToGroup;
        }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        /// <summary>
        /// This is what the data will all be grouped for.
        /// </summary>
        public string Key { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Eg Controller
        /// </summary>
        public string ShowDataFor { get; set; }

        [Display(Name = "Date")]
        public DateTime DateOfTrx { get; set; }

        public bool IsCrawler { get; set; }

        public double Amount { get; set; }
        public double TotalAmount { get; set; }

        private double _percent;
        public double Percent 
        { 
            get 
            {
                if (Amount == 0)
                    return 0;
                if (TotalAmount == 0)
                    return 0;

                return Amount / TotalAmount * 100; 
            } 
            //set { _percent = value; } 
        }


        /// <summary>
        /// This keeps the value of which group the data belongs to.
        /// </summary>
        public string BelongsToGroup { get; set; }


        public string NameCalculated { get; set; }
        public string NameForSorting { get; set; }
        //{
        //    get
        //    {
        //        string newName  = Name +
        //        return 
        //    }
        //}

        public string DisplayName
        {
            get
            {
                string _displayName = string.Format("{0} ({1})", ToString(), PercentString);
                return _displayName;
            }
        }




        public string PercentString
        {
            get
            {
                string str = string.Format("{0:n2}%", Percent);
                return str;
            }
        }




        public override string ToString()
        {
            string str = string.Format("{0} [{1:n0}]", NameCalculated, Amount);
            return str;
        }



        public int ReccursionNum { get; set; }



        /// <summary>
        /// This holds the detailed data for the entire group. Note this definition here is recursive
        /// </summary>
        /// 
        List<DashBoardSingle> _dataDetail;
        public List<DashBoardSingle> DataDetail { get { return (_dataDetail ?? (_dataDetail = new List<DashBoardSingle>())); } set { _dataDetail = value; } }



        /// <summary>
        /// This holds a list of each item. Now within that list of each item, we will find another list of DashBoard items making
        /// each one of these items. Example. If this is a list of years, Each DashBoardGroupItem will contain a list of Months. If it is a 
        /// list of Months, then each DashBoardGroupItem will contain a list of days.
        /// </summary>
        List<DashBoardSingle> _dataGrouped;
        public List<DashBoardSingle> DataGrouped { get { return (_dataGrouped ?? (_dataGrouped = new List<DashBoardSingle>())); } set { _dataGrouped = value; } }



        public string GroupBy { get; set; }

        public static string NextGroupBy(string groupBy)
        {
            switch (groupBy)
            {
                case GroupByConstants.MAIN:
                    return GroupByConstants.NAME;

                case GroupByConstants.NAME:
                    return GroupByConstants.YEAR;

                case GroupByConstants.YEAR:
                    return GroupByConstants.YEAR_MONTH;


                case GroupByConstants.YEAR_MONTH:
                    return GroupByConstants.YEAR_MONTH_DAY;


                case GroupByConstants.YEAR_MONTH_DAY:
                    return GroupByConstants.YEAR_MONTH_DAY_HOUR;


                case GroupByConstants.YEAR_MONTH_DAY_HOUR:
                    return GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE;


                case GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE:
                    return GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE_SECOND;


                case GroupByConstants.YEAR_MONTH_DAY_HOUR_MINUTE_SECOND:
                    return GroupByConstants.DETAIL;

                default:
                    return GroupByConstants.MAIN;

            }
        }






    }
}
