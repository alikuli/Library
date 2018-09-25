using AliKuli.Extentions;
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
        public DashBoardSingle(string name, DateTime date, bool isCrawler, string key, double amount, DateTime beginDate, DateTime endDate, string dataType)
            : this()
        {
            Key = key;
            Name = name;
            DateOfTrx = date;
            IsCrawler = isCrawler;
            Amount = amount;
            BeginDate = beginDate;
            EndDate = endDate;
            DataType = dataType;
        }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        /// <summary>
        /// This is what the data will all be grouped for.
        /// </summary>
        public string Key { get; set; }

        public string Name { get; set; }


        [Display(Name = "Date")]
        public DateTime DateOfTrx { get; set; }

        public bool IsCrawler { get; set; }

        public double Amount { get; set; }


        private double _percent;
        public double Percent { get { return _percent; } set { _percent = value; } }


        public string GroupName { get { return Name.RemoveAllSpaces().RemoveAllDashes().ToLower(); } }




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
            string str = string.Format("{0} - {1:n0}", Name, Amount);
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



        public string DataType { get; set; }

        public static string NextDataType(string currDataType)
        {
            switch (currDataType)
            {
                case DashBoardConstants.YEAR:
                    return DashBoardConstants.YEAR_MONTH;

                case DashBoardConstants.YEAR_MONTH:
                    return DashBoardConstants.YEAR_MONTH_DAY;

                case DashBoardConstants.YEAR_MONTH_DAY:
                    return DashBoardConstants.YEAR_MONTH_DAY_HOUR;

                case DashBoardConstants.YEAR_MONTH_DAY_HOUR:
                    return DashBoardConstants.YEAR_MONTH_DAY_HOUR_MINUTE;

                case DashBoardConstants.YEAR_MONTH_DAY_HOUR_MINUTE:
                    return DashBoardConstants.YEAR_MONTH_DAY_HOUR_MINUTE_SECOND;
                default:
                    return DashBoardConstants.YEAR;
            }
        }


    }
}
