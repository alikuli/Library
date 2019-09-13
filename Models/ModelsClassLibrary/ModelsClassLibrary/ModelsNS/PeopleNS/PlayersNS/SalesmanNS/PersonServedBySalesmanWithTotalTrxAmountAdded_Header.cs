using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;

namespace ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS.SalesmanNS
{
    public class PersonServedBySalesmanWithTotalTrxAmountAdded_Header
    {

        public PersonServedBySalesmanWithTotalTrxAmountAdded_Header(string userId, Person salesmanPerson, DateTime beginDate, DateTime endDate, int minimumSaleAmountRequired)
        {
            UserId = userId;

            SalesmanPersonId = salesmanPerson.Id;
            SalesmanPerson = salesmanPerson;
            BeginDate = beginDate;
            EndDate = endDate;
            MinimumSaleAmountRequired = minimumSaleAmountRequired;
        }


        public int MinimumSaleAmountRequired { get; set; }

        public string UserId { get; set; }
        public string SalesmanPersonId { get; set; }
        public Person SalesmanPerson{ get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Heading()
        {
            string heading = string.Format("People you have Sold/Bought Money between {0} and {1}", BeginDate.ToLongDateString(), EndDate.ToLongDateString());
            return heading;
        }



        public List<PersonServedBySalesmanWithTotalTrxAmountAdded> Detail { get; set; }
    }
}
