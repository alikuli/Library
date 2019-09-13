
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
namespace ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS.SalesmanNS
{
    /// <summary>
    /// This is the interim class to get customers for the salesman
    /// </summary>
    public class PersonServedBySalesman
    {
        public DateTime Date { get; set; }
        public string PersonId { get; set; }
        public Person Person { get; set; }
        public decimal Amount { get; set; }
        public bool IsReceivingMoney { get; set; }
    }
}
