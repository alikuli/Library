using ModelsClassLibrary.CashTrxNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.BuySellDocNS
{
    public class BuySellDocCashTrxApplied : CommonWithId
    {
        public string BuySellDocID { get; set; }
        public virtual BuySellDoc BuySellDoc { get; set; }


        public string CashTrxID { get; set; }
        public virtual CashTrx CashTrx { get; set; }
    }
}
