using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    [ComplexType]
    public class DecimalWithDateComplex : DateAndByComplex
    {
        public decimal Amount { get; set; }
        public string Amount_Formatted
        {
            get
            {
                return string.Format("{0:N2}", Amount);
            }
        }

        public void Add(decimal amount, string userId, string userName)
        {
            Amount = amount;
            SetToTodaysDate(userName, userId);

        }
    }
}
