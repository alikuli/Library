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

        public bool BeginDateGreaterThanEndDate
        {
            get
            {
                //int result = DateTime.Compare(BeginDate, EndDate);
                //return result == 1;
                return Date1GreaterThanDate2(BeginDate, EndDate);
            }
        }

        public bool BeginDateLessThanEndDate
        {
            get
            {
                //int result = DateTime.Compare(BeginDate, EndDate);
                //return result == -1;

                return Date1LessThanDate2(BeginDate, EndDate);
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


        public bool Date1GreaterThanDate2(DateTime date1, DateTime date2)
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
        public bool Date1LessThanDate2(DateTime date1, DateTime date2)
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




        public bool Date1GreaterThanOrEqualToDate2(DateTime date1, DateTime date2)
        {
            if (DatesAreEqual(date1, date2))
                return true;

            if (Date1GreaterThanDate2(date1, date2))
                return true;

            return false;
        }
        public bool Date1LessThanOrEqualDate2(DateTime date1, DateTime date2)
        {
            if (DatesAreEqual(date1, date2))
                return true;

            if (Date1LessThanDate2(date1, date2))
                return true;

            return false;
        }

        public void ErrorCheck()
        {

            isBeginDateGreaterThanEndDateThrowError();
        }



        public bool IsDateWithinBeginAndEndDatesInclusive(DateTime inDate)
        {
            if (Date1LessThanOrEqualDate2(inDate, BeginDate))
                return false;

            if (Date1GreaterThanOrEqualToDate2(inDate, EndDate))
                return false;

            return true;
        }

        private void isBeginDateGreaterThanEndDateThrowError()
        {

            if (BeginDateGreaterThanEndDate)
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
