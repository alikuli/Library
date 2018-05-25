using AliKuli.Extentions;
using ErrorHandlerLibrary.ExceptionsNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.HistoryNS
{
    /// <summary>
    /// This class will be used to calculate clicks and money etc. It makes it easy to graph the information in a manner where you
    /// can compare januarys, febs etc of each year.
    /// </summary>
    [NotMapped]

    public class History
    {


        private const int MAX_NO_OF_YEARS = 13;
        private List<ArrayData> ArrayDataList { get; set; }
        private class ArrayData
        {
            public ArrayData(int year, int month, decimal amount)
            {
                Year = year;
                Month = month;
                Amount = amount;
            }
            public int Year { get; set; }
            public int Month { get; set; }
            public decimal Amount { get; set; }

        }

        //private const int YearsAgo0 = 0;    //Current Year
        //private const int YearAgo1 = 1;     //Last Yr - 1 yr ago
        //private const int YearsAgo2 = 2;    //2 Yrs ago
        //private const int YearsAgo3 = 3;    //3 Yrs ago
        //private const int YearsAgo4 = 4;
        //private const int YearsAgo5 = 5;
        //private const int YearsAgo6 = 6;
        //private const int YearsAgo7 = 7;
        //private const int YearsAgo8 = 8;
        //private const int YearsAgo9 = 9;
        //private const int YearsAgo10 = 10;
        //private const int YearsAgo11 = 11;
        //private const int YearsAgo12 = 12;


        //private const int JAN = 1;
        //private const int FEB = 2;
        //private const int MAR = 3;
        //private const int APR = 4;
        //private const int MAY = 5;
        //private const int JUN = 6;
        //private const int JUL = 7;
        //private const int AUG = 8;
        //private const int SEP = 9;
        //private const int OCT = 10;
        //private const int NOV = 11;
        //private const int DEC = 12;

        /// <summary>
        /// This has an array called Years[Year,Month] which holds the value for that period in decimal form.
        /// </summary>
        public History()
        {
            //Monh number 0 will have all those values that do not fit in
            //months 1-12
            MaxNoOfYears = MAX_NO_OF_YEARS;
            _years = new Decimal[MaxNoOfYears, 13];
            Errors = new ErrorSet();
            Errors.SetLibAndClass("ModelClassLibrary", "History");
            ArrayDataList = new List<ArrayData>();


        }

        public void Add(DateTime? date, decimal amount)
        {

            Date = date ?? DateTime.MinValue;

            ArrayData arrayData = new ArrayData(Date.Year, Date.Month, amount);
            ArrayDataList.Add(arrayData);

        }

        private int MaxNoOfYears { get; set; }
        private decimal[,] _years;
        /// <summary>
        /// This is the array that hold the total History
        /// </summary>
        public Decimal[,] Years
        {
            get
            {
                _years = new decimal[MaxNoOfYears + 1, 13];

                if (!ArrayDataList.IsNullOrEmpty())
                {
                    foreach (var item in ArrayDataList)
                    {
                        int yearsAgo = DateTime.UtcNow.Year - item.Year;
                        _years[yearsAgo, item.Month] += item.Amount;

                        if (MinYear == 0)
                        {
                            MinYear = yearsAgo;
                            MinMonth = item.Month;
                        }

                        if (item.Year <= MinYear)
                        {
                            MinYear = item.Year;
                            if (MinMonth < item.Month)
                            {
                                MinMonth = item.Month;
                            }
                        }
                    }
                }

                return _years;
            }
        }


        /// <summary>
        /// This is the last year of history
        /// </summary>
        public int MinYear { get; set; }
        public int MinMonth { get; set; }




        private DateTime _date;

        /// <summary>
        /// This is the click date and it is used internally only
        /// </summary>
        private DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                DateTime? dateValueNullOrOther = value;

                if (dateValueNullOrOther == DateTime.MinValue)
                {
                    Errors.Add("The date was null.", "Date");
                    throw new Exception(Errors.ToString());
                }

                _date = value;
            }
        }

        private int YearsAgo
        {
            get
            {
                return DateTime.UtcNow.Year - Date.Year;
            }
        }





        private ErrorSet Errors { get; set; }


    }
}
