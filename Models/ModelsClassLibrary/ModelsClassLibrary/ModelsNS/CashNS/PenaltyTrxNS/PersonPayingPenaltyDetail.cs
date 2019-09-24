using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;

namespace ModelsClassLibrary.ModelsNS.CashNS.PenaltyTrxNS
{
    public class PersonPayingPenaltyDetail
    {
        public Person Person { get; set; }
        public Decimal Amount { get; set; }
        public string Comment { get; set; }
        public decimal Percent { get; set; }
        public void Clear()
        {
            Person = null;
            Amount = 0;
            Comment = "";
            Percent = 0;
        }

    }
}
