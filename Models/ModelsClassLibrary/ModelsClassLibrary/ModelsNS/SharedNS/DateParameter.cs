using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class DateParameter
    {
        public DateParameter()
        {

            BeginDate = DateTime.MinValue;
            EndDate = DateTime.MaxValue;
            MessageList = new List<string>();

        }

        public List<string> MessageList { get; set; }


        DateTime _beginDate;

        [Display(Name = "Begin Date")]
        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime BeginDate
        {
            get { return _beginDate; }
            set
            {
                DateTime temp = value;
                _beginDate = new DateTime(temp.Year, temp.Month, temp.Day, 0, 0, 0, DateTimeKind.Utc);
            }
        }


        DateTime _endDate;
        [Display(Name = "End Date")]
        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                DateTime temp = value;
                _endDate = new DateTime(temp.Year, temp.Month, temp.Day, 23, 59, 59, DateTimeKind.Utc);
            }
        }


        //[Display(Name = "End Date")]
        //[Required(ErrorMessage = "Date is required")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime EndDate { get; set; }


        private int compareDates(DateTime date1, DateTime date2)
        {
            return DateTime.Compare(date1, date2);
        }

        public bool BeginDateAndEndDateAreEqual
        {
            get
            {
                return DatesAreEqual(BeginDate, EndDate);
            }
        }

        public bool BeginDateAfterEndDate
        {
            get
            {
                //int result = DateTime.Compare(BeginDate, EndDate);
                //return result == 1;
                return Date1AfterDate2(BeginDate, EndDate);
            }
        }

        public bool BeginDateBeforeEndDate
        {
            get
            {
                //int result = DateTime.Compare(BeginDate, EndDate);
                //return result == -1;

                return Date1BeforeDate2(BeginDate, EndDate);
            }
        }



        public bool DatesAreEqual(DateTime date1, DateTime date2)
        {
            int result = DateTime.Compare(date1, date2);
            bool success = result == 0;

            string msg;
            if (success)
                msg = string.Format("'{0} {1}' < '{2} {3}'",
                    date1.ToLongDateString(),
                    date1.ToLongTimeString(),
                    date2.ToLongDateString(),
                    date2.ToLongTimeString());
            else
                msg = string.Format("'{0} {1}' < '{2} {3}'",
                    date1.ToLongDateString(),
                    date1.ToLongTimeString(),
                    date2.ToLongDateString(),
                    date2.ToLongTimeString());

            MessageList.Add(msg);
            return success;
        }


        public bool Date1AfterDate2(DateTime date1, DateTime date2)
        {
            int result = DateTime.Compare(date1, date2);
            bool success = result == 1;

            string msg;
            if (success)
                msg = string.Format("'{0} {1}' < '{2} {3}'",
                    date1.ToLongDateString(),
                    date1.ToLongTimeString(),
                    date2.ToLongDateString(),
                    date2.ToLongTimeString());
            else
                msg = string.Format("'{0} {1}' < '{2} {3}'",
                    date1.ToLongDateString(),
                    date1.ToLongTimeString(),
                    date2.ToLongDateString(),
                    date2.ToLongTimeString());

            MessageList.Add(msg);
            return success;

        }
        public bool Date1BeforeDate2(DateTime date1, DateTime date2)
        {
            int result = DateTime.Compare(date1, date2);

            bool success = result == -1;

            string msg;
            if (success)
                msg = string.Format("'{0} {1}' < '{2} {3}'",
                    date1.ToLongDateString(),
                    date1.ToLongTimeString(),
                    date2.ToLongDateString(),
                    date2.ToLongTimeString());
            else
                msg = string.Format("'{0} {1}' < '{2} {3}'",
                    date1.ToLongDateString(),
                    date1.ToLongTimeString(),
                    date2.ToLongDateString(),
                    date2.ToLongTimeString());

            MessageList.Add(msg);
            return success;

        }




        public bool Date1AfterOrEqualToDate2(DateTime date1, DateTime date2)
        {
            if (DatesAreEqual(date1, date2))
                return true;

            if (Date1AfterDate2(date1, date2))
                return true;

            return false;
        }
        public bool Date1BeforeOrEqualToDate2(DateTime date1, DateTime date2)
        {
            if (DatesAreEqual(date1, date2))
                return true;

            if (Date1BeforeDate2(date1, date2))
                return true;

            return false;
        }

        public void ErrorCheck()
        {

            isBeginDateAfterEndDateThrowError();
        }


        public bool IsDateWithinBeginAndEndDatesInclusive(DateTime inDate)
        {
            return IsDateWithinBeginAndEndDatesInclusive(inDate, BeginDate, EndDate);
        }

        public bool IsDateWithinBeginAndEndDatesInclusive(DateTime inDate, DateTime beginDate, DateTime endDate)
        {
            if (Date1BeforeDate2(inDate, beginDate))
                return false;

            if (Date1AfterDate2(inDate, endDate))
                return false;

            return true;
        }

        private void isBeginDateAfterEndDateThrowError()
        {

            if (BeginDateAfterEndDate)
                throw new Exception(string.Format("Begin date: '{0} is greater than the end date: '{1}' This is not allowed!", BeginDate, EndDate));
        }

        //[Display(Name = "First Date Compared To Second Date")]
        //public string FirstDateComparedToSecondDate { get; set; }

        //[Display(Name = "Second Date Compared To First Date")]
        //public string SecondDateComparedToFirstDate { get; set; }

        public string Heading
        {
            get
            {
                string _heading = string.Format("Between {0}, {1} and {2}, {3}",
                    BeginDate.ToLongDateString(),
                    BeginDate.ToLongTimeString(),
                    EndDate.ToLongDateString(),
                    EndDate.ToLongTimeString());
                return _heading;
            }
        }

    }
}
