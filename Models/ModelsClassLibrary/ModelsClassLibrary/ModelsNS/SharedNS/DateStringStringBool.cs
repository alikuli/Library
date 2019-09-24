using System;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class DateStringStringBool : StringStringAndBool
    {
        public DateStringStringBool()
            : base()
        {

        }
        public DateStringStringBool(DateTime date, string str1, string str2, bool select)
            : base(str1, str2, select)
        {
            Date = date;
        }

        public DateTime Date { get; set; }
    }
}
