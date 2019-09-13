
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.ComponentModel.DataAnnotations;
namespace ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS.SalesmanNS
{
    public class PersonServedBySalesmanWithTotalTrxAmountAdded
    {
        public string PersonId { get; set; }
        public Person Person { get; set; }
        public decimal Amount { get; set; }

        [Display(Name = "First Date")]
        public DateTime FirstDate { get; set; }
        double DaysAgo_FirstDate()
        {
            return DateTime.Now.Subtract(FirstDate).TotalDays;
        }


        public string DaysAgo_FirstDate_Str()
        {
            if (DaysAgo_FirstDate() == 1)
                return string.Format("({0:N0} day ago)", DaysAgo_FirstDate());
            else
                return string.Format("({0:N0} days ago)", DaysAgo_FirstDate());
        }

        double DaysAgo_LastDate()
        {
            return DateTime.Now.Subtract(LastDate).TotalDays;
        }

        public string DaysAgo_LastDate_Str()
        {
            if (DaysAgo_FirstDate() == 1)
                return string.Format("({0:N0} day ago)", DaysAgo_LastDate());
            else
                return string.Format("({0:N0} days ago)", DaysAgo_LastDate());
        }

        [Display(Name = "Last Date")]
        public DateTime LastDate { get; set; }

    }
}
