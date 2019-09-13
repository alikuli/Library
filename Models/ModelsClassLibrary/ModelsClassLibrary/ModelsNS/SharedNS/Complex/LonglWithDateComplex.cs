using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    [ComplexType]
    public class LongWithDateComplex : DateAndByComplex
    {
        public long Amount { get; set; }
        public string Amount_Formatted
        {
            get
            {
                return string.Format("{0:N0}", Amount);
            }
        }

        public void AddOne(string userId, string userName)
        {
            Amount++;
            SetToTodaysDate(userName, userId);

        }
    }
}
